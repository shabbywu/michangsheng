using System;
using System.Collections.Generic;
using UnityEngine;

namespace WXB
{
	// Token: 0x020006B7 RID: 1719
	public class SymbolTextInit : MonoBehaviour
	{
		// Token: 0x06003651 RID: 13905 RVA: 0x00173E44 File Offset: 0x00172044
		private void init()
		{
			if (SymbolTextInit.Fonts == null)
			{
				SymbolTextInit.Fonts = new Dictionary<string, Font>();
			}
			else
			{
				SymbolTextInit.Fonts.Clear();
			}
			if (this.fonts != null)
			{
				for (int i = 0; i < this.fonts.Length; i++)
				{
					SymbolTextInit.Fonts.Add(this.fonts[i].name, this.fonts[i]);
				}
			}
			if (SymbolTextInit.Sprites == null)
			{
				SymbolTextInit.Sprites = new Dictionary<string, Sprite>();
			}
			else
			{
				SymbolTextInit.Sprites.Clear();
			}
			if (this.sprites != null)
			{
				for (int j = 0; j < this.sprites.Length; j++)
				{
					SymbolTextInit.Sprites.Add(this.sprites[j].name, this.sprites[j]);
				}
			}
			if (SymbolTextInit.Cartoons == null)
			{
				SymbolTextInit.Cartoons = new Dictionary<string, Cartoon>();
			}
			else
			{
				SymbolTextInit.Cartoons.Clear();
			}
			if (this.cartoons != null)
			{
				for (int k = 0; k < this.cartoons.Length; k++)
				{
					SymbolTextInit.Cartoons.Add(this.cartoons[k].name, this.cartoons[k]);
				}
			}
		}

		// Token: 0x06003652 RID: 13906 RVA: 0x00173F56 File Offset: 0x00172156
		private static void Init()
		{
			Resources.Load<SymbolTextInit>("SymbolTextInit").init();
		}

		// Token: 0x06003653 RID: 13907 RVA: 0x00173F68 File Offset: 0x00172168
		public static Font GetFont(string name)
		{
			if (SymbolTextInit.Fonts == null)
			{
				SymbolTextInit.Init();
			}
			Font result;
			if (SymbolTextInit.Fonts.TryGetValue(name, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06003654 RID: 13908 RVA: 0x00173F94 File Offset: 0x00172194
		public static Sprite GetSprite(string name)
		{
			if (SymbolTextInit.Sprites == null)
			{
				SymbolTextInit.Init();
			}
			Sprite result;
			if (SymbolTextInit.Sprites.TryGetValue(name, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06003655 RID: 13909 RVA: 0x00173FC0 File Offset: 0x001721C0
		public static Cartoon GetCartoon(string name)
		{
			if (SymbolTextInit.Cartoons == null)
			{
				SymbolTextInit.Init();
			}
			Cartoon result;
			if (SymbolTextInit.Cartoons.TryGetValue(name, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06003656 RID: 13910 RVA: 0x00173FEC File Offset: 0x001721EC
		public static void GetCartoons(List<Cartoon> cartoons)
		{
			if (SymbolTextInit.Cartoons == null)
			{
				SymbolTextInit.Init();
			}
			foreach (KeyValuePair<string, Cartoon> keyValuePair in SymbolTextInit.Cartoons)
			{
				cartoons.Add(keyValuePair.Value);
			}
		}

		// Token: 0x04002F6F RID: 12143
		private static Dictionary<string, Font> Fonts;

		// Token: 0x04002F70 RID: 12144
		private static Dictionary<string, Sprite> Sprites;

		// Token: 0x04002F71 RID: 12145
		private static Dictionary<string, Cartoon> Cartoons;

		// Token: 0x04002F72 RID: 12146
		[SerializeField]
		private Font[] fonts;

		// Token: 0x04002F73 RID: 12147
		[SerializeField]
		private Sprite[] sprites;

		// Token: 0x04002F74 RID: 12148
		[SerializeField]
		private Cartoon[] cartoons;
	}
}
