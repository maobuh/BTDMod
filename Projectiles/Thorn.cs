using Terraria;
using Terraria.ModLoader;

namespace BTDMod.Projectiles
{
    class Thorn : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.damage = 1;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.friendly = true;
            Projectile.height = 32;
            Projectile.width = 48;
            Projectile.penetrate = 1;
            Projectile.extraUpdates = 2;
            Projectile.localNPCHitCooldown = 0;
            Projectile.scale = 0.5f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Thorn");
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
            base.AI();
        }
    }
}