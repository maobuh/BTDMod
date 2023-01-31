using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Terraria.DataStructures;

namespace BTDMod.Projectiles.DartMonkey
{
    class CrossbowBolt : ModProjectile
    {
        Vector2 dist;
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Melee;
            Projectile.friendly = true;
            Projectile.height = 28;
            Projectile.width = 64;
            Projectile.penetrate = 1;
            Projectile.localNPCHitCooldown = 0;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.timeLeft = 120;
            Projectile.scale = 0.4f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crossbow Bolt");
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Projectile.ai[1] != 1) {
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<CrossbowBolt>(), 0, 0, Projectile.owner, Array.IndexOf(Main.npc, target), 1);
            }
        }
        public override void OnSpawn(IEntitySource source)
        {
            if (Projectile.damage == 0) {
                dist = Main.npc[(int)Projectile.ai[0]].Center - Projectile.Center;
                Projectile.timeLeft = 300;
            }
            base.OnSpawn(source);
        }
        public override void AI()
        {
            Projectile.netUpdate = true;
            // follow the npc around 
            if (Projectile.damage == 0) {
                Projectile.Center = Main.npc[(int)Projectile.ai[0]].Center - dist;
                // explode when hotkey pressed
                if (BTDMod.MonkeyAbilityHotKey.JustPressed) {
                    Projectile.damage = 150 * (int)Main.player[Projectile.owner].GetDamage<MeleeDamageClass>().Multiplicative;
                }
                Projectile.rotation = Projectile.Center.AngleTo(Main.npc[(int)Projectile.ai[0]].Center);
            } else {
                Projectile.rotation = Projectile.velocity.ToRotation();
            }
            base.AI();
        }
    }
}