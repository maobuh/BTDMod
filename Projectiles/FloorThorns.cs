using Terraria;
using Terraria.ModLoader;

namespace BTDMod.Projectiles
{
    class FloorThorns : ModProjectile
    {
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
            Projectile.timeLeft = 10;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Floor Thorn");
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
            base.AI();
        }
    }
}