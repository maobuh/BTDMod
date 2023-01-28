using Terraria;
using Terraria.ModLoader;

namespace BTDMod.Buffs {
    class TurboUncharged: ModBuff
    {
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Turbo Charge Cooldown");
			Description.SetDefault("You're going too fast, slow down");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
		}
    }
}