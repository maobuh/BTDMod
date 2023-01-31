using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using System;

using Terraria;
using Terraria.ModLoader;

namespace BTDMod.Projectiles.GlueGunner
{
    class Glue : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.damage = 0;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.friendly = true;
            Projectile.height = 34;
            Projectile.width = 34;
            Projectile.penetrate = 1;
            Projectile.localNPCHitCooldown = 30;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Glue");
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
            base.AI();
        }
    }
}