using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace YSGame.TuJian;

public class SuperScrollView : MonoBehaviour
{
	private ScrollRect mScrollRect;

	private RectTransform mContentRect;

	public GameObject itemPrefab;

	public float itemHeight;

	private int maxItemCount = 20;

	private List<Dictionary<int, string>> _DataList = new List<Dictionary<int, string>>();

	private int firstIndex;

	private int lastIndex;

	private List<SSVItem> itemList;

	private SSVPool pool = new SSVPool();

	[HideInInspector]
	public int NowSelectItemIndex;

	[HideInInspector]
	public bool NeedResetToTop;

	[HideInInspector]
	public bool NeedSetScroller;

	[HideInInspector]
	public List<Dictionary<int, string>> DataList
	{
		get
		{
			return _DataList;
		}
		set
		{
			_DataList = value;
			NeedResetToTop = true;
			NeedSetScroller = true;
		}
	}

	public int NowSelectID
	{
		get
		{
			if (DataList == null || DataList.Count == 0)
			{
				return -1;
			}
			if (NowSelectItemIndex >= DataList.Count)
			{
				NowSelectItemIndex = 0;
			}
			return DataList[NowSelectItemIndex].Keys.First();
		}
		set
		{
			for (int i = 0; i < DataList.Count; i++)
			{
				if (DataList[i].Keys.First() == value)
				{
					NowSelectItemIndex = i;
					break;
				}
			}
		}
	}

	private void Start()
	{
		Init();
		SetScroller();
	}

	public void Init()
	{
		itemList = new List<SSVItem>();
		mScrollRect = ((Component)((Component)this).transform).GetComponent<ScrollRect>();
		mContentRect = ((Component)((Component)mScrollRect.content).transform).GetComponent<RectTransform>();
		((UnityEvent<Vector2>)(object)mScrollRect.onValueChanged).AddListener((UnityAction<Vector2>)delegate(Vector2 vec)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			OnScrollMove(vec);
		});
		for (int i = 0; i < maxItemCount; i++)
		{
			GameObject val = Object.Instantiate<GameObject>(itemPrefab, ((Component)mContentRect).transform);
			val.GetComponent<SSVItem>().SSV = this;
			pool.Recovery(val.GetComponent<SSVItem>());
		}
	}

	private void Update()
	{
		if (NeedResetToTop)
		{
			ResetToTop();
			NeedResetToTop = false;
		}
		if (NeedSetScroller)
		{
			SetScroller();
			NeedSetScroller = false;
		}
	}

	private void ResetToTop()
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		NowSelectItemIndex = 0;
		firstIndex = 0;
		lastIndex = 0;
		mContentRect.anchoredPosition = new Vector2(mContentRect.anchoredPosition.x, 0f);
	}

	public void ResetToTopByHyperlink()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		firstIndex = 0;
		lastIndex = 0;
		mContentRect.anchoredPosition = new Vector2(mContentRect.anchoredPosition.x, 0f);
	}

	private void SetScroller()
	{
		SetContentHeight();
		InitCountent();
	}

	public void SetContentHeight()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		mContentRect.sizeDelta = new Vector2(mContentRect.sizeDelta.x, itemHeight * (float)DataList.Count);
	}

	public void InitCountent()
	{
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		int num = Mathf.Clamp(DataList.Count, 0, maxItemCount);
		if (num < itemList.Count)
		{
			int num2 = itemList.Count - num;
			for (int i = 0; i < num2; i++)
			{
				pool.Recovery(itemList[0]);
				itemList.RemoveAt(0);
			}
		}
		else if (num > itemList.Count)
		{
			for (int j = itemList.Count; j < num; j++)
			{
				itemList.Add(pool.Get());
			}
		}
		for (int k = 0; k < itemList.Count; k++)
		{
			((Object)itemList[k]).name = k.ToString();
			itemList[k].DataList = DataList;
			itemList[k].DataIndex = k;
			RectTransform component = ((Component)itemList[k]).GetComponent<RectTransform>();
			component.pivot = new Vector2(0.5f, 1f);
			component.anchorMax = new Vector2(0.5f, 1f);
			component.anchorMin = new Vector2(0.5f, 1f);
			component.anchoredPosition = new Vector2(0f, (0f - itemHeight) * (float)k);
			lastIndex = k;
		}
	}

	private void OnScrollMove(Vector2 pVec)
	{
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		if (DataList != null && DataList.Count != 0)
		{
			while (mContentRect.anchoredPosition.y > (float)(firstIndex + 1) * itemHeight && lastIndex != DataList.Count - 1)
			{
				SSVItem sSVItem = itemList[0];
				RectTransform component = ((Component)sSVItem).GetComponent<RectTransform>();
				itemList.RemoveAt(0);
				itemList.Add(sSVItem);
				component.anchoredPosition = new Vector2(0f, (float)(-(lastIndex + 1)) * itemHeight);
				firstIndex++;
				lastIndex++;
				((Object)sSVItem).name = lastIndex.ToString();
				sSVItem.DataIndex = lastIndex;
			}
			while (mContentRect.anchoredPosition.y < (float)firstIndex * itemHeight && firstIndex != 0)
			{
				SSVItem sSVItem2 = itemList[itemList.Count - 1];
				RectTransform component2 = ((Component)sSVItem2).GetComponent<RectTransform>();
				itemList.RemoveAt(itemList.Count - 1);
				itemList.Insert(0, sSVItem2);
				component2.anchoredPosition = new Vector2(0f, (float)(-(firstIndex - 1)) * itemHeight);
				firstIndex--;
				lastIndex--;
				((Object)sSVItem2).name = firstIndex.ToString();
				sSVItem2.DataIndex = firstIndex;
			}
		}
	}
}
