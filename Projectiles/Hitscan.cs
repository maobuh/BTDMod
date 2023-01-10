using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;

namespace BTDMod.Projectiles
{
	public class Hitscan : ModProjectile
	{
		private const float MOVE_DISTANCE = 20f;
		public float Distance {
			get => Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}

		public override void SetDefaults() {
			Projectile.width = 10;
			Projectile.height = 10;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.tileCollide = false;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.hide = true;
		}
		public override bool PreDraw(ref Color lightColor) {
			Vector2 origin = Projectile.Center;
            for (float i = MOVE_DISTANCE; i <= Distance; i += 10) {
				origin += 10 * Projectile.velocity;
				Main.NewText(Projectile.velocity.ToRotation());
				Main.EntitySpriteDraw((Texture2D) ModContent.Request<Texture2D>("joostitemport/Projectiles/Hitscan"), origin - Main.screenPosition,
					new Rectangle(0, 26, 16, 26), Color.White, Projectile.velocity.ToRotation(),
					new Vector2(16 * .5f, 26 * .5f), 1, 0, 0);
			}
			return false;
		}
		// Checks if the projectile collides
		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox) {
			Player player = Main.player[Projectile.owner];
			Vector2 unit = Projectile.velocity;
			float point = 0f;
			// Run an AABB versus Line check to look for collisions, look up AABB collision first to see how it works
			// It will look for collisions on the given line using AABB
			return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), player.Center,
				player.Center + (unit * Distance), 10, ref point);
		}
		public override void AI() {
            if (Projectile.owner != Main.myPlayer) return;
			Player player = Main.player[Projectile.owner];
			Projectile.position = player.Center + (Projectile.velocity * MOVE_DISTANCE);
			Projectile.velocity = Main.MouseWorld - player.Center;
			Projectile.velocity = Projectile.velocity.SafeNormalize(Vector2.Zero);
			Projectile.timeLeft = 60;
            SetLaserPosition(player);
            CastLights();
		}
		private void SetLaserPosition(Player player) {
			for (Distance = MOVE_DISTANCE; Distance <= 2200f; Distance += 5f) {
				Vector2 start = Projectile.Center + (Projectile.velocity * Distance);
				if (!Collision.CanHit(player.Center, 1, 1, start, 1, 1)) {
					Distance -= 5f;
					break;
				}
			}
		}
        // overlaps the laser with a line of light
		private void CastLights() {
			//Add lights
            DelegateMethods.v3_1 = new Vector3(0.1f, 0.8f, 1f);
            Utils.PlotTileLine(Projectile.Center, Projectile.Center + (Projectile.velocity * (Distance - MOVE_DISTANCE)), 26, new Utils.TileActionAttempt(DelegateMethods.CastLight));
		}
		public override bool ShouldUpdatePosition() => false;
	}
}
