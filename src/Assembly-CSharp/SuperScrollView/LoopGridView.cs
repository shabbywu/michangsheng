using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SuperScrollView;

public class LoopGridView : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IEndDragHandler, IDragHandler
{
	private class SnapData
	{
		public SnapStatus mSnapStatus;

		public RowColumnPair mSnapTarget;

		public Vector2 mSnapNeedMoveDir;

		public float mTargetSnapVal;

		public float mCurSnapVal;

		public bool mIsForceSnapTo;

		public void Clear()
		{
			mSnapStatus = SnapStatus.NoTargetSet;
			mIsForceSnapTo = false;
		}
	}

	private class ItemRangeData
	{
		public int mMaxRow;

		public int mMinRow;

		public int mMaxColumn;

		public int mMinColumn;

		public Vector2 mCheckedPosition;
	}

	private Dictionary<string, GridItemPool> mItemPoolDict = new Dictionary<string, GridItemPool>();

	private List<GridItemPool> mItemPoolList = new List<GridItemPool>();

	[SerializeField]
	private List<GridViewItemPrefabConfData> mItemPrefabDataList = new List<GridViewItemPrefabConfData>();

	[SerializeField]
	private GridItemArrangeType mArrangeType;

	private RectTransform mContainerTrans;

	private ScrollRect mScrollRect;

	private RectTransform mScrollRectTransform;

	private RectTransform mViewPortRectTransform;

	private int mItemTotalCount;

	[SerializeField]
	private int mFixedRowOrColumnCount;

	[SerializeField]
	private RectOffset mPadding = new RectOffset();

	[SerializeField]
	private Vector2 mItemPadding = Vector2.zero;

	[SerializeField]
	private Vector2 mItemSize = Vector2.zero;

	[SerializeField]
	private Vector2 mItemRecycleDistance = new Vector2(50f, 50f);

	private Vector2 mItemSizeWithPadding = Vector2.zero;

	private Vector2 mStartPadding;

	private Vector2 mEndPadding;

	private Func<LoopGridView, int, int, int, LoopGridViewItem> mOnGetItemByRowColumn;

	private List<GridItemGroup> mItemGroupObjPool = new List<GridItemGroup>();

	private List<GridItemGroup> mItemGroupList = new List<GridItemGroup>();

	private bool mIsDraging;

	private int mRowCount;

	private int mColumnCount;

	public Action<PointerEventData> mOnBeginDragAction;

	public Action<PointerEventData> mOnDragingAction;

	public Action<PointerEventData> mOnEndDragAction;

	private float mSmoothDumpVel;

	private float mSmoothDumpRate = 0.3f;

	private float mSnapFinishThreshold = 0.1f;

	private float mSnapVecThreshold = 145f;

	[SerializeField]
	private bool mItemSnapEnable;

	[SerializeField]
	private GridFixedType mGridFixedType;

	public Action<LoopGridView, LoopGridViewItem> mOnSnapItemFinished;

	public Action<LoopGridView> mOnSnapNearestChanged;

	private int mLeftSnapUpdateExtraCount = 1;

	[SerializeField]
	private Vector2 mViewPortSnapPivot = Vector2.zero;

	[SerializeField]
	private Vector2 mItemSnapPivot = Vector2.zero;

	private SnapData mCurSnapData = new SnapData();

	private Vector3 mLastSnapCheckPos = Vector3.zero;

	private bool mListViewInited;

	private int mListUpdateCheckFrameCount;

	private ItemRangeData mCurFrameItemRangeData = new ItemRangeData();

	private int mNeedCheckContentPosLeftCount = 1;

	private ClickEventListener mScrollBarClickEventListener1;

	private ClickEventListener mScrollBarClickEventListener2;

	private RowColumnPair mCurSnapNearestItemRowColumn;

	public GridItemArrangeType ArrangeType
	{
		get
		{
			return mArrangeType;
		}
		set
		{
			mArrangeType = value;
		}
	}

	public List<GridViewItemPrefabConfData> ItemPrefabDataList => mItemPrefabDataList;

	public int ItemTotalCount => mItemTotalCount;

	public RectTransform ContainerTrans => mContainerTrans;

