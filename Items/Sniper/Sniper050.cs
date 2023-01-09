using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BTDMod.Projectiles;
using BTDMod.Buffs;
using BTDMod.NPCs;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System;

namespace BTDMod.Items.Sniper
{
    class Sniper050: ModItem
    {
        const int ABILITY_COOLDOWN = 10800;
        const int SCREENWIDTH = 1920;
        const int SCREENHEIGHT = 1080;
        public override void SetDefaults()
        {
            Item.CloneDefaults(ModContent.ItemType<Sniper000>());
            Item.damage = 100;
            Item.rare = ItemRarityID.LightRed;
            Item.shoot = ModContent.ProjectileType<BouncingBullet>();
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sniper Monkey 0-5-0");
            Tooltip.SetDefault("Bullets bounce up to 3 times on nearby enemies\nBullets gain 20% damage every bounce\nAbility summons a Supply Drop that drops hearts upon death\nGrants a buff if a bullet bounces the maximum number of times");
        }
        public override void HoldItem(Player player)
        {
            if (BTDMod.MonkeyAbilityHotKey.JustPressed && !player.HasBuff<SupplyDropCooldown>()) {
                NPC.NewNPC(player.GetSource_FromThis(), (int)player.Center.X + Main.rand.Next(-SCREENWIDTH / 3, SCREENWIDTH / 3),
                                    (int)player.Center.Y - Main.rand.Next(SCREENHEIGHT / 3, SCREENHEIGHT / 2), ModContent.NPCType<SupplyDropParachute>());
                player.AddBuff(ModContent.BuffType<SupplyDropCooldown>(), ABILITY_COOLDOWN);
            }
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 2);
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(Item.type);
            recipe.AddIngredient(null, "Sniper040", 1);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 5);
            recipe.Register();
        }
    }
}