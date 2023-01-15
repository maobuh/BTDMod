using Terraria;
using Terraria.ModLoader;

namespace BTDMod.Buffs {
    class PhoenixCooldown: ModBuff
    {
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("PhoenixCooldown");
			Description.SetDefault("Phoenix is on cooldown, pew pew pew");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
		}
    }
}