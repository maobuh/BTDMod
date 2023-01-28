using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BTDMod.Projectiles;

namespace BTDMod.Items.DartMonkey
{
    class DartMonkey400 : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 40;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Melee;
            Item.shoot = ModContent.ProjectileType<Juggernaut>();
            Item.shootSpeed = 10;
            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.useStyle = ItemUseStyleID.Thrust;
            Item.rare = ItemRarityID.Orange;
            Item.autoReuse = true;
            Item.width = 32;
            Item.height = 32;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Juggernaut");
            Tooltip.SetDefault("Shoots giant spiked balls that bounce of surfaces");
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("IronBar", 5);
            recipe.AddIngredient(ModContent.ItemType<DartMonkey000>(), 1);
            recipe.AddIngredient(ItemID.Obsidian, 10);
            recipe.AddIngredient(ItemID.MeteoriteBar, 4);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}