using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BTDMod.Projectiles;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System;

namespace BTDMod.Items.Druid
{
    class Druid024 : ModItem
    {
        int baseUseTime;
        public override void SetDefaults()
        {
            Item.damage = 30;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Magic;
            Item.shoot = ModContent.ProjectileType<WrathThorn>();
            Item.shootSpeed = 10;
            Item.useTime = 27;
            baseUseTime = Item.useTime;
            Item.useAnimation = 27;
            Item.useStyle = ItemUseStyleID.MowTheLawn;
            Item.rare = ItemRarityID.Orange;
            Item.autoReuse = true;
            Item.width = 32;
            Item.height = 32;
            Item.mana = 8;
        }
        public override void HoldItem(Player player)
        {
            // change attack speed based on player health
            // attackSpeedBonus is in % right now, if attackSpeedBonus is 100 that means doubled attack speed
            double attackSpeedBonus = (player.statLifeMax - player.statLife) / 5;
            if (attackSpeedBonus >= 100) {
                attackSpeedBonus = 100;
            }
            // grab the attack speed bonus from the wrath thorn projectile
            attackSpeedBonus += player.GetModPlayer<BTDPlayer>().wrathAttackSpeedBonus;
            // convert attackSpeedBonus into decimal
            attackSpeedBonus /= 100;
            // add the base attack speed to the attack speed bonus
            attackSpeedBonus++;
            Item.useTime = (int) (baseUseTime / attackSpeedBonus);
            Item.useAnimation = Item.useTime;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Druid of Wrath");
            Tooltip.SetDefault("Gains increasing attack speed as long as it damages something" + "\n" +
            "Does more damage in multiplayer");
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
            recipe.AddIngredient(ItemID.HellstoneBar, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}