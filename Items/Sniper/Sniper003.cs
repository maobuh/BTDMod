using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BTDMod.Items.Sniper
{
    class Sniper003: ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ModContent.ItemType<Sniper000>());
            Item.damage = 20;
            Item.rare = ItemRarityID.Green;
            Item.useTime = 8;
            Item.knockBack = 2;
            Item.useAnimation = 8;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sniper Monkey 0-0-3");
            Tooltip.SetDefault("Shoots pretty fast");
        }
        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(Item.type);
            recipe.AddIngredient(null, "Sniper000", 1);
            recipe.AddIngredient(ItemID.JungleSpores, 8);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}