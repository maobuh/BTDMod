using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BTDMod.Items
{
    class NdujaFrittaTanto : ModItem
    {
        public override void SetDefaults()
        {
            Item.consumable = true;
            Item.maxStack = 999;
            Item.noMelee = true;
            Item.useTime = 17;
            Item.useAnimation = 17;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = ItemRarityID.LightRed;
            Item.autoReuse = true;
            Item.width = 32;
            Item.height = 32;
            Item.buffType = ModContent.BuffType<Buffs.NdujaFrittaTanto>();
            Item.buffTime = 6000;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nduja Fritta Tanto");
            Tooltip.SetDefault("Spew fire in the direction of your mouse for the next 10 seconds\n" +
            "this item is from vampire survivors lol");
        }
    }
}