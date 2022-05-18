using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SuperScrollView
{
	// Token: 0x020009FB RID: 2555
	public class LoopListView2 : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IEndDragHandler, IDragHandler
	{
		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x06004179 RID: 16761 RVA: 0x0002EE8E File Offset: 0x0002D08E
		// (set) Token: 0x0600417A RID: 16762 RVA: 0x0002EE96 File Offset: 0x0002D096
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

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x0600417B RID: 16763 RVA: 0x0002EE9F File Offset: 0x0002D09F
		public List<ItemPrefabConfData> ItemPrefabDataList
		{
			get
			{
				return this.mItemPrefabDataList;
			}
		}

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x0600417C RID: 16764 RVA: 0x0002EEA7 File Offset: 0x0002D0A7
		public List<LoopListViewItem2> ItemList
		{
			get
			{
				return this.mItemList;
			}
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x0600417D RID: 16765 RVA: 0x0002EEAF File Offset: 0x0002D0AF
		public bool IsVertList
		{
			get
			{
				return this.mIsVertList;
			}
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x0600417E RID: 16766 RVA: 0x0002EEB7 File Offset: 0x0002D0B7
		public int ItemTotalCount
		{
			get
			{
				return this.mItemTotalCount;
			}
		}

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x0600417F RID: 16767 RVA: 0x0002EEBF File Offset: 0x0002D0BF
		public RectTransform ContainerTrans
		{
			get
			{
				return this.mContainerTrans;
			}
		}

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x06004180 RID: 16768 RVA: 0x0002EEC7 File Offset: 0x0002D0C7
		public ScrollRect ScrollRect
		{
			get
			{
				return this.mScrollRect;
			}
		}

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x06004181 RID: 16769 RVA: 0x0002EECF File Offset: 0x0002D0CF
		public bool IsDraging
		{
			get
			{
				return this.mIsDraging;
			}
		}

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x06004182 RID: 16770 RVA: 0x0002EED7 File Offset: 0x0002D0D7
		// (set) Token: 0x06004183 RID: 16771 RVA: 0x0002EEDF File Offset: 0x0002D0DF
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

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x06004184 RID: 16772 RVA: 0x0002EEE8 File Offset: 0x0002D0E8
		// (set) Token: 0x06004185 RID: 16773 RVA: 0x0002EEF0 File Offset: 0x0002D0F0
		public bool SupportScrollBar
		{
			get
			{
				return this.mSupportScrollBar;
			}
			set
			{
				this.mSupportScrollBar = value;
			}
		}

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x06004186 RID: 16774 RVA: 0x0002EEF9 File Offset: 0x0002D0F9
		// (set) Token: 0x06004187 RID: 16775 RVA: 0x0002EF01 File Offset: 0x0002D101
		public float SnapMoveDefaultMaxAbsVec
		{
			get
			{
				return this.mSnapMoveDefaultMaxAbsVec;
			}
			set
			{
				this.mSnapMoveDefaultMaxAbsVec = value;
			}
		}

		// Token: 0x06004188 RID: 16776 RVA: 0x001C1ED0 File Offset: 0x001C00D0
		public ItemPrefabConfData GetItemPrefabConfData(string prefabName)
		{
			foreach (ItemPrefabConfData itemPrefabConfData in this.mItemPrefabDataList)
			{
				if (itemPrefabConfData.mItemPrefab == null)
				{
					Debug.LogError("A item prefab is null ");
				}
				else if (prefabName == itemPrefabConfData.mItemPrefab.name)
				{
					return itemPrefabConfData;
				}
			}
			return null;
		}

		// Token: 0x06004189 RID: 16777 RVA: 0x001C1F50 File Offset: 0x001C0150
		public void OnItemPrefabChanged(string prefabName)
		{
			ItemPrefabConfData itemPrefabConfData = this.GetItemPrefabConfData(prefabName);
			if (itemPrefabConfData == null)
			{
				return;
			}
			ItemPool itemPool = null;
			if (!this.mItemPoolDict.TryGetValue(prefabName, out itemPool))
			{
				return;
			}
			int num = -1;
			Vector3 pos = Vector3.zero;
			if (this.mItemList.Count > 0)
			{
				num = this.mItemList[0].ItemIndex;
				pos = this.mItemList[0].CachedRectTransform.anchoredPosition3D;
			}
			this.RecycleAllItem();
			this.ClearAllTmpRecycledItem();
			itemPool.DestroyAllItem();
			itemPool.Init(itemPrefabConfData.mItemPrefab, itemPrefabConfData.mPadding, itemPrefabConfData.mStartPosOffset, itemPrefabConfData.mInitCreateCount, this.mContainerTrans);
			if (num >= 0)
			{
				this.RefreshAllShownItemWithFirstIndexAndPos(num, pos);
			}
		}

		// Token: 0x0600418A RID: 16778 RVA: 0x001C2000 File Offset: 0x001C0200
		public void InitListView(int itemTotalCount, Func<LoopListView2, int, LoopListViewItem2> onGetItemByIndex, LoopListViewInitParam initParam = null)
		{
			if (initParam != null)
			{
				this.mDistanceForRecycle0 = initParam.mDistanceForRecycle0;
				this.mDistanceForNew0 = initParam.mDistanceForNew0;
				this.mDistanceForRecycle1 = initParam.mDistanceForRecycle1;
				this.mDistanceForNew1 = initParam.mDistanceForNew1;
				this.mSmoothDumpRate = initParam.mSmoothDumpRate;
				this.mSnapFinishThreshold = initParam.mSnapFinishThreshold;
				this.mSnapVecThreshold = initParam.mSnapVecThreshold;
				this.mItemDefaultWithPaddingSize = initParam.mItemDefaultWithPaddingSize;
			}
			this.mScrollRect = base.gameObject.GetComponent<ScrollRect>();
			if (this.mScrollRect == null)
			{
				Debug.LogError("ListView Init Failed! ScrollRect component not found!");
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
			this.mCurSnapData.Clear();
			this.mItemPosMgr = new ItemPosMgr(this.mItemDefaultWithPaddingSize);
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
			this.mIsVertList = (this.mArrangeType == ListItemArrangeType.TopToBottom || this.mArrangeType == ListItemArrangeType.BottomToTop);
			this.mScrollRect.horizontal = !this.mIsVertList;
			this.mScrollRect.vertical = this.mIsVertList;
			this.SetScrollbarListener();
			this.AdjustPivot(this.mViewPortRectTransform);
			this.AdjustAnchor(this.mContainerTrans);
			this.AdjustContainerPivot(this.mContainerTrans);
			this.InitItemPool();
			this.mOnGetItemByIndex = onGetItemByIndex;
			if (this.mListViewInited)
			{
				Debug.LogError("LoopListView2.InitListView method can be called only once.");
			}
			this.mListViewInited = true;
			this.ResetListView(true);
			this.mCurSnapData.Clear();
			this.mItemTotalCount = itemTotalCount;
			if (this.mItemTotalCount < 0)
			{
				this.mSupportScrollBar = false;
			}
			if (this.mSupportScrollBar)
			{
				this.mItemPosMgr.SetItemMaxCount(this.mItemTotalCount);
			}
			else
			{
				this.mItemPosMgr.SetItemMaxCount(0);
			}
			this.mCurReadyMaxItemIndex = 0;
			this.mCurReadyMinItemIndex = 0;
			this.mLeftSnapUpdateExtraCount = 1;
			this.mNeedCheckNextMaxItem = true;
			this.mNeedCheckNextMinItem = true;
			this.UpdateContentSize();
		}

		// Token: 0x0600418B RID: 16779 RVA: 0x0002EF0A File Offset: 0x0002D10A
		private void Start()
		{
			if (this.OnListViewStart != null)
			{
				this.OnListViewStart(this);
			}
		}

		// Token: 0x0600418C RID: 16780 RVA: 0x001C2294 File Offset: 0x001C0494
		private void SetScrollbarListener()
		{
			this.mScrollBarClickEventListener = null;
			Scrollbar scrollbar = null;
			if (this.mIsVertList && this.mScrollRect.verticalScrollbar != null)
			{
				scrollbar = this.mScrollRect.verticalScrollbar;
			}
			if (!this.mIsVertList && this.mScrollRect.horizontalScrollbar != null)
			{
				scrollbar = this.mScrollRect.horizontalScrollbar;
			}
			if (scrollbar == null)
			{
				return;
			}
			ClickEventListener clickEventListener = ClickEventListener.Get(scrollbar.gameObject);
			this.mScrollBarClickEventListener = clickEventListener;
			clickEventListener.SetPointerUpHandler(new Action<GameObject>(this.OnPointerUpInScrollBar));
			clickEventListener.SetPointerDownHandler(new Action<GameObject>(this.OnPointerDownInScrollBar));
		}

		// Token: 0x0600418D RID: 16781 RVA: 0x0002EF20 File Offset: 0x0002D120
		private void OnPointerDownInScrollBar(GameObject obj)
		{
			this.mCurSnapData.Clear();
		}

		// Token: 0x0600418E RID: 16782 RVA: 0x0002EF2D File Offset: 0x0002D12D
		private void OnPointerUpInScrollBar(GameObject obj)
		{
			this.ForceSnapUpdateCheck();
		}

		// Token: 0x0600418F RID: 16783 RVA: 0x0002EF35 File Offset: 0x0002D135
		public void ResetListView(bool resetPos = true)
		{
			this.mViewPortRectTransform.GetLocalCorners(this.mViewPortRectLocalCorners);
			if (resetPos)
			{
				this.mContainerTrans.anchoredPosition3D = Vector3.zero;
			}
			this.ForceSnapUpdateCheck();
		}

		// Token: 0x06004190 RID: 16784 RVA: 0x001C233C File Offset: 0x001C053C
		public void SetListItemCount(int itemCount, bool resetPos = true)
		{
			if (itemCount == this.mItemTotalCount)
			{
				return;
			}
			this.mCurSnapData.Clear();
			this.mItemTotalCount = itemCount;
			if (this.mItemTotalCount < 0)
			{
				this.mSupportScrollBar = false;
			}
			if (this.mSupportScrollBar)
			{
				this.mItemPosMgr.SetItemMaxCount(this.mItemTotalCount);
			}
			else
			{
				this.mItemPosMgr.SetItemMaxCount(0);
			}
			if (this.mItemTotalCount == 0)
			{
				this.mCurReadyMaxItemIndex = 0;
				this.mCurReadyMinItemIndex = 0;
				this.mNeedCheckNextMaxItem = false;
				this.mNeedCheckNextMinItem = false;
				this.RecycleAllItem();
				this.ClearAllTmpRecycledItem();
				this.UpdateContentSize();
				return;
			}
			if (this.mCurReadyMaxItemIndex >= this.mItemTotalCount)
			{
				this.mCurReadyMaxItemIndex = this.mItemTotalCount - 1;
			}
			this.mLeftSnapUpdateExtraCount = 1;
			this.mNeedCheckNextMaxItem = true;
			this.mNeedCheckNextMinItem = true;
			if (resetPos)
			{
				this.MovePanelToItemIndex(0, 0f);
				return;
			}
			if (this.mItemList.Count == 0)
			{
				this.MovePanelToItemIndex(0, 0f);
				return;
			}
			int num = this.mItemTotalCount - 1;
			if (this.mItemList[this.mItemList.Count - 1].ItemIndex <= num)
			{
				this.UpdateContentSize();
				this.UpdateAllShownItemsPos();
				return;
			}
			this.MovePanelToItemIndex(num, 0f);
		}

		// Token: 0x06004191 RID: 16785 RVA: 0x001C2470 File Offset: 0x001C0670
		public LoopListViewItem2 GetShownItemByItemIndex(int itemIndex)
		{
			int count = this.mItemList.Count;
			if (count == 0)
			{
				return null;
			}
			if (itemIndex < this.mItemList[0].ItemIndex || itemIndex > this.mItemList[count - 1].ItemIndex)
			{
				return null;
			}
			int index = itemIndex - this.mItemList[0].ItemIndex;
			return this.mItemList[index];
		}

		// Token: 0x06004192 RID: 16786 RVA: 0x001C24DC File Offset: 0x001C06DC
		public LoopListViewItem2 GetShownItemNearestItemIndex(int itemIndex)
		{
			int count = this.mItemList.Count;
			if (count == 0)
			{
				return null;
			}
			if (itemIndex < this.mItemList[0].ItemIndex)
			{
				return this.mItemList[0];
			}
			if (itemIndex > this.mItemList[count - 1].ItemIndex)
			{
				return this.mItemList[count - 1];
			}
			int index = itemIndex - this.mItemList[0].ItemIndex;
			return this.mItemList[index];
		}

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x06004193 RID: 16787 RVA: 0x0002EF61 File Offset: 0x0002D161
		public int ShownItemCount
		{
			get
			{
				return this.mItemList.Count;
			}
		}

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x06004194 RID: 16788 RVA: 0x001C2560 File Offset: 0x001C0760
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

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x06004195 RID: 16789 RVA: 0x001C259C File Offset: 0x001C079C
		public float ViewPortWidth
		{
			get
			{
				return this.mViewPortRectTransform.rect.width;
			}
		}

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x06004196 RID: 16790 RVA: 0x001C25BC File Offset: 0x001C07BC
		public float ViewPortHeight
		{
			get
			{
				return this.mViewPortRectTransform.rect.height;
			}
		}

		// Token: 0x06004197 RID: 16791 RVA: 0x001C25DC File Offset: 0x001C07DC
		public LoopListViewItem2 GetShownItemByIndex(int index)
		{
			int count = this.mItemList.Count;
			if (index < 0 || index >= count)
			{
				return null;
			}
			return this.mItemList[index];
		}

		// Token: 0x06004198 RID: 16792 RVA: 0x0002EF6E File Offset: 0x0002D16E
		public LoopListViewItem2 GetShownItemByIndexWithoutCheck(int index)
		{
			return this.mItemList[index];
		}

		// Token: 0x06004199 RID: 16793 RVA: 0x001C260C File Offset: 0x001C080C
		public int GetIndexInShownItemList(LoopListViewItem2 item)
		{
			if (item == null)
			{
				return -1;
			}
			int count = this.mItemList.Count;
			if (count == 0)
			{
				return -1;
			}
			for (int i = 0; i < count; i++)
			{
				if (this.mItemList[i] == item)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600419A RID: 16794 RVA: 0x001C2658 File Offset: 0x001C0858
		public void DoActionForEachShownItem(Action<LoopListViewItem2, object> action, object param)
		{
			if (action == null)
			{
				return;
			}
			int count = this.mItemList.Count;
			if (count == 0)
			{
				return;
			}
			for (int i = 0; i < count; i++)
			{
				action(this.mItemList[i], param);
			}
		}

		// Token: 0x0600419B RID: 16795 RVA: 0x001C2698 File Offset: 0x001C0898
		public LoopListViewItem2 NewListViewItem(string itemPrefabName)
		{
			ItemPool itemPool = null;
			if (!this.mItemPoolDict.TryGetValue(itemPrefabName, out itemPool))
			{
				return null;
			}
			LoopListViewItem2 item = itemPool.GetItem();
			RectTransform component = item.GetComponent<RectTransform>();
			component.SetParent(this.mContainerTrans);
			component.localScale = Vector3.one;
			component.anchoredPosition3D = Vector3.zero;
			component.localEulerAngles = Vector3.zero;
			item.ParentListView = this;
			return item;
		}

		// Token: 0x0600419C RID: 16796 RVA: 0x001C26F8 File Offset: 0x001C08F8
		public void OnItemSizeChanged(int itemIndex)
		{
			LoopListViewItem2 shownItemByItemIndex = this.GetShownItemByItemIndex(itemIndex);
			if (shownItemByItemIndex == null)
			{
				return;
			}
			if (this.mSupportScrollBar)
			{
				if (this.mIsVertList)
				{
					this.SetItemSize(itemIndex, shownItemByItemIndex.CachedRectTransform.rect.height, shownItemByItemIndex.Padding);
				}
				else
				{
					this.SetItemSize(itemIndex, shownItemByItemIndex.CachedRectTransform.rect.width, shownItemByItemIndex.Padding);
				}
			}
			this.UpdateContentSize();
			this.UpdateAllShownItemsPos();
		}

		// Token: 0x0600419D RID: 16797 RVA: 0x001C2778 File Offset: 0x001C0978
		public void RefreshItemByItemIndex(int itemIndex)
		{
			int count = this.mItemList.Count;
			if (count == 0)
			{
				return;
			}
			if (itemIndex < this.mItemList[0].ItemIndex || itemIndex > this.mItemList[count - 1].ItemIndex)
			{
				return;
			}
			int itemIndex2 = this.mItemList[0].ItemIndex;
			int index = itemIndex - itemIndex2;
			LoopListViewItem2 loopListViewItem = this.mItemList[index];
			Vector3 anchoredPosition3D = loopListViewItem.CachedRectTransform.anchoredPosition3D;
			this.RecycleItemTmp(loopListViewItem);
			LoopListViewItem2 newItemByIndex = this.GetNewItemByIndex(itemIndex);
			if (newItemByIndex == null)
			{
				this.RefreshAllShownItemWithFirstIndex(itemIndex2);
				return;
			}
			this.mItemList[index] = newItemByIndex;
			if (this.mIsVertList)
			{
				anchoredPosition3D.x = newItemByIndex.StartPosOffset;
			}
			else
			{
				anchoredPosition3D.y = newItemByIndex.StartPosOffset;
			}
			newItemByIndex.CachedRectTransform.anchoredPosition3D = anchoredPosition3D;
			this.OnItemSizeChanged(itemIndex);
			this.ClearAllTmpRecycledItem();
		}

		// Token: 0x0600419E RID: 16798 RVA: 0x0002EF7C File Offset: 0x0002D17C
		public void FinishSnapImmediately()
		{
			this.UpdateSnapMove(true, false);
		}

		// Token: 0x0600419F RID: 16799 RVA: 0x001C2864 File Offset: 0x001C0A64
		public void MovePanelToItemIndex(int itemIndex, float offset)
		{
			this.mScrollRect.StopMovement();
			this.mCurSnapData.Clear();
			if (this.mItemTotalCount == 0)
			{
				return;
			}
			if (itemIndex < 0 && this.mItemTotalCount > 0)
			{
				return;
			}
			if (this.mItemTotalCount > 0 && itemIndex >= this.mItemTotalCount)
			{
				itemIndex = this.mItemTotalCount - 1;
			}
			if (offset < 0f)
			{
				offset = 0f;
			}
			Vector3 zero = Vector3.zero;
			float viewPortSize = this.ViewPortSize;
			if (offset > viewPortSize)
			{
				offset = viewPortSize;
			}
			if (this.mArrangeType == ListItemArrangeType.TopToBottom)
			{
				float num = this.mContainerTrans.anchoredPosition3D.y;
				if (num < 0f)
				{
					num = 0f;
				}
				zero.y = -num - offset;
			}
			else if (this.mArrangeType == ListItemArrangeType.BottomToTop)
			{
				float num2 = this.mContainerTrans.anchoredPosition3D.y;
				if (num2 > 0f)
				{
					num2 = 0f;
				}
				zero.y = -num2 + offset;
			}
			else if (this.mArrangeType == ListItemArrangeType.LeftToRight)
			{
				float num3 = this.mContainerTrans.anchoredPosition3D.x;
				if (num3 > 0f)
				{
					num3 = 0f;
				}
				zero.x = -num3 + offset;
			}
			else if (this.mArrangeType == ListItemArrangeType.RightToLeft)
			{
				float num4 = this.mContainerTrans.anchoredPosition3D.x;
				if (num4 < 0f)
				{
					num4 = 0f;
				}
				zero.x = -num4 - offset;
			}
			this.RecycleAllItem();
			LoopListViewItem2 newItemByIndex = this.GetNewItemByIndex(itemIndex);
			if (newItemByIndex == null)
			{
				this.ClearAllTmpRecycledItem();
				return;
			}
			if (this.mIsVertList)
			{
				zero.x = newItemByIndex.StartPosOffset;
			}
			else
			{
				zero.y = newItemByIndex.StartPosOffset;
			}
			newItemByIndex.CachedRectTransform.anchoredPosition3D = zero;
			if (this.mSupportScrollBar)
			{
				if (this.mIsVertList)
				{
					this.SetItemSize(itemIndex, newItemByIndex.CachedRectTransform.rect.height, newItemByIndex.Padding);
				}
				else
				{
					this.SetItemSize(itemIndex, newItemByIndex.CachedRectTransform.rect.width, newItemByIndex.Padding);
				}
			}
			this.mItemList.Add(newItemByIndex);
			this.UpdateContentSize();
			this.UpdateListView(viewPortSize + 100f, viewPortSize + 100f, viewPortSize, viewPortSize);
			this.AdjustPanelPos();
			this.ClearAllTmpRecycledItem();
			this.ForceSnapUpdateCheck();
			this.UpdateSnapMove(false, true);
		}

		// Token: 0x060041A0 RID: 16800 RVA: 0x0002EF86 File Offset: 0x0002D186
		public void RefreshAllShownItem()
		{
			if (this.mItemList.Count == 0)
			{
				return;
			}
			this.RefreshAllShownItemWithFirstIndex(this.mItemList[0].ItemIndex);
		}

		// Token: 0x060041A1 RID: 16801 RVA: 0x001C2AA4 File Offset: 0x001C0CA4
		public void RefreshAllShownItemWithFirstIndex(int firstItemIndex)
		{
			int count = this.mItemList.Count;
			if (count == 0)
			{
				return;
			}
			Vector3 anchoredPosition3D = this.mItemList[0].CachedRectTransform.anchoredPosition3D;
			this.RecycleAllItem();
			for (int i = 0; i < count; i++)
			{
				int num = firstItemIndex + i;
				LoopListViewItem2 newItemByIndex = this.GetNewItemByIndex(num);
				if (newItemByIndex == null)
				{
					break;
				}
				if (this.mIsVertList)
				{
					anchoredPosition3D.x = newItemByIndex.StartPosOffset;
				}
				else
				{
					anchoredPosition3D.y = newItemByIndex.StartPosOffset;
				}
				newItemByIndex.CachedRectTransform.anchoredPosition3D = anchoredPosition3D;
				if (this.mSupportScrollBar)
				{
					if (this.mIsVertList)
					{
						this.SetItemSize(num, newItemByIndex.CachedRectTransform.rect.height, newItemByIndex.Padding);
					}
					else
					{
						this.SetItemSize(num, newItemByIndex.CachedRectTransform.rect.width, newItemByIndex.Padding);
					}
				}
				this.mItemList.Add(newItemByIndex);
			}
			this.UpdateContentSize();
			this.UpdateAllShownItemsPos();
			this.ClearAllTmpRecycledItem();
		}

		// Token: 0x060041A2 RID: 16802 RVA: 0x001C2BB4 File Offset: 0x001C0DB4
		public void RefreshAllShownItemWithFirstIndexAndPos(int firstItemIndex, Vector3 pos)
		{
			this.RecycleAllItem();
			LoopListViewItem2 newItemByIndex = this.GetNewItemByIndex(firstItemIndex);
			if (newItemByIndex == null)
			{
				return;
			}
			if (this.mIsVertList)
			{
				pos.x = newItemByIndex.StartPosOffset;
			}
			else
			{
				pos.y = newItemByIndex.StartPosOffset;
			}
			newItemByIndex.CachedRectTransform.anchoredPosition3D = pos;
			if (this.mSupportScrollBar)
			{
				if (this.mIsVertList)
				{
					this.SetItemSize(firstItemIndex, newItemByIndex.CachedRectTransform.rect.height, newItemByIndex.Padding);
				}
				else
				{
					this.SetItemSize(firstItemIndex, newItemByIndex.CachedRectTransform.rect.width, newItemByIndex.Padding);
				}
			}
			this.mItemList.Add(newItemByIndex);
			this.UpdateContentSize();
			this.UpdateAllShownItemsPos();
			this.UpdateListView(this.mDistanceForRecycle0, this.mDistanceForRecycle1, this.mDistanceForNew0, this.mDistanceForNew1);
			this.ClearAllTmpRecycledItem();
		}

		// Token: 0x060041A3 RID: 16803 RVA: 0x001C2C98 File Offset: 0x001C0E98
		private void RecycleItemTmp(LoopListViewItem2 item)
		{
			if (item == null)
			{
				return;
			}
			if (string.IsNullOrEmpty(item.ItemPrefabName))
			{
				return;
			}
			ItemPool itemPool = null;
			if (!this.mItemPoolDict.TryGetValue(item.ItemPrefabName, out itemPool))
			{
				return;
			}
			itemPool.RecycleItem(item);
		}

		// Token: 0x060041A4 RID: 16804 RVA: 0x001C2CDC File Offset: 0x001C0EDC
		private void ClearAllTmpRecycledItem()
		{
			int count = this.mItemPoolList.Count;
			for (int i = 0; i < count; i++)
			{
				this.mItemPoolList[i].ClearTmpRecycledItem();
			}
		}

		// Token: 0x060041A5 RID: 16805 RVA: 0x001C2D14 File Offset: 0x001C0F14
		private void RecycleAllItem()
		{
			foreach (LoopListViewItem2 item in this.mItemList)
			{
				this.RecycleItemTmp(item);
			}
			this.mItemList.Clear();
		}

		// Token: 0x060041A6 RID: 16806 RVA: 0x001C2D74 File Offset: 0x001C0F74
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

		// Token: 0x060041A7 RID: 16807 RVA: 0x001C2D74 File Offset: 0x001C0F74
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

		// Token: 0x060041A8 RID: 16808 RVA: 0x001C2DE8 File Offset: 0x001C0FE8
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

		// Token: 0x060041A9 RID: 16809 RVA: 0x001C2DE8 File Offset: 0x001C0FE8
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

		// Token: 0x060041AA RID: 16810 RVA: 0x001C2E9C File Offset: 0x001C109C
		private void InitItemPool()
		{
			foreach (ItemPrefabConfData itemPrefabConfData in this.mItemPrefabDataList)
			{
				if (itemPrefabConfData.mItemPrefab == null)
				{
					Debug.LogError("A item prefab is null ");
				}
				else
				{
					string name = itemPrefabConfData.mItemPrefab.name;
					if (this.mItemPoolDict.ContainsKey(name))
					{
						Debug.LogError("A item prefab with name " + name + " has existed!");
					}
					else
					{
						RectTransform component = itemPrefabConfData.mItemPrefab.GetComponent<RectTransform>();
						if (component == null)
						{
							Debug.LogError("RectTransform component is not found in the prefab " + name);
						}
						else
						{
							this.AdjustAnchor(component);
							this.AdjustPivot(component);
							if (itemPrefabConfData.mItemPrefab.GetComponent<LoopListViewItem2>() == null)
							{
								itemPrefabConfData.mItemPrefab.AddComponent<LoopListViewItem2>();
							}
							ItemPool itemPool = new ItemPool();
							itemPool.Init(itemPrefabConfData.mItemPrefab, itemPrefabConfData.mPadding, itemPrefabConfData.mStartPosOffset, itemPrefabConfData.mInitCreateCount, this.mContainerTrans);
							this.mItemPoolDict.Add(name, itemPool);
							this.mItemPoolList.Add(itemPool);
						}
					}
				}
			}
		}

		// Token: 0x060041AB RID: 16811 RVA: 0x0002EFAD File Offset: 0x0002D1AD
		public virtual void OnBeginDrag(PointerEventData eventData)
		{
			if (eventData.button != null)
			{
				return;
			}
			this.mIsDraging = true;
			this.CacheDragPointerEventData(eventData);
			this.mCurSnapData.Clear();
			if (this.mOnBeginDragAction != null)
			{
				this.mOnBeginDragAction();
			}
		}

		// Token: 0x060041AC RID: 16812 RVA: 0x0002EFE4 File Offset: 0x0002D1E4
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
			this.ForceSnapUpdateCheck();
		}

		// Token: 0x060041AD RID: 16813 RVA: 0x0002F016 File Offset: 0x0002D216
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

		// Token: 0x060041AE RID: 16814 RVA: 0x001C2FE8 File Offset: 0x001C11E8
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

		// Token: 0x060041AF RID: 16815 RVA: 0x001C3054 File Offset: 0x001C1254
		private LoopListViewItem2 GetNewItemByIndex(int index)
		{
			if (this.mSupportScrollBar && index < 0)
			{
				return null;
			}
			if (this.mItemTotalCount > 0 && index >= this.mItemTotalCount)
			{
				return null;
			}
			LoopListViewItem2 loopListViewItem = this.mOnGetItemByIndex(this, index);
			if (loopListViewItem == null)
			{
				return null;
			}
			loopListViewItem.ItemIndex = index;
			loopListViewItem.ItemCreatedCheckFrameCount = this.mListUpdateCheckFrameCount;
			return loopListViewItem;
		}

		// Token: 0x060041B0 RID: 16816 RVA: 0x0002F03B File Offset: 0x0002D23B
		private void SetItemSize(int itemIndex, float itemSize, float padding)
		{
			this.mItemPosMgr.SetItemSize(itemIndex, itemSize + padding);
			if (itemIndex >= this.mLastItemIndex)
			{
				this.mLastItemIndex = itemIndex;
				this.mLastItemPadding = padding;
			}
		}

		// Token: 0x060041B1 RID: 16817 RVA: 0x0002F063 File Offset: 0x0002D263
		private bool GetPlusItemIndexAndPosAtGivenPos(float pos, ref int index, ref float itemPos)
		{
			return this.mItemPosMgr.GetItemIndexAndPosAtGivenPos(pos, ref index, ref itemPos);
		}

		// Token: 0x060041B2 RID: 16818 RVA: 0x0002F073 File Offset: 0x0002D273
		private float GetItemPos(int itemIndex)
		{
			return this.mItemPosMgr.GetItemPos(itemIndex);
		}

		// Token: 0x060041B3 RID: 16819 RVA: 0x0002F081 File Offset: 0x0002D281
		public Vector3 GetItemCornerPosInViewPort(LoopListViewItem2 item, ItemCornerEnum corner = ItemCornerEnum.LeftBottom)
		{
			item.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
			return this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[(int)corner]);
		}

		// Token: 0x060041B4 RID: 16820 RVA: 0x001C30B0 File Offset: 0x001C12B0
		private void AdjustPanelPos()
		{
			if (this.mItemList.Count == 0)
			{
				return;
			}
			this.UpdateAllShownItemsPos();
			float viewPortSize = this.ViewPortSize;
			float contentPanelSize = this.GetContentPanelSize();
			if (this.mArrangeType == ListItemArrangeType.TopToBottom)
			{
				if (contentPanelSize <= viewPortSize)
				{
					Vector3 anchoredPosition3D = this.mContainerTrans.anchoredPosition3D;
					anchoredPosition3D.y = 0f;
					this.mContainerTrans.anchoredPosition3D = anchoredPosition3D;
					this.mItemList[0].CachedRectTransform.anchoredPosition3D = new Vector3(this.mItemList[0].StartPosOffset, 0f, 0f);
					this.UpdateAllShownItemsPos();
					return;
				}
				this.mItemList[0].CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
				if (this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[1]).y < this.mViewPortRectLocalCorners[1].y)
				{
					Vector3 anchoredPosition3D2 = this.mContainerTrans.anchoredPosition3D;
					anchoredPosition3D2.y = 0f;
					this.mContainerTrans.anchoredPosition3D = anchoredPosition3D2;
					this.mItemList[0].CachedRectTransform.anchoredPosition3D = new Vector3(this.mItemList[0].StartPosOffset, 0f, 0f);
					this.UpdateAllShownItemsPos();
					return;
				}
				this.mItemList[this.mItemList.Count - 1].CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
				float num = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[0]).y - this.mViewPortRectLocalCorners[0].y;
				if (num > 0f)
				{
					Vector3 anchoredPosition3D3 = this.mItemList[0].CachedRectTransform.anchoredPosition3D;
					anchoredPosition3D3.y -= num;
					this.mItemList[0].CachedRectTransform.anchoredPosition3D = anchoredPosition3D3;
					this.UpdateAllShownItemsPos();
					return;
				}
			}
			else if (this.mArrangeType == ListItemArrangeType.BottomToTop)
			{
				if (contentPanelSize <= viewPortSize)
				{
					Vector3 anchoredPosition3D4 = this.mContainerTrans.anchoredPosition3D;
					anchoredPosition3D4.y = 0f;
					this.mContainerTrans.anchoredPosition3D = anchoredPosition3D4;
					this.mItemList[0].CachedRectTransform.anchoredPosition3D = new Vector3(this.mItemList[0].StartPosOffset, 0f, 0f);
					this.UpdateAllShownItemsPos();
					return;
				}
				this.mItemList[0].CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
				if (this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[0]).y > this.mViewPortRectLocalCorners[0].y)
				{
					Vector3 anchoredPosition3D5 = this.mContainerTrans.anchoredPosition3D;
					anchoredPosition3D5.y = 0f;
					this.mContainerTrans.anchoredPosition3D = anchoredPosition3D5;
					this.mItemList[0].CachedRectTransform.anchoredPosition3D = new Vector3(this.mItemList[0].StartPosOffset, 0f, 0f);
					this.UpdateAllShownItemsPos();
					return;
				}
				this.mItemList[this.mItemList.Count - 1].CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
				Vector3 vector = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[1]);
				float num2 = this.mViewPortRectLocalCorners[1].y - vector.y;
				if (num2 > 0f)
				{
					Vector3 anchoredPosition3D6 = this.mItemList[0].CachedRectTransform.anchoredPosition3D;
					anchoredPosition3D6.y += num2;
					this.mItemList[0].CachedRectTransform.anchoredPosition3D = anchoredPosition3D6;
					this.UpdateAllShownItemsPos();
					return;
				}
			}
			else if (this.mArrangeType == ListItemArrangeType.LeftToRight)
			{
				if (contentPanelSize <= viewPortSize)
				{
					Vector3 anchoredPosition3D7 = this.mContainerTrans.anchoredPosition3D;
					anchoredPosition3D7.x = 0f;
					this.mContainerTrans.anchoredPosition3D = anchoredPosition3D7;
					this.mItemList[0].CachedRectTransform.anchoredPosition3D = new Vector3(0f, this.mItemList[0].StartPosOffset, 0f);
					this.UpdateAllShownItemsPos();
					return;
				}
				this.mItemList[0].CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
				if (this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[1]).x > this.mViewPortRectLocalCorners[1].x)
				{
					Vector3 anchoredPosition3D8 = this.mContainerTrans.anchoredPosition3D;
					anchoredPosition3D8.x = 0f;
					this.mContainerTrans.anchoredPosition3D = anchoredPosition3D8;
					this.mItemList[0].CachedRectTransform.anchoredPosition3D = new Vector3(0f, this.mItemList[0].StartPosOffset, 0f);
					this.UpdateAllShownItemsPos();
					return;
				}
				this.mItemList[this.mItemList.Count - 1].CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
				Vector3 vector2 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[2]);
				float num3 = this.mViewPortRectLocalCorners[2].x - vector2.x;
				if (num3 > 0f)
				{
					Vector3 anchoredPosition3D9 = this.mItemList[0].CachedRectTransform.anchoredPosition3D;
					anchoredPosition3D9.x += num3;
					this.mItemList[0].CachedRectTransform.anchoredPosition3D = anchoredPosition3D9;
					this.UpdateAllShownItemsPos();
					return;
				}
			}
			else if (this.mArrangeType == ListItemArrangeType.RightToLeft)
			{
				if (contentPanelSize <= viewPortSize)
				{
					Vector3 anchoredPosition3D10 = this.mContainerTrans.anchoredPosition3D;
					anchoredPosition3D10.x = 0f;
					this.mContainerTrans.anchoredPosition3D = anchoredPosition3D10;
					this.mItemList[0].CachedRectTransform.anchoredPosition3D = new Vector3(0f, this.mItemList[0].StartPosOffset, 0f);
					this.UpdateAllShownItemsPos();
					return;
				}
				this.mItemList[0].CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
				if (this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[2]).x < this.mViewPortRectLocalCorners[2].x)
				{
					Vector3 anchoredPosition3D11 = this.mContainerTrans.anchoredPosition3D;
					anchoredPosition3D11.x = 0f;
					this.mContainerTrans.anchoredPosition3D = anchoredPosition3D11;
					this.mItemList[0].CachedRectTransform.anchoredPosition3D = new Vector3(0f, this.mItemList[0].StartPosOffset, 0f);
					this.UpdateAllShownItemsPos();
					return;
				}
				this.mItemList[this.mItemList.Count - 1].CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
				float num4 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[1]).x - this.mViewPortRectLocalCorners[1].x;
				if (num4 > 0f)
				{
					Vector3 anchoredPosition3D12 = this.mItemList[0].CachedRectTransform.anchoredPosition3D;
					anchoredPosition3D12.x -= num4;
					this.mItemList[0].CachedRectTransform.anchoredPosition3D = anchoredPosition3D12;
					this.UpdateAllShownItemsPos();
					return;
				}
			}
		}

		// Token: 0x060041B5 RID: 16821 RVA: 0x001C3814 File Offset: 0x001C1A14
		private void Update()
		{
			if (!this.mListViewInited)
			{
				return;
			}
			if (this.mNeedAdjustVec)
			{
				this.mNeedAdjustVec = false;
				if (this.mIsVertList)
				{
					if (this.mScrollRect.velocity.y * this.mAdjustedVec.y > 0f)
					{
						this.mScrollRect.velocity = this.mAdjustedVec;
					}
				}
				else if (this.mScrollRect.velocity.x * this.mAdjustedVec.x > 0f)
				{
					this.mScrollRect.velocity = this.mAdjustedVec;
				}
			}
			if (this.mSupportScrollBar)
			{
				this.mItemPosMgr.Update(false);
			}
			this.UpdateSnapMove(false, false);
			this.UpdateListView(this.mDistanceForRecycle0, this.mDistanceForRecycle1, this.mDistanceForNew0, this.mDistanceForNew1);
			this.ClearAllTmpRecycledItem();
			this.mLastFrameContainerPos = this.mContainerTrans.anchoredPosition3D;
		}

		// Token: 0x060041B6 RID: 16822 RVA: 0x0002F0AB File Offset: 0x0002D2AB
		private void UpdateSnapMove(bool immediate = false, bool forceSendEvent = false)
		{
			if (!this.mItemSnapEnable)
			{
				return;
			}
			if (this.mIsVertList)
			{
				this.UpdateSnapVertical(immediate, forceSendEvent);
				return;
			}
			this.UpdateSnapHorizontal(immediate, forceSendEvent);
		}

		// Token: 0x060041B7 RID: 16823 RVA: 0x001C38FC File Offset: 0x001C1AFC
		public void UpdateAllShownItemSnapData()
		{
			if (!this.mItemSnapEnable)
			{
				return;
			}
			int count = this.mItemList.Count;
			if (count == 0)
			{
				return;
			}
			Vector3 anchoredPosition3D = this.mContainerTrans.anchoredPosition3D;
			LoopListViewItem2 loopListViewItem = this.mItemList[0];
			loopListViewItem.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
			if (this.mArrangeType == ListItemArrangeType.TopToBottom)
			{
				float num = -(1f - this.mViewPortSnapPivot.y) * this.mViewPortRectTransform.rect.height;
				float num2 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[1]).y;
				float num3 = num2 - loopListViewItem.ItemSizeWithPadding;
				float num4 = num2 - loopListViewItem.ItemSize * (1f - this.mItemSnapPivot.y);
				for (int i = 0; i < count; i++)
				{
					this.mItemList[i].DistanceWithViewPortSnapCenter = num - num4;
					if (i + 1 < count)
					{
						num2 = num3;
						num3 -= this.mItemList[i + 1].ItemSizeWithPadding;
						num4 = num2 - this.mItemList[i + 1].ItemSize * (1f - this.mItemSnapPivot.y);
					}
				}
				return;
			}
			if (this.mArrangeType == ListItemArrangeType.BottomToTop)
			{
				float num = this.mViewPortSnapPivot.y * this.mViewPortRectTransform.rect.height;
				float num2 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[0]).y;
				float num3 = num2 + loopListViewItem.ItemSizeWithPadding;
				float num4 = num2 + loopListViewItem.ItemSize * this.mItemSnapPivot.y;
				for (int j = 0; j < count; j++)
				{
					this.mItemList[j].DistanceWithViewPortSnapCenter = num - num4;
					if (j + 1 < count)
					{
						num2 = num3;
						num3 += this.mItemList[j + 1].ItemSizeWithPadding;
						num4 = num2 + this.mItemList[j + 1].ItemSize * this.mItemSnapPivot.y;
					}
				}
				return;
			}
			if (this.mArrangeType == ListItemArrangeType.RightToLeft)
			{
				float num = -(1f - this.mViewPortSnapPivot.x) * this.mViewPortRectTransform.rect.width;
				float num2 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[2]).x;
				float num3 = num2 - loopListViewItem.ItemSizeWithPadding;
				float num4 = num2 - loopListViewItem.ItemSize * (1f - this.mItemSnapPivot.x);
				for (int k = 0; k < count; k++)
				{
					this.mItemList[k].DistanceWithViewPortSnapCenter = num - num4;
					if (k + 1 < count)
					{
						num2 = num3;
						num3 -= this.mItemList[k + 1].ItemSizeWithPadding;
						num4 = num2 - this.mItemList[k + 1].ItemSize * (1f - this.mItemSnapPivot.x);
					}
				}
				return;
			}
			if (this.mArrangeType == ListItemArrangeType.LeftToRight)
			{
				float num = this.mViewPortSnapPivot.x * this.mViewPortRectTransform.rect.width;
				float num2 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[1]).x;
				float num3 = num2 + loopListViewItem.ItemSizeWithPadding;
				float num4 = num2 + loopListViewItem.ItemSize * this.mItemSnapPivot.x;
				for (int l = 0; l < count; l++)
				{
					this.mItemList[l].DistanceWithViewPortSnapCenter = num - num4;
					if (l + 1 < count)
					{
						num2 = num3;
						num3 += this.mItemList[l + 1].ItemSizeWithPadding;
						num4 = num2 + this.mItemList[l + 1].ItemSize * this.mItemSnapPivot.x;
					}
				}
			}
		}

		// Token: 0x060041B8 RID: 16824 RVA: 0x001C3CE4 File Offset: 0x001C1EE4
		private void UpdateSnapVertical(bool immediate = false, bool forceSendEvent = false)
		{
			if (!this.mItemSnapEnable)
			{
				return;
			}
			int count = this.mItemList.Count;
			if (count == 0)
			{
				return;
			}
			Vector3 anchoredPosition3D = this.mContainerTrans.anchoredPosition3D;
			bool flag = anchoredPosition3D.y != this.mLastSnapCheckPos.y;
			this.mLastSnapCheckPos = anchoredPosition3D;
			if (!flag && this.mLeftSnapUpdateExtraCount > 0)
			{
				this.mLeftSnapUpdateExtraCount--;
				flag = true;
			}
			if (flag)
			{
				LoopListViewItem2 loopListViewItem = this.mItemList[0];
				loopListViewItem.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
				int num = -1;
				float num2 = float.MaxValue;
				if (this.mArrangeType == ListItemArrangeType.TopToBottom)
				{
					float num3 = -(1f - this.mViewPortSnapPivot.y) * this.mViewPortRectTransform.rect.height;
					float num4 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[1]).y;
					float num5 = num4 - loopListViewItem.ItemSizeWithPadding;
					float num6 = num4 - loopListViewItem.ItemSize * (1f - this.mItemSnapPivot.y);
					for (int i = 0; i < count; i++)
					{
						float num7 = Mathf.Abs(num3 - num6);
						if (num7 >= num2)
						{
							break;
						}
						num2 = num7;
						num = i;
						if (i + 1 < count)
						{
							num4 = num5;
							num5 -= this.mItemList[i + 1].ItemSizeWithPadding;
							num6 = num4 - this.mItemList[i + 1].ItemSize * (1f - this.mItemSnapPivot.y);
						}
					}
				}
				else if (this.mArrangeType == ListItemArrangeType.BottomToTop)
				{
					float num3 = this.mViewPortSnapPivot.y * this.mViewPortRectTransform.rect.height;
					float num4 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[0]).y;
					float num5 = num4 + loopListViewItem.ItemSizeWithPadding;
					float num6 = num4 + loopListViewItem.ItemSize * this.mItemSnapPivot.y;
					for (int j = 0; j < count; j++)
					{
						float num7 = Mathf.Abs(num3 - num6);
						if (num7 >= num2)
						{
							break;
						}
						num2 = num7;
						num = j;
						if (j + 1 < count)
						{
							num4 = num5;
							num5 += this.mItemList[j + 1].ItemSizeWithPadding;
							num6 = num4 + this.mItemList[j + 1].ItemSize * this.mItemSnapPivot.y;
						}
					}
				}
				if (num >= 0)
				{
					int num8 = this.mCurSnapNearestItemIndex;
					this.mCurSnapNearestItemIndex = this.mItemList[num].ItemIndex;
					if ((forceSendEvent || this.mItemList[num].ItemIndex != num8) && this.mOnSnapNearestChanged != null)
					{
						this.mOnSnapNearestChanged(this, this.mItemList[num]);
					}
				}
				else
				{
					this.mCurSnapNearestItemIndex = -1;
				}
			}
			if (!this.CanSnap())
			{
				this.ClearSnapData();
				return;
			}
			float num9 = Mathf.Abs(this.mScrollRect.velocity.y);
			this.UpdateCurSnapData();
			if (this.mCurSnapData.mSnapStatus != SnapStatus.SnapMoving)
			{
				return;
			}
			if (num9 > 0f)
			{
				this.mScrollRect.StopMovement();
			}
			float mCurSnapVal = this.mCurSnapData.mCurSnapVal;
			if (!this.mCurSnapData.mIsTempTarget)
			{
				if (this.mSmoothDumpVel * this.mCurSnapData.mTargetSnapVal < 0f)
				{
					this.mSmoothDumpVel = 0f;
				}
				this.mCurSnapData.mCurSnapVal = Mathf.SmoothDamp(this.mCurSnapData.mCurSnapVal, this.mCurSnapData.mTargetSnapVal, ref this.mSmoothDumpVel, this.mSmoothDumpRate);
			}
			else
			{
				float mMoveMaxAbsVec = this.mCurSnapData.mMoveMaxAbsVec;
				if (mMoveMaxAbsVec <= 0f)
				{
					mMoveMaxAbsVec = this.mSnapMoveDefaultMaxAbsVec;
				}
				this.mSmoothDumpVel = mMoveMaxAbsVec * Mathf.Sign(this.mCurSnapData.mTargetSnapVal);
				this.mCurSnapData.mCurSnapVal = Mathf.MoveTowards(this.mCurSnapData.mCurSnapVal, this.mCurSnapData.mTargetSnapVal, mMoveMaxAbsVec * Time.deltaTime);
			}
			float num10 = this.mCurSnapData.mCurSnapVal - mCurSnapVal;
			if (immediate || Mathf.Abs(this.mCurSnapData.mTargetSnapVal - this.mCurSnapData.mCurSnapVal) < this.mSnapFinishThreshold)
			{
				anchoredPosition3D.y = anchoredPosition3D.y + this.mCurSnapData.mTargetSnapVal - mCurSnapVal;
				this.mCurSnapData.mSnapStatus = SnapStatus.SnapMoveFinish;
				if (this.mOnSnapItemFinished != null)
				{
					LoopListViewItem2 shownItemByItemIndex = this.GetShownItemByItemIndex(this.mCurSnapNearestItemIndex);
					if (shownItemByItemIndex != null)
					{
						this.mOnSnapItemFinished(this, shownItemByItemIndex);
					}
				}
			}
			else
			{
				anchoredPosition3D.y += num10;
			}
			if (this.mArrangeType == ListItemArrangeType.TopToBottom)
			{
				float num11 = this.mViewPortRectLocalCorners[0].y + this.mContainerTrans.rect.height;
				anchoredPosition3D.y = Mathf.Clamp(anchoredPosition3D.y, 0f, num11);
				this.mContainerTrans.anchoredPosition3D = anchoredPosition3D;
				return;
			}
			if (this.mArrangeType == ListItemArrangeType.BottomToTop)
			{
				float num12 = this.mViewPortRectLocalCorners[1].y - this.mContainerTrans.rect.height;
				anchoredPosition3D.y = Mathf.Clamp(anchoredPosition3D.y, num12, 0f);
				this.mContainerTrans.anchoredPosition3D = anchoredPosition3D;
			}
		}

		// Token: 0x060041B9 RID: 16825 RVA: 0x001C425C File Offset: 0x001C245C
		private void UpdateCurSnapData()
		{
			if (this.mItemList.Count == 0)
			{
				this.mCurSnapData.Clear();
				return;
			}
			if (this.mCurSnapData.mSnapStatus == SnapStatus.SnapMoveFinish)
			{
				if (this.mCurSnapData.mSnapTargetIndex == this.mCurSnapNearestItemIndex)
				{
					return;
				}
				this.mCurSnapData.mSnapStatus = SnapStatus.NoTargetSet;
			}
			if (this.mCurSnapData.mSnapStatus == SnapStatus.SnapMoving)
			{
				if (this.mCurSnapData.mIsForceSnapTo)
				{
					if (this.mCurSnapData.mIsTempTarget)
					{
						LoopListViewItem2 shownItemNearestItemIndex = this.GetShownItemNearestItemIndex(this.mCurSnapData.mSnapTargetIndex);
						if (shownItemNearestItemIndex == null)
						{
							this.mCurSnapData.Clear();
							return;
						}
						if (shownItemNearestItemIndex.ItemIndex == this.mCurSnapData.mSnapTargetIndex)
						{
							this.UpdateAllShownItemSnapData();
							this.mCurSnapData.mTargetSnapVal = shownItemNearestItemIndex.DistanceWithViewPortSnapCenter;
							this.mCurSnapData.mCurSnapVal = 0f;
							this.mCurSnapData.mIsTempTarget = false;
							this.mCurSnapData.mSnapStatus = SnapStatus.SnapMoving;
							return;
						}
						if (this.mCurSnapData.mTempTargetIndex != shownItemNearestItemIndex.ItemIndex)
						{
							this.UpdateAllShownItemSnapData();
							this.mCurSnapData.mTargetSnapVal = shownItemNearestItemIndex.DistanceWithViewPortSnapCenter;
							this.mCurSnapData.mCurSnapVal = 0f;
							this.mCurSnapData.mSnapStatus = SnapStatus.SnapMoving;
							this.mCurSnapData.mIsTempTarget = true;
							this.mCurSnapData.mTempTargetIndex = shownItemNearestItemIndex.ItemIndex;
							return;
						}
					}
					return;
				}
				if (this.mCurSnapData.mSnapTargetIndex == this.mCurSnapNearestItemIndex)
				{
					return;
				}
				this.mCurSnapData.mSnapStatus = SnapStatus.NoTargetSet;
			}
			if (this.mCurSnapData.mSnapStatus == SnapStatus.NoTargetSet)
			{
				if (this.GetShownItemByItemIndex(this.mCurSnapNearestItemIndex) == null)
				{
					return;
				}
				this.mCurSnapData.mSnapTargetIndex = this.mCurSnapNearestItemIndex;
				this.mCurSnapData.mSnapStatus = SnapStatus.TargetHasSet;
				this.mCurSnapData.mIsForceSnapTo = false;
			}
			if (this.mCurSnapData.mSnapStatus == SnapStatus.TargetHasSet)
			{
				LoopListViewItem2 shownItemNearestItemIndex2 = this.GetShownItemNearestItemIndex(this.mCurSnapData.mSnapTargetIndex);
				if (shownItemNearestItemIndex2 == null)
				{
					this.mCurSnapData.Clear();
					return;
				}
				if (shownItemNearestItemIndex2.ItemIndex == this.mCurSnapData.mSnapTargetIndex)
				{
					this.UpdateAllShownItemSnapData();
					this.mCurSnapData.mTargetSnapVal = shownItemNearestItemIndex2.DistanceWithViewPortSnapCenter;
					this.mCurSnapData.mCurSnapVal = 0f;
					this.mCurSnapData.mIsTempTarget = false;
					this.mCurSnapData.mSnapStatus = SnapStatus.SnapMoving;
					return;
				}
				this.UpdateAllShownItemSnapData();
				this.mCurSnapData.mTargetSnapVal = shownItemNearestItemIndex2.DistanceWithViewPortSnapCenter;
				this.mCurSnapData.mCurSnapVal = 0f;
				this.mCurSnapData.mSnapStatus = SnapStatus.SnapMoving;
				this.mCurSnapData.mIsTempTarget = true;
				this.mCurSnapData.mTempTargetIndex = shownItemNearestItemIndex2.ItemIndex;
			}
		}

		// Token: 0x060041BA RID: 16826 RVA: 0x0002EF20 File Offset: 0x0002D120
		public void ClearSnapData()
		{
			this.mCurSnapData.Clear();
		}

		// Token: 0x060041BB RID: 16827 RVA: 0x001C4508 File Offset: 0x001C2708
		public void SetSnapTargetItemIndex(int itemIndex, float moveMaxAbsVec = -1f)
		{
			if (this.mItemTotalCount > 0)
			{
				if (itemIndex >= this.mItemTotalCount)
				{
					itemIndex = this.mItemTotalCount - 1;
				}
				if (itemIndex < 0)
				{
					itemIndex = 0;
				}
			}
			this.mScrollRect.StopMovement();
			this.mCurSnapData.mSnapTargetIndex = itemIndex;
			this.mCurSnapData.mSnapStatus = SnapStatus.TargetHasSet;
			this.mCurSnapData.mIsForceSnapTo = true;
			this.mCurSnapData.mMoveMaxAbsVec = moveMaxAbsVec;
		}

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x060041BC RID: 16828 RVA: 0x0002F0CF File Offset: 0x0002D2CF
		public int CurSnapNearestItemIndex
		{
			get
			{
				return this.mCurSnapNearestItemIndex;
			}
		}

		// Token: 0x060041BD RID: 16829 RVA: 0x0002F0D7 File Offset: 0x0002D2D7
		public void ForceSnapUpdateCheck()
		{
			if (this.mLeftSnapUpdateExtraCount <= 0)
			{
				this.mLeftSnapUpdateExtraCount = 1;
			}
		}

		// Token: 0x060041BE RID: 16830 RVA: 0x001C4574 File Offset: 0x001C2774
		private void UpdateSnapHorizontal(bool immediate = false, bool forceSendEvent = false)
		{
			if (!this.mItemSnapEnable)
			{
				return;
			}
			int count = this.mItemList.Count;
			if (count == 0)
			{
				return;
			}
			Vector3 anchoredPosition3D = this.mContainerTrans.anchoredPosition3D;
			bool flag = anchoredPosition3D.x != this.mLastSnapCheckPos.x;
			this.mLastSnapCheckPos = anchoredPosition3D;
			if (!flag && this.mLeftSnapUpdateExtraCount > 0)
			{
				this.mLeftSnapUpdateExtraCount--;
				flag = true;
			}
			if (flag)
			{
				LoopListViewItem2 loopListViewItem = this.mItemList[0];
				loopListViewItem.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
				int num = -1;
				float num2 = float.MaxValue;
				if (this.mArrangeType == ListItemArrangeType.RightToLeft)
				{
					float num3 = -(1f - this.mViewPortSnapPivot.x) * this.mViewPortRectTransform.rect.width;
					float num4 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[2]).x;
					float num5 = num4 - loopListViewItem.ItemSizeWithPadding;
					float num6 = num4 - loopListViewItem.ItemSize * (1f - this.mItemSnapPivot.x);
					for (int i = 0; i < count; i++)
					{
						float num7 = Mathf.Abs(num3 - num6);
						if (num7 >= num2)
						{
							break;
						}
						num2 = num7;
						num = i;
						if (i + 1 < count)
						{
							num4 = num5;
							num5 -= this.mItemList[i + 1].ItemSizeWithPadding;
							num6 = num4 - this.mItemList[i + 1].ItemSize * (1f - this.mItemSnapPivot.x);
						}
					}
				}
				else if (this.mArrangeType == ListItemArrangeType.LeftToRight)
				{
					float num3 = this.mViewPortSnapPivot.x * this.mViewPortRectTransform.rect.width;
					float num4 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[1]).x;
					float num5 = num4 + loopListViewItem.ItemSizeWithPadding;
					float num6 = num4 + loopListViewItem.ItemSize * this.mItemSnapPivot.x;
					for (int j = 0; j < count; j++)
					{
						float num7 = Mathf.Abs(num3 - num6);
						if (num7 >= num2)
						{
							break;
						}
						num2 = num7;
						num = j;
						if (j + 1 < count)
						{
							num4 = num5;
							num5 += this.mItemList[j + 1].ItemSizeWithPadding;
							num6 = num4 + this.mItemList[j + 1].ItemSize * this.mItemSnapPivot.x;
						}
					}
				}
				if (num >= 0)
				{
					int num8 = this.mCurSnapNearestItemIndex;
					this.mCurSnapNearestItemIndex = this.mItemList[num].ItemIndex;
					if ((forceSendEvent || this.mItemList[num].ItemIndex != num8) && this.mOnSnapNearestChanged != null)
					{
						this.mOnSnapNearestChanged(this, this.mItemList[num]);
					}
				}
				else
				{
					this.mCurSnapNearestItemIndex = -1;
				}
			}
			if (!this.CanSnap())
			{
				this.ClearSnapData();
				return;
			}
			float num9 = Mathf.Abs(this.mScrollRect.velocity.x);
			this.UpdateCurSnapData();
			if (this.mCurSnapData.mSnapStatus != SnapStatus.SnapMoving)
			{
				return;
			}
			if (num9 > 0f)
			{
				this.mScrollRect.StopMovement();
			}
			float mCurSnapVal = this.mCurSnapData.mCurSnapVal;
			if (!this.mCurSnapData.mIsTempTarget)
			{
				if (this.mSmoothDumpVel * this.mCurSnapData.mTargetSnapVal < 0f)
				{
					this.mSmoothDumpVel = 0f;
				}
				this.mCurSnapData.mCurSnapVal = Mathf.SmoothDamp(this.mCurSnapData.mCurSnapVal, this.mCurSnapData.mTargetSnapVal, ref this.mSmoothDumpVel, this.mSmoothDumpRate);
			}
			else
			{
				float mMoveMaxAbsVec = this.mCurSnapData.mMoveMaxAbsVec;
				if (mMoveMaxAbsVec <= 0f)
				{
					mMoveMaxAbsVec = this.mSnapMoveDefaultMaxAbsVec;
				}
				this.mSmoothDumpVel = mMoveMaxAbsVec * Mathf.Sign(this.mCurSnapData.mTargetSnapVal);
				this.mCurSnapData.mCurSnapVal = Mathf.MoveTowards(this.mCurSnapData.mCurSnapVal, this.mCurSnapData.mTargetSnapVal, mMoveMaxAbsVec * Time.deltaTime);
			}
			float num10 = this.mCurSnapData.mCurSnapVal - mCurSnapVal;
			if (immediate || Mathf.Abs(this.mCurSnapData.mTargetSnapVal - this.mCurSnapData.mCurSnapVal) < this.mSnapFinishThreshold)
			{
				anchoredPosition3D.x = anchoredPosition3D.x + this.mCurSnapData.mTargetSnapVal - mCurSnapVal;
				this.mCurSnapData.mSnapStatus = SnapStatus.SnapMoveFinish;
				if (this.mOnSnapItemFinished != null)
				{
					LoopListViewItem2 shownItemByItemIndex = this.GetShownItemByItemIndex(this.mCurSnapNearestItemIndex);
					if (shownItemByItemIndex != null)
					{
						this.mOnSnapItemFinished(this, shownItemByItemIndex);
					}
				}
			}
			else
			{
				anchoredPosition3D.x += num10;
			}
			if (this.mArrangeType == ListItemArrangeType.LeftToRight)
			{
				float num11 = this.mViewPortRectLocalCorners[2].x - this.mContainerTrans.rect.width;
				anchoredPosition3D.x = Mathf.Clamp(anchoredPosition3D.x, num11, 0f);
				this.mContainerTrans.anchoredPosition3D = anchoredPosition3D;
				return;
			}
			if (this.mArrangeType == ListItemArrangeType.RightToLeft)
			{
				float num12 = this.mViewPortRectLocalCorners[1].x + this.mContainerTrans.rect.width;
				anchoredPosition3D.x = Mathf.Clamp(anchoredPosition3D.x, 0f, num12);
				this.mContainerTrans.anchoredPosition3D = anchoredPosition3D;
			}
		}

		// Token: 0x060041BF RID: 16831 RVA: 0x001C4AEC File Offset: 0x001C2CEC
		private bool CanSnap()
		{
			if (this.mIsDraging)
			{
				return false;
			}
			if (this.mScrollBarClickEventListener != null && this.mScrollBarClickEventListener.IsPressd)
			{
				return false;
			}
			if (this.mIsVertList)
			{
				if (this.mContainerTrans.rect.height <= this.ViewPortHeight)
				{
					return false;
				}
			}
			else if (this.mContainerTrans.rect.width <= this.ViewPortWidth)
			{
				return false;
			}
			float num;
			if (this.mIsVertList)
			{
				num = Mathf.Abs(this.mScrollRect.velocity.y);
			}
			else
			{
				num = Mathf.Abs(this.mScrollRect.velocity.x);
			}
			if (num > this.mSnapVecThreshold)
			{
				return false;
			}
			float num2 = 3f;
			Vector3 anchoredPosition3D = this.mContainerTrans.anchoredPosition3D;
			if (this.mArrangeType == ListItemArrangeType.LeftToRight)
			{
				float num3 = this.mViewPortRectLocalCorners[2].x - this.mContainerTrans.rect.width;
				if (anchoredPosition3D.x < num3 - num2 || anchoredPosition3D.x > num2)
				{
					return false;
				}
			}
			else if (this.mArrangeType == ListItemArrangeType.RightToLeft)
			{
				float num4 = this.mViewPortRectLocalCorners[1].x + this.mContainerTrans.rect.width;
				if (anchoredPosition3D.x > num4 + num2 || anchoredPosition3D.x < -num2)
				{
					return false;
				}
			}
			else if (this.mArrangeType == ListItemArrangeType.TopToBottom)
			{
				float num5 = this.mViewPortRectLocalCorners[0].y + this.mContainerTrans.rect.height;
				if (anchoredPosition3D.y > num5 + num2 || anchoredPosition3D.y < -num2)
				{
					return false;
				}
			}
			else if (this.mArrangeType == ListItemArrangeType.BottomToTop)
			{
				float num6 = this.mViewPortRectLocalCorners[1].y - this.mContainerTrans.rect.height;
				if (anchoredPosition3D.y < num6 - num2 || anchoredPosition3D.y > num2)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060041C0 RID: 16832 RVA: 0x001C4CE0 File Offset: 0x001C2EE0
		public void UpdateListView(float distanceForRecycle0, float distanceForRecycle1, float distanceForNew0, float distanceForNew1)
		{
			this.mListUpdateCheckFrameCount++;
			if (this.mIsVertList)
			{
				bool flag = true;
				int num = 0;
				int num2 = 9999;
				while (flag)
				{
					num++;
					if (num >= num2)
					{
						Debug.LogError("UpdateListView Vertical while loop " + num + " times! something is wrong!");
						return;
					}
					flag = this.UpdateForVertList(distanceForRecycle0, distanceForRecycle1, distanceForNew0, distanceForNew1);
				}
				return;
			}
			bool flag2 = true;
			int num3 = 0;
			int num4 = 9999;
			while (flag2)
			{
				num3++;
				if (num3 >= num4)
				{
					Debug.LogError("UpdateListView  Horizontal while loop " + num3 + " times! something is wrong!");
					return;
				}
				flag2 = this.UpdateForHorizontalList(distanceForRecycle0, distanceForRecycle1, distanceForNew0, distanceForNew1);
			}
		}

		// Token: 0x060041C1 RID: 16833 RVA: 0x001C4D88 File Offset: 0x001C2F88
		private bool UpdateForVertList(float distanceForRecycle0, float distanceForRecycle1, float distanceForNew0, float distanceForNew1)
		{
			if (this.mItemTotalCount == 0)
			{
				if (this.mItemList.Count > 0)
				{
					this.RecycleAllItem();
				}
				return false;
			}
			if (this.mArrangeType == ListItemArrangeType.TopToBottom)
			{
				if (this.mItemList.Count == 0)
				{
					float num = this.mContainerTrans.anchoredPosition3D.y;
					if (num < 0f)
					{
						num = 0f;
					}
					int num2 = 0;
					float num3 = -num;
					if (this.mSupportScrollBar)
					{
						if (!this.GetPlusItemIndexAndPosAtGivenPos(num, ref num2, ref num3))
						{
							return false;
						}
						num3 = -num3;
					}
					LoopListViewItem2 newItemByIndex = this.GetNewItemByIndex(num2);
					if (newItemByIndex == null)
					{
						return false;
					}
					if (this.mSupportScrollBar)
					{
						this.SetItemSize(num2, newItemByIndex.CachedRectTransform.rect.height, newItemByIndex.Padding);
					}
					this.mItemList.Add(newItemByIndex);
					newItemByIndex.CachedRectTransform.anchoredPosition3D = new Vector3(newItemByIndex.StartPosOffset, num3, 0f);
					this.UpdateContentSize();
					return true;
				}
				else
				{
					LoopListViewItem2 loopListViewItem = this.mItemList[0];
					loopListViewItem.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
					Vector3 vector = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[1]);
					Vector3 vector2 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[0]);
					if (!this.mIsDraging && loopListViewItem.ItemCreatedCheckFrameCount != this.mListUpdateCheckFrameCount && vector2.y - this.mViewPortRectLocalCorners[1].y > distanceForRecycle0)
					{
						this.mItemList.RemoveAt(0);
						this.RecycleItemTmp(loopListViewItem);
						if (!this.mSupportScrollBar)
						{
							this.UpdateContentSize();
							this.CheckIfNeedUpdataItemPos();
						}
						return true;
					}
					LoopListViewItem2 loopListViewItem2 = this.mItemList[this.mItemList.Count - 1];
					loopListViewItem2.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
					Vector3 vector3 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[1]);
					Vector3 vector4 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[0]);
					if (!this.mIsDraging && loopListViewItem2.ItemCreatedCheckFrameCount != this.mListUpdateCheckFrameCount && this.mViewPortRectLocalCorners[0].y - vector3.y > distanceForRecycle1)
					{
						this.mItemList.RemoveAt(this.mItemList.Count - 1);
						this.RecycleItemTmp(loopListViewItem2);
						if (!this.mSupportScrollBar)
						{
							this.UpdateContentSize();
							this.CheckIfNeedUpdataItemPos();
						}
						return true;
					}
					if (this.mViewPortRectLocalCorners[0].y - vector4.y < distanceForNew1)
					{
						if (loopListViewItem2.ItemIndex > this.mCurReadyMaxItemIndex)
						{
							this.mCurReadyMaxItemIndex = loopListViewItem2.ItemIndex;
							this.mNeedCheckNextMaxItem = true;
						}
						int num4 = loopListViewItem2.ItemIndex + 1;
						if (num4 <= this.mCurReadyMaxItemIndex || this.mNeedCheckNextMaxItem)
						{
							LoopListViewItem2 newItemByIndex2 = this.GetNewItemByIndex(num4);
							if (!(newItemByIndex2 == null))
							{
								if (this.mSupportScrollBar)
								{
									this.SetItemSize(num4, newItemByIndex2.CachedRectTransform.rect.height, newItemByIndex2.Padding);
								}
								this.mItemList.Add(newItemByIndex2);
								float num5 = loopListViewItem2.CachedRectTransform.anchoredPosition3D.y - loopListViewItem2.CachedRectTransform.rect.height - loopListViewItem2.Padding;
								newItemByIndex2.CachedRectTransform.anchoredPosition3D = new Vector3(newItemByIndex2.StartPosOffset, num5, 0f);
								this.UpdateContentSize();
								this.CheckIfNeedUpdataItemPos();
								if (num4 > this.mCurReadyMaxItemIndex)
								{
									this.mCurReadyMaxItemIndex = num4;
								}
								return true;
							}
							this.mCurReadyMaxItemIndex = loopListViewItem2.ItemIndex;
							this.mNeedCheckNextMaxItem = false;
							this.CheckIfNeedUpdataItemPos();
						}
					}
					if (vector.y - this.mViewPortRectLocalCorners[1].y < distanceForNew0)
					{
						if (loopListViewItem.ItemIndex < this.mCurReadyMinItemIndex)
						{
							this.mCurReadyMinItemIndex = loopListViewItem.ItemIndex;
							this.mNeedCheckNextMinItem = true;
						}
						int num6 = loopListViewItem.ItemIndex - 1;
						if (num6 >= this.mCurReadyMinItemIndex || this.mNeedCheckNextMinItem)
						{
							LoopListViewItem2 newItemByIndex3 = this.GetNewItemByIndex(num6);
							if (!(newItemByIndex3 == null))
							{
								if (this.mSupportScrollBar)
								{
									this.SetItemSize(num6, newItemByIndex3.CachedRectTransform.rect.height, newItemByIndex3.Padding);
								}
								this.mItemList.Insert(0, newItemByIndex3);
								float num7 = loopListViewItem.CachedRectTransform.anchoredPosition3D.y + newItemByIndex3.CachedRectTransform.rect.height + newItemByIndex3.Padding;
								newItemByIndex3.CachedRectTransform.anchoredPosition3D = new Vector3(newItemByIndex3.StartPosOffset, num7, 0f);
								this.UpdateContentSize();
								this.CheckIfNeedUpdataItemPos();
								if (num6 < this.mCurReadyMinItemIndex)
								{
									this.mCurReadyMinItemIndex = num6;
								}
								return true;
							}
							this.mCurReadyMinItemIndex = loopListViewItem.ItemIndex;
							this.mNeedCheckNextMinItem = false;
						}
					}
				}
			}
			else if (this.mItemList.Count == 0)
			{
				float num8 = this.mContainerTrans.anchoredPosition3D.y;
				if (num8 > 0f)
				{
					num8 = 0f;
				}
				int num9 = 0;
				float num10 = -num8;
				if (this.mSupportScrollBar && !this.GetPlusItemIndexAndPosAtGivenPos(-num8, ref num9, ref num10))
				{
					return false;
				}
				LoopListViewItem2 newItemByIndex4 = this.GetNewItemByIndex(num9);
				if (newItemByIndex4 == null)
				{
					return false;
				}
				if (this.mSupportScrollBar)
				{
					this.SetItemSize(num9, newItemByIndex4.CachedRectTransform.rect.height, newItemByIndex4.Padding);
				}
				this.mItemList.Add(newItemByIndex4);
				newItemByIndex4.CachedRectTransform.anchoredPosition3D = new Vector3(newItemByIndex4.StartPosOffset, num10, 0f);
				this.UpdateContentSize();
				return true;
			}
			else
			{
				LoopListViewItem2 loopListViewItem3 = this.mItemList[0];
				loopListViewItem3.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
				Vector3 vector5 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[1]);
				Vector3 vector6 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[0]);
				if (!this.mIsDraging && loopListViewItem3.ItemCreatedCheckFrameCount != this.mListUpdateCheckFrameCount && this.mViewPortRectLocalCorners[0].y - vector5.y > distanceForRecycle0)
				{
					this.mItemList.RemoveAt(0);
					this.RecycleItemTmp(loopListViewItem3);
					if (!this.mSupportScrollBar)
					{
						this.UpdateContentSize();
						this.CheckIfNeedUpdataItemPos();
					}
					return true;
				}
				LoopListViewItem2 loopListViewItem4 = this.mItemList[this.mItemList.Count - 1];
				loopListViewItem4.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
				Vector3 vector7 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[1]);
				Vector3 vector8 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[0]);
				if (!this.mIsDraging && loopListViewItem4.ItemCreatedCheckFrameCount != this.mListUpdateCheckFrameCount && vector8.y - this.mViewPortRectLocalCorners[1].y > distanceForRecycle1)
				{
					this.mItemList.RemoveAt(this.mItemList.Count - 1);
					this.RecycleItemTmp(loopListViewItem4);
					if (!this.mSupportScrollBar)
					{
						this.UpdateContentSize();
						this.CheckIfNeedUpdataItemPos();
					}
					return true;
				}
				if (vector7.y - this.mViewPortRectLocalCorners[1].y < distanceForNew1)
				{
					if (loopListViewItem4.ItemIndex > this.mCurReadyMaxItemIndex)
					{
						this.mCurReadyMaxItemIndex = loopListViewItem4.ItemIndex;
						this.mNeedCheckNextMaxItem = true;
					}
					int num11 = loopListViewItem4.ItemIndex + 1;
					if (num11 <= this.mCurReadyMaxItemIndex || this.mNeedCheckNextMaxItem)
					{
						LoopListViewItem2 newItemByIndex5 = this.GetNewItemByIndex(num11);
						if (!(newItemByIndex5 == null))
						{
							if (this.mSupportScrollBar)
							{
								this.SetItemSize(num11, newItemByIndex5.CachedRectTransform.rect.height, newItemByIndex5.Padding);
							}
							this.mItemList.Add(newItemByIndex5);
							float num12 = loopListViewItem4.CachedRectTransform.anchoredPosition3D.y + loopListViewItem4.CachedRectTransform.rect.height + loopListViewItem4.Padding;
							newItemByIndex5.CachedRectTransform.anchoredPosition3D = new Vector3(newItemByIndex5.StartPosOffset, num12, 0f);
							this.UpdateContentSize();
							this.CheckIfNeedUpdataItemPos();
							if (num11 > this.mCurReadyMaxItemIndex)
							{
								this.mCurReadyMaxItemIndex = num11;
							}
							return true;
						}
						this.mNeedCheckNextMaxItem = false;
						this.CheckIfNeedUpdataItemPos();
					}
				}
				if (this.mViewPortRectLocalCorners[0].y - vector6.y < distanceForNew0)
				{
					if (loopListViewItem3.ItemIndex < this.mCurReadyMinItemIndex)
					{
						this.mCurReadyMinItemIndex = loopListViewItem3.ItemIndex;
						this.mNeedCheckNextMinItem = true;
					}
					int num13 = loopListViewItem3.ItemIndex - 1;
					if (num13 >= this.mCurReadyMinItemIndex || this.mNeedCheckNextMinItem)
					{
						LoopListViewItem2 newItemByIndex6 = this.GetNewItemByIndex(num13);
						if (newItemByIndex6 == null)
						{
							this.mNeedCheckNextMinItem = false;
							return false;
						}
						if (this.mSupportScrollBar)
						{
							this.SetItemSize(num13, newItemByIndex6.CachedRectTransform.rect.height, newItemByIndex6.Padding);
						}
						this.mItemList.Insert(0, newItemByIndex6);
						float num14 = loopListViewItem3.CachedRectTransform.anchoredPosition3D.y - newItemByIndex6.CachedRectTransform.rect.height - newItemByIndex6.Padding;
						newItemByIndex6.CachedRectTransform.anchoredPosition3D = new Vector3(newItemByIndex6.StartPosOffset, num14, 0f);
						this.UpdateContentSize();
						this.CheckIfNeedUpdataItemPos();
						if (num13 < this.mCurReadyMinItemIndex)
						{
							this.mCurReadyMinItemIndex = num13;
						}
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060041C2 RID: 16834 RVA: 0x001C56FC File Offset: 0x001C38FC
		private bool UpdateForHorizontalList(float distanceForRecycle0, float distanceForRecycle1, float distanceForNew0, float distanceForNew1)
		{
			if (this.mItemTotalCount == 0)
			{
				if (this.mItemList.Count > 0)
				{
					this.RecycleAllItem();
				}
				return false;
			}
			if (this.mArrangeType == ListItemArrangeType.LeftToRight)
			{
				if (this.mItemList.Count == 0)
				{
					float num = this.mContainerTrans.anchoredPosition3D.x;
					if (num > 0f)
					{
						num = 0f;
					}
					int num2 = 0;
					float num3 = -num;
					if (this.mSupportScrollBar && !this.GetPlusItemIndexAndPosAtGivenPos(-num, ref num2, ref num3))
					{
						return false;
					}
					LoopListViewItem2 newItemByIndex = this.GetNewItemByIndex(num2);
					if (newItemByIndex == null)
					{
						return false;
					}
					if (this.mSupportScrollBar)
					{
						this.SetItemSize(num2, newItemByIndex.CachedRectTransform.rect.width, newItemByIndex.Padding);
					}
					this.mItemList.Add(newItemByIndex);
					newItemByIndex.CachedRectTransform.anchoredPosition3D = new Vector3(num3, newItemByIndex.StartPosOffset, 0f);
					this.UpdateContentSize();
					return true;
				}
				else
				{
					LoopListViewItem2 loopListViewItem = this.mItemList[0];
					loopListViewItem.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
					Vector3 vector = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[1]);
					Vector3 vector2 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[2]);
					if (!this.mIsDraging && loopListViewItem.ItemCreatedCheckFrameCount != this.mListUpdateCheckFrameCount && this.mViewPortRectLocalCorners[1].x - vector2.x > distanceForRecycle0)
					{
						this.mItemList.RemoveAt(0);
						this.RecycleItemTmp(loopListViewItem);
						if (!this.mSupportScrollBar)
						{
							this.UpdateContentSize();
							this.CheckIfNeedUpdataItemPos();
						}
						return true;
					}
					LoopListViewItem2 loopListViewItem2 = this.mItemList[this.mItemList.Count - 1];
					loopListViewItem2.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
					Vector3 vector3 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[1]);
					Vector3 vector4 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[2]);
					if (!this.mIsDraging && loopListViewItem2.ItemCreatedCheckFrameCount != this.mListUpdateCheckFrameCount && vector3.x - this.mViewPortRectLocalCorners[2].x > distanceForRecycle1)
					{
						this.mItemList.RemoveAt(this.mItemList.Count - 1);
						this.RecycleItemTmp(loopListViewItem2);
						if (!this.mSupportScrollBar)
						{
							this.UpdateContentSize();
							this.CheckIfNeedUpdataItemPos();
						}
						return true;
					}
					if (vector4.x - this.mViewPortRectLocalCorners[2].x < distanceForNew1)
					{
						if (loopListViewItem2.ItemIndex > this.mCurReadyMaxItemIndex)
						{
							this.mCurReadyMaxItemIndex = loopListViewItem2.ItemIndex;
							this.mNeedCheckNextMaxItem = true;
						}
						int num4 = loopListViewItem2.ItemIndex + 1;
						if (num4 <= this.mCurReadyMaxItemIndex || this.mNeedCheckNextMaxItem)
						{
							LoopListViewItem2 newItemByIndex2 = this.GetNewItemByIndex(num4);
							if (!(newItemByIndex2 == null))
							{
								if (this.mSupportScrollBar)
								{
									this.SetItemSize(num4, newItemByIndex2.CachedRectTransform.rect.width, newItemByIndex2.Padding);
								}
								this.mItemList.Add(newItemByIndex2);
								float num5 = loopListViewItem2.CachedRectTransform.anchoredPosition3D.x + loopListViewItem2.CachedRectTransform.rect.width + loopListViewItem2.Padding;
								newItemByIndex2.CachedRectTransform.anchoredPosition3D = new Vector3(num5, newItemByIndex2.StartPosOffset, 0f);
								this.UpdateContentSize();
								this.CheckIfNeedUpdataItemPos();
								if (num4 > this.mCurReadyMaxItemIndex)
								{
									this.mCurReadyMaxItemIndex = num4;
								}
								return true;
							}
							this.mCurReadyMaxItemIndex = loopListViewItem2.ItemIndex;
							this.mNeedCheckNextMaxItem = false;
							this.CheckIfNeedUpdataItemPos();
						}
					}
					if (this.mViewPortRectLocalCorners[1].x - vector.x < distanceForNew0)
					{
						if (loopListViewItem.ItemIndex < this.mCurReadyMinItemIndex)
						{
							this.mCurReadyMinItemIndex = loopListViewItem.ItemIndex;
							this.mNeedCheckNextMinItem = true;
						}
						int num6 = loopListViewItem.ItemIndex - 1;
						if (num6 >= this.mCurReadyMinItemIndex || this.mNeedCheckNextMinItem)
						{
							LoopListViewItem2 newItemByIndex3 = this.GetNewItemByIndex(num6);
							if (!(newItemByIndex3 == null))
							{
								if (this.mSupportScrollBar)
								{
									this.SetItemSize(num6, newItemByIndex3.CachedRectTransform.rect.width, newItemByIndex3.Padding);
								}
								this.mItemList.Insert(0, newItemByIndex3);
								float num7 = loopListViewItem.CachedRectTransform.anchoredPosition3D.x - newItemByIndex3.CachedRectTransform.rect.width - newItemByIndex3.Padding;
								newItemByIndex3.CachedRectTransform.anchoredPosition3D = new Vector3(num7, newItemByIndex3.StartPosOffset, 0f);
								this.UpdateContentSize();
								this.CheckIfNeedUpdataItemPos();
								if (num6 < this.mCurReadyMinItemIndex)
								{
									this.mCurReadyMinItemIndex = num6;
								}
								return true;
							}
							this.mCurReadyMinItemIndex = loopListViewItem.ItemIndex;
							this.mNeedCheckNextMinItem = false;
						}
					}
				}
			}
			else if (this.mItemList.Count == 0)
			{
				float num8 = this.mContainerTrans.anchoredPosition3D.x;
				if (num8 < 0f)
				{
					num8 = 0f;
				}
				int num9 = 0;
				float num10 = -num8;
				if (this.mSupportScrollBar)
				{
					if (!this.GetPlusItemIndexAndPosAtGivenPos(num8, ref num9, ref num10))
					{
						return false;
					}
					num10 = -num10;
				}
				LoopListViewItem2 newItemByIndex4 = this.GetNewItemByIndex(num9);
				if (newItemByIndex4 == null)
				{
					return false;
				}
				if (this.mSupportScrollBar)
				{
					this.SetItemSize(num9, newItemByIndex4.CachedRectTransform.rect.width, newItemByIndex4.Padding);
				}
				this.mItemList.Add(newItemByIndex4);
				newItemByIndex4.CachedRectTransform.anchoredPosition3D = new Vector3(num10, newItemByIndex4.StartPosOffset, 0f);
				this.UpdateContentSize();
				return true;
			}
			else
			{
				LoopListViewItem2 loopListViewItem3 = this.mItemList[0];
				loopListViewItem3.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
				Vector3 vector5 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[1]);
				Vector3 vector6 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[2]);
				if (!this.mIsDraging && loopListViewItem3.ItemCreatedCheckFrameCount != this.mListUpdateCheckFrameCount && vector5.x - this.mViewPortRectLocalCorners[2].x > distanceForRecycle0)
				{
					this.mItemList.RemoveAt(0);
					this.RecycleItemTmp(loopListViewItem3);
					if (!this.mSupportScrollBar)
					{
						this.UpdateContentSize();
						this.CheckIfNeedUpdataItemPos();
					}
					return true;
				}
				LoopListViewItem2 loopListViewItem4 = this.mItemList[this.mItemList.Count - 1];
				loopListViewItem4.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
				Vector3 vector7 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[1]);
				Vector3 vector8 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[2]);
				if (!this.mIsDraging && loopListViewItem4.ItemCreatedCheckFrameCount != this.mListUpdateCheckFrameCount && this.mViewPortRectLocalCorners[1].x - vector8.x > distanceForRecycle1)
				{
					this.mItemList.RemoveAt(this.mItemList.Count - 1);
					this.RecycleItemTmp(loopListViewItem4);
					if (!this.mSupportScrollBar)
					{
						this.UpdateContentSize();
						this.CheckIfNeedUpdataItemPos();
					}
					return true;
				}
				if (this.mViewPortRectLocalCorners[1].x - vector7.x < distanceForNew1)
				{
					if (loopListViewItem4.ItemIndex > this.mCurReadyMaxItemIndex)
					{
						this.mCurReadyMaxItemIndex = loopListViewItem4.ItemIndex;
						this.mNeedCheckNextMaxItem = true;
					}
					int num11 = loopListViewItem4.ItemIndex + 1;
					if (num11 <= this.mCurReadyMaxItemIndex || this.mNeedCheckNextMaxItem)
					{
						LoopListViewItem2 newItemByIndex5 = this.GetNewItemByIndex(num11);
						if (!(newItemByIndex5 == null))
						{
							if (this.mSupportScrollBar)
							{
								this.SetItemSize(num11, newItemByIndex5.CachedRectTransform.rect.width, newItemByIndex5.Padding);
							}
							this.mItemList.Add(newItemByIndex5);
							float num12 = loopListViewItem4.CachedRectTransform.anchoredPosition3D.x - loopListViewItem4.CachedRectTransform.rect.width - loopListViewItem4.Padding;
							newItemByIndex5.CachedRectTransform.anchoredPosition3D = new Vector3(num12, newItemByIndex5.StartPosOffset, 0f);
							this.UpdateContentSize();
							this.CheckIfNeedUpdataItemPos();
							if (num11 > this.mCurReadyMaxItemIndex)
							{
								this.mCurReadyMaxItemIndex = num11;
							}
							return true;
						}
						this.mCurReadyMaxItemIndex = loopListViewItem4.ItemIndex;
						this.mNeedCheckNextMaxItem = false;
						this.CheckIfNeedUpdataItemPos();
					}
				}
				if (vector6.x - this.mViewPortRectLocalCorners[2].x < distanceForNew0)
				{
					if (loopListViewItem3.ItemIndex < this.mCurReadyMinItemIndex)
					{
						this.mCurReadyMinItemIndex = loopListViewItem3.ItemIndex;
						this.mNeedCheckNextMinItem = true;
					}
					int num13 = loopListViewItem3.ItemIndex - 1;
					if (num13 >= this.mCurReadyMinItemIndex || this.mNeedCheckNextMinItem)
					{
						LoopListViewItem2 newItemByIndex6 = this.GetNewItemByIndex(num13);
						if (!(newItemByIndex6 == null))
						{
							if (this.mSupportScrollBar)
							{
								this.SetItemSize(num13, newItemByIndex6.CachedRectTransform.rect.width, newItemByIndex6.Padding);
							}
							this.mItemList.Insert(0, newItemByIndex6);
							float num14 = loopListViewItem3.CachedRectTransform.anchoredPosition3D.x + newItemByIndex6.CachedRectTransform.rect.width + newItemByIndex6.Padding;
							newItemByIndex6.CachedRectTransform.anchoredPosition3D = new Vector3(num14, newItemByIndex6.StartPosOffset, 0f);
							this.UpdateContentSize();
							this.CheckIfNeedUpdataItemPos();
							if (num13 < this.mCurReadyMinItemIndex)
							{
								this.mCurReadyMinItemIndex = num13;
							}
							return true;
						}
						this.mCurReadyMinItemIndex = loopListViewItem3.ItemIndex;
						this.mNeedCheckNextMinItem = false;
					}
				}
			}
			return false;
		}

		// Token: 0x060041C3 RID: 16835 RVA: 0x001C6090 File Offset: 0x001C4290
		private float GetContentPanelSize()
		{
			if (this.mSupportScrollBar)
			{
				float num = (this.mItemPosMgr.mTotalSize > 0f) ? (this.mItemPosMgr.mTotalSize - this.mLastItemPadding) : 0f;
				if (num < 0f)
				{
					num = 0f;
				}
				return num;
			}
			int count = this.mItemList.Count;
			if (count == 0)
			{
				return 0f;
			}
			if (count == 1)
			{
				return this.mItemList[0].ItemSize;
			}
			if (count == 2)
			{
				return this.mItemList[0].ItemSizeWithPadding + this.mItemList[1].ItemSize;
			}
			float num2 = 0f;
			for (int i = 0; i < count - 1; i++)
			{
				num2 += this.mItemList[i].ItemSizeWithPadding;
			}
			return num2 + this.mItemList[count - 1].ItemSize;
		}

		// Token: 0x060041C4 RID: 16836 RVA: 0x001C6174 File Offset: 0x001C4374
		private void CheckIfNeedUpdataItemPos()
		{
			if (this.mItemList.Count == 0)
			{
				return;
			}
			if (this.mArrangeType == ListItemArrangeType.TopToBottom)
			{
				LoopListViewItem2 loopListViewItem = this.mItemList[0];
				LoopListViewItem2 loopListViewItem2 = this.mItemList[this.mItemList.Count - 1];
				float contentPanelSize = this.GetContentPanelSize();
				if (loopListViewItem.TopY > 0f || (loopListViewItem.ItemIndex == this.mCurReadyMinItemIndex && loopListViewItem.TopY != 0f))
				{
					this.UpdateAllShownItemsPos();
					return;
				}
				if (-loopListViewItem2.BottomY > contentPanelSize || (loopListViewItem2.ItemIndex == this.mCurReadyMaxItemIndex && -loopListViewItem2.BottomY != contentPanelSize))
				{
					this.UpdateAllShownItemsPos();
					return;
				}
			}
			else if (this.mArrangeType == ListItemArrangeType.BottomToTop)
			{
				LoopListViewItem2 loopListViewItem3 = this.mItemList[0];
				LoopListViewItem2 loopListViewItem4 = this.mItemList[this.mItemList.Count - 1];
				float contentPanelSize2 = this.GetContentPanelSize();
				if (loopListViewItem3.BottomY < 0f || (loopListViewItem3.ItemIndex == this.mCurReadyMinItemIndex && loopListViewItem3.BottomY != 0f))
				{
					this.UpdateAllShownItemsPos();
					return;
				}
				if (loopListViewItem4.TopY > contentPanelSize2 || (loopListViewItem4.ItemIndex == this.mCurReadyMaxItemIndex && loopListViewItem4.TopY != contentPanelSize2))
				{
					this.UpdateAllShownItemsPos();
					return;
				}
			}
			else if (this.mArrangeType == ListItemArrangeType.LeftToRight)
			{
				LoopListViewItem2 loopListViewItem5 = this.mItemList[0];
				LoopListViewItem2 loopListViewItem6 = this.mItemList[this.mItemList.Count - 1];
				float contentPanelSize3 = this.GetContentPanelSize();
				if (loopListViewItem5.LeftX < 0f || (loopListViewItem5.ItemIndex == this.mCurReadyMinItemIndex && loopListViewItem5.LeftX != 0f))
				{
					this.UpdateAllShownItemsPos();
					return;
				}
				if (loopListViewItem6.RightX > contentPanelSize3 || (loopListViewItem6.ItemIndex == this.mCurReadyMaxItemIndex && loopListViewItem6.RightX != contentPanelSize3))
				{
					this.UpdateAllShownItemsPos();
					return;
				}
			}
			else if (this.mArrangeType == ListItemArrangeType.RightToLeft)
			{
				LoopListViewItem2 loopListViewItem7 = this.mItemList[0];
				LoopListViewItem2 loopListViewItem8 = this.mItemList[this.mItemList.Count - 1];
				float contentPanelSize4 = this.GetContentPanelSize();
				if (loopListViewItem7.RightX > 0f || (loopListViewItem7.ItemIndex == this.mCurReadyMinItemIndex && loopListViewItem7.RightX != 0f))
				{
					this.UpdateAllShownItemsPos();
					return;
				}
				if (-loopListViewItem8.LeftX > contentPanelSize4 || (loopListViewItem8.ItemIndex == this.mCurReadyMaxItemIndex && -loopListViewItem8.LeftX != contentPanelSize4))
				{
					this.UpdateAllShownItemsPos();
					return;
				}
			}
		}

		// Token: 0x060041C5 RID: 16837 RVA: 0x001C6400 File Offset: 0x001C4600
		private void UpdateAllShownItemsPos()
		{
			int count = this.mItemList.Count;
			if (count == 0)
			{
				return;
			}
			this.mAdjustedVec = (this.mContainerTrans.anchoredPosition3D - this.mLastFrameContainerPos) / Time.deltaTime;
			if (this.mArrangeType == ListItemArrangeType.TopToBottom)
			{
				float num = 0f;
				if (this.mSupportScrollBar)
				{
					num = -this.GetItemPos(this.mItemList[0].ItemIndex);
				}
				float y = this.mItemList[0].CachedRectTransform.anchoredPosition3D.y;
				float num2 = num - y;
				float num3 = num;
				for (int i = 0; i < count; i++)
				{
					LoopListViewItem2 loopListViewItem = this.mItemList[i];
					loopListViewItem.CachedRectTransform.anchoredPosition3D = new Vector3(loopListViewItem.StartPosOffset, num3, 0f);
					num3 = num3 - loopListViewItem.CachedRectTransform.rect.height - loopListViewItem.Padding;
				}
				if (num2 != 0f)
				{
					Vector2 vector = this.mContainerTrans.anchoredPosition3D;
					vector.y -= num2;
					this.mContainerTrans.anchoredPosition3D = vector;
				}
			}
			else if (this.mArrangeType == ListItemArrangeType.BottomToTop)
			{
				float num4 = 0f;
				if (this.mSupportScrollBar)
				{
					num4 = this.GetItemPos(this.mItemList[0].ItemIndex);
				}
				float y2 = this.mItemList[0].CachedRectTransform.anchoredPosition3D.y;
				float num5 = num4 - y2;
				float num6 = num4;
				for (int j = 0; j < count; j++)
				{
					LoopListViewItem2 loopListViewItem2 = this.mItemList[j];
					loopListViewItem2.CachedRectTransform.anchoredPosition3D = new Vector3(loopListViewItem2.StartPosOffset, num6, 0f);
					num6 = num6 + loopListViewItem2.CachedRectTransform.rect.height + loopListViewItem2.Padding;
				}
				if (num5 != 0f)
				{
					Vector3 anchoredPosition3D = this.mContainerTrans.anchoredPosition3D;
					anchoredPosition3D.y -= num5;
					this.mContainerTrans.anchoredPosition3D = anchoredPosition3D;
				}
			}
			else if (this.mArrangeType == ListItemArrangeType.LeftToRight)
			{
				float num7 = 0f;
				if (this.mSupportScrollBar)
				{
					num7 = this.GetItemPos(this.mItemList[0].ItemIndex);
				}
				float x = this.mItemList[0].CachedRectTransform.anchoredPosition3D.x;
				float num8 = num7 - x;
				float num9 = num7;
				for (int k = 0; k < count; k++)
				{
					LoopListViewItem2 loopListViewItem3 = this.mItemList[k];
					loopListViewItem3.CachedRectTransform.anchoredPosition3D = new Vector3(num9, loopListViewItem3.StartPosOffset, 0f);
					num9 = num9 + loopListViewItem3.CachedRectTransform.rect.width + loopListViewItem3.Padding;
				}
				if (num8 != 0f)
				{
					Vector3 anchoredPosition3D2 = this.mContainerTrans.anchoredPosition3D;
					anchoredPosition3D2.x -= num8;
					this.mContainerTrans.anchoredPosition3D = anchoredPosition3D2;
				}
			}
			else if (this.mArrangeType == ListItemArrangeType.RightToLeft)
			{
				float num10 = 0f;
				if (this.mSupportScrollBar)
				{
					num10 = -this.GetItemPos(this.mItemList[0].ItemIndex);
				}
				float x2 = this.mItemList[0].CachedRectTransform.anchoredPosition3D.x;
				float num11 = num10 - x2;
				float num12 = num10;
				for (int l = 0; l < count; l++)
				{
					LoopListViewItem2 loopListViewItem4 = this.mItemList[l];
					loopListViewItem4.CachedRectTransform.anchoredPosition3D = new Vector3(num12, loopListViewItem4.StartPosOffset, 0f);
					num12 = num12 - loopListViewItem4.CachedRectTransform.rect.width - loopListViewItem4.Padding;
				}
				if (num11 != 0f)
				{
					Vector3 anchoredPosition3D3 = this.mContainerTrans.anchoredPosition3D;
					anchoredPosition3D3.x -= num11;
					this.mContainerTrans.anchoredPosition3D = anchoredPosition3D3;
				}
			}
			if (this.mIsDraging)
			{
				this.mScrollRect.OnBeginDrag(this.mPointerEventData);
				this.mScrollRect.Rebuild(2);
				this.mScrollRect.velocity = this.mAdjustedVec;
				this.mNeedAdjustVec = true;
			}
		}

		// Token: 0x060041C6 RID: 16838 RVA: 0x001C6860 File Offset: 0x001C4A60
		private void UpdateContentSize()
		{
			float contentPanelSize = this.GetContentPanelSize();
			if (this.mIsVertList)
			{
				if (this.mContainerTrans.rect.height != contentPanelSize)
				{
					this.mContainerTrans.SetSizeWithCurrentAnchors(1, contentPanelSize);
					return;
				}
			}
			else if (this.mContainerTrans.rect.width != contentPanelSize)
			{
				this.mContainerTrans.SetSizeWithCurrentAnchors(0, contentPanelSize);
			}
		}

		// Token: 0x04003A38 RID: 14904
		private Dictionary<string, ItemPool> mItemPoolDict = new Dictionary<string, ItemPool>();

		// Token: 0x04003A39 RID: 14905
		private List<ItemPool> mItemPoolList = new List<ItemPool>();

		// Token: 0x04003A3A RID: 14906
		[SerializeField]
		private List<ItemPrefabConfData> mItemPrefabDataList = new List<ItemPrefabConfData>();

		// Token: 0x04003A3B RID: 14907
		[SerializeField]
		private ListItemArrangeType mArrangeType;

		// Token: 0x04003A3C RID: 14908
		private List<LoopListViewItem2> mItemList = new List<LoopListViewItem2>();

		// Token: 0x04003A3D RID: 14909
		private RectTransform mContainerTrans;

		// Token: 0x04003A3E RID: 14910
		private ScrollRect mScrollRect;

		// Token: 0x04003A3F RID: 14911
		private RectTransform mScrollRectTransform;

		// Token: 0x04003A40 RID: 14912
		private RectTransform mViewPortRectTransform;

		// Token: 0x04003A41 RID: 14913
		private float mItemDefaultWithPaddingSize = 20f;

		// Token: 0x04003A42 RID: 14914
		private int mItemTotalCount;

		// Token: 0x04003A43 RID: 14915
		private bool mIsVertList;

		// Token: 0x04003A44 RID: 14916
		private Func<LoopListView2, int, LoopListViewItem2> mOnGetItemByIndex;

		// Token: 0x04003A45 RID: 14917
		private Vector3[] mItemWorldCorners = new Vector3[4];

		// Token: 0x04003A46 RID: 14918
		private Vector3[] mViewPortRectLocalCorners = new Vector3[4];

		// Token: 0x04003A47 RID: 14919
		private int mCurReadyMinItemIndex;

		// Token: 0x04003A48 RID: 14920
		private int mCurReadyMaxItemIndex;

		// Token: 0x04003A49 RID: 14921
		private bool mNeedCheckNextMinItem = true;

		// Token: 0x04003A4A RID: 14922
		private bool mNeedCheckNextMaxItem = true;

		// Token: 0x04003A4B RID: 14923
		private ItemPosMgr mItemPosMgr;

		// Token: 0x04003A4C RID: 14924
		private float mDistanceForRecycle0 = 300f;

		// Token: 0x04003A4D RID: 14925
		private float mDistanceForNew0 = 200f;

		// Token: 0x04003A4E RID: 14926
		private float mDistanceForRecycle1 = 300f;

		// Token: 0x04003A4F RID: 14927
		private float mDistanceForNew1 = 200f;

		// Token: 0x04003A50 RID: 14928
		[SerializeField]
		private bool mSupportScrollBar = true;

		// Token: 0x04003A51 RID: 14929
		private bool mIsDraging;

		// Token: 0x04003A52 RID: 14930
		private PointerEventData mPointerEventData;

		// Token: 0x04003A53 RID: 14931
		public Action mOnBeginDragAction;

		// Token: 0x04003A54 RID: 14932
		public Action mOnDragingAction;

		// Token: 0x04003A55 RID: 14933
		public Action mOnEndDragAction;

		// Token: 0x04003A56 RID: 14934
		private int mLastItemIndex;

		// Token: 0x04003A57 RID: 14935
		private float mLastItemPadding;

		// Token: 0x04003A58 RID: 14936
		private float mSmoothDumpVel;

		// Token: 0x04003A59 RID: 14937
		private float mSmoothDumpRate = 0.3f;

		// Token: 0x04003A5A RID: 14938
		private float mSnapFinishThreshold = 0.1f;

		// Token: 0x04003A5B RID: 14939
		private float mSnapVecThreshold = 145f;

		// Token: 0x04003A5C RID: 14940
		private float mSnapMoveDefaultMaxAbsVec = 3400f;

		// Token: 0x04003A5D RID: 14941
		[SerializeField]
		private bool mItemSnapEnable;

		// Token: 0x04003A5E RID: 14942
		private Vector3 mLastFrameContainerPos = Vector3.zero;

		// Token: 0x04003A5F RID: 14943
		public Action<LoopListView2, LoopListViewItem2> mOnSnapItemFinished;

		// Token: 0x04003A60 RID: 14944
		public Action<LoopListView2, LoopListViewItem2> mOnSnapNearestChanged;

		// Token: 0x04003A61 RID: 14945
		private int mCurSnapNearestItemIndex = -1;

		// Token: 0x04003A62 RID: 14946
		private Vector2 mAdjustedVec;

		// Token: 0x04003A63 RID: 14947
		private bool mNeedAdjustVec;

		// Token: 0x04003A64 RID: 14948
		private int mLeftSnapUpdateExtraCount = 1;

		// Token: 0x04003A65 RID: 14949
		[SerializeField]
		private Vector2 mViewPortSnapPivot = Vector2.zero;

		// Token: 0x04003A66 RID: 14950
		[SerializeField]
		private Vector2 mItemSnapPivot = Vector2.zero;

		// Token: 0x04003A67 RID: 14951
		private ClickEventListener mScrollBarClickEventListener;

		// Token: 0x04003A68 RID: 14952
		private LoopListView2.SnapData mCurSnapData = new LoopListView2.SnapData();

		// Token: 0x04003A69 RID: 14953
		private Vector3 mLastSnapCheckPos = Vector3.zero;

		// Token: 0x04003A6A RID: 14954
		private bool mListViewInited;

		// Token: 0x04003A6B RID: 14955
		private int mListUpdateCheckFrameCount;

		// Token: 0x04003A6C RID: 14956
		public Action<LoopListView2> OnListViewStart;

		// Token: 0x020009FC RID: 2556
		private class SnapData
		{
			// Token: 0x060041C8 RID: 16840 RVA: 0x0002F0E9 File Offset: 0x0002D2E9
			public void Clear()
			{
				this.mSnapStatus = SnapStatus.NoTargetSet;
				this.mTempTargetIndex = -1;
				this.mIsForceSnapTo = false;
				this.mMoveMaxAbsVec = -1f;
			}

			// Token: 0x04003A6D RID: 14957
			public SnapStatus mSnapStatus;

			// Token: 0x04003A6E RID: 14958
			public int mSnapTargetIndex;

			// Token: 0x04003A6F RID: 14959
			public float mTargetSnapVal;

			// Token: 0x04003A70 RID: 14960
			public float mCurSnapVal;

			// Token: 0x04003A71 RID: 14961
			public bool mIsForceSnapTo;

			// Token: 0x04003A72 RID: 14962
			public bool mIsTempTarget;

			// Token: 0x04003A73 RID: 14963
			public int mTempTargetIndex = -1;

			// Token: 0x04003A74 RID: 14964
			public float mMoveMaxAbsVec = -1f;
		}
	}
}
