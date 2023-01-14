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
        // TODO reduce lag by making it like the laser in joost with predraw
        // figure out the best way to get the different stages of damage work with the singular laser projectile
        // its so laggy theres too many projectiles
        int vineRadiusExpansionCooldown;
        int baseUseTime;
        const int maxVineRadius = 40;
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
            for (int k = tileY - player.GetModPlayer<BTDPlayer>().vineRadius; k < tileY + player.GetModPlayer<BTDPlayer>().vineRadius; k++)
            {
                for (int j = tileX - player.GetModPlayer<BTDPlayer>().vineRadius; j <= tileX + player.GetModPlayer<BTDPlayer>().vineRadius; j++)
                {
                    // checks tiles around the player for if they are solid and have an empty space above them
                    if (Main.tile[j, k].TileType != 0 && Main.tileSolid[Main.tile[j, k].TileType] && (!Main.tile[j, k-1].HasTile || !Main.tileSolid[Main.tile[j, k - 1].TileType])) {
                        // recalculate position based off the tile's position in the array
                        Vector2 position = new((j * 16f) - (player.width / 2) + 16f, (k * 16f) + 4);
                        // this calculation is incorrect rn
                        int damage = Item.damage * ((player.GetModPlayer<BTDPlayer>().vineRadius / 20) + 1); // the damage should be a 2:3:4 ratio based on how close the vine is to the player
                        Projectile.NewProjectile(Item.GetSource_ItemUse(Item), position, Vector2.Zero, ModContent.ProjectileType<Vines>(), damage, 0, player.whoAmI);
                    }
                }
                if (vineRadiusExpansionCooldown > 30 && player.GetModPlayer<BTDPlayer>().vineRadius < maxVineRadius) {
                    player.GetModPlayer<BTDPlayer>().vineRadius += 4;
                    vineRadiusExpansionCooldown = 0;
                }
            }
            vineRadiusExpansionCooldown++;
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