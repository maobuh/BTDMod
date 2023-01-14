using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using System;

namespace BTDMod.Projectiles
{
    class FlameBreath : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 18;
            Projectile.penetrate = -1;
            Projectile.alpha = 0;
            Projectile.light = 0.5f;
            Projectile.friendly = true;
            Projectile.timeLeft = 45;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.localNPCHitCooldown = 10;
            Projectile.usesLocalNPCImmunity = true;
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
            // slows down projectile exponentially
            Projectile.velocity = 0.95f * Projectile.velocity;
        }
    }
}