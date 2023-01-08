using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BTDMod.Projectiles
{
    class Juggernaut : ModProjectile
    {
        public override void SetDefaults()
        {
            
        }
        public override void SetStaticDefaults()
        {
            
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return true;
        }
    }
}