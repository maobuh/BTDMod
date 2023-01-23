using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

// copied from example mod sparkle.cs
namespace BTDMod.Dusts
{
	public class ArchmageDust : ModDust
	{
		public override void OnSpawn(Dust dust) {
			int dustRand = Main.rand.Next(0, 3) * 20; 				// chooses which dust to spawn
			dust.frame = new Rectangle(0, dustRand, 20, 20);	// this dust is unusually large
			dust.velocity *= 0.4f; 									// Multiply the dust's start velocity by 0.4, slowing it down
			dust.noGravity = true; 									// Makes the dust have no gravity.
			dust.noLight = true; 									// Makes the dust emit no light.
			dust.scale *= 1.8f; 									// Multiplies the dust's initial scale
		}

		public override bool Update(Dust dust) { // Calls every frame the dust is active
			dust.scale *= 0.98f;

			float light = 0.35f * dust.scale;

			Lighting.AddLight(dust.position, light, light, light);

			if (dust.scale < 0.5f) {
				dust.active = false;
			}

			return false; // Return false to prevent vanilla behavior.
		}
	}
}