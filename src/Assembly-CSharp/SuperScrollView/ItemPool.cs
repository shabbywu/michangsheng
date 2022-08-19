using System;
using System.Collections.Generic;
using UnityEngine;

namespace SuperScrollView
{
	// Token: 0x020006CB RID: 1739
	public class ItemPool
	{
		// Token: 0x0600374E RID: 14158 RVA: 0x00179838 File Offset: 0x00177A38
		public void Init(GameObject prefabObj, float padding, float startPosOffset, int createCount, RectTransform parent)
		{
			this.mPrefabObj = prefabObj;
			this.mPrefabName = this.mPrefabObj.name;
			this.mInitCreateCount = createCount;
			this.mPadding = padding;
			this.mStartPosOffset = startPosOffset;
			this.mItemParent = parent;
			this.mPrefabObj.SetActive(false);
			for (int i = 0; i < this.mInitCreateCount; i++)
			{
				LoopListViewItem2 item = this.CreateItem();
				this.RecycleItemReal(item);
			}
		}

		// Token: 0x0600374F RID: 14159 RVA: 0x001798A8 File Offset: 0x00177AA8
		public LoopListViewItem2 GetItem()
		{
			ItemPool.mCurItemIdCount++;
			LoopListViewItem2 loopListViewItem;
			if (this.mTmpPooledItemList.Count > 0)
			{
				int count = this.mTmpPooledItemList.Count;
				loopListViewItem = this.mTmpPooledItemList[count - 1];
				this.mTmpPooledItemList.RemoveAt(count - 1);
				loopListViewItem.gameObject.SetActive(true);
			}
			else
			{
				int count2 = this.mPooledItemList.Count;
				if (count2 == 0)
				{
					loopListViewItem = this.CreateItem();
				}
				else
				{
					loopListViewItem = this.mPooledItemList[count2 - 1];
					this.mPooledItemList.RemoveAt(count2 - 1);
					loopListViewItem.gameObject.SetActive(true);
				}
			}
			loopListViewItem.Padding = this.mPadding;
			loopListViewItem.ItemId = ItemPool.mCurItemIdCount;
			return loopListViewItem;
		}

		// Token: 0x06003750 RID: 14160 RVA: 0x00179964 File Offset: 0x00177B64
		public void DestroyAllItem()
		{
			this.ClearTmpRecycledItem();
			int count = this.mPooledItemList.Count;
			for (int i = 0; i < count; i++)
			{
				Object.DestroyImmediate(this.mPooledItemList[i].gameObject);
			}
			this.mPooledItemList.Clear();
		}

		// Token: 0x06003751 RID: 14161 RVA: 0x001799B0 File Offset: 0x00177BB0
		public LoopListViewItem2 CreateItem()
		{
			GameObject gameObject = Object.Instantiate<GameObject>(this.mPrefabObj, Vector3.zero, Quaternion.identity, this.mItemParent);
			gameObject.SetActive(true);
			RectTransform component = gameObject.GetComponent<RectTransform>();
			component.localScale = Vector3.one;
			component.anchoredPosition3D = Vector3.zero;
			component.localEulerAngles = Vector3.zero;
			LoopListViewItem2 component2 = gameObject.GetComponent<LoopListViewItem2>();
			component2.ItemPrefabName = this.mPrefabName;
			component2.StartPosOffset = this.mStartPosOffset;
			return component2;
		}

		// Token: 0x06003752 RID: 14162 RVA: 0x00179A22 File Offset: 0x00177C22
		private void RecycleItemReal(LoopListViewItem2 item)
		{
			item.gameObject.SetActive(false);
			this.mPooledItemList.Add(item);
		}

		// Token: 0x06003753 RID: 14163 RVA: 0x00179A3C File Offset: 0x00177C3C
		public void RecycleItem(LoopListViewItem2 item)
		{
			this.mTmpPooledItemList.Add(item);
		}

		// Token: 0x06003754 RID: 14164 RVA: 0x00179A4C File Offset: 0x00177C4C
		public void ClearTmpRecycledItem()
		{
			int count = this.mTmpPooledItemList.Count;
			if (count == 0)
			{
				return;
			}
			for (int i = 0; i < count; i++)
			{
				this.RecycleItemReal(this.mTmpPooledItemList[i]);
			}
			this.mTmpPooledItemList.Clear();
		}

		// Token: 0x04003013 RID: 12307
		private GameObject mPrefabObj;

		// Token: 0x04003014 RID: 12308
		private string mPrefabName;

		// Token: 0x04003015 RID: 12309
		private int mInitCreateCount = 1;

		// Token: 0x04003016 RID: 12310
		private float mPadding;

		// Token: 0x04003017 RID: 12311
		private float mStartPosOffset;

		// Token: 0x04003018 RID: 12312
		private List<LoopListViewItem2> mTmpPooledItemList = new List<LoopListViewItem2>();

		// Token: 0x04003019 RID: 12313
		private List<LoopListViewItem2> mPooledItemList = new List<LoopListViewItem2>();

		// Token: 0x0400301A RID: 12314
		private static int mCurItemIdCount;

		// Token: 0x0400301B RID: 12315
		private RectTransform mItemParent;
	}
}
