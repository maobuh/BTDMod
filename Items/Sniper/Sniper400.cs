using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BTDMod.Projectiles;

namespace BTDMod.Items.Sniper
{
    class Sniper400: ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ModContent.ItemType<Sniper000>());
            Item.damage = 70;
            Item.rare = ItemRarityID.Orange;
            Item.shoot = ModContent.ProjectileType<CrippleBullet>();
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sniper Monkey 4-0-0");
            Tooltip.SetDefault("Inflicts all sorts of debuffs on the enemy");
        }
        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(Item.type);
            recipe.AddIngredient(null, "Sniper000", 1);
            recipe.AddIngredient(ItemID.HellstoneBar, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}