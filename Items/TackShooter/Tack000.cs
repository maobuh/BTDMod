using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BTDMod.Projectiles;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System;

namespace BTDMod.Items.TackShooter
{
    class Tack000 : ModItem
    {
        const int NUM_NAILS = 8;
        public override void SetDefaults()
        {
            Item.damage = 10;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Melee;
            Item.shoot = ModContent.ProjectileType<Nail>();
            Item.shootSpeed = 18;
            Item.useTime = 14;
            Item.useAnimation = 14;
            Item.knockBack = 3;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Blue;
            Item.autoReuse = true;
            Item.width = 36;
            Item.height = 36;
            Item.noUseGraphic = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tack Shooter 0-0-0");
            Tooltip.SetDefault("Monkey Slavery :D, happy to receive an acorn for their ration");
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			const float spread = 2 * (float)Math.PI / NUM_NAILS;
            float baseSpeed = velocity.Length();
            double angle = Math.Atan2(velocity.X, velocity.Y);
			// makes nails shoot out in a circle
			for (int i = 0; i < NUM_NAILS; i++)
			{
				Vector2 newVelocity = new(baseSpeed * (float)Math.Sin(angle), baseSpeed * (float)Math.Cos(angle));
				Projectile.NewProjectile(source, position, newVelocity, type, damage, 0, player.whoAmI);
                angle += spread;
			}
			return false;
		}
        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(Item.type);
            recipe.AddIngredient(ItemID.Acorn, 1);
            recipe.Register();
        }
    }
}