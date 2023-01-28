using Terraria;
using Terraria.ModLoader;

namespace BTDMod.Buffs {
    // SMFC stands for super monkey fan club
    class SMFCCooldown: ModBuff
    {
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Super Monkey Fan Club Ability Cooldown");
			Description.SetDefault("The Super Monkey Fan Club ability is on cooldown");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
		}
    }
}