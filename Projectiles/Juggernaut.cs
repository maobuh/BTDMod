using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;

namespace BTDMod.Projectiles
{
    class Juggernaut : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.height = 34;
            Projectile.width = 34;
            
        }
        public override void SetStaticDefaults()
        {
            
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
			SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            return true;
        }
    }
}