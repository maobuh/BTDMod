using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BTDMod.Projectiles;

namespace BTDMod.Items.Wizard
{
    class Wizard400: ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ModContent.ItemType<Wizard000>());
            Item.damage = 30;
            Item.rare = ItemRarityID.Orange;
            Item.shoot = ModContent.ProjectileType<ArchmageBullet>();
            Item.shootSpeed = 20f;
            Item.useTime = 12;
            Item.useAnimation = 12;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wizard Monkey 4-0-0");
            Tooltip.SetDefault("In the midst of secretly attending a Primary Mentoring\nSession, the wizard remembered how to home their shots.");
        }
        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(Item.type);
            recipe.AddIngredient(null, "Wizard300", 1);
            recipe.AddIngredient(ItemID.PixieDust, 10);
            recipe.AddTile(TileID.Bookcases);
            recipe.Register();
        }
    }
}