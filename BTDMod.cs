using Terraria.ModLoader;

namespace BTDMod
{
	public class BTDMod : Mod
	{
		internal static ModKeybind MonkeyAbilityHotKey;
		public override void Load()
		{
			MonkeyAbilityHotKey = KeybindLoader.RegisterKeybind(ModContent.GetInstance<BTDMod>(), "MonkeyAbilityHotKey", "Z");
		}
	}
}