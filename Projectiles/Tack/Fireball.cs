using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace BTDMod.Projectiles.Tack
{
    class Fireball : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 120;
            Projectile.height = 120;
            Projectile.penetrate = 1;
            Projectile.alpha = 0;
            Projectile.light = 0.5f;
            Projectile.friendly = true;
            Projectile.timeLeft = 120;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.localNPCHitCooldown = 10;
            Projectile.usesLocalNPCImmunity = true;
        }
        public override void AI()
        {
            NPC closest = FindClosestNPC(1000);
            if (closest == null) return;
            Projectile.velocity = (closest.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * 10;
            SpawnDust();
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
        private void SpawnDust() {
            Vector2 dustPos = Projectile.Center;
            float num1 = Projectile.velocity.ToRotation() + ((Main.rand.NextBool(2)? -1.0f : 1.0f) * 1.57f);
            float num2 = (float)((Main.rand.NextDouble() * 0.8f) + 1.0f);
            Vector2 dustVel = new((float)Math.Cos(num1) * num2, (float)Math.Sin(num1) * num2);
            // trail dust
            Dust dust = Main.dust[Dust.NewDust(dustPos, 0, 0, DustID.Flare, dustVel.X, dustVel.Y)];
            dust.noGravity = true;
            dust.scale = 5f;
        }
    }
}