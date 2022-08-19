using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SuperScrollView
{
	// Token: 0x020006D4 RID: 1748
	public class LoopStaggeredGridView : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IEndDragHandler, IDragHandler
	{
		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x060037D2 RID: 14290 RVA: 0x0017ED28 File Offset: 0x0017CF28
		// (set) Token: 0x060037D3 RID: 14291 RVA: 0x0017ED30 File Offset: 0x0017CF30
		public ListItemArrangeType ArrangeType
		{
			get
			{
				return this.mArrangeType;
			}
			set
			{
				this.mArrangeType = value;
			}
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x060037D4 RID: 14292 RVA: 0x0017ED39 File Offset: 0x0017CF39
		public List<StaggeredGridItemPrefabConfData> ItemPrefabDataList
		{
			get
			{
				return this.mItemPrefabDataList;
			}
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x060037D5 RID: 14293 RVA: 0x0017ED41 File Offset: 0x0017CF41
		public int ListUpdateCheckFrameCount
		{
			get
			{
				return this.mListUpdateCheckFrameCount;
			}
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x060037D6 RID: 14294 RVA: 0x0017ED49 File Offset: 0x0017CF49
		public bool IsVertList
		{
			get
			{
				return this.mIsVertList;
			}
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x060037D7 RID: 14295 RVA: 0x0017ED51 File Offset: 0x0017CF51
		public int ItemTotalCount
		{
			get
			{
				return this.mItemTotalCount;
			}
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x060037D8 RID: 14296 RVA: 0x0017ED59 File Offset: 0x0017CF59
		public RectTransform ContainerTrans
		{
			get
			{
				return this.mContainerTrans;
			}
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x060037D9 RID: 14297 RVA: 0x0017ED61 File Offset: 0x0017CF61
		public ScrollRect ScrollRect
		{
			get
			{
				return this.mScrollRect;
			}
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x060037DA RID: 14298 RVA: 0x0017ED69 File Offset: 0x0017CF69
		public bool IsDraging
		{
			get
			{
				return this.mIsDraging;
			}
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x060037DB RID: 14299 RVA: 0x0017ED71 File Offset: 0x0017CF71
		public GridViewLayoutParam LayoutParam
		{
			get
			{
				return this.mLayoutParam;
			}
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x060037DC RID: 14300 RVA: 0x0017ED79 File Offset: 0x0017CF79
		public bool IsInited
		{
			get
			{
				return this.mListViewInited;
			}
		}

		// Token: 0x060037DD RID: 14301 RVA: 0x0017ED84 File Offset: 0x0017CF84
		public StaggeredGridItemGroup GetItemGroupByIndex(int index)
		{
			int count = this.mItemGroupList.Count;
			if (index < 0 || index >= count)
			{
				return null;
			}
			return this.mItemGroupList[index];
		}

		// Token: 0x060037DE RID: 14302 RVA: 0x0017EDB4 File Offset: 0x0017CFB4
		public StaggeredGridItemPrefabConfData GetItemPrefabConfData(string prefabName)
		{
			foreach (StaggeredGridItemPrefabConfData staggeredGridItemPrefabConfData in this.mItemPrefabDataList)
			{
				if (staggeredGridItemPrefabConfData.mItemPrefab == null)
				{
					Debug.LogError("A item prefab is null ");
				}
				else if (prefabName == staggeredGridItemPrefabConfData.mItemPrefab.name)
				{
					return staggeredGridItemPrefabConfData;
				}
			}
			return null;
		}

		// Token: 0x060037DF RID: 14303 RVA: 0x0017EE34 File Offset: 0x0017D034
		public void InitListView(int itemTotalCount, GridViewLayoutParam layoutParam, Func<LoopStaggeredGridView, int, LoopStaggeredGridViewItem> onGetItemByItemIndex, StaggeredGridViewInitParam initParam = null)
		{
			this.mLayoutParam = layoutParam;
			if (this.mLayoutParam == null)
			{
				Debug.LogError("layoutParam can not be null!");
				return;
			}
			if (!this.mLayoutParam.CheckParam())
			{
				return;
			}
			if (initParam != null)
			{
				this.mDistanceForRecycle0 = initParam.mDistanceForRecycle0;
				this.mDistanceForNew0 = initParam.mDistanceForNew0;
				this.mDistanceForRecycle1 = initParam.mDistanceForRecycle1;
				this.mDistanceForNew1 = initParam.mDistanceForNew1;
				this.mItemDefaultWithPaddingSize = initParam.mItemDefaultWithPaddingSize;
			}
			this.mScrollRect = base.gameObject.GetComponent<ScrollRect>();
			if (this.mScrollRect == null)
			{
				Debug.LogError("LoopStaggeredGridView Init Failed! ScrollRect component not found!");
				return;
			}
			if (this.mDistanceForRecycle0 <= this.mDistanceForNew0)
			{
				Debug.LogError("mDistanceForRecycle0 should be bigger than mDistanceForNew0");
			}
			if (this.mDistanceForRecycle1 <= this.mDistanceForNew1)
			{
				Debug.LogError("mDistanceForRecycle1 should be bigger than mDistanceForNew1");
			}
			this.mScrollRectTransform = this.mScrollRect.GetComponent<RectTransform>();
			this.mContainerTrans = this.mScrollRect.content;
			this.mViewPortRectTransform = this.mScrollRect.viewport;
			this.mGroupCount = this.mLayoutParam.mColumnOrRowCount;
			if (this.mViewPortRectTransform == null)
			{
				this.mViewPortRectTransform = this.mScrollRectTransform;
			}
			if (this.mScrollRect.horizontalScrollbarVisibility == 2 && this.mScrollRect.horizontalScrollbar != null)
			{
				Debug.LogError("ScrollRect.horizontalScrollbarVisibility cannot be set to AutoHideAndExpandViewport");
			}
			if (this.mScrollRect.verticalScrollbarVisibility == 2 && this.mScrollRect.verticalScrollbar != null)
			{
				Debug.LogError("ScrollRect.verticalScrollbarVisibility cannot be set to AutoHideAndExpandViewport");
			}
			this.mIsVertList = (this.mArrangeType == ListItemArrangeType.TopToBottom || this.mArrangeType == ListItemArrangeType.BottomToTop);
			this.mScrollRect.horizontal = !this.mIsVertList;
			this.mScrollRect.vertical = this.mIsVertList;
			this.AdjustPivot(this.mViewPortRectTransform);
			this.AdjustAnchor(this.mContainerTrans);
			this.AdjustContainerPivot(this.mContainerTrans);
			this.InitItemPool();
			this.mOnGetItemByItemIndex = onGetItemByItemIndex;
			if (this.mListViewInited)
			{
				Debug.LogError("LoopStaggeredGridView.InitListView method can be called only once.");
			}
			this.mListViewInited = true;
			this.mViewPortRectTransform.GetLocalCorners(this.mViewPortRectLocalCorners);
			this.mContainerTrans.anchoredPosition3D = Vector3.zero;
			this.mItemTotalCount = itemTotalCount;
			this.UpdateLayoutParamAutoValue();
			this.mItemGroupList.Clear();
			for (int i = 0; i < this.mGroupCount; i++)
			{
				StaggeredGridItemGroup staggeredGridItemGroup = new StaggeredGridItemGroup();
				staggeredGridItemGroup.Init(this, this.mItemTotalCount, i, new Func<int, int, LoopStaggeredGridViewItem>(this.GetNewItemByGroupAndIndex));
				this.mItemGroupList.Add(staggeredGridItemGroup);
			}
			this.UpdateContentSize();
		}

		// Token: 0x060037E0 RID: 14304 RVA: 0x0017F0C4 File Offset: 0x0017D2C4
		public void ResetGridViewLayoutParam(int itemTotalCount, GridViewLayoutParam layoutParam)
		{
			if (!this.mListViewInited)
			{
				Debug.LogError("ResetLayoutParam can not use before LoopStaggeredGridView.InitListView are called!");
				return;
			}
			this.mScrollRect.StopMovement();
			this.SetListItemCount(0, true);
			this.RecycleAllItem();
			this.ClearAllTmpRecycledItem();
			this.mLayoutParam = layoutParam;
			if (this.mLayoutParam == null)
			{
				Debug.LogError("layoutParam can not be null!");
				return;
			}
			if (!this.mLayoutParam.CheckParam())
			{
				return;
			}
			this.mGroupCount = this.mLayoutParam.mColumnOrRowCount;
			this.mViewPortRectTransform.GetLocalCorners(this.mViewPortRectLocalCorners);
			this.mContainerTrans.anchoredPosition3D = Vector3.zero;
			this.mItemTotalCount = itemTotalCount;
			this.UpdateLayoutParamAutoValue();
			this.mItemGroupList.Clear();
			for (int i = 0; i < this.mGroupCount; i++)
			{
				StaggeredGridItemGroup staggeredGridItemGroup = new StaggeredGridItemGroup();
				staggeredGridItemGroup.Init(this, this.mItemTotalCount, i, new Func<int, int, LoopStaggeredGridViewItem>(this.GetNewItemByGroupAndIndex));
				this.mItemGroupList.Add(staggeredGridItemGroup);
			}
			this.UpdateContentSize();
		}

		// Token: 0x060037E1 RID: 14305 RVA: 0x0017F1B8 File Offset: 0x0017D3B8
		private void UpdateLayoutParamAutoValue()
		{
			if (this.mLayoutParam.mCustomColumnOrRowOffsetArray == null)
			{
				this.mLayoutParam.mCustomColumnOrRowOffsetArray = new float[this.mGroupCount];
				float num = this.mLayoutParam.mItemWidthOrHeight * (float)this.mGroupCount;
				float num2;
				if (this.IsVertList)
				{
					num2 = (this.ViewPortWidth - this.mLayoutParam.mPadding1 - this.mLayoutParam.mPadding2 - num) / (float)(this.mGroupCount - 1);
				}
				else
				{
					num2 = (this.ViewPortHeight - this.mLayoutParam.mPadding1 - this.mLayoutParam.mPadding2 - num) / (float)(this.mGroupCount - 1);
				}
				float num3 = this.mLayoutParam.mPadding1;
				for (int i = 0; i < this.mGroupCount; i++)
				{
					if (this.IsVertList)
					{
						this.mLayoutParam.mCustomColumnOrRowOffsetArray[i] = num3;
					}
					else
					{
						this.mLayoutParam.mCustomColumnOrRowOffsetArray[i] = -num3;
					}
					num3 = num3 + this.mLayoutParam.mItemWidthOrHeight + num2;
				}
			}
		}

		// Token: 0x060037E2 RID: 14306 RVA: 0x0017F2BC File Offset: 0x0017D4BC
		public LoopStaggeredGridViewItem NewListViewItem(string itemPrefabName)
		{
			StaggeredGridItemPool staggeredGridItemPool = null;
			if (!this.mItemPoolDict.TryGetValue(itemPrefabName, out staggeredGridItemPool))
			{
				return null;
			}
			LoopStaggeredGridViewItem item = staggeredGridItemPool.GetItem();
			RectTransform component = item.GetComponent<RectTransform>();
			component.SetParent(this.mContainerTrans);
			component.localScale = Vector3.one;
			component.anchoredPosition3D = Vector3.zero;
			component.localEulerAngles = Vector3.zero;
			item.ParentListView = this;
			return item;
		}

		// Token: 0x060037E3 RID: 14307 RVA: 0x0017F31C File Offset: 0x0017D51C
		public void SetListItemCount(int itemCount, bool resetPos = true)
		{
			if (itemCount == this.mItemTotalCount)
			{
				return;
			}
			int count = this.mItemGroupList.Count;
			this.mItemTotalCount = itemCount;
			for (int i = 0; i < count; i++)
			{
				this.mItemGroupList[i].SetListItemCount(this.mItemTotalCount);
			}
			this.UpdateContentSize();
			if (this.mItemTotalCount == 0)
			{
				this.mItemIndexDataList.Clear();
				this.ClearAllTmpRecycledItem();
				return;
			}
			int count2 = this.mItemIndexDataList.Count;
			if (count2 > this.mItemTotalCount)
			{
				this.mItemIndexDataList.RemoveRange(this.mItemTotalCount, count2 - this.mItemTotalCount);
			}
			if (resetPos)
			{
				this.MovePanelToItemIndex(0, 0f);
				return;
			}
			if (count2 > this.mItemTotalCount)
			{
				this.MovePanelToItemIndex(this.mItemTotalCount - 1, 0f);
			}
		}

		// Token: 0x060037E4 RID: 14308 RVA: 0x0017F3E4 File Offset: 0x0017D5E4
		public void MovePanelToItemIndex(int itemIndex, float offset)
		{
			this.mScrollRect.StopMovement();
			if (this.mItemTotalCount == 0 || itemIndex < 0)
			{
				return;
			}
			this.CheckAllGroupIfNeedUpdateItemPos();
			this.UpdateContentSize();
			float viewPortSize = this.ViewPortSize;
			float contentSize = this.GetContentSize();
			if (contentSize <= viewPortSize)
			{
				if (this.IsVertList)
				{
					this.SetAnchoredPositionY(this.mContainerTrans, 0f);
					return;
				}
				this.SetAnchoredPositionX(this.mContainerTrans, 0f);
				return;
			}
			else
			{
				if (itemIndex >= this.mItemTotalCount)
				{
					itemIndex = this.mItemTotalCount - 1;
				}
				float itemAbsPosByItemIndex = this.GetItemAbsPosByItemIndex(itemIndex);
				if (itemAbsPosByItemIndex < 0f)
				{
					return;
				}
				if (this.IsVertList)
				{
					float num = (float)((this.mArrangeType == ListItemArrangeType.TopToBottom) ? 1 : -1);
					float num2 = itemAbsPosByItemIndex + offset;
					if (num2 < 0f)
					{
						num2 = 0f;
					}
					if (contentSize - num2 >= viewPortSize)
					{
						this.SetAnchoredPositionY(this.mContainerTrans, num * num2);
						return;
					}
					this.SetAnchoredPositionY(this.mContainerTrans, num * (contentSize - viewPortSize));
					this.UpdateListView(viewPortSize + 100f, viewPortSize + 100f, viewPortSize, viewPortSize);
					this.ClearAllTmpRecycledItem();
					this.UpdateContentSize();
					contentSize = this.GetContentSize();
					if (contentSize - num2 >= viewPortSize)
					{
						this.SetAnchoredPositionY(this.mContainerTrans, num * num2);
						return;
					}
					this.SetAnchoredPositionY(this.mContainerTrans, num * (contentSize - viewPortSize));
					return;
				}
				else
				{
					float num3 = (float)((this.mArrangeType == ListItemArrangeType.RightToLeft) ? 1 : -1);
					float num4 = itemAbsPosByItemIndex + offset;
					if (num4 < 0f)
					{
						num4 = 0f;
					}
					if (contentSize - num4 >= viewPortSize)
					{
						this.SetAnchoredPositionX(this.mContainerTrans, num3 * num4);
						return;
					}
					this.SetAnchoredPositionX(this.mContainerTrans, num3 * (contentSize - viewPortSize));
					this.UpdateListView(viewPortSize + 100f, viewPortSize + 100f, viewPortSize, viewPortSize);
					this.ClearAllTmpRecycledItem();
					this.UpdateContentSize();
					contentSize = this.GetContentSize();
					if (contentSize - num4 >= viewPortSize)
					{
						this.SetAnchoredPositionX(this.mContainerTrans, num3 * num4);
						return;
					}
					this.SetAnchoredPositionX(this.mContainerTrans, num3 * (contentSize - viewPortSize));
					return;
				}
			}
		}

		// Token: 0x060037E5 RID: 14309 RVA: 0x0017F5C8 File Offset: 0x0017D7C8
		public LoopStaggeredGridViewItem GetShownItemByItemIndex(int itemIndex)
		{
			ItemIndexData itemIndexData = this.GetItemIndexData(itemIndex);
			if (itemIndexData == null)
			{
				return null;
			}
			return this.GetItemGroupByIndex(itemIndexData.mGroupIndex).GetShownItemByIndexInGroup(itemIndexData.mIndexInGroup);
		}

		// Token: 0x060037E6 RID: 14310 RVA: 0x0017F5FC File Offset: 0x0017D7FC
		public void RefreshAllShownItem()
		{
			int count = this.mItemGroupList.Count;
			for (int i = 0; i < count; i++)
			{
				this.mItemGroupList[i].RefreshAllShownItem();
			}
		}

		// Token: 0x060037E7 RID: 14311 RVA: 0x0017F634 File Offset: 0x0017D834
		public void OnItemSizeChanged(int itemIndex)
		{
			ItemIndexData itemIndexData = this.GetItemIndexData(itemIndex);
			if (itemIndexData == null)
			{
				return;
			}
			this.GetItemGroupByIndex(itemIndexData.mGroupIndex).OnItemSizeChanged(itemIndexData.mIndexInGroup);
		}

		// Token: 0x060037E8 RID: 14312 RVA: 0x0017F664 File Offset: 0x0017D864
		public void RefreshItemByItemIndex(int itemIndex)
		{
			ItemIndexData itemIndexData = this.GetItemIndexData(itemIndex);
			if (itemIndexData == null)
			{
				return;
			}
			this.GetItemGroupByIndex(itemIndexData.mGroupIndex).RefreshItemByIndexInGroup(itemIndexData.mIndexInGroup);
		}

		// Token: 0x060037E9 RID: 14313 RVA: 0x0017F694 File Offset: 0x0017D894
		public void ResetListView(bool resetPos = true)
		{
			this.mViewPortRectTransform.GetLocalCorners(this.mViewPortRectLocalCorners);
			if (resetPos)
			{
				this.mContainerTrans.anchoredPosition3D = Vector3.zero;
			}
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x060037EA RID: 14314 RVA: 0x0017F6BC File Offset: 0x0017D8BC
		public float ViewPortSize
		{
			get
			{
				if (this.mIsVertList)
				{
					return this.mViewPortRectTransform.rect.height;
				}
				return this.mViewPortRectTransform.rect.width;
			}
		}

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x060037EB RID: 14315 RVA: 0x0017F6F8 File Offset: 0x0017D8F8
		public float ViewPortWidth
		{
			get
			{
				return this.mViewPortRectTransform.rect.width;
			}
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x060037EC RID: 14316 RVA: 0x0017F718 File Offset: 0x0017D918
		public float ViewPortHeight
		{
			get
			{
				return this.mViewPortRectTransform.rect.height;
			}
		}

		// Token: 0x060037ED RID: 14317 RVA: 0x0017F738 File Offset: 0x0017D938
		public void RecycleAllItem()
		{
			int count = this.mItemGroupList.Count;
			for (int i = 0; i < count; i++)
			{
				this.mItemGroupList[i].RecycleAllItem();
			}
		}

		// Token: 0x060037EE RID: 14318 RVA: 0x0017F770 File Offset: 0x0017D970
		public void RecycleItemTmp(LoopStaggeredGridViewItem item)
		{
			if (item == null)
			{
				return;
			}
			if (string.IsNullOrEmpty(item.ItemPrefabName))
			{
				return;
			}
			StaggeredGridItemPool staggeredGridItemPool = null;
			if (!this.mItemPoolDict.TryGetValue(item.ItemPrefabName, out staggeredGridItemPool))
			{
				return;
			}
			staggeredGridItemPool.RecycleItem(item);
		}

		// Token: 0x060037EF RID: 14319 RVA: 0x0017F7B4 File Offset: 0x0017D9B4
		public void ClearAllTmpRecycledItem()
		{
			int count = this.mItemPoolList.Count;
			for (int i = 0; i < count; i++)
			{
				this.mItemPoolList[i].ClearTmpRecycledItem();
			}
		}

		// Token: 0x060037F0 RID: 14320 RVA: 0x0017F7EC File Offset: 0x0017D9EC
		private void AdjustContainerPivot(RectTransform rtf)
		{
			Vector2 pivot = rtf.pivot;
			if (this.mArrangeType == ListItemArrangeType.BottomToTop)
			{
				pivot.y = 0f;
			}
			else if (this.mArrangeType == ListItemArrangeType.TopToBottom)
			{
				pivot.y = 1f;
			}
			else if (this.mArrangeType == ListItemArrangeType.LeftToRight)
			{
				pivot.x = 0f;
			}
			else if (this.mArrangeType == ListItemArrangeType.RightToLeft)
			{
				pivot.x = 1f;
			}
			rtf.pivot = pivot;
		}

		// Token: 0x060037F1 RID: 14321 RVA: 0x0017F860 File Offset: 0x0017DA60
		private void AdjustPivot(RectTransform rtf)
		{
			Vector2 pivot = rtf.pivot;
			if (this.mArrangeType == ListItemArrangeType.BottomToTop)
			{
				pivot.y = 0f;
			}
			else if (this.mArrangeType == ListItemArrangeType.TopToBottom)
			{
				pivot.y = 1f;
			}
			else if (this.mArrangeType == ListItemArrangeType.LeftToRight)
			{
				pivot.x = 0f;
			}
			else if (this.mArrangeType == ListItemArrangeType.RightToLeft)
			{
				pivot.x = 1f;
			}
			rtf.pivot = pivot;
		}

		// Token: 0x060037F2 RID: 14322 RVA: 0x0017F8D4 File Offset: 0x0017DAD4
		private void AdjustContainerAnchor(RectTransform rtf)
		{
			Vector2 anchorMin = rtf.anchorMin;
			Vector2 anchorMax = rtf.anchorMax;
			if (this.mArrangeType == ListItemArrangeType.BottomToTop)
			{
				anchorMin.y = 0f;
				anchorMax.y = 0f;
			}
			else if (this.mArrangeType == ListItemArrangeType.TopToBottom)
			{
				anchorMin.y = 1f;
				anchorMax.y = 1f;
			}
			else if (this.mArrangeType == ListItemArrangeType.LeftToRight)
			{
				anchorMin.x = 0f;
				anchorMax.x = 0f;
			}
			else if (this.mArrangeType == ListItemArrangeType.RightToLeft)
			{
				anchorMin.x = 1f;
				anchorMax.x = 1f;
			}
			rtf.anchorMin = anchorMin;
			rtf.anchorMax = anchorMax;
		}

		// Token: 0x060037F3 RID: 14323 RVA: 0x0017F988 File Offset: 0x0017DB88
		private void AdjustAnchor(RectTransform rtf)
		{
			Vector2 anchorMin = rtf.anchorMin;
			Vector2 anchorMax = rtf.anchorMax;
			if (this.mArrangeType == ListItemArrangeType.BottomToTop)
			{
				anchorMin.y = 0f;
				anchorMax.y = 0f;
			}
			else if (this.mArrangeType == ListItemArrangeType.TopToBottom)
			{
				anchorMin.y = 1f;
				anchorMax.y = 1f;
			}
			else if (this.mArrangeType == ListItemArrangeType.LeftToRight)
			{
				anchorMin.x = 0f;
				anchorMax.x = 0f;
			}
			else if (this.mArrangeType == ListItemArrangeType.RightToLeft)
			{
				anchorMin.x = 1f;
				anchorMax.x = 1f;
			}
			rtf.anchorMin = anchorMin;
			rtf.anchorMax = anchorMax;
		}

		// Token: 0x060037F4 RID: 14324 RVA: 0x0017FA3C File Offset: 0x0017DC3C
		private void InitItemPool()
		{
			foreach (StaggeredGridItemPrefabConfData staggeredGridItemPrefabConfData in this.mItemPrefabDataList)
			{
				if (staggeredGridItemPrefabConfData.mItemPrefab == null)
				{
					Debug.LogError("A item prefab is null ");
				}
				else
				{
					string name = staggeredGridItemPrefabConfData.mItemPrefab.name;
					if (this.mItemPoolDict.ContainsKey(name))
					{
						Debug.LogError("A item prefab with name " + name + " has existed!");
					}
					else
					{
						RectTransform component = staggeredGridItemPrefabConfData.mItemPrefab.GetComponent<RectTransform>();
						if (component == null)
						{
							Debug.LogError("RectTransform component is not found in the prefab " + name);
						}
						else
						{
							this.AdjustAnchor(component);
							this.AdjustPivot(component);
							if (staggeredGridItemPrefabConfData.mItemPrefab.GetComponent<LoopStaggeredGridViewItem>() == null)
							{
								staggeredGridItemPrefabConfData.mItemPrefab.AddComponent<LoopStaggeredGridViewItem>();
							}
							StaggeredGridItemPool staggeredGridItemPool = new StaggeredGridItemPool();
							staggeredGridItemPool.Init(staggeredGridItemPrefabConfData.mItemPrefab, staggeredGridItemPrefabConfData.mPadding, staggeredGridItemPrefabConfData.mInitCreateCount, this.mContainerTrans);
							this.mItemPoolDict.Add(name, staggeredGridItemPool);
							this.mItemPoolList.Add(staggeredGridItemPool);
						}
					}
				}
			}
		}

		// Token: 0x060037F5 RID: 14325 RVA: 0x0017FB80 File Offset: 0x0017DD80
		public virtual void OnBeginDrag(PointerEventData eventData)
		{
			if (eventData.button != null)
			{
				return;
			}
			this.mIsDraging = true;
			this.CacheDragPointerEventData(eventData);
			if (this.mOnBeginDragAction != null)
			{
				this.mOnBeginDragAction();
			}
		}

		// Token: 0x060037F6 RID: 14326 RVA: 0x0017FBAC File Offset: 0x0017DDAC
		public virtual void OnEndDrag(PointerEventData eventData)
		{
			if (eventData.button != null)
			{
				return;
			}
			this.mIsDraging = false;
			this.mPointerEventData = null;
			if (this.mOnEndDragAction != null)
			{
				this.mOnEndDragAction();
			}
		}

		// Token: 0x060037F7 RID: 14327 RVA: 0x0017FBD8 File Offset: 0x0017DDD8
		public virtual void OnDrag(PointerEventData eventData)
		{
			if (eventData.button != null)
			{
				return;
			}
			this.CacheDragPointerEventData(eventData);
			if (this.mOnDragingAction != null)
			{
				this.mOnDragingAction();
			}
		}

		// Token: 0x060037F8 RID: 14328 RVA: 0x0017FC00 File Offset: 0x0017DE00
		private void CacheDragPointerEventData(PointerEventData eventData)
		{
			if (this.mPointerEventData == null)
			{
				this.mPointerEventData = new PointerEventData(EventSystem.current);
			}
			this.mPointerEventData.button = eventData.button;
			this.mPointerEventData.position = eventData.position;
			this.mPointerEventData.pointerPressRaycast = eventData.pointerPressRaycast;
			this.mPointerEventData.pointerCurrentRaycast = eventData.pointerCurrentRaycast;
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x060037F9 RID: 14329 RVA: 0x0017FC69 File Offset: 0x0017DE69
		public int CurMaxCreatedItemIndexCount
		{
			get
			{
				return this.mItemIndexDataList.Count;
			}
		}

		// Token: 0x060037FA RID: 14330 RVA: 0x0017FC78 File Offset: 0x0017DE78
		private void SetAnchoredPositionX(RectTransform rtf, float x)
		{
			Vector3 anchoredPosition3D = rtf.anchoredPosition3D;
			anchoredPosition3D.x = x;
			rtf.anchoredPosition3D = anchoredPosition3D;
		}

		// Token: 0x060037FB RID: 14331 RVA: 0x0017FC9C File Offset: 0x0017DE9C
		private void SetAnchoredPositionY(RectTransform rtf, float y)
		{
			Vector3 anchoredPosition3D = rtf.anchoredPosition3D;
			anchoredPosition3D.y = y;
			rtf.anchoredPosition3D = anchoredPosition3D;
		}

		// Token: 0x060037FC RID: 14332 RVA: 0x0017FCC0 File Offset: 0x0017DEC0
		public ItemIndexData GetItemIndexData(int itemIndex)
		{
			int count = this.mItemIndexDataList.Count;
			if (itemIndex < 0 || itemIndex >= count)
			{
				return null;
			}
			return this.mItemIndexDataList[itemIndex];
		}

		// Token: 0x060037FD RID: 14333 RVA: 0x0017FCF0 File Offset: 0x0017DEF0
		public void UpdateAllGroupShownItemsPos()
		{
			int count = this.mItemGroupList.Count;
			for (int i = 0; i < count; i++)
			{
				this.mItemGroupList[i].UpdateAllShownItemsPos();
			}
		}

		// Token: 0x060037FE RID: 14334 RVA: 0x0017FD28 File Offset: 0x0017DF28
		private void CheckAllGroupIfNeedUpdateItemPos()
		{
			int count = this.mItemGroupList.Count;
			for (int i = 0; i < count; i++)
			{
				this.mItemGroupList[i].CheckIfNeedUpdateItemPos();
			}
		}

		// Token: 0x060037FF RID: 14335 RVA: 0x0017FD60 File Offset: 0x0017DF60
		public float GetItemAbsPosByItemIndex(int itemIndex)
		{
			if (itemIndex < 0 || itemIndex >= this.mItemIndexDataList.Count)
			{
				return -1f;
			}
			ItemIndexData itemIndexData = this.mItemIndexDataList[itemIndex];
			return this.mItemGroupList[itemIndexData.mGroupIndex].GetItemPos(itemIndexData.mIndexInGroup);
		}

		// Token: 0x06003800 RID: 14336 RVA: 0x0017FDB0 File Offset: 0x0017DFB0
		public LoopStaggeredGridViewItem GetNewItemByGroupAndIndex(int groupIndex, int indexInGroup)
		{
			if (indexInGroup < 0)
			{
				return null;
			}
			if (this.mItemTotalCount == 0)
			{
				return null;
			}
			List<int> itemIndexMap = this.mItemGroupList[groupIndex].ItemIndexMap;
			int count = itemIndexMap.Count;
			if (count > indexInGroup)
			{
				int num = itemIndexMap[indexInGroup];
				LoopStaggeredGridViewItem loopStaggeredGridViewItem = this.mOnGetItemByItemIndex(this, num);
				if (loopStaggeredGridViewItem == null)
				{
					return null;
				}
				loopStaggeredGridViewItem.StartPosOffset = this.mLayoutParam.mCustomColumnOrRowOffsetArray[groupIndex];
				loopStaggeredGridViewItem.ItemIndexInGroup = indexInGroup;
				loopStaggeredGridViewItem.ItemIndex = num;
				loopStaggeredGridViewItem.ItemCreatedCheckFrameCount = this.mListUpdateCheckFrameCount;
				return loopStaggeredGridViewItem;
			}
			else
			{
				if (count != indexInGroup)
				{
					return null;
				}
				int count2 = this.mItemIndexDataList.Count;
				if (count2 >= this.mItemTotalCount)
				{
					return null;
				}
				int num = count2;
				LoopStaggeredGridViewItem loopStaggeredGridViewItem = this.mOnGetItemByItemIndex(this, num);
				if (loopStaggeredGridViewItem == null)
				{
					return null;
				}
				itemIndexMap.Add(num);
				ItemIndexData itemIndexData = new ItemIndexData();
				itemIndexData.mGroupIndex = groupIndex;
				itemIndexData.mIndexInGroup = indexInGroup;
				this.mItemIndexDataList.Add(itemIndexData);
				loopStaggeredGridViewItem.StartPosOffset = this.mLayoutParam.mCustomColumnOrRowOffsetArray[groupIndex];
				loopStaggeredGridViewItem.ItemIndexInGroup = indexInGroup;
				loopStaggeredGridViewItem.ItemIndex = num;
				loopStaggeredGridViewItem.ItemCreatedCheckFrameCount = this.mListUpdateCheckFrameCount;
				return loopStaggeredGridViewItem;
			}
		}

		// Token: 0x06003801 RID: 14337 RVA: 0x0017FED4 File Offset: 0x0017E0D4
		private int GetCurShouldAddNewItemGroupIndex()
		{
			float num = float.MaxValue;
			int count = this.mItemGroupList.Count;
			int result = 0;
			for (int i = 0; i < count; i++)
			{
				float shownItemPosMaxValue = this.mItemGroupList[i].GetShownItemPosMaxValue();
				if (shownItemPosMaxValue < num)
				{
					num = shownItemPosMaxValue;
					result = i;
				}
			}
			return result;
		}

		// Token: 0x06003802 RID: 14338 RVA: 0x0017FF1F File Offset: 0x0017E11F
		public void UpdateListViewWithDefault()
		{
			this.UpdateListView(this.mDistanceForRecycle0, this.mDistanceForRecycle1, this.mDistanceForNew0, this.mDistanceForNew1);
			this.UpdateContentSize();
		}

		// Token: 0x06003803 RID: 14339 RVA: 0x0017FF45 File Offset: 0x0017E145
		private void Update()
		{
			if (!this.mListViewInited)
			{
				return;
			}
			this.UpdateListViewWithDefault();
			this.ClearAllTmpRecycledItem();
			this.mLastFrameContainerPos = this.mContainerTrans.anchoredPosition3D;
		}

		// Token: 0x06003804 RID: 14340 RVA: 0x0017FF70 File Offset: 0x0017E170
		public void UpdateListView(float distanceForRecycle0, float distanceForRecycle1, float distanceForNew0, float distanceForNew1)
		{
			this.mListUpdateCheckFrameCount++;
			bool flag = true;
			int num = 0;
			int num2 = 9999;
			int count = this.mItemGroupList.Count;
			for (int i = 0; i < count; i++)
			{
				this.mItemGroupList[i].UpdateListViewPart1(distanceForRecycle0, distanceForRecycle1, distanceForNew0, distanceForNew1);
			}
			while (flag)
			{
				num++;
				if (num >= num2)
				{
					Debug.LogError("UpdateListView while loop " + num + " times! something is wrong!");
					return;
				}
				int curShouldAddNewItemGroupIndex = this.GetCurShouldAddNewItemGroupIndex();
				flag = this.mItemGroupList[curShouldAddNewItemGroupIndex].UpdateListViewPart2(distanceForRecycle0, distanceForRecycle1, distanceForNew0, distanceForNew1);
			}
		}

		// Token: 0x06003805 RID: 14341 RVA: 0x00180010 File Offset: 0x0017E210
		public float GetContentSize()
		{
			if (this.mIsVertList)
			{
				return this.mContainerTrans.rect.height;
			}
			return this.mContainerTrans.rect.width;
		}

		// Token: 0x06003806 RID: 14342 RVA: 0x0018004C File Offset: 0x0017E24C
		public void UpdateContentSize()
		{
			int count = this.mItemGroupList.Count;
			float num = 0f;
			for (int i = 0; i < count; i++)
			{
				float contentPanelSize = this.mItemGroupList[i].GetContentPanelSize();
				if (contentPanelSize > num)
				{
					num = contentPanelSize;
				}
			}
			if (this.mIsVertList)
			{
				if (this.mContainerTrans.rect.height != num)
				{
					this.mContainerTrans.SetSizeWithCurrentAnchors(1, num);
					return;
				}
			}
			else if (this.mContainerTrans.rect.width != num)
			{
				this.mContainerTrans.SetSizeWithCurrentAnchors(0, num);
			}
		}

		// Token: 0x0400307B RID: 12411
		private Dictionary<string, StaggeredGridItemPool> mItemPoolDict = new Dictionary<string, StaggeredGridItemPool>();

		// Token: 0x0400307C RID: 12412
		private List<StaggeredGridItemPool> mItemPoolList = new List<StaggeredGridItemPool>();

		// Token: 0x0400307D RID: 12413
		[SerializeField]
		private List<StaggeredGridItemPrefabConfData> mItemPrefabDataList = new List<StaggeredGridItemPrefabConfData>();

		// Token: 0x0400307E RID: 12414
		[SerializeField]
		private ListItemArrangeType mArrangeType;

		// Token: 0x0400307F RID: 12415
		private RectTransform mContainerTrans;

		// Token: 0x04003080 RID: 12416
		private ScrollRect mScrollRect;

		// Token: 0x04003081 RID: 12417
		private int mGroupCount;

		// Token: 0x04003082 RID: 12418
		private List<StaggeredGridItemGroup> mItemGroupList = new List<StaggeredGridItemGroup>();

		// Token: 0x04003083 RID: 12419
		private List<ItemIndexData> mItemIndexDataList = new List<ItemIndexData>();

		// Token: 0x04003084 RID: 12420
		private RectTransform mScrollRectTransform;

		// Token: 0x04003085 RID: 12421
		private RectTransform mViewPortRectTransform;

		// Token: 0x04003086 RID: 12422
		private float mItemDefaultWithPaddingSize = 20f;

		// Token: 0x04003087 RID: 12423
		private int mItemTotalCount;

		// Token: 0x04003088 RID: 12424
		private bool mIsVertList;

		// Token: 0x04003089 RID: 12425
		private Func<LoopStaggeredGridView, int, LoopStaggeredGridViewItem> mOnGetItemByItemIndex;

		// Token: 0x0400308A RID: 12426
		private Vector3[] mItemWorldCorners = new Vector3[4];

		// Token: 0x0400308B RID: 12427
		private Vector3[] mViewPortRectLocalCorners = new Vector3[4];

		// Token: 0x0400308C RID: 12428
		private float mDistanceForRecycle0 = 300f;

		// Token: 0x0400308D RID: 12429
		private float mDistanceForNew0 = 200f;

		// Token: 0x0400308E RID: 12430
		private float mDistanceForRecycle1 = 300f;

		// Token: 0x0400308F RID: 12431
		private float mDistanceForNew1 = 200f;

		// Token: 0x04003090 RID: 12432
		private bool mIsDraging;

		// Token: 0x04003091 RID: 12433
		private PointerEventData mPointerEventData;

		// Token: 0x04003092 RID: 12434
		public Action mOnBeginDragAction;

		// Token: 0x04003093 RID: 12435
		public Action mOnDragingAction;

		// Token: 0x04003094 RID: 12436
		public Action mOnEndDragAction;

		// Token: 0x04003095 RID: 12437
		private Vector3 mLastFrameContainerPos = Vector3.zero;

		// Token: 0x04003096 RID: 12438
		private bool mListViewInited;

		// Token: 0x04003097 RID: 12439
		private int mListUpdateCheckFrameCount;

		// Token: 0x04003098 RID: 12440
		private GridViewLayoutParam mLayoutParam;
	}
}
