using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BTDMod.Projectiles;

namespace BTDMod.Items.Wizard
{
    class Wizard300: ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ModContent.ItemType<Wizard000>());
            Item.damage = 30;
            Item.rare = ItemRarityID.Green;
            Item.shoot = ModContent.ProjectileType<MageBullet>();
            Item.shootSpeed = 20f;
            Item.useTime = 20;
            Item.useAnimation = 20;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wizard Monkey 3-0-0");
            Tooltip.SetDefault("Harder, Better, Faster, Stronger");
        }
        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(Item.type);
            recipe.AddIngredient(null, "Wizard000", 1);
            recipe.AddIngredient(ItemID.Book, 3);
            recipe.AddTile(TileID.Bookcases);
            recipe.Register();
        }
    }
}