using Terraria;
using Terraria.ModLoader;

namespace BTDMod.Buffs {
    class MaelstromCooldown: ModBuff
    {
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Maelstrom Cooldown");
			Description.SetDefault("Maelstrom is cooling");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
		}
    }
}