using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using System;

namespace BTDMod.Projectiles
{
    class Dart : ModProjectile
    {
        private int bunces = 2;
        public override void SetDefaults()
        {
            Projectile.damage = 1;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.friendly = true;
            Projectile.height = 32;
            Projectile.width = 20;
            Projectile.aiStyle = ProjAIStyleID.Arrow;
			Projectile.localNPCHitCooldown = 0;
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
            base.AI();
        }
        public override bool OnTileCollide(Vector2 oldVelocity) {
			if (bunces > 0)
			{
				Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
				SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
				// If the projectile hits the left or right side of the tile, reverse the X velocity
				if (Math.Abs(Projectile.velocity.X - oldVelocity.X) > float.Epsilon) {
					Projectile.velocity.X = -(oldVelocity.X * 0.5f);
				}

				// If the projectile hits the top or bottom side of the tile, reverse the Y velocity
				if (Math.Abs(Projectile.velocity.Y - oldVelocity.Y) > float.Epsilon) {
					Projectile.velocity.Y = -(oldVelocity.Y * 0.5f);
				}
				bunces--;
			}
			else
			{
				Projectile.Kill();
			}
			return false;
		}
    }
}