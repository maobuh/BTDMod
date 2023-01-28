using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System;
using System.Linq;
using BTDMod.Buffs;

namespace BTDMod.Projectiles.Boomerang
{
    class MOABPress : KylieBoomerang
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.localNPCHitCooldown = 6;
        }
    }
}