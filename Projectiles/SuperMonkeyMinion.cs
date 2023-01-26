using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;

namespace BTDMod.Projectiles
{
    class SuperMonkeyMinion : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Super Monkey Fan Club Member");
            Main.projFrames[Projectile.type] = 2;
            Main.projPet[Projectile.type] = true;
        }
        public override void SetDefaults()
        {
			Projectile.width = 62;
			Projectile.height = 61;
			Projectile.tileCollide = false;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.penetrate = 1;
            Projectile.timeLeft = 900;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (player.dead || !player.active) {
				Projectile.Kill();
			}
            // Starting search distance
			float distanceFromTarget = 700f;
			Vector2 targetCenter = Projectile.position;
			bool foundTarget = false;
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

            if (foundTarget && Projectile.timeLeft % 4 == 0) {
            	Projectile.frame = (Projectile.frame + 1) % 2;
                Projectile.rotation = (float)Math.Atan2(Projectile.DirectionTo(targetCenter).Y, Projectile.DirectionTo(targetCenter).X);
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, Projectile.DirectionTo(targetCenter) * 50, ModContent.ProjectileType<Dart>(), 2, 0, Projectile.owner);
            }
        }
    }
}