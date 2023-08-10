using System.Collections.Generic;
using UnityEngine;

namespace SuperScrollView;

public class StaggeredGridItemPool
{
	private GameObject mPrefabObj;

	private string mPrefabName;

	private int mInitCreateCount = 1;

	private float mPadding;

	private List<LoopStaggeredGridViewItem> mTmpPooledItemList = new List<LoopStaggeredGridViewItem>();

	private List<LoopStaggeredGridViewItem> mPooledItemList = new List<LoopStaggeredGridViewItem>();

	private static int mCurItemIdCount;

	private RectTransform mItemParent;

	public void Init(GameObject prefabObj, float padding, int createCount, RectTransform parent)
	{
		mPrefabObj = prefabObj;
		mPrefabName = ((Object)mPrefabObj).name;
		mInitCreateCount = createCount;
		mPadding = padding;
		mItemParent = parent;
		mPrefabObj.SetActive(false);
		for (int i = 0; i < mInitCreateCount; i++)
		{
			LoopStaggeredGridViewItem item = CreateItem();
			RecycleItemReal(item);
		}
	}

	public LoopStaggeredGridViewItem GetItem()
	{
		mCurItemIdCount++;
		LoopStaggeredGridViewItem loopStaggeredGridViewItem = null;
		if (mTmpPooledItemList.Count > 0)
		{
			int count = mTmpPooledItemList.Count;
			loopStaggeredGridViewItem = mTmpPooledItemList[count - 1];
			mTmpPooledItemList.RemoveAt(count - 1);
			((Component)loopStaggeredGridViewItem).gameObject.SetActive(true);
		}
		else
		{
			int count2 = mPooledItemList.Count;
			if (count2 == 0)
			{
				loopStaggeredGridViewItem = CreateItem();
			}
			else
			{
				loopStaggeredGridViewItem = mPooledItemList[count2 - 1];
				mPooledItemList.RemoveAt(count2 - 1);
				((Component)loopStaggeredGridViewItem).gameObject.SetActive(true);
			}
		}
		loopStaggeredGridViewItem.Padding = mPadding;
		loopStaggeredGridViewItem.ItemId = mCurItemIdCount;
		return loopStaggeredGridViewItem;
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

	public LoopStaggeredGridViewItem CreateItem()
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
		LoopStaggeredGridViewItem component2 = obj.GetComponent<LoopStaggeredGridViewItem>();
		component2.ItemPrefabName = mPrefabName;
		component2.StartPosOffset = 0f;
		return component2;
	}

	private void RecycleItemReal(LoopStaggeredGridViewItem item)
	{
		((Component)item).gameObject.SetActive(false);
		mPooledItemList.Add(item);
	}

	public void RecycleItem(LoopStaggeredGridViewItem item)
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
