using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BTDMod.Buffs;

namespace BTDMod.Projectiles
{
	public class PhoenixMinion : ModProjectile
	{
		const int flyRadius = 80;
		const float angleChange = (float) (1 / (180 / Math.PI));
		const int shootSpeed = 3;
		float Timer {
			get => Projectile.localAI[0];
			set => Projectile.localAI[0] = value;
		}
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Phoenix");
			Main.projFrames[Projectile.type] = 1;
			Main.projPet[Projectile.type] = true;
			ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
			ProjectileID.Sets.MinionShot[Projectile.type] = true;
			ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
		}
		public override void AI() {
			// active check (checks if player is alive, despawns minion if the player died)
			Player player = Main.player[Projectile.owner];
			if (player.dead) {
				player.ClearBuff(ModContent.BuffType<PhoenixBuff>());
			}
			// despawns the phoenix if the phoenix buff time runs out (which is the duration of the phoenix)
			if (!player.HasBuff(ModContent.BuffType<PhoenixBuff>())) {
				Projectile.Kill();
			}
			// creates the animation for the phoenix
			float dirAngle = Projectile.velocity.ToRotation() + angleChange;
			// velocity is in the direction of the centrifugal force
			Projectile.velocity = new((float) Math.Cos(dirAngle), (float) Math.Sin(dirAngle));
			// rotate the sprite by 90 deg so its in the correct direction
			Projectile.rotation = Projectile.velocity.ToRotation() - ((float) Math.PI / 2);
			// phoenix rotates in a circle around the player
			Projectile.Center = player.Center + (flyRadius * Projectile.velocity);

			// searches for closest enemy and shoots fireballs in the direction
			NPC closestNPC = FindClosestNPC(960);
			if (closestNPC == null) return;
			// fires a projectile every shootSpeed frames
			if (Timer % shootSpeed == 0) {
				const float projSpeed = 10f;
				Vector2 targetDir = (closestNPC.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * projSpeed;
				Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center, targetDir, ModContent.ProjectileType<PhoenixBreath>(), (int)player.GetDamage<MagicDamageClass>().ApplyTo(100), 0, player.whoAmI);
			}
			Timer++;
		}
		public NPC FindClosestNPC(float maxDetectDistance) {
			NPC closestNPC = null;

			// Using squared values in distance checks will let us skip square root calculations, drastically improving this method's speed.
			float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;

			// Loop through all NPCs(max always 200)
			for (int k = 0; k < Main.maxNPCs; k++) {
				NPC target = Main.npc[k];
				// Check if NPC able to be targeted. It means that NPC is
				// 1. active (alive)
				// 2. chaseable (e.g. not a cultist archer)
				// 3. max life bigger than 5 (e.g. not a critter)
				// 4. can take damage (e.g. moonlord core after all it's parts are downed)
				// 5. hostile (!friendly)
				// 6. not immortal (e.g. not a target dummy)
				if (target.CanBeChasedBy()) {
					// The DistanceSquared function returns a squared distance between 2 points, skipping relatively expensive square root calculations
					float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, Projectile.Center);
					// Check if it is within the radius
					if (sqrDistanceToTarget < sqrMaxDetectDistance) {
                        // check that the target has not already been hit
						sqrMaxDetectDistance = sqrDistanceToTarget;
						closestNPC = target;
					}
				}
			}
            return closestNPC;
		}
		public override void SetDefaults()
		{
			Projectile.netImportant = true;
			Projectile.width = 114;
			Projectile.height = 90;
			Projectile.friendly = true;
			Projectile.minion = true;
			Projectile.minionSlots = 0;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 600;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
		}
        public override bool MinionContactDamage()
        {
            return false;
        }
		public override bool? CanCutTiles() {
			return false;
		}
	}
}