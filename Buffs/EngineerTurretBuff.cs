using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using BTDMod.Projectiles;

namespace BTDMod.Buffs {
    class EngineerTurretBuff: ModBuff
    {
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Engineer Turret");
			Description.SetDefault("Shooting industrial love");
			Main.debuff[Type] = false;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
		}
		public override void Update(Player player, ref int buffIndex) {
			// If the minions exist reset the buff time, otherwise remove the buff from the player
			if (CountTurrets(player) > 0) {
				player.buffTime[buffIndex] = 2;
			}
			else {
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
		private static int CountTurrets(Player player) {
			int num = 0;
			int[] TurretTypes =
			{
				ModContent.ProjectileType<EngineerTurret>(),
				ModContent.ProjectileType<EngineerTurretBlack>(),
				ModContent.ProjectileType<EngineerTurretYellow>(),
				ModContent.ProjectileType<EngineerTurretRed>(),
				ModContent.ProjectileType<EngineerTurretWhite>(),
				ModContent.ProjectileType<EngineerTurretPurple>()
			};
			foreach (int i in TurretTypes) {
				num += player.ownedProjectileCounts[i];
			}
			return num;
		}
	}
}