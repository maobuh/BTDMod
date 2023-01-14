using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BTDMod.Projectiles;

namespace BTDMod.Items.Wizard
{
    class Wizard000: ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 10;
            Item.knockBack = 2;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.height = 24;
            Item.width = 52;
            Item.rare = ItemRarityID.Blue;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.UseSound = SoundID.Item39;
            Item.DamageType = DamageClass.Magic;
            Item.shoot = ModContent.ProjectileType<WizardBullet>();
            Item.shootSpeed = 10f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wizard Monkey 0-0-0");
            Tooltip.SetDefault("They use the lean spell");
        }
        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(Item.type);
            recipe.AddIngredient(ItemID.FallenStar, 3);
            recipe.Register();
        }
    }
}