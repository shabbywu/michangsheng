using UnityEngine;

namespace SuperScrollView;

public class LoopListViewItem2 : MonoBehaviour
{
	private int mItemIndex = -1;

	private int mItemId = -1;

	private LoopListView2 mParentListView;

	private bool mIsInitHandlerCalled;

	private string mItemPrefabName;

	private RectTransform mCachedRectTransform;

	private float mPadding;

	private float mDistanceWithViewPortSnapCenter;

	private int mItemCreatedCheckFrameCount;

	private float mStartPosOffset;

	private object mUserObjectData;

	private int mUserIntData1;

	private int mUserIntData2;

	private string mUserStringData1;

	private string mUserStringData2;

	public object UserObjectData
	{
		get
		{
			return mUserObjectData;
		}
		set
		{
			mUserObjectData = value;
		}
	}

	public int UserIntData1
	{
		get
		{
			return mUserIntData1;
		}
		set
		{
			mUserIntData1 = value;
		}
	}

	public int UserIntData2
	{
		get
		{
			return mUserIntData2;
		}
		set
		{
			mUserIntData2 = value;
		}
	}

	public string UserStringData1
	{
		get
		{
			return mUserStringData1;
		}
		set
		{
			mUserStringData1 = value;
		}
	}

	public string UserStringData2
	{
		get
		{
			return mUserStringData2;
		}
		set
		{
			mUserStringData2 = value;
		}
	}

	public float DistanceWithViewPortSnapCenter
	{
		get
		{
			return mDistanceWithViewPortSnapCenter;
		}
		set
		{
			mDistanceWithViewPortSnapCenter = value;
		}
	}

	public float StartPosOffset
	{
		get
		{
			return mStartPosOffset;
		}
		set
		{
			mStartPosOffset = value;
		}
	}

	public int ItemCreatedCheckFrameCount
	{
		get
		{
			return mItemCreatedCheckFrameCount;
		}
		set
		{
			mItemCreatedCheckFrameCount = value;
		}
	}

	public float Padding
	{
		get
		{
			return mPadding;
		}
		set
		{
			mPadding = value;
		}
	}

	public RectTransform CachedRectTransform
	{
		get
		{
			if ((Object)(object)mCachedRectTransform == (Object)null)
			{
				mCachedRectTransform = ((Component)this).gameObject.GetComponent<RectTransform>();
			}
			return mCachedRectTransform;
		}
	}

	public string ItemPrefabName
	{
		get
		{
			return mItemPrefabName;
		}
		set
		{
			mItemPrefabName = value;
		}
	}

	public int ItemIndex
	{
		get
		{
			return mItemIndex;
		}
		set
		{
			mItemIndex = value;
		}
	}

	public int ItemId
	{
		get
		{
			return mItemId;
		}
		set
		{
			mItemId = value;
		}
	}

	public bool IsInitHandlerCalled
	{
		get
		{
			return mIsInitHandlerCalled;
		}
		set
		{
			mIsInitHandlerCalled = value;
		}
	}

	public LoopListView2 ParentListView
	{
		get
		{
			return mParentListView;
		}
		set
		{
			mParentListView = value;
		}
	}

	public float TopY
	{
		get
		{
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_003a: Unknown result type (might be due to invalid IL or missing references)
			//IL_003f: Unknown result type (might be due to invalid IL or missing references)
			switch (ParentListView.ArrangeType)
			{
			case ListItemArrangeType.TopToBottom:
				return CachedRectTransform.anchoredPosition3D.y;
			case ListItemArrangeType.BottomToTop:
			{
				float y = CachedRectTransform.anchoredPosition3D.y;
				Rect rect = CachedRectTransform.rect;
				return y + ((Rect)(ref rect)).height;
			}
			default:
				return 0f;
			}
		}
	}

	public float BottomY
	{
		get
		{
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_003e: Unknown result type (might be due to invalid IL or missing references)
			switch (ParentListView.ArrangeType)
			{
			case ListItemArrangeType.TopToBottom:
			{
				float y = CachedRectTransform.anchoredPosition3D.y;
				Rect rect = CachedRectTransform.rect;
				return y - ((Rect)(ref rect)).height;
			}
			case ListItemArrangeType.BottomToTop:
				return CachedRectTransform.anchoredPosition3D.y;
			default:
				return 0f;
			}
		}
	}

	public float LeftX
	{
		get
		{
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_003b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0040: Unknown result type (might be due to invalid IL or missing references)
			switch (ParentListView.ArrangeType)
			{
			case ListItemArrangeType.LeftToRight:
				return CachedRectTransform.anchoredPosition3D.x;
			case ListItemArrangeType.RightToLeft:
			{
				float x = CachedRectTransform.anchoredPosition3D.x;
				Rect rect = CachedRectTransform.rect;
				return x - ((Rect)(ref rect)).width;
			}
			default:
				return 0f;
			}
		}
	}

	public float RightX
	{
		get
		{
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_003f: Unknown result type (might be due to invalid IL or missing references)
			switch (ParentListView.ArrangeType)
			{
			case ListItemArrangeType.LeftToRight:
			{
				float x = CachedRectTransform.anchoredPosition3D.x;
				Rect rect = CachedRectTransform.rect;
				return x + ((Rect)(ref rect)).width;
			}
			case ListItemArrangeType.RightToLeft:
				return CachedRectTransform.anchoredPosition3D.x;
			default:
				return 0f;
			}
		}
	}

	public float ItemSize
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			Rect rect;
			if (ParentListView.IsVertList)
			{
				rect = CachedRectTransform.rect;
				return ((Rect)(ref rect)).height;
			}
			rect = CachedRectTransform.rect;
			return ((Rect)(ref rect)).width;
		}
	}

	public float ItemSizeWithPadding => ItemSize + mPadding;
}
