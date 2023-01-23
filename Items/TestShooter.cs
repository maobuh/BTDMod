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
            Item.shoot = ModContent.ProjectileType<Thorn>();
            Item.shootSpeed = 10;
            Item.useTime = 5;
            Item.useAnimation = 5;
            Item.useStyle = ItemUseStyleID.MowTheLawn;
            Item.rare = ItemRarityID.Green;
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