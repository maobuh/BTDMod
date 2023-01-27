using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using BTDMod.Items;

namespace BTDMod.NPCs
{
	// [AutoloadHead] and npc.townNPC are extremely important and absolutely both necessary for any Town NPC to work at all.
	[AutoloadHead]
	public class Geraldo : ModNPC
	{
		public override string Texture => "ExampleMod/NPCs/ExamplePerson";

		public override string[] AltTextures => new[] { "ExampleMod/NPCs/ExamplePerson_Alt_1" };

		public override bool Autoload(ref string name) {
			name = "Example Person";
			return mod.Properties.Autoload;
		}

		public override void SetStaticDefaults() {
			// DisplayName automatically assigned from .lang files, but the commented line below is the normal approach.
			DisplayName.SetDefault("Geraldo");
			Main.npcFrameCount[npc.type] = 25;
			NPCID.Sets.ExtraFramesCount[npc.type] = 9;
			NPCID.Sets.AttackFrameCount[npc.type] = 4;
			NPCID.Sets.DangerDetectRange[npc.type] = 700;
			NPCID.Sets.AttackType[npc.type] = 0;
			NPCID.Sets.AttackTime[npc.type] = 90;
			NPCID.Sets.AttackAverageChance[npc.type] = 30;
			NPCID.Sets.HatOffsetY[npc.type] = 4;
		}

		public override void SetDefaults() {
			npc.townNPC = true;
			npc.friendly = true;
			npc.width = 18; // sprite
			npc.height = 40; // sprite
			npc.aiStyle = 7;
			npc.damage = 10;
			npc.defense = 15;
			npc.lifeMax = 250;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
			npc.knockBackResist = 0.5f;
			animationType = NPCID.Guide;
		}

		public override void HitEffect(int hitDirection, double damage) {
			int num = npc.life > 0 ? 1 : 5;
			for (int k = 0; k < num; k++) {
				Dust.NewDust(npc.position, npc.width, npc.height, ModContent.DustType<Sparkle>());
			}
		}

		public override bool CanTownNPCSpawn(int numTownNPCs, int money) {
			for (int k = 0; k < 255; k++) {
				Player player = Main.player[k];
				if (!player.active) {
					continue;
				}

				foreach (Item item in player.inventory) {
					if (item.type == ModContent.ItemType<ExampleItem>() || item.type == ModContent.ItemType<Items.Placeable.ExampleBlock>()) {
						return true;
					}
				}
			}
			return false;
		}

		public override string TownNPCName() {
			return "Geraldo";
		}

		public override void FindFrame(int frameHeight) {
			/*npc.frame.Width = 40;
			if (((int)Main.time / 10) % 2 == 0)
			{
				npc.frame.X = 40;
			}
			else
			{
				npc.frame.X = 0;
			}*/
		}

		public override string GetChat() {
			// int partyGirl = NPC.FindFirstNPC(NPCID.PartyGirl);
			// if (partyGirl >= 0 && Main.rand.NextBool(4)) {
			// 	return "Can you please tell " + Main.npc[partyGirl].GivenName + " to stop decorating my house with colors?";
			// }
			switch (Main.rand.Next(4)) {
				case 0:
					return "Sometimes I feel like I'm different from everyone else here.";
				case 1:
					return "What's your favorite color? My favorite colors are white and black.";
				case 2:
					return "ima gonna eat you";
				default:
					return "What? I don't have any arms or legs? Oh, don't be ridiculous!";
			}
		}

		/* 
		// Consider using this alternate approach to choosing a random thing. Very useful for a variety of use cases.
		// The WeightedRandom class needs "using Terraria.Utilities;" to use
		public override string GetChat()
		{
			WeightedRandom<string> chat = new WeightedRandom<string>();
			int partyGirl = NPC.FindFirstNPC(NPCID.PartyGirl);
			if (partyGirl >= 0 && Main.rand.NextBool(4))
			{
				chat.Add("Can you please tell " + Main.npc[partyGirl].GivenName + " to stop decorating my house with colors?");
			}
			chat.Add("Sometimes I feel like I'm different from everyone else here.");
			chat.Add("What's your favorite color? My favorite colors are white and black.");
			chat.Add("What? I don't have any arms or legs? Oh, don't be ridiculous!");
			chat.Add("This message has a weight of 5, meaning it appears 5 times more often.", 5.0);
			chat.Add("This message has a weight of 0.1, meaning it appears 10 times as rare.", 0.1);
			return chat; // chat is implicitly cast to a string. You can also do "return chat.Get();" if that makes you feel better
		}
		*/

		public override void SetChatButtons(ref string button, ref string button2) {
			// Language.GetTextValue("LegacyInterface.28"); might be shop but real? idk
			button = "Shop";
			button2 = "Chat";
		}

