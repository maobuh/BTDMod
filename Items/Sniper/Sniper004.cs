using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace BTDMod.Items.Sniper
{
    class Sniper004: ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ModContent.ItemType<Sniper000>());
            Item.damage = 30;
            Item.rare = ItemRarityID.LightRed;
            Item.useTime = 6;
            Item.knockBack = 2;
            Item.useAnimation = 6;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sniper Monkey 0-0-4");
            Tooltip.SetDefault("Shoots very fast");
        }
        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(Item.type);
            recipe.AddIngredient(null, "Sniper003", 1);

            Recipe recipe2 = recipe.Clone();

            recipe.AddIngredient(ItemID.TitaniumBar, 5);
            recipe.Register();

            recipe2.AddIngredient(ItemID.AdamantiteBar, 5);
            recipe2.Register();
        }
    }
}