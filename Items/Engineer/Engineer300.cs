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
	public class Engineer300 : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Engineer Monkey 3-0-0");
			Tooltip.SetDefault("Engineer gaming\n" +
							   "Makes shooty turrets that go pew pew.");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true; // This lets the player target anywhere on the whole screen while using a controller
			ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;
		}

		public override void SetDefaults() {
			Item.damage = 15;
			Item.knockBack = 3f;
			Item.width = 34;
			Item.height = 34;
			Item.useTime = 12;
			Item.useAnimation = 12;
			Item.useStyle = ItemUseStyleID.Swing; // how the player's arm moves when using the item
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item44; // What sound should play when using the item
			Item.autoReuse = true;

			// These below are needed for a minion weapon
			Item.noMelee = true; // this item doesn't do any melee damage
			Item.DamageType = DamageClass.Summon; // Makes the damage register as summon. If your item does not have any damage type, it becomes true damage (which means that damage scalars will not affect it). Be sure to have a damage type
			Item.buffType = ModContent.BuffType<EngineerTurretBuff>();
            Item.shoot = ModContent.ProjectileType<EngineerTurret>(); // This item creates the minion projectile
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
			// Here you can change where the minion is spawned. Most vanilla minions spawn at the cursor position
			int x = (int) Main.MouseWorld.X / 16;
			Vector2? newPosition = null;
			const int maxDrop = 30;
			for (int i = 0; i < maxDrop; i++) {
				int y = ((int) Main.MouseWorld.Y / 16) + i;
				Tile tile = Framing.GetTileSafely(x,y);
				// tile.HasTile && tile.BlockType == BlockType.Solid || Main.tileSolidTop[tile.TileType] doesnt work since
				// it checks solid blocks such as trees/actuated blocks etc.
				// !Collision.CanHit(Main.MouseWorld, 34, 24, Main.MouseWorld + new Vector2(0, i * 16), 34, 24) checks line of sight between 2 positions
				// and does some funky ass stuff and idk why
				if (tile.HasTile && tile.BlockType == BlockType.Solid && Main.tileSolid[tile.TileType] && !tile.IsActuated) {
					newPosition = new Vector2(16 * x, 16 * y);
					break;
				}
			}
			if (newPosition == null) return false;
			position = (Vector2)newPosition;
			// This is needed so the buff that keeps your minion alive and allows you to despawn it properly applies
			player.AddBuff(Item.buffType, 2);
			// Minions have to be spawned manually, then have originalDamage assigned to the damage of the summon item
			var projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, Main.myPlayer);
			projectile.originalDamage = Item.damage;

			// Since we spawned the projectile manually already, we do not need the game to spawn it for ourselves anymore, so return false
			return false;
		}
		public override void AddRecipes() {
			Recipe recipe = Recipe.Create(Item.type);
            recipe.AddRecipeGroup("IronBar", 5);
            recipe.Register();
		}
	}
}