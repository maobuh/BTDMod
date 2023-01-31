using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BTDMod.Projectiles;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System;

namespace BTDMod.Items.TackShooter
{
    class Tack004 : ModItem
    {
        const double DEGREE = Math.PI / 180;
        const int NUM_NAILS = 16;
        public override void SetDefaults()
        {
            Item.damage = 60;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Melee;
            Item.shoot = ModContent.ProjectileType<Nail>();
            Item.shootSpeed = 18;
            Item.useTime = 6;
            Item.useAnimation = 6;
            Item.knockBack = 3;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Pink;
            Item.autoReuse = true;
            Item.width = 36;
            Item.height = 36;
            Item.noUseGraphic = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tack Shooter 0-0-4");
            Tooltip.SetDefault("Turned black from dead minimum wage monkeys.\n" +
                               "Mixed well with the souls");
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			const double spread = 360 / NUM_NAILS * DEGREE;
            float baseSpeed = velocity.Length();
            double baseAngle = Math.Atan2(velocity.X, velocity.Y);
			// makes nails shoot out in a circle
			for (int i = 0; i < NUM_NAILS; i++)
			{
				double angle = baseAngle + (i * spread);
				Vector2 newVelocity = new(baseSpeed * (float)Math.Sin(angle), baseSpeed * (float)Math.Cos(angle));
				Projectile.NewProjectile(source, position, newVelocity, type, damage, 0, player.whoAmI);
			}
			return false;
		}
        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(Item.type);
            recipe.AddIngredient(null, "Tack003", 1);
            recipe.AddIngredient(ItemID.SoulofFright, 1);
            recipe.AddIngredient(ItemID.SoulofSight, 1);
            recipe.AddIngredient(ItemID.SoulofMight, 1);
            recipe.AddIngredient(ItemID.SoulofFlight, 1);
            recipe.AddIngredient(ItemID.SoulofLight, 1);
            recipe.AddIngredient(ItemID.SoulofNight, 1);
            recipe.Register();
        }
    }
}