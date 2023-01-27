using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BTDMod.Items.Heroes
{
    // TODO how many seconds should the buff last
    class HotSauce : ModItem
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
            Item.buff = ModContent.BuffType<BTDMod.Buffs.HotSauce>();
            Item.buffTime = 6000; // or whatever this is supposed to be
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bottle of 'Gerry's Fire' Hot Sauce");
            Tooltip.SetDefault("Spew fire in the direction of your mouse for the next [seconds] seconds");
        }
    }
}