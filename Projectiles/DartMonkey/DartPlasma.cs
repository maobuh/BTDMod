using Terraria;
using Terraria.ModLoader;

namespace BTDMod.Projectiles.DartMonkey
{
    class DartPlasma : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Melee;
            Projectile.friendly = true;
            Projectile.height = 32;
            Projectile.width = 32;
            Projectile.penetrate = 5;
            Projectile.localNPCHitCooldown = 60;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.timeLeft = 30;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Plasma");
        }
    }
}