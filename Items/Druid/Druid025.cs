using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BTDMod.Projectiles;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System;

namespace BTDMod.Items.Druid
{
    class Druid025 : ModItem
    {
        int baseUseTime;
        int baseDamage;
        int extraDamage;
        public override void SetDefaults()
        {
            Item.damage = 80;
            baseDamage = Item.damage;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Magic;
            Item.shoot = ModContent.ProjectileType<LightningOfWrath>();
            Item.shootSpeed = 10;
            Item.useTime = 27;
            baseUseTime = Item.useTime;
            Item.useAnimation = 27;
            Item.useStyle = ItemUseStyleID.MowTheLawn;
            Item.rare = ItemRarityID.Lime;
            Item.autoReuse = true;
            Item.width = 32;
            Item.height = 32;
            Item.mana = 10;
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

            // change damage based on the health of enemies on the screen
            extraDamage = 0;
            for (int i = 0; i < Main.maxNPCs; i++) {
                if (Main.npc[i].active) {
                    extraDamage++;
                }
            }
            Item.damage = baseDamage + extraDamage;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Avatar of Wrath");
            Tooltip.SetDefault("Gains more damage the more enemies are around you");
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
				Projectile.NewProjectile(source, position, newVelocity, type, damage, 0, player.whoAmI, extraDamage / 25);
			}
			return false;
		}
        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(Item.type);
            recipe.AddIngredient(null, "Druid024", 1);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 5);
            recipe.AddIngredient(ItemID.SoulofMight, 5);
            recipe.AddIngredient(ItemID.SoulofNight, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}