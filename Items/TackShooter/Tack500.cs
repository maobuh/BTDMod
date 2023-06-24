using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BTDMod.Projectiles;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System;

namespace BTDMod.Items.TackShooter
{
    class Tack500 : ModItem
    {
        const int NUM_NAILS = 18;
        double baseAngle;
        int timer;
        public override void SetDefaults()
        {
            Item.damage = 40;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Melee;
            Item.shoot = ModContent.ProjectileType<FlameBreath>();
            Item.shootSpeed = 18;
            Item.useTime = 6;
            Item.useAnimation = 6;
            Item.knockBack = 3;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.LightRed;
            Item.autoReuse = true;
            Item.width = 36;
            Item.height = 36;
            Item.noUseGraphic = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tack Shooter 5-0-0");
            Tooltip.SetDefault("They tried putting mages inside the tack");
        }
        public override void HoldItem(Player player)
        {
            NPC closest = FindClosestNPC(1000, player);
            if (closest == null) return;
            if (timer++ % 60 == 0) {
                Projectile.NewProjectile(new EntitySource_ItemUse(player, Item), player.Center, Item.shootSpeed * new Vector2(0,1), ModContent.ProjectileType<Projectiles.Tack.Fireball>(), Item.damage * 10, 0, player.whoAmI);
            }
        }
        public NPC FindClosestNPC(float maxDetectDistance, Player player) {
			NPC closestNPC = null;

			// Using squared values in distance checks will let us skip square root calculations, drastically improving this method's speed.
			float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;

			// Loop through all NPCs(max always 200)
			for (int k = 0; k < Main.maxNPCs; k++) {
				NPC target = Main.npc[k];
				// Check if NPC able to be targeted. It means that NPC is
				// 1. active (alive)
				// 2. chaseable (e.g. not a cultist archer)
				// 3. max life bigger than 5 (e.g. not a critter)
				// 4. can take damage (e.g. moonlord core after all it's parts are downed)
				// 5. hostile (!friendly)
				// 6. not immortal (e.g. not a target dummy)
				if (target.CanBeChasedBy()) {
					// The DistanceSquared function returns a squared distance between 2 points, skipping relatively expensive square root calculations
					float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, player.Center);
					// Check if it is within the radius
					if (sqrDistanceToTarget < sqrMaxDetectDistance) {
                        // check that the target has not already been hit
                        sqrMaxDetectDistance = sqrDistanceToTarget;
                        closestNPC = target;
					}
				}
			}
            return closestNPC;
		}
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			const float spread = 2 * (float)Math.PI / NUM_NAILS;
            float baseSpeed = velocity.Length();
            baseAngle += 0.1f;
            double angle = baseAngle;
			// makes flames shoot out in a circle
			for (int i = 0; i < NUM_NAILS; i++)
			{
				Vector2 newVelocity = new(baseSpeed * (float)Math.Sin(angle), baseSpeed * (float)Math.Cos(angle));
				Projectile proj = Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, 0, player.whoAmI);
                proj.scale = 5f;
                proj.tileCollide = false;
                angle += spread;
			}
			return false;
		}
        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(Item.type);
            recipe.AddIngredient(null, "Tack400", 1);
            recipe.AddIngredient(ItemID.SoulofMight, 3);
            recipe.Register();
        }
    }
}