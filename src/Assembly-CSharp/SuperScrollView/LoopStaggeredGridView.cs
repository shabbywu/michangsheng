using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SuperScrollView
{
	// Token: 0x02000A02 RID: 2562
	public class LoopStaggeredGridView : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IEndDragHandler, IDragHandler
	{
		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x060041F4 RID: 16884 RVA: 0x0002F2A5 File Offset: 0x0002D4A5
		// (set) Token: 0x060041F5 RID: 16885 RVA: 0x0002F2AD File Offset: 0x0002D4AD
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

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x060041F6 RID: 16886 RVA: 0x0002F2B6 File Offset: 0x0002D4B6
		public List<StaggeredGridItemPrefabConfData> ItemPrefabDataList
		{
			get
			{
				return this.mItemPrefabDataList;
			}
		}

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x060041F7 RID: 16887 RVA: 0x0002F2BE File Offset: 0x0002D4BE
		public int ListUpdateCheckFrameCount
		{
			get
			{
				return this.mListUpdateCheckFrameCount;
			}
		}

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x060041F8 RID: 16888 RVA: 0x0002F2C6 File Offset: 0x0002D4C6
		public bool IsVertList
		{
			get
			{
				return this.mIsVertList;
			}
		}

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x060041F9 RID: 16889 RVA: 0x0002F2CE File Offset: 0x0002D4CE
		public int ItemTotalCount
		{
			get
			{
				return this.mItemTotalCount;
			}
		}

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x060041FA RID: 16890 RVA: 0x0002F2D6 File Offset: 0x0002D4D6
		public RectTransform ContainerTrans
		{
			get
			{
				return this.mContainerTrans;
			}
		}

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x060041FB RID: 16891 RVA: 0x0002F2DE File Offset: 0x0002D4DE
		public ScrollRect ScrollRect
		{
			get
			{
				return this.mScrollRect;
			}
		}

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x060041FC RID: 16892 RVA: 0x0002F2E6 File Offset: 0x0002D4E6
		public bool IsDraging
		{
			get
			{
				return this.mIsDraging;
			}
		}

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x060041FD RID: 16893 RVA: 0x0002F2EE File Offset: 0x0002D4EE
		public GridViewLayoutParam LayoutParam
		{
			get
			{
				return this.mLayoutParam;
			}
		}

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x060041FE RID: 16894 RVA: 0x0002F2F6 File Offset: 0x0002D4F6
		public bool IsInited
		{
			get
			{
				return this.mListViewInited;
			}
		}

		// Token: 0x060041FF RID: 16895 RVA: 0x001C6BEC File Offset: 0x001C4DEC
		public StaggeredGridItemGroup GetItemGroupByIndex(int index)
		{
			int count = this.mItemGroupList.Count;
			if (index < 0 || index >= count)
			{
				return null;
			}
			return this.mItemGroupList[index];
		}

		// Token: 0x06004200 RID: 16896 RVA: 0x001C6C1C File Offset: 0x001C4E1C
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

		// Token: 0x06004201 RID: 16897 RVA: 0x001C6C9C File Offset: 0x001C4E9C
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

		// Token: 0x06004202 RID: 16898 RVA: 0x001C6F2C File Offset: 0x001C512C
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

		// Token: 0x06004203 RID: 16899 RVA: 0x001C7020 File Offset: 0x001C5220
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

		// Token: 0x06004204 RID: 16900 RVA: 0x001C7124 File Offset: 0x001C5324
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

		// Token: 0x06004205 RID: 16901 RVA: 0x001C7184 File Offset: 0x001C5384
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

		// Token: 0x06004206 RID: 16902 RVA: 0x001C724C File Offset: 0x001C544C
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

		// Token: 0x06004207 RID: 16903 RVA: 0x001C7430 File Offset: 0x001C5630
		public LoopStaggeredGridViewItem GetShownItemByItemIndex(int itemIndex)
		{
			ItemIndexData itemIndexData = this.GetItemIndexData(itemIndex);
			if (itemIndexData == null)
			{
				return null;
			}
			return this.GetItemGroupByIndex(itemIndexData.mGroupIndex).GetShownItemByIndexInGroup(itemIndexData.mIndexInGroup);
		}

		// Token: 0x06004208 RID: 16904 RVA: 0x001C7464 File Offset: 0x001C5664
		public void RefreshAllShownItem()
		{
			int count = this.mItemGroupList.Count;
			for (int i = 0; i < count; i++)
			{
				this.mItemGroupList[i].RefreshAllShownItem();
			}
		}

		// Token: 0x06004209 RID: 16905 RVA: 0x001C749C File Offset: 0x001C569C
		public void OnItemSizeChanged(int itemIndex)
		{
			ItemIndexData itemIndexData = this.GetItemIndexData(itemIndex);
			if (itemIndexData == null)
			{
				return;
			}
			this.GetItemGroupByIndex(itemIndexData.mGroupIndex).OnItemSizeChanged(itemIndexData.mIndexInGroup);
		}

		// Token: 0x0600420A RID: 16906 RVA: 0x001C74CC File Offset: 0x001C56CC
		public void RefreshItemByItemIndex(int itemIndex)
		{
			ItemIndexData itemIndexData = this.GetItemIndexData(itemIndex);
			if (itemIndexData == null)
			{
				return;
			}
			this.GetItemGroupByIndex(itemIndexData.mGroupIndex).RefreshItemByIndexInGroup(itemIndexData.mIndexInGroup);
		}

		// Token: 0x0600420B RID: 16907 RVA: 0x0002F2FE File Offset: 0x0002D4FE
		public void ResetListView(bool resetPos = true)
		{
			this.mViewPortRectTransform.GetLocalCorners(this.mViewPortRectLocalCorners);
			if (resetPos)
			{
				this.mContainerTrans.anchoredPosition3D = Vector3.zero;
			}
		}

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x0600420C RID: 16908 RVA: 0x001C74FC File Offset: 0x001C56FC
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

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x0600420D RID: 16909 RVA: 0x001C7538 File Offset: 0x001C5738
		public float ViewPortWidth
		{
			get
			{
				return this.mViewPortRectTransform.rect.width;
			}
		}

		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x0600420E RID: 16910 RVA: 0x001C7558 File Offset: 0x001C5758
		public float ViewPortHeight
		{
			get
			{
				return this.mViewPortRectTransform.rect.height;
			}
		}

		// Token: 0x0600420F RID: 16911 RVA: 0x001C7578 File Offset: 0x001C5778
		public void RecycleAllItem()
		{
			int count = this.mItemGroupList.Count;
			for (int i = 0; i < count; i++)
			{
				this.mItemGroupList[i].RecycleAllItem();
			}
		}

		// Token: 0x06004210 RID: 16912 RVA: 0x001C75B0 File Offset: 0x001C57B0
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

		// Token: 0x06004211 RID: 16913 RVA: 0x001C75F4 File Offset: 0x001C57F4
		public void ClearAllTmpRecycledItem()
		{
			int count = this.mItemPoolList.Count;
			for (int i = 0; i < count; i++)
			{
				this.mItemPoolList[i].ClearTmpRecycledItem();
			}
		}

		// Token: 0x06004212 RID: 16914 RVA: 0x001C762C File Offset: 0x001C582C
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

		// Token: 0x06004213 RID: 16915 RVA: 0x001C762C File Offset: 0x001C582C
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

		// Token: 0x06004214 RID: 16916 RVA: 0x001C76A0 File Offset: 0x001C58A0
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

		// Token: 0x06004215 RID: 16917 RVA: 0x001C76A0 File Offset: 0x001C58A0
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

		// Token: 0x06004216 RID: 16918 RVA: 0x001C7754 File Offset: 0x001C5954
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

		// Token: 0x06004217 RID: 16919 RVA: 0x0002F324 File Offset: 0x0002D524
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

		// Token: 0x06004218 RID: 16920 RVA: 0x0002F350 File Offset: 0x0002D550
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

		// Token: 0x06004219 RID: 16921 RVA: 0x0002F37C File Offset: 0x0002D57C
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

		// Token: 0x0600421A RID: 16922 RVA: 0x001C7898 File Offset: 0x001C5A98
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

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x0600421B RID: 16923 RVA: 0x0002F3A1 File Offset: 0x0002D5A1
		public int CurMaxCreatedItemIndexCount
		{
			get
			{
				return this.mItemIndexDataList.Count;
			}
		}

		// Token: 0x0600421C RID: 16924 RVA: 0x001C7904 File Offset: 0x001C5B04
		private void SetAnchoredPositionX(RectTransform rtf, float x)
		{
			Vector3 anchoredPosition3D = rtf.anchoredPosition3D;
			anchoredPosition3D.x = x;
			rtf.anchoredPosition3D = anchoredPosition3D;
		}

		// Token: 0x0600421D RID: 16925 RVA: 0x001C7928 File Offset: 0x001C5B28
		private void SetAnchoredPositionY(RectTransform rtf, float y)
		{
			Vector3 anchoredPosition3D = rtf.anchoredPosition3D;
			anchoredPosition3D.y = y;
			rtf.anchoredPosition3D = anchoredPosition3D;
		}

		// Token: 0x0600421E RID: 16926 RVA: 0x001C794C File Offset: 0x001C5B4C
		public ItemIndexData GetItemIndexData(int itemIndex)
		{
			int count = this.mItemIndexDataList.Count;
			if (itemIndex < 0 || itemIndex >= count)
			{
				return null;
			}
			return this.mItemIndexDataList[itemIndex];
		}

		// Token: 0x0600421F RID: 16927 RVA: 0x001C797C File Offset: 0x001C5B7C
		public void UpdateAllGroupShownItemsPos()
		{
			int count = this.mItemGroupList.Count;
			for (int i = 0; i < count; i++)
			{
				this.mItemGroupList[i].UpdateAllShownItemsPos();
			}
		}

		// Token: 0x06004220 RID: 16928 RVA: 0x001C79B4 File Offset: 0x001C5BB4
		private void CheckAllGroupIfNeedUpdateItemPos()
		{
			int count = this.mItemGroupList.Count;
			for (int i = 0; i < count; i++)
			{
				this.mItemGroupList[i].CheckIfNeedUpdateItemPos();
			}
		}

		// Token: 0x06004221 RID: 16929 RVA: 0x001C79EC File Offset: 0x001C5BEC
		public float GetItemAbsPosByItemIndex(int itemIndex)
		{
			if (itemIndex < 0 || itemIndex >= this.mItemIndexDataList.Count)
			{
				return -1f;
			}
			ItemIndexData itemIndexData = this.mItemIndexDataList[itemIndex];
			return this.mItemGroupList[itemIndexData.mGroupIndex].GetItemPos(itemIndexData.mIndexInGroup);
		}

		// Token: 0x06004222 RID: 16930 RVA: 0x001C7A3C File Offset: 0x001C5C3C
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

		// Token: 0x06004223 RID: 16931 RVA: 0x001C7B60 File Offset: 0x001C5D60
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

		// Token: 0x06004224 RID: 16932 RVA: 0x0002F3AE File Offset: 0x0002D5AE
		public void UpdateListViewWithDefault()
		{
			this.UpdateListView(this.mDistanceForRecycle0, this.mDistanceForRecycle1, this.mDistanceForNew0, this.mDistanceForNew1);
			this.UpdateContentSize();
		}

		// Token: 0x06004225 RID: 16933 RVA: 0x0002F3D4 File Offset: 0x0002D5D4
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

		// Token: 0x06004226 RID: 16934 RVA: 0x001C7BAC File Offset: 0x001C5DAC
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

		// Token: 0x06004227 RID: 16935 RVA: 0x001C7C4C File Offset: 0x001C5E4C
		public float GetContentSize()
		{
			if (this.mIsVertList)
			{
				return this.mContainerTrans.rect.height;
			}
			return this.mContainerTrans.rect.width;
		}

		// Token: 0x06004228 RID: 16936 RVA: 0x001C7C88 File Offset: 0x001C5E88
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

		// Token: 0x04003A93 RID: 14995
		private Dictionary<string, StaggeredGridItemPool> mItemPoolDict = new Dictionary<string, StaggeredGridItemPool>();

		// Token: 0x04003A94 RID: 14996
		private List<StaggeredGridItemPool> mItemPoolList = new List<StaggeredGridItemPool>();

		// Token: 0x04003A95 RID: 14997
		[SerializeField]
		private List<StaggeredGridItemPrefabConfData> mItemPrefabDataList = new List<StaggeredGridItemPrefabConfData>();

		// Token: 0x04003A96 RID: 14998
		[SerializeField]
		private ListItemArrangeType mArrangeType;

		// Token: 0x04003A97 RID: 14999
		private RectTransform mContainerTrans;

		// Token: 0x04003A98 RID: 15000
		private ScrollRect mScrollRect;

		// Token: 0x04003A99 RID: 15001
		private int mGroupCount;

		// Token: 0x04003A9A RID: 15002
		private List<StaggeredGridItemGroup> mItemGroupList = new List<StaggeredGridItemGroup>();

		// Token: 0x04003A9B RID: 15003
		private List<ItemIndexData> mItemIndexDataList = new List<ItemIndexData>();

		// Token: 0x04003A9C RID: 15004
		private RectTransform mScrollRectTransform;

		// Token: 0x04003A9D RID: 15005
		private RectTransform mViewPortRectTransform;

		// Token: 0x04003A9E RID: 15006
		private float mItemDefaultWithPaddingSize = 20f;

		// Token: 0x04003A9F RID: 15007
		private int mItemTotalCount;

		// Token: 0x04003AA0 RID: 15008
		private bool mIsVertList;

		// Token: 0x04003AA1 RID: 15009
		private Func<LoopStaggeredGridView, int, LoopStaggeredGridViewItem> mOnGetItemByItemIndex;

		// Token: 0x04003AA2 RID: 15010
		private Vector3[] mItemWorldCorners = new Vector3[4];

		// Token: 0x04003AA3 RID: 15011
		private Vector3[] mViewPortRectLocalCorners = new Vector3[4];

		// Token: 0x04003AA4 RID: 15012
		private float mDistanceForRecycle0 = 300f;

		// Token: 0x04003AA5 RID: 15013
		private float mDistanceForNew0 = 200f;

		// Token: 0x04003AA6 RID: 15014
		private float mDistanceForRecycle1 = 300f;

		// Token: 0x04003AA7 RID: 15015
		private float mDistanceForNew1 = 200f;

		// Token: 0x04003AA8 RID: 15016
		private bool mIsDraging;

		// Token: 0x04003AA9 RID: 15017
		private PointerEventData mPointerEventData;

		// Token: 0x04003AAA RID: 15018
		public Action mOnBeginDragAction;

		// Token: 0x04003AAB RID: 15019
		public Action mOnDragingAction;

		// Token: 0x04003AAC RID: 15020
		public Action mOnEndDragAction;

		// Token: 0x04003AAD RID: 15021
		private Vector3 mLastFrameContainerPos = Vector3.zero;

		// Token: 0x04003AAE RID: 15022
		private bool mListViewInited;

		// Token: 0x04003AAF RID: 15023
		private int mListUpdateCheckFrameCount;

		// Token: 0x04003AB0 RID: 15024
		private GridViewLayoutParam mLayoutParam;
	}
}
