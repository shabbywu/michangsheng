using System;
using System.Collections.Generic;
using UnityEngine;

namespace WXB
{
	// Token: 0x020009DE RID: 2526
	public class SymbolTextInit : MonoBehaviour
	{
		// Token: 0x06004059 RID: 16473 RVA: 0x001BC6BC File Offset: 0x001BA8BC
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

		// Token: 0x0600405A RID: 16474 RVA: 0x0002E40C File Offset: 0x0002C60C
		private static void Init()
		{
			Resources.Load<SymbolTextInit>("SymbolTextInit").init();
		}

		// Token: 0x0600405B RID: 16475 RVA: 0x001BC7D0 File Offset: 0x001BA9D0
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

		// Token: 0x0600405C RID: 16476 RVA: 0x001BC7FC File Offset: 0x001BA9FC
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

		// Token: 0x0600405D RID: 16477 RVA: 0x001BC828 File Offset: 0x001BAA28
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

		// Token: 0x0600405E RID: 16478 RVA: 0x001BC854 File Offset: 0x001BAA54
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

		// Token: 0x04003961 RID: 14689
		private static Dictionary<string, Font> Fonts;

		// Token: 0x04003962 RID: 14690
		private static Dictionary<string, Sprite> Sprites;

		// Token: 0x04003963 RID: 14691
		private static Dictionary<string, Cartoon> Cartoons;

		// Token: 0x04003964 RID: 14692
		[SerializeField]
		private Font[] fonts;

		// Token: 0x04003965 RID: 14693
		[SerializeField]
		private Sprite[] sprites;

		// Token: 0x04003966 RID: 14694
		[SerializeField]
		private Cartoon[] cartoons;
	}
}
