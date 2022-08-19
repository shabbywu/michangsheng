using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Spine.Unity;
using UnityEngine;
using UnityEngine.U2D;

// Token: 0x020003B5 RID: 949
public class ResManager : MonoBehaviour
{
	// Token: 0x06001EC2 RID: 7874 RVA: 0x000D7E06 File Offset: 0x000D6006
	private void Awake()
	{
		ResManager.inst = this;
	}

	// Token: 0x06001EC3 RID: 7875 RVA: 0x000D7E0E File Offset: 0x000D600E
	public void Preload(int taskID)
	{
		Loom.RunAsync(delegate
		{
			this.LoadAsync(taskID);
		});
	}

	// Token: 0x06001EC4 RID: 7876 RVA: 0x000D7E34 File Offset: 0x000D6034
	public void LoadAsync(int taskID)
	{
		try
		{
			this.skillEffectBundle = AssetBundle.LoadFromFile(ResManager.RootPath + "/skilleffect");
			PreloadManager.Inst.TaskDone(taskID);
		}
		catch (Exception arg)
		{
			PreloadManager.IsException = true;
			PreloadManager.ExceptionData += string.Format("{0}\n", arg);
			PreloadManager.Inst.TaskDone(taskID);
		}
	}

	// Token: 0x06001EC5 RID: 7877 RVA: 0x000D7EA8 File Offset: 0x000D60A8
	public Object[] LoadSprites(string path)
	{
		if (!this.spritesDictionary.ContainsKey(path))
		{
			this.spritesDictionary.Add(path, Resources.LoadAll(path));
		}
		return this.spritesDictionary[path];
	}

	// Token: 0x06001EC6 RID: 7878 RVA: 0x000D7ED8 File Offset: 0x000D60D8
	public Dictionary<string, Sprite> LoadSpriteAtlas(string path)
	{
		if (!this.spriteAtlasDictionary.ContainsKey(path))
		{
			SpriteAtlas spriteAtlas = Resources.Load<SpriteAtlas>(path);
			Dictionary<string, Sprite> dictionary = new Dictionary<string, Sprite>();
			Sprite[] array = new Sprite[spriteAtlas.spriteCount];
			spriteAtlas.GetSprites(array);
			foreach (Sprite sprite in array)
			{
				string text = sprite.name.Replace("(Clone)", "");
				if (dictionary.ContainsKey(text))
				{
					Debug.LogError("图集中存在相同的图片名称" + text);
					break;
				}
				dictionary.Add(text, sprite);
			}
			this.spriteAtlasDictionary.Add(path, dictionary);
		}
		return this.spriteAtlasDictionary[path];
	}

	// Token: 0x06001EC7 RID: 7879 RVA: 0x000D7F8C File Offset: 0x000D618C
	public SkeletonDataAsset LoadABSkeletonDataAsset(int sexType, string path)
	{
		if (this.man == null || this.woman == null)
		{
			this.man = AssetBundle.LoadFromFile(ResManager.RootPath + "/man");
			this.woman = AssetBundle.LoadFromFile(ResManager.RootPath + "/woman");
		}
		if (!this.abSkeletonDataAssetDict.ContainsKey(path))
		{
			if (sexType == 1)
			{
				this.abSkeletonDataAssetDict.Add(path, this.man.LoadAsset<SkeletonDataAsset>(path));
			}
			else
			{
				this.abSkeletonDataAssetDict.Add(path, this.woman.LoadAsset<SkeletonDataAsset>(path));
			}
		}
		return this.abSkeletonDataAssetDict[path];
	}

	// Token: 0x06001EC8 RID: 7880 RVA: 0x000D803C File Offset: 0x000D623C
	public Sprite LoadSprite(string path)
	{
		if (this.spriteDictionary.Count >= 20)
		{
			this.spriteDictionary = new Dictionary<string, Sprite>();
		}
		if (!this.spriteDictionary.ContainsKey(path))
		{
			this.spriteDictionary.Add(path, Resources.Load<Sprite>(path));
		}
		return this.spriteDictionary[path];
	}

	// Token: 0x06001EC9 RID: 7881 RVA: 0x000D8090 File Offset: 0x000D6290
	public Texture2D LoadTexture2D(string path)
	{
		if (this.texture2Dictionary.Count >= 20)
		{
			this.texture2Dictionary = new Dictionary<string, Texture2D>();
		}
		if (!this.texture2Dictionary.ContainsKey(path))
		{
			this.texture2Dictionary.Add(path, Resources.Load<Texture2D>(path));
		}
		return this.texture2Dictionary[path];
	}

	// Token: 0x06001ECA RID: 7882 RVA: 0x000D80E4 File Offset: 0x000D62E4
	public Object LoadSkillEffect(string path)
	{
		Object result;
		try
		{
			path = path.Replace(".0", "");
			Object @object = this.skillEffectBundle.LoadAsset<GameObject>(path);
			if (RoundManager.instance != null)
			{
				RoundManager.instance.SkillList.Add(path);
			}
			result = @object;
		}
		catch (Exception ex)
		{
			Debug.LogError(string.Concat(new string[]
			{
				"加载技能特效时出现异常，path:",
				path,
				"\n",
				ex.Message,
				"\n",
				ex.StackTrace
			}));
			result = null;
		}
		return result;
	}

