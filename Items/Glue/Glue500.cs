using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BTDMod.Projectiles.Glue;

namespace BTDMod.Items.Glue
{
    class Glue500 : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 1;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<GlueAcidPlus>();
            Item.shootSpeed = 16;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.LightRed;
            Item.autoReuse = true;
            Item.width = 28;
            Item.height = 14;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Glue 5-0-0");
            Tooltip.SetDefault("chemical warfare");
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Glue400", 1);
            recipe.AddIngredient(ItemID.FlaskofCursedFlames, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}