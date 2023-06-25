using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BTDMod.Buffs;
using Microsoft.Xna.Framework;
using System;

namespace BTDMod.Projectiles.Glue
{
    class GlueBall : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.height = 14;
            Projectile.width = 14;
            Projectile.knockBack = 0;
            Projectile.penetrate = 6;
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (!target.boss) {
                target.AddBuff(ModContent.BuffType<Slowed>(), 90);
            }
            SpawnDust();
            base.OnHitNPC(target, damage, knockback, crit);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            SpawnDust();
            return base.OnTileCollide(oldVelocity);
        }
        private void SpawnDust() {
            Vector2 dustPos = Projectile.Center;
            float projVel = Projectile.velocity.Length();
            float initAngle = Projectile.velocity.ToRotation();
            const int numDust = 10;
            const float angleDiff = (float)Math.PI / numDust / 2;
            // spawn dust in a fountain like shape
            for (int i = 0; i < (numDust/2); i++) {
                float angle = initAngle + (i * angleDiff);
                Vector2 dustVel = new((float)Math.Cos(angle), (float)Math.Sin(angle));
                dustVel *= projVel / 5;
                Dust dust = Main.dust[Dust.NewDust(dustPos, 0, 0, DustID.Honey)];
                dust.noGravity = true;
                dust.scale = 1.5f;
                dust.velocity = dustVel;
            }
            for (int i = 0; i < (numDust/2); i++) {
                float angle = initAngle - (i * angleDiff);
                Vector2 dustVel = new((float)Math.Cos(angle), (float)Math.Sin(angle));
                dustVel *= projVel / 5;
                Dust dust = Main.dust[Dust.NewDust(dustPos, 0, 0, DustID.Honey)];
                dust.noGravity = true;
                dust.scale = 1.5f;
                dust.velocity = dustVel;
            }
        }
    }
}