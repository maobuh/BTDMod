using Terraria;
using Terraria.ModLoader;

namespace BTDMod.Buffs {
    class Marked: ModBuff
    {
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Marked");
			Description.SetDefault("Increase Damage by 100%, Ranged Crit Chance by 25%");
			Main.debuff[Type] = false;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
		}
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage<RangedDamageClass>() += 1.5f;
            player.GetCritChance<RangedDamageClass>() += 25;
        }
    }
}