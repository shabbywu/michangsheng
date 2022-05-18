using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.U2D;

// Token: 0x02000540 RID: 1344
public class ResManager : MonoBehaviour
{
	// Token: 0x06002245 RID: 8773 RVA: 0x0001C179 File Offset: 0x0001A379
	private void Awake()
	{
		ResManager.inst = this;
	}

	// Token: 0x06002246 RID: 8774 RVA: 0x0001C181 File Offset: 0x0001A381
	public void Preload(int taskID)
	{
		Loom.RunAsync(delegate
		{
			this.LoadAsync(taskID);
		});
	}

	// Token: 0x06002247 RID: 8775 RVA: 0x0011B20C File Offset: 0x0011940C
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
		}
	}

	// Token: 0x06002248 RID: 8776 RVA: 0x0001C1A7 File Offset: 0x0001A3A7
	public Object[] LoadSprites(string path)
	{
		if (!this.spritesDictionary.ContainsKey(path))
		{
			this.spritesDictionary.Add(path, Resources.LoadAll(path));
		}
		return this.spritesDictionary[path];
	}

	// Token: 0x06002249 RID: 8777 RVA: 0x0011B274 File Offset: 0x00119474
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

	// Token: 0x0600224A RID: 8778 RVA: 0x0011B328 File Offset: 0x00119528
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

	// Token: 0x0600224B RID: 8779 RVA: 0x0011B37C File Offset: 0x0011957C
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

	// Token: 0x0600224C RID: 8780 RVA: 0x0011B3D0 File Offset: 0x001195D0
	public Object LoadSkillEffect(string path)
	{
		Object result;
		try
		{
			path = path.Replace(".0", "");
			result = this.skillEffectBundle.LoadAsset<GameObject>(path);
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

	// Token: 0x0600224D RID: 8781 RVA: 0x0011B454 File Offset: 0x00119654
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

	// Token: 0x0600224E RID: 8782 RVA: 0x0011B4C0 File Offset: 0x001196C0
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

	// Token: 0x0600224F RID: 8783 RVA: 0x0001C1D5 File Offset: 0x0001A3D5
	public bool CheckHasBuffEffect(string path)
	{
		path = path.Replace(".0", "");
		path = "BuffEffect/" + path + "/" + path;
		return !(Resources.Load<GameObject>(path) == null);
	}

	// Token: 0x06002250 RID: 8784 RVA: 0x0011B558 File Offset: 0x00119758
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

	// Token: 0x06002251 RID: 8785 RVA: 0x0011B5BC File Offset: 0x001197BC
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

	// Token: 0x06002252 RID: 8786 RVA: 0x0011B608 File Offset: 0x00119808
	public object LoadObject(string path)
	{
		IFormatter formatter = new BinaryFormatter();
		Stream stream = new FileStream(ResManager.RootPath + path, FileMode.Open, FileAccess.Read, FileShare.Read);
		object result = formatter.Deserialize(stream);
		stream.Close();
		return result;
	}

	// Token: 0x06002253 RID: 8787 RVA: 0x0011B63C File Offset: 0x0011983C
	public void SaveStream(string path, object obj)
	{
		IFormatter formatter = new BinaryFormatter();
		Stream stream = new FileStream(ResManager.RootPath + path, FileMode.Create, FileAccess.Write, FileShare.None);
		formatter.Serialize(stream, obj);
		stream.Close();
	}

	// Token: 0x170002B0 RID: 688
	// (get) Token: 0x06002254 RID: 8788 RVA: 0x0011B670 File Offset: 0x00119870
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

	// Token: 0x04001DA4 RID: 7588
	public static ResManager inst;

	// Token: 0x04001DA5 RID: 7589
	public Dictionary<string, string> pathDictionary = new Dictionary<string, string>();

	// Token: 0x04001DA6 RID: 7590
	private Dictionary<string, Object[]> spritesDictionary = new Dictionary<string, Object[]>();

	// Token: 0x04001DA7 RID: 7591
	private Dictionary<string, Sprite> spriteDictionary = new Dictionary<string, Sprite>();

	// Token: 0x04001DA8 RID: 7592
	private Dictionary<string, Dictionary<string, Sprite>> spriteAtlasDictionary = new Dictionary<string, Dictionary<string, Sprite>>();

	// Token: 0x04001DA9 RID: 7593
	private Dictionary<string, Texture2D> texture2Dictionary = new Dictionary<string, Texture2D>();

	// Token: 0x04001DAA RID: 7594
	private Dictionary<string, Object> buffEffectDictionary = new Dictionary<string, Object>();

	// Token: 0x04001DAB RID: 7595
	private Dictionary<string, GameObject> PrefabDictionary = new Dictionary<string, GameObject>();

	// Token: 0x04001DAC RID: 7596
	private AssetBundle skillEffectBundle;
}
