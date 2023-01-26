using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using System;

namespace BTDMod.Projectiles
{
    class PhoenixBreath : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 18;
            Projectile.penetrate = -1;
            Projectile.alpha = 0;
            Projectile.light = 0.5f;
            Projectile.friendly = true;
            Projectile.timeLeft = 240;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.localNPCHitCooldown = 20;
            Projectile.usesLocalNPCImmunity = true;
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
            SpawnDust();
        }
        // does particle effects
        // copied from joost focussoulsbeam
        private void SpawnDust() {
            Vector2 dustPos = Projectile.Center;
            float num1 = Projectile.velocity.ToRotation() + ((Main.rand.NextBool(2)? -1.0f : 1.0f) * 1.57f);
            float num2 = (float)((Main.rand.NextDouble() * 0.8f) + 1.0f);
            Vector2 dustVel = new((float)Math.Cos(num1) * num2, (float)Math.Sin(num1) * num2);
            Dust dust = Main.dust[Dust.NewDust(dustPos, 0, 0, DustID.Flare, dustVel.X, dustVel.Y)];
            dust.noGravity = true;
            dust.scale = 1.2f;
        }
    }
}