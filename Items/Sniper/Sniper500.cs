using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BTDMod.Projectiles;
using Microsoft.Xna.Framework;
using Terraria.Graphics;
using System;

namespace BTDMod.Items.Sniper
{
    class Sniper500: ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ModContent.ItemType<Sniper000>());
            Item.damage = 700;
            Item.rare = ItemRarityID.Cyan;
            Item.shoot = ModContent.ProjectileType<HitscanBullet>();
            Item.useTime = 60;
            Item.useAnimation = 60;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sniper Monkey 5-0-0");
            Tooltip.SetDefault("Inflicts all sorts of debuffs on the enemy\nAlso does hitscan with a cooler scope\nIncreases damage when shot at the smallest crosshair\nYou can autofire the weapon to get the buff after timing the first shot");
        }
        public override void HoldItem(Player player)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<LaserSight>()] < 1) {
                Projectile.NewProjectile(Item.GetSource_FromThis(), player.Center, Vector2.Zero, ModContent.ProjectileType<LaserSight>(), 0, 0, player.whoAmI);
            }
            if (player.ownedProjectileCounts[ModContent.ProjectileType<Scope>()] < 1) {
                Projectile.NewProjectile(Item.GetSource_FromThis(), Main.MouseWorld, Vector2.Zero, ModContent.ProjectileType<Scope>(), 0, 0, player.whoAmI);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(Item.type);
            recipe.AddIngredient(null, "Sniper400", 1);
            recipe.AddIngredient(ItemID.FragmentVortex, 3);
            recipe.Register();
        }
    }
}