	public float ViewPortWidth
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			Rect rect = mViewPortRectTransform.rect;
			return ((Rect)(ref rect)).width;
		}
	}

	public float ViewPortHeight
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			Rect rect = mViewPortRectTransform.rect;
			return ((Rect)(ref rect)).height;
		}
	}

	public ScrollRect ScrollRect => mScrollRect;

	public bool IsDraging => mIsDraging;

	public bool ItemSnapEnable
	{
		get
		{
			return mItemSnapEnable;
		}
		set
		{
			mItemSnapEnable = value;
		}
	}

	public Vector2 ItemSize
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return mItemSize;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			SetItemSize(value);
		}
	}

	public Vector2 ItemPadding
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return mItemPadding;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			SetItemPadding(value);
		}
	}

	public Vector2 ItemSizeWithPadding => mItemSizeWithPadding;

	public RectOffset Padding
	{
		get
		{
			return mPadding;
		}
		set
		{
			SetPadding(value);
		}
	}

	public RowColumnPair CurSnapNearestItemRowColumn => mCurSnapNearestItemRowColumn;

	public GridViewItemPrefabConfData GetItemPrefabConfData(string prefabName)
	{
		foreach (GridViewItemPrefabConfData mItemPrefabData in mItemPrefabDataList)
		{
			if ((Object)(object)mItemPrefabData.mItemPrefab == (Object)null)
			{
				Debug.LogError((object)"A item prefab is null ");
			}
			else if (prefabName == ((Object)mItemPrefabData.mItemPrefab).name)
			{
				return mItemPrefabData;
			}
		}
		return null;
	}

	public void InitGridView(int itemTotalCount, Func<LoopGridView, int, int, int, LoopGridViewItem> onGetItemByRowColumn, LoopGridViewSettingParam settingParam = null, LoopGridViewInitParam initParam = null)
	{
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Invalid comparison between Unknown and I4
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Invalid comparison between Unknown and I4
		if (mListViewInited)
		{
			Debug.LogError((object)"LoopGridView.InitListView method can be called only once.");
			return;
		}
		mListViewInited = true;
		if (itemTotalCount < 0)
		{
			Debug.LogError((object)"itemTotalCount is  < 0");
			itemTotalCount = 0;
		}
		if (settingParam != null)
		{
			UpdateFromSettingParam(settingParam);
		}
		if (initParam != null)
		{
			mSmoothDumpRate = initParam.mSmoothDumpRate;
			mSnapFinishThreshold = initParam.mSnapFinishThreshold;
			mSnapVecThreshold = initParam.mSnapVecThreshold;
		}
		mScrollRect = ((Component)this).gameObject.GetComponent<ScrollRect>();
		if ((Object)(object)mScrollRect == (Object)null)
		{
			Debug.LogError((object)"ListView Init Failed! ScrollRect component not found!");
			return;
		}
		mCurSnapData.Clear();
		mScrollRectTransform = ((Component)mScrollRect).GetComponent<RectTransform>();
		mContainerTrans = mScrollRect.content;
		mViewPortRectTransform = mScrollRect.viewport;
		if ((Object)(object)mViewPortRectTransform == (Object)null)
		{
			mViewPortRectTransform = mScrollRectTransform;
		}
		if ((int)mScrollRect.horizontalScrollbarVisibility == 2 && (Object)(object)mScrollRect.horizontalScrollbar != (Object)null)
		{
			Debug.LogError((object)"ScrollRect.horizontalScrollbarVisibility cannot be set to AutoHideAndExpandViewport");
		}
		if ((int)mScrollRect.verticalScrollbarVisibility == 2 && (Object)(object)mScrollRect.verticalScrollbar != (Object)null)
		{
			Debug.LogError((object)"ScrollRect.verticalScrollbarVisibility cannot be set to AutoHideAndExpandViewport");
		}
		SetScrollbarListener();
		AdjustViewPortPivot();
		AdjustContainerAnchorAndPivot();
		InitItemPool();
		mOnGetItemByRowColumn = onGetItemByRowColumn;
		mNeedCheckContentPosLeftCount = 4;
		mCurSnapData.Clear();
		mItemTotalCount = itemTotalCount;
		UpdateAllGridSetting();
	}

	public void SetListItemCount(int itemCount, bool resetPos = true)
	{
		if (itemCount < 0 || itemCount == mItemTotalCount)
		{
			return;
		}
		mCurSnapData.Clear();
		mItemTotalCount = itemCount;
		UpdateColumnRowCount();
		UpdateContentSize();
		ForceToCheckContentPos();
		if (mItemTotalCount == 0)
		{
			RecycleAllItem();
			ClearAllTmpRecycledItem();
			return;
		}
		VaildAndSetContainerPos();
		UpdateGridViewContent();
		ClearAllTmpRecycledItem();
		if (resetPos)
		{
			MovePanelToItemByRowColumn(0, 0);
		}
	}

	public LoopGridViewItem NewListViewItem(string itemPrefabName)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		GridItemPool value = null;
		if (!mItemPoolDict.TryGetValue(itemPrefabName, out value))
		{
			return null;
		}
		LoopGridViewItem item = value.GetItem();
		RectTransform component = ((Component)item).GetComponent<RectTransform>();
		((Transform)component).SetParent((Transform)(object)mContainerTrans);
		((Transform)component).localScale = Vector3.one;
		component.anchoredPosition3D = Vector3.zero;
		((Transform)component).localEulerAngles = Vector3.zero;
		item.ParentGridView = this;
		return item;
	}

	public void RefreshItemByItemIndex(int itemIndex)
	{
		if (itemIndex >= 0 && itemIndex < ItemTotalCount && mItemGroupList.Count != 0)
		{
			RowColumnPair rowColumnByItemIndex = GetRowColumnByItemIndex(itemIndex);
			RefreshItemByRowColumn(rowColumnByItemIndex.mRow, rowColumnByItemIndex.mColumn);
		}
	}

	public void RefreshItemByRowColumn(int row, int column)
	{
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		if (mItemGroupList.Count == 0)
		{
			return;
		}
		if (mGridFixedType == GridFixedType.ColumnCountFixed)
		{
			GridItemGroup shownGroup = GetShownGroup(row);
			if (shownGroup == null)
			{
				return;
			}
			LoopGridViewItem itemByColumn = shownGroup.GetItemByColumn(column);
			if (!((Object)(object)itemByColumn == (Object)null))
			{
				LoopGridViewItem newItemByRowColumn = GetNewItemByRowColumn(row, column);
				if (!((Object)(object)newItemByRowColumn == (Object)null))
				{
					Vector3 anchoredPosition3D = itemByColumn.CachedRectTransform.anchoredPosition3D;
					shownGroup.ReplaceItem(itemByColumn, newItemByRowColumn);
					RecycleItemTmp(itemByColumn);
					newItemByRowColumn.CachedRectTransform.anchoredPosition3D = anchoredPosition3D;
					ClearAllTmpRecycledItem();
				}
			}
			return;
		}
		GridItemGroup shownGroup2 = GetShownGroup(column);
		if (shownGroup2 == null)
		{
			return;
		}
		LoopGridViewItem itemByRow = shownGroup2.GetItemByRow(row);
		if (!((Object)(object)itemByRow == (Object)null))
		{
			LoopGridViewItem newItemByRowColumn2 = GetNewItemByRowColumn(row, column);
			if (!((Object)(object)newItemByRowColumn2 == (Object)null))
			{
				Vector3 anchoredPosition3D2 = itemByRow.CachedRectTransform.anchoredPosition3D;
				shownGroup2.ReplaceItem(itemByRow, newItemByRowColumn2);
				RecycleItemTmp(itemByRow);
				newItemByRowColumn2.CachedRectTransform.anchoredPosition3D = anchoredPosition3D2;
				ClearAllTmpRecycledItem();
			}
		}
	}

	public void ClearSnapData()
	{
		mCurSnapData.Clear();
	}

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
		mCurSnapData.mSnapTarget.mRow = row;
		mCurSnapData.mSnapTarget.mColumn = column;
		mCurSnapData.mSnapStatus = SnapStatus.TargetHasSet;
		mCurSnapData.mIsForceSnapTo = true;
	}

	public void ForceSnapUpdateCheck()
	{
		if (mLeftSnapUpdateExtraCount <= 0)
		{
			mLeftSnapUpdateExtraCount = 1;
		}
	}

	public void ForceToCheckContentPos()
	{
		if (mNeedCheckContentPosLeftCount <= 0)
		{
			mNeedCheckContentPosLeftCount = 1;
		}
	}

	public void MovePanelToItemByIndex(int itemIndex, float offsetX = 0f, float offsetY = 0f)
	{
		if (ItemTotalCount != 0)
		{
			if (itemIndex >= ItemTotalCount)
			{
				itemIndex = ItemTotalCount - 1;
			}
			if (itemIndex < 0)
			{
				itemIndex = 0;
			}
			RowColumnPair rowColumnByItemIndex = GetRowColumnByItemIndex(itemIndex);
			MovePanelToItemByRowColumn(rowColumnByItemIndex.mRow, rowColumnByItemIndex.mColumn, offsetX, offsetY);
		}
	}

	public void MovePanelToItemByRowColumn(int row, int column, float offsetX = 0f, float offsetY = 0f)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		mScrollRect.StopMovement();
		mCurSnapData.Clear();
		if (mItemTotalCount == 0)
		{
			return;
		}
		Vector2 itemPos = GetItemPos(row, column);
		Vector3 anchoredPosition3D = mContainerTrans.anchoredPosition3D;
		Rect rect;
		if (mScrollRect.horizontal)
		{
			rect = ContainerTrans.rect;
			float num = Mathf.Max(((Rect)(ref rect)).width - ViewPortWidth, 0f);
			if (num > 0f)
			{
				float num2 = 0f - itemPos.x + offsetX;
				num2 = Mathf.Min(Mathf.Abs(num2), num) * Mathf.Sign(num2);
				anchoredPosition3D.x = num2;
			}
		}
		if (mScrollRect.vertical)
		{
			rect = ContainerTrans.rect;
			float num3 = Mathf.Max(((Rect)(ref rect)).height - ViewPortHeight, 0f);
			if (num3 > 0f)
			{
				float num4 = 0f - itemPos.y + offsetY;
				num4 = Mathf.Min(Mathf.Abs(num4), num3) * Mathf.Sign(num4);
				anchoredPosition3D.y = num4;
			}
		}
		if (anchoredPosition3D != mContainerTrans.anchoredPosition3D)
		{
			mContainerTrans.anchoredPosition3D = anchoredPosition3D;
		}
		VaildAndSetContainerPos();
		ForceToCheckContentPos();
	}

	public void RefreshAllShownItem()
	{
		if (mItemGroupList.Count != 0)
		{
			ForceToCheckContentPos();
			RecycleAllItem();
			UpdateGridViewContent();
		}
	}

	public virtual void OnBeginDrag(PointerEventData eventData)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		if ((int)eventData.button == 0)
		{
			mCurSnapData.Clear();
			mIsDraging = true;
			if (mOnBeginDragAction != null)
			{
				mOnBeginDragAction(eventData);
			}
		}
	}

	public virtual void OnEndDrag(PointerEventData eventData)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		if ((int)eventData.button == 0)
		{
			mIsDraging = false;
			ForceSnapUpdateCheck();
			if (mOnEndDragAction != null)
			{
				mOnEndDragAction(eventData);
			}
		}
	}

	public virtual void OnDrag(PointerEventData eventData)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		if ((int)eventData.button == 0 && mOnDragingAction != null)
		{
			mOnDragingAction(eventData);
		}
	}

	public int GetItemIndexByRowColumn(int row, int column)
	{
		if (mGridFixedType == GridFixedType.ColumnCountFixed)
		{
			return row * mFixedRowOrColumnCount + column;
		}
		return column * mFixedRowOrColumnCount + row;
	}

	public RowColumnPair GetRowColumnByItemIndex(int itemIndex)
	{
		if (itemIndex < 0)
		{
			itemIndex = 0;
		}
		if (mGridFixedType == GridFixedType.ColumnCountFixed)
		{
			int row = itemIndex / mFixedRowOrColumnCount;
			int column = itemIndex % mFixedRowOrColumnCount;
			return new RowColumnPair(row, column);
		}
		int column2 = itemIndex / mFixedRowOrColumnCount;
		return new RowColumnPair(itemIndex % mFixedRowOrColumnCount, column2);
	}

	public Vector2 GetItemAbsPos(int row, int column)
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		float num = mStartPadding.x + (float)column * mItemSizeWithPadding.x;
		float num2 = mStartPadding.y + (float)row * mItemSizeWithPadding.y;
		return new Vector2(num, num2);
	}

	public Vector2 GetItemPos(int row, int column)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		Vector2 itemAbsPos = GetItemAbsPos(row, column);
		float x = itemAbsPos.x;
		float y = itemAbsPos.y;
		if (ArrangeType == GridItemArrangeType.TopLeftToBottomRight)
		{
			return new Vector2(x, 0f - y);
		}
		if (ArrangeType == GridItemArrangeType.BottomLeftToTopRight)
		{
			return new Vector2(x, y);
		}
		if (ArrangeType == GridItemArrangeType.TopRightToBottomLeft)
		{
			return new Vector2(0f - x, 0f - y);
		}
		if (ArrangeType == GridItemArrangeType.BottomRightToTopLeft)
		{
			return new Vector2(0f - x, y);
		}
		return Vector2.zero;
	}

	public LoopGridViewItem GetShownItemByItemIndex(int itemIndex)
	{
		if (itemIndex < 0 || itemIndex >= ItemTotalCount)
		{
			return null;
		}
		if (mItemGroupList.Count == 0)
		{
			return null;
		}
		RowColumnPair rowColumnByItemIndex = GetRowColumnByItemIndex(itemIndex);
		return GetShownItemByRowColumn(rowColumnByItemIndex.mRow, rowColumnByItemIndex.mColumn);
	}

	public LoopGridViewItem GetShownItemByRowColumn(int row, int column)
	{
		if (mItemGroupList.Count == 0)
		{
			return null;
		}
		if (mGridFixedType == GridFixedType.ColumnCountFixed)
		{
			return GetShownGroup(row)?.GetItemByColumn(column);
		}
		return GetShownGroup(column)?.GetItemByRow(row);
	}

	public void UpdateAllGridSetting()
	{
		UpdateStartEndPadding();
		UpdateItemSize();
		UpdateColumnRowCount();
		UpdateContentSize();
		ForceSnapUpdateCheck();
		ForceToCheckContentPos();
	}

	public void SetGridFixedGroupCount(GridFixedType fixedType, int count)
	{
		if (mGridFixedType != fixedType || mFixedRowOrColumnCount != count)
		{
			mGridFixedType = fixedType;
			mFixedRowOrColumnCount = count;
			UpdateColumnRowCount();
			UpdateContentSize();
			if (mItemGroupList.Count != 0)
			{
				RecycleAllItem();
				ForceSnapUpdateCheck();
				ForceToCheckContentPos();
			}
		}
	}

	public void SetItemSize(Vector2 newSize)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		if (!(newSize == mItemSize))
		{
			mItemSize = newSize;
			UpdateItemSize();
			UpdateContentSize();
			if (mItemGroupList.Count != 0)
			{
				RecycleAllItem();
				ForceSnapUpdateCheck();
				ForceToCheckContentPos();
			}
		}
	}

	public void SetItemPadding(Vector2 newPadding)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		if (!(newPadding == mItemPadding))
		{
			mItemPadding = newPadding;
			UpdateItemSize();
			UpdateContentSize();
			if (mItemGroupList.Count != 0)
			{
				RecycleAllItem();
				ForceSnapUpdateCheck();
				ForceToCheckContentPos();
			}
		}
	}

	public void SetPadding(RectOffset newPadding)
	{
		if (newPadding != mPadding)
		{
			mPadding = newPadding;
			UpdateStartEndPadding();
			UpdateContentSize();
			if (mItemGroupList.Count != 0)
			{
				RecycleAllItem();
				ForceSnapUpdateCheck();
				ForceToCheckContentPos();
			}
		}
	}

	public void UpdateContentSize()
	{
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		float num = mStartPadding.x + (float)mColumnCount * mItemSizeWithPadding.x - mItemPadding.x + mEndPadding.x;
		float num2 = mStartPadding.y + (float)mRowCount * mItemSizeWithPadding.y - mItemPadding.y + mEndPadding.y;
		Rect rect = mContainerTrans.rect;
		if (((Rect)(ref rect)).height != num2)
		{
			mContainerTrans.SetSizeWithCurrentAnchors((Axis)1, num2);
		}
		rect = mContainerTrans.rect;
		if (((Rect)(ref rect)).width != num)
		{
			mContainerTrans.SetSizeWithCurrentAnchors((Axis)0, num);
		}
	}

	public void VaildAndSetContainerPos()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		Vector3 anchoredPosition3D = mContainerTrans.anchoredPosition3D;
		mContainerTrans.anchoredPosition3D = Vector2.op_Implicit(GetContainerVaildPos(anchoredPosition3D.x, anchoredPosition3D.y));
	}

	public void ClearAllTmpRecycledItem()
	{
		int count = mItemPoolList.Count;
		for (int i = 0; i < count; i++)
		{
			mItemPoolList[i].ClearTmpRecycledItem();
		}
	}

	public void RecycleAllItem()
	{
		foreach (GridItemGroup mItemGroup in mItemGroupList)
		{
			RecycleItemGroupTmp(mItemGroup);
		}
		mItemGroupList.Clear();
	}

	public void UpdateGridViewContent()
	{
		mListUpdateCheckFrameCount++;
		if (mItemTotalCount == 0)
		{
			if (mItemGroupList.Count > 0)
			{
				RecycleAllItem();
			}
			return;
		}
		UpdateCurFrameItemRangeData();
		if (mGridFixedType == GridFixedType.ColumnCountFixed)
		{
			int count = mItemGroupList.Count;
			int mMinRow = mCurFrameItemRangeData.mMinRow;
			int mMaxRow = mCurFrameItemRangeData.mMaxRow;
			for (int num = count - 1; num >= 0; num--)
			{
				GridItemGroup gridItemGroup = mItemGroupList[num];
				if (gridItemGroup.GroupIndex < mMinRow || gridItemGroup.GroupIndex > mMaxRow)
				{
					RecycleItemGroupTmp(gridItemGroup);
					mItemGroupList.RemoveAt(num);
				}
			}
			if (mItemGroupList.Count == 0)
			{
				GridItemGroup item = CreateItemGroup(mMinRow);
				mItemGroupList.Add(item);
			}
			while (mItemGroupList[0].GroupIndex > mMinRow)
			{
				GridItemGroup item2 = CreateItemGroup(mItemGroupList[0].GroupIndex - 1);
				mItemGroupList.Insert(0, item2);
			}
			while (mItemGroupList[mItemGroupList.Count - 1].GroupIndex < mMaxRow)
			{
				GridItemGroup item3 = CreateItemGroup(mItemGroupList[mItemGroupList.Count - 1].GroupIndex + 1);
				mItemGroupList.Add(item3);
			}
			int count2 = mItemGroupList.Count;
			for (int i = 0; i < count2; i++)
			{
				UpdateRowItemGroupForRecycleAndNew(mItemGroupList[i]);
			}
			return;
		}
		int count3 = mItemGroupList.Count;
		int mMinColumn = mCurFrameItemRangeData.mMinColumn;
		int mMaxColumn = mCurFrameItemRangeData.mMaxColumn;
		for (int num2 = count3 - 1; num2 >= 0; num2--)
		{
			GridItemGroup gridItemGroup2 = mItemGroupList[num2];
			if (gridItemGroup2.GroupIndex < mMinColumn || gridItemGroup2.GroupIndex > mMaxColumn)
			{
				RecycleItemGroupTmp(gridItemGroup2);
				mItemGroupList.RemoveAt(num2);
			}
		}
		if (mItemGroupList.Count == 0)
		{
			GridItemGroup item4 = CreateItemGroup(mMinColumn);
			mItemGroupList.Add(item4);
		}
		while (mItemGroupList[0].GroupIndex > mMinColumn)
		{
			GridItemGroup item5 = CreateItemGroup(mItemGroupList[0].GroupIndex - 1);
			mItemGroupList.Insert(0, item5);
		}
		while (mItemGroupList[mItemGroupList.Count - 1].GroupIndex < mMaxColumn)
		{
			GridItemGroup item6 = CreateItemGroup(mItemGroupList[mItemGroupList.Count - 1].GroupIndex + 1);
			mItemGroupList.Add(item6);
		}
		int count4 = mItemGroupList.Count;
		for (int j = 0; j < count4; j++)
		{
			UpdateColumnItemGroupForRecycleAndNew(mItemGroupList[j]);
		}
	}

	public void UpdateStartEndPadding()
	{
		if (ArrangeType == GridItemArrangeType.TopLeftToBottomRight)
		{
			mStartPadding.x = mPadding.left;
			mStartPadding.y = mPadding.top;
			mEndPadding.x = mPadding.right;
			mEndPadding.y = mPadding.bottom;
		}
		else if (ArrangeType == GridItemArrangeType.BottomLeftToTopRight)
		{
			mStartPadding.x = mPadding.left;
			mStartPadding.y = mPadding.bottom;
			mEndPadding.x = mPadding.right;
			mEndPadding.y = mPadding.top;
		}
		else if (ArrangeType == GridItemArrangeType.TopRightToBottomLeft)
		{
			mStartPadding.x = mPadding.right;
			mStartPadding.y = mPadding.top;
			mEndPadding.x = mPadding.left;
			mEndPadding.y = mPadding.bottom;
		}
		else if (ArrangeType == GridItemArrangeType.BottomRightToTopLeft)
		{
			mStartPadding.x = mPadding.right;
			mStartPadding.y = mPadding.bottom;
			mEndPadding.x = mPadding.left;
			mEndPadding.y = mPadding.top;
		}
	}

	public void UpdateItemSize()
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		if (mItemSize.x > 0f && mItemSize.y > 0f)
		{
			mItemSizeWithPadding = mItemSize + mItemPadding;
			return;
		}
		if (mItemPrefabDataList.Count != 0)
		{
			GameObject mItemPrefab = mItemPrefabDataList[0].mItemPrefab;
			if (!((Object)(object)mItemPrefab == (Object)null))
			{
				RectTransform component = mItemPrefab.GetComponent<RectTransform>();
				if (!((Object)(object)component == (Object)null))
				{
					Rect rect = component.rect;
					mItemSize = ((Rect)(ref rect)).size;
					mItemSizeWithPadding = mItemSize + mItemPadding;
				}
			}
		}
		if (mItemSize.x <= 0f || mItemSize.y <= 0f)
		{
			Debug.LogError((object)"Error, ItemSize is invaild.");
		}
	}

	public void UpdateColumnRowCount()
	{
		if (mGridFixedType == GridFixedType.ColumnCountFixed)
		{
			mColumnCount = mFixedRowOrColumnCount;
			mRowCount = mItemTotalCount / mColumnCount;
			if (mItemTotalCount % mColumnCount > 0)
			{
				mRowCount++;
			}
			if (mItemTotalCount <= mColumnCount)
			{
				mColumnCount = mItemTotalCount;
			}
		}
		else
		{
			mRowCount = mFixedRowOrColumnCount;
			mColumnCount = mItemTotalCount / mRowCount;
			if (mItemTotalCount % mRowCount > 0)
			{
				mColumnCount++;
			}
			if (mItemTotalCount <= mRowCount)
			{
				mRowCount = mItemTotalCount;
			}
		}
	}

	private bool IsContainerTransCanMove()
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		if (mItemTotalCount == 0)
		{
			return false;
		}
		Rect rect;
		if (mScrollRect.horizontal)
		{
			rect = ContainerTrans.rect;
			if (((Rect)(ref rect)).width > ViewPortWidth)
			{
				return true;
			}
		}
		if (mScrollRect.vertical)
		{
			rect = ContainerTrans.rect;
			if (((Rect)(ref rect)).height > ViewPortHeight)
			{
				return true;
			}
		}
		return false;
	}

	private void RecycleItemGroupTmp(GridItemGroup group)
	{
		if (group != null)
		{
			while ((Object)(object)group.First != (Object)null)
			{
				LoopGridViewItem item = group.RemoveFirst();
				RecycleItemTmp(item);
			}
			group.Clear();
			RecycleOneItemGroupObj(group);
		}
	}

	private void RecycleItemTmp(LoopGridViewItem item)
	{
		if (!((Object)(object)item == (Object)null) && !string.IsNullOrEmpty(item.ItemPrefabName))
		{
			GridItemPool value = null;
			if (mItemPoolDict.TryGetValue(item.ItemPrefabName, out value))
			{
				value.RecycleItem(item);
			}
		}
	}

	private void AdjustViewPortPivot()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		RectTransform val = mViewPortRectTransform;
		if (ArrangeType == GridItemArrangeType.TopLeftToBottomRight)
		{
			val.pivot = new Vector2(0f, 1f);
		}
		else if (ArrangeType == GridItemArrangeType.BottomLeftToTopRight)
		{
			val.pivot = new Vector2(0f, 0f);
		}
		else if (ArrangeType == GridItemArrangeType.TopRightToBottomLeft)
		{
			val.pivot = new Vector2(1f, 1f);
		}
		else if (ArrangeType == GridItemArrangeType.BottomRightToTopLeft)
		{
			val.pivot = new Vector2(1f, 0f);
		}
	}

	private void AdjustContainerAnchorAndPivot()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		RectTransform containerTrans = ContainerTrans;
		if (ArrangeType == GridItemArrangeType.TopLeftToBottomRight)
		{
			containerTrans.anchorMin = new Vector2(0f, 1f);
			containerTrans.anchorMax = new Vector2(0f, 1f);
			containerTrans.pivot = new Vector2(0f, 1f);
		}
		else if (ArrangeType == GridItemArrangeType.BottomLeftToTopRight)
		{
			containerTrans.anchorMin = new Vector2(0f, 0f);
			containerTrans.anchorMax = new Vector2(0f, 0f);
			containerTrans.pivot = new Vector2(0f, 0f);
		}
		else if (ArrangeType == GridItemArrangeType.TopRightToBottomLeft)
		{
			containerTrans.anchorMin = new Vector2(1f, 1f);
			containerTrans.anchorMax = new Vector2(1f, 1f);
			containerTrans.pivot = new Vector2(1f, 1f);
		}
		else if (ArrangeType == GridItemArrangeType.BottomRightToTopLeft)
		{
			containerTrans.anchorMin = new Vector2(1f, 0f);
			containerTrans.anchorMax = new Vector2(1f, 0f);
			containerTrans.pivot = new Vector2(1f, 0f);
		}
	}

	private void AdjustItemAnchorAndPivot(RectTransform rtf)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		if (ArrangeType == GridItemArrangeType.TopLeftToBottomRight)
		{
			rtf.anchorMin = new Vector2(0f, 1f);
			rtf.anchorMax = new Vector2(0f, 1f);
			rtf.pivot = new Vector2(0f, 1f);
		}
		else if (ArrangeType == GridItemArrangeType.BottomLeftToTopRight)
		{
			rtf.anchorMin = new Vector2(0f, 0f);
			rtf.anchorMax = new Vector2(0f, 0f);
			rtf.pivot = new Vector2(0f, 0f);
		}
		else if (ArrangeType == GridItemArrangeType.TopRightToBottomLeft)
		{
			rtf.anchorMin = new Vector2(1f, 1f);
			rtf.anchorMax = new Vector2(1f, 1f);
			rtf.pivot = new Vector2(1f, 1f);
		}
		else if (ArrangeType == GridItemArrangeType.BottomRightToTopLeft)
		{
			rtf.anchorMin = new Vector2(1f, 0f);
			rtf.anchorMax = new Vector2(1f, 0f);
			rtf.pivot = new Vector2(1f, 0f);
		}
	}

	private void InitItemPool()
	{
		foreach (GridViewItemPrefabConfData mItemPrefabData in mItemPrefabDataList)
		{
			if ((Object)(object)mItemPrefabData.mItemPrefab == (Object)null)
			{
				Debug.LogError((object)"A item prefab is null ");
				continue;
			}
			string name = ((Object)mItemPrefabData.mItemPrefab).name;
			if (mItemPoolDict.ContainsKey(name))
			{
				Debug.LogError((object)("A item prefab with name " + name + " has existed!"));
				continue;
			}
			RectTransform component = mItemPrefabData.mItemPrefab.GetComponent<RectTransform>();
			if ((Object)(object)component == (Object)null)
			{
				Debug.LogError((object)("RectTransform component is not found in the prefab " + name));
				continue;
			}
			AdjustItemAnchorAndPivot(component);
			if ((Object)(object)mItemPrefabData.mItemPrefab.GetComponent<LoopGridViewItem>() == (Object)null)
			{
				mItemPrefabData.mItemPrefab.AddComponent<LoopGridViewItem>();
			}
			GridItemPool gridItemPool = new GridItemPool();
			gridItemPool.Init(mItemPrefabData.mItemPrefab, mItemPrefabData.mInitCreateCount, mContainerTrans);
			mItemPoolDict.Add(name, gridItemPool);
			mItemPoolList.Add(gridItemPool);
		}
	}

	private LoopGridViewItem GetNewItemByRowColumn(int row, int column)
	{
		int itemIndexByRowColumn = GetItemIndexByRowColumn(row, column);
		if (itemIndexByRowColumn < 0 || itemIndexByRowColumn >= ItemTotalCount)
		{
			return null;
		}
		LoopGridViewItem loopGridViewItem = mOnGetItemByRowColumn(this, itemIndexByRowColumn, row, column);
		if ((Object)(object)loopGridViewItem == (Object)null)
		{
			return null;
		}
		loopGridViewItem.NextItem = null;
		loopGridViewItem.PrevItem = null;
		loopGridViewItem.Row = row;
		loopGridViewItem.Column = column;
		loopGridViewItem.ItemIndex = itemIndexByRowColumn;
		loopGridViewItem.ItemCreatedCheckFrameCount = mListUpdateCheckFrameCount;
		return loopGridViewItem;
	}

	private RowColumnPair GetCeilItemRowColumnAtGivenAbsPos(float ax, float ay)
	{
		ax = Mathf.Abs(ax);
		ay = Mathf.Abs(ay);
		int num = Mathf.CeilToInt((ay - mStartPadding.y) / mItemSizeWithPadding.y) - 1;
		int num2 = Mathf.CeilToInt((ax - mStartPadding.x) / mItemSizeWithPadding.x) - 1;
		if (num < 0)
		{
			num = 0;
		}
		if (num >= mRowCount)
		{
			num = mRowCount - 1;
		}
		if (num2 < 0)
		{
			num2 = 0;
		}
		if (num2 >= mColumnCount)
		{
			num2 = mColumnCount - 1;
		}
		return new RowColumnPair(num, num2);
	}

	private void Update()
	{
		if (mListViewInited)
		{
			UpdateSnapMove();
			UpdateGridViewContent();
			ClearAllTmpRecycledItem();
		}
	}

	private GridItemGroup CreateItemGroup(int groupIndex)
	{
		GridItemGroup oneItemGroupObj = GetOneItemGroupObj();
		oneItemGroupObj.GroupIndex = groupIndex;
		return oneItemGroupObj;
	}

	private Vector2 GetContainerMovedDistance()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		Vector2 containerVaildPos = GetContainerVaildPos(ContainerTrans.anchoredPosition3D.x, ContainerTrans.anchoredPosition3D.y);
		return new Vector2(Mathf.Abs(containerVaildPos.x), Mathf.Abs(containerVaildPos.y));
	}

	private Vector2 GetContainerVaildPos(float curX, float curY)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		Rect rect = ContainerTrans.rect;
		float num = Mathf.Max(((Rect)(ref rect)).width - ViewPortWidth, 0f);
		rect = ContainerTrans.rect;
		float num2 = Mathf.Max(((Rect)(ref rect)).height - ViewPortHeight, 0f);
		if (mArrangeType == GridItemArrangeType.TopLeftToBottomRight)
		{
			curX = Mathf.Clamp(curX, 0f - num, 0f);
			curY = Mathf.Clamp(curY, 0f, num2);
		}
		else if (mArrangeType == GridItemArrangeType.BottomLeftToTopRight)
		{
			curX = Mathf.Clamp(curX, 0f - num, 0f);
			curY = Mathf.Clamp(curY, 0f - num2, 0f);
		}
		else if (mArrangeType == GridItemArrangeType.BottomRightToTopLeft)
		{
			curX = Mathf.Clamp(curX, 0f, num);
			curY = Mathf.Clamp(curY, 0f - num2, 0f);
		}
		else if (mArrangeType == GridItemArrangeType.TopRightToBottomLeft)
		{
			curX = Mathf.Clamp(curX, 0f, num);
			curY = Mathf.Clamp(curY, 0f, num2);
		}
		return new Vector2(curX, curY);
	}

	private void UpdateCurFrameItemRangeData()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		Vector2 containerMovedDistance = GetContainerMovedDistance();
		if (mNeedCheckContentPosLeftCount > 0 || !(mCurFrameItemRangeData.mCheckedPosition == containerMovedDistance))
		{
			if (mNeedCheckContentPosLeftCount > 0)
			{
				mNeedCheckContentPosLeftCount--;
			}
			float num = containerMovedDistance.x - mItemRecycleDistance.x;
			float num2 = containerMovedDistance.y - mItemRecycleDistance.y;
			if (num < 0f)
			{
				num = 0f;
			}
			if (num2 < 0f)
			{
				num2 = 0f;
			}
			RowColumnPair ceilItemRowColumnAtGivenAbsPos = GetCeilItemRowColumnAtGivenAbsPos(num, num2);
			mCurFrameItemRangeData.mMinColumn = ceilItemRowColumnAtGivenAbsPos.mColumn;
			mCurFrameItemRangeData.mMinRow = ceilItemRowColumnAtGivenAbsPos.mRow;
			num = containerMovedDistance.x + mItemRecycleDistance.x + ViewPortWidth;
			num2 = containerMovedDistance.y + mItemRecycleDistance.y + ViewPortHeight;
			ceilItemRowColumnAtGivenAbsPos = GetCeilItemRowColumnAtGivenAbsPos(num, num2);
			mCurFrameItemRangeData.mMaxColumn = ceilItemRowColumnAtGivenAbsPos.mColumn;
			mCurFrameItemRangeData.mMaxRow = ceilItemRowColumnAtGivenAbsPos.mRow;
			mCurFrameItemRangeData.mCheckedPosition = containerMovedDistance;
		}
	}

	private void UpdateRowItemGroupForRecycleAndNew(GridItemGroup group)
	{
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		int mMinColumn = mCurFrameItemRangeData.mMinColumn;
		int mMaxColumn = mCurFrameItemRangeData.mMaxColumn;
		int groupIndex = group.GroupIndex;
		while ((Object)(object)group.First != (Object)null && group.First.Column < mMinColumn)
		{
			RecycleItemTmp(group.RemoveFirst());
		}
		while ((Object)(object)group.Last != (Object)null && (group.Last.Column > mMaxColumn || group.Last.ItemIndex >= ItemTotalCount))
		{
			RecycleItemTmp(group.RemoveLast());
		}
		if ((Object)(object)group.First == (Object)null)
		{
			LoopGridViewItem newItemByRowColumn = GetNewItemByRowColumn(groupIndex, mMinColumn);
			if ((Object)(object)newItemByRowColumn == (Object)null)
			{
				return;
			}
			newItemByRowColumn.CachedRectTransform.anchoredPosition3D = Vector2.op_Implicit(GetItemPos(newItemByRowColumn.Row, newItemByRowColumn.Column));
			group.AddFirst(newItemByRowColumn);
		}
		while (group.First.Column > mMinColumn)
		{
			LoopGridViewItem newItemByRowColumn2 = GetNewItemByRowColumn(groupIndex, group.First.Column - 1);
			if ((Object)(object)newItemByRowColumn2 == (Object)null)
			{
				break;
			}
			newItemByRowColumn2.CachedRectTransform.anchoredPosition3D = Vector2.op_Implicit(GetItemPos(newItemByRowColumn2.Row, newItemByRowColumn2.Column));
			group.AddFirst(newItemByRowColumn2);
		}
		while (group.Last.Column < mMaxColumn)
		{
			LoopGridViewItem newItemByRowColumn3 = GetNewItemByRowColumn(groupIndex, group.Last.Column + 1);
			if (!((Object)(object)newItemByRowColumn3 == (Object)null))
			{
				newItemByRowColumn3.CachedRectTransform.anchoredPosition3D = Vector2.op_Implicit(GetItemPos(newItemByRowColumn3.Row, newItemByRowColumn3.Column));
				group.AddLast(newItemByRowColumn3);
				continue;
			}
			break;
		}
	}

	private void UpdateColumnItemGroupForRecycleAndNew(GridItemGroup group)
	{
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		int mMinRow = mCurFrameItemRangeData.mMinRow;
		int mMaxRow = mCurFrameItemRangeData.mMaxRow;
		int groupIndex = group.GroupIndex;
		while ((Object)(object)group.First != (Object)null && group.First.Row < mMinRow)
		{
			RecycleItemTmp(group.RemoveFirst());
		}
		while ((Object)(object)group.Last != (Object)null && (group.Last.Row > mMaxRow || group.Last.ItemIndex >= ItemTotalCount))
		{
			RecycleItemTmp(group.RemoveLast());
		}
		if ((Object)(object)group.First == (Object)null)
		{
			LoopGridViewItem newItemByRowColumn = GetNewItemByRowColumn(mMinRow, groupIndex);
			if ((Object)(object)newItemByRowColumn == (Object)null)
			{
				return;
			}
			newItemByRowColumn.CachedRectTransform.anchoredPosition3D = Vector2.op_Implicit(GetItemPos(newItemByRowColumn.Row, newItemByRowColumn.Column));
			group.AddFirst(newItemByRowColumn);
		}
		while (group.First.Row > mMinRow)
		{
			LoopGridViewItem newItemByRowColumn2 = GetNewItemByRowColumn(group.First.Row - 1, groupIndex);
			if ((Object)(object)newItemByRowColumn2 == (Object)null)
			{
				break;
			}
			newItemByRowColumn2.CachedRectTransform.anchoredPosition3D = Vector2.op_Implicit(GetItemPos(newItemByRowColumn2.Row, newItemByRowColumn2.Column));
			group.AddFirst(newItemByRowColumn2);
		}
		while (group.Last.Row < mMaxRow)
		{
			LoopGridViewItem newItemByRowColumn3 = GetNewItemByRowColumn(group.Last.Row + 1, groupIndex);
			if (!((Object)(object)newItemByRowColumn3 == (Object)null))
			{
				newItemByRowColumn3.CachedRectTransform.anchoredPosition3D = Vector2.op_Implicit(GetItemPos(newItemByRowColumn3.Row, newItemByRowColumn3.Column));
				group.AddLast(newItemByRowColumn3);
				continue;
			}
			break;
		}
	}

	private void SetScrollbarListener()
	{
		if (ItemSnapEnable)
		{
			mScrollBarClickEventListener1 = null;
			mScrollBarClickEventListener2 = null;
			Scrollbar val = null;
			Scrollbar val2 = null;
			if (mScrollRect.vertical && (Object)(object)mScrollRect.verticalScrollbar != (Object)null)
			{
				val = mScrollRect.verticalScrollbar;
			}
			if (mScrollRect.horizontal && (Object)(object)mScrollRect.horizontalScrollbar != (Object)null)
			{
				val2 = mScrollRect.horizontalScrollbar;
			}
			if ((Object)(object)val != (Object)null)
			{
				ClickEventListener clickEventListener = (mScrollBarClickEventListener1 = ClickEventListener.Get(((Component)val).gameObject));
				clickEventListener.SetPointerUpHandler(OnPointerUpInScrollBar);
				clickEventListener.SetPointerDownHandler(OnPointerDownInScrollBar);
			}
			if ((Object)(object)val2 != (Object)null)
			{
				ClickEventListener clickEventListener2 = (mScrollBarClickEventListener2 = ClickEventListener.Get(((Component)val2).gameObject));
				clickEventListener2.SetPointerUpHandler(OnPointerUpInScrollBar);
				clickEventListener2.SetPointerDownHandler(OnPointerDownInScrollBar);
			}
		}
	}

	private void OnPointerDownInScrollBar(GameObject obj)
	{
		mCurSnapData.Clear();
	}

	private void OnPointerUpInScrollBar(GameObject obj)
	{
		ForceSnapUpdateCheck();
	}

	private RowColumnPair FindNearestItemWithLocalPos(float x, float y)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		Vector2 val = default(Vector2);
		((Vector2)(ref val))._002Ector(x, y);
		RowColumnPair ceilItemRowColumnAtGivenAbsPos = GetCeilItemRowColumnAtGivenAbsPos(val.x, val.y);
		int mRow = ceilItemRowColumnAtGivenAbsPos.mRow;
		int mColumn = ceilItemRowColumnAtGivenAbsPos.mColumn;
		float num = 0f;
		RowColumnPair result = new RowColumnPair(-1, -1);
		_ = Vector2.zero;
		float num2 = float.MaxValue;
		for (int i = mRow - 1; i <= mRow + 1; i++)
		{
			for (int j = mColumn - 1; j <= mColumn + 1; j++)
			{
				if (i >= 0 && i < mRowCount && j >= 0 && j < mColumnCount)
				{
					Vector2 val2 = GetItemSnapPivotLocalPos(i, j) - val;
					num = ((Vector2)(ref val2)).sqrMagnitude;
					if (num < num2)
					{
						num2 = num;
						result.mRow = i;
						result.mColumn = j;
					}
				}
			}
		}
		return result;
	}

	private Vector2 GetItemSnapPivotLocalPos(int row, int column)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		Vector2 itemAbsPos = GetItemAbsPos(row, column);
		if (mArrangeType == GridItemArrangeType.TopLeftToBottomRight)
		{
			float num = itemAbsPos.x + mItemSize.x * mItemSnapPivot.x;
			float num2 = 0f - itemAbsPos.y - mItemSize.y * (1f - mItemSnapPivot.y);
			return new Vector2(num, num2);
		}
		if (mArrangeType == GridItemArrangeType.BottomLeftToTopRight)
		{
			float num3 = itemAbsPos.x + mItemSize.x * mItemSnapPivot.x;
			float num4 = itemAbsPos.y + mItemSize.y * mItemSnapPivot.y;
			return new Vector2(num3, num4);
		}
		if (mArrangeType == GridItemArrangeType.TopRightToBottomLeft)
		{
			float num5 = 0f - itemAbsPos.x - mItemSize.x * (1f - mItemSnapPivot.x);
			float num6 = 0f - itemAbsPos.y - mItemSize.y * (1f - mItemSnapPivot.y);
			return new Vector2(num5, num6);
		}
		if (mArrangeType == GridItemArrangeType.BottomRightToTopLeft)
		{
			float num7 = 0f - itemAbsPos.x - mItemSize.x * (1f - mItemSnapPivot.x);
			float num8 = itemAbsPos.y + mItemSize.y * mItemSnapPivot.y;
			return new Vector2(num7, num8);
		}
		return Vector2.zero;
	}

	private Vector2 GetViewPortSnapPivotLocalPos(Vector2 pos)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		float num = 0f;
		float num2 = 0f;
		if (mArrangeType == GridItemArrangeType.TopLeftToBottomRight)
		{
			num = 0f - pos.x + ViewPortWidth * mViewPortSnapPivot.x;
			num2 = 0f - pos.y - ViewPortHeight * (1f - mViewPortSnapPivot.y);
		}
		else if (mArrangeType == GridItemArrangeType.BottomLeftToTopRight)
		{
			num = 0f - pos.x + ViewPortWidth * mViewPortSnapPivot.x;
			num2 = 0f - pos.y + ViewPortHeight * mViewPortSnapPivot.y;
		}
		else if (mArrangeType == GridItemArrangeType.TopRightToBottomLeft)
		{
			num = 0f - pos.x - ViewPortWidth * (1f - mViewPortSnapPivot.x);
			num2 = 0f - pos.y - ViewPortHeight * (1f - mViewPortSnapPivot.y);
		}
		else if (mArrangeType == GridItemArrangeType.BottomRightToTopLeft)
		{
			num = 0f - pos.x - ViewPortWidth * (1f - mViewPortSnapPivot.x);
			num2 = 0f - pos.y + ViewPortHeight * mViewPortSnapPivot.y;
		}
		return new Vector2(num, num2);
	}

	private void UpdateNearestSnapItem(bool forceSendEvent)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		if (!mItemSnapEnable || mItemGroupList.Count == 0 || !IsContainerTransCanMove())
		{
			return;
		}
		Vector2 containerVaildPos = GetContainerVaildPos(ContainerTrans.anchoredPosition3D.x, ContainerTrans.anchoredPosition3D.y);
		bool flag = containerVaildPos.y != mLastSnapCheckPos.y || containerVaildPos.x != mLastSnapCheckPos.x;
		mLastSnapCheckPos = Vector2.op_Implicit(containerVaildPos);
		if (!flag && mLeftSnapUpdateExtraCount > 0)
		{
			mLeftSnapUpdateExtraCount--;
			flag = true;
		}
		if (!flag)
		{
			return;
		}
		RowColumnPair rowColumnPair = new RowColumnPair(-1, -1);
		Vector2 viewPortSnapPivotLocalPos = GetViewPortSnapPivotLocalPos(containerVaildPos);
		rowColumnPair = FindNearestItemWithLocalPos(viewPortSnapPivotLocalPos.x, viewPortSnapPivotLocalPos.y);
		if (rowColumnPair.mRow >= 0)
		{
			RowColumnPair rowColumnPair2 = mCurSnapNearestItemRowColumn;
			mCurSnapNearestItemRowColumn = rowColumnPair;
			if ((forceSendEvent || rowColumnPair2 != mCurSnapNearestItemRowColumn) && mOnSnapNearestChanged != null)
			{
				mOnSnapNearestChanged(this);
			}
		}
		else
		{
			mCurSnapNearestItemRowColumn.mRow = -1;
			mCurSnapNearestItemRowColumn.mColumn = -1;
		}
	}

	private void UpdateFromSettingParam(LoopGridViewSettingParam param)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Expected O, but got Unknown
		if (param != null)
		{
			if (param.mItemSize != null)
			{
				mItemSize = (Vector2)param.mItemSize;
			}
			if (param.mItemPadding != null)
			{
				mItemPadding = (Vector2)param.mItemPadding;
			}
			if (param.mPadding != null)
			{
				mPadding = (RectOffset)param.mPadding;
			}
			if (param.mGridFixedType != null)
			{
				mGridFixedType = (GridFixedType)param.mGridFixedType;
			}
			if (param.mFixedRowOrColumnCount != null)
			{
				mFixedRowOrColumnCount = (int)param.mFixedRowOrColumnCount;
			}
		}
	}

	public void FinishSnapImmediately()
	{
		UpdateSnapMove(immediate: true);
	}

	private void UpdateSnapMove(bool immediate = false, bool forceSendEvent = false)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		if (!mItemSnapEnable)
		{
			return;
		}
		UpdateNearestSnapItem(forceSendEvent: false);
		Vector2 val = Vector2.op_Implicit(mContainerTrans.anchoredPosition3D);
		if (!CanSnap())
		{
			ClearSnapData();
			return;
		}
		UpdateCurSnapData();
		if (mCurSnapData.mSnapStatus != SnapStatus.SnapMoving)
		{
			return;
		}
		if (Mathf.Abs(mScrollRect.velocity.x) + Mathf.Abs(mScrollRect.velocity.y) > 0f)
		{
			mScrollRect.StopMovement();
		}
		float mCurSnapVal = mCurSnapData.mCurSnapVal;
		mCurSnapData.mCurSnapVal = Mathf.SmoothDamp(mCurSnapData.mCurSnapVal, mCurSnapData.mTargetSnapVal, ref mSmoothDumpVel, mSmoothDumpRate);
		float num = mCurSnapData.mCurSnapVal - mCurSnapVal;
		if (immediate || Mathf.Abs(mCurSnapData.mTargetSnapVal - mCurSnapData.mCurSnapVal) < mSnapFinishThreshold)
		{
			val += (mCurSnapData.mTargetSnapVal - mCurSnapVal) * mCurSnapData.mSnapNeedMoveDir;
			mCurSnapData.mSnapStatus = SnapStatus.SnapMoveFinish;
			if (mOnSnapItemFinished != null)
			{
				LoopGridViewItem shownItemByRowColumn = GetShownItemByRowColumn(mCurSnapNearestItemRowColumn.mRow, mCurSnapNearestItemRowColumn.mColumn);
				if ((Object)(object)shownItemByRowColumn != (Object)null)
				{
					mOnSnapItemFinished(this, shownItemByRowColumn);
				}
			}
		}
		else
		{
			val += num * mCurSnapData.mSnapNeedMoveDir;
		}
		mContainerTrans.anchoredPosition3D = Vector2.op_Implicit(GetContainerVaildPos(val.x, val.y));
	}

	private GridItemGroup GetShownGroup(int groupIndex)
	{
		if (groupIndex < 0)
		{
			return null;
		}
		int count = mItemGroupList.Count;
		if (count == 0)
		{
			return null;
		}
		if (groupIndex < mItemGroupList[0].GroupIndex || groupIndex > mItemGroupList[count - 1].GroupIndex)
		{
			return null;
		}
		int index = groupIndex - mItemGroupList[0].GroupIndex;
		return mItemGroupList[index];
	}

	private void FillCurSnapData(int row, int column)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		Vector2 itemSnapPivotLocalPos = GetItemSnapPivotLocalPos(row, column);
		Vector2 containerVaildPos = GetContainerVaildPos(ContainerTrans.anchoredPosition3D.x, ContainerTrans.anchoredPosition3D.y);
		Vector2 val = GetViewPortSnapPivotLocalPos(containerVaildPos) - itemSnapPivotLocalPos;
		if (!mScrollRect.horizontal)
		{
			val.x = 0f;
		}
		if (!mScrollRect.vertical)
		{
			val.y = 0f;
		}
		mCurSnapData.mTargetSnapVal = ((Vector2)(ref val)).magnitude;
		mCurSnapData.mCurSnapVal = 0f;
		mCurSnapData.mSnapNeedMoveDir = ((Vector2)(ref val)).normalized;
	}

	private void UpdateCurSnapData()
	{
		if (mItemGroupList.Count == 0)
		{
			mCurSnapData.Clear();
			return;
		}
		if (mCurSnapData.mSnapStatus == SnapStatus.SnapMoveFinish)
		{
			if (mCurSnapData.mSnapTarget == mCurSnapNearestItemRowColumn)
			{
				return;
			}
			mCurSnapData.mSnapStatus = SnapStatus.NoTargetSet;
		}
		if (mCurSnapData.mSnapStatus == SnapStatus.SnapMoving)
		{
			if (mCurSnapData.mSnapTarget == mCurSnapNearestItemRowColumn || mCurSnapData.mIsForceSnapTo)
			{
				return;
			}
			mCurSnapData.mSnapStatus = SnapStatus.NoTargetSet;
		}
		if (mCurSnapData.mSnapStatus == SnapStatus.NoTargetSet)
		{
			if ((Object)(object)GetShownItemByRowColumn(mCurSnapNearestItemRowColumn.mRow, mCurSnapNearestItemRowColumn.mColumn) == (Object)null)
			{
				return;
			}
			mCurSnapData.mSnapTarget = mCurSnapNearestItemRowColumn;
			mCurSnapData.mSnapStatus = SnapStatus.TargetHasSet;
			mCurSnapData.mIsForceSnapTo = false;
		}
		if (mCurSnapData.mSnapStatus == SnapStatus.TargetHasSet)
		{
			LoopGridViewItem shownItemByRowColumn = GetShownItemByRowColumn(mCurSnapData.mSnapTarget.mRow, mCurSnapData.mSnapTarget.mColumn);
			if ((Object)(object)shownItemByRowColumn == (Object)null)
			{
				mCurSnapData.Clear();
				return;
			}
			FillCurSnapData(shownItemByRowColumn.Row, shownItemByRowColumn.Column);
			mCurSnapData.mSnapStatus = SnapStatus.SnapMoving;
		}
	}

	private bool CanSnap()
	{
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		if (mIsDraging)
		{
			return false;
		}
		if ((Object)(object)mScrollBarClickEventListener1 != (Object)null && mScrollBarClickEventListener1.IsPressd)
		{
			return false;
		}
		if ((Object)(object)mScrollBarClickEventListener2 != (Object)null && mScrollBarClickEventListener2.IsPressd)
		{
			return false;
		}
		if (!IsContainerTransCanMove())
		{
			return false;
		}
		if (Mathf.Abs(mScrollRect.velocity.x) > mSnapVecThreshold)
		{
			return false;
		}
		if (Mathf.Abs(mScrollRect.velocity.y) > mSnapVecThreshold)
		{
			return false;
		}
		Vector3 anchoredPosition3D = mContainerTrans.anchoredPosition3D;
		Vector2 containerVaildPos = GetContainerVaildPos(anchoredPosition3D.x, anchoredPosition3D.y);
		if (Mathf.Abs(anchoredPosition3D.x - containerVaildPos.x) > 3f)
		{
			return false;
		}
		if (Mathf.Abs(anchoredPosition3D.y - containerVaildPos.y) > 3f)
		{
			return false;
		}
		return true;
	}

	private GridItemGroup GetOneItemGroupObj()
	{
		int count = mItemGroupObjPool.Count;
		if (count == 0)
		{
			return new GridItemGroup();
		}
		GridItemGroup result = mItemGroupObjPool[count - 1];
		mItemGroupObjPool.RemoveAt(count - 1);
		return result;
	}

	private void RecycleOneItemGroupObj(GridItemGroup obj)
	{
		mItemGroupObjPool.Add(obj);
	}
}
