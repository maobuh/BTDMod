using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BTDMod.Projectiles;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System;

namespace BTDMod.Items.Druid
{
    class Druid032 : ModItem
    {
        // TODO the mana cost when holding the item is inconsistent; doesnt use any mana until max vine radius where it suddenly deletes all ur mana
        // test the fix
        int vineRadiusExpansionCooldown;
        int baseUseTime;
        const int maxVineRadius = 40;
        public override void SetDefaults()
        {
            Item.damage = 20;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Magic;
            Item.shoot = ModContent.ProjectileType<Thorn>();
            Item.shootSpeed = 10;
            Item.useTime = 27;
            baseUseTime = Item.useTime;
            Item.useAnimation = 27;
            Item.useStyle = ItemUseStyleID.MowTheLawn;
            Item.rare = ItemRarityID.Green;
            Item.autoReuse = true;
            Item.width = 32;
            Item.height = 32;
            Item.mana = 5;
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
            Item.useAnimation = Item.useTime;
            // spawn vines around the player
            // find tile adjacent to player, code adapted from Player.AdjTiles()
            // Player.AdjTiles is originally used for finding crafting stations around the player
            // tileX, tileY is the player's position converted into tiles
            // no idea why they do width / 2 and not height / 2
            int tileX = (int)((player.position.X + (player.width / 2)) / 16f);
            int tileY = (int)((player.position.Y + player.height) / 16f);
            for (int j = tileX - player.GetModPlayer<BTDPlayer>().vineRadius; j <= tileX + player.GetModPlayer<BTDPlayer>().vineRadius; j++)
            {
                for (int k = tileY - player.GetModPlayer<BTDPlayer>().vineRadius; k < tileY + player.GetModPlayer<BTDPlayer>().vineRadius; k++)
                {
                    // checks tiles around the player for if they are solid and have an empty space above them
                    if (Main.tile[j, k].TileType != 0 && Main.tileSolid[Main.tile[j, k].TileType] && (!Main.tile[j, k-1].HasTile || !Main.tileSolid[Main.tile[j, k - 1].TileType])) {
                        // recalculate position based off the tile's position in the array
                        Vector2 position = new((j * 16f) - (player.width / 2) + 16f, (k * 16f) + 4);
                        if (!Array.Exists(Main.projectile, element => element.Center == position)) {
                            Projectile.NewProjectile(Item.GetSource_ItemUse(Item), position, Vector2.Zero, ModContent.ProjectileType<Vines>(), Item.damage / 2, 0, player.whoAmI, Item.shootSpeed, 2);
                        }
                    }
                }
            }
            if (vineRadiusExpansionCooldown > 30 && player.statMana > 10) {
                if (player.GetModPlayer<BTDPlayer>().vineRadius < maxVineRadius) {
                    player.GetModPlayer<BTDPlayer>().vineRadius += 4;
                }
                player.statMana -= 5;
                vineRadiusExpansionCooldown = 0;
            }
            vineRadiusExpansionCooldown++;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Druid of the Jungle");
            Tooltip.SetDefault("Summons spiky vines on blocks around the player.");
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			// 15 degrees but in radians for the spread of the projectiles
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
				Projectile.NewProjectile(source, position, newVelocity, type, damage, 0, player.whoAmI);
			}
			return false;
		}
        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(Item.type);
            recipe.AddIngredient(null, "Druid022", 1);
            recipe.AddIngredient(ItemID.Vine, 1);
            recipe.AddIngredient(ItemID.JungleSpores, 2);
            recipe.AddTile(TileID.Anvils);

            Recipe recipe2 = recipe.Clone();
            recipe.AddIngredient(ItemID.DemoniteBar, 5);
            recipe2.AddIngredient(ItemID.CrimtaneBar, 5);
            recipe2.Register();
        }
    }
}