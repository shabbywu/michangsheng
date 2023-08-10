using System.Collections.Generic;
using UnityEngine;
using YSGame;

public class UIPopTip : MonoBehaviour
{
	public static UIPopTip Inst;

	[SerializeField]
	private RectTransform TipItemRoot;

	[SerializeField]
	private GameObject TipPrefab;

	[SerializeField]
	private List<Sprite> Icon;

	[SerializeField]
	private List<AudioClip> PopEffectSounds;

	private List<UIPopTipItem> Tips = new List<UIPopTipItem>();

	private Queue<PopTipData> WaitForShow = new Queue<PopTipData>();

	private float minCD;

	private float tweenDestoryCD;

	private Dictionary<string, int> addItemMergeMsgDict = new Dictionary<string, int>();

	private float addItemMergeCD;

	private void Awake()
	{
		Inst = this;
	}

	private void Update()
	{
		if (minCD > 0f)
		{
			minCD -= Time.deltaTime;
		}
		if (addItemMergeCD > 0f)
		{
			addItemMergeCD -= Time.deltaTime;
		}
		if (addItemMergeCD <= 0f)
		{
			addItemMergeCD = 0.5f;
			foreach (KeyValuePair<string, int> item2 in addItemMergeMsgDict)
			{
				Pop($"获得{item2.Key}x{item2.Value}", PopTipIconType.包裹);
			}
			addItemMergeMsgDict.Clear();
		}
		if (minCD <= 0f && WaitForShow.Count > 0)
		{
			minCD = 0.8f;
			PopTipData data = WaitForShow.Dequeue();
			UIPopTipItem item = CreateTipObject(data);
			Tips.Insert(0, item);
			for (int num = Tips.Count - 1; num >= 0; num--)
			{
				Tips[num].MsgIndex = num;
				if (num >= 9)
				{
					Tips.RemoveAt(num);
				}
			}
			tweenDestoryCD = (float)Tips.Count * 0.2f + 2f;
		}
		if (tweenDestoryCD > 0f)
		{
			tweenDestoryCD -= Time.deltaTime;
		}
		if (Tips.Count > 0 && (int)(tweenDestoryCD / 0.2f) < Tips.Count)
		{
			UIPopTipItem uIPopTipItem = Tips[Tips.Count - 1];
			Tips.Remove(uIPopTipItem);
			uIPopTipItem.TweenDestory();
		}
	}

	public void Pop(string msg, PopTipIconType iconType = PopTipIconType.叹号)
	{
		PopTipData popTipData = new PopTipData();
		popTipData.IconType = iconType;
		popTipData.Msg = msg;
		WaitForShow.Enqueue(popTipData);
	}

	public void PopAddItem(string itemName, int itemCount)
	{
		if (!addItemMergeMsgDict.ContainsKey(itemName))
		{
			addItemMergeMsgDict.Add(itemName, 0);
		}
		addItemMergeMsgDict[itemName] += itemCount;
	}

	private UIPopTipItem CreateTipObject(PopTipData data)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		GameObject obj = Object.Instantiate<GameObject>(TipPrefab, (Transform)(object)TipItemRoot);
		Transform transform = obj.transform;
		((RectTransform)((transform is RectTransform) ? transform : null)).anchoredPosition = new Vector2(500f, 0f);
		UIPopTipItem component = obj.GetComponent<UIPopTipItem>();
		int iconType = (int)data.IconType;
		component.IconImage.sprite = Icon[iconType];
		component.MsgText.text = data.Msg;
		if (PopEffectSounds.Count > iconType && (Object)(object)PopEffectSounds[iconType] != (Object)null)
		{
			MusicMag.instance.PlayEffectMusic(PopEffectSounds[iconType]);
		}
		return component;
	}
}
