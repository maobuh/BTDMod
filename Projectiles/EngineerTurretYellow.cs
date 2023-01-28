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
	public class EngineerTurretYellow: EngineerTurret {
        public override bool PreDraw(ref Color lightColor) {
            projColour = "Yellow";
            return base.PreDraw(ref lightColor);
        }
        public override void AI() {
            projType = ProjectileID.MiniRetinaLaser;
            base.AI();
        }
    }
}