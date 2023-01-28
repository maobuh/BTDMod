using Terraria;

namespace BTDMod.Projectiles
{
    class WrathThorn : Thorn
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.penetrate = 2;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[Projectile.owner];
            if (player.GetModPlayer<BTDPlayer>().wrathAttackSpeedBonus < 100) {
                player.GetModPlayer<BTDPlayer>().wrathAttackSpeedBonus += 4;
            }
            player.GetModPlayer<BTDPlayer>().attackSpeedResetCheck = !player.GetModPlayer<BTDPlayer>().attackSpeedResetCheck;
            base.OnHitNPC(target, damage, knockback, crit);
        }
    }
}