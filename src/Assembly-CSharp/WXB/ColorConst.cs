using System.Collections.Generic;
using UnityEngine;

namespace WXB;

public static class ColorConst
{
	public static Color aqua = new Color(0f, 1f, 1f, 1f);

	public static Color brown = new Color(0.64705884f, 14f / 85f, 14f / 85f, 1f);

	public static Color darkblue = new Color(0f, 0f, 32f / 51f, 1f);

	public static Color fuchsia = new Color(1f, 0f, 1f, 1f);

	public static Color lightblue = new Color(0.6784314f, 72f / 85f, 46f / 51f, 1f);

	public static Color lime = new Color(0f, 1f, 0f, 1f);

	public static Color maroon = new Color(0.5019608f, 0f, 0f, 1f);

	public static Color navy = new Color(0f, 0f, 0.5019608f, 1f);

	public static Color olive = new Color(0.5019608f, 0.5019608f, 0f, 1f);

	public static Color orange = new Color(1f, 0.64705884f, 0f, 1f);

	public static Color purple = new Color(0.5019608f, 0f, 0.5019608f, 1f);

	public static Color silver = new Color(64f / 85f, 64f / 85f, 64f / 85f, 1f);

	public static Color teal = new Color(0f, 0.5019608f, 0.5019608f, 1f);

	public static Dictionary<string, Color> NameToColors = new Dictionary<string, Color>();

	public static void Set(string name, Color c)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		NameToColors[name] = c;
	}

	public static bool Get(string name, out Color color)
	{
		//IL_0463: Unknown result type (might be due to invalid IL or missing references)
		//IL_0468: Unknown result type (might be due to invalid IL or missing references)
		//IL_0497: Unknown result type (might be due to invalid IL or missing references)
		//IL_049c: Unknown result type (might be due to invalid IL or missing references)
		//IL_042f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0434: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0504: Unknown result type (might be due to invalid IL or missing references)
		//IL_0581: Unknown result type (might be due to invalid IL or missing references)
		//IL_0586: Unknown result type (might be due to invalid IL or missing references)
		//IL_058e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0593: Unknown result type (might be due to invalid IL or missing references)
		//IL_059b: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0567: Unknown result type (might be due to invalid IL or missing references)
		//IL_056c: Unknown result type (might be due to invalid IL or missing references)
		//IL_04be: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_047d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0482: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_043c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0441: Unknown result type (might be due to invalid IL or missing references)
		//IL_0470: Unknown result type (might be due to invalid IL or missing references)
		//IL_0475: Unknown result type (might be due to invalid IL or missing references)
		//IL_0456: Unknown result type (might be due to invalid IL or missing references)
		//IL_045b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0533: Unknown result type (might be due to invalid IL or missing references)
		//IL_0538: Unknown result type (might be due to invalid IL or missing references)
		//IL_054d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0552: Unknown result type (might be due to invalid IL or missing references)
		//IL_0574: Unknown result type (might be due to invalid IL or missing references)
		//IL_0579: Unknown result type (might be due to invalid IL or missing references)
		//IL_055a: Unknown result type (might be due to invalid IL or missing references)
		//IL_055f: Unknown result type (might be due to invalid IL or missing references)
		//IL_050c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0511: Unknown result type (might be due to invalid IL or missing references)
		//IL_048a: Unknown result type (might be due to invalid IL or missing references)
		//IL_048f: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0519: Unknown result type (might be due to invalid IL or missing references)
		//IL_051e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0526: Unknown result type (might be due to invalid IL or missing references)
		//IL_052b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0449: Unknown result type (might be due to invalid IL or missing references)
		//IL_044e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0540: Unknown result type (might be due to invalid IL or missing references)
		//IL_0545: Unknown result type (might be due to invalid IL or missing references)
		if (NameToColors.TryGetValue(name, out color))
		{
			return true;
		}
		switch (name)
		{
		case "aqua":
			color = aqua;
			return true;
		case "brown":
			color = brown;
			return true;
		case "darkblue":
			color = darkblue;
			return true;
		case "fuchsia":
			color = fuchsia;
			return true;
		case "lightblue":
			color = lightblue;
			return true;
		case "lime":
			color = lime;
			return true;
		case "maroon":
			color = maroon;
			return true;
		case "navy":
			color = navy;
			return true;
		case "olive":
			color = olive;
			return true;
		case "orange":
			color = orange;
			return true;
		case "purple":
			color = purple;
			return true;
		case "silver":
			color = silver;
			return true;
		case "teal":
			color = teal;
			return true;
		case "black":
			color = Color.black;
			return true;
		case "blue":
			color = Color.blue;
			return true;
		case "cyan":
			color = Color.cyan;
			return true;
		case "green":
			color = Color.green;
			return true;
		case "grey":
			color = Color.grey;
			return true;
		case "magenta":
			color = Color.magenta;
			return true;
		case "red":
			color = Color.red;
			return true;
		case "white":
			color = Color.white;
			return true;
		case "yellow":
			color = Color.yellow;
			return true;
		case "R":
			color = Color.red;
			return true;
		case "G":
			color = Color.green;
			return true;
		case "B":
			color = Color.blue;
			return true;
		case "K":
			color = Color.black;
			return true;
		case "Y":
			color = Color.yellow;
			return true;
		case "W":
			color = Color.white;
			return true;
		default:
			color = Color.white;
			return false;
		}
	}

	public static Color Get(string name, Color d)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		if (Get(name, out var color))
		{
			return color;
		}
		return d;
	}
}
