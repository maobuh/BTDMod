using Terraria;
using Terraria.ModLoader;

namespace BTDMod.Projectiles
{
    class VineExplosion : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.damage = 1;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.friendly = true;
            Projectile.height = 34;
            Projectile.width = 34;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 10;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vine Explosion");
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + 0.785398f;
            base.AI();
        }
    }
}