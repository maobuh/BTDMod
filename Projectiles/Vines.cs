using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;
using System;

namespace BTDMod.Projectiles
{
    class Vines : ModProjectile
    {
        // TODO make the vines have different sprites the closer to the player they are
        // three stages just like in balon game
        // Projectile.ai[0] to Projectile.ai[1] will the x values of the position where the vines need to be drawn to and from
        const float vineSpacing = 8f;
        bool explode;
        public override void SetDefaults()
        {
            Projectile.damage = 1;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.friendly = true;
            Projectile.height = 34;
            Projectile.width = 34;
            Projectile.penetrate = -1;
            Projectile.extraUpdates = 2;
            Projectile.localNPCHitCooldown = 0;
            Projectile.timeLeft = 180;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vines");
        }
        public override void PostDraw(Color lightColor)
        {
            // kill any new vines that spawn on top of this one
            // in postdraw only cause it happens after setdefaults
            if (Array.Exists(Main.projectile, element => element.position == Projectile.position) && !Array.Find(Main.projectile, element => element.position == Projectile.position).Equals(Projectile)) {
                explode = false;
                Projectile.Kill();
            }
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            // vine boom
            if (BTDMod.MonkeyAbilityHotKey.JustPressed) {
                explode = true;
                // vines closer to the player explode earlier than the ones further away
                // 40 tiles(max vine range) * 16 pixels per tile
                Projectile.timeLeft = (int)(Projectile.Center - player.Center).Length() / 40 * 16;
            }
            base.AI();
        }
        public override void Kill(int timeLeft)
        {
            if (explode) {
                // once per tile that the vine covers
                for (int i = 0; i < (Projectile.ai[1] - Projectile.ai[0]) / 16; i++) {
                    Vector2 velocity = new();
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, velocity, ModContent.ProjectileType<VineExplosion>(), Projectile.damage * 75, 0, Projectile.owner);
                }
            }
        }
    }
}