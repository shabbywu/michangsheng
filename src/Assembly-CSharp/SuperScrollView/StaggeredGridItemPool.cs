using System;
using System.Collections.Generic;
using UnityEngine;

namespace SuperScrollView
{
	// Token: 0x020006D7 RID: 1751
	public class StaggeredGridItemPool
	{
		// Token: 0x06003856 RID: 14422 RVA: 0x0018310C File Offset: 0x0018130C
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

		// Token: 0x06003857 RID: 14423 RVA: 0x00183174 File Offset: 0x00181374
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

		// Token: 0x06003858 RID: 14424 RVA: 0x00183230 File Offset: 0x00181430
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

		// Token: 0x06003859 RID: 14425 RVA: 0x0018327C File Offset: 0x0018147C
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

		// Token: 0x0600385A RID: 14426 RVA: 0x001832ED File Offset: 0x001814ED
		private void RecycleItemReal(LoopStaggeredGridViewItem item)
		{
			item.gameObject.SetActive(false);
			this.mPooledItemList.Add(item);
		}

		// Token: 0x0600385B RID: 14427 RVA: 0x00183307 File Offset: 0x00181507
		public void RecycleItem(LoopStaggeredGridViewItem item)
		{
			this.mTmpPooledItemList.Add(item);
		}

		// Token: 0x0600385C RID: 14428 RVA: 0x00183318 File Offset: 0x00181518
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

		// Token: 0x040030C4 RID: 12484
		private GameObject mPrefabObj;

		// Token: 0x040030C5 RID: 12485
		private string mPrefabName;

		// Token: 0x040030C6 RID: 12486
		private int mInitCreateCount = 1;

		// Token: 0x040030C7 RID: 12487
		private float mPadding;

		// Token: 0x040030C8 RID: 12488
		private List<LoopStaggeredGridViewItem> mTmpPooledItemList = new List<LoopStaggeredGridViewItem>();

		// Token: 0x040030C9 RID: 12489
		private List<LoopStaggeredGridViewItem> mPooledItemList = new List<LoopStaggeredGridViewItem>();

		// Token: 0x040030CA RID: 12490
		private static int mCurItemIdCount;

		// Token: 0x040030CB RID: 12491
		private RectTransform mItemParent;
	}
}
