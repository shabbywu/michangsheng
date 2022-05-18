using System;
using System.Collections.Generic;
using UnityEngine;
using YSGame;

// Token: 0x020004DF RID: 1247
public class UIPopTip : MonoBehaviour
{
	// Token: 0x0600208E RID: 8334 RVA: 0x0001AC6E File Offset: 0x00018E6E
	private void Awake()
	{
		UIPopTip.Inst = this;
	}

	// Token: 0x0600208F RID: 8335 RVA: 0x001135D4 File Offset: 0x001117D4
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

	// Token: 0x06002090 RID: 8336 RVA: 0x001137CC File Offset: 0x001119CC
	public void Pop(string msg, PopTipIconType iconType = PopTipIconType.叹号)
	{
		PopTipData popTipData = new PopTipData();
		popTipData.IconType = iconType;
		popTipData.Msg = msg;
		this.WaitForShow.Enqueue(popTipData);
	}

	// Token: 0x06002091 RID: 8337 RVA: 0x001137FC File Offset: 0x001119FC
	public void PopAddItem(string itemName, int itemCount)
	{
		if (!this.addItemMergeMsgDict.ContainsKey(itemName))
		{
			this.addItemMergeMsgDict.Add(itemName, 0);
		}
		Dictionary<string, int> dictionary = this.addItemMergeMsgDict;
		dictionary[itemName] += itemCount;
	}

	// Token: 0x06002092 RID: 8338 RVA: 0x00113840 File Offset: 0x00111A40
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

	// Token: 0x04001BFF RID: 7167
	public static UIPopTip Inst;

	// Token: 0x04001C00 RID: 7168
	[SerializeField]
	private RectTransform TipItemRoot;

	// Token: 0x04001C01 RID: 7169
	[SerializeField]
	private GameObject TipPrefab;

	// Token: 0x04001C02 RID: 7170
	[SerializeField]
	private List<Sprite> Icon;

	// Token: 0x04001C03 RID: 7171
	[SerializeField]
	private List<AudioClip> PopEffectSounds;

	// Token: 0x04001C04 RID: 7172
	private List<UIPopTipItem> Tips = new List<UIPopTipItem>();

	// Token: 0x04001C05 RID: 7173
	private Queue<PopTipData> WaitForShow = new Queue<PopTipData>();

	// Token: 0x04001C06 RID: 7174
	private float minCD;

	// Token: 0x04001C07 RID: 7175
	private float tweenDestoryCD;

	// Token: 0x04001C08 RID: 7176
	private Dictionary<string, int> addItemMergeMsgDict = new Dictionary<string, int>();

	// Token: 0x04001C09 RID: 7177
	private float addItemMergeCD;
}
