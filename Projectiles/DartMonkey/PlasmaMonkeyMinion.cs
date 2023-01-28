using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace BTDMod.Projectiles.DartMonkey
{
    class PlasmaMonkeyMinion : SuperMonkeyMinion
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Plasma Monkey Fan Club Member");
            Main.projFrames[Projectile.type] = 2;
            Main.projPet[Projectile.type] = true;
        }
        public override void Shoot(Vector2 targetCenter) {
			Projectile.frame = (Projectile.frame + 1) % 2;
			Projectile.rotation = (float)Math.Atan2(Projectile.DirectionTo(targetCenter).Y, Projectile.DirectionTo(targetCenter).X);
			Projectile.NewProjectile(Terraria.Entity.InheritSource(Projectile), Projectile.Center, Projectile.DirectionTo(targetCenter) * 20, ModContent.ProjectileType<DartPlasma>(), Projectile.damage, 0, Projectile.owner);
        }
    }
}