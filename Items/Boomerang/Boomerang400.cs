using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BTDMod.Projectiles;

namespace BTDMod.Items.Boomerang {
    class Boomerang400 : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 30;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Melee;
            Item.shoot = ModContent.ProjectileType<Projectiles.Boomerang.MOARGlaive>();
            Item.shootSpeed = 10;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Orange;
            Item.autoReuse = true;
            Item.width = 42;
            Item.height = 40;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Boomerang Monkey 4-0-0");
            Tooltip.SetDefault("Throws a boomerang that bounces between more enemies at a greater range");
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.HellstoneBar, 5);
            recipe.AddIngredient(null, "Boomerang300", 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}