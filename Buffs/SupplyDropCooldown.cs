using Terraria;
using Terraria.ModLoader;

namespace BTDMod.Buffs {
    class SupplyDropCooldown: ModBuff
    {
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("SupplyDropCooldown");
			Description.SetDefault("SupplyDrop is on cooldown, deal damage with this weapon to decrease the timer");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
		}
    }
}