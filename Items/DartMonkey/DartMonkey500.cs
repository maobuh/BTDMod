using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BTDMod.Projectiles;

namespace BTDMod.Items.DartMonkey
{
    class DartMonkey500 : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 30;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Melee;
            Item.shoot = ModContent.ProjectileType<UltraJuggernaut>();
            Item.shootSpeed = 10;
            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.useStyle = ItemUseStyleID.Thrust;
            Item.rare = ItemRarityID.LightRed;
            Item.autoReuse = true;
            Item.width = 32;
            Item.height = 32;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ultra Juggernaut");
            Tooltip.SetDefault("Shoots giant spiked balls that bounce of surfaces" + "\n"
            + "Spiked balls will explode after piercing 100 enemies and after reaching max bounces");
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddRecipeGroup("IronBar", 5);
            recipe.AddTile(TileID.Anvils);
            recipe.AddIngredient(ItemID.HellstoneBar, 5);
            recipe.AddIngredient(ModContent.ItemType<DartMonkey400>());

            Recipe recipe2 = recipe.Clone();
            recipe.AddIngredient(ItemID.CobaltBar, 1);
            recipe2.AddIngredient(ItemID.PalladiumBar, 1);

            recipe.Register();
            recipe2.Register();
        }
    }
}