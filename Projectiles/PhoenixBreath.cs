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
        }
    }
}