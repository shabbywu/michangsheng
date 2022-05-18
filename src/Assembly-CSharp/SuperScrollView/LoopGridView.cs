using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SuperScrollView
{
	// Token: 0x020009F4 RID: 2548
	public class LoopGridView : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IEndDragHandler, IDragHandler
	{
		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x060040F3 RID: 16627 RVA: 0x0002EAB2 File Offset: 0x0002CCB2
		// (set) Token: 0x060040F4 RID: 16628 RVA: 0x0002EABA File Offset: 0x0002CCBA
		public GridItemArrangeType ArrangeType
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

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x060040F5 RID: 16629 RVA: 0x0002EAC3 File Offset: 0x0002CCC3
		public List<GridViewItemPrefabConfData> ItemPrefabDataList
		{
			get
			{
				return this.mItemPrefabDataList;
			}
		}

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x060040F6 RID: 16630 RVA: 0x0002EACB File Offset: 0x0002CCCB
		public int ItemTotalCount
		{
			get
			{
				return this.mItemTotalCount;
			}
		}

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x060040F7 RID: 16631 RVA: 0x0002EAD3 File Offset: 0x0002CCD3
		public RectTransform ContainerTrans
		{
			get
			{
				return this.mContainerTrans;
			}
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x060040F8 RID: 16632 RVA: 0x001BF348 File Offset: 0x001BD548
		public float ViewPortWidth
		{
			get
			{
				return this.mViewPortRectTransform.rect.width;
			}
		}

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x060040F9 RID: 16633 RVA: 0x001BF368 File Offset: 0x001BD568
		public float ViewPortHeight
		{
			get
			{
				return this.mViewPortRectTransform.rect.height;
			}
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x060040FA RID: 16634 RVA: 0x0002EADB File Offset: 0x0002CCDB
		public ScrollRect ScrollRect
		{
			get
			{
				return this.mScrollRect;
			}
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x060040FB RID: 16635 RVA: 0x0002EAE3 File Offset: 0x0002CCE3
		public bool IsDraging
		{
			get
			{
				return this.mIsDraging;
			}
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x060040FC RID: 16636 RVA: 0x0002EAEB File Offset: 0x0002CCEB
		// (set) Token: 0x060040FD RID: 16637 RVA: 0x0002EAF3 File Offset: 0x0002CCF3
		public bool ItemSnapEnable
		{
			get
			{
				return this.mItemSnapEnable;
			}
			set
			{
				this.mItemSnapEnable = value;
			}
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x060040FE RID: 16638 RVA: 0x0002EAFC File Offset: 0x0002CCFC
		// (set) Token: 0x060040FF RID: 16639 RVA: 0x0002EB04 File Offset: 0x0002CD04
		public Vector2 ItemSize
		{
			get
			{
				return this.mItemSize;
			}
			set
			{
				this.SetItemSize(value);
			}
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x06004100 RID: 16640 RVA: 0x0002EB0D File Offset: 0x0002CD0D
		// (set) Token: 0x06004101 RID: 16641 RVA: 0x0002EB15 File Offset: 0x0002CD15
		public Vector2 ItemPadding
		{
			get
			{
				return this.mItemPadding;
			}
			set
			{
				this.SetItemPadding(value);
			}
		}

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x06004102 RID: 16642 RVA: 0x0002EB1E File Offset: 0x0002CD1E
		public Vector2 ItemSizeWithPadding
		{
			get
			{
				return this.mItemSizeWithPadding;
			}
		}

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x06004103 RID: 16643 RVA: 0x0002EB26 File Offset: 0x0002CD26
		// (set) Token: 0x06004104 RID: 16644 RVA: 0x0002EB2E File Offset: 0x0002CD2E
		public RectOffset Padding
		{
			get
			{
				return this.mPadding;
			}
			set
			{
				this.SetPadding(value);
			}
		}

		// Token: 0x06004105 RID: 16645 RVA: 0x001BF388 File Offset: 0x001BD588
		public GridViewItemPrefabConfData GetItemPrefabConfData(string prefabName)
		{
			foreach (GridViewItemPrefabConfData gridViewItemPrefabConfData in this.mItemPrefabDataList)
			{
				if (gridViewItemPrefabConfData.mItemPrefab == null)
				{
					Debug.LogError("A item prefab is null ");
				}
				else if (prefabName == gridViewItemPrefabConfData.mItemPrefab.name)
				{
					return gridViewItemPrefabConfData;
				}
			}
			return null;
		}

		// Token: 0x06004106 RID: 16646 RVA: 0x001BF408 File Offset: 0x001BD608
		public void InitGridView(int itemTotalCount, Func<LoopGridView, int, int, int, LoopGridViewItem> onGetItemByRowColumn, LoopGridViewSettingParam settingParam = null, LoopGridViewInitParam initParam = null)
		{
			if (this.mListViewInited)
			{
				Debug.LogError("LoopGridView.InitListView method can be called only once.");
				return;
			}
			this.mListViewInited = true;
			if (itemTotalCount < 0)
			{
				Debug.LogError("itemTotalCount is  < 0");
				itemTotalCount = 0;
			}
			if (settingParam != null)
			{
				this.UpdateFromSettingParam(settingParam);
			}
			if (initParam != null)
			{
				this.mSmoothDumpRate = initParam.mSmoothDumpRate;
				this.mSnapFinishThreshold = initParam.mSnapFinishThreshold;
				this.mSnapVecThreshold = initParam.mSnapVecThreshold;
			}
			this.mScrollRect = base.gameObject.GetComponent<ScrollRect>();
			if (this.mScrollRect == null)
			{
				Debug.LogError("ListView Init Failed! ScrollRect component not found!");
				return;
			}
			this.mCurSnapData.Clear();
			this.mScrollRectTransform = this.mScrollRect.GetComponent<RectTransform>();
			this.mContainerTrans = this.mScrollRect.content;
			this.mViewPortRectTransform = this.mScrollRect.viewport;
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
			this.SetScrollbarListener();
			this.AdjustViewPortPivot();
			this.AdjustContainerAnchorAndPivot();
			this.InitItemPool();
			this.mOnGetItemByRowColumn = onGetItemByRowColumn;
			this.mNeedCheckContentPosLeftCount = 4;
			this.mCurSnapData.Clear();
			this.mItemTotalCount = itemTotalCount;
			this.UpdateAllGridSetting();
		}

		// Token: 0x06004107 RID: 16647 RVA: 0x001BF58C File Offset: 0x001BD78C
		public void SetListItemCount(int itemCount, bool resetPos = true)
		{
			if (itemCount < 0)
			{
				return;
			}
			if (itemCount == this.mItemTotalCount)
			{
				return;
			}
			this.mCurSnapData.Clear();
			this.mItemTotalCount = itemCount;
			this.UpdateColumnRowCount();
			this.UpdateContentSize();
			this.ForceToCheckContentPos();
			if (this.mItemTotalCount == 0)
			{
				this.RecycleAllItem();
				this.ClearAllTmpRecycledItem();
				return;
			}
			this.VaildAndSetContainerPos();
			this.UpdateGridViewContent();
			this.ClearAllTmpRecycledItem();
			if (resetPos)
			{
				this.MovePanelToItemByRowColumn(0, 0, 0f, 0f);
				return;
			}
		}

		// Token: 0x06004108 RID: 16648 RVA: 0x001BF60C File Offset: 0x001BD80C
		public LoopGridViewItem NewListViewItem(string itemPrefabName)
		{
			GridItemPool gridItemPool = null;
			if (!this.mItemPoolDict.TryGetValue(itemPrefabName, out gridItemPool))
			{
				return null;
			}
			LoopGridViewItem item = gridItemPool.GetItem();
			RectTransform component = item.GetComponent<RectTransform>();
			component.SetParent(this.mContainerTrans);
			component.localScale = Vector3.one;
			component.anchoredPosition3D = Vector3.zero;
			component.localEulerAngles = Vector3.zero;
			item.ParentGridView = this;
			return item;
		}

		// Token: 0x06004109 RID: 16649 RVA: 0x001BF66C File Offset: 0x001BD86C
		public void RefreshItemByItemIndex(int itemIndex)
		{
			if (itemIndex < 0 || itemIndex >= this.ItemTotalCount)
			{
				return;
			}
			if (this.mItemGroupList.Count == 0)
			{
				return;
			}
			RowColumnPair rowColumnByItemIndex = this.GetRowColumnByItemIndex(itemIndex);
			this.RefreshItemByRowColumn(rowColumnByItemIndex.mRow, rowColumnByItemIndex.mColumn);
		}

		// Token: 0x0600410A RID: 16650 RVA: 0x001BF6B0 File Offset: 0x001BD8B0
		public void RefreshItemByRowColumn(int row, int column)
		{
			if (this.mItemGroupList.Count == 0)
			{
				return;
			}
			if (this.mGridFixedType == GridFixedType.ColumnCountFixed)
			{
				GridItemGroup shownGroup = this.GetShownGroup(row);
				if (shownGroup == null)
				{
					return;
				}
				LoopGridViewItem itemByColumn = shownGroup.GetItemByColumn(column);
				if (itemByColumn == null)
				{
					return;
				}
				LoopGridViewItem newItemByRowColumn = this.GetNewItemByRowColumn(row, column);
				if (newItemByRowColumn == null)
				{
					return;
				}
				Vector3 anchoredPosition3D = itemByColumn.CachedRectTransform.anchoredPosition3D;
				shownGroup.ReplaceItem(itemByColumn, newItemByRowColumn);
				this.RecycleItemTmp(itemByColumn);
				newItemByRowColumn.CachedRectTransform.anchoredPosition3D = anchoredPosition3D;
				this.ClearAllTmpRecycledItem();
				return;
			}
			else
			{
				GridItemGroup shownGroup2 = this.GetShownGroup(column);
				if (shownGroup2 == null)
				{
					return;
				}
				LoopGridViewItem itemByRow = shownGroup2.GetItemByRow(row);
				if (itemByRow == null)
				{
					return;
				}
				LoopGridViewItem newItemByRowColumn2 = this.GetNewItemByRowColumn(row, column);
				if (newItemByRowColumn2 == null)
				{
					return;
				}
				Vector3 anchoredPosition3D2 = itemByRow.CachedRectTransform.anchoredPosition3D;
				shownGroup2.ReplaceItem(itemByRow, newItemByRowColumn2);
				this.RecycleItemTmp(itemByRow);
				newItemByRowColumn2.CachedRectTransform.anchoredPosition3D = anchoredPosition3D2;
				this.ClearAllTmpRecycledItem();
				return;
			}
		}

		// Token: 0x0600410B RID: 16651 RVA: 0x0002EB37 File Offset: 0x0002CD37
		public void ClearSnapData()
		{
			this.mCurSnapData.Clear();
		}

		// Token: 0x0600410C RID: 16652 RVA: 0x001BF7A0 File Offset: 0x001BD9A0
		public void SetSnapTargetItemRowColumn(int row, int column)
		{
			if (row < 0)
			{
				row = 0;
			}
			if (column < 0)
			{
				column = 0;
			}
			this.mCurSnapData.mSnapTarget.mRow = row;
			this.mCurSnapData.mSnapTarget.mColumn = column;
			this.mCurSnapData.mSnapStatus = SnapStatus.TargetHasSet;
			this.mCurSnapData.mIsForceSnapTo = true;
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x0600410D RID: 16653 RVA: 0x0002EB44 File Offset: 0x0002CD44
		public RowColumnPair CurSnapNearestItemRowColumn
		{
			get
			{
				return this.mCurSnapNearestItemRowColumn;
			}
		}

		// Token: 0x0600410E RID: 16654 RVA: 0x0002EB4C File Offset: 0x0002CD4C
		public void ForceSnapUpdateCheck()
		{
			if (this.mLeftSnapUpdateExtraCount <= 0)
			{
				this.mLeftSnapUpdateExtraCount = 1;
			}
		}

		// Token: 0x0600410F RID: 16655 RVA: 0x0002EB5E File Offset: 0x0002CD5E
		public void ForceToCheckContentPos()
		{
			if (this.mNeedCheckContentPosLeftCount <= 0)
			{
				this.mNeedCheckContentPosLeftCount = 1;
			}
		}

		// Token: 0x06004110 RID: 16656 RVA: 0x001BF7F8 File Offset: 0x001BD9F8
		public void MovePanelToItemByIndex(int itemIndex, float offsetX = 0f, float offsetY = 0f)
		{
			if (this.ItemTotalCount == 0)
			{
				return;
			}
			if (itemIndex >= this.ItemTotalCount)
			{
				itemIndex = this.ItemTotalCount - 1;
			}
			if (itemIndex < 0)
			{
				itemIndex = 0;
			}
			RowColumnPair rowColumnByItemIndex = this.GetRowColumnByItemIndex(itemIndex);
			this.MovePanelToItemByRowColumn(rowColumnByItemIndex.mRow, rowColumnByItemIndex.mColumn, offsetX, offsetY);
		}

		// Token: 0x06004111 RID: 16657 RVA: 0x001BF844 File Offset: 0x001BDA44
		public void MovePanelToItemByRowColumn(int row, int column, float offsetX = 0f, float offsetY = 0f)
		{
			this.mScrollRect.StopMovement();
			this.mCurSnapData.Clear();
			if (this.mItemTotalCount == 0)
			{
				return;
			}
			Vector2 itemPos = this.GetItemPos(row, column);
			Vector3 anchoredPosition3D = this.mContainerTrans.anchoredPosition3D;
			if (this.mScrollRect.horizontal)
			{
				float num = Mathf.Max(this.ContainerTrans.rect.width - this.ViewPortWidth, 0f);
				if (num > 0f)
				{
					float num2 = -itemPos.x + offsetX;
					num2 = Mathf.Min(Mathf.Abs(num2), num) * Mathf.Sign(num2);
					anchoredPosition3D.x = num2;
				}
			}
			if (this.mScrollRect.vertical)
			{
				float num3 = Mathf.Max(this.ContainerTrans.rect.height - this.ViewPortHeight, 0f);
				if (num3 > 0f)
				{
					float num4 = -itemPos.y + offsetY;
					num4 = Mathf.Min(Mathf.Abs(num4), num3) * Mathf.Sign(num4);
					anchoredPosition3D.y = num4;
				}
			}
			if (anchoredPosition3D != this.mContainerTrans.anchoredPosition3D)
			{
				this.mContainerTrans.anchoredPosition3D = anchoredPosition3D;
			}
			this.VaildAndSetContainerPos();
			this.ForceToCheckContentPos();
		}

		// Token: 0x06004112 RID: 16658 RVA: 0x0002EB70 File Offset: 0x0002CD70
		public void RefreshAllShownItem()
		{
			if (this.mItemGroupList.Count == 0)
			{
				return;
			}
			this.ForceToCheckContentPos();
			this.RecycleAllItem();
			this.UpdateGridViewContent();
		}

		// Token: 0x06004113 RID: 16659 RVA: 0x0002EB92 File Offset: 0x0002CD92
		public virtual void OnBeginDrag(PointerEventData eventData)
		{
			if (eventData.button != null)
			{
				return;
			}
			this.mCurSnapData.Clear();
			this.mIsDraging = true;
			if (this.mOnBeginDragAction != null)
			{
				this.mOnBeginDragAction(eventData);
			}
		}

		// Token: 0x06004114 RID: 16660 RVA: 0x0002EBC3 File Offset: 0x0002CDC3
		public virtual void OnEndDrag(PointerEventData eventData)
		{
			if (eventData.button != null)
			{
				return;
			}
			this.mIsDraging = false;
			this.ForceSnapUpdateCheck();
			if (this.mOnEndDragAction != null)
			{
				this.mOnEndDragAction(eventData);
			}
		}

		// Token: 0x06004115 RID: 16661 RVA: 0x0002EBEF File Offset: 0x0002CDEF
		public virtual void OnDrag(PointerEventData eventData)
		{
			if (eventData.button != null)
			{
				return;
			}
			if (this.mOnDragingAction != null)
			{
				this.mOnDragingAction(eventData);
			}
		}

		// Token: 0x06004116 RID: 16662 RVA: 0x0002EC0E File Offset: 0x0002CE0E
		public int GetItemIndexByRowColumn(int row, int column)
		{
			if (this.mGridFixedType == GridFixedType.ColumnCountFixed)
			{
				return row * this.mFixedRowOrColumnCount + column;
			}
			return column * this.mFixedRowOrColumnCount + row;
		}

		// Token: 0x06004117 RID: 16663 RVA: 0x001BF980 File Offset: 0x001BDB80
		public RowColumnPair GetRowColumnByItemIndex(int itemIndex)
		{
			if (itemIndex < 0)
			{
				itemIndex = 0;
			}
			if (this.mGridFixedType == GridFixedType.ColumnCountFixed)
			{
				int row = itemIndex / this.mFixedRowOrColumnCount;
				int column = itemIndex % this.mFixedRowOrColumnCount;
				return new RowColumnPair(row, column);
			}
			int column2 = itemIndex / this.mFixedRowOrColumnCount;
			return new RowColumnPair(itemIndex % this.mFixedRowOrColumnCount, column2);
		}

		// Token: 0x06004118 RID: 16664 RVA: 0x001BF9CC File Offset: 0x001BDBCC
		public Vector2 GetItemAbsPos(int row, int column)
		{
			float num = this.mStartPadding.x + (float)column * this.mItemSizeWithPadding.x;
			float num2 = this.mStartPadding.y + (float)row * this.mItemSizeWithPadding.y;
			return new Vector2(num, num2);
		}

		// Token: 0x06004119 RID: 16665 RVA: 0x001BFA14 File Offset: 0x001BDC14
		public Vector2 GetItemPos(int row, int column)
		{
			Vector2 itemAbsPos = this.GetItemAbsPos(row, column);
			float x = itemAbsPos.x;
			float y = itemAbsPos.y;
			if (this.ArrangeType == GridItemArrangeType.TopLeftToBottomRight)
			{
				return new Vector2(x, -y);
			}
			if (this.ArrangeType == GridItemArrangeType.BottomLeftToTopRight)
			{
				return new Vector2(x, y);
			}
			if (this.ArrangeType == GridItemArrangeType.TopRightToBottomLeft)
			{
				return new Vector2(-x, -y);
			}
			if (this.ArrangeType == GridItemArrangeType.BottomRightToTopLeft)
			{
				return new Vector2(-x, y);
			}
			return Vector2.zero;
		}

		// Token: 0x0600411A RID: 16666 RVA: 0x001BFA84 File Offset: 0x001BDC84
		public LoopGridViewItem GetShownItemByItemIndex(int itemIndex)
		{
			if (itemIndex < 0 || itemIndex >= this.ItemTotalCount)
			{
				return null;
			}
			if (this.mItemGroupList.Count == 0)
			{
				return null;
			}
			RowColumnPair rowColumnByItemIndex = this.GetRowColumnByItemIndex(itemIndex);
			return this.GetShownItemByRowColumn(rowColumnByItemIndex.mRow, rowColumnByItemIndex.mColumn);
		}

		// Token: 0x0600411B RID: 16667 RVA: 0x001BFACC File Offset: 0x001BDCCC
		public LoopGridViewItem GetShownItemByRowColumn(int row, int column)
		{
			if (this.mItemGroupList.Count == 0)
			{
				return null;
			}
			if (this.mGridFixedType == GridFixedType.ColumnCountFixed)
			{
				GridItemGroup shownGroup = this.GetShownGroup(row);
				if (shownGroup == null)
				{
					return null;
				}
				return shownGroup.GetItemByColumn(column);
			}
			else
			{
				GridItemGroup shownGroup2 = this.GetShownGroup(column);
				if (shownGroup2 == null)
				{
					return null;
				}
				return shownGroup2.GetItemByRow(row);
			}
		}

		// Token: 0x0600411C RID: 16668 RVA: 0x0002EC2D File Offset: 0x0002CE2D
		public void UpdateAllGridSetting()
		{
			this.UpdateStartEndPadding();
			this.UpdateItemSize();
			this.UpdateColumnRowCount();
			this.UpdateContentSize();
			this.ForceSnapUpdateCheck();
			this.ForceToCheckContentPos();
		}

		// Token: 0x0600411D RID: 16669 RVA: 0x001BFB1C File Offset: 0x001BDD1C
		public void SetGridFixedGroupCount(GridFixedType fixedType, int count)
		{
			if (this.mGridFixedType == fixedType && this.mFixedRowOrColumnCount == count)
			{
				return;
			}
			this.mGridFixedType = fixedType;
			this.mFixedRowOrColumnCount = count;
			this.UpdateColumnRowCount();
			this.UpdateContentSize();
			if (this.mItemGroupList.Count == 0)
			{
				return;
			}
			this.RecycleAllItem();
			this.ForceSnapUpdateCheck();
			this.ForceToCheckContentPos();
		}

		// Token: 0x0600411E RID: 16670 RVA: 0x001BFB78 File Offset: 0x001BDD78
		public void SetItemSize(Vector2 newSize)
		{
			if (newSize == this.mItemSize)
			{
				return;
			}
			this.mItemSize = newSize;
			this.UpdateItemSize();
			this.UpdateContentSize();
			if (this.mItemGroupList.Count == 0)
			{
				return;
			}
			this.RecycleAllItem();
			this.ForceSnapUpdateCheck();
			this.ForceToCheckContentPos();
		}

		// Token: 0x0600411F RID: 16671 RVA: 0x001BFBC8 File Offset: 0x001BDDC8
		public void SetItemPadding(Vector2 newPadding)
		{
			if (newPadding == this.mItemPadding)
			{
				return;
			}
			this.mItemPadding = newPadding;
			this.UpdateItemSize();
			this.UpdateContentSize();
			if (this.mItemGroupList.Count == 0)
			{
				return;
			}
			this.RecycleAllItem();
			this.ForceSnapUpdateCheck();
			this.ForceToCheckContentPos();
		}

		// Token: 0x06004120 RID: 16672 RVA: 0x0002EC53 File Offset: 0x0002CE53
		public void SetPadding(RectOffset newPadding)
		{
			if (newPadding == this.mPadding)
			{
				return;
			}
			this.mPadding = newPadding;
			this.UpdateStartEndPadding();
			this.UpdateContentSize();
			if (this.mItemGroupList.Count == 0)
			{
				return;
			}
			this.RecycleAllItem();
			this.ForceSnapUpdateCheck();
			this.ForceToCheckContentPos();
		}

		// Token: 0x06004121 RID: 16673 RVA: 0x001BFC18 File Offset: 0x001BDE18
		public void UpdateContentSize()
		{
			float num = this.mStartPadding.x + (float)this.mColumnCount * this.mItemSizeWithPadding.x - this.mItemPadding.x + this.mEndPadding.x;
			float num2 = this.mStartPadding.y + (float)this.mRowCount * this.mItemSizeWithPadding.y - this.mItemPadding.y + this.mEndPadding.y;
			if (this.mContainerTrans.rect.height != num2)
			{
				this.mContainerTrans.SetSizeWithCurrentAnchors(1, num2);
			}
			if (this.mContainerTrans.rect.width != num)
			{
				this.mContainerTrans.SetSizeWithCurrentAnchors(0, num);
			}
		}

		// Token: 0x06004122 RID: 16674 RVA: 0x001BFCDC File Offset: 0x001BDEDC
		public void VaildAndSetContainerPos()
		{
			Vector3 anchoredPosition3D = this.mContainerTrans.anchoredPosition3D;
			this.mContainerTrans.anchoredPosition3D = this.GetContainerVaildPos(anchoredPosition3D.x, anchoredPosition3D.y);
		}

		// Token: 0x06004123 RID: 16675 RVA: 0x001BFD18 File Offset: 0x001BDF18
		public void ClearAllTmpRecycledItem()
		{
			int count = this.mItemPoolList.Count;
			for (int i = 0; i < count; i++)
			{
				this.mItemPoolList[i].ClearTmpRecycledItem();
			}
		}

		// Token: 0x06004124 RID: 16676 RVA: 0x001BFD50 File Offset: 0x001BDF50
		public void RecycleAllItem()
		{
			foreach (GridItemGroup group in this.mItemGroupList)
			{
				this.RecycleItemGroupTmp(group);
			}
			this.mItemGroupList.Clear();
		}

		// Token: 0x06004125 RID: 16677 RVA: 0x001BFDB0 File Offset: 0x001BDFB0
		public void UpdateGridViewContent()
		{
			this.mListUpdateCheckFrameCount++;
			if (this.mItemTotalCount == 0)
			{
				if (this.mItemGroupList.Count > 0)
				{
					this.RecycleAllItem();
				}
				return;
			}
			this.UpdateCurFrameItemRangeData();
			if (this.mGridFixedType == GridFixedType.ColumnCountFixed)
			{
				int count = this.mItemGroupList.Count;
				int mMinRow = this.mCurFrameItemRangeData.mMinRow;
				int mMaxRow = this.mCurFrameItemRangeData.mMaxRow;
				for (int i = count - 1; i >= 0; i--)
				{
					GridItemGroup gridItemGroup = this.mItemGroupList[i];
					if (gridItemGroup.GroupIndex < mMinRow || gridItemGroup.GroupIndex > mMaxRow)
					{
						this.RecycleItemGroupTmp(gridItemGroup);
						this.mItemGroupList.RemoveAt(i);
					}
				}
				if (this.mItemGroupList.Count == 0)
				{
					GridItemGroup item = this.CreateItemGroup(mMinRow);
					this.mItemGroupList.Add(item);
				}
				while (this.mItemGroupList[0].GroupIndex > mMinRow)
				{
					GridItemGroup item2 = this.CreateItemGroup(this.mItemGroupList[0].GroupIndex - 1);
					this.mItemGroupList.Insert(0, item2);
				}
				while (this.mItemGroupList[this.mItemGroupList.Count - 1].GroupIndex < mMaxRow)
				{
					GridItemGroup item3 = this.CreateItemGroup(this.mItemGroupList[this.mItemGroupList.Count - 1].GroupIndex + 1);
					this.mItemGroupList.Add(item3);
				}
				int count2 = this.mItemGroupList.Count;
				for (int j = 0; j < count2; j++)
				{
					this.UpdateRowItemGroupForRecycleAndNew(this.mItemGroupList[j]);
				}
				return;
			}
			int count3 = this.mItemGroupList.Count;
			int mMinColumn = this.mCurFrameItemRangeData.mMinColumn;
			int mMaxColumn = this.mCurFrameItemRangeData.mMaxColumn;
			for (int k = count3 - 1; k >= 0; k--)
			{
				GridItemGroup gridItemGroup2 = this.mItemGroupList[k];
				if (gridItemGroup2.GroupIndex < mMinColumn || gridItemGroup2.GroupIndex > mMaxColumn)
				{
					this.RecycleItemGroupTmp(gridItemGroup2);
					this.mItemGroupList.RemoveAt(k);
				}
			}
			if (this.mItemGroupList.Count == 0)
			{
				GridItemGroup item4 = this.CreateItemGroup(mMinColumn);
				this.mItemGroupList.Add(item4);
			}
			while (this.mItemGroupList[0].GroupIndex > mMinColumn)
			{
				GridItemGroup item5 = this.CreateItemGroup(this.mItemGroupList[0].GroupIndex - 1);
				this.mItemGroupList.Insert(0, item5);
			}
			while (this.mItemGroupList[this.mItemGroupList.Count - 1].GroupIndex < mMaxColumn)
			{
				GridItemGroup item6 = this.CreateItemGroup(this.mItemGroupList[this.mItemGroupList.Count - 1].GroupIndex + 1);
				this.mItemGroupList.Add(item6);
			}
			int count4 = this.mItemGroupList.Count;
			for (int l = 0; l < count4; l++)
			{
				this.UpdateColumnItemGroupForRecycleAndNew(this.mItemGroupList[l]);
			}
		}

		// Token: 0x06004126 RID: 16678 RVA: 0x001C00A4 File Offset: 0x001BE2A4
		public void UpdateStartEndPadding()
		{
			if (this.ArrangeType == GridItemArrangeType.TopLeftToBottomRight)
			{
				this.mStartPadding.x = (float)this.mPadding.left;
				this.mStartPadding.y = (float)this.mPadding.top;
				this.mEndPadding.x = (float)this.mPadding.right;
				this.mEndPadding.y = (float)this.mPadding.bottom;
				return;
			}
			if (this.ArrangeType == GridItemArrangeType.BottomLeftToTopRight)
			{
				this.mStartPadding.x = (float)this.mPadding.left;
				this.mStartPadding.y = (float)this.mPadding.bottom;
				this.mEndPadding.x = (float)this.mPadding.right;
				this.mEndPadding.y = (float)this.mPadding.top;
				return;
			}
			if (this.ArrangeType == GridItemArrangeType.TopRightToBottomLeft)
			{
				this.mStartPadding.x = (float)this.mPadding.right;
				this.mStartPadding.y = (float)this.mPadding.top;
				this.mEndPadding.x = (float)this.mPadding.left;
				this.mEndPadding.y = (float)this.mPadding.bottom;
				return;
			}
			if (this.ArrangeType == GridItemArrangeType.BottomRightToTopLeft)
			{
				this.mStartPadding.x = (float)this.mPadding.right;
				this.mStartPadding.y = (float)this.mPadding.bottom;
				this.mEndPadding.x = (float)this.mPadding.left;
				this.mEndPadding.y = (float)this.mPadding.top;
			}
		}

		// Token: 0x06004127 RID: 16679 RVA: 0x001C0248 File Offset: 0x001BE448
		public void UpdateItemSize()
		{
			if (this.mItemSize.x > 0f && this.mItemSize.y > 0f)
			{
				this.mItemSizeWithPadding = this.mItemSize + this.mItemPadding;
				return;
			}
			if (this.mItemPrefabDataList.Count != 0)
			{
				GameObject mItemPrefab = this.mItemPrefabDataList[0].mItemPrefab;
				if (!(mItemPrefab == null))
				{
					RectTransform component = mItemPrefab.GetComponent<RectTransform>();
					if (!(component == null))
					{
						this.mItemSize = component.rect.size;
						this.mItemSizeWithPadding = this.mItemSize + this.mItemPadding;
					}
				}
			}
			if (this.mItemSize.x <= 0f || this.mItemSize.y <= 0f)
			{
				Debug.LogError("Error, ItemSize is invaild.");
			}
		}

		// Token: 0x06004128 RID: 16680 RVA: 0x001C0324 File Offset: 0x001BE524
		public void UpdateColumnRowCount()
		{
			if (this.mGridFixedType == GridFixedType.ColumnCountFixed)
			{
				this.mColumnCount = this.mFixedRowOrColumnCount;
				this.mRowCount = this.mItemTotalCount / this.mColumnCount;
				if (this.mItemTotalCount % this.mColumnCount > 0)
				{
					this.mRowCount++;
				}
				if (this.mItemTotalCount <= this.mColumnCount)
				{
					this.mColumnCount = this.mItemTotalCount;
					return;
				}
			}
			else
			{
				this.mRowCount = this.mFixedRowOrColumnCount;
				this.mColumnCount = this.mItemTotalCount / this.mRowCount;
				if (this.mItemTotalCount % this.mRowCount > 0)
				{
					this.mColumnCount++;
				}
				if (this.mItemTotalCount <= this.mRowCount)
				{
					this.mRowCount = this.mItemTotalCount;
				}
			}
		}

		// Token: 0x06004129 RID: 16681 RVA: 0x001C03E8 File Offset: 0x001BE5E8
		private bool IsContainerTransCanMove()
		{
			return this.mItemTotalCount != 0 && ((this.mScrollRect.horizontal && this.ContainerTrans.rect.width > this.ViewPortWidth) || (this.mScrollRect.vertical && this.ContainerTrans.rect.height > this.ViewPortHeight));
		}

		// Token: 0x0600412A RID: 16682 RVA: 0x001C0454 File Offset: 0x001BE654
		private void RecycleItemGroupTmp(GridItemGroup group)
		{
			if (group == null)
			{
				return;
			}
			while (group.First != null)
			{
				LoopGridViewItem item = group.RemoveFirst();
				this.RecycleItemTmp(item);
			}
			group.Clear();
			this.RecycleOneItemGroupObj(group);
		}

		// Token: 0x0600412B RID: 16683 RVA: 0x001C0490 File Offset: 0x001BE690
		private void RecycleItemTmp(LoopGridViewItem item)
		{
			if (item == null)
			{
				return;
			}
			if (string.IsNullOrEmpty(item.ItemPrefabName))
			{
				return;
			}
			GridItemPool gridItemPool = null;
			if (!this.mItemPoolDict.TryGetValue(item.ItemPrefabName, out gridItemPool))
			{
				return;
			}
			gridItemPool.RecycleItem(item);
		}

		// Token: 0x0600412C RID: 16684 RVA: 0x001C04D4 File Offset: 0x001BE6D4
		private void AdjustViewPortPivot()
		{
			RectTransform rectTransform = this.mViewPortRectTransform;
			if (this.ArrangeType == GridItemArrangeType.TopLeftToBottomRight)
			{
				rectTransform.pivot = new Vector2(0f, 1f);
				return;
			}
			if (this.ArrangeType == GridItemArrangeType.BottomLeftToTopRight)
			{
				rectTransform.pivot = new Vector2(0f, 0f);
				return;
			}
			if (this.ArrangeType == GridItemArrangeType.TopRightToBottomLeft)
			{
				rectTransform.pivot = new Vector2(1f, 1f);
				return;
			}
			if (this.ArrangeType == GridItemArrangeType.BottomRightToTopLeft)
			{
				rectTransform.pivot = new Vector2(1f, 0f);
			}
		}

		// Token: 0x0600412D RID: 16685 RVA: 0x001C0564 File Offset: 0x001BE764
		private void AdjustContainerAnchorAndPivot()
		{
			RectTransform containerTrans = this.ContainerTrans;
			if (this.ArrangeType == GridItemArrangeType.TopLeftToBottomRight)
			{
				containerTrans.anchorMin = new Vector2(0f, 1f);
				containerTrans.anchorMax = new Vector2(0f, 1f);
				containerTrans.pivot = new Vector2(0f, 1f);
				return;
			}
			if (this.ArrangeType == GridItemArrangeType.BottomLeftToTopRight)
			{
				containerTrans.anchorMin = new Vector2(0f, 0f);
				containerTrans.anchorMax = new Vector2(0f, 0f);
				containerTrans.pivot = new Vector2(0f, 0f);
				return;
			}
			if (this.ArrangeType == GridItemArrangeType.TopRightToBottomLeft)
			{
				containerTrans.anchorMin = new Vector2(1f, 1f);
				containerTrans.anchorMax = new Vector2(1f, 1f);
				containerTrans.pivot = new Vector2(1f, 1f);
				return;
			}
			if (this.ArrangeType == GridItemArrangeType.BottomRightToTopLeft)
			{
				containerTrans.anchorMin = new Vector2(1f, 0f);
				containerTrans.anchorMax = new Vector2(1f, 0f);
				containerTrans.pivot = new Vector2(1f, 0f);
			}
		}

		// Token: 0x0600412E RID: 16686 RVA: 0x001C069C File Offset: 0x001BE89C
		private void AdjustItemAnchorAndPivot(RectTransform rtf)
		{
			if (this.ArrangeType == GridItemArrangeType.TopLeftToBottomRight)
			{
				rtf.anchorMin = new Vector2(0f, 1f);
				rtf.anchorMax = new Vector2(0f, 1f);
				rtf.pivot = new Vector2(0f, 1f);
				return;
			}
			if (this.ArrangeType == GridItemArrangeType.BottomLeftToTopRight)
			{
				rtf.anchorMin = new Vector2(0f, 0f);
				rtf.anchorMax = new Vector2(0f, 0f);
				rtf.pivot = new Vector2(0f, 0f);
				return;
			}
			if (this.ArrangeType == GridItemArrangeType.TopRightToBottomLeft)
			{
				rtf.anchorMin = new Vector2(1f, 1f);
				rtf.anchorMax = new Vector2(1f, 1f);
				rtf.pivot = new Vector2(1f, 1f);
				return;
			}
			if (this.ArrangeType == GridItemArrangeType.BottomRightToTopLeft)
			{
				rtf.anchorMin = new Vector2(1f, 0f);
				rtf.anchorMax = new Vector2(1f, 0f);
				rtf.pivot = new Vector2(1f, 0f);
			}
		}

		// Token: 0x0600412F RID: 16687 RVA: 0x001C07CC File Offset: 0x001BE9CC
		private void InitItemPool()
		{
			foreach (GridViewItemPrefabConfData gridViewItemPrefabConfData in this.mItemPrefabDataList)
			{
				if (gridViewItemPrefabConfData.mItemPrefab == null)
				{
					Debug.LogError("A item prefab is null ");
				}
				else
				{
					string name = gridViewItemPrefabConfData.mItemPrefab.name;
					if (this.mItemPoolDict.ContainsKey(name))
					{
						Debug.LogError("A item prefab with name " + name + " has existed!");
					}
					else
					{
						RectTransform component = gridViewItemPrefabConfData.mItemPrefab.GetComponent<RectTransform>();
						if (component == null)
						{
							Debug.LogError("RectTransform component is not found in the prefab " + name);
						}
						else
						{
							this.AdjustItemAnchorAndPivot(component);
							if (gridViewItemPrefabConfData.mItemPrefab.GetComponent<LoopGridViewItem>() == null)
							{
								gridViewItemPrefabConfData.mItemPrefab.AddComponent<LoopGridViewItem>();
							}
							GridItemPool gridItemPool = new GridItemPool();
							gridItemPool.Init(gridViewItemPrefabConfData.mItemPrefab, gridViewItemPrefabConfData.mInitCreateCount, this.mContainerTrans);
							this.mItemPoolDict.Add(name, gridItemPool);
							this.mItemPoolList.Add(gridItemPool);
						}
					}
				}
			}
		}

		// Token: 0x06004130 RID: 16688 RVA: 0x001C08F8 File Offset: 0x001BEAF8
		private LoopGridViewItem GetNewItemByRowColumn(int row, int column)
		{
			int itemIndexByRowColumn = this.GetItemIndexByRowColumn(row, column);
			if (itemIndexByRowColumn < 0 || itemIndexByRowColumn >= this.ItemTotalCount)
			{
				return null;
			}
			LoopGridViewItem loopGridViewItem = this.mOnGetItemByRowColumn(this, itemIndexByRowColumn, row, column);
			if (loopGridViewItem == null)
			{
				return null;
			}
			loopGridViewItem.NextItem = null;
			loopGridViewItem.PrevItem = null;
			loopGridViewItem.Row = row;
			loopGridViewItem.Column = column;
			loopGridViewItem.ItemIndex = itemIndexByRowColumn;
			loopGridViewItem.ItemCreatedCheckFrameCount = this.mListUpdateCheckFrameCount;
			return loopGridViewItem;
		}

		// Token: 0x06004131 RID: 16689 RVA: 0x001C0968 File Offset: 0x001BEB68
		private RowColumnPair GetCeilItemRowColumnAtGivenAbsPos(float ax, float ay)
		{
			ax = Mathf.Abs(ax);
			ay = Mathf.Abs(ay);
			int num = Mathf.CeilToInt((ay - this.mStartPadding.y) / this.mItemSizeWithPadding.y) - 1;
			int num2 = Mathf.CeilToInt((ax - this.mStartPadding.x) / this.mItemSizeWithPadding.x) - 1;
			if (num < 0)
			{
				num = 0;
			}
			if (num >= this.mRowCount)
			{
				num = this.mRowCount - 1;
			}
			if (num2 < 0)
			{
				num2 = 0;
			}
			if (num2 >= this.mColumnCount)
			{
				num2 = this.mColumnCount - 1;
			}
			return new RowColumnPair(num, num2);
		}

		// Token: 0x06004132 RID: 16690 RVA: 0x0002EC92 File Offset: 0x0002CE92
		private void Update()
		{
			if (!this.mListViewInited)
			{
				return;
			}
			this.UpdateSnapMove(false, false);
			this.UpdateGridViewContent();
			this.ClearAllTmpRecycledItem();
		}

		// Token: 0x06004133 RID: 16691 RVA: 0x0002ECB1 File Offset: 0x0002CEB1
		private GridItemGroup CreateItemGroup(int groupIndex)
		{
			GridItemGroup oneItemGroupObj = this.GetOneItemGroupObj();
			oneItemGroupObj.GroupIndex = groupIndex;
			return oneItemGroupObj;
		}

		// Token: 0x06004134 RID: 16692 RVA: 0x001C0A00 File Offset: 0x001BEC00
		private Vector2 GetContainerMovedDistance()
		{
			Vector2 containerVaildPos = this.GetContainerVaildPos(this.ContainerTrans.anchoredPosition3D.x, this.ContainerTrans.anchoredPosition3D.y);
			return new Vector2(Mathf.Abs(containerVaildPos.x), Mathf.Abs(containerVaildPos.y));
		}

		// Token: 0x06004135 RID: 16693 RVA: 0x001C0A50 File Offset: 0x001BEC50
		private Vector2 GetContainerVaildPos(float curX, float curY)
		{
			float num = Mathf.Max(this.ContainerTrans.rect.width - this.ViewPortWidth, 0f);
			float num2 = Mathf.Max(this.ContainerTrans.rect.height - this.ViewPortHeight, 0f);
			if (this.mArrangeType == GridItemArrangeType.TopLeftToBottomRight)
			{
				curX = Mathf.Clamp(curX, -num, 0f);
				curY = Mathf.Clamp(curY, 0f, num2);
			}
			else if (this.mArrangeType == GridItemArrangeType.BottomLeftToTopRight)
			{
				curX = Mathf.Clamp(curX, -num, 0f);
				curY = Mathf.Clamp(curY, -num2, 0f);
			}
			else if (this.mArrangeType == GridItemArrangeType.BottomRightToTopLeft)
			{
				curX = Mathf.Clamp(curX, 0f, num);
				curY = Mathf.Clamp(curY, -num2, 0f);
			}
			else if (this.mArrangeType == GridItemArrangeType.TopRightToBottomLeft)
			{
				curX = Mathf.Clamp(curX, 0f, num);
				curY = Mathf.Clamp(curY, 0f, num2);
			}
			return new Vector2(curX, curY);
		}

		// Token: 0x06004136 RID: 16694 RVA: 0x001C0B4C File Offset: 0x001BED4C
		private void UpdateCurFrameItemRangeData()
		{
			Vector2 containerMovedDistance = this.GetContainerMovedDistance();
			if (this.mNeedCheckContentPosLeftCount <= 0 && this.mCurFrameItemRangeData.mCheckedPosition == containerMovedDistance)
			{
				return;
			}
			if (this.mNeedCheckContentPosLeftCount > 0)
			{
				this.mNeedCheckContentPosLeftCount--;
			}
			float num = containerMovedDistance.x - this.mItemRecycleDistance.x;
			float num2 = containerMovedDistance.y - this.mItemRecycleDistance.y;
			if (num < 0f)
			{
				num = 0f;
			}
			if (num2 < 0f)
			{
				num2 = 0f;
			}
			RowColumnPair ceilItemRowColumnAtGivenAbsPos = this.GetCeilItemRowColumnAtGivenAbsPos(num, num2);
			this.mCurFrameItemRangeData.mMinColumn = ceilItemRowColumnAtGivenAbsPos.mColumn;
			this.mCurFrameItemRangeData.mMinRow = ceilItemRowColumnAtGivenAbsPos.mRow;
			num = containerMovedDistance.x + this.mItemRecycleDistance.x + this.ViewPortWidth;
			num2 = containerMovedDistance.y + this.mItemRecycleDistance.y + this.ViewPortHeight;
			ceilItemRowColumnAtGivenAbsPos = this.GetCeilItemRowColumnAtGivenAbsPos(num, num2);
			this.mCurFrameItemRangeData.mMaxColumn = ceilItemRowColumnAtGivenAbsPos.mColumn;
			this.mCurFrameItemRangeData.mMaxRow = ceilItemRowColumnAtGivenAbsPos.mRow;
			this.mCurFrameItemRangeData.mCheckedPosition = containerMovedDistance;
		}

		// Token: 0x06004137 RID: 16695 RVA: 0x001C0C6C File Offset: 0x001BEE6C
		private void UpdateRowItemGroupForRecycleAndNew(GridItemGroup group)
		{
			int mMinColumn = this.mCurFrameItemRangeData.mMinColumn;
			int mMaxColumn = this.mCurFrameItemRangeData.mMaxColumn;
			int groupIndex = group.GroupIndex;
			while (group.First != null)
			{
				if (group.First.Column >= mMinColumn)
				{
					break;
				}
				this.RecycleItemTmp(group.RemoveFirst());
			}
			while (group.Last != null && (group.Last.Column > mMaxColumn || group.Last.ItemIndex >= this.ItemTotalCount))
			{
				this.RecycleItemTmp(group.RemoveLast());
			}
			if (group.First == null)
			{
				LoopGridViewItem newItemByRowColumn = this.GetNewItemByRowColumn(groupIndex, mMinColumn);
				if (newItemByRowColumn == null)
				{
					return;
				}
				newItemByRowColumn.CachedRectTransform.anchoredPosition3D = this.GetItemPos(newItemByRowColumn.Row, newItemByRowColumn.Column);
				group.AddFirst(newItemByRowColumn);
			}
			while (group.First.Column > mMinColumn)
			{
				LoopGridViewItem newItemByRowColumn2 = this.GetNewItemByRowColumn(groupIndex, group.First.Column - 1);
				if (newItemByRowColumn2 == null)
				{
					IL_182:
					while (group.Last.Column < mMaxColumn)
					{
						LoopGridViewItem newItemByRowColumn3 = this.GetNewItemByRowColumn(groupIndex, group.Last.Column + 1);
						if (newItemByRowColumn3 == null)
						{
							break;
						}
						newItemByRowColumn3.CachedRectTransform.anchoredPosition3D = this.GetItemPos(newItemByRowColumn3.Row, newItemByRowColumn3.Column);
						group.AddLast(newItemByRowColumn3);
					}
					return;
				}
				newItemByRowColumn2.CachedRectTransform.anchoredPosition3D = this.GetItemPos(newItemByRowColumn2.Row, newItemByRowColumn2.Column);
				group.AddFirst(newItemByRowColumn2);
			}
			goto IL_182;
		}

		// Token: 0x06004138 RID: 16696 RVA: 0x001C0E0C File Offset: 0x001BF00C
		private void UpdateColumnItemGroupForRecycleAndNew(GridItemGroup group)
		{
			int mMinRow = this.mCurFrameItemRangeData.mMinRow;
			int mMaxRow = this.mCurFrameItemRangeData.mMaxRow;
			int groupIndex = group.GroupIndex;
			while (group.First != null)
			{
				if (group.First.Row >= mMinRow)
				{
					break;
				}
				this.RecycleItemTmp(group.RemoveFirst());
			}
			while (group.Last != null && (group.Last.Row > mMaxRow || group.Last.ItemIndex >= this.ItemTotalCount))
			{
				this.RecycleItemTmp(group.RemoveLast());
			}
			if (group.First == null)
			{
				LoopGridViewItem newItemByRowColumn = this.GetNewItemByRowColumn(mMinRow, groupIndex);
				if (newItemByRowColumn == null)
				{
					return;
				}
				newItemByRowColumn.CachedRectTransform.anchoredPosition3D = this.GetItemPos(newItemByRowColumn.Row, newItemByRowColumn.Column);
				group.AddFirst(newItemByRowColumn);
			}
			while (group.First.Row > mMinRow)
			{
				LoopGridViewItem newItemByRowColumn2 = this.GetNewItemByRowColumn(group.First.Row - 1, groupIndex);
				if (newItemByRowColumn2 == null)
				{
					IL_182:
					while (group.Last.Row < mMaxRow)
					{
						LoopGridViewItem newItemByRowColumn3 = this.GetNewItemByRowColumn(group.Last.Row + 1, groupIndex);
						if (newItemByRowColumn3 == null)
						{
							break;
						}
						newItemByRowColumn3.CachedRectTransform.anchoredPosition3D = this.GetItemPos(newItemByRowColumn3.Row, newItemByRowColumn3.Column);
						group.AddLast(newItemByRowColumn3);
					}
					return;
				}
				newItemByRowColumn2.CachedRectTransform.anchoredPosition3D = this.GetItemPos(newItemByRowColumn2.Row, newItemByRowColumn2.Column);
				group.AddFirst(newItemByRowColumn2);
			}
			goto IL_182;
		}

		// Token: 0x06004139 RID: 16697 RVA: 0x001C0FAC File Offset: 0x001BF1AC
		private void SetScrollbarListener()
		{
			if (!this.ItemSnapEnable)
			{
				return;
			}
			this.mScrollBarClickEventListener1 = null;
			this.mScrollBarClickEventListener2 = null;
			Scrollbar scrollbar = null;
			Scrollbar scrollbar2 = null;
			if (this.mScrollRect.vertical && this.mScrollRect.verticalScrollbar != null)
			{
				scrollbar = this.mScrollRect.verticalScrollbar;
			}
			if (this.mScrollRect.horizontal && this.mScrollRect.horizontalScrollbar != null)
			{
				scrollbar2 = this.mScrollRect.horizontalScrollbar;
			}
			if (scrollbar != null)
			{
				ClickEventListener clickEventListener = ClickEventListener.Get(scrollbar.gameObject);
				this.mScrollBarClickEventListener1 = clickEventListener;
				clickEventListener.SetPointerUpHandler(new Action<GameObject>(this.OnPointerUpInScrollBar));
				clickEventListener.SetPointerDownHandler(new Action<GameObject>(this.OnPointerDownInScrollBar));
			}
			if (scrollbar2 != null)
			{
				ClickEventListener clickEventListener2 = ClickEventListener.Get(scrollbar2.gameObject);
				this.mScrollBarClickEventListener2 = clickEventListener2;
				clickEventListener2.SetPointerUpHandler(new Action<GameObject>(this.OnPointerUpInScrollBar));
				clickEventListener2.SetPointerDownHandler(new Action<GameObject>(this.OnPointerDownInScrollBar));
			}
		}

		// Token: 0x0600413A RID: 16698 RVA: 0x0002EB37 File Offset: 0x0002CD37
		private void OnPointerDownInScrollBar(GameObject obj)
		{
			this.mCurSnapData.Clear();
		}

		// Token: 0x0600413B RID: 16699 RVA: 0x0002ECC0 File Offset: 0x0002CEC0
		private void OnPointerUpInScrollBar(GameObject obj)
		{
			this.ForceSnapUpdateCheck();
		}

		// Token: 0x0600413C RID: 16700 RVA: 0x001C10AC File Offset: 0x001BF2AC
		private RowColumnPair FindNearestItemWithLocalPos(float x, float y)
		{
			Vector2 vector;
			vector..ctor(x, y);
			RowColumnPair ceilItemRowColumnAtGivenAbsPos = this.GetCeilItemRowColumnAtGivenAbsPos(vector.x, vector.y);
			int mRow = ceilItemRowColumnAtGivenAbsPos.mRow;
			int mColumn = ceilItemRowColumnAtGivenAbsPos.mColumn;
			RowColumnPair result = new RowColumnPair(-1, -1);
			Vector2 zero = Vector2.zero;
			float num = float.MaxValue;
			for (int i = mRow - 1; i <= mRow + 1; i++)
			{
				for (int j = mColumn - 1; j <= mColumn + 1; j++)
				{
					if (i >= 0 && i < this.mRowCount && j >= 0 && j < this.mColumnCount)
					{
						float sqrMagnitude = (this.GetItemSnapPivotLocalPos(i, j) - vector).sqrMagnitude;
						if (sqrMagnitude < num)
						{
							num = sqrMagnitude;
							result.mRow = i;
							result.mColumn = j;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600413D RID: 16701 RVA: 0x001C117C File Offset: 0x001BF37C
		private Vector2 GetItemSnapPivotLocalPos(int row, int column)
		{
			Vector2 itemAbsPos = this.GetItemAbsPos(row, column);
			if (this.mArrangeType == GridItemArrangeType.TopLeftToBottomRight)
			{
				float num = itemAbsPos.x + this.mItemSize.x * this.mItemSnapPivot.x;
				float num2 = -itemAbsPos.y - this.mItemSize.y * (1f - this.mItemSnapPivot.y);
				return new Vector2(num, num2);
			}
			if (this.mArrangeType == GridItemArrangeType.BottomLeftToTopRight)
			{
				float num3 = itemAbsPos.x + this.mItemSize.x * this.mItemSnapPivot.x;
				float num4 = itemAbsPos.y + this.mItemSize.y * this.mItemSnapPivot.y;
				return new Vector2(num3, num4);
			}
			if (this.mArrangeType == GridItemArrangeType.TopRightToBottomLeft)
			{
				float num5 = -itemAbsPos.x - this.mItemSize.x * (1f - this.mItemSnapPivot.x);
				float num6 = -itemAbsPos.y - this.mItemSize.y * (1f - this.mItemSnapPivot.y);
				return new Vector2(num5, num6);
			}
			if (this.mArrangeType == GridItemArrangeType.BottomRightToTopLeft)
			{
				float num7 = -itemAbsPos.x - this.mItemSize.x * (1f - this.mItemSnapPivot.x);
				float num8 = itemAbsPos.y + this.mItemSize.y * this.mItemSnapPivot.y;
				return new Vector2(num7, num8);
			}
			return Vector2.zero;
		}

		// Token: 0x0600413E RID: 16702 RVA: 0x001C12E8 File Offset: 0x001BF4E8
		private Vector2 GetViewPortSnapPivotLocalPos(Vector2 pos)
		{
			float num = 0f;
			float num2 = 0f;
			if (this.mArrangeType == GridItemArrangeType.TopLeftToBottomRight)
			{
				num = -pos.x + this.ViewPortWidth * this.mViewPortSnapPivot.x;
				num2 = -pos.y - this.ViewPortHeight * (1f - this.mViewPortSnapPivot.y);
			}
			else if (this.mArrangeType == GridItemArrangeType.BottomLeftToTopRight)
			{
				num = -pos.x + this.ViewPortWidth * this.mViewPortSnapPivot.x;
				num2 = -pos.y + this.ViewPortHeight * this.mViewPortSnapPivot.y;
			}
			else if (this.mArrangeType == GridItemArrangeType.TopRightToBottomLeft)
			{
				num = -pos.x - this.ViewPortWidth * (1f - this.mViewPortSnapPivot.x);
				num2 = -pos.y - this.ViewPortHeight * (1f - this.mViewPortSnapPivot.y);
			}
			else if (this.mArrangeType == GridItemArrangeType.BottomRightToTopLeft)
			{
				num = -pos.x - this.ViewPortWidth * (1f - this.mViewPortSnapPivot.x);
				num2 = -pos.y + this.ViewPortHeight * this.mViewPortSnapPivot.y;
			}
			return new Vector2(num, num2);
		}

		// Token: 0x0600413F RID: 16703 RVA: 0x001C1428 File Offset: 0x001BF628
		private void UpdateNearestSnapItem(bool forceSendEvent)
		{
			if (!this.mItemSnapEnable)
			{
				return;
			}
			if (this.mItemGroupList.Count == 0)
			{
				return;
			}
			if (!this.IsContainerTransCanMove())
			{
				return;
			}
			Vector2 containerVaildPos = this.GetContainerVaildPos(this.ContainerTrans.anchoredPosition3D.x, this.ContainerTrans.anchoredPosition3D.y);
			bool flag = containerVaildPos.y != this.mLastSnapCheckPos.y || containerVaildPos.x != this.mLastSnapCheckPos.x;
			this.mLastSnapCheckPos = containerVaildPos;
			if (!flag && this.mLeftSnapUpdateExtraCount > 0)
			{
				this.mLeftSnapUpdateExtraCount--;
				flag = true;
			}
			if (flag)
			{
				RowColumnPair rowColumnPair = new RowColumnPair(-1, -1);
				Vector2 viewPortSnapPivotLocalPos = this.GetViewPortSnapPivotLocalPos(containerVaildPos);
				rowColumnPair = this.FindNearestItemWithLocalPos(viewPortSnapPivotLocalPos.x, viewPortSnapPivotLocalPos.y);
				if (rowColumnPair.mRow >= 0)
				{
					RowColumnPair a = this.mCurSnapNearestItemRowColumn;
					this.mCurSnapNearestItemRowColumn = rowColumnPair;
					if ((forceSendEvent || a != this.mCurSnapNearestItemRowColumn) && this.mOnSnapNearestChanged != null)
					{
						this.mOnSnapNearestChanged(this);
						return;
					}
				}
				else
				{
					this.mCurSnapNearestItemRowColumn.mRow = -1;
					this.mCurSnapNearestItemRowColumn.mColumn = -1;
				}
			}
		}

		// Token: 0x06004140 RID: 16704 RVA: 0x001C1550 File Offset: 0x001BF750
		private void UpdateFromSettingParam(LoopGridViewSettingParam param)
		{
			if (param == null)
			{
				return;
			}
			if (param.mItemSize != null)
			{
				this.mItemSize = (Vector2)param.mItemSize;
			}
			if (param.mItemPadding != null)
			{
				this.mItemPadding = (Vector2)param.mItemPadding;
			}
			if (param.mPadding != null)
			{
				this.mPadding = (RectOffset)param.mPadding;
			}
			if (param.mGridFixedType != null)
			{
				this.mGridFixedType = (GridFixedType)param.mGridFixedType;
			}
			if (param.mFixedRowOrColumnCount != null)
			{
				this.mFixedRowOrColumnCount = (int)param.mFixedRowOrColumnCount;
			}
		}

		// Token: 0x06004141 RID: 16705 RVA: 0x0002ECC8 File Offset: 0x0002CEC8
		public void FinishSnapImmediately()
		{
			this.UpdateSnapMove(true, false);
		}

		// Token: 0x06004142 RID: 16706 RVA: 0x001C15E0 File Offset: 0x001BF7E0
		private void UpdateSnapMove(bool immediate = false, bool forceSendEvent = false)
		{
			if (!this.mItemSnapEnable)
			{
				return;
			}
			this.UpdateNearestSnapItem(false);
			Vector2 vector = this.mContainerTrans.anchoredPosition3D;
			if (!this.CanSnap())
			{
				this.ClearSnapData();
				return;
			}
			this.UpdateCurSnapData();
			if (this.mCurSnapData.mSnapStatus != SnapStatus.SnapMoving)
			{
				return;
			}
			if (Mathf.Abs(this.mScrollRect.velocity.x) + Mathf.Abs(this.mScrollRect.velocity.y) > 0f)
			{
				this.mScrollRect.StopMovement();
			}
			float mCurSnapVal = this.mCurSnapData.mCurSnapVal;
			this.mCurSnapData.mCurSnapVal = Mathf.SmoothDamp(this.mCurSnapData.mCurSnapVal, this.mCurSnapData.mTargetSnapVal, ref this.mSmoothDumpVel, this.mSmoothDumpRate);
			float num = this.mCurSnapData.mCurSnapVal - mCurSnapVal;
			if (immediate || Mathf.Abs(this.mCurSnapData.mTargetSnapVal - this.mCurSnapData.mCurSnapVal) < this.mSnapFinishThreshold)
			{
				vector += (this.mCurSnapData.mTargetSnapVal - mCurSnapVal) * this.mCurSnapData.mSnapNeedMoveDir;
				this.mCurSnapData.mSnapStatus = SnapStatus.SnapMoveFinish;
				if (this.mOnSnapItemFinished != null)
				{
					LoopGridViewItem shownItemByRowColumn = this.GetShownItemByRowColumn(this.mCurSnapNearestItemRowColumn.mRow, this.mCurSnapNearestItemRowColumn.mColumn);
					if (shownItemByRowColumn != null)
					{
						this.mOnSnapItemFinished(this, shownItemByRowColumn);
					}
				}
			}
			else
			{
				vector += num * this.mCurSnapData.mSnapNeedMoveDir;
			}
			this.mContainerTrans.anchoredPosition3D = this.GetContainerVaildPos(vector.x, vector.y);
		}

		// Token: 0x06004143 RID: 16707 RVA: 0x001C178C File Offset: 0x001BF98C
		private GridItemGroup GetShownGroup(int groupIndex)
		{
			if (groupIndex < 0)
			{
				return null;
			}
			int count = this.mItemGroupList.Count;
			if (count == 0)
			{
				return null;
			}
			if (groupIndex < this.mItemGroupList[0].GroupIndex || groupIndex > this.mItemGroupList[count - 1].GroupIndex)
			{
				return null;
			}
			int index = groupIndex - this.mItemGroupList[0].GroupIndex;
			return this.mItemGroupList[index];
		}

		// Token: 0x06004144 RID: 16708 RVA: 0x001C17FC File Offset: 0x001BF9FC
		private void FillCurSnapData(int row, int column)
		{
			Vector2 itemSnapPivotLocalPos = this.GetItemSnapPivotLocalPos(row, column);
			Vector2 containerVaildPos = this.GetContainerVaildPos(this.ContainerTrans.anchoredPosition3D.x, this.ContainerTrans.anchoredPosition3D.y);
			Vector2 vector = this.GetViewPortSnapPivotLocalPos(containerVaildPos) - itemSnapPivotLocalPos;
			if (!this.mScrollRect.horizontal)
			{
				vector.x = 0f;
			}
			if (!this.mScrollRect.vertical)
			{
				vector.y = 0f;
			}
			this.mCurSnapData.mTargetSnapVal = vector.magnitude;
			this.mCurSnapData.mCurSnapVal = 0f;
			this.mCurSnapData.mSnapNeedMoveDir = vector.normalized;
		}

		// Token: 0x06004145 RID: 16709 RVA: 0x001C18B0 File Offset: 0x001BFAB0
		private void UpdateCurSnapData()
		{
			if (this.mItemGroupList.Count == 0)
			{
				this.mCurSnapData.Clear();
				return;
			}
			if (this.mCurSnapData.mSnapStatus == SnapStatus.SnapMoveFinish)
			{
				if (this.mCurSnapData.mSnapTarget == this.mCurSnapNearestItemRowColumn)
				{
					return;
				}
				this.mCurSnapData.mSnapStatus = SnapStatus.NoTargetSet;
			}
			if (this.mCurSnapData.mSnapStatus == SnapStatus.SnapMoving)
			{
				if (this.mCurSnapData.mSnapTarget == this.mCurSnapNearestItemRowColumn || this.mCurSnapData.mIsForceSnapTo)
				{
					return;
				}
				this.mCurSnapData.mSnapStatus = SnapStatus.NoTargetSet;
			}
			if (this.mCurSnapData.mSnapStatus == SnapStatus.NoTargetSet)
			{
				if (this.GetShownItemByRowColumn(this.mCurSnapNearestItemRowColumn.mRow, this.mCurSnapNearestItemRowColumn.mColumn) == null)
				{
					return;
				}
				this.mCurSnapData.mSnapTarget = this.mCurSnapNearestItemRowColumn;
				this.mCurSnapData.mSnapStatus = SnapStatus.TargetHasSet;
				this.mCurSnapData.mIsForceSnapTo = false;
			}
			if (this.mCurSnapData.mSnapStatus == SnapStatus.TargetHasSet)
			{
				LoopGridViewItem shownItemByRowColumn = this.GetShownItemByRowColumn(this.mCurSnapData.mSnapTarget.mRow, this.mCurSnapData.mSnapTarget.mColumn);
				if (shownItemByRowColumn == null)
				{
					this.mCurSnapData.Clear();
					return;
				}
				this.FillCurSnapData(shownItemByRowColumn.Row, shownItemByRowColumn.Column);
				this.mCurSnapData.mSnapStatus = SnapStatus.SnapMoving;
			}
		}

		// Token: 0x06004146 RID: 16710 RVA: 0x001C1A0C File Offset: 0x001BFC0C
		private bool CanSnap()
		{
			if (this.mIsDraging)
			{
				return false;
			}
			if (this.mScrollBarClickEventListener1 != null && this.mScrollBarClickEventListener1.IsPressd)
			{
				return false;
			}
			if (this.mScrollBarClickEventListener2 != null && this.mScrollBarClickEventListener2.IsPressd)
			{
				return false;
			}
			if (!this.IsContainerTransCanMove())
			{
				return false;
			}
			if (Mathf.Abs(this.mScrollRect.velocity.x) > this.mSnapVecThreshold)
			{
				return false;
			}
			if (Mathf.Abs(this.mScrollRect.velocity.y) > this.mSnapVecThreshold)
			{
				return false;
			}
			Vector3 anchoredPosition3D = this.mContainerTrans.anchoredPosition3D;
			Vector2 containerVaildPos = this.GetContainerVaildPos(anchoredPosition3D.x, anchoredPosition3D.y);
			return Mathf.Abs(anchoredPosition3D.x - containerVaildPos.x) <= 3f && Mathf.Abs(anchoredPosition3D.y - containerVaildPos.y) <= 3f;
		}

		// Token: 0x06004147 RID: 16711 RVA: 0x001C1AFC File Offset: 0x001BFCFC
		private GridItemGroup GetOneItemGroupObj()
		{
			int count = this.mItemGroupObjPool.Count;
			if (count == 0)
			{
				return new GridItemGroup();
			}
			GridItemGroup result = this.mItemGroupObjPool[count - 1];
			this.mItemGroupObjPool.RemoveAt(count - 1);
			return result;
		}

		// Token: 0x06004148 RID: 16712 RVA: 0x0002ECD2 File Offset: 0x0002CED2
		private void RecycleOneItemGroupObj(GridItemGroup obj)
		{
			this.mItemGroupObjPool.Add(obj);
		}

		// Token: 0x040039DA RID: 14810
		private Dictionary<string, GridItemPool> mItemPoolDict = new Dictionary<string, GridItemPool>();

		// Token: 0x040039DB RID: 14811
		private List<GridItemPool> mItemPoolList = new List<GridItemPool>();

		// Token: 0x040039DC RID: 14812
		[SerializeField]
		private List<GridViewItemPrefabConfData> mItemPrefabDataList = new List<GridViewItemPrefabConfData>();

		// Token: 0x040039DD RID: 14813
		[SerializeField]
		private GridItemArrangeType mArrangeType;

		// Token: 0x040039DE RID: 14814
		private RectTransform mContainerTrans;

		// Token: 0x040039DF RID: 14815
		private ScrollRect mScrollRect;

		// Token: 0x040039E0 RID: 14816
		private RectTransform mScrollRectTransform;

		// Token: 0x040039E1 RID: 14817
		private RectTransform mViewPortRectTransform;

		// Token: 0x040039E2 RID: 14818
		private int mItemTotalCount;

		// Token: 0x040039E3 RID: 14819
		[SerializeField]
		private int mFixedRowOrColumnCount;

		// Token: 0x040039E4 RID: 14820
		[SerializeField]
		private RectOffset mPadding = new RectOffset();

		// Token: 0x040039E5 RID: 14821
		[SerializeField]
		private Vector2 mItemPadding = Vector2.zero;

		// Token: 0x040039E6 RID: 14822
		[SerializeField]
		private Vector2 mItemSize = Vector2.zero;

		// Token: 0x040039E7 RID: 14823
		[SerializeField]
		private Vector2 mItemRecycleDistance = new Vector2(50f, 50f);

		// Token: 0x040039E8 RID: 14824
		private Vector2 mItemSizeWithPadding = Vector2.zero;

		// Token: 0x040039E9 RID: 14825
		private Vector2 mStartPadding;

		// Token: 0x040039EA RID: 14826
		private Vector2 mEndPadding;

		// Token: 0x040039EB RID: 14827
		private Func<LoopGridView, int, int, int, LoopGridViewItem> mOnGetItemByRowColumn;

		// Token: 0x040039EC RID: 14828
		private List<GridItemGroup> mItemGroupObjPool = new List<GridItemGroup>();

		// Token: 0x040039ED RID: 14829
		private List<GridItemGroup> mItemGroupList = new List<GridItemGroup>();

		// Token: 0x040039EE RID: 14830
		private bool mIsDraging;

		// Token: 0x040039EF RID: 14831
		private int mRowCount;

		// Token: 0x040039F0 RID: 14832
		private int mColumnCount;

		// Token: 0x040039F1 RID: 14833
		public Action<PointerEventData> mOnBeginDragAction;

		// Token: 0x040039F2 RID: 14834
		public Action<PointerEventData> mOnDragingAction;

		// Token: 0x040039F3 RID: 14835
		public Action<PointerEventData> mOnEndDragAction;

		// Token: 0x040039F4 RID: 14836
		private float mSmoothDumpVel;

		// Token: 0x040039F5 RID: 14837
		private float mSmoothDumpRate = 0.3f;

		// Token: 0x040039F6 RID: 14838
		private float mSnapFinishThreshold = 0.1f;

		// Token: 0x040039F7 RID: 14839
		private float mSnapVecThreshold = 145f;

		// Token: 0x040039F8 RID: 14840
		[SerializeField]
		private bool mItemSnapEnable;

		// Token: 0x040039F9 RID: 14841
		[SerializeField]
		private GridFixedType mGridFixedType;

		// Token: 0x040039FA RID: 14842
		public Action<LoopGridView, LoopGridViewItem> mOnSnapItemFinished;

		// Token: 0x040039FB RID: 14843
		public Action<LoopGridView> mOnSnapNearestChanged;

		// Token: 0x040039FC RID: 14844
		private int mLeftSnapUpdateExtraCount = 1;

		// Token: 0x040039FD RID: 14845
		[SerializeField]
		private Vector2 mViewPortSnapPivot = Vector2.zero;

		// Token: 0x040039FE RID: 14846
		[SerializeField]
		private Vector2 mItemSnapPivot = Vector2.zero;

		// Token: 0x040039FF RID: 14847
		private LoopGridView.SnapData mCurSnapData = new LoopGridView.SnapData();

		// Token: 0x04003A00 RID: 14848
		private Vector3 mLastSnapCheckPos = Vector3.zero;

		// Token: 0x04003A01 RID: 14849
		private bool mListViewInited;

		// Token: 0x04003A02 RID: 14850
		private int mListUpdateCheckFrameCount;

		// Token: 0x04003A03 RID: 14851
		private LoopGridView.ItemRangeData mCurFrameItemRangeData = new LoopGridView.ItemRangeData();

		// Token: 0x04003A04 RID: 14852
		private int mNeedCheckContentPosLeftCount = 1;

		// Token: 0x04003A05 RID: 14853
		private ClickEventListener mScrollBarClickEventListener1;

		// Token: 0x04003A06 RID: 14854
		private ClickEventListener mScrollBarClickEventListener2;

		// Token: 0x04003A07 RID: 14855
		private RowColumnPair mCurSnapNearestItemRowColumn;

		// Token: 0x020009F5 RID: 2549
		private class SnapData
		{
			// Token: 0x0600414A RID: 16714 RVA: 0x0002ECE0 File Offset: 0x0002CEE0
			public void Clear()
			{
				this.mSnapStatus = SnapStatus.NoTargetSet;
				this.mIsForceSnapTo = false;
			}

			// Token: 0x04003A08 RID: 14856
			public SnapStatus mSnapStatus;

			// Token: 0x04003A09 RID: 14857
			public RowColumnPair mSnapTarget;

			// Token: 0x04003A0A RID: 14858
			public Vector2 mSnapNeedMoveDir;

			// Token: 0x04003A0B RID: 14859
			public float mTargetSnapVal;

			// Token: 0x04003A0C RID: 14860
			public float mCurSnapVal;

			// Token: 0x04003A0D RID: 14861
			public bool mIsForceSnapTo;
		}

		// Token: 0x020009F6 RID: 2550
		private class ItemRangeData
		{
			// Token: 0x04003A0E RID: 14862
			public int mMaxRow;

			// Token: 0x04003A0F RID: 14863
			public int mMinRow;

			// Token: 0x04003A10 RID: 14864
			public int mMaxColumn;

			// Token: 0x04003A11 RID: 14865
			public int mMinColumn;

			// Token: 0x04003A12 RID: 14866
			public Vector2 mCheckedPosition;
		}
	}
}
