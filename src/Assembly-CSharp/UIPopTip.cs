using System;
using System.Collections.Generic;
using UnityEngine;
using YSGame;

// Token: 0x02000362 RID: 866
public class UIPopTip : MonoBehaviour
{
	// Token: 0x06001D24 RID: 7460 RVA: 0x000CEC12 File Offset: 0x000CCE12
	private void Awake()
	{
		UIPopTip.Inst = this;
	}

	// Token: 0x06001D25 RID: 7461 RVA: 0x000CEC1C File Offset: 0x000CCE1C
	private void Update()
	{
		if (this.minCD > 0f)
		{
			this.minCD -= Time.deltaTime;
		}
		if (this.addItemMergeCD > 0f)
		{
			this.addItemMergeCD -= Time.deltaTime;
		}
		if (this.addItemMergeCD <= 0f)
		{
			this.addItemMergeCD = 0.5f;
			foreach (KeyValuePair<string, int> keyValuePair in this.addItemMergeMsgDict)
			{
				this.Pop(string.Format("获得{0}x{1}", keyValuePair.Key, keyValuePair.Value), PopTipIconType.包裹);
			}
			this.addItemMergeMsgDict.Clear();
		}
		if (this.minCD <= 0f && this.WaitForShow.Count > 0)
		{
			this.minCD = 0.8f;
			PopTipData data = this.WaitForShow.Dequeue();
			UIPopTipItem item = this.CreateTipObject(data);
			this.Tips.Insert(0, item);
			for (int i = this.Tips.Count - 1; i >= 0; i--)
			{
				this.Tips[i].MsgIndex = i;
				if (i >= 9)
				{
					this.Tips.RemoveAt(i);
				}
			}
			this.tweenDestoryCD = (float)this.Tips.Count * 0.2f + 2f;
		}
		if (this.tweenDestoryCD > 0f)
		{
			this.tweenDestoryCD -= Time.deltaTime;
		}
		if (this.Tips.Count > 0 && (int)(this.tweenDestoryCD / 0.2f) < this.Tips.Count)
		{
			UIPopTipItem uipopTipItem = this.Tips[this.Tips.Count - 1];
			this.Tips.Remove(uipopTipItem);
			uipopTipItem.TweenDestory();
		}
	}

	// Token: 0x06001D26 RID: 7462 RVA: 0x000CEE14 File Offset: 0x000CD014
	public void Pop(string msg, PopTipIconType iconType = PopTipIconType.叹号)
	{
		PopTipData popTipData = new PopTipData();
		popTipData.IconType = iconType;
		popTipData.Msg = msg;
		this.WaitForShow.Enqueue(popTipData);
	}

	// Token: 0x06001D27 RID: 7463 RVA: 0x000CEE44 File Offset: 0x000CD044
	public void PopAddItem(string itemName, int itemCount)
	{
		if (!this.addItemMergeMsgDict.ContainsKey(itemName))
		{
			this.addItemMergeMsgDict.Add(itemName, 0);
		}
		Dictionary<string, int> dictionary = this.addItemMergeMsgDict;
		dictionary[itemName] += itemCount;
	}

	// Token: 0x06001D28 RID: 7464 RVA: 0x000CEE88 File Offset: 0x000CD088
	private UIPopTipItem CreateTipObject(PopTipData data)
	{
		GameObject gameObject = Object.Instantiate<GameObject>(this.TipPrefab, this.TipItemRoot);
		(gameObject.transform as RectTransform).anchoredPosition = new Vector2(500f, 0f);
		UIPopTipItem component = gameObject.GetComponent<UIPopTipItem>();
		int iconType = (int)data.IconType;
		component.IconImage.sprite = this.Icon[iconType];
		component.MsgText.text = data.Msg;
		if (this.PopEffectSounds.Count > iconType && this.PopEffectSounds[iconType] != null)
		{
			MusicMag.instance.PlayEffectMusic(this.PopEffectSounds[iconType], 1f);
		}
		return component;
	}

	// Token: 0x040017AD RID: 6061
	public static UIPopTip Inst;

	// Token: 0x040017AE RID: 6062
	[SerializeField]
	private RectTransform TipItemRoot;

	// Token: 0x040017AF RID: 6063
	[SerializeField]
	private GameObject TipPrefab;

	// Token: 0x040017B0 RID: 6064
	[SerializeField]
	private List<Sprite> Icon;

	// Token: 0x040017B1 RID: 6065
	[SerializeField]
	private List<AudioClip> PopEffectSounds;

	// Token: 0x040017B2 RID: 6066
	private List<UIPopTipItem> Tips = new List<UIPopTipItem>();

	// Token: 0x040017B3 RID: 6067
	private Queue<PopTipData> WaitForShow = new Queue<PopTipData>();

	// Token: 0x040017B4 RID: 6068
	private float minCD;

	// Token: 0x040017B5 RID: 6069
	private float tweenDestoryCD;

	// Token: 0x040017B6 RID: 6070
	private Dictionary<string, int> addItemMergeMsgDict = new Dictionary<string, int>();

	// Token: 0x040017B7 RID: 6071
	private float addItemMergeCD;
}
