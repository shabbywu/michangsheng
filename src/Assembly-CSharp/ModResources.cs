using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x02000305 RID: 773
public static class ModResources
{
	// Token: 0x06001721 RID: 5921 RVA: 0x000146C2 File Offset: 0x000128C2
	public static void InitPath()
	{
		if (!ModResources.inited)
		{
			ModResources.inited = true;
			ModResources.TargetDirPath = Application.dataPath + "/Res";
			ModResources.ModDirPath = Application.dataPath + "/../ModRes";
		}
	}

	// Token: 0x06001722 RID: 5922 RVA: 0x000CC44C File Offset: 0x000CA64C
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

	// Token: 0x06001723 RID: 5923 RVA: 0x000146F9 File Offset: 0x000128F9
	public static void ClearCache()
	{
		ModResources._SpriteCache.Clear();
		ModResources._Texture2DCache.Clear();
		ModResources._TextCache.Clear();
	}

	// Token: 0x06001724 RID: 5924 RVA: 0x00014719 File Offset: 0x00012919
	public static void ClearSpriteCache()
	{
		ModResources._SpriteCache.Clear();
	}

	// Token: 0x06001725 RID: 5925 RVA: 0x00014725 File Offset: 0x00012925
	public static void ClearTextureCache()
	{
		ModResources._Texture2DCache.Clear();
	}

	// Token: 0x06001726 RID: 5926 RVA: 0x00014731 File Offset: 0x00012931
	public static void ClearTextCache()
	{
		ModResources._TextCache.Clear();
	}

	// Token: 0x06001727 RID: 5927 RVA: 0x0001473D File Offset: 0x0001293D
	private static string ParsePath(string path)
	{
		ModResources.InitPath();
		return ModResources.TargetDirPath + "/" + path;
	}

	// Token: 0x06001728 RID: 5928 RVA: 0x00014754 File Offset: 0x00012954
	private static string ParseModPath(string path)
	{
		return ModResources.ModDirPath + "/" + path;
	}

	// Token: 0x06001729 RID: 5929 RVA: 0x000CC4A4 File Offset: 0x000CA6A4
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

	// Token: 0x0600172A RID: 5930 RVA: 0x000CC5C4 File Offset: 0x000CA7C4
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

	// Token: 0x0600172B RID: 5931 RVA: 0x000CC6A8 File Offset: 0x000CA8A8
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
			Sprite sprite2 = Resources.Load<Sprite>(path);
			if (sprite2 != null)
			{
				sprite = sprite2;
			}
		}
		ModResources._SpriteCache.Add(path, sprite);
		return sprite;
	}

	// Token: 0x0400127A RID: 4730
	private static Dictionary<string, Sprite> _SpriteCache = new Dictionary<string, Sprite>();

	// Token: 0x0400127B RID: 4731
	private static Dictionary<string, Texture2D> _Texture2DCache = new Dictionary<string, Texture2D>();

	// Token: 0x0400127C RID: 4732
	private static Dictionary<string, string> _TextCache = new Dictionary<string, string>();

	// Token: 0x0400127D RID: 4733
	private static bool inited;

	// Token: 0x0400127E RID: 4734
	public static string TargetDirPath;

	// Token: 0x0400127F RID: 4735
	public static string ModDirPath;
}
