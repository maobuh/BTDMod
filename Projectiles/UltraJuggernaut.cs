using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;

namespace BTDMod.Projectiles
{
    class UltraJuggernaut : ModProjectile
    {
        int totalCollisions;
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ultra Juggernaut");
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            totalCollisions++;
            if (totalCollisions > 1)
            {
                Projectile.velocity = -oldVelocity;
                Projectile.NewProjectile()
            }
            else {
                return false;
            }
			SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            return false;
        }
    }
}