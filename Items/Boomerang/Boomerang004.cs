using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BTDMod.Projectiles;

namespace BTDMod.Items.Boomerang {
    class Boomerang004 : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 50;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Melee;
            Item.shoot = ModContent.ProjectileType<Projectiles.Boomerang.MOABPress>();
            Item.shootSpeed = 16;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.rare = ItemRarityID.LightRed;
            Item.autoReuse = true;
            Item.width = 42;
            Item.height = 40;
            Item.noUseGraphic = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Boomerang Monkey 0-0-4");
            Tooltip.SetDefault("Throws boomerangs very straight");
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.CrystalShard, 1);
            recipe.AddIngredient(null, "Boomerang003", 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}