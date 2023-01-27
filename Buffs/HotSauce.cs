using Terraria;
using Terraria.ModLoader;

namespace BTDMod.Buffs {
    class HotSauce: ModBuff
    {
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("On Fire!");
			Description.SetDefault("you are spewing hot fire all over the place right now, arent you, you little piggy");
			Main.debuff[Type] = false;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
		}
        public override void Update(Player player, ref int buffIndex)
        {
            player.directionTo(Main.mouseworld); // something like this
            Projectile.NewProjectile(); // fill this in once the nft fire is made
        }
    }
}