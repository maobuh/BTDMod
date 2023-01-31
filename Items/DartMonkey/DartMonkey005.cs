using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BTDMod.Items.DartMonkey
{
    class DartMonkey005 : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 40;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Generic;
            Item.shoot = ModContent.ProjectileType<Projectiles.DartMonkey.CrossbowBolt>();
            Item.shootSpeed = 50;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Red;
            Item.autoReuse = true;
            Item.width = 32;
            Item.height = 32;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crossbow Master");
            Tooltip.SetDefault("Crossbow bolts will stick in an enemy when they hit it\n" +
                               "Press the Monkey Ability Hotkey to detonate any crossbow bolts for large damage");
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.FragmentSolar, 1);
            recipe.AddIngredient(ModContent.ItemType<DartMonkey004>(), 1);
            recipe.AddTile(TileID.LunarCraftingStation);
            base.AddRecipes();
        }
    }
}