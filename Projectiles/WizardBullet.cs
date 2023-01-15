using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using System;

namespace BTDMod.Projectiles
{
    class WizardBullet : ModProjectile
    {
        const float MAX_ANGLE_CHANGE = 0.15f;
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
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
            NPC closest = FindClosestNPC(500);
            if (closest == null) return;
            // the stuff below does the projectile homing
            Vector2 targetDir = closest.Center - Projectile.Center;
            Vector2 velocity = Projectile.velocity;
            targetDir.Normalize();
            velocity.Normalize();
            // THIS FUCKING WORKS, THANK YOU MATH STACKEXCHANGE GUY https://math.stackexchange.com/questions/878785/how-to-find-an-angle-in-range0-360-between-2-vectors
            // finds the angle between the targetDir and velocity
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
            Projectile.velocity = new(baseSpeed * (float)Math.Sin(baseAngle), baseSpeed * (float)Math.Cos(baseAngle));
            Projectile.rotation = Projectile.velocity.ToRotation();
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