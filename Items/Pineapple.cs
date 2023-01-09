using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace BTDMod.Items
{
    class Pineapple : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 300;
            Item.consumable = true;
            Item.maxStack = 999;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Generic;
            Item.shoot = ModContent.ProjectileType<Projectiles.Pineapple>();
            Item.shootSpeed = 0;
            Item.useTime = 60;
            Item.useAnimation = 1;
            Item.useStyle = ItemUseStyleID.MowTheLawn;
            Item.value = Item.sellPrice(0, 0, 0, 20);
            Item.rare = 2;
            Item.autoReuse = true;
            Item.width = 32;
            Item.height = 32;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pineapple");
            Tooltip.SetDefault("Left Click to place a pineapple that will" + "\n"
             + "stay in place for 3 seconds then explode");
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, Main.MouseWorld, Vector2.Zero, type, Item.damage, 0, player.whoAmI);
            return false;
        }
    }
}