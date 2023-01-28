using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BTDMod.Projectiles;
using BTDMod.Buffs;

namespace BTDMod.Items.Boomerang {
    class Boomerang040 : ModItem
    {
        const int ABILITY_COOLDOWN = 45 * 60;
        const int ABILITY_DURATION = 10 * 60;
        public override void SetDefaults()
        {
            Item.damage = 60;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Melee;
            Item.shoot = ModContent.ProjectileType<Projectiles.Boomerang.BionicBoomerang>();
            Item.shootSpeed = 15;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.rare = ItemRarityID.Green;
            Item.autoReuse = true;
            Item.width = 42;
            Item.height = 40;
            Item.noUseGraphic = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Boomerang Monkey 0-4-0");
            Tooltip.SetDefault("Throws boomerangs fast");
        }
        public override void HoldItem(Player player)
        {
            if (BTDMod.MonkeyAbilityHotKey.JustPressed && !player.HasBuff<TurboUncharged>()) {
                player.AddBuff(ModContent.BuffType<TurboUncharged>(), ABILITY_COOLDOWN);
                player.AddBuff(ModContent.BuffType<TurboCharged>(), ABILITY_DURATION);
                if (player.HasBuff<TurboCharged>()) {
                    Item.damage += (int) (1.1 * Item.damage);
                    Item.useTime = 5;
                }
            }
            if (!player.HasBuff<TurboCharged>()) {
                Item.damage = 60;
                Item.useTime = 10;
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.PixieDust, 5);
            recipe.AddIngredient(null, "Boomerang030", 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}