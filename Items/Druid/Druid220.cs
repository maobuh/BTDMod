using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BTDMod.Projectiles;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System;

// TODO: NEEDS LIGHTNING

namespace BTDMod.Items.Druid
{
    class Druid220 : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 1;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Generic;
            Item.shoot = ModContent.ProjectileType<Thorn>();
            Item.shootSpeed = 10;
            Item.useTime = 30;
            Item.useAnimation = 1;
            Item.useStyle = ItemUseStyleID.MowTheLawn;
            Item.rare = 2;
            Item.autoReuse = true;
            Item.width = 32;
            Item.height = 32;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Druid Monkey 2-2-0");
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			// 5 degrees but in radians for the spread of the projectiles
			const float spread = 0.261799f;
			// makes 8 projectile when shoot
			for (int i = 0; i < 8; i++)
			{
				// math for making random new angle
				float baseSpeed = velocity.Length();
				double baseAngle = Math.Atan2(velocity.X, velocity.Y);
				double randomAngle = baseAngle + ((Main.rand.NextFloat() - 0.5f) * spread);
				Vector2 newVelocity = new(baseSpeed * (float)Math.Sin(randomAngle), baseSpeed * (float)Math.Cos(randomAngle));
				// make new projectile
				Projectile.NewProjectile(source, position, newVelocity, type, 1, 0, player.whoAmI);
			}
			return false;
		}
    }
}