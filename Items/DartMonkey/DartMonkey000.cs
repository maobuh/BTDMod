using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BTDMod.Projectiles;

namespace BTDMod.Items.DartMonkey
{
    class DartMonkey000 : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 30;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Generic;
            Item.shoot = ModContent.ProjectileType<Dart>();
            Item.shootSpeed = 10;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.MowTheLawn;
            Item.rare = ItemRarityID.Green;
            Item.autoReuse = true;
            Item.width = 32;
            Item.height = 32;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dart Monkey 0-0-0");
            Tooltip.SetDefault("shoots dart");
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("IronBar", 5);
            recipe.AddIngredient(ItemID.Wood, 20);
            recipe.AddIngredient(ItemID.Acorn, 1);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}