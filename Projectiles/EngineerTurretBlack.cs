using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using Terraria.ModLoader;
using BTDMod.Buffs;

namespace BTDMod.Projectiles
{
	public class EngineerTurretBlack: EngineerTurret {
        public override bool PreDraw(ref Color lightColor) {
            projColour = "Black";
            return base.PreDraw(ref lightColor);
        }
        public override void AI() {
            projType = ProjectileID.RocketI;
            base.AI();
        }
    }
}