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
            Item.damage = 15;
            Item.rare = ItemRarityID.Green;
            Item.useTime = 6;
            Item.knockBack = 2;
            Item.useAnimation = 6;
            Item.shoot = ProjectileID.Bullet;
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
            recipe.Register();
        }
    }
}