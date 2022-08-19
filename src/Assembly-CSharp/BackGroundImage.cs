using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000182 RID: 386
public class BackGroundImage : MonoBehaviour
{
	// Token: 0x06001067 RID: 4199 RVA: 0x00060735 File Offset: 0x0005E935
	private void Awake()
	{
		this.sr = base.GetComponent<SpriteRenderer>();
		this.image = base.GetComponent<Image>();
	}

	// Token: 0x06001068 RID: 4200 RVA: 0x0006074F File Offset: 0x0005E94F
	private void Start()
	{
		this.LoadBG(this.BGName);
	}

	// Token: 0x06001069 RID: 4201 RVA: 0x00060760 File Offset: 0x0005E960
	public static void Init()
	{
		BackGroundImage.inited = true;
		BackGroundImage.BGAB = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/background");
		if (BackGroundImage.BGAB == null)
		{
			Debug.LogError("背景图AB包加载失败，请检查AB");
			return;
		}
		foreach (Sprite sprite in BackGroundImage.BGAB.LoadAllAssets<Sprite>())
		{
			BackGroundImage.BGDict[sprite.name] = sprite;
		}
	}

	// Token: 0x0600106A RID: 4202 RVA: 0x000607D2 File Offset: 0x0005E9D2
	public static Sprite GetSprite(string bgName)
	{
		if (!BackGroundImage.inited)
		{
			BackGroundImage.Init();
		}
		if (BackGroundImage.BGDict.ContainsKey(bgName))
		{
			return BackGroundImage.BGDict[bgName];
		}
		return null;
	}

	// Token: 0x0600106B RID: 4203 RVA: 0x000607FC File Offset: 0x0005E9FC
	public void LoadBG(string bgName)
	{
		if (this.sr == null && this.image == null)
		{
			return;
		}
		if (BackGroundImage.BGDict.ContainsKey(bgName))
		{
			if (this.sr != null)
			{
				this.sr.sprite = BackGroundImage.GetSprite(bgName);
			}
			if (this.image != null)
			{
				this.image.sprite = BackGroundImage.GetSprite(bgName);
				return;
			}
		}
		else
		{
			Debug.LogError("加载背景图错误，没有背景图 " + bgName);
		}
	}

	// Token: 0x0600106C RID: 4204 RVA: 0x00060884 File Offset: 0x0005EA84
	public void SyncImageName()
	{
		this.sr = base.GetComponent<SpriteRenderer>();
		this.image = base.GetComponent<Image>();
		if (this.sr != null && this.sr.sprite != null)
		{
			this.BGName = this.sr.sprite.name;
			this.sr.sprite = null;
		}
		if (this.image != null && this.image.sprite != null)
		{
			this.BGName = this.image.sprite.name;
			this.image.sprite = null;
		}
	}

	// Token: 0x04000BE0 RID: 3040
	public string BGName = "1";

	// Token: 0x04000BE1 RID: 3041
	[HideInInspector]
	public SpriteRenderer sr;

	// Token: 0x04000BE2 RID: 3042
	[HideInInspector]
	public Image image;

	// Token: 0x04000BE3 RID: 3043
	public static Dictionary<string, Sprite> BGDict = new Dictionary<string, Sprite>();

	// Token: 0x04000BE4 RID: 3044
	public static AssetBundle BGAB;

	// Token: 0x04000BE5 RID: 3045
	private static bool inited;
}
