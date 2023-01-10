using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BTDMod.Projectiles;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System;

namespace BTDMod.Items.Druid
{
    class Druid052 : ModItem
    {
        int thornRadius = 1;
        int baseUseTime;
        public override void SetDefaults()
        {
            Item.damage = 1;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Generic;
            Item.shoot = ModContent.ProjectileType<Thorn>();
            Item.shootSpeed = 10;
            Item.useTime = 27;
            baseUseTime = Item.useTime;
            Item.useAnimation = 1;
            Item.useStyle = ItemUseStyleID.MowTheLawn;
            Item.rare = 2;
            Item.autoReuse = true;
            Item.width = 32;
            Item.height = 32;
        }
        public override void HoldItem(Player player)
        {
            // change attack speed based on player health
            int attackSpeedBonus = (player.statLifeMax - player.statLife) / 5;
            if (attackSpeedBonus >= 100) {
                Item.useTime = baseUseTime / 2;
            } else {
                Item.useTime = baseUseTime * 100 / (100 + attackSpeedBonus);
            }
            // spawn vines around the player
            // find tile adjacent to player, code adapted from Player.AdjTiles()
            // Player.AdjTiles is originally used for finding crafting stations around the player
            // num3, num4 is the player's position converted into tiles
            // no idea why they do width / 2 and not height / 2
            int num3 = (int)((player.position.X + (player.width / 2)) / 16f);
            int num4 = (int)((player.position.Y + player.height) / 16f);
            for (int j = num3 - thornRadius; j <= num3 + thornRadius; j++)
            {
                for (int k = num4 - thornRadius; k < num4 + thornRadius; k++)
                {
                    Main.NewText(Main.tile[j, k].TileType);
                    if (Main.tile[j, k].TileType != 0) {
                        // recalculate position based off
                        Vector2 position = new((j * 16f) - (player.width / 2) + 16f, (k * 16f) + 4);
                        Projectile.NewProjectile(Item.GetSource_FromThis(), position, Vector2.Zero, ModContent.ProjectileType<FloorThorns>(), Item.damage, 0, player.whoAmI);
                    }
                    // if (NPC.IsValidSpawningGroundTile(j, k)) {
                    //     
                    // }
                }
            }
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spirit of the Forest");
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			// 5 degrees but in radians for the spread of the projectiles
			const float spread = 0.261799f;
			// makes 8 projectile when shoot
			for (int i = 0; i < 8; i++)
			{
				// math for making random new angle
				float baseSpeed = velocity.Length();
				double baseAngle = Math.Atan2(velocity.X, velocity.Y);
				double randomAngle = baseAngle + ((Main.rand.NextFloat() - 0.5f) * spread);
				Vector2 newVelocity = new(baseSpeed * (float)Math.Sin(randomAngle), baseSpeed * (float)Math.Cos(randomAngle));
				// make new projectile
				Projectile.NewProjectile(source, position, newVelocity, type, 1, 0, player.whoAmI);
			}
			return false;
		}
    }
}