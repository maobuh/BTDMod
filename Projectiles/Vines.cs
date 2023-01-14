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
        const int numThorns = 5;
        bool explode;
        public override void SetDefaults()
        {
            Projectile.damage = 1;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.friendly = true;
            Projectile.height = 34;
            Projectile.width = 34;
            Projectile.penetrate = -1;
            Projectile.localNPCHitCooldown = 10;
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
                // this is the justin case
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
                // no idea by how much tho just felt right
                Projectile.timeLeft = (int)(Projectile.Center - player.Center).Length() / 40 * 4;
            }
            base.AI();
        }
        public override void Kill(int timeLeft)
        {
            Player player = Main.player[Projectile.owner];
            if (explode) {
                // once per tile that the vine covers
                double angle = Math.PI * 2 / numThorns;
                for (int i = 0; i < numThorns; i++) {
                    Vector2 velocity = new((float) Math.Cos(angle), (float) Math.Sin(angle));
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, velocity * Projectile.ai[0], ModContent.ProjectileType<VineExplosion>(), Projectile.damage * 75, 0, Projectile.owner);
                    angle += Math.PI * 2 / numThorns;
                    player.GetModPlayer<BTDPlayer>().vineRadius = 4;
                }
            }
        }
    }
}