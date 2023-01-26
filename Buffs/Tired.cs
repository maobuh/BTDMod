using Terraria;
using Terraria.ModLoader;

namespace BTDMod.Buffs {
    class Tired: ModBuff
    {
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tired");
			Description.SetDefault("Your summons are tired from that electrifying buff");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
		}
    }
}