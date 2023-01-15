using Terraria;

namespace BTDMod.Projectiles
{
    class LightningOfWrath : Thorn
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lightning of Wrath");
			Main.projFrames[Projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.width = 21;
            Projectile.height = 8;
        }
    }
}