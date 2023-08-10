using System.Collections.Generic;
using UnityEngine;

namespace WXB;

public class SymbolTextInit : MonoBehaviour
{
	private static Dictionary<string, Font> Fonts;

	private static Dictionary<string, Sprite> Sprites;

	private static Dictionary<string, Cartoon> Cartoons;

	[SerializeField]
	private Font[] fonts;

	[SerializeField]
	private Sprite[] sprites;

	[SerializeField]
	private Cartoon[] cartoons;

	private void init()
	{
		if (Fonts == null)
		{
			Fonts = new Dictionary<string, Font>();
		}
		else
		{
			Fonts.Clear();
		}
		if (fonts != null)
		{
			for (int i = 0; i < fonts.Length; i++)
			{
				Fonts.Add(((Object)fonts[i]).name, fonts[i]);
			}
		}
		if (Sprites == null)
		{
			Sprites = new Dictionary<string, Sprite>();
		}
		else
		{
			Sprites.Clear();
		}
		if (sprites != null)
		{
			for (int j = 0; j < sprites.Length; j++)
			{
				Sprites.Add(((Object)sprites[j]).name, sprites[j]);
			}
		}
		if (Cartoons == null)
		{
			Cartoons = new Dictionary<string, Cartoon>();
		}
		else
		{
			Cartoons.Clear();
		}
		if (cartoons != null)
		{
			for (int k = 0; k < cartoons.Length; k++)
			{
				Cartoons.Add(cartoons[k].name, cartoons[k]);
			}
		}
	}

	private static void Init()
	{
		Resources.Load<SymbolTextInit>("SymbolTextInit").init();
	}

	public static Font GetFont(string name)
	{
		if (Fonts == null)
		{
			Init();
		}
		if (Fonts.TryGetValue(name, out var value))
		{
			return value;
		}
		return null;
	}

	public static Sprite GetSprite(string name)
	{
		if (Sprites == null)
		{
			Init();
		}
		if (Sprites.TryGetValue(name, out var value))
		{
			return value;
		}
		return null;
	}

	public static Cartoon GetCartoon(string name)
	{
		if (Cartoons == null)
		{
			Init();
		}
		if (Cartoons.TryGetValue(name, out var value))
		{
			return value;
		}
		return null;
	}

	public static void GetCartoons(List<Cartoon> cartoons)
	{
		if (Cartoons == null)
		{
			Init();
		}
		foreach (KeyValuePair<string, Cartoon> cartoon in Cartoons)
		{
			cartoons.Add(cartoon.Value);
		}
	}
}
