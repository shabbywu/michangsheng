using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SuperScrollView
{
	// Token: 0x020006CE RID: 1742
	public class LoopListView2 : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IEndDragHandler, IDragHandler
	{
		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06003759 RID: 14169 RVA: 0x00179B07 File Offset: 0x00177D07
		// (set) Token: 0x0600375A RID: 14170 RVA: 0x00179B0F File Offset: 0x00177D0F
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

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x0600375B RID: 14171 RVA: 0x00179B18 File Offset: 0x00177D18
		public List<ItemPrefabConfData> ItemPrefabDataList
		{
			get
			{
				return this.mItemPrefabDataList;
			}
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x0600375C RID: 14172 RVA: 0x00179B20 File Offset: 0x00177D20
		public List<LoopListViewItem2> ItemList
		{
			get
			{
				return this.mItemList;
			}
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x0600375D RID: 14173 RVA: 0x00179B28 File Offset: 0x00177D28
		public bool IsVertList
		{
			get
			{
				return this.mIsVertList;
			}
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x0600375E RID: 14174 RVA: 0x00179B30 File Offset: 0x00177D30
		public int ItemTotalCount
		{
			get
			{
				return this.mItemTotalCount;
			}
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x0600375F RID: 14175 RVA: 0x00179B38 File Offset: 0x00177D38
		public RectTransform ContainerTrans
		{
			get
			{
				return this.mContainerTrans;
			}
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06003760 RID: 14176 RVA: 0x00179B40 File Offset: 0x00177D40
		public ScrollRect ScrollRect
		{
			get
			{
				return this.mScrollRect;
			}
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06003761 RID: 14177 RVA: 0x00179B48 File Offset: 0x00177D48
		public bool IsDraging
		{
			get
			{
				return this.mIsDraging;
			}
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06003762 RID: 14178 RVA: 0x00179B50 File Offset: 0x00177D50
		// (set) Token: 0x06003763 RID: 14179 RVA: 0x00179B58 File Offset: 0x00177D58
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

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06003764 RID: 14180 RVA: 0x00179B61 File Offset: 0x00177D61
		// (set) Token: 0x06003765 RID: 14181 RVA: 0x00179B69 File Offset: 0x00177D69
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

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06003766 RID: 14182 RVA: 0x00179B72 File Offset: 0x00177D72
		// (set) Token: 0x06003767 RID: 14183 RVA: 0x00179B7A File Offset: 0x00177D7A
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

		// Token: 0x06003768 RID: 14184 RVA: 0x00179B84 File Offset: 0x00177D84
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

		// Token: 0x06003769 RID: 14185 RVA: 0x00179C04 File Offset: 0x00177E04
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

		// Token: 0x0600376A RID: 14186 RVA: 0x00179CB4 File Offset: 0x00177EB4
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

		// Token: 0x0600376B RID: 14187 RVA: 0x00179F45 File Offset: 0x00178145
		private void Start()
		{
			if (this.OnListViewStart != null)
			{
				this.OnListViewStart(this);
			}
		}

		// Token: 0x0600376C RID: 14188 RVA: 0x00179F5C File Offset: 0x0017815C
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

		// Token: 0x0600376D RID: 14189 RVA: 0x0017A001 File Offset: 0x00178201
		private void OnPointerDownInScrollBar(GameObject obj)
		{
			this.mCurSnapData.Clear();
		}

		// Token: 0x0600376E RID: 14190 RVA: 0x0017A00E File Offset: 0x0017820E
		private void OnPointerUpInScrollBar(GameObject obj)
		{
			this.ForceSnapUpdateCheck();
		}

		// Token: 0x0600376F RID: 14191 RVA: 0x0017A016 File Offset: 0x00178216
		public void ResetListView(bool resetPos = true)
		{
			this.mViewPortRectTransform.GetLocalCorners(this.mViewPortRectLocalCorners);
			if (resetPos)
			{
				this.mContainerTrans.anchoredPosition3D = Vector3.zero;
			}
			this.ForceSnapUpdateCheck();
		}

		// Token: 0x06003770 RID: 14192 RVA: 0x0017A044 File Offset: 0x00178244
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

		// Token: 0x06003771 RID: 14193 RVA: 0x0017A178 File Offset: 0x00178378
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

		// Token: 0x06003772 RID: 14194 RVA: 0x0017A1E4 File Offset: 0x001783E4
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

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06003773 RID: 14195 RVA: 0x0017A268 File Offset: 0x00178468
		public int ShownItemCount
		{
			get
			{
				return this.mItemList.Count;
			}
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06003774 RID: 14196 RVA: 0x0017A278 File Offset: 0x00178478
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

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x06003775 RID: 14197 RVA: 0x0017A2B4 File Offset: 0x001784B4
		public float ViewPortWidth
		{
			get
			{
				return this.mViewPortRectTransform.rect.width;
			}
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06003776 RID: 14198 RVA: 0x0017A2D4 File Offset: 0x001784D4
		public float ViewPortHeight
		{
			get
			{
				return this.mViewPortRectTransform.rect.height;
			}
		}

		// Token: 0x06003777 RID: 14199 RVA: 0x0017A2F4 File Offset: 0x001784F4
		public LoopListViewItem2 GetShownItemByIndex(int index)
		{
			int count = this.mItemList.Count;
			if (index < 0 || index >= count)
			{
				return null;
			}
			return this.mItemList[index];
		}

		// Token: 0x06003778 RID: 14200 RVA: 0x0017A323 File Offset: 0x00178523
		public LoopListViewItem2 GetShownItemByIndexWithoutCheck(int index)
		{
			return this.mItemList[index];
		}

		// Token: 0x06003779 RID: 14201 RVA: 0x0017A334 File Offset: 0x00178534
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

		// Token: 0x0600377A RID: 14202 RVA: 0x0017A380 File Offset: 0x00178580
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

		// Token: 0x0600377B RID: 14203 RVA: 0x0017A3C0 File Offset: 0x001785C0
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

		// Token: 0x0600377C RID: 14204 RVA: 0x0017A420 File Offset: 0x00178620
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

		// Token: 0x0600377D RID: 14205 RVA: 0x0017A4A0 File Offset: 0x001786A0
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

		// Token: 0x0600377E RID: 14206 RVA: 0x0017A589 File Offset: 0x00178789
		public void FinishSnapImmediately()
		{
			this.UpdateSnapMove(true, false);
		}

		// Token: 0x0600377F RID: 14207 RVA: 0x0017A594 File Offset: 0x00178794
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

		// Token: 0x06003780 RID: 14208 RVA: 0x0017A7D1 File Offset: 0x001789D1
		public void RefreshAllShownItem()
		{
			if (this.mItemList.Count == 0)
			{
				return;
			}
			this.RefreshAllShownItemWithFirstIndex(this.mItemList[0].ItemIndex);
		}

		// Token: 0x06003781 RID: 14209 RVA: 0x0017A7F8 File Offset: 0x001789F8
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

		// Token: 0x06003782 RID: 14210 RVA: 0x0017A908 File Offset: 0x00178B08
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

		// Token: 0x06003783 RID: 14211 RVA: 0x0017A9EC File Offset: 0x00178BEC
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

		// Token: 0x06003784 RID: 14212 RVA: 0x0017AA30 File Offset: 0x00178C30
		private void ClearAllTmpRecycledItem()
		{
			int count = this.mItemPoolList.Count;
			for (int i = 0; i < count; i++)
			{
				this.mItemPoolList[i].ClearTmpRecycledItem();
			}
		}

		// Token: 0x06003785 RID: 14213 RVA: 0x0017AA68 File Offset: 0x00178C68
		private void RecycleAllItem()
		{
			foreach (LoopListViewItem2 item in this.mItemList)
			{
				this.RecycleItemTmp(item);
			}
			this.mItemList.Clear();
		}

		// Token: 0x06003786 RID: 14214 RVA: 0x0017AAC8 File Offset: 0x00178CC8
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

		// Token: 0x06003787 RID: 14215 RVA: 0x0017AB3C File Offset: 0x00178D3C
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

		// Token: 0x06003788 RID: 14216 RVA: 0x0017ABB0 File Offset: 0x00178DB0
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

		// Token: 0x06003789 RID: 14217 RVA: 0x0017AC64 File Offset: 0x00178E64
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

		// Token: 0x0600378A RID: 14218 RVA: 0x0017AD18 File Offset: 0x00178F18
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

		// Token: 0x0600378B RID: 14219 RVA: 0x0017AE64 File Offset: 0x00179064
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

		// Token: 0x0600378C RID: 14220 RVA: 0x0017AE9B File Offset: 0x0017909B
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

		// Token: 0x0600378D RID: 14221 RVA: 0x0017AECD File Offset: 0x001790CD
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

		// Token: 0x0600378E RID: 14222 RVA: 0x0017AEF4 File Offset: 0x001790F4
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

		// Token: 0x0600378F RID: 14223 RVA: 0x0017AF60 File Offset: 0x00179160
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

		// Token: 0x06003790 RID: 14224 RVA: 0x0017AFBC File Offset: 0x001791BC
		private void SetItemSize(int itemIndex, float itemSize, float padding)
		{
			this.mItemPosMgr.SetItemSize(itemIndex, itemSize + padding);
			if (itemIndex >= this.mLastItemIndex)
			{
				this.mLastItemIndex = itemIndex;
				this.mLastItemPadding = padding;
			}
		}

		// Token: 0x06003791 RID: 14225 RVA: 0x0017AFE4 File Offset: 0x001791E4
		private bool GetPlusItemIndexAndPosAtGivenPos(float pos, ref int index, ref float itemPos)
		{
			return this.mItemPosMgr.GetItemIndexAndPosAtGivenPos(pos, ref index, ref itemPos);
		}

		// Token: 0x06003792 RID: 14226 RVA: 0x0017AFF4 File Offset: 0x001791F4
		private float GetItemPos(int itemIndex)
		{
			return this.mItemPosMgr.GetItemPos(itemIndex);
		}

		// Token: 0x06003793 RID: 14227 RVA: 0x0017B002 File Offset: 0x00179202
		public Vector3 GetItemCornerPosInViewPort(LoopListViewItem2 item, ItemCornerEnum corner = ItemCornerEnum.LeftBottom)
		{
			item.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
			return this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[(int)corner]);
		}

		// Token: 0x06003794 RID: 14228 RVA: 0x0017B02C File Offset: 0x0017922C
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

		// Token: 0x06003795 RID: 14229 RVA: 0x0017B790 File Offset: 0x00179990
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

		// Token: 0x06003796 RID: 14230 RVA: 0x0017B878 File Offset: 0x00179A78
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

		// Token: 0x06003797 RID: 14231 RVA: 0x0017B89C File Offset: 0x00179A9C
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

		// Token: 0x06003798 RID: 14232 RVA: 0x0017BC84 File Offset: 0x00179E84
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

		// Token: 0x06003799 RID: 14233 RVA: 0x0017C1FC File Offset: 0x0017A3FC
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

		// Token: 0x0600379A RID: 14234 RVA: 0x0017A001 File Offset: 0x00178201
		public void ClearSnapData()
		{
			this.mCurSnapData.Clear();
		}

		// Token: 0x0600379B RID: 14235 RVA: 0x0017C4A8 File Offset: 0x0017A6A8
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

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x0600379C RID: 14236 RVA: 0x0017C513 File Offset: 0x0017A713
		public int CurSnapNearestItemIndex
		{
			get
			{
				return this.mCurSnapNearestItemIndex;
			}
		}

		// Token: 0x0600379D RID: 14237 RVA: 0x0017C51B File Offset: 0x0017A71B
		public void ForceSnapUpdateCheck()
		{
			if (this.mLeftSnapUpdateExtraCount <= 0)
			{
				this.mLeftSnapUpdateExtraCount = 1;
			}
		}

		// Token: 0x0600379E RID: 14238 RVA: 0x0017C530 File Offset: 0x0017A730
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

		// Token: 0x0600379F RID: 14239 RVA: 0x0017CAA8 File Offset: 0x0017ACA8
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

		// Token: 0x060037A0 RID: 14240 RVA: 0x0017CC9C File Offset: 0x0017AE9C
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

		// Token: 0x060037A1 RID: 14241 RVA: 0x0017CD44 File Offset: 0x0017AF44
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

		// Token: 0x060037A2 RID: 14242 RVA: 0x0017D6B8 File Offset: 0x0017B8B8
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

		// Token: 0x060037A3 RID: 14243 RVA: 0x0017E04C File Offset: 0x0017C24C
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

		// Token: 0x060037A4 RID: 14244 RVA: 0x0017E130 File Offset: 0x0017C330
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

		// Token: 0x060037A5 RID: 14245 RVA: 0x0017E3BC File Offset: 0x0017C5BC
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

		// Token: 0x060037A6 RID: 14246 RVA: 0x0017E81C File Offset: 0x0017CA1C
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

		// Token: 0x04003028 RID: 12328
		private Dictionary<string, ItemPool> mItemPoolDict = new Dictionary<string, ItemPool>();

		// Token: 0x04003029 RID: 12329
		private List<ItemPool> mItemPoolList = new List<ItemPool>();

		// Token: 0x0400302A RID: 12330
		[SerializeField]
		private List<ItemPrefabConfData> mItemPrefabDataList = new List<ItemPrefabConfData>();

		// Token: 0x0400302B RID: 12331
		[SerializeField]
		private ListItemArrangeType mArrangeType;

		// Token: 0x0400302C RID: 12332
		private List<LoopListViewItem2> mItemList = new List<LoopListViewItem2>();

		// Token: 0x0400302D RID: 12333
		private RectTransform mContainerTrans;

		// Token: 0x0400302E RID: 12334
		private ScrollRect mScrollRect;

		// Token: 0x0400302F RID: 12335
		private RectTransform mScrollRectTransform;

		// Token: 0x04003030 RID: 12336
		private RectTransform mViewPortRectTransform;

		// Token: 0x04003031 RID: 12337
		private float mItemDefaultWithPaddingSize = 20f;

		// Token: 0x04003032 RID: 12338
		private int mItemTotalCount;

		// Token: 0x04003033 RID: 12339
		private bool mIsVertList;

		// Token: 0x04003034 RID: 12340
		private Func<LoopListView2, int, LoopListViewItem2> mOnGetItemByIndex;

		// Token: 0x04003035 RID: 12341
		private Vector3[] mItemWorldCorners = new Vector3[4];

		// Token: 0x04003036 RID: 12342
		private Vector3[] mViewPortRectLocalCorners = new Vector3[4];

		// Token: 0x04003037 RID: 12343
		private int mCurReadyMinItemIndex;

		// Token: 0x04003038 RID: 12344
		private int mCurReadyMaxItemIndex;

		// Token: 0x04003039 RID: 12345
		private bool mNeedCheckNextMinItem = true;

		// Token: 0x0400303A RID: 12346
		private bool mNeedCheckNextMaxItem = true;

		// Token: 0x0400303B RID: 12347
		private ItemPosMgr mItemPosMgr;

		// Token: 0x0400303C RID: 12348
		private float mDistanceForRecycle0 = 300f;

		// Token: 0x0400303D RID: 12349
		private float mDistanceForNew0 = 200f;

		// Token: 0x0400303E RID: 12350
		private float mDistanceForRecycle1 = 300f;

		// Token: 0x0400303F RID: 12351
		private float mDistanceForNew1 = 200f;

		// Token: 0x04003040 RID: 12352
		[SerializeField]
		private bool mSupportScrollBar = true;

		// Token: 0x04003041 RID: 12353
		private bool mIsDraging;

		// Token: 0x04003042 RID: 12354
		private PointerEventData mPointerEventData;

		// Token: 0x04003043 RID: 12355
		public Action mOnBeginDragAction;

		// Token: 0x04003044 RID: 12356
		public Action mOnDragingAction;

		// Token: 0x04003045 RID: 12357
		public Action mOnEndDragAction;

		// Token: 0x04003046 RID: 12358
		private int mLastItemIndex;

		// Token: 0x04003047 RID: 12359
		private float mLastItemPadding;

		// Token: 0x04003048 RID: 12360
		private float mSmoothDumpVel;

		// Token: 0x04003049 RID: 12361
		private float mSmoothDumpRate = 0.3f;

		// Token: 0x0400304A RID: 12362
		private float mSnapFinishThreshold = 0.1f;

		// Token: 0x0400304B RID: 12363
		private float mSnapVecThreshold = 145f;

		// Token: 0x0400304C RID: 12364
		private float mSnapMoveDefaultMaxAbsVec = 3400f;

		// Token: 0x0400304D RID: 12365
		[SerializeField]
		private bool mItemSnapEnable;

		// Token: 0x0400304E RID: 12366
		private Vector3 mLastFrameContainerPos = Vector3.zero;

		// Token: 0x0400304F RID: 12367
		public Action<LoopListView2, LoopListViewItem2> mOnSnapItemFinished;

		// Token: 0x04003050 RID: 12368
		public Action<LoopListView2, LoopListViewItem2> mOnSnapNearestChanged;

		// Token: 0x04003051 RID: 12369
		private int mCurSnapNearestItemIndex = -1;

		// Token: 0x04003052 RID: 12370
		private Vector2 mAdjustedVec;

		// Token: 0x04003053 RID: 12371
		private bool mNeedAdjustVec;

		// Token: 0x04003054 RID: 12372
		private int mLeftSnapUpdateExtraCount = 1;

		// Token: 0x04003055 RID: 12373
		[SerializeField]
		private Vector2 mViewPortSnapPivot = Vector2.zero;

		// Token: 0x04003056 RID: 12374
		[SerializeField]
		private Vector2 mItemSnapPivot = Vector2.zero;

		// Token: 0x04003057 RID: 12375
		private ClickEventListener mScrollBarClickEventListener;

		// Token: 0x04003058 RID: 12376
		private LoopListView2.SnapData mCurSnapData = new LoopListView2.SnapData();

		// Token: 0x04003059 RID: 12377
		private Vector3 mLastSnapCheckPos = Vector3.zero;

		// Token: 0x0400305A RID: 12378
		private bool mListViewInited;

		// Token: 0x0400305B RID: 12379
		private int mListUpdateCheckFrameCount;

		// Token: 0x0400305C RID: 12380
		public Action<LoopListView2> OnListViewStart;

		// Token: 0x02001513 RID: 5395
		private class SnapData
		{
			// Token: 0x060082F4 RID: 33524 RVA: 0x002DD458 File Offset: 0x002DB658
			public void Clear()
			{
				this.mSnapStatus = SnapStatus.NoTargetSet;
				this.mTempTargetIndex = -1;
				this.mIsForceSnapTo = false;
				this.mMoveMaxAbsVec = -1f;
			}

			// Token: 0x04006E53 RID: 28243
			public SnapStatus mSnapStatus;

			// Token: 0x04006E54 RID: 28244
			public int mSnapTargetIndex;

			// Token: 0x04006E55 RID: 28245
			public float mTargetSnapVal;

			// Token: 0x04006E56 RID: 28246
			public float mCurSnapVal;

			// Token: 0x04006E57 RID: 28247
			public bool mIsForceSnapTo;

			// Token: 0x04006E58 RID: 28248
			public bool mIsTempTarget;

			// Token: 0x04006E59 RID: 28249
			public int mTempTargetIndex = -1;

			// Token: 0x04006E5A RID: 28250
			public float mMoveMaxAbsVec = -1f;
		}
	}
}
