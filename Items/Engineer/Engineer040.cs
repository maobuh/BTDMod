using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BTDMod.Projectiles;
using BTDMod.Buffs;
using BTDMod.NPCs;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System;

namespace BTDMod.Items.Engineer
{
    class Engineer040: ModItem
    {
        const int ABILITY_COOLDOWN = 7200;
        const int ABILITY_DURATION = 900;
        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.LightRed;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Engineer Monkey 0-4-0");
            Tooltip.SetDefault("Doesn't attack, but grants a buff that doubles summon\ndamage and crit chance upon pressing the Monkey Button for 15 seconds.");
        }
        public override void HoldItem(Player player)
        {
            if (BTDMod.MonkeyAbilityHotKey.JustPressed && !player.HasBuff<Tired>()) {
                player.AddBuff(ModContent.BuffType<Overclock>(), ABILITY_DURATION);
                player.AddBuff(ModContent.BuffType<Tired>(), ABILITY_COOLDOWN);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(Item.type);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.AddIngredient(ItemID.RainCloud, 5);
            recipe.Register();
        }
    }
}