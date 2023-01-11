using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BTDMod.Projectiles
{
    class HitscanBullet : ModProjectile
    {
        const int MAX_DISTANCE = 4000;
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 2;
            Projectile.penetrate = 1;
            Projectile.alpha = 0;
            Projectile.friendly = true;
            Projectile.scale = 1.2f;
            Projectile.timeLeft = 10;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.hide = true;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Ichor, 600);
            target.AddBuff(BuffID.Daybreak, 300);
            target.AddBuff(BuffID.CursedInferno, 300);
            target.AddBuff(BuffID.ShadowFlame, 300);
            target.AddBuff(BuffID.Frostburn, 300);
            target.AddBuff(BuffID.OnFire, 300);
            target.AddBuff(BuffID.Poisoned, 300);
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
            Vector2? position = FindEnemyPosition();
            if (position == null) {
                Projectile.Kill();
                return;
            }
            Projectile.position = (Vector2) position;
        }
        // Finds the position of the first enemy in the path, return null if no enemy found
        private Vector2? FindEnemyPosition() {
            Vector2? position = null;
            // finds distance to first enemy in path
			float TargetDistance = MAX_DISTANCE;
			// iterate over all npcs
			for (int k = 0; k < Main.maxNPCs; k++) {
				NPC target = Main.npc[k];
				if (target.CanBeChasedBy()) {
					float DistanceToTarget = Vector2.Distance(target.Center, Projectile.Center);
					if (DistanceToTarget < TargetDistance) {
						Rectangle targetHitbox = target.Hitbox;
						float point = 0f;
						// check collision with enemy hitbox
						if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center,
																Projectile.Center + (Projectile.velocity * MAX_DISTANCE), 10, ref point)) {
							TargetDistance = DistanceToTarget;
                            position = target.Center;
						}
					}
				}
			}
            return position;
        }
    }
}