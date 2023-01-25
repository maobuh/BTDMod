using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using System;
using BTDMod.Dusts;

namespace BTDMod.Projectiles
{
    class ArchmageBullet : ModProjectile
    {
        const float MAX_ANGLE_CHANGE = 0.4f;
        const float HOMING_DELAY = 15;
        float Timer {
            get => Projectile.localAI[0];
            set => Projectile.localAI[0] = value;
        }
        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 16;
            Projectile.penetrate = 3;
            Projectile.alpha = 0;
            Projectile.light = 0.5f;
            Projectile.friendly = true;
            Projectile.timeLeft = 480;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.scale = 1.2f;
            Projectile.penetrate = 7;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20;
            Projectile.netImportant = true;
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
            Timer++;
            // spawns dusts at 8 frame intervals
            if ((Timer % 8) == 0) {
                SpawnDust();
            }
            // does not home until after a delay
            if (Timer < HOMING_DELAY) return;
            // finds the closest npc to home in onto
            NPC closest = FindClosestNPC(500);
            if (closest == null) return;

            // the stuff below does the projectile homing
            Vector2 targetDir = closest.Center - Projectile.Center;
            Vector2 velocity = Projectile.velocity;
            targetDir.Normalize();
            velocity.Normalize();
            // THIS FUCKING WORKS, THANK YOU MATH STACKEXCHANGE GUY https://math.stackexchange.com/questions/878785/how-to-find-an-angle-in-range0-360-between-2-vectors
            // finds the angle between the targetDir and velocity
            // this will slowly curve the projectile to the correct position as AI reruns
            float dotProd = Vector2.Dot(targetDir, velocity);
            dotProd = dotProd > 1? 1: dotProd;
            float det = (targetDir.X * velocity.Y) - (targetDir.Y * velocity.X);
            double angleChange = Math.Atan2(det, dotProd);
            // the change in the angle has a maximum to ensure a nice looking curve
            if (angleChange > MAX_ANGLE_CHANGE) {
                angleChange = angleChange > 0? MAX_ANGLE_CHANGE: -MAX_ANGLE_CHANGE;
            }
            double baseAngle = Math.Atan2(Projectile.velocity.X, Projectile.velocity.Y);
            baseAngle += angleChange;
            float baseSpeed = Projectile.velocity.Length();
            // rotate the velocity by the angleChange
            Projectile.velocity = new(baseSpeed * (float)Math.Sin(baseAngle), baseSpeed * (float)Math.Cos(baseAngle));
            // rotate sprite to point in the correct direction
            Projectile.rotation = Projectile.velocity.ToRotation();
        }
        // does particle effects
        // copied from joost focussoulsbeam
        private void SpawnDust() {
            Vector2 dustPos = Projectile.Center;
            Dust dust = Main.dust[Dust.NewDust(dustPos, 0, 0, ModContent.DustType<ArchmageDust>(), 0, 0, 0)];
            dust.rotation = Projectile.velocity.ToRotation();
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
    }
}