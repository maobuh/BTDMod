using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using System;
using System.Linq;
using BTDMod.Items.Boomerang;
using Microsoft.Xna.Framework.Graphics;

namespace BTDMod.Projectiles.Boomerang
{
    class GlaiveLordRing : ModProjectile
    {
        const float DEFAULT_ANGLE_CHANGE = (float) (3 / (180 / Math.PI));
        const float DEFAULT_FLY_RADIUS = 96f;
        const float MAX_ANGLE_CHANGE = (float) (15 / (180 / Math.PI));
        float flyRadius = DEFAULT_FLY_RADIUS;
        float angleChange = DEFAULT_ANGLE_CHANGE;
        protected float maxDetectRadius = 320f;
        public override void SetDefaults()
        {
            Projectile.width = 42;
            Projectile.height = 40;
            Projectile.penetrate = 100;
            Projectile.alpha = 0;
            Projectile.light = 0.5f;
            Projectile.friendly = true;
            Projectile.scale = 1f;
            Projectile.timeLeft = 2;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.netUpdate = true;
            Projectile.tileCollide = false;
        }
        public NPC FindClosestNPC(float maxDetectDistance) {
			NPC closestNPC = null;
            Player player = Main.player[Projectile.owner];
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
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            // makes the boomerang spin
            Projectile.rotation += 0.2f;
            Projectile.penetrate = 100;
            if (player.HeldItem.type == ModContent.ItemType<Boomerang500>()) {
                Projectile.Center = player.Center;
                Projectile.timeLeft = 2;
            }
            NPC closestNPC = FindClosestNPC(maxDetectRadius);
            if (closestNPC == null) {
                // reset rotation speed and radius slowly if there are no enemies nearby
                if (angleChange > DEFAULT_ANGLE_CHANGE) {
                    angleChange -= (float) (0.5 / (180 / Math.PI));
                }
                if (flyRadius > DEFAULT_FLY_RADIUS) {
                    flyRadius -= 4;
                }
                return;
            }
            // increase flyRadius to the enemy's distance and the rotation speed if an enemy is nearby
            float distanceToNPC = (closestNPC.Center - player.Center).Length();
            flyRadius += flyRadius < distanceToNPC? 4: -4;
            if (flyRadius < DEFAULT_FLY_RADIUS) {
                flyRadius = DEFAULT_FLY_RADIUS;
            }
            if (angleChange < MAX_ANGLE_CHANGE) {
                angleChange += (float) (0.5 / (180 / Math.PI));
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Player player = Main.player[Projectile.owner];
            float dirAngle = Projectile.velocity.ToRotation() + angleChange;
            Projectile.velocity = new((float) Math.Cos(dirAngle), (float) Math.Sin(dirAngle));
            for (int i = 0; i < 3; i++) {
                Vector2 position = player.Center + (flyRadius * new Vector2((float) Math.Cos(dirAngle), (float) Math.Sin(dirAngle)));
                Main.EntitySpriteDraw((Texture2D) ModContent.Request<Texture2D>("BTDMod/Projectiles/Boomerang/GlaiveLordRing"), position - Main.screenPosition,
                new Rectangle(0, 0, Projectile.width, Projectile.height), Color.White, 0, new Vector2(Projectile.width / 2, Projectile.height / 2), 1f, 0, 0);
                // rotating boomerangs are just visual, actual hitbox comes from this
                Projectile.NewProjectile(Projectile.InheritSource(Projectile), position, Vector2.Zero, ModContent.ProjectileType<GlaiveLordHitbox>(), Projectile.damage, 0, player.whoAmI);
                dirAngle += (float)(2 * Math.PI / 3);
            }
            return false;
        }
    }
}