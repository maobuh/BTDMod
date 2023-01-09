using Terraria;
using Terraria.ModLoader;

namespace BTDMod.Buffs {
    class EliteBuff: ModBuff
    {
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("EliteBuff");
			Description.SetDefault("Increase Ranged Damage by 10%, Ranged Crit CHance by 10% and Bouncing Bullet bounce range by 50%");
			Main.debuff[Type] = false;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
		}
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage<RangedDamageClass>() += 0.1f;
            player.GetCritChance<RangedDamageClass>() += 10;
        }
    }
}