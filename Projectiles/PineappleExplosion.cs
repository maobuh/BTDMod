using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace BTDMod.Projectiles
{
    class PineappleExplosion : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Generic;
            Projectile.friendly = true;
            Projectile.height = 256;
            Projectile.width = 256;
            Projectile.penetrate = 32767;
            Projectile.timeLeft = 16;
            Projectile.tileCollide = false;
            Projectile.light = 10;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pineapple Explosion");
        }
        public override void AI()
        {
            if (Projectile.timeLeft % 2 == 0) {
                Projectile.position += Vector2.One;
            } else {
                Projectile.position -= Vector2.One;
            }
        }
    }
}