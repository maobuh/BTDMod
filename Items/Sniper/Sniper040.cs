using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BTDMod.Projectiles;
using BTDMod.Buffs;
using BTDMod.NPCs;

namespace BTDMod.Items.Sniper
{
    class Sniper040: ModItem
    {
        const int ABILITY_COOLDOWN = 10800;
        const int SCREENWIDTH = 1920;
        const int SCREENHEIGHT = 1080;
        public override void SetDefaults()
        {
            Item.CloneDefaults(ModContent.ItemType<Sniper000>());
            Item.damage = 80;
            Item.rare = ItemRarityID.LightRed;
            Item.shoot = ModContent.ProjectileType<BouncingBullet>();
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sniper Monkey 0-4-0");
            Tooltip.SetDefault("Bullets bounce up to 3 times on nearby enemies\nAbility summons a Supply Drop that drops hearts upon death");
        }
        public override void HoldItem(Player player)
        {
            if (BTDMod.MonkeyAbilityHotKey.JustPressed && !player.HasBuff<SupplyDropCooldown>()) {
                NPC.NewNPC(player.GetSource_FromThis(), (int)player.Center.X + Main.rand.Next(-SCREENWIDTH / 3, SCREENWIDTH / 3),
                                    (int)player.Center.Y - Main.rand.Next(SCREENHEIGHT / 3, SCREENHEIGHT / 2), ModContent.NPCType<SupplyDropParachute>());
                player.AddBuff(ModContent.BuffType<SupplyDropCooldown>(), ABILITY_COOLDOWN);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(Item.type);
            recipe.AddIngredient(null, "Sniper030", 1);

            Recipe recipe2 = recipe.Clone();

            recipe.AddIngredient(ItemID.MythrilBar, 5);
            recipe.Register();

            recipe2.AddIngredient(ItemID.OrichalcumBar, 5);
            recipe2.Register();
        }
    }
}