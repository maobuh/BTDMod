using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using System;
using BTDMod.Items.Druid;
using BTDMod.Buffs;
using Microsoft.Xna.Framework.Graphics;

namespace BTDMod.Projectiles
{
    class Vines : ModProjectile
    {
        // Projectile.ai[0] is where the laser should start
        // Projectile.ai[1] is how long the laser should be in tiles
        const int numThorns = 5;
        const int shootSpeed = 10;
        bool explode;
        public override void SetDefaults()
        {
            Projectile.damage = 1;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.friendly = true;
            Projectile.height = 16;
            Projectile.width = 32;
            Projectile.penetrate = -1;
            Projectile.localNPCHitCooldown = 30;
            Projectile.timeLeft = 10;
            Projectile.light = 10f;
            Projectile.netImportant = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vines");
			Main.projFrames[Projectile.type] = 3;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            DrawLaser((Texture2D)ModContent.Request<Texture2D>("BTDMod/Projectiles/Vines"), new Vector2(Projectile.ai[0], Projectile.position.Y), new Vector2(1, 0), 16);
            return base.PreDraw(ref lightColor);
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            // vine boom
            if (BTDMod.MonkeyAbilityHotKey.JustPressed && player.HeldItem.type == ModContent.ItemType<Druid052>() && !player.HasBuff(ModContent.BuffType<VineExplosionCooldown>())) {
                explode = true;
                // vines closer to the player explode earlier than the ones further away
                // no idea by how much tho just felt right
                Projectile.timeLeft = (int)(Projectile.Center - player.Center).Length() / 40 * 4;
            }
            // kill projectile if its too far away from the player (840 tiles)
            if ((player.Center - Projectile.Center).Length() > 840) {
                Projectile.Kill();
            }
            Projectile.netUpdate = true;
            base.AI();
        }
        public override void Kill(int timeLeft)
        {
            Player player = Main.player[Projectile.owner];
            if (explode) {
                // once per vine
                double angle = Math.PI * 2 / numThorns;
                for (int i = 0; i < numThorns; i++) {
                    Vector2 velocity = new((float) Math.Cos(angle), (float) Math.Sin(angle));
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, velocity * shootSpeed, ModContent.ProjectileType<VineExplosion>(), Projectile.damage * 10, 0, Projectile.owner);
                    angle += Math.PI * 2 / numThorns;
                    player.GetModPlayer<BTDPlayer>().vineRadius = 4;
                }
                if (Projectile.ai[1] > 2) {
                    player.AddBuff(ModContent.BuffType<VineExplosionCooldown>(), 360);
                }
                Projectile.netUpdate = true;
            }
        }
        // texture - texture of the laser
        // start - where the laser starts
        // unit - unit vector in the direction of where the laser is going
        // step - how many pixels are between each repeat of the laser body
        // rotation - rotation
        // scale - scale
        public void DrawLaser(Texture2D texture, Vector2 start, Vector2 unit,
								float step, float rotation = 0f, float scale = 1f)
		{
			float r = unit.ToRotation() + rotation;
			#region Draw laser body
			for (float i = step; i <= Projectile.ai[1]; i += step)
			{
				Main.EntitySpriteDraw(texture, start - Main.screenPosition,
					new Rectangle(0, 0, 32, 16), Color.White, r,
					new Vector2(16 / 2, 26 / 2), scale, 0, 0);
				start += step * unit;
			}
			#endregion
        }
    }
}