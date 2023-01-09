using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BTDMod.Items.Sniper
{
    class Sniper000: ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 10;
            Item.knockBack = 8;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.height = 24;
            Item.width = 52;
            Item.rare = ItemRarityID.Blue;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.UseSound = SoundID.Item11;
            Item.DamageType = DamageClass.Ranged;
            Item.shoot = ProjectileID.Bullet;
            Item.shootSpeed = 16f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sniper Monkey 0-0-0");
            Tooltip.SetDefault("They got a gun");
        }
        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(Item.type);
            recipe.AddIngredient(ItemID.IronBar, 5);
            recipe.Register();

            recipe = Recipe.Create(Item.type);
            recipe.AddIngredient(ItemID.LeadBar, 5);
            recipe.Register();
        }
    }
}