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
			DisplayName.SetDefault("Engineer Monkey 4-0-0");
			Tooltip.SetDefault("Gave the turrets a paint job and tried inserting all\n" +
							   "sorts of projectile in there. :flushed:");
		}

		public override void SetDefaults() {
			base.SetDefaults();
			Item.damage = 50;
			Item.rare = ItemRarityID.Orange;
			// without setting shoot here as well, the first turret that spawns when u go ingame is the default green one
			// only setting this in defaults and not shoot, randomises the turrets only once, meaning the same turret will always spawn
			RandomiseTurret();
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
			RandomiseTurret();
			return base.Shoot(player, source, position, velocity, type, damage, knockback);
		}
		public override void AddRecipes() {
			Recipe recipe = Recipe.Create(Item.type);
            recipe.AddIngredient(ItemID.UnicornHorn);
			recipe.AddIngredient(null, "Engineer300", 1);
			recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
		}
		private void RandomiseTurret() {
			switch (Main.rand.Next(0,4))
			{
				case 0: Item.shoot = ModContent.ProjectileType<EngineerTurretRed>(); break;
				case 1: Item.shoot = ModContent.ProjectileType<EngineerTurretBlack>(); break;
				case 2: Item.shoot = ModContent.ProjectileType<EngineerTurretWhite>(); break;
				case 3: Item.shoot = ModContent.ProjectileType<EngineerTurretYellow>(); break;
			}
		}
	}
}