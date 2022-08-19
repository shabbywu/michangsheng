using System;
using System.Collections.Generic;
using UnityEngine;

namespace SuperScrollView
{
	// Token: 0x020006C5 RID: 1733
	public class GridItemPool
	{
		// Token: 0x060036CA RID: 14026 RVA: 0x00176950 File Offset: 0x00174B50
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

		// Token: 0x060036CB RID: 14027 RVA: 0x001769B0 File Offset: 0x00174BB0
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

		// Token: 0x060036CC RID: 14028 RVA: 0x00176A60 File Offset: 0x00174C60
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

		// Token: 0x060036CD RID: 14029 RVA: 0x00176AAC File Offset: 0x00174CAC
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

		// Token: 0x060036CE RID: 14030 RVA: 0x00176B12 File Offset: 0x00174D12
		private void RecycleItemReal(LoopGridViewItem item)
		{
			item.gameObject.SetActive(false);
			this.mPooledItemList.Add(item);
		}

		// Token: 0x060036CF RID: 14031 RVA: 0x00176B2C File Offset: 0x00174D2C
		public void RecycleItem(LoopGridViewItem item)
		{
			item.PrevItem = null;
			item.NextItem = null;
			this.mTmpPooledItemList.Add(item);
		}

		// Token: 0x060036D0 RID: 14032 RVA: 0x00176B48 File Offset: 0x00174D48
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

		// Token: 0x04002FC4 RID: 12228
		private GameObject mPrefabObj;

		// Token: 0x04002FC5 RID: 12229
		private string mPrefabName;

		// Token: 0x04002FC6 RID: 12230
		private int mInitCreateCount = 1;

		// Token: 0x04002FC7 RID: 12231
		private List<LoopGridViewItem> mTmpPooledItemList = new List<LoopGridViewItem>();

		// Token: 0x04002FC8 RID: 12232
		private List<LoopGridViewItem> mPooledItemList = new List<LoopGridViewItem>();

		// Token: 0x04002FC9 RID: 12233
		private static int mCurItemIdCount;

		// Token: 0x04002FCA RID: 12234
		private RectTransform mItemParent;
	}
}
