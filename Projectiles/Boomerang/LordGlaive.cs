namespace BTDMod.Projectiles.Boomerang
{
    class LordGlaive : Glaive
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.penetrate = 50;
            maxDetectRadius = 480f;
        }
    }
}