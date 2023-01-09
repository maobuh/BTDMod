using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BTDMod.NPCs {
    class SupplyDrop: ModNPC
    {
        public override void SetStaticDefaults() {
			Main.npcFrameCount[NPC.type] = 1;
		}
        public override void SetDefaults() {
			NPC.width = 66; // The width of the npc's hitbox (in pixels)
			NPC.height = 96; // The height of the npc's hitbox (in pixels).
			NPC.damage = 0; // The amount of damage that this npc deals
			NPC.defense = 0; // The amount of defense that this npc has
			NPC.lifeMax = 10; // The amount of health that this npc has
			NPC.value = 0f; // How many copper coins the NPC will drop when killed.
		}
        public override void OnKill()
        {
            for (int i = 0; i < 4; i++) {
                Item.NewItem(NPC.GetSource_Death(), NPC.getRect(), ItemID.Heart);
            }
        }
    }
}