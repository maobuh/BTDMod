using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using System;
using BTDMod.Dusts;

namespace BTDMod.Projectiles
{
    class MageBullet : ModProjectile
    {
        float Timer {
            get => Projectile.localAI[0];
            set => Projectile.localAI[0] = value;
        }
        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 16;
            Projectile.penetrate = 7;
            Projectile.alpha = 0;
            Projectile.light = 0.5f;
            Projectile.friendly = true;
            Projectile.timeLeft = 480;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.scale = 1.2f;
            Projectile.penetrate = 7;
            Projectile.netImportant = true;
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
            Timer++;
            if ((Timer %= 8) == 0) {
                SpawnDust();
            }
            // rotate sprite to point in the correct direction
            Projectile.rotation = Projectile.velocity.ToRotation();
        }
        // does particle effects
        // copied from joost focussoulsbeam
        private void SpawnDust() {
            Vector2 dustPos = Projectile.Center;
            Dust dust = Main.dust[Dust.NewDust(dustPos, 0, 0, ModContent.DustType<ArchmageDust>(), 0, 0, 0)];
            dust.rotation = Projectile.velocity.ToRotation();
        }
    }
}