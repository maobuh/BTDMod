using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BTDMod.Projectiles;
using BTDMod.Buffs;

namespace BTDMod.Items.Boomerang {
    class Boomerang050 : ModItem
    {
        const int ABILITY_COOLDOWN = 90 * 60;
        const int ABILITY_DURATION = 10 * 60;
        public override void SetDefaults()
        {
            Item.damage = 70;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Melee;
            Item.shoot = ModContent.ProjectileType<Projectiles.Boomerang.BionicBoomerang>();
            Item.shootSpeed = 18;
            Item.useTime = 5;
            Item.useAnimation = 5;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.rare = ItemRarityID.Cyan;
            Item.autoReuse = true;
            Item.width = 42;
            Item.height = 40;
            Item.noUseGraphic = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Boomerang Monkey 0-5-0");
            Tooltip.SetDefault("Throws boomerangs extremely fast\n" +
                               "Press ability to throw faster and do a lot more damage");
        }
        public override void HoldItem(Player player)
        {
            if (BTDMod.MonkeyAbilityHotKey.JustPressed && !player.HasBuff<TurboUncharged>()) {
                player.AddBuff(ModContent.BuffType<TurboUncharged>(), ABILITY_COOLDOWN);
                player.AddBuff(ModContent.BuffType<TurboCharged>(), ABILITY_DURATION);
                if (player.HasBuff<TurboCharged>()) {
                    Item.damage += (int) (1.2 * Item.damage);
                    Item.useTime = 2;
                }
            }
            if (!player.HasBuff<TurboCharged>()) {
                Item.damage = 80;
                Item.useTime = 5;
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.FragmentSolar, 3);
            recipe.AddIngredient(null, "Boomerang040", 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}