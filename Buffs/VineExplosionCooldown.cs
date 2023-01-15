using Terraria;
using Terraria.ModLoader;

namespace BTDMod.Buffs {
    class VineExplosionCooldown: ModBuff
    {
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Vine Explosion Cooldown");
			Description.SetDefault("The Vine Explosion ability is on cooldown");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
		}
    }
}