using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.Enums;
using ReLogic.Content;
using BTDMod.Items.Sniper;

namespace BTDMod.Projectiles
{
	public class LaserSight : ModProjectile
	{
		const int INTERVAL = 20;
		private const float MOVE_DISTANCE = 20f;
		const int SCOPE_RADIUS = 23;
		public float Distance {
			get => Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}

		public override void SetDefaults() {
			Projectile.width = 10;
			Projectile.height = 2;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.tileCollide = false;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.netUpdate = false;
		}
		public override bool PreDraw(ref Color lightColor) {
			// based off ExampleLaser, spawns a lot of the sprites in a line with gaps
			Vector2 origin = Projectile.Center;
            for (float i = MOVE_DISTANCE; i < Distance; i += INTERVAL) {
				origin += INTERVAL * Projectile.velocity;
				Main.EntitySpriteDraw((Texture2D) ModContent.Request<Texture2D>("BTDMod/Projectiles/LaserSight"), origin - Main.screenPosition,
					new Rectangle(0, 0, 10, 2), Color.White, Projectile.velocity.ToRotation(),
					new Vector2(10 * .5f, 2 * .5f), 1, 0, 0);
			}
			return false;
		}
		// // Checks if the projectile collides
		// public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox) {
		// 	Vector2 unit = Projectile.velocity;
		// 	float point = 0f;
		// 	// Run an AABB versus Line check to look for collisions, look up AABB collision first to see how it works
		// 	// It will look for collisions on the given line using AABB
		// 	return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center,
		// 		Projectile.Center + (unit * Distance), 10, ref point);
		// }
		public override void AI() {
			// for multiplayer visuals
            if (Projectile.owner != Main.myPlayer) return;
			Player player = Main.player[Projectile.owner];
			// if the player is no longer holding the weapon, remove the laser sight
			if (player.HeldItem.type != ModContent.ItemType<Sniper500>()) {
                Projectile.Kill();
            }
			// move the laser sight with the player
			Projectile.Center = player.Center + (Projectile.velocity * MOVE_DISTANCE);
			// velocity is in the direction of the mouse
			Projectile.velocity = Main.MouseWorld - player.Center;
			Projectile.velocity = Projectile.velocity.SafeNormalize(Vector2.Zero);
			// continuously refresh the projectile lifespan 
			Projectile.timeLeft = 2;
            SetLaserPosition(player);
            CastLights();
		}
		// finds the distance at which the laser first hits a tile or enemy
		private void SetLaserPosition(Player player) {
			// finds distance to first tile, max distance at the mouse position
			float mouseDistance = Vector2.Distance(Main.MouseWorld, player.Center);
			for (Distance = MOVE_DISTANCE; Distance <= mouseDistance; Distance += 5f) {
				Vector2 start = Projectile.Center + (Projectile.velocity * Distance);
				// check if tile can be hit
				if (!Collision.CanHit(player.Center, 1, 1, start, 1, 1)) {
					Distance -= 5f;
					break;
				}
			}
			// finds distance to first enemy in path
			float TargetDistance = mouseDistance;
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
																Projectile.Center + (Projectile.velocity * Distance), 10, ref point)) {
							TargetDistance = DistanceToTarget;
						}
					}
				}
			}
			// distance is whatever is smaller of distance to tile or npc
			Distance = Distance < TargetDistance? Distance: TargetDistance;
			// so that the laser sight doesnt overlap with the scope sprite
			if (Distance == mouseDistance) {
				Distance -= SCOPE_RADIUS;
			}
		}
        // overlaps the laser with a line of light
		private void CastLights() {
			//Add lights
            DelegateMethods.v3_1 = new Vector3(0.1f, 0.8f, 1f);
            Utils.PlotTileLine(Projectile.Center, Projectile.Center + (Projectile.velocity * (Distance - MOVE_DISTANCE)), 26, new Utils.TileActionAttempt(DelegateMethods.CastLight));
		}
		public override bool ShouldUpdatePosition() => false;
		public override void CutTiles()
		{
			DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
			Vector2 unit = Projectile.velocity;
			Utils.PlotTileLine(Projectile.Center, Projectile.Center + (unit * Distance), (Projectile.width + 16) * Projectile.scale, new Utils.TileActionAttempt(DelegateMethods.CutTiles));
		}
		// cant destroy grass, mushrooms etc. 
		public override bool? CanDamage()
        {
            return false;
        }
	}
}
