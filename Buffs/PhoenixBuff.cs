using Terraria;
using Terraria.ModLoader;

namespace BTDMod.Buffs {
    class PhoenixBuff: ModBuff
    {
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Phoenix");
			Description.SetDefault("Phoenix spittin' fire");
			Main.debuff[Type] = false;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
		}
    }
}