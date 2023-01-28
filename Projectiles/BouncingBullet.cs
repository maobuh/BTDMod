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
        // an array that stores npcs that have already been hit by the bullet
        NPC[] alreadyHit = new NPC[4];
        const int ELITE = 2;
        const int SUPPLY = 1;
        const float DMG_MULT = 1.2f;
        public float Bounce {
            get => Projectile.localAI[1];
            set => Projectile.localAI[1] = value;
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
            Projectile.netUpdate = true;
            Player player = Main.player[Projectile.owner];
            if (player.HasBuff<SupplyDropCooldown>()) {
                int index = player.FindBuffIndex(ModContent.BuffType<SupplyDropCooldown>());
                int timeLeft = player.buffTime[index];
                // reduce the cooldown of the debuff based on the damage dealt by the bullet
                timeLeft -= damage / 300 * 60;
                // delete the buff, which we will then reapply based on the new timeleft
                player.DelBuff(index);
                // if the cooldown is 0, the debuff is not reapplied
                if (timeLeft <= 0) return;
                // add a debuff that corresponds to the cooldown of the supply drop
                player.AddBuff(ModContent.BuffType<SupplyDropCooldown>(), timeLeft);
            }
            // add hit target to array 
            alreadyHit[(int) Bounce] = target;
            Bounce++;
            // if the bullet has bounced the maximum number of times, and the bullet was shot by
            // the 005 Sniper (Projectile.ai[0] == 1)
            if (Projectile.ai[0] == ELITE && Bounce == 4) {
                player.AddBuff(ModContent.BuffType<EliteBuff>(), 600);
            }
            // damage increases by DMG_MULT every bounce for 040 and 050
            if ((Projectile.ai[0] == SUPPLY || Projectile.ai[0] == ELITE) && Bounce >= 1) {
                Projectile.damage = (int) (damage * DMG_MULT);
            }
            float maxDetectRadius = 480f;
            // homing range increases from elite buff
            if (player.HasBuff<EliteBuff>()) {
                maxDetectRadius *= 1.5f;
            }
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
            Projectile.rotation = Projectile.velocity.ToRotation();
        }
    }
}