using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGroundImage : MonoBehaviour
{
	public string BGName = "1";

	[HideInInspector]
	public SpriteRenderer sr;

	[HideInInspector]
	public Image image;

	public static Dictionary<string, Sprite> BGDict = new Dictionary<string, Sprite>();

	public static AssetBundle BGAB;

	private static bool inited;

	private void Awake()
	{
		sr = ((Component)this).GetComponent<SpriteRenderer>();
		image = ((Component)this).GetComponent<Image>();
	}

	private void Start()
	{
		LoadBG(BGName);
	}

	public static void Init()
	{
		inited = true;
		BGAB = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/background");
		if ((Object)(object)BGAB == (Object)null)
		{
			Debug.LogError((object)"背景图AB包加载失败，请检查AB");
			return;
		}
		Sprite[] array = BGAB.LoadAllAssets<Sprite>();
		foreach (Sprite val in array)
		{
			BGDict[((Object)val).name] = val;
		}
	}

	public static Sprite GetSprite(string bgName)
	{
		if (!inited)
		{
			Init();
		}
		if (BGDict.ContainsKey(bgName))
		{
			return BGDict[bgName];
		}
		return null;
	}

	public void LoadBG(string bgName)
	{
		if ((Object)(object)sr == (Object)null && (Object)(object)image == (Object)null)
		{
			return;
		}
		if (BGDict.ContainsKey(bgName))
		{
			if ((Object)(object)sr != (Object)null)
			{
				sr.sprite = GetSprite(bgName);
			}
			if ((Object)(object)image != (Object)null)
			{
				image.sprite = GetSprite(bgName);
			}
		}
		else
		{
			Debug.LogError((object)("加载背景图错误，没有背景图 " + bgName));
		}
	}

	public void SyncImageName()
	{
		sr = ((Component)this).GetComponent<SpriteRenderer>();
		image = ((Component)this).GetComponent<Image>();
		if ((Object)(object)sr != (Object)null && (Object)(object)sr.sprite != (Object)null)
		{
			BGName = ((Object)sr.sprite).name;
			sr.sprite = null;
		}
		if ((Object)(object)image != (Object)null && (Object)(object)image.sprite != (Object)null)
		{
			BGName = ((Object)image.sprite).name;
			image.sprite = null;
		}
	}
}
