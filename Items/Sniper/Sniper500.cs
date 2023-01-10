using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BTDMod.Projectiles;
using Microsoft.Xna.Framework;

namespace BTDMod.Items.Sniper
{
    class Sniper500: ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ModContent.ItemType<Sniper000>());
            Item.damage = 1000;
            Item.rare = ItemRarityID.Orange;
            Item.shoot = ModContent.ProjectileType<CrippleBullet>();
            Item.useTime = 40;
            Item.useAnimation = 40;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sniper Monkey 5-0-0");
            Tooltip.SetDefault("Inflicts all sorts of debuffs on the enemy\nAlso does hitscan with a cooler scope");
        }
        public override void HoldItem(Player player)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<Scope>()] < 1) {
                Projectile.NewProjectile(Item.GetSource_FromThis(), Main.MouseWorld, Vector2.Zero, ModContent.ProjectileType<Scope>(), 0, 0, player.whoAmI);
            }
            if (player.ownedProjectileCounts[ModContent.ProjectileType<Hitscan>()] < 1) {
                Vector2 direction = Main.MouseWorld - player.Center;
                direction = direction.SafeNormalize(Vector2.Zero);
                Projectile.NewProjectile(Item.GetSource_FromThis(), player.Center, direction, ModContent.ProjectileType<Hitscan>(), 0, 0, player.whoAmI);
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