	// Token: 0x06001ECB RID: 7883 RVA: 0x000D8184 File Offset: 0x000D6384
	public bool CheckHasSkillEffect(string path)
	{
		if (RoundManager.instance != null && RoundManager.instance.IsVirtual)
		{
			return false;
		}
		path = path.Replace(".0", "");
		GameObject gameObject = null;
		try
		{
			gameObject = this.skillEffectBundle.LoadAsset<GameObject>(path);
		}
		catch
		{
		}
		return !(gameObject == null);
	}

	// Token: 0x06001ECC RID: 7884 RVA: 0x000D81F0 File Offset: 0x000D63F0
	public Object LoadBuffEffect(string path)
	{
		if (RoundManager.instance != null && RoundManager.instance.IsVirtual)
		{
			return null;
		}
		if (this.buffEffectDictionary.Count > 20)
		{
			this.buffEffectDictionary = new Dictionary<string, Object>();
		}
		path = path.Replace(".0", "");
		path = "BuffEffect/" + path + "/" + path;
		if (!this.buffEffectDictionary.ContainsKey(path))
		{
			GameObject value = Resources.Load<GameObject>(path);
			this.buffEffectDictionary.Add(path, value);
		}
		return this.buffEffectDictionary[path];
	}

	// Token: 0x06001ECD RID: 7885 RVA: 0x000D8287 File Offset: 0x000D6487
	public bool CheckHasBuffEffect(string path)
	{
		path = path.Replace(".0", "");
		path = "BuffEffect/" + path + "/" + path;
		return !(Resources.Load<GameObject>(path) == null);
	}

	// Token: 0x06001ECE RID: 7886 RVA: 0x000D82C0 File Offset: 0x000D64C0
	public GameObject LoadPrefab(string path)
	{
		path = "Prefab/" + path;
		if (this.PrefabDictionary.Count > 10)
		{
			this.PrefabDictionary = new Dictionary<string, GameObject>();
		}
		if (!this.PrefabDictionary.ContainsKey(path))
		{
			GameObject value = Resources.Load<GameObject>(path);
			this.PrefabDictionary.Add(path, value);
		}
		return this.PrefabDictionary[path];
	}

	// Token: 0x06001ECF RID: 7887 RVA: 0x000D8324 File Offset: 0x000D6524
	public GameObject LoadTalk(string path)
	{
		path = "talkPrefab/" + path;
		if (!this.PrefabDictionary.ContainsKey(path))
		{
			GameObject value = Resources.Load<GameObject>(path);
			this.PrefabDictionary.Add(path, value);
		}
		return this.PrefabDictionary[path];
	}

	// Token: 0x06001ED0 RID: 7888 RVA: 0x000D8370 File Offset: 0x000D6570
	public object LoadObject(string path)
	{
		IFormatter formatter = new BinaryFormatter();
		Stream stream = new FileStream(ResManager.RootPath + path, FileMode.Open, FileAccess.Read, FileShare.Read);
		object result = formatter.Deserialize(stream);
		stream.Close();
		return result;
	}

	// Token: 0x06001ED1 RID: 7889 RVA: 0x000D83A4 File Offset: 0x000D65A4
	public void SaveStream(string path, object obj)
	{
		IFormatter formatter = new BinaryFormatter();
		Stream stream = new FileStream(ResManager.RootPath + path, FileMode.Create, FileAccess.Write, FileShare.None);
		formatter.Serialize(stream, obj);
		stream.Close();
	}

	// Token: 0x1700025E RID: 606
	// (get) Token: 0x06001ED2 RID: 7890 RVA: 0x000D83D8 File Offset: 0x000D65D8
	private static string RootPath
	{
		get
		{
			if (Application.platform == 8)
			{
				return Application.dataPath + "/Raw/";
			}
			if (Application.platform == 11)
			{
				return "jar:file://" + Application.dataPath + "!/assets/";
			}
			return Application.streamingAssetsPath + "/";
		}
	}

	// Token: 0x04001930 RID: 6448
	public static ResManager inst;

	// Token: 0x04001931 RID: 6449
	public Dictionary<string, string> pathDictionary = new Dictionary<string, string>();

	// Token: 0x04001932 RID: 6450
	private Dictionary<string, Object[]> spritesDictionary = new Dictionary<string, Object[]>();

	// Token: 0x04001933 RID: 6451
	private Dictionary<string, Sprite> spriteDictionary = new Dictionary<string, Sprite>();

	// Token: 0x04001934 RID: 6452
	private Dictionary<string, Dictionary<string, Sprite>> spriteAtlasDictionary = new Dictionary<string, Dictionary<string, Sprite>>();

	// Token: 0x04001935 RID: 6453
	private Dictionary<string, Texture2D> texture2Dictionary = new Dictionary<string, Texture2D>();

	// Token: 0x04001936 RID: 6454
	private Dictionary<string, Object> buffEffectDictionary = new Dictionary<string, Object>();

	// Token: 0x04001937 RID: 6455
	private Dictionary<string, GameObject> PrefabDictionary = new Dictionary<string, GameObject>();

	// Token: 0x04001938 RID: 6456
	private Dictionary<string, SkeletonDataAsset> abSkeletonDataAssetDict = new Dictionary<string, SkeletonDataAsset>();

	// Token: 0x04001939 RID: 6457
	private AssetBundle man;

	// Token: 0x0400193A RID: 6458
	private AssetBundle woman;

	// Token: 0x0400193B RID: 6459
	private AssetBundle skillEffectBundle;
}
