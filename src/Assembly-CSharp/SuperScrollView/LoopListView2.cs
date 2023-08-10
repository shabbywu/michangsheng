using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SuperScrollView;

public class LoopListView2 : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IEndDragHandler, IDragHandler
{
	private class SnapData
	{
		public SnapStatus mSnapStatus;

		public int mSnapTargetIndex;

		public float mTargetSnapVal;

		public float mCurSnapVal;

		public bool mIsForceSnapTo;

		public bool mIsTempTarget;

		public int mTempTargetIndex = -1;

		public float mMoveMaxAbsVec = -1f;

		public void Clear()
		{
			mSnapStatus = SnapStatus.NoTargetSet;
			mTempTargetIndex = -1;
			mIsForceSnapTo = false;
			mMoveMaxAbsVec = -1f;
		}
	}

	private Dictionary<string, ItemPool> mItemPoolDict = new Dictionary<string, ItemPool>();

	private List<ItemPool> mItemPoolList = new List<ItemPool>();

	[SerializeField]
	private List<ItemPrefabConfData> mItemPrefabDataList = new List<ItemPrefabConfData>();

	[SerializeField]
	private ListItemArrangeType mArrangeType;

	private List<LoopListViewItem2> mItemList = new List<LoopListViewItem2>();

	private RectTransform mContainerTrans;

	private ScrollRect mScrollRect;

	private RectTransform mScrollRectTransform;

	private RectTransform mViewPortRectTransform;

	private float mItemDefaultWithPaddingSize = 20f;

	private int mItemTotalCount;

	private bool mIsVertList;

	private Func<LoopListView2, int, LoopListViewItem2> mOnGetItemByIndex;

	private Vector3[] mItemWorldCorners = (Vector3[])(object)new Vector3[4];

	private Vector3[] mViewPortRectLocalCorners = (Vector3[])(object)new Vector3[4];

	private int mCurReadyMinItemIndex;

	private int mCurReadyMaxItemIndex;

	private bool mNeedCheckNextMinItem = true;

	private bool mNeedCheckNextMaxItem = true;

	private ItemPosMgr mItemPosMgr;

	private float mDistanceForRecycle0 = 300f;

	private float mDistanceForNew0 = 200f;

	private float mDistanceForRecycle1 = 300f;

	private float mDistanceForNew1 = 200f;

	[SerializeField]
	private bool mSupportScrollBar = true;

	private bool mIsDraging;

	private PointerEventData mPointerEventData;

	public Action mOnBeginDragAction;

	public Action mOnDragingAction;

	public Action mOnEndDragAction;

	private int mLastItemIndex;

	private float mLastItemPadding;

	private float mSmoothDumpVel;

	private float mSmoothDumpRate = 0.3f;

	private float mSnapFinishThreshold = 0.1f;

	private float mSnapVecThreshold = 145f;

	private float mSnapMoveDefaultMaxAbsVec = 3400f;

	[SerializeField]
	private bool mItemSnapEnable;

	private Vector3 mLastFrameContainerPos = Vector3.zero;

	public Action<LoopListView2, LoopListViewItem2> mOnSnapItemFinished;

	public Action<LoopListView2, LoopListViewItem2> mOnSnapNearestChanged;

	private int mCurSnapNearestItemIndex = -1;

	private Vector2 mAdjustedVec;

	private bool mNeedAdjustVec;

	private int mLeftSnapUpdateExtraCount = 1;

	[SerializeField]
	private Vector2 mViewPortSnapPivot = Vector2.zero;

	[SerializeField]
	private Vector2 mItemSnapPivot = Vector2.zero;

	private ClickEventListener mScrollBarClickEventListener;

	private SnapData mCurSnapData = new SnapData();

	private Vector3 mLastSnapCheckPos = Vector3.zero;

	private bool mListViewInited;

	private int mListUpdateCheckFrameCount;

	public Action<LoopListView2> OnListViewStart;

	public ListItemArrangeType ArrangeType
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

	public List<ItemPrefabConfData> ItemPrefabDataList => mItemPrefabDataList;

	public List<LoopListViewItem2> ItemList => mItemList;

	public bool IsVertList => mIsVertList;

	public int ItemTotalCount => mItemTotalCount;

	public RectTransform ContainerTrans => mContainerTrans;

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

	public bool SupportScrollBar
	{
		get
		{
			return mSupportScrollBar;
		}
		set
		{
			mSupportScrollBar = value;
		}
	}

	public float SnapMoveDefaultMaxAbsVec
	{
		get
		{
			return mSnapMoveDefaultMaxAbsVec;
		}
		set
		{
			mSnapMoveDefaultMaxAbsVec = value;
		}
	}

	public int ShownItemCount => mItemList.Count;

	public float ViewPortSize
	{
		get
		{
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_000e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			Rect rect;
			if (mIsVertList)
			{
				rect = mViewPortRectTransform.rect;
				return ((Rect)(ref rect)).height;
			}
			rect = mViewPortRectTransform.rect;
			return ((Rect)(ref rect)).width;
		}
	}

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

	public int CurSnapNearestItemIndex => mCurSnapNearestItemIndex;

	public ItemPrefabConfData GetItemPrefabConfData(string prefabName)
	{
		foreach (ItemPrefabConfData mItemPrefabData in mItemPrefabDataList)
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

	public void OnItemPrefabChanged(string prefabName)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		ItemPrefabConfData itemPrefabConfData = GetItemPrefabConfData(prefabName);
		if (itemPrefabConfData == null)
		{
			return;
		}
		ItemPool value = null;
		if (mItemPoolDict.TryGetValue(prefabName, out value))
		{
			int num = -1;
			Vector3 pos = Vector3.zero;
			if (mItemList.Count > 0)
			{
				num = mItemList[0].ItemIndex;
				pos = mItemList[0].CachedRectTransform.anchoredPosition3D;
			}
			RecycleAllItem();
			ClearAllTmpRecycledItem();
			value.DestroyAllItem();
			value.Init(itemPrefabConfData.mItemPrefab, itemPrefabConfData.mPadding, itemPrefabConfData.mStartPosOffset, itemPrefabConfData.mInitCreateCount, mContainerTrans);
			if (num >= 0)
			{
				RefreshAllShownItemWithFirstIndexAndPos(num, pos);
			}
		}
	}

	public void InitListView(int itemTotalCount, Func<LoopListView2, int, LoopListViewItem2> onGetItemByIndex, LoopListViewInitParam initParam = null)
	{
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Invalid comparison between Unknown and I4
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Invalid comparison between Unknown and I4
		if (initParam != null)
		{
			mDistanceForRecycle0 = initParam.mDistanceForRecycle0;
			mDistanceForNew0 = initParam.mDistanceForNew0;
			mDistanceForRecycle1 = initParam.mDistanceForRecycle1;
			mDistanceForNew1 = initParam.mDistanceForNew1;
			mSmoothDumpRate = initParam.mSmoothDumpRate;
			mSnapFinishThreshold = initParam.mSnapFinishThreshold;
			mSnapVecThreshold = initParam.mSnapVecThreshold;
			mItemDefaultWithPaddingSize = initParam.mItemDefaultWithPaddingSize;
		}
		mScrollRect = ((Component)this).gameObject.GetComponent<ScrollRect>();
		if ((Object)(object)mScrollRect == (Object)null)
		{
			Debug.LogError((object)"ListView Init Failed! ScrollRect component not found!");
			return;
		}
		if (mDistanceForRecycle0 <= mDistanceForNew0)
		{
			Debug.LogError((object)"mDistanceForRecycle0 should be bigger than mDistanceForNew0");
		}
		if (mDistanceForRecycle1 <= mDistanceForNew1)
		{
			Debug.LogError((object)"mDistanceForRecycle1 should be bigger than mDistanceForNew1");
		}
		mCurSnapData.Clear();
		mItemPosMgr = new ItemPosMgr(mItemDefaultWithPaddingSize);
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
		mIsVertList = mArrangeType == ListItemArrangeType.TopToBottom || mArrangeType == ListItemArrangeType.BottomToTop;
		mScrollRect.horizontal = !mIsVertList;
		mScrollRect.vertical = mIsVertList;
		SetScrollbarListener();
		AdjustPivot(mViewPortRectTransform);
		AdjustAnchor(mContainerTrans);
		AdjustContainerPivot(mContainerTrans);
		InitItemPool();
		mOnGetItemByIndex = onGetItemByIndex;
		if (mListViewInited)
		{
			Debug.LogError((object)"LoopListView2.InitListView method can be called only once.");
		}
		mListViewInited = true;
		ResetListView();
		mCurSnapData.Clear();
		mItemTotalCount = itemTotalCount;
		if (mItemTotalCount < 0)
		{
			mSupportScrollBar = false;
		}
		if (mSupportScrollBar)
		{
			mItemPosMgr.SetItemMaxCount(mItemTotalCount);
		}
		else
		{
			mItemPosMgr.SetItemMaxCount(0);
		}
		mCurReadyMaxItemIndex = 0;
		mCurReadyMinItemIndex = 0;
		mLeftSnapUpdateExtraCount = 1;
		mNeedCheckNextMaxItem = true;
		mNeedCheckNextMinItem = true;
		UpdateContentSize();
	}

	private void Start()
	{
		if (OnListViewStart != null)
		{
			OnListViewStart(this);
		}
	}

	private void SetScrollbarListener()
	{
		mScrollBarClickEventListener = null;
		Scrollbar val = null;
		if (mIsVertList && (Object)(object)mScrollRect.verticalScrollbar != (Object)null)
		{
			val = mScrollRect.verticalScrollbar;
		}
		if (!mIsVertList && (Object)(object)mScrollRect.horizontalScrollbar != (Object)null)
		{
			val = mScrollRect.horizontalScrollbar;
		}
		if (!((Object)(object)val == (Object)null))
		{
			ClickEventListener clickEventListener = (mScrollBarClickEventListener = ClickEventListener.Get(((Component)val).gameObject));
			clickEventListener.SetPointerUpHandler(OnPointerUpInScrollBar);
			clickEventListener.SetPointerDownHandler(OnPointerDownInScrollBar);
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

	public void ResetListView(bool resetPos = true)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		mViewPortRectTransform.GetLocalCorners(mViewPortRectLocalCorners);
		if (resetPos)
		{
			mContainerTrans.anchoredPosition3D = Vector3.zero;
		}
		ForceSnapUpdateCheck();
	}

	public void SetListItemCount(int itemCount, bool resetPos = true)
	{
		if (itemCount == mItemTotalCount)
		{
			return;
		}
		mCurSnapData.Clear();
		mItemTotalCount = itemCount;
		if (mItemTotalCount < 0)
		{
			mSupportScrollBar = false;
		}
		if (mSupportScrollBar)
		{
			mItemPosMgr.SetItemMaxCount(mItemTotalCount);
		}
		else
		{
			mItemPosMgr.SetItemMaxCount(0);
		}
		if (mItemTotalCount == 0)
		{
			mCurReadyMaxItemIndex = 0;
			mCurReadyMinItemIndex = 0;
			mNeedCheckNextMaxItem = false;
			mNeedCheckNextMinItem = false;
			RecycleAllItem();
			ClearAllTmpRecycledItem();
			UpdateContentSize();
			return;
		}
		if (mCurReadyMaxItemIndex >= mItemTotalCount)
		{
			mCurReadyMaxItemIndex = mItemTotalCount - 1;
		}
		mLeftSnapUpdateExtraCount = 1;
		mNeedCheckNextMaxItem = true;
		mNeedCheckNextMinItem = true;
		if (resetPos)
		{
			MovePanelToItemIndex(0, 0f);
			return;
		}
		if (mItemList.Count == 0)
		{
			MovePanelToItemIndex(0, 0f);
			return;
		}
		int num = mItemTotalCount - 1;
		if (mItemList[mItemList.Count - 1].ItemIndex <= num)
		{
			UpdateContentSize();
			UpdateAllShownItemsPos();
		}
		else
		{
			MovePanelToItemIndex(num, 0f);
		}
	}

	public LoopListViewItem2 GetShownItemByItemIndex(int itemIndex)
	{
		int count = mItemList.Count;
		if (count == 0)
		{
			return null;
		}
		if (itemIndex < mItemList[0].ItemIndex || itemIndex > mItemList[count - 1].ItemIndex)
		{
			return null;
		}
		int index = itemIndex - mItemList[0].ItemIndex;
		return mItemList[index];
	}

	public LoopListViewItem2 GetShownItemNearestItemIndex(int itemIndex)
	{
		int count = mItemList.Count;
		if (count == 0)
		{
			return null;
		}
		if (itemIndex < mItemList[0].ItemIndex)
		{
			return mItemList[0];
		}
		if (itemIndex > mItemList[count - 1].ItemIndex)
		{
			return mItemList[count - 1];
		}
		int index = itemIndex - mItemList[0].ItemIndex;
		return mItemList[index];
	}

	public LoopListViewItem2 GetShownItemByIndex(int index)
	{
		int count = mItemList.Count;
		if (index < 0 || index >= count)
		{
			return null;
		}
		return mItemList[index];
	}

	public LoopListViewItem2 GetShownItemByIndexWithoutCheck(int index)
	{
		return mItemList[index];
	}

	public int GetIndexInShownItemList(LoopListViewItem2 item)
	{
		if ((Object)(object)item == (Object)null)
		{
			return -1;
		}
		int count = mItemList.Count;
		if (count == 0)
		{
			return -1;
		}
		for (int i = 0; i < count; i++)
		{
			if ((Object)(object)mItemList[i] == (Object)(object)item)
			{
				return i;
			}
		}
		return -1;
	}

	public void DoActionForEachShownItem(Action<LoopListViewItem2, object> action, object param)
	{
		if (action == null)
		{
			return;
		}
		int count = mItemList.Count;
		if (count != 0)
		{
			for (int i = 0; i < count; i++)
			{
				action(mItemList[i], param);
			}
		}
	}

	public LoopListViewItem2 NewListViewItem(string itemPrefabName)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		ItemPool value = null;
		if (!mItemPoolDict.TryGetValue(itemPrefabName, out value))
		{
			return null;
		}
		LoopListViewItem2 item = value.GetItem();
		RectTransform component = ((Component)item).GetComponent<RectTransform>();
		((Transform)component).SetParent((Transform)(object)mContainerTrans);
		((Transform)component).localScale = Vector3.one;
		component.anchoredPosition3D = Vector3.zero;
		((Transform)component).localEulerAngles = Vector3.zero;
		item.ParentListView = this;
		return item;
	}

