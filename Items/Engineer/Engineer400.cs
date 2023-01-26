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
	public class Engineer400 : Engineer300
	{
		public override void SetStaticDefaults() {
			base.SetStaticDefaults();
			DisplayName.SetDefault("Engineer 4-0-0");
			Tooltip.SetDefault("Engineer gaming");
		}

		public override void SetDefaults() {
			base.SetDefaults();
			Item.damage = 35;
			Item.rare = ItemRarityID.Orange;
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
			switch (Main.rand.Next(0,4))
			{
				case 0: Item.shoot = ModContent.ProjectileType<EngineerTurretRed>(); break;
				case 1: Item.shoot = ModContent.ProjectileType<EngineerTurretBlack>(); break;
				case 2: Item.shoot = ModContent.ProjectileType<EngineerTurretWhite>(); break;
				case 3: Item.shoot = ModContent.ProjectileType<EngineerTurretYellow>(); break;
			}
			return base.Shoot(player, source, position, velocity, type, damage, knockback);
		}
		public override void AddRecipes() {
			Recipe recipe = Recipe.Create(Item.type);
            recipe.AddIngredient(ItemID.UnicornHorn);
			recipe.AddIngredient(null, "Engineer300", 1);
			recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
		}
	}
}