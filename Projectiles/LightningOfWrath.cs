using Terraria;

namespace BTDMod.Projectiles
{
    class LightningOfWrath : WrathThorn
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lightning of Wrath");
			Main.projFrames[Projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.penetrate = 4;
            Projectile.width = 21;
            Projectile.height = 8;
        }
        public override void AI()
        {
            if (Projectile.ai[0] > 3) {
                Projectile.ai[0] = 3;
            }
            Projectile.frame = (int) Projectile.ai[0];
            base.AI();
        }
    }
}