using System;
using UnityEngine;

namespace SuperScrollView
{
	// Token: 0x020006CF RID: 1743
	public class LoopListViewItem2 : MonoBehaviour
	{
		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x060037A8 RID: 14248 RVA: 0x0017E994 File Offset: 0x0017CB94
		// (set) Token: 0x060037A9 RID: 14249 RVA: 0x0017E99C File Offset: 0x0017CB9C
		public object UserObjectData
		{
			get
			{
				return this.mUserObjectData;
			}
			set
			{
				this.mUserObjectData = value;
			}
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x060037AA RID: 14250 RVA: 0x0017E9A5 File Offset: 0x0017CBA5
		// (set) Token: 0x060037AB RID: 14251 RVA: 0x0017E9AD File Offset: 0x0017CBAD
		public int UserIntData1
		{
			get
			{
				return this.mUserIntData1;
			}
			set
			{
				this.mUserIntData1 = value;
			}
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x060037AC RID: 14252 RVA: 0x0017E9B6 File Offset: 0x0017CBB6
		// (set) Token: 0x060037AD RID: 14253 RVA: 0x0017E9BE File Offset: 0x0017CBBE
		public int UserIntData2
		{
			get
			{
				return this.mUserIntData2;
			}
			set
			{
				this.mUserIntData2 = value;
			}
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x060037AE RID: 14254 RVA: 0x0017E9C7 File Offset: 0x0017CBC7
		// (set) Token: 0x060037AF RID: 14255 RVA: 0x0017E9CF File Offset: 0x0017CBCF
		public string UserStringData1
		{
			get
			{
				return this.mUserStringData1;
			}
			set
			{
				this.mUserStringData1 = value;
			}
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x060037B0 RID: 14256 RVA: 0x0017E9D8 File Offset: 0x0017CBD8
		// (set) Token: 0x060037B1 RID: 14257 RVA: 0x0017E9E0 File Offset: 0x0017CBE0
		public string UserStringData2
		{
			get
			{
				return this.mUserStringData2;
			}
			set
			{
				this.mUserStringData2 = value;
			}
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x060037B2 RID: 14258 RVA: 0x0017E9E9 File Offset: 0x0017CBE9
		// (set) Token: 0x060037B3 RID: 14259 RVA: 0x0017E9F1 File Offset: 0x0017CBF1
		public float DistanceWithViewPortSnapCenter
		{
			get
			{
				return this.mDistanceWithViewPortSnapCenter;
			}
			set
			{
				this.mDistanceWithViewPortSnapCenter = value;
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x060037B4 RID: 14260 RVA: 0x0017E9FA File Offset: 0x0017CBFA
		// (set) Token: 0x060037B5 RID: 14261 RVA: 0x0017EA02 File Offset: 0x0017CC02
		public float StartPosOffset
		{
			get
			{
				return this.mStartPosOffset;
			}
			set
			{
				this.mStartPosOffset = value;
			}
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x060037B6 RID: 14262 RVA: 0x0017EA0B File Offset: 0x0017CC0B
		// (set) Token: 0x060037B7 RID: 14263 RVA: 0x0017EA13 File Offset: 0x0017CC13
		public int ItemCreatedCheckFrameCount
		{
			get
			{
				return this.mItemCreatedCheckFrameCount;
			}
			set
			{
				this.mItemCreatedCheckFrameCount = value;
			}
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x060037B8 RID: 14264 RVA: 0x0017EA1C File Offset: 0x0017CC1C
		// (set) Token: 0x060037B9 RID: 14265 RVA: 0x0017EA24 File Offset: 0x0017CC24
		public float Padding
		{
			get
			{
				return this.mPadding;
			}
			set
			{
				this.mPadding = value;
			}
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x060037BA RID: 14266 RVA: 0x0017EA2D File Offset: 0x0017CC2D
		public RectTransform CachedRectTransform
		{
			get
			{
				if (this.mCachedRectTransform == null)
				{
					this.mCachedRectTransform = base.gameObject.GetComponent<RectTransform>();
				}
				return this.mCachedRectTransform;
			}
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x060037BB RID: 14267 RVA: 0x0017EA54 File Offset: 0x0017CC54
		// (set) Token: 0x060037BC RID: 14268 RVA: 0x0017EA5C File Offset: 0x0017CC5C
		public string ItemPrefabName
		{
			get
			{
				return this.mItemPrefabName;
			}
			set
			{
				this.mItemPrefabName = value;
			}
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x060037BD RID: 14269 RVA: 0x0017EA65 File Offset: 0x0017CC65
		// (set) Token: 0x060037BE RID: 14270 RVA: 0x0017EA6D File Offset: 0x0017CC6D
		public int ItemIndex
		{
			get
			{
				return this.mItemIndex;
			}
			set
			{
				this.mItemIndex = value;
			}
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x060037BF RID: 14271 RVA: 0x0017EA76 File Offset: 0x0017CC76
		// (set) Token: 0x060037C0 RID: 14272 RVA: 0x0017EA7E File Offset: 0x0017CC7E
		public int ItemId
		{
			get
			{
				return this.mItemId;
			}
			set
			{
				this.mItemId = value;
			}
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x060037C1 RID: 14273 RVA: 0x0017EA87 File Offset: 0x0017CC87
		// (set) Token: 0x060037C2 RID: 14274 RVA: 0x0017EA8F File Offset: 0x0017CC8F
		public bool IsInitHandlerCalled
		{
			get
			{
				return this.mIsInitHandlerCalled;
			}
			set
			{
				this.mIsInitHandlerCalled = value;
			}
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x060037C3 RID: 14275 RVA: 0x0017EA98 File Offset: 0x0017CC98
		// (set) Token: 0x060037C4 RID: 14276 RVA: 0x0017EAA0 File Offset: 0x0017CCA0
		public LoopListView2 ParentListView
		{
			get
			{
				return this.mParentListView;
			}
			set
			{
				this.mParentListView = value;
			}
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x060037C5 RID: 14277 RVA: 0x0017EAAC File Offset: 0x0017CCAC
		public float TopY
		{
			get
			{
				ListItemArrangeType arrangeType = this.ParentListView.ArrangeType;
				if (arrangeType == ListItemArrangeType.TopToBottom)
				{
					return this.CachedRectTransform.anchoredPosition3D.y;
				}
				if (arrangeType == ListItemArrangeType.BottomToTop)
				{
					return this.CachedRectTransform.anchoredPosition3D.y + this.CachedRectTransform.rect.height;
				}
				return 0f;
			}
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x060037C6 RID: 14278 RVA: 0x0017EB08 File Offset: 0x0017CD08
		public float BottomY
		{
			get
			{
				ListItemArrangeType arrangeType = this.ParentListView.ArrangeType;
				if (arrangeType == ListItemArrangeType.TopToBottom)
				{
					return this.CachedRectTransform.anchoredPosition3D.y - this.CachedRectTransform.rect.height;
				}
				if (arrangeType == ListItemArrangeType.BottomToTop)
				{
					return this.CachedRectTransform.anchoredPosition3D.y;
				}
				return 0f;
			}
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x060037C7 RID: 14279 RVA: 0x0017EB64 File Offset: 0x0017CD64
		public float LeftX
		{
			get
			{
				ListItemArrangeType arrangeType = this.ParentListView.ArrangeType;
				if (arrangeType == ListItemArrangeType.LeftToRight)
				{
					return this.CachedRectTransform.anchoredPosition3D.x;
				}
				if (arrangeType == ListItemArrangeType.RightToLeft)
				{
					return this.CachedRectTransform.anchoredPosition3D.x - this.CachedRectTransform.rect.width;
				}
				return 0f;
			}
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x060037C8 RID: 14280 RVA: 0x0017EBC0 File Offset: 0x0017CDC0
		public float RightX
		{
			get
			{
				ListItemArrangeType arrangeType = this.ParentListView.ArrangeType;
				if (arrangeType == ListItemArrangeType.LeftToRight)
				{
					return this.CachedRectTransform.anchoredPosition3D.x + this.CachedRectTransform.rect.width;
				}
				if (arrangeType == ListItemArrangeType.RightToLeft)
				{
					return this.CachedRectTransform.anchoredPosition3D.x;
				}
				return 0f;
			}
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x060037C9 RID: 14281 RVA: 0x0017EC1C File Offset: 0x0017CE1C
		public float ItemSize
		{
			get
			{
				if (this.ParentListView.IsVertList)
				{
					return this.CachedRectTransform.rect.height;
				}
				return this.CachedRectTransform.rect.width;
			}
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x060037CA RID: 14282 RVA: 0x0017EC5D File Offset: 0x0017CE5D
		public float ItemSizeWithPadding
		{
			get
			{
				return this.ItemSize + this.mPadding;
			}
		}

		// Token: 0x0400305D RID: 12381
		private int mItemIndex = -1;

		// Token: 0x0400305E RID: 12382
		private int mItemId = -1;

		// Token: 0x0400305F RID: 12383
		private LoopListView2 mParentListView;

		// Token: 0x04003060 RID: 12384
		private bool mIsInitHandlerCalled;

		// Token: 0x04003061 RID: 12385
		private string mItemPrefabName;

		// Token: 0x04003062 RID: 12386
		private RectTransform mCachedRectTransform;

		// Token: 0x04003063 RID: 12387
		private float mPadding;

		// Token: 0x04003064 RID: 12388
		private float mDistanceWithViewPortSnapCenter;

		// Token: 0x04003065 RID: 12389
		private int mItemCreatedCheckFrameCount;

		// Token: 0x04003066 RID: 12390
		private float mStartPosOffset;

		// Token: 0x04003067 RID: 12391
		private object mUserObjectData;

		// Token: 0x04003068 RID: 12392
		private int mUserIntData1;

		// Token: 0x04003069 RID: 12393
		private int mUserIntData2;

		// Token: 0x0400306A RID: 12394
		private string mUserStringData1;

		// Token: 0x0400306B RID: 12395
		private string mUserStringData2;
	}
}
