using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x020001F1 RID: 497
public static class ModResources
{
	// Token: 0x06001477 RID: 5239 RVA: 0x00083587 File Offset: 0x00081787
	public static void InitPath()
	{
		if (!ModResources.inited)
		{
			ModResources.inited = true;
			ModResources.TargetDirPath = Application.dataPath + "/Res";
			ModResources.ModDirPath = Application.dataPath + "/../ModRes";
		}
	}

	// Token: 0x06001478 RID: 5240 RVA: 0x000835C0 File Offset: 0x000817C0
	public static void RemoveCache(string path)
	{
		if (ModResources._SpriteCache.ContainsKey(path))
		{
			ModResources._SpriteCache.Remove(path);
		}
		if (ModResources._Texture2DCache.ContainsKey(path))
		{
			ModResources._Texture2DCache.Remove(path);
		}
		if (ModResources._TextCache.ContainsKey(path))
		{
			ModResources._TextCache.Remove(path);
		}
	}

	// Token: 0x06001479 RID: 5241 RVA: 0x00083618 File Offset: 0x00081818
	public static void ClearCache()
	{
		ModResources._SpriteCache.Clear();
		ModResources._Texture2DCache.Clear();
		ModResources._TextCache.Clear();
	}

	// Token: 0x0600147A RID: 5242 RVA: 0x00083638 File Offset: 0x00081838
	public static void ClearSpriteCache()
	{
		ModResources._SpriteCache.Clear();
	}

	// Token: 0x0600147B RID: 5243 RVA: 0x00083644 File Offset: 0x00081844
	public static void ClearTextureCache()
	{
		ModResources._Texture2DCache.Clear();
	}

	// Token: 0x0600147C RID: 5244 RVA: 0x00083650 File Offset: 0x00081850
	public static void ClearTextCache()
	{
		ModResources._TextCache.Clear();
	}

	// Token: 0x0600147D RID: 5245 RVA: 0x0008365C File Offset: 0x0008185C
	private static string ParsePath(string path)
	{
		ModResources.InitPath();
		return ModResources.TargetDirPath + "/" + path;
	}

	// Token: 0x0600147E RID: 5246 RVA: 0x00083673 File Offset: 0x00081873
	private static string ParseModPath(string path)
	{
		return ModResources.ModDirPath + "/" + path;
	}

	// Token: 0x0600147F RID: 5247 RVA: 0x00083688 File Offset: 0x00081888
	public static string LoadText(string path)
	{
		if (ModResources._TextCache.ContainsKey(path))
		{
			return ModResources._TextCache[path];
		}
		string text = ModResources.ParsePath(path);
		string text2 = ModResources.ParseModPath(path);
		string text3 = "";
		if (File.Exists(text2))
		{
			text3 = File.ReadAllText(text2);
			ModResources._TextCache[path] = text3;
			return text3;
		}
		if (File.Exists(text2 + ".txt"))
		{
			text3 = File.ReadAllText(text2 + ".txt");
			ModResources._TextCache[path] = text3;
			return text3;
		}
		if (File.Exists(text2 + ".json"))
		{
			text3 = File.ReadAllText(text2 + ".json");
			ModResources._TextCache[path] = text3;
			return text3;
		}
		if (File.Exists(text))
		{
			text3 = File.ReadAllText(text);
		}
		else if (File.Exists(text + ".txt"))
		{
			text3 = File.ReadAllText(text + ".txt");
		}
		else if (File.Exists(text + ".json"))
		{
			text3 = File.ReadAllText(text + ".json");
		}
		ModResources._TextCache[path] = text3;
		return text3;
	}

	// Token: 0x06001480 RID: 5248 RVA: 0x000837A8 File Offset: 0x000819A8
	public static Texture2D LoadTexture2D(string path)
	{
		if (ModResources._Texture2DCache.ContainsKey(path))
		{
			return ModResources._Texture2DCache[path];
		}
		string text = ModResources.ParsePath(path);
		string text2 = ModResources.ParseModPath(path);
		Texture2D texture2D;
		if (FileEx.LoadTex2D(text2, out texture2D))
		{
			ModResources._Texture2DCache.Add(path, texture2D);
			return texture2D;
		}
		if (FileEx.LoadTex2D(text2 + ".png", out texture2D))
		{
			ModResources._Texture2DCache.Add(path, texture2D);
			return texture2D;
		}
		if (FileEx.LoadTex2D(text2 + ".jpg", out texture2D))
		{
			ModResources._Texture2DCache.Add(path, texture2D);
			return texture2D;
		}
		if (!FileEx.LoadTex2D(text, out texture2D) && !FileEx.LoadTex2D(text + ".png", out texture2D) && !FileEx.LoadTex2D(text + ".jpg", out texture2D))
		{
			Texture2D texture2D2 = Resources.Load<Texture2D>(path);
			if (texture2D2 != null)
			{
				texture2D = texture2D2;
			}
		}
		ModResources._Texture2DCache.Add(path, texture2D);
		return texture2D;
	}

	// Token: 0x06001481 RID: 5249 RVA: 0x0008388C File Offset: 0x00081A8C
	public static Sprite LoadSprite(string path)
	{
		if (ModResources._SpriteCache.ContainsKey(path))
		{
			return ModResources._SpriteCache[path];
		}
		string text = ModResources.ParsePath(path);
		string text2 = ModResources.ParseModPath(path);
		Sprite sprite;
		if (FileEx.LoadSprite(text2, out sprite))
		{
			ModResources._SpriteCache.Add(path, sprite);
			return sprite;
		}
		if (FileEx.LoadSprite(text2 + ".png", out sprite))
		{
			ModResources._SpriteCache.Add(path, sprite);
			return sprite;
		}
		if (FileEx.LoadSprite(text2 + ".jpg", out sprite))
		{
			ModResources._SpriteCache.Add(path, sprite);
			return sprite;
		}
		if (!FileEx.LoadSprite(text, out sprite) && !FileEx.LoadSprite(text + ".png", out sprite) && !FileEx.LoadSprite(text + ".jpg", out sprite))
		{
			Sprite sprite2 = ResManager.inst.LoadSprite(path);
			if (sprite2 != null)
			{
				sprite = sprite2;
			}
		}
		ModResources._SpriteCache.Add(path, sprite);
		return sprite;
	}

	// Token: 0x04000F38 RID: 3896
	public static Dictionary<string, Sprite> _SpriteCache = new Dictionary<string, Sprite>();

	// Token: 0x04000F39 RID: 3897
	public static Dictionary<string, Texture2D> _Texture2DCache = new Dictionary<string, Texture2D>();

	// Token: 0x04000F3A RID: 3898
	public static Dictionary<string, string> _TextCache = new Dictionary<string, string>();

	// Token: 0x04000F3B RID: 3899
	public static bool inited;

	// Token: 0x04000F3C RID: 3900
	public static string TargetDirPath;

	// Token: 0x04000F3D RID: 3901
	public static string ModDirPath;
}
