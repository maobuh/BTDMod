using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.Audio;
using BTDMod.Projectiles;
using BTDMod.Buffs;

namespace BTDMod.Items.Wizard
{
    class Wizard040: ModItem
    {
        const int ABILITY_COOLDOWN = 5400;
        const int ABILITY_DURATION = 900;
        const int TOTAL_PROJ = 5;
        int spreadPos = TOTAL_PROJ - 1;
        int spreadPos2 = TOTAL_PROJ - 1;
        int direction = -1;
        public override void SetDefaults()
        {
            Item.damage = 50;
            Item.knockBack = 2;
            Item.useTime = 2;
            Item.useAnimation = 2;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.height = 24;
            Item.width = 52;
            Item.rare = ItemRarityID.Orange;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Magic;
            Item.shoot = ModContent.ProjectileType<FlameBreath>();
            Item.shootSpeed = 16f;
            Item.mana = 3;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wizard Monkey 0-4-0");
            Tooltip.SetDefault("Ate a jalapeno one day.\nPress the monkey button to spawn a phoenix for 15s");
        }
        public override void UseAnimation(Player player)
        {
            spreadPos2 = spreadPos;
            if (spreadPos == TOTAL_PROJ - 1) {
                direction = -1;
            } else if (spreadPos == 0) {
                direction = 1;
            }
            spreadPos += direction;
        }
        public override void HoldItem(Player player)
        {
            if (BTDMod.MonkeyAbilityHotKey.JustPressed && !player.HasBuff<PhoenixCooldown>()) {
                Projectile.NewProjectile(new EntitySource_ItemUse(player, Item), player.Center, new Vector2(0, -1), ModContent.ProjectileType<PhoenixMinion>(), 0, 0, player.whoAmI);
                player.AddBuff(ModContent.BuffType<PhoenixCooldown>(), ABILITY_COOLDOWN);
                player.AddBuff(ModContent.BuffType<PhoenixBuff>(), ABILITY_DURATION);
            }
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            // gives spread to the projectile by rotating the projectile direction
            double baseAngle = Math.Atan2(velocity.X, velocity.Y);
            const double spread = 8 / (180 / Math.PI);
            float baseSpeed = velocity.Length();
            double currAngle1 = baseAngle + ((spreadPos - 2) * spread);
            double currAngle2 = baseAngle + ((spreadPos2 - 2) * spread);
            velocity = new(baseSpeed * (float)Math.Sin(currAngle1), baseSpeed * (float)Math.Cos(currAngle1));
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
            velocity = new(baseSpeed * (float)Math.Sin(currAngle2), baseSpeed * (float)Math.Cos(currAngle2));
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(Item.type);
            recipe.AddIngredient(null, "Wizard030", 1);
            recipe.AddIngredient(ItemID.HellstoneBrick, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}