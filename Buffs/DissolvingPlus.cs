using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using BTDMod.NPCs;

namespace BTDMod.Buffs {
    class DissolvingPlus: ModBuff
    {
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Melting");
			Description.SetDefault("somewhat quickly dying");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
		}
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<BTDGlobalNPC>().DissolvingPlus = true;
        }
    }
}