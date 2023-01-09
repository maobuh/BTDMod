using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using System;

namespace BTDMod.Projectiles
{
    class Pineapple : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Generic;
            Projectile.friendly = true;
            Projectile.height = 32;
            Projectile.width = 32;
            Projectile.penetrate = 32767;
            Projectile.timeLeft = 180;
            Projectile.tileCollide = false;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Exploding Pineapple");
        }
        public override void Kill(int timeLeft)
        {
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, Vector2.Zero, ModContent.ProjectileType<PineappleExplosion>(), Projectile.damage, 0, Main.player[Projectile.owner].whoAmI);
            base.Kill(timeLeft);
        }
        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            hitbox = new(0, 0, 0, 0);
        }
    }
}