	public void OnItemSizeChanged(int itemIndex)
	{
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		LoopListViewItem2 shownItemByItemIndex = GetShownItemByItemIndex(itemIndex);
		if ((Object)(object)shownItemByItemIndex == (Object)null)
		{
			return;
		}
		if (mSupportScrollBar)
		{
			Rect rect;
			if (mIsVertList)
			{
				rect = shownItemByItemIndex.CachedRectTransform.rect;
				SetItemSize(itemIndex, ((Rect)(ref rect)).height, shownItemByItemIndex.Padding);
			}
			else
			{
				rect = shownItemByItemIndex.CachedRectTransform.rect;
				SetItemSize(itemIndex, ((Rect)(ref rect)).width, shownItemByItemIndex.Padding);
			}
		}
		UpdateContentSize();
		UpdateAllShownItemsPos();
	}

	public void RefreshItemByItemIndex(int itemIndex)
	{
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		int count = mItemList.Count;
		if (count == 0 || itemIndex < mItemList[0].ItemIndex || itemIndex > mItemList[count - 1].ItemIndex)
		{
			return;
		}
		int itemIndex2 = mItemList[0].ItemIndex;
		int index = itemIndex - itemIndex2;
		LoopListViewItem2 loopListViewItem = mItemList[index];
		Vector3 anchoredPosition3D = loopListViewItem.CachedRectTransform.anchoredPosition3D;
		RecycleItemTmp(loopListViewItem);
		LoopListViewItem2 newItemByIndex = GetNewItemByIndex(itemIndex);
		if ((Object)(object)newItemByIndex == (Object)null)
		{
			RefreshAllShownItemWithFirstIndex(itemIndex2);
			return;
		}
		mItemList[index] = newItemByIndex;
		if (mIsVertList)
		{
			anchoredPosition3D.x = newItemByIndex.StartPosOffset;
		}
		else
		{
			anchoredPosition3D.y = newItemByIndex.StartPosOffset;
		}
		newItemByIndex.CachedRectTransform.anchoredPosition3D = anchoredPosition3D;
		OnItemSizeChanged(itemIndex);
		ClearAllTmpRecycledItem();
	}

	public void FinishSnapImmediately()
	{
		UpdateSnapMove(immediate: true);
	}

	public void MovePanelToItemIndex(int itemIndex, float offset)
	{
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		mScrollRect.StopMovement();
		mCurSnapData.Clear();
		if (mItemTotalCount == 0 || (itemIndex < 0 && mItemTotalCount > 0))
		{
			return;
		}
		if (mItemTotalCount > 0 && itemIndex >= mItemTotalCount)
		{
			itemIndex = mItemTotalCount - 1;
		}
		if (offset < 0f)
		{
			offset = 0f;
		}
		Vector3 zero = Vector3.zero;
		float viewPortSize = ViewPortSize;
		if (offset > viewPortSize)
		{
			offset = viewPortSize;
		}
		if (mArrangeType == ListItemArrangeType.TopToBottom)
		{
			float num = mContainerTrans.anchoredPosition3D.y;
			if (num < 0f)
			{
				num = 0f;
			}
			zero.y = 0f - num - offset;
		}
		else if (mArrangeType == ListItemArrangeType.BottomToTop)
		{
			float num2 = mContainerTrans.anchoredPosition3D.y;
			if (num2 > 0f)
			{
				num2 = 0f;
			}
			zero.y = 0f - num2 + offset;
		}
		else if (mArrangeType == ListItemArrangeType.LeftToRight)
		{
			float num3 = mContainerTrans.anchoredPosition3D.x;
			if (num3 > 0f)
			{
				num3 = 0f;
			}
			zero.x = 0f - num3 + offset;
		}
		else if (mArrangeType == ListItemArrangeType.RightToLeft)
		{
			float num4 = mContainerTrans.anchoredPosition3D.x;
			if (num4 < 0f)
			{
				num4 = 0f;
			}
			zero.x = 0f - num4 - offset;
		}
		RecycleAllItem();
		LoopListViewItem2 newItemByIndex = GetNewItemByIndex(itemIndex);
		if ((Object)(object)newItemByIndex == (Object)null)
		{
			ClearAllTmpRecycledItem();
			return;
		}
		if (mIsVertList)
		{
			zero.x = newItemByIndex.StartPosOffset;
		}
		else
		{
			zero.y = newItemByIndex.StartPosOffset;
		}
		newItemByIndex.CachedRectTransform.anchoredPosition3D = zero;
		if (mSupportScrollBar)
		{
			Rect rect;
			if (mIsVertList)
			{
				int itemIndex2 = itemIndex;
				rect = newItemByIndex.CachedRectTransform.rect;
				SetItemSize(itemIndex2, ((Rect)(ref rect)).height, newItemByIndex.Padding);
			}
			else
			{
				int itemIndex3 = itemIndex;
				rect = newItemByIndex.CachedRectTransform.rect;
				SetItemSize(itemIndex3, ((Rect)(ref rect)).width, newItemByIndex.Padding);
			}
		}
		mItemList.Add(newItemByIndex);
		UpdateContentSize();
		UpdateListView(viewPortSize + 100f, viewPortSize + 100f, viewPortSize, viewPortSize);
		AdjustPanelPos();
		ClearAllTmpRecycledItem();
		ForceSnapUpdateCheck();
		UpdateSnapMove(immediate: false, forceSendEvent: true);
	}

	public void RefreshAllShownItem()
	{
		if (mItemList.Count != 0)
		{
			RefreshAllShownItemWithFirstIndex(mItemList[0].ItemIndex);
		}
	}

