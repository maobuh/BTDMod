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
            Item.noMelee = true;
            Item.DamageType = DamageClass.Generic;
            Item.shoot = ModContent.ProjectileType<UltraJuggernaut>();
            Item.shootSpeed = 10;
            Item.useTime = 60;
            Item.useAnimation = 60;
            Item.useStyle = ItemUseStyleID.MowTheLawn;
            Item.rare = 2;
            Item.autoReuse = true;
            Item.width = 32;
            Item.height = 32;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("shoot porjectile (for testing)");
        }
    }
}