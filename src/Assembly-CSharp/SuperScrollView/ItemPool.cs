using System.Collections.Generic;
using UnityEngine;

namespace SuperScrollView;

public class ItemPool
{
	private GameObject mPrefabObj;

	private string mPrefabName;

	private int mInitCreateCount = 1;

	private float mPadding;

	private float mStartPosOffset;

	private List<LoopListViewItem2> mTmpPooledItemList = new List<LoopListViewItem2>();

	private List<LoopListViewItem2> mPooledItemList = new List<LoopListViewItem2>();

	private static int mCurItemIdCount;

	private RectTransform mItemParent;

	public void Init(GameObject prefabObj, float padding, float startPosOffset, int createCount, RectTransform parent)
	{
		mPrefabObj = prefabObj;
		mPrefabName = ((Object)mPrefabObj).name;
		mInitCreateCount = createCount;
		mPadding = padding;
		mStartPosOffset = startPosOffset;
		mItemParent = parent;
		mPrefabObj.SetActive(false);
		for (int i = 0; i < mInitCreateCount; i++)
		{
			LoopListViewItem2 item = CreateItem();
			RecycleItemReal(item);
		}
	}

	public LoopListViewItem2 GetItem()
	{
		mCurItemIdCount++;
		LoopListViewItem2 loopListViewItem = null;
		if (mTmpPooledItemList.Count > 0)
		{
			int count = mTmpPooledItemList.Count;
			loopListViewItem = mTmpPooledItemList[count - 1];
			mTmpPooledItemList.RemoveAt(count - 1);
			((Component)loopListViewItem).gameObject.SetActive(true);
		}
		else
		{
			int count2 = mPooledItemList.Count;
			if (count2 == 0)
			{
				loopListViewItem = CreateItem();
			}
			else
			{
				loopListViewItem = mPooledItemList[count2 - 1];
				mPooledItemList.RemoveAt(count2 - 1);
				((Component)loopListViewItem).gameObject.SetActive(true);
			}
		}
		loopListViewItem.Padding = mPadding;
		loopListViewItem.ItemId = mCurItemIdCount;
		return loopListViewItem;
	}

	public void DestroyAllItem()
	{
		ClearTmpRecycledItem();
		int count = mPooledItemList.Count;
		for (int i = 0; i < count; i++)
		{
			Object.DestroyImmediate((Object)(object)((Component)mPooledItemList[i]).gameObject);
		}
		mPooledItemList.Clear();
	}

	public LoopListViewItem2 CreateItem()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		GameObject obj = Object.Instantiate<GameObject>(mPrefabObj, Vector3.zero, Quaternion.identity, (Transform)(object)mItemParent);
		obj.SetActive(true);
		RectTransform component = obj.GetComponent<RectTransform>();
		((Transform)component).localScale = Vector3.one;
		component.anchoredPosition3D = Vector3.zero;
		((Transform)component).localEulerAngles = Vector3.zero;
		LoopListViewItem2 component2 = obj.GetComponent<LoopListViewItem2>();
		component2.ItemPrefabName = mPrefabName;
		component2.StartPosOffset = mStartPosOffset;
		return component2;
	}

	private void RecycleItemReal(LoopListViewItem2 item)
	{
		((Component)item).gameObject.SetActive(false);
		mPooledItemList.Add(item);
	}

	public void RecycleItem(LoopListViewItem2 item)
	{
		mTmpPooledItemList.Add(item);
	}

	public void ClearTmpRecycledItem()
	{
		int count = mTmpPooledItemList.Count;
		if (count != 0)
		{
			for (int i = 0; i < count; i++)
			{
				RecycleItemReal(mTmpPooledItemList[i]);
			}
			mTmpPooledItemList.Clear();
		}
	}
}
