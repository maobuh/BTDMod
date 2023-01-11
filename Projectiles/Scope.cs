using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using BTDMod.Items.Sniper;
using BTDMod.Buffs;
using System;

namespace BTDMod.Projectiles
{
    class Scope : ModProjectile
    {
        readonly float ROTATE_SPEED = (float)(0.05 / Math.PI);
        const int UNIQUE_FRAMES = 5;
        const int FRAMES = (UNIQUE_FRAMES * 2) - 2;
        float UseTime {
            get => Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }
        const int PITY_FRAMES = 5;
        int currentFrame;
        public override void SetDefaults()
        {
            Projectile.width = 46;
            Projectile.height = 46;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.alpha = 0;
            Projectile.light = 2f;
            Projectile.friendly = true;
            Projectile.scale = 1.2f;
            Projectile.extraUpdates = 1;
            Projectile.frame = 5;
            Projectile.netUpdate = false;
        }
        public override bool? CanDamage()
        {
            return false;
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = UNIQUE_FRAMES;
        }
        public override void AI()
        {
            if (Projectile.owner != Main.myPlayer) return;
            Projectile.timeLeft = 2;
            Player player = Main.player[Projectile.owner];
            float frameTime = UseTime * (Projectile.extraUpdates + 1) / FRAMES;
            // deals with animation frames
            if (++Projectile.frameCounter >= frameTime)
            {
                Projectile.frameCounter = 0;
                // technically 8 frames total instead of 5, since the scope is supposed to enlarge then minimise in a cycle
                currentFrame = ++currentFrame % FRAMES;
                Projectile.frame = currentFrame >= UNIQUE_FRAMES? FRAMES - currentFrame: currentFrame;
            }
            if (Projectile.frame == 0 && Projectile.frameCounter == 0) {
                player.AddBuff(ModContent.BuffType<Marked>(), (int) frameTime + PITY_FRAMES);
                SoundEngine.PlaySound(SoundID.MaxMana);
            }
            // remove the scope if the player is no longer holding Sniper 500
            if (player.HeldItem.type != ModContent.ItemType<Sniper500>()) {
                Projectile.Kill();
            }
            // scope should be positioned on players mouse
            Projectile.Center = Main.MouseWorld;
            // rotate the scope slowly
            Projectile.rotation += ROTATE_SPEED;
        }
    }
}