using System;
using System.IO;
using UnityEngine;

// Token: 0x020001BB RID: 443
public static class FileEx
{
	// Token: 0x0600126C RID: 4716 RVA: 0x000708CC File Offset: 0x0006EACC
	public static bool LoadTex2D(string path, out Texture2D tex)
	{
		tex = new Texture2D(2, 2, 5, false, false);
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

	// Token: 0x0600126D RID: 4717 RVA: 0x0007091C File Offset: 0x0006EB1C
	public static bool LoadSprite(string path, out Sprite sprite)
	{
		Texture2D texture2D;
		if (FileEx.LoadTex2D(path, out texture2D))
		{
			sprite = Sprite.Create(texture2D, new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), new Vector2(0.5f, 0.5f));
			return true;
		}
		sprite = null;
		return false;
	}
}
