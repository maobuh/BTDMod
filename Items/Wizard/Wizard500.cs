using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BTDMod.Projectiles;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System;

namespace BTDMod.Items.Wizard
{
    class Wizard500: ModItem
    {
        const int PURPLE_SHOOT_SPEED = 10;
        int timer;
        public override void SetDefaults()
        {
            Item.CloneDefaults(ModContent.ItemType<Wizard000>());
            Item.damage = 70;
            Item.rare = ItemRarityID.Cyan;
            Item.shoot = ModContent.ProjectileType<ArchmageBullet>();
            Item.shootSpeed = 20f;
            Item.useTime = 4;
            Item.useAnimation = 4;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wizard Monkey 5-0-0");
            Tooltip.SetDefault("I put my name in the goblet of fire, the Archmage replied calmly.");
        }
        public override void HoldItem(Player player)
        {
            timer++;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (timer > PURPLE_SHOOT_SPEED) {
                Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
                timer = 0;
            }
            Projectile.NewProjectile(source, position, velocity * 0.5f, ModContent.ProjectileType<ArchmageFireball>(), damage, knockback, player.whoAmI);
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(Item.type);
            recipe.AddIngredient(null, "Wizard400", 1);
            recipe.AddIngredient(ItemID.Ectoplasm, 5);
            recipe.AddTile(TileID.Bookcases);
            recipe.Register();
        }
    }
}