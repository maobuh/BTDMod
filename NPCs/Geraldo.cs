using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using BTDMod.Items;
using System.Collections.Generic;

namespace BTDMod.NPCs
{
	[AutoloadHead]
	public class Geraldo : ModNPC
	{
		public override void SetStaticDefaults() {
			// DisplayName automatically assigned from .lang files, but the commented line below is the normal approach.
			DisplayName.SetDefault("Geraldo");
			Main.npcFrameCount[NPC.type] = 25;
			NPCID.Sets.ExtraFramesCount[NPC.type] = 9;
			NPCID.Sets.AttackFrameCount[NPC.type] = 4;
			NPCID.Sets.DangerDetectRange[NPC.type] = 700;
			NPCID.Sets.AttackType[NPC.type] = 0;
			NPCID.Sets.AttackTime[NPC.type] = 90;
			NPCID.Sets.AttackAverageChance[NPC.type] = 30;
			NPCID.Sets.HatOffsetY[NPC.type] = 4;
		}
		public override void SetDefaults() {
			NPC.townNPC = true;
			NPC.friendly = true;
			NPC.width = 18; // sprite
			NPC.height = 40; // sprite
			NPC.aiStyle = 7;
			NPC.damage = 10;
			NPC.defense = 15;
			NPC.lifeMax = 250;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.knockBackResist = 0.5f;
			AnimationType = NPCID.Guide;
		}
		public override bool CanTownNPCSpawn(int numTownNPCs, int money) {
			return NPC.downedBoss1;
		}
		public override List<string> SetNPCNameList() {
			return new List<string> {
				"Geraldo"
			};
		}
		public override string GetChat() {
			// int partyGirl = NPC.FindFirstNPC(NPCID.PartyGirl);
			// if (partyGirl >= 0 && Main.rand.NextBool(4)) {
			// 	return "Can you please tell " + Main.NPC[partyGirl].GivenName + " to stop decorating my house with colors?";
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
				// make geraldo speak to you somehow
			}
		}

		public override void SetupShop(Chest shop, ref int nextSlot) {
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<Pineapple>());
			nextSlot++;
			// shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Heroes.HotSauce>());
			// nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<NdujaFrittaTanto>());
			nextSlot++;
			if (Main.hardMode) {
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Heroes.RejuvPotion>());
				nextSlot++;
			}
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
			// // Here is an example of how your NPC can sell items from other mods.
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
		public override void OnKill () {
			// TODO make geraldos stick
			// Item.NewItem(NPC.GetSource_DropAsItem(), NPC.Center, NPC.Center, ModContent.ItemType<Items.Heroes.GeraldoStaff>());
		}

		// Make this Town NPC teleport to the King and/or Queen statue when triggered.
		public override bool CanGoToStatue(bool toKingStatue) {
			return true;
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
			// projType = ModContent.ProjectileType<GeraldoLightning>();
			attackDelay = 1;
		}

		public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset) {
			multiplier = 12f;
			randomOffset = 2f;
		}
	}
}