using Terraria;
using Terraria.ModLoader;

namespace BTDMod.Buffs {
    class TurboCharged: ModBuff
    {
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Turbo Charged");
			Description.SetDefault("Double attack speed && damage increased by 10%");
			Main.debuff[Type] = false;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
		}
    }
}