using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BTDMod.Projectiles;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System;

namespace BTDMod.Items.Druid
{
    class Druid022 : ModItem
    {
        int baseUseTime;
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
            Item.rare = ItemRarityID.Blue;
            Item.autoReuse = true;
            Item.width = 32;
            Item.height = 32;
            Item.mana = 3;
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
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Heart of Vengeance");
            Tooltip.SetDefault("Gains 10% attack speed for each heart lost");
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
            recipe.AddIngredient(null, "Druid000", 1);
            recipe.AddIngredient(ItemID.Cactus, 5);
            recipe.AddIngredient(ItemID.Wood, 1);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}