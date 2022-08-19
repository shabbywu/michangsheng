using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SuperScrollView
{
	// Token: 0x020006C9 RID: 1737
	public class LoopGridView : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IEndDragHandler, IDragHandler
	{
		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x060036D6 RID: 14038 RVA: 0x00176BBE File Offset: 0x00174DBE
		// (set) Token: 0x060036D7 RID: 14039 RVA: 0x00176BC6 File Offset: 0x00174DC6
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

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x060036D8 RID: 14040 RVA: 0x00176BCF File Offset: 0x00174DCF
		public List<GridViewItemPrefabConfData> ItemPrefabDataList
		{
			get
			{
				return this.mItemPrefabDataList;
			}
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x060036D9 RID: 14041 RVA: 0x00176BD7 File Offset: 0x00174DD7
		public int ItemTotalCount
		{
			get
			{
				return this.mItemTotalCount;
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x060036DA RID: 14042 RVA: 0x00176BDF File Offset: 0x00174DDF
		public RectTransform ContainerTrans
		{
			get
			{
				return this.mContainerTrans;
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x060036DB RID: 14043 RVA: 0x00176BE8 File Offset: 0x00174DE8
		public float ViewPortWidth
		{
			get
			{
				return this.mViewPortRectTransform.rect.width;
			}
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x060036DC RID: 14044 RVA: 0x00176C08 File Offset: 0x00174E08
		public float ViewPortHeight
		{
			get
			{
				return this.mViewPortRectTransform.rect.height;
			}
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x060036DD RID: 14045 RVA: 0x00176C28 File Offset: 0x00174E28
		public ScrollRect ScrollRect
		{
			get
			{
				return this.mScrollRect;
			}
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x060036DE RID: 14046 RVA: 0x00176C30 File Offset: 0x00174E30
		public bool IsDraging
		{
			get
			{
				return this.mIsDraging;
			}
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x060036DF RID: 14047 RVA: 0x00176C38 File Offset: 0x00174E38
		// (set) Token: 0x060036E0 RID: 14048 RVA: 0x00176C40 File Offset: 0x00174E40
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

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x060036E1 RID: 14049 RVA: 0x00176C49 File Offset: 0x00174E49
		// (set) Token: 0x060036E2 RID: 14050 RVA: 0x00176C51 File Offset: 0x00174E51
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

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x060036E3 RID: 14051 RVA: 0x00176C5A File Offset: 0x00174E5A
		// (set) Token: 0x060036E4 RID: 14052 RVA: 0x00176C62 File Offset: 0x00174E62
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

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x060036E5 RID: 14053 RVA: 0x00176C6B File Offset: 0x00174E6B
		public Vector2 ItemSizeWithPadding
		{
			get
			{
				return this.mItemSizeWithPadding;
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x060036E6 RID: 14054 RVA: 0x00176C73 File Offset: 0x00174E73
		// (set) Token: 0x060036E7 RID: 14055 RVA: 0x00176C7B File Offset: 0x00174E7B
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

		// Token: 0x060036E8 RID: 14056 RVA: 0x00176C84 File Offset: 0x00174E84
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

		// Token: 0x060036E9 RID: 14057 RVA: 0x00176D04 File Offset: 0x00174F04
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

		// Token: 0x060036EA RID: 14058 RVA: 0x00176E88 File Offset: 0x00175088
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

		// Token: 0x060036EB RID: 14059 RVA: 0x00176F08 File Offset: 0x00175108
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

		// Token: 0x060036EC RID: 14060 RVA: 0x00176F68 File Offset: 0x00175168
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

		// Token: 0x060036ED RID: 14061 RVA: 0x00176FAC File Offset: 0x001751AC
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

		// Token: 0x060036EE RID: 14062 RVA: 0x0017709B File Offset: 0x0017529B
		public void ClearSnapData()
		{
			this.mCurSnapData.Clear();
		}

		// Token: 0x060036EF RID: 14063 RVA: 0x001770A8 File Offset: 0x001752A8
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

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x060036F0 RID: 14064 RVA: 0x001770FD File Offset: 0x001752FD
		public RowColumnPair CurSnapNearestItemRowColumn
		{
			get
			{
				return this.mCurSnapNearestItemRowColumn;
			}
		}

		// Token: 0x060036F1 RID: 14065 RVA: 0x00177105 File Offset: 0x00175305
		public void ForceSnapUpdateCheck()
		{
			if (this.mLeftSnapUpdateExtraCount <= 0)
			{
				this.mLeftSnapUpdateExtraCount = 1;
			}
		}

		// Token: 0x060036F2 RID: 14066 RVA: 0x00177117 File Offset: 0x00175317
		public void ForceToCheckContentPos()
		{
			if (this.mNeedCheckContentPosLeftCount <= 0)
			{
				this.mNeedCheckContentPosLeftCount = 1;
			}
		}

		// Token: 0x060036F3 RID: 14067 RVA: 0x0017712C File Offset: 0x0017532C
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

		// Token: 0x060036F4 RID: 14068 RVA: 0x00177178 File Offset: 0x00175378
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

		// Token: 0x060036F5 RID: 14069 RVA: 0x001772B2 File Offset: 0x001754B2
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

		// Token: 0x060036F6 RID: 14070 RVA: 0x001772D4 File Offset: 0x001754D4
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

		// Token: 0x060036F7 RID: 14071 RVA: 0x00177305 File Offset: 0x00175505
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

		// Token: 0x060036F8 RID: 14072 RVA: 0x00177331 File Offset: 0x00175531
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

		// Token: 0x060036F9 RID: 14073 RVA: 0x00177350 File Offset: 0x00175550
		public int GetItemIndexByRowColumn(int row, int column)
		{
			if (this.mGridFixedType == GridFixedType.ColumnCountFixed)
			{
				return row * this.mFixedRowOrColumnCount + column;
			}
			return column * this.mFixedRowOrColumnCount + row;
		}

		// Token: 0x060036FA RID: 14074 RVA: 0x00177370 File Offset: 0x00175570
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

		// Token: 0x060036FB RID: 14075 RVA: 0x001773BC File Offset: 0x001755BC
		public Vector2 GetItemAbsPos(int row, int column)
		{
			float num = this.mStartPadding.x + (float)column * this.mItemSizeWithPadding.x;
			float num2 = this.mStartPadding.y + (float)row * this.mItemSizeWithPadding.y;
			return new Vector2(num, num2);
		}

		// Token: 0x060036FC RID: 14076 RVA: 0x00177404 File Offset: 0x00175604
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

		// Token: 0x060036FD RID: 14077 RVA: 0x00177474 File Offset: 0x00175674
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

		// Token: 0x060036FE RID: 14078 RVA: 0x001774BC File Offset: 0x001756BC
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

		// Token: 0x060036FF RID: 14079 RVA: 0x00177509 File Offset: 0x00175709
		public void UpdateAllGridSetting()
		{
			this.UpdateStartEndPadding();
			this.UpdateItemSize();
			this.UpdateColumnRowCount();
			this.UpdateContentSize();
			this.ForceSnapUpdateCheck();
			this.ForceToCheckContentPos();
		}

		// Token: 0x06003700 RID: 14080 RVA: 0x00177530 File Offset: 0x00175730
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

		// Token: 0x06003701 RID: 14081 RVA: 0x0017758C File Offset: 0x0017578C
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

		// Token: 0x06003702 RID: 14082 RVA: 0x001775DC File Offset: 0x001757DC
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

		// Token: 0x06003703 RID: 14083 RVA: 0x0017762B File Offset: 0x0017582B
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

		// Token: 0x06003704 RID: 14084 RVA: 0x0017766C File Offset: 0x0017586C
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

		// Token: 0x06003705 RID: 14085 RVA: 0x00177730 File Offset: 0x00175930
		public void VaildAndSetContainerPos()
		{
			Vector3 anchoredPosition3D = this.mContainerTrans.anchoredPosition3D;
			this.mContainerTrans.anchoredPosition3D = this.GetContainerVaildPos(anchoredPosition3D.x, anchoredPosition3D.y);
		}

		// Token: 0x06003706 RID: 14086 RVA: 0x0017776C File Offset: 0x0017596C
		public void ClearAllTmpRecycledItem()
		{
			int count = this.mItemPoolList.Count;
			for (int i = 0; i < count; i++)
			{
				this.mItemPoolList[i].ClearTmpRecycledItem();
			}
		}

		// Token: 0x06003707 RID: 14087 RVA: 0x001777A4 File Offset: 0x001759A4
		public void RecycleAllItem()
		{
			foreach (GridItemGroup group in this.mItemGroupList)
			{
				this.RecycleItemGroupTmp(group);
			}
			this.mItemGroupList.Clear();
		}

		// Token: 0x06003708 RID: 14088 RVA: 0x00177804 File Offset: 0x00175A04
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

		// Token: 0x06003709 RID: 14089 RVA: 0x00177AF8 File Offset: 0x00175CF8
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

		// Token: 0x0600370A RID: 14090 RVA: 0x00177C9C File Offset: 0x00175E9C
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

		// Token: 0x0600370B RID: 14091 RVA: 0x00177D78 File Offset: 0x00175F78
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

		// Token: 0x0600370C RID: 14092 RVA: 0x00177E3C File Offset: 0x0017603C
		private bool IsContainerTransCanMove()
		{
			return this.mItemTotalCount != 0 && ((this.mScrollRect.horizontal && this.ContainerTrans.rect.width > this.ViewPortWidth) || (this.mScrollRect.vertical && this.ContainerTrans.rect.height > this.ViewPortHeight));
		}

		// Token: 0x0600370D RID: 14093 RVA: 0x00177EA8 File Offset: 0x001760A8
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

		// Token: 0x0600370E RID: 14094 RVA: 0x00177EE4 File Offset: 0x001760E4
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

		// Token: 0x0600370F RID: 14095 RVA: 0x00177F28 File Offset: 0x00176128
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

		// Token: 0x06003710 RID: 14096 RVA: 0x00177FB8 File Offset: 0x001761B8
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

		// Token: 0x06003711 RID: 14097 RVA: 0x001780F0 File Offset: 0x001762F0
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

		// Token: 0x06003712 RID: 14098 RVA: 0x00178220 File Offset: 0x00176420
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

		// Token: 0x06003713 RID: 14099 RVA: 0x0017834C File Offset: 0x0017654C
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

		// Token: 0x06003714 RID: 14100 RVA: 0x001783BC File Offset: 0x001765BC
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

		// Token: 0x06003715 RID: 14101 RVA: 0x00178452 File Offset: 0x00176652
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

		// Token: 0x06003716 RID: 14102 RVA: 0x00178471 File Offset: 0x00176671
		private GridItemGroup CreateItemGroup(int groupIndex)
		{
			GridItemGroup oneItemGroupObj = this.GetOneItemGroupObj();
			oneItemGroupObj.GroupIndex = groupIndex;
			return oneItemGroupObj;
		}

		// Token: 0x06003717 RID: 14103 RVA: 0x00178480 File Offset: 0x00176680
		private Vector2 GetContainerMovedDistance()
		{
			Vector2 containerVaildPos = this.GetContainerVaildPos(this.ContainerTrans.anchoredPosition3D.x, this.ContainerTrans.anchoredPosition3D.y);
			return new Vector2(Mathf.Abs(containerVaildPos.x), Mathf.Abs(containerVaildPos.y));
		}

		// Token: 0x06003718 RID: 14104 RVA: 0x001784D0 File Offset: 0x001766D0
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

		// Token: 0x06003719 RID: 14105 RVA: 0x001785CC File Offset: 0x001767CC
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

		// Token: 0x0600371A RID: 14106 RVA: 0x001786EC File Offset: 0x001768EC
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

		// Token: 0x0600371B RID: 14107 RVA: 0x0017888C File Offset: 0x00176A8C
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

		// Token: 0x0600371C RID: 14108 RVA: 0x00178A2C File Offset: 0x00176C2C
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

		// Token: 0x0600371D RID: 14109 RVA: 0x0017709B File Offset: 0x0017529B
		private void OnPointerDownInScrollBar(GameObject obj)
		{
			this.mCurSnapData.Clear();
		}

		// Token: 0x0600371E RID: 14110 RVA: 0x00178B2C File Offset: 0x00176D2C
		private void OnPointerUpInScrollBar(GameObject obj)
		{
			this.ForceSnapUpdateCheck();
		}

		// Token: 0x0600371F RID: 14111 RVA: 0x00178B34 File Offset: 0x00176D34
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

		// Token: 0x06003720 RID: 14112 RVA: 0x00178C04 File Offset: 0x00176E04
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

		// Token: 0x06003721 RID: 14113 RVA: 0x00178D70 File Offset: 0x00176F70
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

		// Token: 0x06003722 RID: 14114 RVA: 0x00178EB0 File Offset: 0x001770B0
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

		// Token: 0x06003723 RID: 14115 RVA: 0x00178FD8 File Offset: 0x001771D8
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

		// Token: 0x06003724 RID: 14116 RVA: 0x00179066 File Offset: 0x00177266
		public void FinishSnapImmediately()
		{
			this.UpdateSnapMove(true, false);
		}

		// Token: 0x06003725 RID: 14117 RVA: 0x00179070 File Offset: 0x00177270
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

		// Token: 0x06003726 RID: 14118 RVA: 0x0017921C File Offset: 0x0017741C
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

		// Token: 0x06003727 RID: 14119 RVA: 0x0017928C File Offset: 0x0017748C
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

		// Token: 0x06003728 RID: 14120 RVA: 0x00179340 File Offset: 0x00177540
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

		// Token: 0x06003729 RID: 14121 RVA: 0x0017949C File Offset: 0x0017769C
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

		// Token: 0x0600372A RID: 14122 RVA: 0x0017958C File Offset: 0x0017778C
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

		// Token: 0x0600372B RID: 14123 RVA: 0x001795CA File Offset: 0x001777CA
		private void RecycleOneItemGroupObj(GridItemGroup obj)
		{
			this.mItemGroupObjPool.Add(obj);
		}

		// Token: 0x04002FD5 RID: 12245
		private Dictionary<string, GridItemPool> mItemPoolDict = new Dictionary<string, GridItemPool>();

		// Token: 0x04002FD6 RID: 12246
		private List<GridItemPool> mItemPoolList = new List<GridItemPool>();

		// Token: 0x04002FD7 RID: 12247
		[SerializeField]
		private List<GridViewItemPrefabConfData> mItemPrefabDataList = new List<GridViewItemPrefabConfData>();

		// Token: 0x04002FD8 RID: 12248
		[SerializeField]
		private GridItemArrangeType mArrangeType;

		// Token: 0x04002FD9 RID: 12249
		private RectTransform mContainerTrans;

		// Token: 0x04002FDA RID: 12250
		private ScrollRect mScrollRect;

		// Token: 0x04002FDB RID: 12251
		private RectTransform mScrollRectTransform;

		// Token: 0x04002FDC RID: 12252
		private RectTransform mViewPortRectTransform;

		// Token: 0x04002FDD RID: 12253
		private int mItemTotalCount;

		// Token: 0x04002FDE RID: 12254
		[SerializeField]
		private int mFixedRowOrColumnCount;

		// Token: 0x04002FDF RID: 12255
		[SerializeField]
		private RectOffset mPadding = new RectOffset();

		// Token: 0x04002FE0 RID: 12256
		[SerializeField]
		private Vector2 mItemPadding = Vector2.zero;

		// Token: 0x04002FE1 RID: 12257
		[SerializeField]
		private Vector2 mItemSize = Vector2.zero;

		// Token: 0x04002FE2 RID: 12258
		[SerializeField]
		private Vector2 mItemRecycleDistance = new Vector2(50f, 50f);

		// Token: 0x04002FE3 RID: 12259
		private Vector2 mItemSizeWithPadding = Vector2.zero;

		// Token: 0x04002FE4 RID: 12260
		private Vector2 mStartPadding;

		// Token: 0x04002FE5 RID: 12261
		private Vector2 mEndPadding;

		// Token: 0x04002FE6 RID: 12262
		private Func<LoopGridView, int, int, int, LoopGridViewItem> mOnGetItemByRowColumn;

		// Token: 0x04002FE7 RID: 12263
		private List<GridItemGroup> mItemGroupObjPool = new List<GridItemGroup>();

		// Token: 0x04002FE8 RID: 12264
		private List<GridItemGroup> mItemGroupList = new List<GridItemGroup>();

		// Token: 0x04002FE9 RID: 12265
		private bool mIsDraging;

		// Token: 0x04002FEA RID: 12266
		private int mRowCount;

		// Token: 0x04002FEB RID: 12267
		private int mColumnCount;

		// Token: 0x04002FEC RID: 12268
		public Action<PointerEventData> mOnBeginDragAction;

		// Token: 0x04002FED RID: 12269
		public Action<PointerEventData> mOnDragingAction;

		// Token: 0x04002FEE RID: 12270
		public Action<PointerEventData> mOnEndDragAction;

		// Token: 0x04002FEF RID: 12271
		private float mSmoothDumpVel;

		// Token: 0x04002FF0 RID: 12272
		private float mSmoothDumpRate = 0.3f;

		// Token: 0x04002FF1 RID: 12273
		private float mSnapFinishThreshold = 0.1f;

		// Token: 0x04002FF2 RID: 12274
		private float mSnapVecThreshold = 145f;

		// Token: 0x04002FF3 RID: 12275
		[SerializeField]
		private bool mItemSnapEnable;

		// Token: 0x04002FF4 RID: 12276
		[SerializeField]
		private GridFixedType mGridFixedType;

		// Token: 0x04002FF5 RID: 12277
		public Action<LoopGridView, LoopGridViewItem> mOnSnapItemFinished;

		// Token: 0x04002FF6 RID: 12278
		public Action<LoopGridView> mOnSnapNearestChanged;

		// Token: 0x04002FF7 RID: 12279
		private int mLeftSnapUpdateExtraCount = 1;

		// Token: 0x04002FF8 RID: 12280
		[SerializeField]
		private Vector2 mViewPortSnapPivot = Vector2.zero;

		// Token: 0x04002FF9 RID: 12281
		[SerializeField]
		private Vector2 mItemSnapPivot = Vector2.zero;

		// Token: 0x04002FFA RID: 12282
		private LoopGridView.SnapData mCurSnapData = new LoopGridView.SnapData();

		// Token: 0x04002FFB RID: 12283
		private Vector3 mLastSnapCheckPos = Vector3.zero;

		// Token: 0x04002FFC RID: 12284
		private bool mListViewInited;

		// Token: 0x04002FFD RID: 12285
		private int mListUpdateCheckFrameCount;

		// Token: 0x04002FFE RID: 12286
		private LoopGridView.ItemRangeData mCurFrameItemRangeData = new LoopGridView.ItemRangeData();

		// Token: 0x04002FFF RID: 12287
		private int mNeedCheckContentPosLeftCount = 1;

		// Token: 0x04003000 RID: 12288
		private ClickEventListener mScrollBarClickEventListener1;

		// Token: 0x04003001 RID: 12289
		private ClickEventListener mScrollBarClickEventListener2;

		// Token: 0x04003002 RID: 12290
		private RowColumnPair mCurSnapNearestItemRowColumn;

		// Token: 0x02001511 RID: 5393
		private class SnapData
		{
			// Token: 0x060082F1 RID: 33521 RVA: 0x002DD448 File Offset: 0x002DB648
			public void Clear()
			{
				this.mSnapStatus = SnapStatus.NoTargetSet;
				this.mIsForceSnapTo = false;
			}

			// Token: 0x04006E48 RID: 28232
			public SnapStatus mSnapStatus;

			// Token: 0x04006E49 RID: 28233
			public RowColumnPair mSnapTarget;

			// Token: 0x04006E4A RID: 28234
			public Vector2 mSnapNeedMoveDir;

			// Token: 0x04006E4B RID: 28235
			public float mTargetSnapVal;

			// Token: 0x04006E4C RID: 28236
			public float mCurSnapVal;

			// Token: 0x04006E4D RID: 28237
			public bool mIsForceSnapTo;
		}

		// Token: 0x02001512 RID: 5394
		private class ItemRangeData
		{
			// Token: 0x04006E4E RID: 28238
			public int mMaxRow;

			// Token: 0x04006E4F RID: 28239
			public int mMinRow;

			// Token: 0x04006E50 RID: 28240
			public int mMaxColumn;

			// Token: 0x04006E51 RID: 28241
			public int mMinColumn;

			// Token: 0x04006E52 RID: 28242
			public Vector2 mCheckedPosition;
		}
	}
}
