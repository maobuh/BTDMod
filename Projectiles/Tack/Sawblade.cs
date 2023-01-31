using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BTDMod.Projectiles.Tack
{
    class Sawblade : ModProjectile
    {
        bool onspawn = true;
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.penetrate = -1;
            Projectile.alpha = 0;
            Projectile.light = 0.5f;
            Projectile.friendly = true;
            Projectile.scale = 1f;
            Projectile.timeLeft = 360;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 5;
        }
        public override void AI()
        {
            if (onspawn) {
                Projectile.rotation = Projectile.velocity.ToRotation();
                onspawn = false;
            }
            Projectile.rotation += 0.1f;
        }
    }
}