using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BTDMod.Projectiles
{
    class Nail : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 6;
            Projectile.penetrate = 3;
            Projectile.alpha = 0;
            Projectile.friendly = true;
            Projectile.scale = 1f;
            Projectile.timeLeft = 180;
            Projectile.DamageType = DamageClass.Summon;
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
        }
    }
}