using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using System;
using Terraria.DataStructures;

namespace BTDMod.Projectiles
{
    class NecroSkull : ModProjectile
    {
        const float MAX_ANGLE_CHANGE = 0.05f;
        const int UNIQUE_FRAMES = 3;
        const int FRAMES = (UNIQUE_FRAMES * 2) - 2;
        const float HOMING_DELAY = 15;
        int currentFrame;
        bool hitTarget;
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
            Projectile.scale = 0.6f;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20;
            Projectile.tileCollide = false;
            Projectile.netImportant = true;
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = UNIQUE_FRAMES;
        }
        public override void AI()
        {
            // deals with animation frames
            float frameTime = 10 / (Projectile.extraUpdates + 1);
            // frameTime is how long each frame should last for
            if (++Projectile.frameCounter >= frameTime)
            {
                Projectile.frameCounter = 0;
                // allows the animation to loop forwards then backwards etc.
                currentFrame = ++currentFrame % FRAMES;
                Projectile.frame = currentFrame >= UNIQUE_FRAMES? FRAMES - currentFrame: currentFrame;
            }
            SpawnDust();
            Timer++;
            if (Timer < HOMING_DELAY) return;
            NPC closest = FindClosestNPC(750);
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
            FixRotation();
        }
        // does particle effects
        // copied from joost focussoulsbeam
        private void SpawnDust() {
            Vector2 dustPos = Projectile.Center;
            float num1 = Projectile.velocity.ToRotation() + ((Main.rand.NextBool(2)? -1.0f : 1.0f) * 1.57f);
            float num2 = (float)((Main.rand.NextDouble() * 0.8f) + 1.0f);
            Vector2 dustVel = new((float)Math.Cos(num1) * num2, (float)Math.Sin(num1) * num2);
            Dust dust = Main.dust[Dust.NewDust(dustPos, 0, 0, DustID.Shadowflame, dustVel.X, dustVel.Y, 0)];
            dust.noGravity = true;
            dust.scale = 1.2f;
        }
        // ensures the skull is upright when spawned and before hitting an enemy,
        // but doesnt flip the sprite anymore after it hits an enemy cos it looks weird
        private void FixRotation() {
            Projectile.rotation = Projectile.velocity.ToRotation();
            if (hitTarget) return;
            if (Projectile.velocity.X < 0) {
                Projectile.rotation += (float)Math.PI;
                Projectile.spriteDirection = -1;
            }
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
                        sqrMaxDetectDistance = sqrDistanceToTarget;
                        closestNPC = target;
					}
				}
			}
            return closestNPC;
		}
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            // spawns 5 more skulls if the target is dead
            if (!target.active) {
                // the new projectiles spawn at an angle
                const int numProj = 5;
                double baseAngle = Math.Atan2(Projectile.velocity.X, Projectile.velocity.Y);
                const double spread = 20 / (180 / Math.PI); // each projectile spawns at a 20 degree angle from the previous
                float baseSpeed = Projectile.velocity.Length();
                double angle = baseAngle - (spread * (numProj - 1) / 2);
                for (int i = 0; i < numProj; i++) {
                    angle += spread;
                    Vector2 velocity = new(baseSpeed * (float)Math.Sin(angle), baseSpeed * (float)Math.Cos(angle));
                    Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center, velocity, Projectile.type, damage, knockback, Projectile.owner);
                }
            }
            hitTarget = true;
        }
        // makes the sprite rotation correct when spawned from killing an enemy
        public override void OnSpawn(IEntitySource source)
        {
            FixRotation();
        }
    }
}