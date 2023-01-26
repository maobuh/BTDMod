using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BTDMod.Projectiles
{
    class SnowPiss : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 2;
            Projectile.penetrate = -1;
            Projectile.alpha = 0;
            Projectile.light = 0.5f;
            Projectile.friendly = true;
            Projectile.scale = 1f;
            Projectile.timeLeft = 300;
            Projectile.DamageType = DamageClass.Summon;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Ichor, 600);
            target.AddBuff(BuffID.Frostburn, 300);
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
        }
    }
}