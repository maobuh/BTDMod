using Terraria;
using Terraria.ModLoader;

namespace BTDMod.Projectiles
{
    class Vines : ModProjectile
    {
        // TODO make the vines have different sprites the closer to the player they are
        // three stages just like in balon game
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
            DisplayName.SetDefault("Vines");
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
            base.AI();
        }
    }
}