		public override void OnChatButtonClicked(bool firstButton, ref bool shop) {
			if (firstButton) {
				shop = true;
			}
			else {
				switch (Main.rand.Next(4)){
					case 0:
						Main.npcChatText = "suck dick";
					default:
						Main.npcChatText = "eat dick";
				}
			}
		}

		public override void SetupShop(Chest shop, ref int nextSlot) {
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<Heroes.RejuvPotion>());
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<Pineapple>());
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<Heroes.HotSauce>());
			nextSlot++;
			// shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Placeable.ExampleWorkbench>());
			// nextSlot++;
			// shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Placeable.ExampleChair>());
			// nextSlot++;
			// shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Placeable.ExampleDoor>());
			// nextSlot++;
			// shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Placeable.ExampleBed>());
			// nextSlot++;
			// shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Placeable.ExampleChest>());
			// nextSlot++;
			// shop.item[nextSlot].SetDefaults(ModContent.ItemType<ExamplePickaxe>());
			// nextSlot++;
			// shop.item[nextSlot].SetDefaults(ModContent.ItemType<ExampleHamaxe>());
			// nextSlot++;
			// if (Main.LocalPlayer.HasBuff(BuffID.Lifeforce)) {
			// 	shop.item[nextSlot].SetDefaults(ModContent.ItemType<ExampleHealingPotion>());
			// 	nextSlot++;
			// }
			// if (Main.LocalPlayer.GetModPlayer<ExamplePlayer>().ZoneExample && !ModContent.GetInstance<ExampleConfigServer>().DisableExampleWings) {
			// 	shop.item[nextSlot].SetDefaults(ModContent.ItemType<ExampleWings>());
			// 	nextSlot++;
			// }
			// if (Main.moonPhase < 2) {
			// 	shop.item[nextSlot].SetDefaults(ModContent.ItemType<ExampleSword>());
			// 	nextSlot++;
			// }
			// else if (Main.moonPhase < 4) {
			// 	shop.item[nextSlot].SetDefaults(ModContent.ItemType<ExampleGun>());
			// 	nextSlot++;
			// 	shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Weapons.ExampleBullet>());
			// 	nextSlot++;
			// }
			// else if (Main.moonPhase < 6) {
			// 	shop.item[nextSlot].SetDefaults(ModContent.ItemType<ExampleStaff>());
			// 	nextSlot++;
			// }
			// else {
			// }
			// // Here is an example of how your npc can sell items from other mods.
			// var modSummonersAssociation = ModLoader.GetMod("SummonersAssociation");
			// if (modSummonersAssociation != null) {
			// 	shop.item[nextSlot].SetDefaults(modSummonersAssociation.ItemType("BloodTalisman"));
			// 	nextSlot++;
			// }

			// if (!Main.LocalPlayer.GetModPlayer<ExamplePlayer>().examplePersonGiftReceived && ModContent.GetInstance<ExampleConfigServer>().ExamplePersonFreeGiftList != null)
			// {
			// 	foreach (var item in ModContent.GetInstance<ExampleConfigServer>().ExamplePersonFreeGiftList)
			// 	{
			// 		if (item.IsUnloaded)
			// 			continue;
			// 		shop.item[nextSlot].SetDefaults(item.Type);
			// 		shop.item[nextSlot].shopCustomPrice = 0;
			// 		shop.item[nextSlot].GetGlobalItem<ExampleInstancedGlobalItem>().examplePersonFreeGift = true;
			// 		nextSlot++;
			// 		// TODO: Have tModLoader handle index issues.
			// 	}	
			// }
		}

		public override void NPCLoot() {
			// TODO make geraldos stick
			Item.NewItem(npc.getRect(), ModContent.ItemType<Items.Heroes.GeraldoStaff>());
		}

		// Make this Town NPC teleport to the King and/or Queen statue when triggered.
		public override bool CanGoToStatue(bool toKingStatue) {
			return true;
		}

		// Make something happen when the npc teleports to a statue. Since this method only runs server side, any visual effects like dusts or gores have to be synced across all clients manually.
		public override void OnGoToStatue(bool toKingStatue) {
			if (Main.netMode == NetmodeID.Server) {
				ModPacket packet = mod.GetPacket();
				packet.Write((byte)ExampleModMessageType.ExampleTeleportToStatue);
				packet.Write((byte)npc.whoAmI);
				packet.Send();
			}
			else {
				StatueTeleport();
			}
		}

		public override void TownNPCAttackStrength(ref int damage, ref float knockback) {
			damage = 20;
			knockback = 4f;
		}

		public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown) {
			cooldown = 30;
			randExtraCooldown = 30;
		}
		// make this shoot the staff projectile
		public override void TownNPCAttackProj(ref int projType, ref int attackDelay) {
			projType = ModContent.ProjectileType<GeraldoLightning>();
			attackDelay = 1;
		}

		public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset) {
			multiplier = 12f;
			randomOffset = 2f;
		}
	}
}