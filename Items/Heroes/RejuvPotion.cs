using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BTDMod.Items.Heroes
{
    class RejuvPotion : ModItem
    {
        public override void SetDefaults()
        {
            Item.consumable = true;
            Item.maxStack = 999;
            Item.noMelee = true;
            Item.useTime = 17;
            Item.useAnimation = 17;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.value = Item.sellPrice(0, 0, 20, 0);
            Item.rare = ItemRarityID.LightRed;
            Item.autoReuse = true;
            Item.width = 32;
            Item.height = 32;
            Item.potion = true;
            Item.healLife = 100;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Regen Potion");
            Tooltip.SetDefault("Removes all debuffs and heals the player for 100 health" + "\n" +
            "Gives you potion sickness for 3 minutes after consumption" + "\n" +
            "Cannot be used while you have potion sickness");
        }
        public override bool? UseItem(Player player)
        {
            for (int i = 0; i < player.buffType.Length; i++) {
                if (Main.debuff[player.buffType[i]]) {
                    player.DelBuff(player.buffType[i]);
                }
            }
            return base.UseItem(player);
        }
    }
}