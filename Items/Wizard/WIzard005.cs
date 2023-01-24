using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BTDMod.Projectiles;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace BTDMod.Items.Wizard
{
    class Wizard005: ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ModContent.ItemType<Wizard000>());
            Item.damage = 70;
            Item.rare = ItemRarityID.Pink;
            Item.shoot = ModContent.ProjectileType<NecroSkull>();
            Item.shootSpeed = 10f;
            Item.useTime = 20;
            Item.useAnimation = 20;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wizard Monkey 0-0-5");
            Tooltip.SetDefault("The Archmage and him were willing to put aside their differences\nto share the arts of Shimmer and Arcane Mastery");
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            // spawns the projectile a bit ahead of the player to make it look nice (2 blocks ahead)
            Projectile.NewProjectile(source, position + (velocity / Item.shootSpeed * 32), velocity, type, damage, knockback, player.whoAmI);
            // also shoots out the purple homing projectile but at reduced damage and faster speed
            Projectile.NewProjectile(source, position + (velocity / Item.shootSpeed * 32), velocity * 2.5f, ModContent.ProjectileType<ArchmageBullet>(), damage / 2, knockback, player.whoAmI);
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(Item.type);
            recipe.AddIngredient(null, "Wizard004", 1);
            recipe.AddIngredient(ItemID.SoulofFright, 3);
            recipe.AddTile(TileID.Bookcases);
            recipe.Register();
        }
    }
}