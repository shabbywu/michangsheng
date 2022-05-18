using System;
using System.Collections.Generic;
using UnityEngine;

namespace SuperScrollView
{
	// Token: 0x020009F0 RID: 2544
	public class GridItemPool
	{
		// Token: 0x060040E7 RID: 16615 RVA: 0x001BF13C File Offset: 0x001BD33C
		public void Init(GameObject prefabObj, int createCount, RectTransform parent)
		{
			this.mPrefabObj = prefabObj;
			this.mPrefabName = this.mPrefabObj.name;
			this.mInitCreateCount = createCount;
			this.mItemParent = parent;
			this.mPrefabObj.SetActive(false);
			for (int i = 0; i < this.mInitCreateCount; i++)
			{
				LoopGridViewItem item = this.CreateItem();
				this.RecycleItemReal(item);
			}
		}

		// Token: 0x060040E8 RID: 16616 RVA: 0x001BF19C File Offset: 0x001BD39C
		public LoopGridViewItem GetItem()
		{
			GridItemPool.mCurItemIdCount++;
			LoopGridViewItem loopGridViewItem;
			if (this.mTmpPooledItemList.Count > 0)
			{
				int count = this.mTmpPooledItemList.Count;
				loopGridViewItem = this.mTmpPooledItemList[count - 1];
				this.mTmpPooledItemList.RemoveAt(count - 1);
				loopGridViewItem.gameObject.SetActive(true);
			}
			else
			{
				int count2 = this.mPooledItemList.Count;
				if (count2 == 0)
				{
					loopGridViewItem = this.CreateItem();
				}
				else
				{
					loopGridViewItem = this.mPooledItemList[count2 - 1];
					this.mPooledItemList.RemoveAt(count2 - 1);
					loopGridViewItem.gameObject.SetActive(true);
				}
			}
			loopGridViewItem.ItemId = GridItemPool.mCurItemIdCount;
			return loopGridViewItem;
		}

		// Token: 0x060040E9 RID: 16617 RVA: 0x001BF24C File Offset: 0x001BD44C
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

		// Token: 0x060040EA RID: 16618 RVA: 0x001BF298 File Offset: 0x001BD498
		public LoopGridViewItem CreateItem()
		{
			GameObject gameObject = Object.Instantiate<GameObject>(this.mPrefabObj, Vector3.zero, Quaternion.identity, this.mItemParent);
			gameObject.SetActive(true);
			RectTransform component = gameObject.GetComponent<RectTransform>();
			component.localScale = Vector3.one;
			component.anchoredPosition3D = Vector3.zero;
			component.localEulerAngles = Vector3.zero;
			LoopGridViewItem component2 = gameObject.GetComponent<LoopGridViewItem>();
			component2.ItemPrefabName = this.mPrefabName;
			return component2;
		}

		// Token: 0x060040EB RID: 16619 RVA: 0x0002EA4C File Offset: 0x0002CC4C
		private void RecycleItemReal(LoopGridViewItem item)
		{
			item.gameObject.SetActive(false);
			this.mPooledItemList.Add(item);
		}

		// Token: 0x060040EC RID: 16620 RVA: 0x0002EA66 File Offset: 0x0002CC66
		public void RecycleItem(LoopGridViewItem item)
		{
			item.PrevItem = null;
			item.NextItem = null;
			this.mTmpPooledItemList.Add(item);
		}

		// Token: 0x060040ED RID: 16621 RVA: 0x001BF300 File Offset: 0x001BD500
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

		// Token: 0x040039C9 RID: 14793
		private GameObject mPrefabObj;

		// Token: 0x040039CA RID: 14794
		private string mPrefabName;

		// Token: 0x040039CB RID: 14795
		private int mInitCreateCount = 1;

		// Token: 0x040039CC RID: 14796
		private List<LoopGridViewItem> mTmpPooledItemList = new List<LoopGridViewItem>();

		// Token: 0x040039CD RID: 14797
		private List<LoopGridViewItem> mPooledItemList = new List<LoopGridViewItem>();

		// Token: 0x040039CE RID: 14798
		private static int mCurItemIdCount;

		// Token: 0x040039CF RID: 14799
		private RectTransform mItemParent;
	}
}
