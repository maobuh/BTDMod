using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BTDMod.Items.DartMonkey
{
    class DartMonkey004 : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 40;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Generic;
            Item.shoot = ModContent.ProjectileType<Projectiles.DartMonkey.CrossbowBolt004>();
            Item.shootSpeed = 50;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.MowTheLawn;
            Item.rare = ItemRarityID.Lime;
            Item.autoReuse = true;
            Item.width = 32;
            Item.height = 32;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sharp Shooter");
            Tooltip.SetDefault("Shoots crossbow bolts that go faster and do more damage");
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.HallowedBar, 1);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 1);
            recipe.AddIngredient(ItemID.TitaniumBar, 1);
            recipe.AddIngredient(ModContent.ItemType<DartMonkey000>(), 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}