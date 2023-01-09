using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BTDMod.Items;

namespace BTDMod.NPCs
{
    class BTDGlobalNPC : GlobalNPC
    {
        public override void SetupShop(int type, Chest shop, ref int nextSlot)
        {
            if (type == NPCID.Merchant) {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<Pineapple>());
                nextSlot++;
            }
            base.SetupShop(type, shop, ref nextSlot);
        }
    }
}