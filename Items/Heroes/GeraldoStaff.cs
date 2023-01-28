using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BTDMod.Items.Heroes
{
    class GeraldoStaff : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 1;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Magic;
            Item.shoot = ModContent.ProjectileType<Projectiles.Heroes.GeraldoLightning>(); // fuck im gonna have to make lightning still
            Item.shootSpeed = 10;
            Item.useTime = 5;
            Item.useAnimation = 5;
            Item.useStyle = ItemUseStyleID.MowTheLawn;
            Item.rare = ItemRarityID.Expert;
            Item.autoReuse = true;
            Item.width = 32;
            Item.height = 32;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Geraldo's Staff");
            Tooltip.SetDefault("geraldo... he was my best friend..." + "\n" +
            "you. why did you murder him!?!??!??! you bastard!!!" + "\n" + 
            "you fucking bastard");
        }
    }
}