using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;
using System;

namespace BTDMod.Projectiles
{
    class Vines : ModProjectile
    {
        // TODO make the vines have different sprites the closer to the player they are
        // three stages just like in balon game
        // Projectile.ai[0] to Projectile.ai[1] will the x values of the position where the vines need to be drawn to and from
        const float vineSpacing = 8f;
        public override void SetDefaults()
        {
            Projectile.damage = 1;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.friendly = true;
            Projectile.height = 34;
            Projectile.width = 34;
            Projectile.penetrate = -1;
            Projectile.extraUpdates = 2;
            Projectile.localNPCHitCooldown = 0;
            Projectile.timeLeft = 100;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vines");
        }
        public override void PostDraw(Color lightColor)
        {
            if (Array.Exists(Main.projectile, element => element.position == Projectile.position) && !Array.Find(Main.projectile, element => element.position == Projectile.position).Equals(Projectile)) {
                Projectile.Kill();
                Main.NewText("buh");
            }
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
            base.AI();
        }
        // public override bool PreDraw(ref Color lightColor)
        // {
        //     Vector2 start = new(Projectile.ai[0], Projectile.position.Y);
        //     for (float i = vineSpacing; i <= Projectile.ai[1]; i += vineSpacing) {
        //         Main.EntitySpriteDraw((Texture2D) ModContent.Request<Texture2D>("BTDMod/Projectiles/Vines", (AssetRequestMode) 2), start - Main.screenPosition, new Rectangle(0, 0, 32, 16), Color.White, Projectile.Center.ToRotation(), new Vector2(32 / 2, 16 / 8), 1, 0, 0);
        //         start += Projectile.Center * vineSpacing;
        //     }
        //     return true;
        // }
    }
}