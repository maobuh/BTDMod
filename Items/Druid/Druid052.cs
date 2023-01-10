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
        // TODO increase thornradius over time while player has item equipped
        // reset thornradius when player unequips item
        // why do vines spawn on tile borders - find out and fix
        int thornRadius = 10;
        int baseUseTime;
        public override void SetDefaults()
        {
            Item.damage = 1;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Magic;
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
            // tileX, tileY is the player's position converted into tiles
            // no idea why they do width / 2 and not height / 2
            int tileX = (int)((player.position.X + (player.width / 2)) / 16f);
            int tileY = (int)((player.position.Y + player.height) / 16f);
            for (int j = tileX - thornRadius; j <= tileX + thornRadius; j++)
            {
                for (int k = tileY - thornRadius; k < tileY + thornRadius; k++)
                {
                    if (Main.tile[j, k].TileType != 0 && Main.tile[j, k-1].TileType == 0) {
                        // recalculate position based off the tile shown
                        Vector2 position = new((j * 16f) - (player.width / 2) + 16f, (k * 16f) + 4);
                        Projectile.NewProjectile(Item.GetSource_FromThis(), position, Vector2.Zero, ModContent.ProjectileType<Vines>(), Item.damage, 0, player.whoAmI);
                    }
                }
            }
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spirit of the Forest");
            Tooltip.SetDefault("Summons spiky vines on blocks around the player." +
            "\n" + "Press the Monkey Ability Hotkey to explode these vines for l a r g e damage." +
            "\n" + "The closer the vines are to the player, the more damage they do (both passively and when they explode)");
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