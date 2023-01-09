using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using System;

namespace BTDMod.Projectiles
{
    class UltraJuggernaut : ModProjectile
    {
        int totalCollisions;
        int originalPenetrate;
        public override void SetDefaults()
        {
            Projectile.damage = 1;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 200;
            originalPenetrate = Projectile.penetrate;
            Projectile.width = 64;
            Projectile.height = 60;
            Projectile.friendly = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ultra Juggernaut");
        }
        public override void AI()
        {
            // split the projectile 
            if (Projectile.penetrate == originalPenetrate / 2) {
                SplitProjectile(Projectile.velocity, true);
                Projectile.penetrate--;
            }
            base.AI();
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            totalCollisions++;
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
			SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            // If the projectile hits the left or right side of the tile, reverse the X velocity
            if (Math.Abs(Projectile.velocity.X - oldVelocity.X) > float.Epsilon) {
                Projectile.velocity.X = -oldVelocity.X;
            }
            // If the projectile hits the top or bottom side of the tile, reverse the Y velocity
            if (Math.Abs(Projectile.velocity.Y - oldVelocity.Y) > float.Epsilon) {
                Projectile.velocity.Y = -oldVelocity.Y;
            }
            if (totalCollisions > 1)
            {
                SplitProjectile(Projectile.velocity, true);
                Projectile.Kill();
            }
            return false;
        }
        public override void Kill(int timeLeft)
        {
            SplitProjectile(Projectile.velocity, false);
            base.Kill(timeLeft);
        }
        private void SplitProjectile(Vector2 velocity, bool offset) {
            Vector2 oldAngle = velocity;
            oldAngle.Normalize();
            float newAngle;
            if (offset)
            {
                // calculate vectors 30 degrees apart from the original (the number is 30 degrees in radians(0.523599f))
                newAngle = (float)Math.Atan(oldAngle.Y / oldAngle.X) + 0.523599f;
            } else {
                newAngle = (float)Math.Atan(oldAngle.Y / oldAngle.X);
            }
            Vector2 direction = new((float) Math.Cos(newAngle), (float) Math.Sin(newAngle));
            // summon new juggernaut projectile 60 degrees apart from each other
            for (int i = 0; i < 6; i++)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, Projectile.velocity.Length() * direction, ModContent.ProjectileType<Juggernaut>(), Projectile.damage, 0, Main.player[Projectile.owner].whoAmI);
                // recalculate next projectiles direction 60(1.0472f) degrees away from the original projectile
                newAngle = (float)Math.Atan(oldAngle.Y / oldAngle.X) - (1.0472f * i);
                direction = new Vector2((float) Math.Cos(newAngle), (float) Math.Sin(newAngle));
            }
        }
    }
}