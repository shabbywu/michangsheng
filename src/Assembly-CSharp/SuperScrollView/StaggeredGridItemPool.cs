using System;
using System.Collections.Generic;
using UnityEngine;

namespace SuperScrollView
{
	// Token: 0x02000A05 RID: 2565
	public class StaggeredGridItemPool
	{
		// Token: 0x06004278 RID: 17016 RVA: 0x001CAAA0 File Offset: 0x001C8CA0
		public void Init(GameObject prefabObj, float padding, int createCount, RectTransform parent)
		{
			this.mPrefabObj = prefabObj;
			this.mPrefabName = this.mPrefabObj.name;
			this.mInitCreateCount = createCount;
			this.mPadding = padding;
			this.mItemParent = parent;
			this.mPrefabObj.SetActive(false);
			for (int i = 0; i < this.mInitCreateCount; i++)
			{
				LoopStaggeredGridViewItem item = this.CreateItem();
				this.RecycleItemReal(item);
			}
		}

		// Token: 0x06004279 RID: 17017 RVA: 0x001CAB08 File Offset: 0x001C8D08
		public LoopStaggeredGridViewItem GetItem()
		{
			StaggeredGridItemPool.mCurItemIdCount++;
			LoopStaggeredGridViewItem loopStaggeredGridViewItem;
			if (this.mTmpPooledItemList.Count > 0)
			{
				int count = this.mTmpPooledItemList.Count;
				loopStaggeredGridViewItem = this.mTmpPooledItemList[count - 1];
				this.mTmpPooledItemList.RemoveAt(count - 1);
				loopStaggeredGridViewItem.gameObject.SetActive(true);
			}
			else
			{
				int count2 = this.mPooledItemList.Count;
				if (count2 == 0)
				{
					loopStaggeredGridViewItem = this.CreateItem();
				}
				else
				{
					loopStaggeredGridViewItem = this.mPooledItemList[count2 - 1];
					this.mPooledItemList.RemoveAt(count2 - 1);
					loopStaggeredGridViewItem.gameObject.SetActive(true);
				}
			}
			loopStaggeredGridViewItem.Padding = this.mPadding;
			loopStaggeredGridViewItem.ItemId = StaggeredGridItemPool.mCurItemIdCount;
			return loopStaggeredGridViewItem;
		}

		// Token: 0x0600427A RID: 17018 RVA: 0x001CABC4 File Offset: 0x001C8DC4
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

		// Token: 0x0600427B RID: 17019 RVA: 0x001CAC10 File Offset: 0x001C8E10
		public LoopStaggeredGridViewItem CreateItem()
		{
			GameObject gameObject = Object.Instantiate<GameObject>(this.mPrefabObj, Vector3.zero, Quaternion.identity, this.mItemParent);
			gameObject.SetActive(true);
			RectTransform component = gameObject.GetComponent<RectTransform>();
			component.localScale = Vector3.one;
			component.anchoredPosition3D = Vector3.zero;
			component.localEulerAngles = Vector3.zero;
			LoopStaggeredGridViewItem component2 = gameObject.GetComponent<LoopStaggeredGridViewItem>();
			component2.ItemPrefabName = this.mPrefabName;
			component2.StartPosOffset = 0f;
			return component2;
		}

		// Token: 0x0600427C RID: 17020 RVA: 0x0002F6A0 File Offset: 0x0002D8A0
		private void RecycleItemReal(LoopStaggeredGridViewItem item)
		{
			item.gameObject.SetActive(false);
			this.mPooledItemList.Add(item);
		}

		// Token: 0x0600427D RID: 17021 RVA: 0x0002F6BA File Offset: 0x0002D8BA
		public void RecycleItem(LoopStaggeredGridViewItem item)
		{
			this.mTmpPooledItemList.Add(item);
		}

		// Token: 0x0600427E RID: 17022 RVA: 0x001CAC84 File Offset: 0x001C8E84
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

		// Token: 0x04003ADC RID: 15068
		private GameObject mPrefabObj;

		// Token: 0x04003ADD RID: 15069
		private string mPrefabName;

		// Token: 0x04003ADE RID: 15070
		private int mInitCreateCount = 1;

		// Token: 0x04003ADF RID: 15071
		private float mPadding;

		// Token: 0x04003AE0 RID: 15072
		private List<LoopStaggeredGridViewItem> mTmpPooledItemList = new List<LoopStaggeredGridViewItem>();

		// Token: 0x04003AE1 RID: 15073
		private List<LoopStaggeredGridViewItem> mPooledItemList = new List<LoopStaggeredGridViewItem>();

		// Token: 0x04003AE2 RID: 15074
		private static int mCurItemIdCount;

		// Token: 0x04003AE3 RID: 15075
		private RectTransform mItemParent;
	}
}
