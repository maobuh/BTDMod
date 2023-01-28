using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System;
using System.Linq;
using BTDMod.Buffs;

namespace BTDMod.Projectiles.Boomerang
{
    class BionicBoomerang : ModProjectile {
        protected float RETURN_DELAY = 45f;
        float originalSpeed;
        float Timer {
            get => Projectile.localAI[0];
            set => Projectile.localAI[0] = value;
        }
        public override void SetDefaults()
        {
            Projectile.width = 42;
            Projectile.height = 40;
            Projectile.penetrate = 8;
            Projectile.alpha = 0;
            Projectile.light = 0.5f;
            Projectile.friendly = true;
            Projectile.scale = 1f;
            Projectile.timeLeft = 360;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.netUpdate = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 30;
        }
        public override void AI()
        {
            // below AI attempts to mimick the vanilla boomerang ai
            Player player = Main.player[Projectile.owner];
            if (Timer == 0) {
                // initial speed of the boomerang
                originalSpeed = Projectile.velocity.Length();
            }
            // increases damage and attack speed if the turbo charged buff is active
            // makes the boomerang spin
            Projectile.rotation += 0.2f;
            // after some delay, the boomerang will start returning
            if (Timer > RETURN_DELAY) {
                // direction from the boomerang to the player
                Vector2 playerDir = player.Center - Projectile.Center;
                // if the boomerang is within 1 block of the player, despawn it
                if (playerDir.Length() < 16) {
                    Projectile.Kill();
                }
                playerDir.Normalize();
                playerDir *= 4;
                // slowly decrease the boomerang speed until it reaches the same velocity as its original, but in the opposite direction
                if (Projectile.velocity.Length() <= originalSpeed) {
                    Projectile.velocity += playerDir;
                } else {
                    // if the velocity is at a maximum, just home into the player
                    double angleToPlayer = Math.Atan2(playerDir.X, playerDir.Y);
                    Projectile.velocity = new(originalSpeed * (float)Math.Sin(angleToPlayer), originalSpeed * (float)Math.Cos(angleToPlayer));
                }
                // vanilla boomerangs have a slight curve when they return, so the stuff below gives the boomerang some extra velocity 
                // to create that curve
                if (Projectile.velocity.Length() < 128) {
                    // boomerang gets extra vertical velocity if its further horizontally than vertically away from the player
                    if (Math.Abs(playerDir.X) < Math.Abs(playerDir.Y)) {
                        Projectile.velocity += new Vector2(playerDir.X, 0);
                    } else {
                        // otherwise it gets extra horizontal velocity
                        Projectile.velocity += new Vector2(0, playerDir.Y);
                    }
                }
            }
            Timer++;
        }
    }
}