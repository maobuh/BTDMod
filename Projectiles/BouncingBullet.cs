using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using System;
using System.Linq;
using BTDMod.Buffs;

namespace BTDMod.Projectiles
{
    class BouncingBullet : ModProjectile
    {
        NPC[] alreadyHit = new NPC[4];
        public float Bounce {
            get => Projectile.ai[1];
            set => Projectile.ai[1] = value;
        }
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 2;
            Projectile.penetrate = 4;
            Projectile.alpha = 0;
            Projectile.light = 0.5f;
            Projectile.friendly = true;
            Projectile.scale = 1.2f;
            Projectile.timeLeft = 600;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.extraUpdates = 1;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[Projectile.owner];
            if (player.HasBuff<SupplyDropCooldown>()) {
                int index = player.FindBuffIndex(ModContent.BuffType<SupplyDropCooldown>());
                int timeLeft = player.buffTime[index];
                timeLeft -= damage / 300 * 60;
                player.DelBuff(index);

                if (timeLeft <= 0) return;
                player.AddBuff(ModContent.BuffType<SupplyDropCooldown>(), timeLeft);
            }
            alreadyHit[(int) Bounce] = target;
            Bounce++;
            const float maxDetectRadius = 640f;
            NPC closestNPC = FindClosestNPC(maxDetectRadius, alreadyHit);
			if (closestNPC == null) {
                Projectile.Kill();
                return;
            }
            const float projSpeed = 20f;
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
            Projectile.rotation = Projectile.velocity.ToRotation();
        }
    }
}