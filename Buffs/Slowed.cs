using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using BTDMod.NPCs;

namespace BTDMod.Buffs {
    class Slowed: ModBuff
    {
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Slowed");
			Description.SetDefault("slowed");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
		}
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<BTDGlobalNPC>().Slowed = true;
        }
    }
}