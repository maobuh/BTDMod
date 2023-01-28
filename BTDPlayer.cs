using Terraria;
using Terraria.ModLoader;
using BTDMod.Items.Druid;

namespace BTDMod
{
    class BTDPlayer : ModPlayer
    {
        public int vineRadius;
        public double wrathAttackSpeedBonus;
        public int wrathAttackSpeedBonusResetTimer;
        public bool attackSpeedResetCheck;
        bool oldAttackSpeedResetCheck;
        public override void UpdateEquips()
        {
            if (Player.HeldItem.type != ModContent.ItemType<Druid052>() && Player.HeldItem.type != ModContent.ItemType<Druid032>() && vineRadius != 4) {
                vineRadius = 4;
            }
            base.UpdateEquips();
        }
        public override void PostUpdate()
        {
            if (wrathAttackSpeedBonusResetTimer > 120) {
                wrathAttackSpeedBonus = 0;
                wrathAttackSpeedBonusResetTimer = 0;
            }
            if (attackSpeedResetCheck == oldAttackSpeedResetCheck) {
                wrathAttackSpeedBonusResetTimer++;
            } else {
                wrathAttackSpeedBonusResetTimer = 0;
            }
            oldAttackSpeedResetCheck = attackSpeedResetCheck;
            base.PostUpdate();
        }
    }
}