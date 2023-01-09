using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BTDMod.Projectiles;

namespace BTDMod.Items.Sniper
{
    class Sniper030: ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ModContent.ItemType<Sniper000>());
            Item.damage = 30;
            Item.rare = ItemRarityID.Green;
            Item.shoot = ModContent.ProjectileType<BouncingBullet>();
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sniper Monkey 0-3-0");
            Tooltip.SetDefault("Bullets bounce up to 3 times on nearby enemies");
        }
        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(Item.type);
            recipe.AddIngredient(null, "Sniper000", 1);

            Recipe recipe2 = recipe.Clone();

            recipe.AddIngredient(ItemID.DemoniteBar, 5);
            recipe.Register();

            recipe2.AddIngredient(ItemID.CrimtaneBar, 5);
            recipe2.Register();
        }
    }
}