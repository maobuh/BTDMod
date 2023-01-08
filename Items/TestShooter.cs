using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BTDMod.Projectiles;

namespace BTDMod.Items
{
    class TestShooter : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 1;
            Item.DamageType = DamageClass.Generic;
            Item.shoot = ModContent.ProjectileType<UltraJuggernaut>();
            Item.shootSpeed = 1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("shoot porjectile (for testing)");
        }
    }
}