using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Spine.Unity;
using UnityEngine;
using UnityEngine.U2D;

public class ResManager : MonoBehaviour
{
	public static ResManager inst;

	public Dictionary<string, string> pathDictionary = new Dictionary<string, string>();

	private Dictionary<string, Object[]> spritesDictionary = new Dictionary<string, Object[]>();

	private Dictionary<string, Sprite> spriteDictionary = new Dictionary<string, Sprite>();

	private Dictionary<string, Dictionary<string, Sprite>> spriteAtlasDictionary = new Dictionary<string, Dictionary<string, Sprite>>();

	private Dictionary<string, Texture2D> texture2Dictionary = new Dictionary<string, Texture2D>();

	private Dictionary<string, Object> buffEffectDictionary = new Dictionary<string, Object>();

	private Dictionary<string, GameObject> PrefabDictionary = new Dictionary<string, GameObject>();

	private Dictionary<string, SkeletonDataAsset> abSkeletonDataAssetDict = new Dictionary<string, SkeletonDataAsset>();

	private AssetBundle man;

	private AssetBundle woman;

	private AssetBundle skillEffectBundle;

	private static string RootPath
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Invalid comparison between Unknown and I4
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			//IL_001f: Invalid comparison between Unknown and I4
			if ((int)Application.platform == 8)
			{
				return Application.dataPath + "/Raw/";
			}
			if ((int)Application.platform == 11)
			{
				return "jar:file://" + Application.dataPath + "!/assets/";
			}
			return Application.streamingAssetsPath + "/";
		}
	}

	private void Awake()
	{
		inst = this;
	}

	public void Preload(int taskID)
	{
		Loom.RunAsync(delegate
		{
			LoadAsync(taskID);
		});
	}

	public void LoadAsync(int taskID)
	{
		try
		{
			skillEffectBundle = AssetBundle.LoadFromFile(RootPath + "/skilleffect");
			PreloadManager.Inst.TaskDone(taskID);
		}
		catch (Exception arg)
		{
			PreloadManager.IsException = true;
			PreloadManager.ExceptionData += $"{arg}\n";
			PreloadManager.Inst.TaskDone(taskID);
		}
	}

	public Object[] LoadSprites(string path)
	{
		if (!spritesDictionary.ContainsKey(path))
		{
			spritesDictionary.Add(path, Resources.LoadAll(path));
		}
		return spritesDictionary[path];
	}

	public Dictionary<string, Sprite> LoadSpriteAtlas(string path)
	{
		if (!spriteAtlasDictionary.ContainsKey(path))
		{
			SpriteAtlas obj = Resources.Load<SpriteAtlas>(path);
			Dictionary<string, Sprite> dictionary = new Dictionary<string, Sprite>();
			Sprite[] array = (Sprite[])(object)new Sprite[obj.spriteCount];
			obj.GetSprites(array);
			string text = "";
			Sprite[] array2 = array;
			foreach (Sprite val in array2)
			{
				text = ((Object)val).name.Replace("(Clone)", "");
				if (dictionary.ContainsKey(text))
				{
					Debug.LogError((object)("图集中存在相同的图片名称" + text));
					break;
				}
				dictionary.Add(text, val);
			}
			spriteAtlasDictionary.Add(path, dictionary);
		}
		return spriteAtlasDictionary[path];
	}

	public SkeletonDataAsset LoadABSkeletonDataAsset(int sexType, string path)
	{
		if ((Object)(object)man == (Object)null || (Object)(object)woman == (Object)null)
		{
			man = AssetBundle.LoadFromFile(RootPath + "/man");
			woman = AssetBundle.LoadFromFile(RootPath + "/woman");
		}
		if (!abSkeletonDataAssetDict.ContainsKey(path))
		{
			if (sexType == 1)
			{
				abSkeletonDataAssetDict.Add(path, man.LoadAsset<SkeletonDataAsset>(path));
			}
			else
			{
				abSkeletonDataAssetDict.Add(path, woman.LoadAsset<SkeletonDataAsset>(path));
			}
		}
		return abSkeletonDataAssetDict[path];
	}

	public Sprite LoadSprite(string path)
	{
		if (spriteDictionary.Count >= 20)
		{
			spriteDictionary = new Dictionary<string, Sprite>();
		}
		if (!spriteDictionary.ContainsKey(path))
		{
			spriteDictionary.Add(path, Resources.Load<Sprite>(path));
		}
		return spriteDictionary[path];
	}

	public Texture2D LoadTexture2D(string path)
	{
		if (texture2Dictionary.Count >= 20)
		{
			texture2Dictionary = new Dictionary<string, Texture2D>();
		}
		if (!texture2Dictionary.ContainsKey(path))
		{
			texture2Dictionary.Add(path, Resources.Load<Texture2D>(path));
		}
		return texture2Dictionary[path];
	}

	public Object LoadSkillEffect(string path)
	{
		try
		{
			path = path.Replace(".0", "");
			GameObject result = skillEffectBundle.LoadAsset<GameObject>(path);
			if ((Object)(object)RoundManager.instance != (Object)null)
			{
				RoundManager.instance.SkillList.Add(path);
			}
			return (Object)(object)result;
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("加载技能特效时出现异常，path:" + path + "\n" + ex.Message + "\n" + ex.StackTrace));
			return null;
		}
	}

	public bool CheckHasSkillEffect(string path)
	{
		if ((Object)(object)RoundManager.instance != (Object)null && RoundManager.instance.IsVirtual)
		{
			return false;
		}
		path = path.Replace(".0", "");
		GameObject val = null;
		try
		{
			val = skillEffectBundle.LoadAsset<GameObject>(path);
		}
		catch
		{
		}
		if ((Object)(object)val == (Object)null)
		{
			return false;
		}
		return true;
	}

	public Object LoadBuffEffect(string path)
	{
		if ((Object)(object)RoundManager.instance != (Object)null && RoundManager.instance.IsVirtual)
		{
			return null;
		}
		if (buffEffectDictionary.Count > 20)
		{
			buffEffectDictionary = new Dictionary<string, Object>();
		}
		path = path.Replace(".0", "");
		path = "BuffEffect/" + path + "/" + path;
		GameObject val = null;
		if (!buffEffectDictionary.ContainsKey(path))
		{
			val = Resources.Load<GameObject>(path);
			buffEffectDictionary.Add(path, (Object)(object)val);
		}
		return buffEffectDictionary[path];
	}

	public bool CheckHasBuffEffect(string path)
	{
		path = path.Replace(".0", "");
		path = "BuffEffect/" + path + "/" + path;
		if ((Object)(object)Resources.Load<GameObject>(path) == (Object)null)
		{
			return false;
		}
		return true;
	}

	public GameObject LoadPrefab(string path)
	{
		path = "Prefab/" + path;
		GameObject val = null;
		if (PrefabDictionary.Count > 10)
		{
			PrefabDictionary = new Dictionary<string, GameObject>();
		}
		if (!PrefabDictionary.ContainsKey(path))
		{
			val = Resources.Load<GameObject>(path);
			PrefabDictionary.Add(path, val);
		}
		return PrefabDictionary[path];
	}

	public GameObject LoadTalk(string path)
	{
		path = "talkPrefab/" + path;
		GameObject val = null;
		if (!PrefabDictionary.ContainsKey(path))
		{
			val = Resources.Load<GameObject>(path);
			PrefabDictionary.Add(path, val);
		}
		return PrefabDictionary[path];
	}

	public object LoadObject(string path)
	{
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		Stream stream = new FileStream(RootPath + path, FileMode.Open, FileAccess.Read, FileShare.Read);
		object result = ((IFormatter)binaryFormatter).Deserialize(stream);
		stream.Close();
		return result;
	}

	public void SaveStream(string path, object obj)
	{
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		Stream stream = new FileStream(RootPath + path, FileMode.Create, FileAccess.Write, FileShare.None);
		((IFormatter)binaryFormatter).Serialize(stream, obj);
		stream.Close();
	}
}
