namespace BTDMod.Projectiles.Boomerang
{
    class MOARGlaive : Glaive
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.penetrate = 30;
            maxDetectRadius = 480f;
        }
    }
}