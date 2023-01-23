using Terraria;
using Terraria.ModLoader;
using BTDMod.Items.Druid;

namespace BTDMod
{
    class BTDPlayer : ModPlayer
    {
        public int vineRadius;
        public override void UpdateEquips()
        {
            if (Player.HeldItem.type != ModContent.ItemType<Druid052>() && Player.HeldItem.type != ModContent.ItemType<Druid032>() && vineRadius != 4) {
                vineRadius = 4;
            }
            base.UpdateEquips();
        }
    }
}