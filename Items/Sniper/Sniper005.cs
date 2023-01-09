using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace BTDMod.Items.Sniper
{
    class Sniper005: ModItem
    {
        float distance = 1000;
        const int BASE_DAMAGE = 80;
        const float MULTIPLIER = 1.2f;
        public override void SetDefaults()
        {
            Item.CloneDefaults(ModContent.ItemType<Sniper000>());
            Item.damage = BASE_DAMAGE;
            Item.rare = ItemRarityID.LightRed;
            Item.useTime = 6;
            Item.knockBack = 2;
            Item.useAnimation = 6;
            Item.shoot = ProjectileID.Bullet;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sniper Monkey 0-0-5");
            Tooltip.SetDefault("Shoots so goddamn fast\nDeals more damage the closer you are to the enemy");
        }
        public override void HoldItem(Player player)
        {
            NPC closest = FindClosestNPC(1000, player);
            if (closest == null) {
                Item.damage = BASE_DAMAGE;
                return;
            }

            // Item damage increases the closer the player is to an enemy
            distance = Vector2.Distance(player.Center, closest.Center);
            if (distance > 800) {
                Item.damage = BASE_DAMAGE;
            } else if (distance > 400) {
                Item.damage = (int) (BASE_DAMAGE * MULTIPLIER);
            } else if (distance > 200) {
                Item.damage = (int) (BASE_DAMAGE * Math.Pow(MULTIPLIER, 2));
            } else {
                Item.damage = (int) (BASE_DAMAGE * Math.Pow(MULTIPLIER, 3));
            }
            Item.damage = (int) player.GetDamage<RangedDamageClass>().ApplyTo(Item.damage);
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
                        sqrMaxDetectDistance = sqrDistanceToTarget;
                        closestNPC = target;
					}
				}
			}
            return closestNPC;
		}
        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(Item.type);
            recipe.AddIngredient(null, "Sniper004", 1);
            recipe.AddIngredient(ItemID.BeetleHusk, 5);
            recipe.Register();
        }
    }
}