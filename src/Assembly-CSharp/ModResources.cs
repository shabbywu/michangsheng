using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class ModResources
{
	public static Dictionary<string, Sprite> _SpriteCache = new Dictionary<string, Sprite>();

	public static Dictionary<string, Texture2D> _Texture2DCache = new Dictionary<string, Texture2D>();

	public static Dictionary<string, string> _TextCache = new Dictionary<string, string>();

	public static bool inited;

	public static string TargetDirPath;

	public static string ModDirPath;

	public static void InitPath()
	{
		if (!inited)
		{
			inited = true;
			TargetDirPath = Application.dataPath + "/Res";
			ModDirPath = Application.dataPath + "/../ModRes";
		}
	}

	public static void RemoveCache(string path)
	{
		if (_SpriteCache.ContainsKey(path))
		{
			_SpriteCache.Remove(path);
		}
		if (_Texture2DCache.ContainsKey(path))
		{
			_Texture2DCache.Remove(path);
		}
		if (_TextCache.ContainsKey(path))
		{
			_TextCache.Remove(path);
		}
	}

	public static void ClearCache()
	{
		_SpriteCache.Clear();
		_Texture2DCache.Clear();
		_TextCache.Clear();
	}

	public static void ClearSpriteCache()
	{
		_SpriteCache.Clear();
	}

	public static void ClearTextureCache()
	{
		_Texture2DCache.Clear();
	}

	public static void ClearTextCache()
	{
		_TextCache.Clear();
	}

	private static string ParsePath(string path)
	{
		InitPath();
		return TargetDirPath + "/" + path;
	}

	private static string ParseModPath(string path)
	{
		return ModDirPath + "/" + path;
	}

	public static string LoadText(string path)
	{
		if (_TextCache.ContainsKey(path))
		{
			return _TextCache[path];
		}
		string text = ParsePath(path);
		string text2 = ParseModPath(path);
		string text3 = "";
		if (File.Exists(text2))
		{
			text3 = File.ReadAllText(text2);
			_TextCache[path] = text3;
			return text3;
		}
		if (File.Exists(text2 + ".txt"))
		{
			text3 = File.ReadAllText(text2 + ".txt");
			_TextCache[path] = text3;
			return text3;
		}
		if (File.Exists(text2 + ".json"))
		{
			text3 = File.ReadAllText(text2 + ".json");
			_TextCache[path] = text3;
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
		_TextCache[path] = text3;
		return text3;
	}

	public static Texture2D LoadTexture2D(string path)
	{
		if (_Texture2DCache.ContainsKey(path))
		{
			return _Texture2DCache[path];
		}
		string text = ParsePath(path);
		string text2 = ParseModPath(path);
		if (FileEx.LoadTex2D(text2, out var tex))
		{
			_Texture2DCache.Add(path, tex);
			return tex;
		}
		if (FileEx.LoadTex2D(text2 + ".png", out tex))
		{
			_Texture2DCache.Add(path, tex);
			return tex;
		}
		if (FileEx.LoadTex2D(text2 + ".jpg", out tex))
		{
			_Texture2DCache.Add(path, tex);
			return tex;
		}
		if (!FileEx.LoadTex2D(text, out tex) && !FileEx.LoadTex2D(text + ".png", out tex) && !FileEx.LoadTex2D(text + ".jpg", out tex))
		{
			Texture2D val = Resources.Load<Texture2D>(path);
			if ((Object)(object)val != (Object)null)
			{
				tex = val;
			}
		}
		_Texture2DCache.Add(path, tex);
		return tex;
	}

	public static Sprite LoadSprite(string path)
	{
		if (_SpriteCache.ContainsKey(path))
		{
			return _SpriteCache[path];
		}
		string text = ParsePath(path);
		string text2 = ParseModPath(path);
		if (FileEx.LoadSprite(text2, out var sprite))
		{
			_SpriteCache.Add(path, sprite);
			return sprite;
		}
		if (FileEx.LoadSprite(text2 + ".png", out sprite))
		{
			_SpriteCache.Add(path, sprite);
			return sprite;
		}
		if (FileEx.LoadSprite(text2 + ".jpg", out sprite))
		{
			_SpriteCache.Add(path, sprite);
			return sprite;
		}
		if (!FileEx.LoadSprite(text, out sprite) && !FileEx.LoadSprite(text + ".png", out sprite) && !FileEx.LoadSprite(text + ".jpg", out sprite))
		{
			Sprite val = ResManager.inst.LoadSprite(path);
			if ((Object)(object)val != (Object)null)
			{
				sprite = val;
			}
		}
		_SpriteCache.Add(path, sprite);
		return sprite;
	}
}
