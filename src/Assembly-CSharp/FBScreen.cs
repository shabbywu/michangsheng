using System.Collections.Generic;
using UnityEngine;

public class FBScreen
{
	public class Layout
	{
		public class OptionLeft : Layout
		{
			public float Amount;
		}

		public class OptionTop : Layout
		{
			public float Amount;
		}

		public class OptionCenterHorizontal : Layout
		{
		}

		public class OptionCenterVertical : Layout
		{
		}
	}

	private static bool resizable;

	public static bool FullScreen
	{
		get
		{
			return Screen.fullScreen;
		}
		set
		{
			Screen.fullScreen = value;
		}
	}

	public static bool Resizable => resizable;

	public static int Width => Screen.width;

	public static int Height => Screen.height;

	public static void SetResolution(int width, int height, bool fullscreen, int preferredRefreshRate = 0, params Layout[] layoutParams)
	{
		Screen.SetResolution(width, height, fullscreen, preferredRefreshRate);
	}

	public static void SetAspectRatio(int width, int height, params Layout[] layoutParams)
	{
		Screen.SetResolution(Screen.height / height * width, Screen.height, Screen.fullScreen);
	}

	public static void SetUnityPlayerEmbedCSS(string key, string value)
	{
	}

	public static Layout.OptionLeft Left(float amount)
	{
		return new Layout.OptionLeft
		{
			Amount = amount
		};
	}

	public static Layout.OptionTop Top(float amount)
	{
		return new Layout.OptionTop
		{
			Amount = amount
		};
	}

	public static Layout.OptionCenterHorizontal CenterHorizontal()
	{
		return new Layout.OptionCenterHorizontal();
	}

	public static Layout.OptionCenterVertical CenterVertical()
	{
		return new Layout.OptionCenterVertical();
	}

	private static void SetLayout(IEnumerable<Layout> parameters)
	{
	}
}
