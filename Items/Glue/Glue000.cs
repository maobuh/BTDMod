using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BTDMod.Projectiles.Glue;

namespace BTDMod.Items.Glue
{
    class Glue000 : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 1;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<GlueBall>();
            Item.shootSpeed = 10;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Green;
            Item.autoReuse = true;
            Item.width = 28;
            Item.height = 20;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Glue 0-0-0");
            Tooltip.SetDefault("shoots glue, doesnt work on bosses");
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Wood, 5);
            recipe.AddIngredient(ItemID.HoneyBlock, 1);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}