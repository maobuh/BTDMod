using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using BTDMod.Projectiles;
using BTDMod.Buffs;

namespace BTDMod.Items.Engineer
{
	public class Engineer500 : Engineer300
	{
		public override void SetStaticDefaults() {
			base.SetStaticDefaults();
			DisplayName.SetDefault("Engineer Monkey 5-0-0");
			Tooltip.SetDefault("Made their turrets join the Plasma Monkey Fanclub.");
		}

		public override void SetDefaults() {
			base.SetDefaults();
			Item.damage = 80;
			Item.rare = ItemRarityID.Cyan;
			Item.shoot = ModContent.ProjectileType<EngineerTurretPurple>();
		}
		public override void AddRecipes() {
			Recipe recipe = Recipe.Create(Item.type);
            recipe.AddIngredient(ItemID.LunarBar, 3);
			recipe.AddIngredient(null, "Engineer400", 1);
			recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
		}
	}
}