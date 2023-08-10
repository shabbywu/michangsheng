using System.IO;
using UnityEngine;

public static class FileEx
{
	public static bool LoadTex2D(string path, out Texture2D tex)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Expected O, but got Unknown
		tex = new Texture2D(2, 2, (TextureFormat)5, false, false);
		if (File.Exists(path))
		{
			byte[] array = File.ReadAllBytes(path);
			try
			{
				ImageConversion.LoadImage(tex, array);
			}
			catch
			{
				return false;
			}
			return true;
		}
		return false;
	}

	public static bool LoadSprite(string path, out Sprite sprite)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		if (LoadTex2D(path, out var tex))
		{
			sprite = Sprite.Create(tex, new Rect(0f, 0f, (float)((Texture)tex).width, (float)((Texture)tex).height), new Vector2(0.5f, 0.5f));
			return true;
		}
		sprite = null;
		return false;
	}
}
