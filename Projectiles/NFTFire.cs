using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BTDMod.Projectiles
{
    class NFTFire : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.penetrate = -1;
            Projectile.alpha = 0;
            Projectile.friendly = true;
            Projectile.timeLeft = 40;
            Projectile.DamageType = DamageClass.Generic;
        }
    }
}