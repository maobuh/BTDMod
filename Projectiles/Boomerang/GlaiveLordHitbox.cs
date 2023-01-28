using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using System;
using System.Linq;
using BTDMod.Items.Boomerang;
using Microsoft.Xna.Framework.Graphics;

namespace BTDMod.Projectiles.Boomerang
{
    class GlaiveLordHitbox : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 42;
            Projectile.height = 40;
            Projectile.penetrate = 100;
            Projectile.friendly = true;
            Projectile.scale = 1f;
            Projectile.timeLeft = 2;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.hide = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20;
        }
    }
}