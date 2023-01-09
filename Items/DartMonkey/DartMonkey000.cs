using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BTDMod.Projectiles;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace BTDMod.Items.DartMonkey
{
    class DartMonkey000 : ModItem
    {
        public override void SetDefaults()
        {
            // todo balance for early game
            Item.damage = 5;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Generic;
            Item.shoot = ModContent.ProjectileType<Dart>();
            Item.shootSpeed = 10;
            Item.useTime = 30;
            Item.useAnimation =30;
            Item.useStyle = ItemUseStyleID.MowTheLawn;
            Item.rare = 2;
            Item.autoReuse = true;
            Item.width = 32;
            Item.height = 32;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dart Monkey 0-0-0");
            Tooltip.SetDefault("");
        }
    }
}