using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BTDMod.Items;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Shaders;

namespace BTDMod.NPCs
{
    class BTDGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public bool Dissolving;
        public bool DissolvingPlus;
        public bool Slowed;
        public override void SetupShop(int type, Chest shop, ref int nextSlot)
        {
            if (type == NPCID.Merchant) {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<Pineapple>());
                nextSlot++;
            }
            base.SetupShop(type, shop, ref nextSlot);
        }
        public override void ResetEffects(NPC npc)
        {
            Dissolving = false;
            DissolvingPlus = false;
            Slowed = false;
        }
        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            // don't put this debuff on bosses
            // queen bee will be permanently frozen if she is slowed during her charge
            if (Slowed) {
                npc.velocity = npc.oldVelocity / 2;
            }
            // 50 damage / sec
            if (Dissolving)
            {
                if (npc.lifeRegen > 0) {
                    npc.lifeRegen = 0;
                }

                npc.lifeRegen -= 100;

                if (damage < 10) {
                    damage = 10;
                }
            }
            // 100 damage / sec
            if (DissolvingPlus)
            {
                if (npc.lifeRegen > 0) {
                    npc.lifeRegen = 0;
                }

                npc.lifeRegen -= 200;

                if (damage < 20) {
                    damage = 20;
                }
            }
        }
        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (Slowed) {
                drawColor = Colors.RarityYellow;
            }
            if (Dissolving || DissolvingPlus)
            {
                Vector2 dustPos = npc.Center;
                dustPos.X += (Main.rand.NextBool(2)? -1.0f : 1.0f) * npc.width * Main.rand.NextFloat(0.5f);
                dustPos.Y += (Main.rand.NextBool(2)? -1.0f : 1.0f) * npc.height * Main.rand.NextFloat(0.5f);
                int d = Dust.NewDust(dustPos, 0, 0, DustID.CursedTorch, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f);
                Main.dust[d].noGravity = true;
                Main.dust[d].velocity *= 3f;
                Main.dust[d].scale += 0.5f;

            }
        }
    }
}