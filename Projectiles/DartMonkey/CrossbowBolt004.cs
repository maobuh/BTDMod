using Terraria;
using Terraria.ModLoader;

namespace BTDMod.Projectiles.DartMonkey
{
    class CrossbowBolt004 : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Melee;
            Projectile.friendly = true;
            Projectile.height = 28;
            Projectile.width = 64;
            Projectile.penetrate = 1;
            Projectile.localNPCHitCooldown = 0;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.timeLeft = 120;
            Projectile.scale = 0.4f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crossbow Bolt");
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
            base.AI();
        }
    }
}