using Terraria;
using Terraria.ModLoader;

namespace BTDMod.Buffs {
    class Overclock: ModBuff
    {
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Overclocked");
			Description.SetDefault("Doubles Summon Damage and Crit Chance");
			Main.debuff[Type] = false;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
		}
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage<SummonDamageClass>() *= 2f;
            player.GetCritChance<SummonDamageClass>() *= 2;
        }
    }
}