	public void RefreshAllShownItemWithFirstIndex(int firstItemIndex)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		int count = mItemList.Count;
		if (count == 0)
		{
			return;
		}
		Vector3 anchoredPosition3D = mItemList[0].CachedRectTransform.anchoredPosition3D;
		RecycleAllItem();
		for (int i = 0; i < count; i++)
		{
			int num = firstItemIndex + i;
			LoopListViewItem2 newItemByIndex = GetNewItemByIndex(num);
			if ((Object)(object)newItemByIndex == (Object)null)
			{
				break;
			}
			if (mIsVertList)
			{
				anchoredPosition3D.x = newItemByIndex.StartPosOffset;
			}
			else
			{
				anchoredPosition3D.y = newItemByIndex.StartPosOffset;
			}
			newItemByIndex.CachedRectTransform.anchoredPosition3D = anchoredPosition3D;
			if (mSupportScrollBar)
			{
				Rect rect;
				if (mIsVertList)
				{
					rect = newItemByIndex.CachedRectTransform.rect;
					SetItemSize(num, ((Rect)(ref rect)).height, newItemByIndex.Padding);
				}
				else
				{
					rect = newItemByIndex.CachedRectTransform.rect;
					SetItemSize(num, ((Rect)(ref rect)).width, newItemByIndex.Padding);
				}
			}
			mItemList.Add(newItemByIndex);
		}
		UpdateContentSize();
		UpdateAllShownItemsPos();
		ClearAllTmpRecycledItem();
	}

	public void RefreshAllShownItemWithFirstIndexAndPos(int firstItemIndex, Vector3 pos)
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		RecycleAllItem();
		LoopListViewItem2 newItemByIndex = GetNewItemByIndex(firstItemIndex);
		if ((Object)(object)newItemByIndex == (Object)null)
		{
			return;
		}
		if (mIsVertList)
		{
			pos.x = newItemByIndex.StartPosOffset;
		}
		else
		{
			pos.y = newItemByIndex.StartPosOffset;
		}
		newItemByIndex.CachedRectTransform.anchoredPosition3D = pos;
		if (mSupportScrollBar)
		{
			Rect rect;
			if (mIsVertList)
			{
				rect = newItemByIndex.CachedRectTransform.rect;
				SetItemSize(firstItemIndex, ((Rect)(ref rect)).height, newItemByIndex.Padding);
			}
			else
			{
				rect = newItemByIndex.CachedRectTransform.rect;
				SetItemSize(firstItemIndex, ((Rect)(ref rect)).width, newItemByIndex.Padding);
			}
		}
		mItemList.Add(newItemByIndex);
		UpdateContentSize();
		UpdateAllShownItemsPos();
		UpdateListView(mDistanceForRecycle0, mDistanceForRecycle1, mDistanceForNew0, mDistanceForNew1);
		ClearAllTmpRecycledItem();
	}

	private void RecycleItemTmp(LoopListViewItem2 item)
	{
		if (!((Object)(object)item == (Object)null) && !string.IsNullOrEmpty(item.ItemPrefabName))
		{
			ItemPool value = null;
			if (mItemPoolDict.TryGetValue(item.ItemPrefabName, out value))
			{
				value.RecycleItem(item);
			}
		}
	}

	private void ClearAllTmpRecycledItem()
	{
		int count = mItemPoolList.Count;
		for (int i = 0; i < count; i++)
		{
			mItemPoolList[i].ClearTmpRecycledItem();
		}
	}

	private void RecycleAllItem()
	{
		foreach (LoopListViewItem2 mItem in mItemList)
		{
			RecycleItemTmp(mItem);
		}
		mItemList.Clear();
	}

	private void AdjustContainerPivot(RectTransform rtf)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		Vector2 pivot = rtf.pivot;
		if (mArrangeType == ListItemArrangeType.BottomToTop)
		{
			pivot.y = 0f;
		}
		else if (mArrangeType == ListItemArrangeType.TopToBottom)
		{
			pivot.y = 1f;
		}
		else if (mArrangeType == ListItemArrangeType.LeftToRight)
		{
			pivot.x = 0f;
		}
		else if (mArrangeType == ListItemArrangeType.RightToLeft)
		{
			pivot.x = 1f;
		}
		rtf.pivot = pivot;
	}

	private void AdjustPivot(RectTransform rtf)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		Vector2 pivot = rtf.pivot;
		if (mArrangeType == ListItemArrangeType.BottomToTop)
		{
			pivot.y = 0f;
		}
		else if (mArrangeType == ListItemArrangeType.TopToBottom)
		{
			pivot.y = 1f;
		}
		else if (mArrangeType == ListItemArrangeType.LeftToRight)
		{
			pivot.x = 0f;
		}
		else if (mArrangeType == ListItemArrangeType.RightToLeft)
		{
			pivot.x = 1f;
		}
		rtf.pivot = pivot;
	}

	private void AdjustContainerAnchor(RectTransform rtf)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		Vector2 anchorMin = rtf.anchorMin;
		Vector2 anchorMax = rtf.anchorMax;
		if (mArrangeType == ListItemArrangeType.BottomToTop)
		{
			anchorMin.y = 0f;
			anchorMax.y = 0f;
		}
		else if (mArrangeType == ListItemArrangeType.TopToBottom)
		{
			anchorMin.y = 1f;
			anchorMax.y = 1f;
		}
		else if (mArrangeType == ListItemArrangeType.LeftToRight)
		{
			anchorMin.x = 0f;
			anchorMax.x = 0f;
		}
		else if (mArrangeType == ListItemArrangeType.RightToLeft)
		{
			anchorMin.x = 1f;
			anchorMax.x = 1f;
		}
		rtf.anchorMin = anchorMin;
		rtf.anchorMax = anchorMax;
	}

	private void AdjustAnchor(RectTransform rtf)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		Vector2 anchorMin = rtf.anchorMin;
		Vector2 anchorMax = rtf.anchorMax;
		if (mArrangeType == ListItemArrangeType.BottomToTop)
		{
			anchorMin.y = 0f;
			anchorMax.y = 0f;
		}
		else if (mArrangeType == ListItemArrangeType.TopToBottom)
		{
			anchorMin.y = 1f;
			anchorMax.y = 1f;
		}
		else if (mArrangeType == ListItemArrangeType.LeftToRight)
		{
			anchorMin.x = 0f;
			anchorMax.x = 0f;
		}
		else if (mArrangeType == ListItemArrangeType.RightToLeft)
		{
			anchorMin.x = 1f;
			anchorMax.x = 1f;
		}
		rtf.anchorMin = anchorMin;
		rtf.anchorMax = anchorMax;
	}

	private void InitItemPool()
	{
		foreach (ItemPrefabConfData mItemPrefabData in mItemPrefabDataList)
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
			AdjustAnchor(component);
			AdjustPivot(component);
			if ((Object)(object)mItemPrefabData.mItemPrefab.GetComponent<LoopListViewItem2>() == (Object)null)
			{
				mItemPrefabData.mItemPrefab.AddComponent<LoopListViewItem2>();
			}
			ItemPool itemPool = new ItemPool();
			itemPool.Init(mItemPrefabData.mItemPrefab, mItemPrefabData.mPadding, mItemPrefabData.mStartPosOffset, mItemPrefabData.mInitCreateCount, mContainerTrans);
			mItemPoolDict.Add(name, itemPool);
			mItemPoolList.Add(itemPool);
		}
	}

	public virtual void OnBeginDrag(PointerEventData eventData)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		if ((int)eventData.button == 0)
		{
			mIsDraging = true;
			CacheDragPointerEventData(eventData);
			mCurSnapData.Clear();
			if (mOnBeginDragAction != null)
			{
				mOnBeginDragAction();
			}
		}
	}

	public virtual void OnEndDrag(PointerEventData eventData)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		if ((int)eventData.button == 0)
		{
			mIsDraging = false;
			mPointerEventData = null;
			if (mOnEndDragAction != null)
			{
				mOnEndDragAction();
			}
			ForceSnapUpdateCheck();
		}
	}

	public virtual void OnDrag(PointerEventData eventData)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		if ((int)eventData.button == 0)
		{
			CacheDragPointerEventData(eventData);
			if (mOnDragingAction != null)
			{
				mOnDragingAction();
			}
		}
	}

	private void CacheDragPointerEventData(PointerEventData eventData)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		if (mPointerEventData == null)
		{
			mPointerEventData = new PointerEventData(EventSystem.current);
		}
		mPointerEventData.button = eventData.button;
		mPointerEventData.position = eventData.position;
		mPointerEventData.pointerPressRaycast = eventData.pointerPressRaycast;
		mPointerEventData.pointerCurrentRaycast = eventData.pointerCurrentRaycast;
	}

	private LoopListViewItem2 GetNewItemByIndex(int index)
	{
		if (mSupportScrollBar && index < 0)
		{
			return null;
		}
		if (mItemTotalCount > 0 && index >= mItemTotalCount)
		{
			return null;
		}
		LoopListViewItem2 loopListViewItem = mOnGetItemByIndex(this, index);
		if ((Object)(object)loopListViewItem == (Object)null)
		{
			return null;
		}
		loopListViewItem.ItemIndex = index;
		loopListViewItem.ItemCreatedCheckFrameCount = mListUpdateCheckFrameCount;
		return loopListViewItem;
	}

	private void SetItemSize(int itemIndex, float itemSize, float padding)
	{
		mItemPosMgr.SetItemSize(itemIndex, itemSize + padding);
		if (itemIndex >= mLastItemIndex)
		{
			mLastItemIndex = itemIndex;
			mLastItemPadding = padding;
		}
	}

	private bool GetPlusItemIndexAndPosAtGivenPos(float pos, ref int index, ref float itemPos)
	{
		return mItemPosMgr.GetItemIndexAndPosAtGivenPos(pos, ref index, ref itemPos);
	}

	private float GetItemPos(int itemIndex)
	{
		return mItemPosMgr.GetItemPos(itemIndex);
	}

	public Vector3 GetItemCornerPosInViewPort(LoopListViewItem2 item, ItemCornerEnum corner = ItemCornerEnum.LeftBottom)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		item.CachedRectTransform.GetWorldCorners(mItemWorldCorners);
		return ((Transform)mViewPortRectTransform).InverseTransformPoint(mItemWorldCorners[(int)corner]);
	}

	private void AdjustPanelPos()
	{
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0456: Unknown result type (might be due to invalid IL or missing references)
		//IL_045b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_041c: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0344: Unknown result type (might be due to invalid IL or missing references)
		//IL_0349: Unknown result type (might be due to invalid IL or missing references)
		//IL_035c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_0627: Unknown result type (might be due to invalid IL or missing references)
		//IL_062c: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0510: Unknown result type (might be due to invalid IL or missing references)
		//IL_0515: Unknown result type (might be due to invalid IL or missing references)
		//IL_051a: Unknown result type (might be due to invalid IL or missing references)
		//IL_052d: Unknown result type (might be due to invalid IL or missing references)
		//IL_047e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0483: Unknown result type (might be due to invalid IL or missing references)
		//IL_0497: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0383: Unknown result type (might be due to invalid IL or missing references)
		//IL_0388: Unknown result type (might be due to invalid IL or missing references)
		//IL_038c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_064f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0654: Unknown result type (might be due to invalid IL or missing references)
		//IL_0668: Unknown result type (might be due to invalid IL or missing references)
		//IL_069b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0554: Unknown result type (might be due to invalid IL or missing references)
		//IL_0559: Unknown result type (might be due to invalid IL or missing references)
		//IL_055d: Unknown result type (might be due to invalid IL or missing references)
		//IL_057d: Unknown result type (might be due to invalid IL or missing references)
		//IL_071e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0723: Unknown result type (might be due to invalid IL or missing references)
		//IL_0727: Unknown result type (might be due to invalid IL or missing references)
		//IL_0747: Unknown result type (might be due to invalid IL or missing references)
		if (mItemList.Count == 0)
		{
			return;
		}
		UpdateAllShownItemsPos();
		float viewPortSize = ViewPortSize;
		float contentPanelSize = GetContentPanelSize();
		if (mArrangeType == ListItemArrangeType.TopToBottom)
		{
			if (contentPanelSize <= viewPortSize)
			{
				Vector3 anchoredPosition3D = mContainerTrans.anchoredPosition3D;
				anchoredPosition3D.y = 0f;
				mContainerTrans.anchoredPosition3D = anchoredPosition3D;
				mItemList[0].CachedRectTransform.anchoredPosition3D = new Vector3(mItemList[0].StartPosOffset, 0f, 0f);
				UpdateAllShownItemsPos();
				return;
			}
			mItemList[0].CachedRectTransform.GetWorldCorners(mItemWorldCorners);
			if (((Transform)mViewPortRectTransform).InverseTransformPoint(mItemWorldCorners[1]).y < mViewPortRectLocalCorners[1].y)
			{
				Vector3 anchoredPosition3D2 = mContainerTrans.anchoredPosition3D;
				anchoredPosition3D2.y = 0f;
				mContainerTrans.anchoredPosition3D = anchoredPosition3D2;
				mItemList[0].CachedRectTransform.anchoredPosition3D = new Vector3(mItemList[0].StartPosOffset, 0f, 0f);
				UpdateAllShownItemsPos();
				return;
			}
			mItemList[mItemList.Count - 1].CachedRectTransform.GetWorldCorners(mItemWorldCorners);
			float num = ((Transform)mViewPortRectTransform).InverseTransformPoint(mItemWorldCorners[0]).y - mViewPortRectLocalCorners[0].y;
			if (num > 0f)
			{
				Vector3 anchoredPosition3D3 = mItemList[0].CachedRectTransform.anchoredPosition3D;
				anchoredPosition3D3.y -= num;
				mItemList[0].CachedRectTransform.anchoredPosition3D = anchoredPosition3D3;
				UpdateAllShownItemsPos();
			}
		}
		else if (mArrangeType == ListItemArrangeType.BottomToTop)
		{
			if (contentPanelSize <= viewPortSize)
			{
				Vector3 anchoredPosition3D4 = mContainerTrans.anchoredPosition3D;
				anchoredPosition3D4.y = 0f;
				mContainerTrans.anchoredPosition3D = anchoredPosition3D4;
				mItemList[0].CachedRectTransform.anchoredPosition3D = new Vector3(mItemList[0].StartPosOffset, 0f, 0f);
				UpdateAllShownItemsPos();
				return;
			}
			mItemList[0].CachedRectTransform.GetWorldCorners(mItemWorldCorners);
			if (((Transform)mViewPortRectTransform).InverseTransformPoint(mItemWorldCorners[0]).y > mViewPortRectLocalCorners[0].y)
			{
				Vector3 anchoredPosition3D5 = mContainerTrans.anchoredPosition3D;
				anchoredPosition3D5.y = 0f;
				mContainerTrans.anchoredPosition3D = anchoredPosition3D5;
				mItemList[0].CachedRectTransform.anchoredPosition3D = new Vector3(mItemList[0].StartPosOffset, 0f, 0f);
				UpdateAllShownItemsPos();
				return;
			}
			mItemList[mItemList.Count - 1].CachedRectTransform.GetWorldCorners(mItemWorldCorners);
			Vector3 val = ((Transform)mViewPortRectTransform).InverseTransformPoint(mItemWorldCorners[1]);
			float num2 = mViewPortRectLocalCorners[1].y - val.y;
			if (num2 > 0f)
			{
				Vector3 anchoredPosition3D6 = mItemList[0].CachedRectTransform.anchoredPosition3D;
				anchoredPosition3D6.y += num2;
				mItemList[0].CachedRectTransform.anchoredPosition3D = anchoredPosition3D6;
				UpdateAllShownItemsPos();
			}
		}
		else if (mArrangeType == ListItemArrangeType.LeftToRight)
		{
			if (contentPanelSize <= viewPortSize)
			{
				Vector3 anchoredPosition3D7 = mContainerTrans.anchoredPosition3D;
				anchoredPosition3D7.x = 0f;
				mContainerTrans.anchoredPosition3D = anchoredPosition3D7;
				mItemList[0].CachedRectTransform.anchoredPosition3D = new Vector3(0f, mItemList[0].StartPosOffset, 0f);
				UpdateAllShownItemsPos();
				return;
			}
			mItemList[0].CachedRectTransform.GetWorldCorners(mItemWorldCorners);
			if (((Transform)mViewPortRectTransform).InverseTransformPoint(mItemWorldCorners[1]).x > mViewPortRectLocalCorners[1].x)
			{
				Vector3 anchoredPosition3D8 = mContainerTrans.anchoredPosition3D;
				anchoredPosition3D8.x = 0f;
				mContainerTrans.anchoredPosition3D = anchoredPosition3D8;
				mItemList[0].CachedRectTransform.anchoredPosition3D = new Vector3(0f, mItemList[0].StartPosOffset, 0f);
				UpdateAllShownItemsPos();
				return;
			}
			mItemList[mItemList.Count - 1].CachedRectTransform.GetWorldCorners(mItemWorldCorners);
			Vector3 val2 = ((Transform)mViewPortRectTransform).InverseTransformPoint(mItemWorldCorners[2]);
			float num3 = mViewPortRectLocalCorners[2].x - val2.x;
			if (num3 > 0f)
			{
				Vector3 anchoredPosition3D9 = mItemList[0].CachedRectTransform.anchoredPosition3D;
				anchoredPosition3D9.x += num3;
				mItemList[0].CachedRectTransform.anchoredPosition3D = anchoredPosition3D9;
				UpdateAllShownItemsPos();
			}
		}
		else
		{
			if (mArrangeType != ListItemArrangeType.RightToLeft)
			{
				return;
			}
			if (contentPanelSize <= viewPortSize)
			{
				Vector3 anchoredPosition3D10 = mContainerTrans.anchoredPosition3D;
				anchoredPosition3D10.x = 0f;
				mContainerTrans.anchoredPosition3D = anchoredPosition3D10;
				mItemList[0].CachedRectTransform.anchoredPosition3D = new Vector3(0f, mItemList[0].StartPosOffset, 0f);
				UpdateAllShownItemsPos();
				return;
			}
			mItemList[0].CachedRectTransform.GetWorldCorners(mItemWorldCorners);
			if (((Transform)mViewPortRectTransform).InverseTransformPoint(mItemWorldCorners[2]).x < mViewPortRectLocalCorners[2].x)
			{
				Vector3 anchoredPosition3D11 = mContainerTrans.anchoredPosition3D;
				anchoredPosition3D11.x = 0f;
				mContainerTrans.anchoredPosition3D = anchoredPosition3D11;
				mItemList[0].CachedRectTransform.anchoredPosition3D = new Vector3(0f, mItemList[0].StartPosOffset, 0f);
				UpdateAllShownItemsPos();
				return;
			}
			mItemList[mItemList.Count - 1].CachedRectTransform.GetWorldCorners(mItemWorldCorners);
			float num4 = ((Transform)mViewPortRectTransform).InverseTransformPoint(mItemWorldCorners[1]).x - mViewPortRectLocalCorners[1].x;
			if (num4 > 0f)
			{
				Vector3 anchoredPosition3D12 = mItemList[0].CachedRectTransform.anchoredPosition3D;
				anchoredPosition3D12.x -= num4;
				mItemList[0].CachedRectTransform.anchoredPosition3D = anchoredPosition3D12;
				UpdateAllShownItemsPos();
			}
		}
	}

	private void Update()
	{
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		if (!mListViewInited)
		{
			return;
		}
		if (mNeedAdjustVec)
		{
			mNeedAdjustVec = false;
			if (mIsVertList)
			{
				if (mScrollRect.velocity.y * mAdjustedVec.y > 0f)
				{
					mScrollRect.velocity = mAdjustedVec;
				}
			}
			else if (mScrollRect.velocity.x * mAdjustedVec.x > 0f)
			{
				mScrollRect.velocity = mAdjustedVec;
			}
		}
		if (mSupportScrollBar)
		{
			mItemPosMgr.Update(updateAll: false);
		}
		UpdateSnapMove();
		UpdateListView(mDistanceForRecycle0, mDistanceForRecycle1, mDistanceForNew0, mDistanceForNew1);
		ClearAllTmpRecycledItem();
		mLastFrameContainerPos = mContainerTrans.anchoredPosition3D;
	}

	private void UpdateSnapMove(bool immediate = false, bool forceSendEvent = false)
	{
		if (mItemSnapEnable)
		{
			if (mIsVertList)
			{
				UpdateSnapVertical(immediate, forceSendEvent);
			}
			else
			{
				UpdateSnapHorizontal(immediate, forceSendEvent);
			}
		}
	}

	public void UpdateAllShownItemSnapData()
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0344: Unknown result type (might be due to invalid IL or missing references)
		if (!mItemSnapEnable)
		{
			return;
		}
		int count = mItemList.Count;
		if (count == 0)
		{
			return;
		}
		_ = mContainerTrans.anchoredPosition3D;
		LoopListViewItem2 loopListViewItem = mItemList[0];
		loopListViewItem.CachedRectTransform.GetWorldCorners(mItemWorldCorners);
		float num = 0f;
		float num2 = 0f;
		float num3 = 0f;
		float num4 = 0f;
		Rect rect;
		if (mArrangeType == ListItemArrangeType.TopToBottom)
		{
			float num5 = 0f - (1f - mViewPortSnapPivot.y);
			rect = mViewPortRectTransform.rect;
			num4 = num5 * ((Rect)(ref rect)).height;
			num = ((Transform)mViewPortRectTransform).InverseTransformPoint(mItemWorldCorners[1]).y;
			num2 = num - loopListViewItem.ItemSizeWithPadding;
			num3 = num - loopListViewItem.ItemSize * (1f - mItemSnapPivot.y);
			for (int i = 0; i < count; i++)
			{
				mItemList[i].DistanceWithViewPortSnapCenter = num4 - num3;
				if (i + 1 < count)
				{
					num = num2;
					num2 -= mItemList[i + 1].ItemSizeWithPadding;
					num3 = num - mItemList[i + 1].ItemSize * (1f - mItemSnapPivot.y);
				}
			}
		}
		else if (mArrangeType == ListItemArrangeType.BottomToTop)
		{
			float y = mViewPortSnapPivot.y;
			rect = mViewPortRectTransform.rect;
			num4 = y * ((Rect)(ref rect)).height;
			num = ((Transform)mViewPortRectTransform).InverseTransformPoint(mItemWorldCorners[0]).y;
			num2 = num + loopListViewItem.ItemSizeWithPadding;
			num3 = num + loopListViewItem.ItemSize * mItemSnapPivot.y;
			for (int j = 0; j < count; j++)
			{
				mItemList[j].DistanceWithViewPortSnapCenter = num4 - num3;
				if (j + 1 < count)
				{
					num = num2;
					num2 += mItemList[j + 1].ItemSizeWithPadding;
					num3 = num + mItemList[j + 1].ItemSize * mItemSnapPivot.y;
				}
			}
		}
		else if (mArrangeType == ListItemArrangeType.RightToLeft)
		{
			float num6 = 0f - (1f - mViewPortSnapPivot.x);
			rect = mViewPortRectTransform.rect;
			num4 = num6 * ((Rect)(ref rect)).width;
			num = ((Transform)mViewPortRectTransform).InverseTransformPoint(mItemWorldCorners[2]).x;
			num2 = num - loopListViewItem.ItemSizeWithPadding;
			num3 = num - loopListViewItem.ItemSize * (1f - mItemSnapPivot.x);
			for (int k = 0; k < count; k++)
			{
				mItemList[k].DistanceWithViewPortSnapCenter = num4 - num3;
				if (k + 1 < count)
				{
					num = num2;
					num2 -= mItemList[k + 1].ItemSizeWithPadding;
					num3 = num - mItemList[k + 1].ItemSize * (1f - mItemSnapPivot.x);
				}
			}
		}
		else
		{
			if (mArrangeType != ListItemArrangeType.LeftToRight)
			{
				return;
			}
			float x = mViewPortSnapPivot.x;
			rect = mViewPortRectTransform.rect;
			num4 = x * ((Rect)(ref rect)).width;
			num = ((Transform)mViewPortRectTransform).InverseTransformPoint(mItemWorldCorners[1]).x;
			num2 = num + loopListViewItem.ItemSizeWithPadding;
			num3 = num + loopListViewItem.ItemSize * mItemSnapPivot.x;
			for (int l = 0; l < count; l++)
			{
				mItemList[l].DistanceWithViewPortSnapCenter = num4 - num3;
				if (l + 1 < count)
				{
					num = num2;
					num2 += mItemList[l + 1].ItemSizeWithPadding;
					num3 = num + mItemList[l + 1].ItemSize * mItemSnapPivot.x;
				}
			}
		}
	}

	private void UpdateSnapVertical(bool immediate = false, bool forceSendEvent = false)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_030f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0457: Unknown result type (might be due to invalid IL or missing references)
		//IL_04af: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_050c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0533: Unknown result type (might be due to invalid IL or missing references)
		//IL_0538: Unknown result type (might be due to invalid IL or missing references)
		//IL_0546: Unknown result type (might be due to invalid IL or missing references)
		//IL_0563: Unknown result type (might be due to invalid IL or missing references)
		if (!mItemSnapEnable)
		{
			return;
		}
		int count = mItemList.Count;
		if (count == 0)
		{
			return;
		}
		Vector3 anchoredPosition3D = mContainerTrans.anchoredPosition3D;
		bool flag = anchoredPosition3D.y != mLastSnapCheckPos.y;
		mLastSnapCheckPos = anchoredPosition3D;
		if (!flag && mLeftSnapUpdateExtraCount > 0)
		{
			mLeftSnapUpdateExtraCount--;
			flag = true;
		}
		Rect rect;
		if (flag)
		{
			LoopListViewItem2 loopListViewItem = mItemList[0];
			loopListViewItem.CachedRectTransform.GetWorldCorners(mItemWorldCorners);
			int num = -1;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = 0f;
			float num5 = float.MaxValue;
			float num6 = 0f;
			float num7 = 0f;
			if (mArrangeType == ListItemArrangeType.TopToBottom)
			{
				float num8 = 0f - (1f - mViewPortSnapPivot.y);
				rect = mViewPortRectTransform.rect;
				num7 = num8 * ((Rect)(ref rect)).height;
				num2 = ((Transform)mViewPortRectTransform).InverseTransformPoint(mItemWorldCorners[1]).y;
				num3 = num2 - loopListViewItem.ItemSizeWithPadding;
				num4 = num2 - loopListViewItem.ItemSize * (1f - mItemSnapPivot.y);
				for (int i = 0; i < count; i++)
				{
					num6 = Mathf.Abs(num7 - num4);
					if (!(num6 < num5))
					{
						break;
					}
					num5 = num6;
					num = i;
					if (i + 1 < count)
					{
						num2 = num3;
						num3 -= mItemList[i + 1].ItemSizeWithPadding;
						num4 = num2 - mItemList[i + 1].ItemSize * (1f - mItemSnapPivot.y);
					}
				}
			}
			else if (mArrangeType == ListItemArrangeType.BottomToTop)
			{
				float y = mViewPortSnapPivot.y;
				rect = mViewPortRectTransform.rect;
				num7 = y * ((Rect)(ref rect)).height;
				num2 = ((Transform)mViewPortRectTransform).InverseTransformPoint(mItemWorldCorners[0]).y;
				num3 = num2 + loopListViewItem.ItemSizeWithPadding;
				num4 = num2 + loopListViewItem.ItemSize * mItemSnapPivot.y;
				for (int j = 0; j < count; j++)
				{
					num6 = Mathf.Abs(num7 - num4);
					if (!(num6 < num5))
					{
						break;
					}
					num5 = num6;
					num = j;
					if (j + 1 < count)
					{
						num2 = num3;
						num3 += mItemList[j + 1].ItemSizeWithPadding;
						num4 = num2 + mItemList[j + 1].ItemSize * mItemSnapPivot.y;
					}
				}
			}
			if (num >= 0)
			{
				int num9 = mCurSnapNearestItemIndex;
				mCurSnapNearestItemIndex = mItemList[num].ItemIndex;
				if ((forceSendEvent || mItemList[num].ItemIndex != num9) && mOnSnapNearestChanged != null)
				{
					mOnSnapNearestChanged(this, mItemList[num]);
				}
			}
			else
			{
				mCurSnapNearestItemIndex = -1;
			}
		}
		if (!CanSnap())
		{
			ClearSnapData();
			return;
		}
		float num10 = Mathf.Abs(mScrollRect.velocity.y);
		UpdateCurSnapData();
		if (mCurSnapData.mSnapStatus != SnapStatus.SnapMoving)
		{
			return;
		}
		if (num10 > 0f)
		{
			mScrollRect.StopMovement();
		}
		float mCurSnapVal = mCurSnapData.mCurSnapVal;
		if (!mCurSnapData.mIsTempTarget)
		{
			if (mSmoothDumpVel * mCurSnapData.mTargetSnapVal < 0f)
			{
				mSmoothDumpVel = 0f;
			}
			mCurSnapData.mCurSnapVal = Mathf.SmoothDamp(mCurSnapData.mCurSnapVal, mCurSnapData.mTargetSnapVal, ref mSmoothDumpVel, mSmoothDumpRate);
		}
		else
		{
			float mMoveMaxAbsVec = mCurSnapData.mMoveMaxAbsVec;
			if (mMoveMaxAbsVec <= 0f)
			{
				mMoveMaxAbsVec = mSnapMoveDefaultMaxAbsVec;
			}
			mSmoothDumpVel = mMoveMaxAbsVec * Mathf.Sign(mCurSnapData.mTargetSnapVal);
			mCurSnapData.mCurSnapVal = Mathf.MoveTowards(mCurSnapData.mCurSnapVal, mCurSnapData.mTargetSnapVal, mMoveMaxAbsVec * Time.deltaTime);
		}
		float num11 = mCurSnapData.mCurSnapVal - mCurSnapVal;
		if (immediate || Mathf.Abs(mCurSnapData.mTargetSnapVal - mCurSnapData.mCurSnapVal) < mSnapFinishThreshold)
		{
			anchoredPosition3D.y = anchoredPosition3D.y + mCurSnapData.mTargetSnapVal - mCurSnapVal;
			mCurSnapData.mSnapStatus = SnapStatus.SnapMoveFinish;
			if (mOnSnapItemFinished != null)
			{
				LoopListViewItem2 shownItemByItemIndex = GetShownItemByItemIndex(mCurSnapNearestItemIndex);
				if ((Object)(object)shownItemByItemIndex != (Object)null)
				{
					mOnSnapItemFinished(this, shownItemByItemIndex);
				}
			}
		}
		else
		{
			anchoredPosition3D.y += num11;
		}
		if (mArrangeType == ListItemArrangeType.TopToBottom)
		{
			float y2 = mViewPortRectLocalCorners[0].y;
			rect = mContainerTrans.rect;
			float num12 = y2 + ((Rect)(ref rect)).height;
			anchoredPosition3D.y = Mathf.Clamp(anchoredPosition3D.y, 0f, num12);
			mContainerTrans.anchoredPosition3D = anchoredPosition3D;
		}
		else if (mArrangeType == ListItemArrangeType.BottomToTop)
		{
			float y3 = mViewPortRectLocalCorners[1].y;
			rect = mContainerTrans.rect;
			float num13 = y3 - ((Rect)(ref rect)).height;
			anchoredPosition3D.y = Mathf.Clamp(anchoredPosition3D.y, num13, 0f);
			mContainerTrans.anchoredPosition3D = anchoredPosition3D;
		}
	}

	private void UpdateCurSnapData()
	{
		if (mItemList.Count == 0)
		{
			mCurSnapData.Clear();
			return;
		}
		if (mCurSnapData.mSnapStatus == SnapStatus.SnapMoveFinish)
		{
			if (mCurSnapData.mSnapTargetIndex == mCurSnapNearestItemIndex)
			{
				return;
			}
			mCurSnapData.mSnapStatus = SnapStatus.NoTargetSet;
		}
		if (mCurSnapData.mSnapStatus == SnapStatus.SnapMoving)
		{
			if (mCurSnapData.mIsForceSnapTo)
			{
				if (mCurSnapData.mIsTempTarget)
				{
					LoopListViewItem2 shownItemNearestItemIndex = GetShownItemNearestItemIndex(mCurSnapData.mSnapTargetIndex);
					if ((Object)(object)shownItemNearestItemIndex == (Object)null)
					{
						mCurSnapData.Clear();
					}
					else if (shownItemNearestItemIndex.ItemIndex == mCurSnapData.mSnapTargetIndex)
					{
						UpdateAllShownItemSnapData();
						mCurSnapData.mTargetSnapVal = shownItemNearestItemIndex.DistanceWithViewPortSnapCenter;
						mCurSnapData.mCurSnapVal = 0f;
						mCurSnapData.mIsTempTarget = false;
						mCurSnapData.mSnapStatus = SnapStatus.SnapMoving;
					}
					else if (mCurSnapData.mTempTargetIndex != shownItemNearestItemIndex.ItemIndex)
					{
						UpdateAllShownItemSnapData();
						mCurSnapData.mTargetSnapVal = shownItemNearestItemIndex.DistanceWithViewPortSnapCenter;
						mCurSnapData.mCurSnapVal = 0f;
						mCurSnapData.mSnapStatus = SnapStatus.SnapMoving;
						mCurSnapData.mIsTempTarget = true;
						mCurSnapData.mTempTargetIndex = shownItemNearestItemIndex.ItemIndex;
					}
				}
				return;
			}
			if (mCurSnapData.mSnapTargetIndex == mCurSnapNearestItemIndex)
			{
				return;
			}
			mCurSnapData.mSnapStatus = SnapStatus.NoTargetSet;
		}
		if (mCurSnapData.mSnapStatus == SnapStatus.NoTargetSet)
		{
			if ((Object)(object)GetShownItemByItemIndex(mCurSnapNearestItemIndex) == (Object)null)
			{
				return;
			}
			mCurSnapData.mSnapTargetIndex = mCurSnapNearestItemIndex;
			mCurSnapData.mSnapStatus = SnapStatus.TargetHasSet;
			mCurSnapData.mIsForceSnapTo = false;
		}
		if (mCurSnapData.mSnapStatus == SnapStatus.TargetHasSet)
		{
			LoopListViewItem2 shownItemNearestItemIndex2 = GetShownItemNearestItemIndex(mCurSnapData.mSnapTargetIndex);
			if ((Object)(object)shownItemNearestItemIndex2 == (Object)null)
			{
				mCurSnapData.Clear();
			}
			else if (shownItemNearestItemIndex2.ItemIndex == mCurSnapData.mSnapTargetIndex)
			{
				UpdateAllShownItemSnapData();
				mCurSnapData.mTargetSnapVal = shownItemNearestItemIndex2.DistanceWithViewPortSnapCenter;
				mCurSnapData.mCurSnapVal = 0f;
				mCurSnapData.mIsTempTarget = false;
				mCurSnapData.mSnapStatus = SnapStatus.SnapMoving;
			}
			else
			{
				UpdateAllShownItemSnapData();
				mCurSnapData.mTargetSnapVal = shownItemNearestItemIndex2.DistanceWithViewPortSnapCenter;
				mCurSnapData.mCurSnapVal = 0f;
				mCurSnapData.mSnapStatus = SnapStatus.SnapMoving;
				mCurSnapData.mIsTempTarget = true;
				mCurSnapData.mTempTargetIndex = shownItemNearestItemIndex2.ItemIndex;
			}
		}
	}

	public void ClearSnapData()
	{
		mCurSnapData.Clear();
	}

	public void SetSnapTargetItemIndex(int itemIndex, float moveMaxAbsVec = -1f)
	{
		if (mItemTotalCount > 0)
		{
			if (itemIndex >= mItemTotalCount)
			{
				itemIndex = mItemTotalCount - 1;
			}
			if (itemIndex < 0)
			{
				itemIndex = 0;
			}
		}
		mScrollRect.StopMovement();
		mCurSnapData.mSnapTargetIndex = itemIndex;
		mCurSnapData.mSnapStatus = SnapStatus.TargetHasSet;
		mCurSnapData.mIsForceSnapTo = true;
		mCurSnapData.mMoveMaxAbsVec = moveMaxAbsVec;
	}

	public void ForceSnapUpdateCheck()
	{
		if (mLeftSnapUpdateExtraCount <= 0)
		{
			mLeftSnapUpdateExtraCount = 1;
		}
	}

	private void UpdateSnapHorizontal(bool immediate = false, bool forceSendEvent = false)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0310: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0458: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04de: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_050e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0535: Unknown result type (might be due to invalid IL or missing references)
		//IL_053a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0548: Unknown result type (might be due to invalid IL or missing references)
		//IL_0565: Unknown result type (might be due to invalid IL or missing references)
		if (!mItemSnapEnable)
		{
			return;
		}
		int count = mItemList.Count;
		if (count == 0)
		{
			return;
		}
		Vector3 anchoredPosition3D = mContainerTrans.anchoredPosition3D;
		bool flag = anchoredPosition3D.x != mLastSnapCheckPos.x;
		mLastSnapCheckPos = anchoredPosition3D;
		if (!flag && mLeftSnapUpdateExtraCount > 0)
		{
			mLeftSnapUpdateExtraCount--;
			flag = true;
		}
		Rect rect;
		if (flag)
		{
			LoopListViewItem2 loopListViewItem = mItemList[0];
			loopListViewItem.CachedRectTransform.GetWorldCorners(mItemWorldCorners);
			int num = -1;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = 0f;
			float num5 = float.MaxValue;
			float num6 = 0f;
			float num7 = 0f;
			if (mArrangeType == ListItemArrangeType.RightToLeft)
			{
				float num8 = 0f - (1f - mViewPortSnapPivot.x);
				rect = mViewPortRectTransform.rect;
				num7 = num8 * ((Rect)(ref rect)).width;
				num2 = ((Transform)mViewPortRectTransform).InverseTransformPoint(mItemWorldCorners[2]).x;
				num3 = num2 - loopListViewItem.ItemSizeWithPadding;
				num4 = num2 - loopListViewItem.ItemSize * (1f - mItemSnapPivot.x);
				for (int i = 0; i < count; i++)
				{
					num6 = Mathf.Abs(num7 - num4);
					if (!(num6 < num5))
					{
						break;
					}
					num5 = num6;
					num = i;
					if (i + 1 < count)
					{
						num2 = num3;
						num3 -= mItemList[i + 1].ItemSizeWithPadding;
						num4 = num2 - mItemList[i + 1].ItemSize * (1f - mItemSnapPivot.x);
					}
				}
			}
			else if (mArrangeType == ListItemArrangeType.LeftToRight)
			{
				float x = mViewPortSnapPivot.x;
				rect = mViewPortRectTransform.rect;
				num7 = x * ((Rect)(ref rect)).width;
				num2 = ((Transform)mViewPortRectTransform).InverseTransformPoint(mItemWorldCorners[1]).x;
				num3 = num2 + loopListViewItem.ItemSizeWithPadding;
				num4 = num2 + loopListViewItem.ItemSize * mItemSnapPivot.x;
				for (int j = 0; j < count; j++)
				{
					num6 = Mathf.Abs(num7 - num4);
					if (!(num6 < num5))
					{
						break;
					}
					num5 = num6;
					num = j;
					if (j + 1 < count)
					{
						num2 = num3;
						num3 += mItemList[j + 1].ItemSizeWithPadding;
						num4 = num2 + mItemList[j + 1].ItemSize * mItemSnapPivot.x;
					}
				}
			}
			if (num >= 0)
			{
				int num9 = mCurSnapNearestItemIndex;
				mCurSnapNearestItemIndex = mItemList[num].ItemIndex;
				if ((forceSendEvent || mItemList[num].ItemIndex != num9) && mOnSnapNearestChanged != null)
				{
					mOnSnapNearestChanged(this, mItemList[num]);
				}
			}
			else
			{
				mCurSnapNearestItemIndex = -1;
			}
		}
		if (!CanSnap())
		{
			ClearSnapData();
			return;
		}
		float num10 = Mathf.Abs(mScrollRect.velocity.x);
		UpdateCurSnapData();
		if (mCurSnapData.mSnapStatus != SnapStatus.SnapMoving)
		{
			return;
		}
		if (num10 > 0f)
		{
			mScrollRect.StopMovement();
		}
		float mCurSnapVal = mCurSnapData.mCurSnapVal;
		if (!mCurSnapData.mIsTempTarget)
		{
			if (mSmoothDumpVel * mCurSnapData.mTargetSnapVal < 0f)
			{
				mSmoothDumpVel = 0f;
			}
			mCurSnapData.mCurSnapVal = Mathf.SmoothDamp(mCurSnapData.mCurSnapVal, mCurSnapData.mTargetSnapVal, ref mSmoothDumpVel, mSmoothDumpRate);
		}
		else
		{
			float mMoveMaxAbsVec = mCurSnapData.mMoveMaxAbsVec;
			if (mMoveMaxAbsVec <= 0f)
			{
				mMoveMaxAbsVec = mSnapMoveDefaultMaxAbsVec;
			}
			mSmoothDumpVel = mMoveMaxAbsVec * Mathf.Sign(mCurSnapData.mTargetSnapVal);
			mCurSnapData.mCurSnapVal = Mathf.MoveTowards(mCurSnapData.mCurSnapVal, mCurSnapData.mTargetSnapVal, mMoveMaxAbsVec * Time.deltaTime);
		}
		float num11 = mCurSnapData.mCurSnapVal - mCurSnapVal;
		if (immediate || Mathf.Abs(mCurSnapData.mTargetSnapVal - mCurSnapData.mCurSnapVal) < mSnapFinishThreshold)
		{
			anchoredPosition3D.x = anchoredPosition3D.x + mCurSnapData.mTargetSnapVal - mCurSnapVal;
			mCurSnapData.mSnapStatus = SnapStatus.SnapMoveFinish;
			if (mOnSnapItemFinished != null)
			{
				LoopListViewItem2 shownItemByItemIndex = GetShownItemByItemIndex(mCurSnapNearestItemIndex);
				if ((Object)(object)shownItemByItemIndex != (Object)null)
				{
					mOnSnapItemFinished(this, shownItemByItemIndex);
				}
			}
		}
		else
		{
			anchoredPosition3D.x += num11;
		}
		if (mArrangeType == ListItemArrangeType.LeftToRight)
		{
			float x2 = mViewPortRectLocalCorners[2].x;
			rect = mContainerTrans.rect;
			float num12 = x2 - ((Rect)(ref rect)).width;
			anchoredPosition3D.x = Mathf.Clamp(anchoredPosition3D.x, num12, 0f);
			mContainerTrans.anchoredPosition3D = anchoredPosition3D;
		}
		else if (mArrangeType == ListItemArrangeType.RightToLeft)
		{
			float x3 = mViewPortRectLocalCorners[1].x;
			rect = mContainerTrans.rect;
			float num13 = x3 + ((Rect)(ref rect)).width;
			anchoredPosition3D.x = Mathf.Clamp(anchoredPosition3D.x, 0f, num13);
			mContainerTrans.anchoredPosition3D = anchoredPosition3D;
		}
	}

	private bool CanSnap()
	{
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		if (mIsDraging)
		{
			return false;
		}
		if ((Object)(object)mScrollBarClickEventListener != (Object)null && mScrollBarClickEventListener.IsPressd)
		{
			return false;
		}
		Rect rect;
		if (mIsVertList)
		{
			rect = mContainerTrans.rect;
			if (((Rect)(ref rect)).height <= ViewPortHeight)
			{
				return false;
			}
		}
		else
		{
			rect = mContainerTrans.rect;
			if (((Rect)(ref rect)).width <= ViewPortWidth)
			{
				return false;
			}
		}
		float num = 0f;
		num = ((!mIsVertList) ? Mathf.Abs(mScrollRect.velocity.x) : Mathf.Abs(mScrollRect.velocity.y));
		if (num > mSnapVecThreshold)
		{
			return false;
		}
		float num2 = 3f;
		Vector3 anchoredPosition3D = mContainerTrans.anchoredPosition3D;
		if (mArrangeType == ListItemArrangeType.LeftToRight)
		{
			float x = mViewPortRectLocalCorners[2].x;
			rect = mContainerTrans.rect;
			float num3 = x - ((Rect)(ref rect)).width;
			if (anchoredPosition3D.x < num3 - num2 || anchoredPosition3D.x > num2)
			{
				return false;
			}
		}
		else if (mArrangeType == ListItemArrangeType.RightToLeft)
		{
			float x2 = mViewPortRectLocalCorners[1].x;
			rect = mContainerTrans.rect;
			float num4 = x2 + ((Rect)(ref rect)).width;
			if (anchoredPosition3D.x > num4 + num2 || anchoredPosition3D.x < 0f - num2)
			{
				return false;
			}
		}
		else if (mArrangeType == ListItemArrangeType.TopToBottom)
		{
			float y = mViewPortRectLocalCorners[0].y;
			rect = mContainerTrans.rect;
			float num5 = y + ((Rect)(ref rect)).height;
			if (anchoredPosition3D.y > num5 + num2 || anchoredPosition3D.y < 0f - num2)
			{
				return false;
			}
		}
		else if (mArrangeType == ListItemArrangeType.BottomToTop)
		{
			float y2 = mViewPortRectLocalCorners[1].y;
			rect = mContainerTrans.rect;
			float num6 = y2 - ((Rect)(ref rect)).height;
			if (anchoredPosition3D.y < num6 - num2 || anchoredPosition3D.y > num2)
			{
				return false;
			}
		}
		return true;
	}

	public void UpdateListView(float distanceForRecycle0, float distanceForRecycle1, float distanceForNew0, float distanceForNew1)
	{
		mListUpdateCheckFrameCount++;
		if (mIsVertList)
		{
			bool flag = true;
			int num = 0;
			int num2 = 9999;
			while (flag)
			{
				num++;
				if (num >= num2)
				{
					Debug.LogError((object)("UpdateListView Vertical while loop " + num + " times! something is wrong!"));
					break;
				}
				flag = UpdateForVertList(distanceForRecycle0, distanceForRecycle1, distanceForNew0, distanceForNew1);
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
				Debug.LogError((object)("UpdateListView  Horizontal while loop " + num3 + " times! something is wrong!"));
				break;
			}
			flag2 = UpdateForHorizontalList(distanceForRecycle0, distanceForRecycle1, distanceForNew0, distanceForNew1);
		}
	}

	private bool UpdateForVertList(float distanceForRecycle0, float distanceForRecycle1, float distanceForNew0, float distanceForNew1)
	{
		//IL_05c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_05db: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_067d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0682: Unknown result type (might be due to invalid IL or missing references)
		//IL_0687: Unknown result type (might be due to invalid IL or missing references)
		//IL_0696: Unknown result type (might be due to invalid IL or missing references)
		//IL_069b: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_070b: Unknown result type (might be due to invalid IL or missing references)
		//IL_060f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_084e: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0399: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0583: Unknown result type (might be due to invalid IL or missing references)
		//IL_0547: Unknown result type (might be due to invalid IL or missing references)
		//IL_054c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0813: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_0332: Unknown result type (might be due to invalid IL or missing references)
		//IL_0342: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Unknown result type (might be due to invalid IL or missing references)
		//IL_08fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_090e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0913: Unknown result type (might be due to invalid IL or missing references)
		//IL_093c: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0463: Unknown result type (might be due to invalid IL or missing references)
		//IL_0474: Unknown result type (might be due to invalid IL or missing references)
		//IL_0479: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0435: Unknown result type (might be due to invalid IL or missing references)
		//IL_043a: Unknown result type (might be due to invalid IL or missing references)
		if (mItemTotalCount == 0)
		{
			if (mItemList.Count > 0)
			{
				RecycleAllItem();
			}
			return false;
		}
		Rect rect;
		if (mArrangeType == ListItemArrangeType.TopToBottom)
		{
			if (mItemList.Count == 0)
			{
				float num = mContainerTrans.anchoredPosition3D.y;
				if (num < 0f)
				{
					num = 0f;
				}
				int index = 0;
				float itemPos = 0f - num;
				if (mSupportScrollBar)
				{
					if (!GetPlusItemIndexAndPosAtGivenPos(num, ref index, ref itemPos))
					{
						return false;
					}
					itemPos = 0f - itemPos;
				}
				LoopListViewItem2 newItemByIndex = GetNewItemByIndex(index);
				if ((Object)(object)newItemByIndex == (Object)null)
				{
					return false;
				}
				if (mSupportScrollBar)
				{
					int itemIndex = index;
					rect = newItemByIndex.CachedRectTransform.rect;
					SetItemSize(itemIndex, ((Rect)(ref rect)).height, newItemByIndex.Padding);
				}
				mItemList.Add(newItemByIndex);
				newItemByIndex.CachedRectTransform.anchoredPosition3D = new Vector3(newItemByIndex.StartPosOffset, itemPos, 0f);
				UpdateContentSize();
				return true;
			}
			LoopListViewItem2 loopListViewItem = mItemList[0];
			loopListViewItem.CachedRectTransform.GetWorldCorners(mItemWorldCorners);
			Vector3 val = ((Transform)mViewPortRectTransform).InverseTransformPoint(mItemWorldCorners[1]);
			Vector3 val2 = ((Transform)mViewPortRectTransform).InverseTransformPoint(mItemWorldCorners[0]);
			if (!mIsDraging && loopListViewItem.ItemCreatedCheckFrameCount != mListUpdateCheckFrameCount && val2.y - mViewPortRectLocalCorners[1].y > distanceForRecycle0)
			{
				mItemList.RemoveAt(0);
				RecycleItemTmp(loopListViewItem);
				if (!mSupportScrollBar)
				{
					UpdateContentSize();
					CheckIfNeedUpdataItemPos();
				}
				return true;
			}
			LoopListViewItem2 loopListViewItem2 = mItemList[mItemList.Count - 1];
			loopListViewItem2.CachedRectTransform.GetWorldCorners(mItemWorldCorners);
			Vector3 val3 = ((Transform)mViewPortRectTransform).InverseTransformPoint(mItemWorldCorners[1]);
			Vector3 val4 = ((Transform)mViewPortRectTransform).InverseTransformPoint(mItemWorldCorners[0]);
			if (!mIsDraging && loopListViewItem2.ItemCreatedCheckFrameCount != mListUpdateCheckFrameCount && mViewPortRectLocalCorners[0].y - val3.y > distanceForRecycle1)
			{
				mItemList.RemoveAt(mItemList.Count - 1);
				RecycleItemTmp(loopListViewItem2);
				if (!mSupportScrollBar)
				{
					UpdateContentSize();
					CheckIfNeedUpdataItemPos();
				}
				return true;
			}
			if (mViewPortRectLocalCorners[0].y - val4.y < distanceForNew1)
			{
				if (loopListViewItem2.ItemIndex > mCurReadyMaxItemIndex)
				{
					mCurReadyMaxItemIndex = loopListViewItem2.ItemIndex;
					mNeedCheckNextMaxItem = true;
				}
				int num2 = loopListViewItem2.ItemIndex + 1;
				if (num2 <= mCurReadyMaxItemIndex || mNeedCheckNextMaxItem)
				{
					LoopListViewItem2 newItemByIndex2 = GetNewItemByIndex(num2);
					if (!((Object)(object)newItemByIndex2 == (Object)null))
					{
						if (mSupportScrollBar)
						{
							rect = newItemByIndex2.CachedRectTransform.rect;
							SetItemSize(num2, ((Rect)(ref rect)).height, newItemByIndex2.Padding);
						}
						mItemList.Add(newItemByIndex2);
						float y = loopListViewItem2.CachedRectTransform.anchoredPosition3D.y;
						rect = loopListViewItem2.CachedRectTransform.rect;
						float num3 = y - ((Rect)(ref rect)).height - loopListViewItem2.Padding;
						newItemByIndex2.CachedRectTransform.anchoredPosition3D = new Vector3(newItemByIndex2.StartPosOffset, num3, 0f);
						UpdateContentSize();
						CheckIfNeedUpdataItemPos();
						if (num2 > mCurReadyMaxItemIndex)
						{
							mCurReadyMaxItemIndex = num2;
						}
						return true;
					}
					mCurReadyMaxItemIndex = loopListViewItem2.ItemIndex;
					mNeedCheckNextMaxItem = false;
					CheckIfNeedUpdataItemPos();
				}
			}
			if (val.y - mViewPortRectLocalCorners[1].y < distanceForNew0)
			{
				if (loopListViewItem.ItemIndex < mCurReadyMinItemIndex)
				{
					mCurReadyMinItemIndex = loopListViewItem.ItemIndex;
					mNeedCheckNextMinItem = true;
				}
				int num4 = loopListViewItem.ItemIndex - 1;
				if (num4 >= mCurReadyMinItemIndex || mNeedCheckNextMinItem)
				{
					LoopListViewItem2 newItemByIndex3 = GetNewItemByIndex(num4);
					if (!((Object)(object)newItemByIndex3 == (Object)null))
					{
						if (mSupportScrollBar)
						{
							rect = newItemByIndex3.CachedRectTransform.rect;
							SetItemSize(num4, ((Rect)(ref rect)).height, newItemByIndex3.Padding);
						}
						mItemList.Insert(0, newItemByIndex3);
						float y2 = loopListViewItem.CachedRectTransform.anchoredPosition3D.y;
						rect = newItemByIndex3.CachedRectTransform.rect;
						float num5 = y2 + ((Rect)(ref rect)).height + newItemByIndex3.Padding;
						newItemByIndex3.CachedRectTransform.anchoredPosition3D = new Vector3(newItemByIndex3.StartPosOffset, num5, 0f);
						UpdateContentSize();
						CheckIfNeedUpdataItemPos();
						if (num4 < mCurReadyMinItemIndex)
						{
							mCurReadyMinItemIndex = num4;
						}
						return true;
					}
					mCurReadyMinItemIndex = loopListViewItem.ItemIndex;
					mNeedCheckNextMinItem = false;
				}
			}
		}
		else
		{
			if (mItemList.Count == 0)
			{
				float num6 = mContainerTrans.anchoredPosition3D.y;
				if (num6 > 0f)
				{
					num6 = 0f;
				}
				int index2 = 0;
				float itemPos2 = 0f - num6;
				if (mSupportScrollBar && !GetPlusItemIndexAndPosAtGivenPos(0f - num6, ref index2, ref itemPos2))
				{
					return false;
				}
				LoopListViewItem2 newItemByIndex4 = GetNewItemByIndex(index2);
				if ((Object)(object)newItemByIndex4 == (Object)null)
				{
					return false;
				}
				if (mSupportScrollBar)
				{
					int itemIndex2 = index2;
					rect = newItemByIndex4.CachedRectTransform.rect;
					SetItemSize(itemIndex2, ((Rect)(ref rect)).height, newItemByIndex4.Padding);
				}
				mItemList.Add(newItemByIndex4);
				newItemByIndex4.CachedRectTransform.anchoredPosition3D = new Vector3(newItemByIndex4.StartPosOffset, itemPos2, 0f);
				UpdateContentSize();
				return true;
			}
			LoopListViewItem2 loopListViewItem3 = mItemList[0];
			loopListViewItem3.CachedRectTransform.GetWorldCorners(mItemWorldCorners);
			Vector3 val5 = ((Transform)mViewPortRectTransform).InverseTransformPoint(mItemWorldCorners[1]);
			Vector3 val6 = ((Transform)mViewPortRectTransform).InverseTransformPoint(mItemWorldCorners[0]);
			if (!mIsDraging && loopListViewItem3.ItemCreatedCheckFrameCount != mListUpdateCheckFrameCount && mViewPortRectLocalCorners[0].y - val5.y > distanceForRecycle0)
			{
				mItemList.RemoveAt(0);
				RecycleItemTmp(loopListViewItem3);
				if (!mSupportScrollBar)
				{
					UpdateContentSize();
					CheckIfNeedUpdataItemPos();
				}
				return true;
			}
			LoopListViewItem2 loopListViewItem4 = mItemList[mItemList.Count - 1];
			loopListViewItem4.CachedRectTransform.GetWorldCorners(mItemWorldCorners);
			Vector3 val7 = ((Transform)mViewPortRectTransform).InverseTransformPoint(mItemWorldCorners[1]);
			Vector3 val8 = ((Transform)mViewPortRectTransform).InverseTransformPoint(mItemWorldCorners[0]);
			if (!mIsDraging && loopListViewItem4.ItemCreatedCheckFrameCount != mListUpdateCheckFrameCount && val8.y - mViewPortRectLocalCorners[1].y > distanceForRecycle1)
			{
				mItemList.RemoveAt(mItemList.Count - 1);
				RecycleItemTmp(loopListViewItem4);
				if (!mSupportScrollBar)
				{
					UpdateContentSize();
					CheckIfNeedUpdataItemPos();
				}
				return true;
			}
			if (val7.y - mViewPortRectLocalCorners[1].y < distanceForNew1)
			{
				if (loopListViewItem4.ItemIndex > mCurReadyMaxItemIndex)
				{
					mCurReadyMaxItemIndex = loopListViewItem4.ItemIndex;
					mNeedCheckNextMaxItem = true;
				}
				int num7 = loopListViewItem4.ItemIndex + 1;
				if (num7 <= mCurReadyMaxItemIndex || mNeedCheckNextMaxItem)
				{
					LoopListViewItem2 newItemByIndex5 = GetNewItemByIndex(num7);
					if (!((Object)(object)newItemByIndex5 == (Object)null))
					{
						if (mSupportScrollBar)
						{
							rect = newItemByIndex5.CachedRectTransform.rect;
							SetItemSize(num7, ((Rect)(ref rect)).height, newItemByIndex5.Padding);
						}
						mItemList.Add(newItemByIndex5);
						float y3 = loopListViewItem4.CachedRectTransform.anchoredPosition3D.y;
						rect = loopListViewItem4.CachedRectTransform.rect;
						float num8 = y3 + ((Rect)(ref rect)).height + loopListViewItem4.Padding;
						newItemByIndex5.CachedRectTransform.anchoredPosition3D = new Vector3(newItemByIndex5.StartPosOffset, num8, 0f);
						UpdateContentSize();
						CheckIfNeedUpdataItemPos();
						if (num7 > mCurReadyMaxItemIndex)
						{
							mCurReadyMaxItemIndex = num7;
						}
						return true;
					}
					mNeedCheckNextMaxItem = false;
					CheckIfNeedUpdataItemPos();
				}
			}
			if (mViewPortRectLocalCorners[0].y - val6.y < distanceForNew0)
			{
				if (loopListViewItem3.ItemIndex < mCurReadyMinItemIndex)
				{
					mCurReadyMinItemIndex = loopListViewItem3.ItemIndex;
					mNeedCheckNextMinItem = true;
				}
				int num9 = loopListViewItem3.ItemIndex - 1;
				if (num9 >= mCurReadyMinItemIndex || mNeedCheckNextMinItem)
				{
					LoopListViewItem2 newItemByIndex6 = GetNewItemByIndex(num9);
					if ((Object)(object)newItemByIndex6 == (Object)null)
					{
						mNeedCheckNextMinItem = false;
						return false;
					}
					if (mSupportScrollBar)
					{
						rect = newItemByIndex6.CachedRectTransform.rect;
						SetItemSize(num9, ((Rect)(ref rect)).height, newItemByIndex6.Padding);
					}
					mItemList.Insert(0, newItemByIndex6);
					float y4 = loopListViewItem3.CachedRectTransform.anchoredPosition3D.y;
					rect = newItemByIndex6.CachedRectTransform.rect;
					float num10 = y4 - ((Rect)(ref rect)).height - newItemByIndex6.Padding;
					newItemByIndex6.CachedRectTransform.anchoredPosition3D = new Vector3(newItemByIndex6.StartPosOffset, num10, 0f);
					UpdateContentSize();
					CheckIfNeedUpdataItemPos();
					if (num9 < mCurReadyMinItemIndex)
					{
						mCurReadyMinItemIndex = num9;
					}
					return true;
				}
			}
		}
		return false;
	}

	private bool UpdateForHorizontalList(float distanceForRecycle0, float distanceForRecycle1, float distanceForNew0, float distanceForNew1)
	{
		//IL_05c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04df: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_067e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0683: Unknown result type (might be due to invalid IL or missing references)
		//IL_0688: Unknown result type (might be due to invalid IL or missing references)
		//IL_0697: Unknown result type (might be due to invalid IL or missing references)
		//IL_069c: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_071d: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_084b: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0584: Unknown result type (might be due to invalid IL or missing references)
		//IL_0548: Unknown result type (might be due to invalid IL or missing references)
		//IL_054d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0821: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0344: Unknown result type (might be due to invalid IL or missing references)
		//IL_036c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_091b: Unknown result type (might be due to invalid IL or missing references)
		//IL_092c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0931: Unknown result type (might be due to invalid IL or missing references)
		//IL_095a: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0460: Unknown result type (might be due to invalid IL or missing references)
		//IL_0471: Unknown result type (might be due to invalid IL or missing references)
		//IL_0476: Unknown result type (might be due to invalid IL or missing references)
		//IL_049f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0432: Unknown result type (might be due to invalid IL or missing references)
		//IL_0437: Unknown result type (might be due to invalid IL or missing references)
		if (mItemTotalCount == 0)
		{
			if (mItemList.Count > 0)
			{
				RecycleAllItem();
			}
			return false;
		}
		Rect rect;
		if (mArrangeType == ListItemArrangeType.LeftToRight)
		{
			if (mItemList.Count == 0)
			{
				float num = mContainerTrans.anchoredPosition3D.x;
				if (num > 0f)
				{
					num = 0f;
				}
				int index = 0;
				float itemPos = 0f - num;
				if (mSupportScrollBar && !GetPlusItemIndexAndPosAtGivenPos(0f - num, ref index, ref itemPos))
				{
					return false;
				}
				LoopListViewItem2 newItemByIndex = GetNewItemByIndex(index);
				if ((Object)(object)newItemByIndex == (Object)null)
				{
					return false;
				}
				if (mSupportScrollBar)
				{
					int itemIndex = index;
					rect = newItemByIndex.CachedRectTransform.rect;
					SetItemSize(itemIndex, ((Rect)(ref rect)).width, newItemByIndex.Padding);
				}
				mItemList.Add(newItemByIndex);
				newItemByIndex.CachedRectTransform.anchoredPosition3D = new Vector3(itemPos, newItemByIndex.StartPosOffset, 0f);
				UpdateContentSize();
				return true;
			}
			LoopListViewItem2 loopListViewItem = mItemList[0];
			loopListViewItem.CachedRectTransform.GetWorldCorners(mItemWorldCorners);
			Vector3 val = ((Transform)mViewPortRectTransform).InverseTransformPoint(mItemWorldCorners[1]);
			Vector3 val2 = ((Transform)mViewPortRectTransform).InverseTransformPoint(mItemWorldCorners[2]);
			if (!mIsDraging && loopListViewItem.ItemCreatedCheckFrameCount != mListUpdateCheckFrameCount && mViewPortRectLocalCorners[1].x - val2.x > distanceForRecycle0)
			{
				mItemList.RemoveAt(0);
				RecycleItemTmp(loopListViewItem);
				if (!mSupportScrollBar)
				{
					UpdateContentSize();
					CheckIfNeedUpdataItemPos();
				}
				return true;
			}
			LoopListViewItem2 loopListViewItem2 = mItemList[mItemList.Count - 1];
			loopListViewItem2.CachedRectTransform.GetWorldCorners(mItemWorldCorners);
			Vector3 val3 = ((Transform)mViewPortRectTransform).InverseTransformPoint(mItemWorldCorners[1]);
			Vector3 val4 = ((Transform)mViewPortRectTransform).InverseTransformPoint(mItemWorldCorners[2]);
			if (!mIsDraging && loopListViewItem2.ItemCreatedCheckFrameCount != mListUpdateCheckFrameCount && val3.x - mViewPortRectLocalCorners[2].x > distanceForRecycle1)
			{
				mItemList.RemoveAt(mItemList.Count - 1);
				RecycleItemTmp(loopListViewItem2);
				if (!mSupportScrollBar)
				{
					UpdateContentSize();
					CheckIfNeedUpdataItemPos();
				}
				return true;
			}
			if (val4.x - mViewPortRectLocalCorners[2].x < distanceForNew1)
			{
				if (loopListViewItem2.ItemIndex > mCurReadyMaxItemIndex)
				{
					mCurReadyMaxItemIndex = loopListViewItem2.ItemIndex;
					mNeedCheckNextMaxItem = true;
				}
				int num2 = loopListViewItem2.ItemIndex + 1;
				if (num2 <= mCurReadyMaxItemIndex || mNeedCheckNextMaxItem)
				{
					LoopListViewItem2 newItemByIndex2 = GetNewItemByIndex(num2);
					if (!((Object)(object)newItemByIndex2 == (Object)null))
					{
						if (mSupportScrollBar)
						{
							rect = newItemByIndex2.CachedRectTransform.rect;
							SetItemSize(num2, ((Rect)(ref rect)).width, newItemByIndex2.Padding);
						}
						mItemList.Add(newItemByIndex2);
						float x = loopListViewItem2.CachedRectTransform.anchoredPosition3D.x;
						rect = loopListViewItem2.CachedRectTransform.rect;
						float num3 = x + ((Rect)(ref rect)).width + loopListViewItem2.Padding;
						newItemByIndex2.CachedRectTransform.anchoredPosition3D = new Vector3(num3, newItemByIndex2.StartPosOffset, 0f);
						UpdateContentSize();
						CheckIfNeedUpdataItemPos();
						if (num2 > mCurReadyMaxItemIndex)
						{
							mCurReadyMaxItemIndex = num2;
						}
						return true;
					}
					mCurReadyMaxItemIndex = loopListViewItem2.ItemIndex;
					mNeedCheckNextMaxItem = false;
					CheckIfNeedUpdataItemPos();
				}
			}
			if (mViewPortRectLocalCorners[1].x - val.x < distanceForNew0)
			{
				if (loopListViewItem.ItemIndex < mCurReadyMinItemIndex)
				{
					mCurReadyMinItemIndex = loopListViewItem.ItemIndex;
					mNeedCheckNextMinItem = true;
				}
				int num4 = loopListViewItem.ItemIndex - 1;
				if (num4 >= mCurReadyMinItemIndex || mNeedCheckNextMinItem)
				{
					LoopListViewItem2 newItemByIndex3 = GetNewItemByIndex(num4);
					if (!((Object)(object)newItemByIndex3 == (Object)null))
					{
						if (mSupportScrollBar)
						{
							rect = newItemByIndex3.CachedRectTransform.rect;
							SetItemSize(num4, ((Rect)(ref rect)).width, newItemByIndex3.Padding);
						}
						mItemList.Insert(0, newItemByIndex3);
						float x2 = loopListViewItem.CachedRectTransform.anchoredPosition3D.x;
						rect = newItemByIndex3.CachedRectTransform.rect;
						float num5 = x2 - ((Rect)(ref rect)).width - newItemByIndex3.Padding;
						newItemByIndex3.CachedRectTransform.anchoredPosition3D = new Vector3(num5, newItemByIndex3.StartPosOffset, 0f);
						UpdateContentSize();
						CheckIfNeedUpdataItemPos();
						if (num4 < mCurReadyMinItemIndex)
						{
							mCurReadyMinItemIndex = num4;
						}
						return true;
					}
					mCurReadyMinItemIndex = loopListViewItem.ItemIndex;
					mNeedCheckNextMinItem = false;
				}
			}
		}
		else
		{
			if (mItemList.Count == 0)
			{
				float num6 = mContainerTrans.anchoredPosition3D.x;
				if (num6 < 0f)
				{
					num6 = 0f;
				}
				int index2 = 0;
				float itemPos2 = 0f - num6;
				if (mSupportScrollBar)
				{
					if (!GetPlusItemIndexAndPosAtGivenPos(num6, ref index2, ref itemPos2))
					{
						return false;
					}
					itemPos2 = 0f - itemPos2;
				}
				LoopListViewItem2 newItemByIndex4 = GetNewItemByIndex(index2);
				if ((Object)(object)newItemByIndex4 == (Object)null)
				{
					return false;
				}
				if (mSupportScrollBar)
				{
					int itemIndex2 = index2;
					rect = newItemByIndex4.CachedRectTransform.rect;
					SetItemSize(itemIndex2, ((Rect)(ref rect)).width, newItemByIndex4.Padding);
				}
				mItemList.Add(newItemByIndex4);
				newItemByIndex4.CachedRectTransform.anchoredPosition3D = new Vector3(itemPos2, newItemByIndex4.StartPosOffset, 0f);
				UpdateContentSize();
				return true;
			}
			LoopListViewItem2 loopListViewItem3 = mItemList[0];
			loopListViewItem3.CachedRectTransform.GetWorldCorners(mItemWorldCorners);
			Vector3 val5 = ((Transform)mViewPortRectTransform).InverseTransformPoint(mItemWorldCorners[1]);
			Vector3 val6 = ((Transform)mViewPortRectTransform).InverseTransformPoint(mItemWorldCorners[2]);
			if (!mIsDraging && loopListViewItem3.ItemCreatedCheckFrameCount != mListUpdateCheckFrameCount && val5.x - mViewPortRectLocalCorners[2].x > distanceForRecycle0)
			{
				mItemList.RemoveAt(0);
				RecycleItemTmp(loopListViewItem3);
				if (!mSupportScrollBar)
				{
					UpdateContentSize();
					CheckIfNeedUpdataItemPos();
				}
				return true;
			}
			LoopListViewItem2 loopListViewItem4 = mItemList[mItemList.Count - 1];
			loopListViewItem4.CachedRectTransform.GetWorldCorners(mItemWorldCorners);
			Vector3 val7 = ((Transform)mViewPortRectTransform).InverseTransformPoint(mItemWorldCorners[1]);
			Vector3 val8 = ((Transform)mViewPortRectTransform).InverseTransformPoint(mItemWorldCorners[2]);
			if (!mIsDraging && loopListViewItem4.ItemCreatedCheckFrameCount != mListUpdateCheckFrameCount && mViewPortRectLocalCorners[1].x - val8.x > distanceForRecycle1)
			{
				mItemList.RemoveAt(mItemList.Count - 1);
				RecycleItemTmp(loopListViewItem4);
				if (!mSupportScrollBar)
				{
					UpdateContentSize();
					CheckIfNeedUpdataItemPos();
				}
				return true;
			}
			if (mViewPortRectLocalCorners[1].x - val7.x < distanceForNew1)
			{
				if (loopListViewItem4.ItemIndex > mCurReadyMaxItemIndex)
				{
					mCurReadyMaxItemIndex = loopListViewItem4.ItemIndex;
					mNeedCheckNextMaxItem = true;
				}
				int num7 = loopListViewItem4.ItemIndex + 1;
				if (num7 <= mCurReadyMaxItemIndex || mNeedCheckNextMaxItem)
				{
					LoopListViewItem2 newItemByIndex5 = GetNewItemByIndex(num7);
					if (!((Object)(object)newItemByIndex5 == (Object)null))
					{
						if (mSupportScrollBar)
						{
							rect = newItemByIndex5.CachedRectTransform.rect;
							SetItemSize(num7, ((Rect)(ref rect)).width, newItemByIndex5.Padding);
						}
						mItemList.Add(newItemByIndex5);
						float x3 = loopListViewItem4.CachedRectTransform.anchoredPosition3D.x;
						rect = loopListViewItem4.CachedRectTransform.rect;
						float num8 = x3 - ((Rect)(ref rect)).width - loopListViewItem4.Padding;
						newItemByIndex5.CachedRectTransform.anchoredPosition3D = new Vector3(num8, newItemByIndex5.StartPosOffset, 0f);
						UpdateContentSize();
						CheckIfNeedUpdataItemPos();
						if (num7 > mCurReadyMaxItemIndex)
						{
							mCurReadyMaxItemIndex = num7;
						}
						return true;
					}
					mCurReadyMaxItemIndex = loopListViewItem4.ItemIndex;
					mNeedCheckNextMaxItem = false;
					CheckIfNeedUpdataItemPos();
				}
			}
			if (val6.x - mViewPortRectLocalCorners[2].x < distanceForNew0)
			{
				if (loopListViewItem3.ItemIndex < mCurReadyMinItemIndex)
				{
					mCurReadyMinItemIndex = loopListViewItem3.ItemIndex;
					mNeedCheckNextMinItem = true;
				}
				int num9 = loopListViewItem3.ItemIndex - 1;
				if (num9 >= mCurReadyMinItemIndex || mNeedCheckNextMinItem)
				{
					LoopListViewItem2 newItemByIndex6 = GetNewItemByIndex(num9);
					if (!((Object)(object)newItemByIndex6 == (Object)null))
					{
						if (mSupportScrollBar)
						{
							rect = newItemByIndex6.CachedRectTransform.rect;
							SetItemSize(num9, ((Rect)(ref rect)).width, newItemByIndex6.Padding);
						}
						mItemList.Insert(0, newItemByIndex6);
						float x4 = loopListViewItem3.CachedRectTransform.anchoredPosition3D.x;
						rect = newItemByIndex6.CachedRectTransform.rect;
						float num10 = x4 + ((Rect)(ref rect)).width + newItemByIndex6.Padding;
						newItemByIndex6.CachedRectTransform.anchoredPosition3D = new Vector3(num10, newItemByIndex6.StartPosOffset, 0f);
						UpdateContentSize();
						CheckIfNeedUpdataItemPos();
						if (num9 < mCurReadyMinItemIndex)
						{
							mCurReadyMinItemIndex = num9;
						}
						return true;
					}
					mCurReadyMinItemIndex = loopListViewItem3.ItemIndex;
					mNeedCheckNextMinItem = false;
				}
			}
		}
		return false;
	}

	private float GetContentPanelSize()
	{
		if (mSupportScrollBar)
		{
			float num = ((mItemPosMgr.mTotalSize > 0f) ? (mItemPosMgr.mTotalSize - mLastItemPadding) : 0f);
			if (num < 0f)
			{
				num = 0f;
			}
			return num;
		}
		int count = mItemList.Count;
		switch (count)
		{
		case 0:
			return 0f;
		case 1:
			return mItemList[0].ItemSize;
		case 2:
			return mItemList[0].ItemSizeWithPadding + mItemList[1].ItemSize;
		default:
		{
			float num2 = 0f;
			for (int i = 0; i < count - 1; i++)
			{
				num2 += mItemList[i].ItemSizeWithPadding;
			}
			return num2 + mItemList[count - 1].ItemSize;
		}
		}
	}

	private void CheckIfNeedUpdataItemPos()
	{
		if (mItemList.Count == 0)
		{
			return;
		}
		if (mArrangeType == ListItemArrangeType.TopToBottom)
		{
			LoopListViewItem2 loopListViewItem = mItemList[0];
			LoopListViewItem2 loopListViewItem2 = mItemList[mItemList.Count - 1];
			float contentPanelSize = GetContentPanelSize();
			if (loopListViewItem.TopY > 0f || (loopListViewItem.ItemIndex == mCurReadyMinItemIndex && loopListViewItem.TopY != 0f))
			{
				UpdateAllShownItemsPos();
			}
			else if (0f - loopListViewItem2.BottomY > contentPanelSize || (loopListViewItem2.ItemIndex == mCurReadyMaxItemIndex && 0f - loopListViewItem2.BottomY != contentPanelSize))
			{
				UpdateAllShownItemsPos();
			}
		}
		else if (mArrangeType == ListItemArrangeType.BottomToTop)
		{
			LoopListViewItem2 loopListViewItem3 = mItemList[0];
			LoopListViewItem2 loopListViewItem4 = mItemList[mItemList.Count - 1];
			float contentPanelSize2 = GetContentPanelSize();
			if (loopListViewItem3.BottomY < 0f || (loopListViewItem3.ItemIndex == mCurReadyMinItemIndex && loopListViewItem3.BottomY != 0f))
			{
				UpdateAllShownItemsPos();
			}
			else if (loopListViewItem4.TopY > contentPanelSize2 || (loopListViewItem4.ItemIndex == mCurReadyMaxItemIndex && loopListViewItem4.TopY != contentPanelSize2))
			{
				UpdateAllShownItemsPos();
			}
		}
		else if (mArrangeType == ListItemArrangeType.LeftToRight)
		{
			LoopListViewItem2 loopListViewItem5 = mItemList[0];
			LoopListViewItem2 loopListViewItem6 = mItemList[mItemList.Count - 1];
			float contentPanelSize3 = GetContentPanelSize();
			if (loopListViewItem5.LeftX < 0f || (loopListViewItem5.ItemIndex == mCurReadyMinItemIndex && loopListViewItem5.LeftX != 0f))
			{
				UpdateAllShownItemsPos();
			}
			else if (loopListViewItem6.RightX > contentPanelSize3 || (loopListViewItem6.ItemIndex == mCurReadyMaxItemIndex && loopListViewItem6.RightX != contentPanelSize3))
			{
				UpdateAllShownItemsPos();
			}
		}
		else if (mArrangeType == ListItemArrangeType.RightToLeft)
		{
			LoopListViewItem2 loopListViewItem7 = mItemList[0];
			LoopListViewItem2 loopListViewItem8 = mItemList[mItemList.Count - 1];
			float contentPanelSize4 = GetContentPanelSize();
			if (loopListViewItem7.RightX > 0f || (loopListViewItem7.ItemIndex == mCurReadyMinItemIndex && loopListViewItem7.RightX != 0f))
			{
				UpdateAllShownItemsPos();
			}
			else if (0f - loopListViewItem8.LeftX > contentPanelSize4 || (loopListViewItem8.ItemIndex == mCurReadyMaxItemIndex && 0f - loopListViewItem8.LeftX != contentPanelSize4))
			{
				UpdateAllShownItemsPos();
			}
		}
	}

	private void UpdateAllShownItemsPos()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0441: Unknown result type (might be due to invalid IL or missing references)
		//IL_036a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_040e: Unknown result type (might be due to invalid IL or missing references)
		int count = mItemList.Count;
		if (count == 0)
		{
			return;
		}
		mAdjustedVec = Vector2.op_Implicit((mContainerTrans.anchoredPosition3D - mLastFrameContainerPos) / Time.deltaTime);
		Rect rect;
		if (mArrangeType == ListItemArrangeType.TopToBottom)
		{
			float num = 0f;
			if (mSupportScrollBar)
			{
				num = 0f - GetItemPos(mItemList[0].ItemIndex);
			}
			float y = mItemList[0].CachedRectTransform.anchoredPosition3D.y;
			float num2 = num - y;
			float num3 = num;
			for (int i = 0; i < count; i++)
			{
				LoopListViewItem2 loopListViewItem = mItemList[i];
				loopListViewItem.CachedRectTransform.anchoredPosition3D = new Vector3(loopListViewItem.StartPosOffset, num3, 0f);
				float num4 = num3;
				rect = loopListViewItem.CachedRectTransform.rect;
				num3 = num4 - ((Rect)(ref rect)).height - loopListViewItem.Padding;
			}
			if (num2 != 0f)
			{
				Vector2 val = Vector2.op_Implicit(mContainerTrans.anchoredPosition3D);
				val.y -= num2;
				mContainerTrans.anchoredPosition3D = Vector2.op_Implicit(val);
			}
		}
		else if (mArrangeType == ListItemArrangeType.BottomToTop)
		{
			float num5 = 0f;
			if (mSupportScrollBar)
			{
				num5 = GetItemPos(mItemList[0].ItemIndex);
			}
			float y2 = mItemList[0].CachedRectTransform.anchoredPosition3D.y;
			float num6 = num5 - y2;
			float num7 = num5;
			for (int j = 0; j < count; j++)
			{
				LoopListViewItem2 loopListViewItem2 = mItemList[j];
				loopListViewItem2.CachedRectTransform.anchoredPosition3D = new Vector3(loopListViewItem2.StartPosOffset, num7, 0f);
				float num8 = num7;
				rect = loopListViewItem2.CachedRectTransform.rect;
				num7 = num8 + ((Rect)(ref rect)).height + loopListViewItem2.Padding;
			}
			if (num6 != 0f)
			{
				Vector3 anchoredPosition3D = mContainerTrans.anchoredPosition3D;
				anchoredPosition3D.y -= num6;
				mContainerTrans.anchoredPosition3D = anchoredPosition3D;
			}
		}
		else if (mArrangeType == ListItemArrangeType.LeftToRight)
		{
			float num9 = 0f;
			if (mSupportScrollBar)
			{
				num9 = GetItemPos(mItemList[0].ItemIndex);
			}
			float x = mItemList[0].CachedRectTransform.anchoredPosition3D.x;
			float num10 = num9 - x;
			float num11 = num9;
			for (int k = 0; k < count; k++)
			{
				LoopListViewItem2 loopListViewItem3 = mItemList[k];
				loopListViewItem3.CachedRectTransform.anchoredPosition3D = new Vector3(num11, loopListViewItem3.StartPosOffset, 0f);
				float num12 = num11;
				rect = loopListViewItem3.CachedRectTransform.rect;
				num11 = num12 + ((Rect)(ref rect)).width + loopListViewItem3.Padding;
			}
			if (num10 != 0f)
			{
				Vector3 anchoredPosition3D2 = mContainerTrans.anchoredPosition3D;
				anchoredPosition3D2.x -= num10;
				mContainerTrans.anchoredPosition3D = anchoredPosition3D2;
			}
		}
		else if (mArrangeType == ListItemArrangeType.RightToLeft)
		{
			float num13 = 0f;
			if (mSupportScrollBar)
			{
				num13 = 0f - GetItemPos(mItemList[0].ItemIndex);
			}
			float x2 = mItemList[0].CachedRectTransform.anchoredPosition3D.x;
			float num14 = num13 - x2;
			float num15 = num13;
			for (int l = 0; l < count; l++)
			{
				LoopListViewItem2 loopListViewItem4 = mItemList[l];
				loopListViewItem4.CachedRectTransform.anchoredPosition3D = new Vector3(num15, loopListViewItem4.StartPosOffset, 0f);
				float num16 = num15;
				rect = loopListViewItem4.CachedRectTransform.rect;
				num15 = num16 - ((Rect)(ref rect)).width - loopListViewItem4.Padding;
			}
			if (num14 != 0f)
			{
				Vector3 anchoredPosition3D3 = mContainerTrans.anchoredPosition3D;
				anchoredPosition3D3.x -= num14;
				mContainerTrans.anchoredPosition3D = anchoredPosition3D3;
			}
		}
		if (mIsDraging)
		{
			mScrollRect.OnBeginDrag(mPointerEventData);
			mScrollRect.Rebuild((CanvasUpdate)2);
			mScrollRect.velocity = mAdjustedVec;
			mNeedAdjustVec = true;
		}
	}

	private void UpdateContentSize()
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		float contentPanelSize = GetContentPanelSize();
		Rect rect;
		if (mIsVertList)
		{
			rect = mContainerTrans.rect;
			if (((Rect)(ref rect)).height != contentPanelSize)
			{
				mContainerTrans.SetSizeWithCurrentAnchors((Axis)1, contentPanelSize);
			}
		}
		else
		{
			rect = mContainerTrans.rect;
			if (((Rect)(ref rect)).width != contentPanelSize)
			{
				mContainerTrans.SetSizeWithCurrentAnchors((Axis)0, contentPanelSize);
			}
		}
	}
}
