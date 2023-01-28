using Terraria;
using Terraria.ModLoader;

namespace BTDMod.Buffs {
    class TurboCharged: ModBuff
    {
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Turbo Charged");
			Description.SetDefault("Go crazy");
			Main.debuff[Type] = false;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
		}
    }
}