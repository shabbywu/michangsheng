using System.Collections.Generic;
using UnityEngine;

namespace SuperScrollView;

public class GridItemPool
{
	private GameObject mPrefabObj;

	private string mPrefabName;

	private int mInitCreateCount = 1;

	private List<LoopGridViewItem> mTmpPooledItemList = new List<LoopGridViewItem>();

	private List<LoopGridViewItem> mPooledItemList = new List<LoopGridViewItem>();

	private static int mCurItemIdCount;

	private RectTransform mItemParent;

	public void Init(GameObject prefabObj, int createCount, RectTransform parent)
	{
		mPrefabObj = prefabObj;
		mPrefabName = ((Object)mPrefabObj).name;
		mInitCreateCount = createCount;
		mItemParent = parent;
		mPrefabObj.SetActive(false);
		for (int i = 0; i < mInitCreateCount; i++)
		{
			LoopGridViewItem item = CreateItem();
			RecycleItemReal(item);
		}
	}

	public LoopGridViewItem GetItem()
	{
		mCurItemIdCount++;
		LoopGridViewItem loopGridViewItem = null;
		if (mTmpPooledItemList.Count > 0)
		{
			int count = mTmpPooledItemList.Count;
			loopGridViewItem = mTmpPooledItemList[count - 1];
			mTmpPooledItemList.RemoveAt(count - 1);
			((Component)loopGridViewItem).gameObject.SetActive(true);
		}
		else
		{
			int count2 = mPooledItemList.Count;
			if (count2 == 0)
			{
				loopGridViewItem = CreateItem();
			}
			else
			{
				loopGridViewItem = mPooledItemList[count2 - 1];
				mPooledItemList.RemoveAt(count2 - 1);
				((Component)loopGridViewItem).gameObject.SetActive(true);
			}
		}
		loopGridViewItem.ItemId = mCurItemIdCount;
		return loopGridViewItem;
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

	public LoopGridViewItem CreateItem()
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
		LoopGridViewItem component2 = obj.GetComponent<LoopGridViewItem>();
		component2.ItemPrefabName = mPrefabName;
		return component2;
	}

	private void RecycleItemReal(LoopGridViewItem item)
	{
		((Component)item).gameObject.SetActive(false);
		mPooledItemList.Add(item);
	}

	public void RecycleItem(LoopGridViewItem item)
	{
		item.PrevItem = null;
		item.NextItem = null;
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
