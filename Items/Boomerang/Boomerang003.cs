using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BTDMod.Projectiles;

namespace BTDMod.Items.Boomerang {
    class Boomerang003 : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 30;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Melee;
            Item.shoot = ModContent.ProjectileType<Projectiles.Boomerang.KylieBoomerang>();
            Item.shootSpeed = 15;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.rare = ItemRarityID.Green;
            Item.autoReuse = true;
            Item.width = 42;
            Item.height = 40;
            Item.noUseGraphic = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Boomerang Monkey 0-0-3");
            Tooltip.SetDefault("Throws boomerangs straight");
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Bunny, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}