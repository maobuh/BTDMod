using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace BTDMod.Buffs {
    class NdujaFrittaTanto: ModBuff
    {
        const int baseSpeed = 20;
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("On Fire!");
			Description.SetDefault("you are spewing hot fire all over the place right now arent you, you little piggy");
			Main.debuff[Type] = false;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
		}
        public override void Update(Player player, ref int buffIndex)
        {
            Vector2 velocity = player.Center.DirectionTo(Main.MouseWorld); // something like this
            // 5 degrees but in radians for the spread of the projectiles
			const float spread = 0.261799f;
			// makes 5 projectile when shoot
            // math for making random new angle
            double baseAngle = Math.Atan2(velocity.X, velocity.Y);
            double randomAngle = baseAngle + ((Main.rand.NextFloat() - 0.5f) * spread);
            Vector2 newVelocity = new(baseSpeed * (float)Math.Sin(randomAngle), baseSpeed * (float)Math.Cos(randomAngle));
            Projectile.NewProjectile(player.GetSource_Buff(buffIndex), player.Center, newVelocity, ModContent.ProjectileType<Projectiles.NFTFire>(), 50, 10, player.whoAmI); // fill this in once the nft fire is made
        }
    }
}