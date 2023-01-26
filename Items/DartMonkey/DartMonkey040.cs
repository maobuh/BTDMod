using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BTDMod.Projectiles;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System;
using BTDMod.Buffs;

namespace BTDMod.Items.DartMonkey
{
    class DartMonkey040 : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 20;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Melee;
            Item.shoot = ModContent.ProjectileType<Dart>();
            Item.shootSpeed = 10;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.rare = ItemRarityID.Blue;
            Item.autoReuse = true;
            Item.width = 32;
            Item.height = 32;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Super Monkey Fan Club");
            Tooltip.SetDefault("Press the Monkey Ability Hotkey to spawn a swarm of Super Monkeys");
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DartMonkey000>(), 1);
            recipe.AddIngredient(ItemID.ManaCrystal, 1);
            recipe.AddIngredient(ItemID.HallowedBar, 1);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float shootSpeed = velocity.Length();
            Vector2 direction = velocity;
            direction.Normalize();
            double angle = Math.Atan2(direction.Y, direction.X);
            for (int i = -1; i < 2; i++) {
                double newAngle = angle + (Math.PI / 12 * i);
                Vector2 newDirection = new((float)Math.Cos(newAngle), (float)Math.Sin(newAngle));
                Projectile.NewProjectile(source, position, newDirection * shootSpeed, type, damage, knockback, player.whoAmI);
            }
            return false;
        }
        public override void HoldItem(Player player)
        {
            if (!player.HasBuff(ModContent.BuffType<SMFCCooldown>()) && BTDMod.MonkeyAbilityHotKey.JustPressed) {
                for (int i = 0; i < 10; i++) {
                    Vector2 randPos = new(player.Center.X + ((Main.rand.NextFloat() - 0.5f) * 16 * 10), player.Center.Y + ((Main.rand.NextFloat() - 0.5f) * 16 * 10));
                    Projectile.NewProjectile(Item.GetSource_ItemUse(Item), randPos, Vector2.Zero, ModContent.ProjectileType<SuperMonkeyMinion>(), Item.damage, 0, player.whoAmI);
                }
                player.AddBuff(ModContent.BuffType<SMFCCooldown>(), 1); // cooldown is 3000 normally, set to 1 for testing
            }
        }
    }
}