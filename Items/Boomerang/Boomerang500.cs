using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.Audio;
using BTDMod.Projectiles.Boomerang;
using BTDMod.Buffs;

namespace BTDMod.Items.Boomerang {
    class Boomerang500 : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 64;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Melee;
            Item.shoot = ModContent.ProjectileType<Projectiles.Boomerang.LordGlaive>();
            Item.shootSpeed = 10;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.LightRed;
            Item.autoReuse = true;
            Item.width = 42;
            Item.height = 40;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Boomerang Monkey 5-0-0");
            Tooltip.SetDefault("Gains a new boomerang ring");
        }
        public override void HoldItem(Player player)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<GlaiveLordRing>()] > 0) return;
            Projectile.NewProjectile(new EntitySource_ItemUse(player, Item), player.Center, new Vector2(0,-1), ModContent.ProjectileType<GlaiveLordRing>(), Item.damage * 2, 0, player.whoAmI);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.LihzahrdBrick, 3);
            recipe.AddIngredient(null, "Boomerang400", 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}