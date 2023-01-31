using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BTDMod.Projectiles;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System;
using BTDMod.Buffs;
using BTDMod.Projectiles.Tack;

namespace BTDMod.Items.TackShooter
{
    class Tack040 : ModItem
    {
        const int ABILITY_DURATION = 3 * 60;
        const int ABILITY_COOLDOWN = 90 * 60;
        int timer = ABILITY_DURATION;
        Vector2 abilityVelocity;
        double abilityAngle;
        const double DEGREE = Math.PI / 180;
        const int NUM_NAILS = 8;
        public override void SetDefaults()
        {
            Item.damage = 20;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Melee;
            Item.shoot = ModContent.ProjectileType<Sawblade>();
            Item.shootSpeed = 18;
            Item.useTime = 14;
            Item.useAnimation = 14;
            Item.knockBack = 3;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Green;
            Item.autoReuse = true;
            Item.width = 36;
            Item.height = 36;
            Item.noUseGraphic = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tack Shooter 0-4-0");
            Tooltip.SetDefault("The Meat Grinder");
        }
        public override void HoldItem(Player player)
        {
            if (BTDMod.MonkeyAbilityHotKey.JustPressed && !player.HasBuff<MaelstromCooldown>()) {
                player.AddBuff(ModContent.BuffType<MaelstromCooldown>(), ABILITY_COOLDOWN);
                timer = 0;
                abilityAngle = 0;
            }
            // while the ability is active, shoot 2 blades in opposite direction, which rotates in a circle
            if (timer < ABILITY_DURATION) {
                timer++;
                abilityVelocity = Item.shootSpeed * new Vector2((float)Math.Sin(abilityAngle), (float)Math.Cos(abilityAngle));
                abilityAngle += 0.2f;
                Projectile.NewProjectile(new EntitySource_ItemUse(player, Item), player.Center, abilityVelocity, ModContent.ProjectileType<Sawblade>(), Item.damage, 0, player.whoAmI);
                Projectile.NewProjectile(new EntitySource_ItemUse(player, Item), player.Center, -abilityVelocity, ModContent.ProjectileType<Sawblade>(), Item.damage, 0, player.whoAmI);
            }
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			const double spread = 360 / NUM_NAILS * DEGREE;
            float baseSpeed = velocity.Length();
            double baseAngle = Math.Atan2(velocity.X, velocity.Y);
			// makes saws shoot out in a circle
			for (int i = 0; i < NUM_NAILS; i++)
			{
				double angle = baseAngle + (i * spread);
				Vector2 newVelocity = new(baseSpeed * (float)Math.Sin(angle), baseSpeed * (float)Math.Cos(angle));
				Projectile.NewProjectile(source, position, newVelocity, type, damage, 0, player.whoAmI);
			}
			return false;
		}
        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(Item.type);
            recipe.AddIngredient(null, "Tack000", 1);
            recipe.AddIngredient(ItemID.SpikyBall, 20);
            recipe.Register();
        }
    }
}