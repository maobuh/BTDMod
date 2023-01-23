using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using System;
using BTDMod.Items.Druid;
using BTDMod.Buffs;

namespace BTDMod.Projectiles
{
    class Vines : ModProjectile
    {
        // Projectile.ai[0] is the item shoot speed
        // Projectile.ai[1] is which spirte the projectile will use based on how close the vine is to the player when it spawned
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
            Projectile.localNPCHitCooldown = 30;
            Projectile.timeLeft = 120;
            Projectile.light = 10f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vines");
			Main.projFrames[Projectile.type] = 3;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            if (Projectile.ai[1] > 2) {
                Projectile.frame = 2;
            }
            Projectile.frame = (int) Projectile.ai[1];
            return base.PreDraw(ref lightColor);
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            // vine boom
            if (BTDMod.MonkeyAbilityHotKey.JustPressed && player.HeldItem.type == ModContent.ItemType<Druid052>() && !player.HasBuff(ModContent.BuffType<VineExplosionCooldown>())) {
                explode = true;
                // vines closer to the player explode earlier than the ones further away
                // no idea by how much tho just felt right
                Projectile.timeLeft = (int)(Projectile.Center - player.Center).Length() / 40 * 4;
            }
            // change projectile frame based on how close the vine is to the player
            base.AI();
        }
        public override void Kill(int timeLeft)
        {
            Player player = Main.player[Projectile.owner];
            if (explode) {
                // once per vine
                double angle = Math.PI * 2 / numThorns;
                for (int i = 0; i < numThorns; i++) {
                    Vector2 velocity = new((float) Math.Cos(angle), (float) Math.Sin(angle));
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, velocity * Projectile.ai[0], ModContent.ProjectileType<VineExplosion>(), Projectile.damage * 10, 0, Projectile.owner);
                    angle += Math.PI * 2 / numThorns;
                    player.GetModPlayer<BTDPlayer>().vineRadius = 4;
                }
                if (Projectile.ai[1] > 2) {
                    player.AddBuff(ModContent.BuffType<VineExplosionCooldown>(), 360);
                }
            }
        }
    }
}