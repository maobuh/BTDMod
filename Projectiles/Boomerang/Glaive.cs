using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using System;
using System.Linq;
using BTDMod.Buffs;

namespace BTDMod.Projectiles.Boomerang
{
    class Glaive : ModProjectile
    {
        protected float maxDetectRadius = 320f;
        // an array that stores npcs that have already been hit by the bullet
        NPC[] alreadyHit = new NPC[30];
        public float Bounce {
            get => Projectile.localAI[0];
            set => Projectile.localAI[0] = value;
        }
        public override void SetDefaults()
        {
            Projectile.width = 42;
            Projectile.height = 40;
            Projectile.penetrate = 10;
            Projectile.alpha = 0;
            Projectile.light = 0.5f;
            Projectile.friendly = true;
            Projectile.scale = 1f;
            Projectile.timeLeft = 360;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.netUpdate = true;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            // add hit target to array 
            alreadyHit[(int) Bounce] = target;
            Bounce++;
            // homing range increases from elite buff
            NPC closestNPC = FindClosestNPC(maxDetectRadius, alreadyHit);
			if (closestNPC == null) {
                Projectile.Kill();
                return;
            }
            float projSpeed = Projectile.velocity.Length();
            // If found, change the velocity of the projectile and turn it in the direction of the target
			// Use the SafeNormalize extension method to avoid NaNs returned by Vector2.Normalize when the vector is zero
			Projectile.velocity = (closestNPC.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * projSpeed;
        }
        public NPC FindClosestNPC(float maxDetectDistance, NPC[] alreadyHit) {
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
                        if (!alreadyHit.Contains(target)) {
                            sqrMaxDetectDistance = sqrDistanceToTarget;
						    closestNPC = target;
                        }
					}
				}
			}
            return closestNPC;
		}
        public override void AI()
        {
            // makes the boomerang spin
            Projectile.rotation += 0.2f;
        }
    }
}