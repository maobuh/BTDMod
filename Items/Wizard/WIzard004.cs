using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BTDMod.Projectiles;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace BTDMod.Items.Wizard
{
    class Wizard004: ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ModContent.ItemType<Wizard000>());
            Item.damage = 24;
            Item.rare = ItemRarityID.Green;
            Item.shoot = ModContent.ProjectileType<NecroSkull>();
            Item.shootSpeed = 7f;
            Item.useTime = 20;
            Item.useAnimation = 20;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wizard Monkey 0-0-4");
            Tooltip.SetDefault("Desecrates the dead to make more homies");
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            // spawns the projectile a bit ahead of the player to make it look nice (2 blocks ahead)
            Projectile.NewProjectile(source, position + (velocity / Item.shootSpeed * 32), velocity, type, damage, knockback, player.whoAmI);
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(Item.type);
            recipe.AddIngredient(null, "Wizard000", 1);
            recipe.AddIngredient(ItemID.Bone, 3);
            recipe.AddTile(TileID.Bookcases);
            recipe.Register();
        }
    }
}