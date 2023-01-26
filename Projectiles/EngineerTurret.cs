using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using Terraria.ModLoader;
using BTDMod.Buffs;

namespace BTDMod.Projectiles
{
    // This minion shows a few mandatory things that make it behave properly.
	// Its attack pattern is simple: If an enemy is in range of 43 tiles, it will fly to it and deal contact damage
	// If the player targets a certain NPC with right-click, it will fly through tiles to it
	// If it isn't attacking, it will float near the player with minimal movement
	public class EngineerTurret: ModProjectile
	{
		const int shootSpeed = 20;
		float Timer {
			get => Projectile.localAI[0];
			set => Projectile.localAI[0] = value;
		}
		Vector2 nailVelocity = Vector2.Zero;
		// some magic number to make the turret look like its sitting on the tile properly
		Vector2 spriteOffset = new(0,-5);
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Engineer Turret");
			// This is necessary for right-click targeting
			ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;

			Main.projPet[Projectile.type] = true; // Denotes that this projectile is a pet or minion

			ProjectileID.Sets.MinionSacrificable[Projectile.type] = true; // This is needed so your minion can properly spawn when summoned and replaced when other minions are summoned
			ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true; // Make the cultist resistant to this projectile, as it's resistant to all homing projectiles.
		}

		public sealed override void SetDefaults() {
			Projectile.width = 34;
			Projectile.height = 24;
			Projectile.tileCollide = true;

			// These below are needed for a minion weapon
			Projectile.friendly = true; // Only controls if it deals damage to enemies on contact (more on that later)
			Projectile.minion = true; // Declares this as a minion (has many effects)
			Projectile.DamageType = DamageClass.Summon; // Declares the damage type (needed for it to deal damage)
			Projectile.minionSlots = 0.5f; // Amount of slots this minion occupies from the total minion slots available to the player (more on that later)
			Projectile.penetrate = -1; // Needed so the minion doesn't despawn on collision with enemies or tiles
			Projectile.netUpdate = true;
		}

		// Here you can decide if your minion breaks things like grass or pots
		public override bool? CanCutTiles() {
			return false;
		}

		// This is mandatory if your minion deals contact damage (further related stuff in AI() in the Movement region)
		public override bool MinionContactDamage() {
			return false;
		}
        public override bool PreDraw(ref Color lightColor)
        {
			// draws the stand of the turret
			Main.EntitySpriteDraw((Texture2D) ModContent.Request<Texture2D>("BTDMod/Projectiles/EngineerTurretStand"), Projectile.Center + spriteOffset - Main.screenPosition,
			new Rectangle(0, 0, 34, 12), Color.White, 0, new Vector2(34 / 2, 12 / 2), 1f, 0, 0);
			// draws the turret, rotates it according to the direction it's firing the nails
			Main.EntitySpriteDraw((Texture2D) ModContent.Request<Texture2D>("BTDMod/Projectiles/EngineerTurret"), Projectile.Center + spriteOffset + new Vector2(0,-10) - Main.screenPosition,
				new Rectangle(0, 0, 34, 16), Color.White, nailVelocity.ToRotation(), new Vector2(34 / 2, 16 / 2), 1f, 0, 0);
            return false;
        }
        public override void AI() {
			Player owner = Main.player[Projectile.owner];
			if (!CheckActive(owner)) {
				return;
			}
			SearchForTargets(owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter);
			if (!foundTarget) return;
			if (Timer % shootSpeed == 0) {
				const float projSpeed = 20f;
				nailVelocity = (targetCenter - Projectile.Center).SafeNormalize(Vector2.Zero) * projSpeed;
				Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center + new Vector2(0,-16), nailVelocity, ModContent.ProjectileType<Nail>(), Projectile.damage, 0, owner.whoAmI);
			}
			Timer++;
		}

		// This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not
		private bool CheckActive(Player owner) {
			if (owner.dead || !owner.active) {
				owner.ClearBuff(ModContent.BuffType<EngineerTurretBuff>());

				return false;
			}

			if (owner.HasBuff(ModContent.BuffType<EngineerTurretBuff>())) {
				Projectile.timeLeft = 2;
			}

			return true;
		}

		private void SearchForTargets(Player owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter) {
			// Starting search distance
			distanceFromTarget = 600f;
			targetCenter = Projectile.position;
			foundTarget = false;

			// This code is required if your minion weapon has the targeting feature
			if (owner.HasMinionAttackTargetNPC) {
				NPC npc = Main.npc[owner.MinionAttackTargetNPC];
				float between = Vector2.Distance(npc.Center, Projectile.Center);

				// Reasonable distance away so it doesn't target across multiple screens
				if (between < 2000f) {
					distanceFromTarget = between;
					targetCenter = npc.Center;
					foundTarget = true;
				}
			}

			if (!foundTarget) {
				// This code is required either way, used for finding a target
				for (int i = 0; i < Main.maxNPCs; i++) {
					NPC npc = Main.npc[i];

					if (npc.CanBeChasedBy()) {
						float between = Vector2.Distance(npc.Center, Projectile.Center);
						bool closest = Vector2.Distance(Projectile.Center, targetCenter) > between;
						bool inRange = between < distanceFromTarget;
						bool lineOfSight = Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height);
						// Additional check for this specific minion behavior, otherwise it will stop attacking once it dashed through an enemy while flying though tiles afterwards
						// The number depends on various parameters seen in the movement code below. Test different ones out until it works alright
						bool closeThroughWall = between < 100f;

						if (((closest && inRange) || !foundTarget) && (lineOfSight || closeThroughWall)) {
							distanceFromTarget = between;
							targetCenter = npc.Center;
							foundTarget = true;
						}
					}
				}
			}
		}
		// ensures the turret is standing up when spawned
		public override void OnSpawn(IEntitySource source)
        {
            Projectile.rotation = 0;
        }
	}
}