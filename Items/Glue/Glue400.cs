using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BTDMod.Projectiles.Glue;

namespace BTDMod.Items.Glue
{
    class Glue400 : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 1;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<GlueAcid>();
            Item.shootSpeed = 14;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Green;
            Item.autoReuse = true;
            Item.width = 28;
            Item.height = 14;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Glue 4-0-0");
            Tooltip.SetDefault("shoots acid");
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Glue000", 1);
            recipe.AddIngredient(ItemID.FlaskofPoison, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}