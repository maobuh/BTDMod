using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BTDMod.Projectiles;

namespace BTDMod.Items.GlueGunner
{
    class GlueGunner : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 0;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Generic;
            Item.shoot = ModContent.ProjectileType<Projectiles.GlueGunner.Glue>();
            Item.shootSpeed = 10;
            Item.useTime = 60;
            Item.useAnimation = 60;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Blue;
            Item.autoReuse = true;
            Item.width = 32;
            Item.height = 32;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("0-0-0 Glue Gunner");
            Tooltip.SetDefault("0 ranged damage\n0% critical strike chance\nVery slow speed\nNo knockback");
        }
    }
}