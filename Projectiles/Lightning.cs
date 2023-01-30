using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace BTDMod.Projectiles
{
    // Projectile.ai[0] will be if the lightning is an offshoot or the main one
    // the main one will have a lot of pierce but the offshoot one dies as soon as it hits an enemy
    class Lightning : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.ShadowBeamFriendly);
            Projectile.alpha = 255;
        }
        public override void AI()
        {
            Projectile.localAI[0]++;
            if (Projectile.localAI[0] > 9f)
            {
                for (int i = 0; i < 4; i++)
                {
                    Vector2 position10 = Projectile.position;
                    position10 -= Projectile.velocity * (i * 0.25f);
                    int dustID = Dust.NewDust(position10, 1, 1, DustID.BubbleBurst_Green); // make new dust
                    Main.dust[dustID].position = position10;
                    Main.dust[dustID].scale = Main.rand.Next(70, 110) * 0.013f;
                    Dust dust195 = Main.dust[dustID];
                    dust195.velocity *= 0.2f;
                }
            }
            // spawn new Projectile if there is one close by
            // make sure new Projectile has Projectile.ai[0] = 1
            base.AI();
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Projectile.ai[0] == 1) {
                Projectile.Kill();
            }
            base.OnHitNPC(target, damage, knockback, crit);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (Projectile.velocity.X != oldVelocity.X)
			{
                Projectile.position.X += Projectile.velocity.X;
				Projectile.velocity.X = -oldVelocity.X;
			}
			if (Projectile.velocity.Y != oldVelocity.Y)
			{
                Projectile.position.Y += Projectile.velocity.Y;
				Projectile.velocity.Y = -oldVelocity.Y;
			}
			return false; // return false because we are handling collision
		}
    }
}