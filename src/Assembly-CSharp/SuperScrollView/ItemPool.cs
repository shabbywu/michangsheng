using System;
using System.Collections.Generic;
using UnityEngine;

namespace SuperScrollView
{
	// Token: 0x020009F8 RID: 2552
	public class ItemPool
	{
		// Token: 0x0600416E RID: 16750 RVA: 0x001C1C30 File Offset: 0x001BFE30
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

		// Token: 0x0600416F RID: 16751 RVA: 0x001C1CA0 File Offset: 0x001BFEA0
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

		// Token: 0x06004170 RID: 16752 RVA: 0x001C1D5C File Offset: 0x001BFF5C
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

		// Token: 0x06004171 RID: 16753 RVA: 0x001C1DA8 File Offset: 0x001BFFA8
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

		// Token: 0x06004172 RID: 16754 RVA: 0x0002EE5F File Offset: 0x0002D05F
		private void RecycleItemReal(LoopListViewItem2 item)
		{
			item.gameObject.SetActive(false);
			this.mPooledItemList.Add(item);
		}

		// Token: 0x06004173 RID: 16755 RVA: 0x0002EE79 File Offset: 0x0002D079
		public void RecycleItem(LoopListViewItem2 item)
		{
			this.mTmpPooledItemList.Add(item);
		}

		// Token: 0x06004174 RID: 16756 RVA: 0x001C1E1C File Offset: 0x001C001C
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

		// Token: 0x04003A23 RID: 14883
		private GameObject mPrefabObj;

		// Token: 0x04003A24 RID: 14884
		private string mPrefabName;

		// Token: 0x04003A25 RID: 14885
		private int mInitCreateCount = 1;

		// Token: 0x04003A26 RID: 14886
		private float mPadding;

		// Token: 0x04003A27 RID: 14887
		private float mStartPosOffset;

		// Token: 0x04003A28 RID: 14888
		private List<LoopListViewItem2> mTmpPooledItemList = new List<LoopListViewItem2>();

		// Token: 0x04003A29 RID: 14889
		private List<LoopListViewItem2> mPooledItemList = new List<LoopListViewItem2>();

		// Token: 0x04003A2A RID: 14890
		private static int mCurItemIdCount;

		// Token: 0x04003A2B RID: 14891
		private RectTransform mItemParent;
	}
}
