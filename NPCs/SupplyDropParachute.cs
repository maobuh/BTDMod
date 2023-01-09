using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace BTDMod.NPCs {
    class SupplyDropParachute: ModNPC
    {
		Vector2? oldPos;
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
			NPC.noGravity = true;
			NPC.velocity = new(0, 2);
		}
        public override void OnKill()
        {
			NPC.NewNPC(NPC.GetSource_Death(), (int)NPC.Center.X, (int)NPC.Center.Y + (NPC.height / 2), ModContent.NPCType<SupplyDrop>());
        }
        public override void AI()
        {
            if (oldPos == NPC.position) {
				NPC.NewNPC(NPC.GetSource_Death(), (int)NPC.Center.X, (int)NPC.Center.Y + (NPC.height / 2), ModContent.NPCType<SupplyDrop>());
				NPC.active = false;
			}
			oldPos = NPC.position;
        }
    }
}