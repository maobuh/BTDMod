using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BTDMod.Projectiles;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System;

namespace BTDMod.Items.TackShooter
{
    class Tack400 : ModItem
    {
        const int NUM_NAILS = 18;
        double baseAngle;
        public override void SetDefaults()
        {
            Item.damage = 20;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Melee;
            Item.shoot = ModContent.ProjectileType<FlameBreath>();
            Item.shootSpeed = 18;
            Item.useTime = 6;
            Item.useAnimation = 6;
            Item.knockBack = 3;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Green;
            Item.autoReuse = true;
            Item.width = 36;
            Item.height = 36;
            Item.noUseGraphic = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tack Shooter 4-0-0");
            Tooltip.SetDefault("Flames of anger");
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			const float spread = 2 * (float)Math.PI / NUM_NAILS;
            float baseSpeed = velocity.Length();
            baseAngle += 0.1f;
            double angle = baseAngle;
			// makes flames shoot out in a circle
			for (int i = 0; i < NUM_NAILS; i++)
			{
				Vector2 newVelocity = new(baseSpeed * (float)Math.Sin(angle), baseSpeed * (float)Math.Cos(angle));
				Projectile proj = Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, 0, player.whoAmI);
                proj.scale = 5f;
                proj.tileCollide = false;
                angle += spread;
			}
			return false;
		}
        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(Item.type);
            recipe.AddIngredient(null, "Tack000", 1);
            recipe.AddIngredient(ItemID.HellstoneBar, 3);
            recipe.Register();
        }
    }
}