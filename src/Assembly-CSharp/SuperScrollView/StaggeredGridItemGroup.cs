using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SuperScrollView
{
	// Token: 0x02000A04 RID: 2564
	public class StaggeredGridItemGroup
	{
		// Token: 0x06004252 RID: 16978 RVA: 0x001C7F74 File Offset: 0x001C6174
		public void Init(LoopStaggeredGridView parent, int itemTotalCount, int groupIndex, Func<int, int, LoopStaggeredGridViewItem> onGetItemByIndex)
		{
			this.mGroupIndex = groupIndex;
			this.mParentGridView = parent;
			this.mArrangeType = this.mParentGridView.ArrangeType;
			this.mGameObject = this.mParentGridView.gameObject;
			this.mScrollRect = this.mGameObject.GetComponent<ScrollRect>();
			this.mItemPosMgr = new ItemPosMgr(this.mItemDefaultWithPaddingSize);
			this.mScrollRectTransform = this.mScrollRect.GetComponent<RectTransform>();
			this.mContainerTrans = this.mScrollRect.content;
			this.mViewPortRectTransform = this.mScrollRect.viewport;
			if (this.mViewPortRectTransform == null)
			{
				this.mViewPortRectTransform = this.mScrollRectTransform;
			}
			this.mIsVertList = (this.mArrangeType == ListItemArrangeType.TopToBottom || this.mArrangeType == ListItemArrangeType.BottomToTop);
			this.mOnGetItemByIndex = onGetItemByIndex;
			this.mItemTotalCount = itemTotalCount;
			this.mViewPortRectTransform.GetLocalCorners(this.mViewPortRectLocalCorners);
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
			this.mNeedCheckNextMaxItem = true;
			this.mNeedCheckNextMinItem = true;
		}

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x06004253 RID: 16979 RVA: 0x0002F55F File Offset: 0x0002D75F
		public List<int> ItemIndexMap
		{
			get
			{
				return this.mItemIndexMap;
			}
		}

		// Token: 0x06004254 RID: 16980 RVA: 0x0002F567 File Offset: 0x0002D767
		public void ResetListView()
		{
			this.mViewPortRectTransform.GetLocalCorners(this.mViewPortRectLocalCorners);
		}

		// Token: 0x06004255 RID: 16981 RVA: 0x001C80B0 File Offset: 0x001C62B0
		public LoopStaggeredGridViewItem GetShownItemByItemIndex(int itemIndex)
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
			for (int i = 0; i < count; i++)
			{
				LoopStaggeredGridViewItem loopStaggeredGridViewItem = this.mItemList[i];
				if (loopStaggeredGridViewItem.ItemIndex == itemIndex)
				{
					return loopStaggeredGridViewItem;
				}
			}
			return null;
		}

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x06004256 RID: 16982 RVA: 0x001C8120 File Offset: 0x001C6320
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

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x06004257 RID: 16983 RVA: 0x001C815C File Offset: 0x001C635C
		public float ViewPortWidth
		{
			get
			{
				return this.mViewPortRectTransform.rect.width;
			}
		}

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x06004258 RID: 16984 RVA: 0x001C817C File Offset: 0x001C637C
		public float ViewPortHeight
		{
			get
			{
				return this.mViewPortRectTransform.rect.height;
			}
		}

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x06004259 RID: 16985 RVA: 0x0002F57A File Offset: 0x0002D77A
		private bool IsDraging
		{
			get
			{
				return this.mParentGridView.IsDraging;
			}
		}

		// Token: 0x0600425A RID: 16986 RVA: 0x001C819C File Offset: 0x001C639C
		public LoopStaggeredGridViewItem GetShownItemByIndexInGroup(int indexInGroup)
		{
			int count = this.mItemList.Count;
			if (count == 0)
			{
				return null;
			}
			if (indexInGroup < this.mItemList[0].ItemIndexInGroup || indexInGroup > this.mItemList[count - 1].ItemIndexInGroup)
			{
				return null;
			}
			int index = indexInGroup - this.mItemList[0].ItemIndexInGroup;
			return this.mItemList[index];
		}

		// Token: 0x0600425B RID: 16987 RVA: 0x001C8208 File Offset: 0x001C6408
		public int GetIndexInShownItemList(LoopStaggeredGridViewItem item)
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

		// Token: 0x0600425C RID: 16988 RVA: 0x0002F587 File Offset: 0x0002D787
		public void RefreshAllShownItem()
		{
			if (this.mItemList.Count == 0)
			{
				return;
			}
			this.RefreshAllShownItemWithFirstIndexInGroup(this.mItemList[0].ItemIndexInGroup);
		}

		// Token: 0x0600425D RID: 16989 RVA: 0x001C8254 File Offset: 0x001C6454
		public void OnItemSizeChanged(int indexInGroup)
		{
			LoopStaggeredGridViewItem shownItemByIndexInGroup = this.GetShownItemByIndexInGroup(indexInGroup);
			if (shownItemByIndexInGroup == null)
			{
				return;
			}
			if (this.mSupportScrollBar)
			{
				if (this.mIsVertList)
				{
					this.SetItemSize(indexInGroup, shownItemByIndexInGroup.CachedRectTransform.rect.height, shownItemByIndexInGroup.Padding);
				}
				else
				{
					this.SetItemSize(indexInGroup, shownItemByIndexInGroup.CachedRectTransform.rect.width, shownItemByIndexInGroup.Padding);
				}
			}
			this.UpdateAllShownItemsPos();
		}

		// Token: 0x0600425E RID: 16990 RVA: 0x001C82CC File Offset: 0x001C64CC
		public void RefreshItemByIndexInGroup(int indexInGroup)
		{
			int count = this.mItemList.Count;
			if (count == 0)
			{
				return;
			}
			if (indexInGroup < this.mItemList[0].ItemIndexInGroup || indexInGroup > this.mItemList[count - 1].ItemIndexInGroup)
			{
				return;
			}
			int itemIndexInGroup = this.mItemList[0].ItemIndexInGroup;
			int index = indexInGroup - itemIndexInGroup;
			LoopStaggeredGridViewItem loopStaggeredGridViewItem = this.mItemList[index];
			Vector3 anchoredPosition3D = loopStaggeredGridViewItem.CachedRectTransform.anchoredPosition3D;
			this.RecycleItemTmp(loopStaggeredGridViewItem);
			LoopStaggeredGridViewItem newItemByIndexInGroup = this.GetNewItemByIndexInGroup(indexInGroup);
			if (newItemByIndexInGroup == null)
			{
				this.RefreshAllShownItemWithFirstIndexInGroup(itemIndexInGroup);
				return;
			}
			this.mItemList[index] = newItemByIndexInGroup;
			if (this.mIsVertList)
			{
				anchoredPosition3D.x = newItemByIndexInGroup.StartPosOffset;
			}
			else
			{
				anchoredPosition3D.y = newItemByIndexInGroup.StartPosOffset;
			}
			newItemByIndexInGroup.CachedRectTransform.anchoredPosition3D = anchoredPosition3D;
			this.OnItemSizeChanged(indexInGroup);
			this.ClearAllTmpRecycledItem();
		}

		// Token: 0x0600425F RID: 16991 RVA: 0x001C83B8 File Offset: 0x001C65B8
		public void RefreshAllShownItemWithFirstIndexInGroup(int firstItemIndexInGroup)
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
				int num = firstItemIndexInGroup + i;
				LoopStaggeredGridViewItem newItemByIndexInGroup = this.GetNewItemByIndexInGroup(num);
				if (newItemByIndexInGroup == null)
				{
					break;
				}
				if (this.mIsVertList)
				{
					anchoredPosition3D.x = newItemByIndexInGroup.StartPosOffset;
				}
				else
				{
					anchoredPosition3D.y = newItemByIndexInGroup.StartPosOffset;
				}
				newItemByIndexInGroup.CachedRectTransform.anchoredPosition3D = anchoredPosition3D;
				if (this.mSupportScrollBar)
				{
					if (this.mIsVertList)
					{
						this.SetItemSize(num, newItemByIndexInGroup.CachedRectTransform.rect.height, newItemByIndexInGroup.Padding);
					}
					else
					{
						this.SetItemSize(num, newItemByIndexInGroup.CachedRectTransform.rect.width, newItemByIndexInGroup.Padding);
					}
				}
				this.mItemList.Add(newItemByIndexInGroup);
			}
			this.UpdateAllShownItemsPos();
			this.ClearAllTmpRecycledItem();
		}

		// Token: 0x06004260 RID: 16992 RVA: 0x001C84C4 File Offset: 0x001C66C4
		public void RefreshAllShownItemWithFirstIndexAndPos(int firstItemIndexInGroup, Vector3 pos)
		{
			this.RecycleAllItem();
			LoopStaggeredGridViewItem newItemByIndexInGroup = this.GetNewItemByIndexInGroup(firstItemIndexInGroup);
			if (newItemByIndexInGroup == null)
			{
				return;
			}
			if (this.mIsVertList)
			{
				pos.x = newItemByIndexInGroup.StartPosOffset;
			}
			else
			{
				pos.y = newItemByIndexInGroup.StartPosOffset;
			}
			newItemByIndexInGroup.CachedRectTransform.anchoredPosition3D = pos;
			if (this.mSupportScrollBar)
			{
				if (this.mIsVertList)
				{
					this.SetItemSize(firstItemIndexInGroup, newItemByIndexInGroup.CachedRectTransform.rect.height, newItemByIndexInGroup.Padding);
				}
				else
				{
					this.SetItemSize(firstItemIndexInGroup, newItemByIndexInGroup.CachedRectTransform.rect.width, newItemByIndexInGroup.Padding);
				}
			}
			this.mItemList.Add(newItemByIndexInGroup);
			this.UpdateAllShownItemsPos();
			this.mParentGridView.UpdateListViewWithDefault();
			this.ClearAllTmpRecycledItem();
		}

		// Token: 0x06004261 RID: 16993 RVA: 0x0002F5AE File Offset: 0x0002D7AE
		private void SetItemSize(int itemIndex, float itemSize, float padding)
		{
			this.mItemPosMgr.SetItemSize(itemIndex, itemSize + padding);
			if (itemIndex >= this.mLastItemIndex)
			{
				this.mLastItemIndex = itemIndex;
				this.mLastItemPadding = padding;
			}
		}

		// Token: 0x06004262 RID: 16994 RVA: 0x0002F5D6 File Offset: 0x0002D7D6
		private bool GetPlusItemIndexAndPosAtGivenPos(float pos, ref int index, ref float itemPos)
		{
			return this.mItemPosMgr.GetItemIndexAndPosAtGivenPos(pos, ref index, ref itemPos);
		}

		// Token: 0x06004263 RID: 16995 RVA: 0x0002F5E6 File Offset: 0x0002D7E6
		public float GetItemPos(int itemIndex)
		{
			return this.mItemPosMgr.GetItemPos(itemIndex);
		}

		// Token: 0x06004264 RID: 16996 RVA: 0x0002F5F4 File Offset: 0x0002D7F4
		public Vector3 GetItemCornerPosInViewPort(LoopStaggeredGridViewItem item, ItemCornerEnum corner = ItemCornerEnum.LeftBottom)
		{
			item.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
			return this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[(int)corner]);
		}

		// Token: 0x06004265 RID: 16997 RVA: 0x0002F61E File Offset: 0x0002D81E
		public void RecycleItemTmp(LoopStaggeredGridViewItem item)
		{
			this.mParentGridView.RecycleItemTmp(item);
		}

		// Token: 0x06004266 RID: 16998 RVA: 0x001C8590 File Offset: 0x001C6790
		public void RecycleAllItem()
		{
			foreach (LoopStaggeredGridViewItem item in this.mItemList)
			{
				this.RecycleItemTmp(item);
			}
			this.mItemList.Clear();
		}

		// Token: 0x06004267 RID: 16999 RVA: 0x0002F62C File Offset: 0x0002D82C
		public void ClearAllTmpRecycledItem()
		{
			this.mParentGridView.ClearAllTmpRecycledItem();
		}

		// Token: 0x06004268 RID: 17000 RVA: 0x0002F639 File Offset: 0x0002D839
		private LoopStaggeredGridViewItem GetNewItemByIndexInGroup(int indexInGroup)
		{
			return this.mParentGridView.GetNewItemByGroupAndIndex(this.mGroupIndex, indexInGroup);
		}

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x06004269 RID: 17001 RVA: 0x0002F64D File Offset: 0x0002D84D
		public int HadCreatedItemCount
		{
			get
			{
				return this.mItemIndexMap.Count;
			}
		}

		// Token: 0x0600426A RID: 17002 RVA: 0x001C85F0 File Offset: 0x001C67F0
		public void SetListItemCount(int itemCount)
		{
			if (itemCount == this.mItemTotalCount)
			{
				return;
			}
			int num = this.mItemTotalCount;
			this.mItemTotalCount = itemCount;
			this.UpdateItemIndexMap(num);
			if (num < this.mItemTotalCount)
			{
				this.mItemPosMgr.SetItemMaxCount(this.mItemTotalCount);
			}
			else
			{
				this.mItemPosMgr.SetItemMaxCount(this.HadCreatedItemCount);
				this.mItemPosMgr.SetItemMaxCount(this.mItemTotalCount);
			}
			this.RecycleAllItem();
			if (this.mItemTotalCount == 0)
			{
				this.mCurReadyMaxItemIndex = 0;
				this.mCurReadyMinItemIndex = 0;
				this.mNeedCheckNextMaxItem = false;
				this.mNeedCheckNextMinItem = false;
				this.mItemIndexMap.Clear();
				return;
			}
			if (this.mCurReadyMaxItemIndex >= this.mItemTotalCount)
			{
				this.mCurReadyMaxItemIndex = this.mItemTotalCount - 1;
			}
			this.mNeedCheckNextMaxItem = true;
			this.mNeedCheckNextMinItem = true;
		}

		// Token: 0x0600426B RID: 17003 RVA: 0x001C86BC File Offset: 0x001C68BC
		private void UpdateItemIndexMap(int oldItemTotalCount)
		{
			int count = this.mItemIndexMap.Count;
			if (count == 0)
			{
				return;
			}
			if (this.mItemTotalCount == 0)
			{
				this.mItemIndexMap.Clear();
				return;
			}
			if (this.mItemTotalCount >= oldItemTotalCount)
			{
				return;
			}
			int itemTotalCount = this.mParentGridView.ItemTotalCount;
			if (this.mItemIndexMap[count - 1] < itemTotalCount)
			{
				return;
			}
			int i = 0;
			int num = count - 1;
			int num2 = 0;
			while (i <= num)
			{
				int num3 = (i + num) / 2;
				int num4 = this.mItemIndexMap[num3];
				if (num4 == itemTotalCount)
				{
					num2 = num3;
					break;
				}
				if (num4 >= itemTotalCount)
				{
					break;
				}
				i = num3 + 1;
				num2 = i;
			}
			int num5 = 0;
			for (int j = num2; j < count; j++)
			{
				if (this.mItemIndexMap[j] >= itemTotalCount)
				{
					num5 = j;
					break;
				}
			}
			this.mItemIndexMap.RemoveRange(num5, count - num5);
		}

		// Token: 0x0600426C RID: 17004 RVA: 0x001C8790 File Offset: 0x001C6990
		public void UpdateListViewPart1(float distanceForRecycle0, float distanceForRecycle1, float distanceForNew0, float distanceForNew1)
		{
			if (this.mSupportScrollBar)
			{
				this.mItemPosMgr.Update(false);
			}
			this.mListUpdateCheckFrameCount = this.mParentGridView.ListUpdateCheckFrameCount;
			bool flag = true;
			int num = 0;
			int num2 = 9999;
			while (flag)
			{
				num++;
				if (num >= num2)
				{
					Debug.LogError("UpdateListViewPart1 while loop " + num + " times! something is wrong!");
					break;
				}
				if (this.mIsVertList)
				{
					flag = this.UpdateForVertListPart1(distanceForRecycle0, distanceForRecycle1, distanceForNew0, distanceForNew1);
				}
				else
				{
					flag = this.UpdateForHorizontalListPart1(distanceForRecycle0, distanceForRecycle1, distanceForNew0, distanceForNew1);
				}
			}
			this.mLastFrameContainerPos = this.mContainerTrans.anchoredPosition3D;
		}

		// Token: 0x0600426D RID: 17005 RVA: 0x0002F65A File Offset: 0x0002D85A
		public bool UpdateListViewPart2(float distanceForRecycle0, float distanceForRecycle1, float distanceForNew0, float distanceForNew1)
		{
			if (this.mIsVertList)
			{
				return this.UpdateForVertListPart2(distanceForRecycle0, distanceForRecycle1, distanceForNew0, distanceForNew1);
			}
			return this.UpdateForHorizontalListPart2(distanceForRecycle0, distanceForRecycle1, distanceForNew0, distanceForNew1);
		}

		// Token: 0x0600426E RID: 17006 RVA: 0x001C8828 File Offset: 0x001C6A28
		public bool UpdateForVertListPart1(float distanceForRecycle0, float distanceForRecycle1, float distanceForNew0, float distanceForNew1)
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
					LoopStaggeredGridViewItem newItemByIndexInGroup = this.GetNewItemByIndexInGroup(num2);
					if (newItemByIndexInGroup == null)
					{
						return false;
					}
					if (this.mSupportScrollBar)
					{
						this.SetItemSize(num2, newItemByIndexInGroup.CachedRectTransform.rect.height, newItemByIndexInGroup.Padding);
					}
					this.mItemList.Add(newItemByIndexInGroup);
					newItemByIndexInGroup.CachedRectTransform.anchoredPosition3D = new Vector3(newItemByIndexInGroup.StartPosOffset, num3, 0f);
					return true;
				}
				else
				{
					LoopStaggeredGridViewItem loopStaggeredGridViewItem = this.mItemList[0];
					loopStaggeredGridViewItem.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
					Vector3 vector = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[1]);
					Vector3 vector2 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[0]);
					if (!this.IsDraging && loopStaggeredGridViewItem.ItemCreatedCheckFrameCount != this.mListUpdateCheckFrameCount && vector2.y - this.mViewPortRectLocalCorners[1].y > distanceForRecycle0)
					{
						this.mItemList.RemoveAt(0);
						this.RecycleItemTmp(loopStaggeredGridViewItem);
						if (!this.mSupportScrollBar)
						{
							this.CheckIfNeedUpdateItemPos();
						}
						return true;
					}
					LoopStaggeredGridViewItem loopStaggeredGridViewItem2 = this.mItemList[this.mItemList.Count - 1];
					loopStaggeredGridViewItem2.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
					Vector3 vector3 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[1]);
					Vector3 vector4 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[0]);
					if (!this.IsDraging && loopStaggeredGridViewItem2.ItemCreatedCheckFrameCount != this.mListUpdateCheckFrameCount && this.mViewPortRectLocalCorners[0].y - vector3.y > distanceForRecycle1)
					{
						this.mItemList.RemoveAt(this.mItemList.Count - 1);
						this.RecycleItemTmp(loopStaggeredGridViewItem2);
						if (!this.mSupportScrollBar)
						{
							this.CheckIfNeedUpdateItemPos();
						}
						return true;
					}
					if (vector.y - this.mViewPortRectLocalCorners[1].y < distanceForNew0)
					{
						if (loopStaggeredGridViewItem.ItemIndexInGroup < this.mCurReadyMinItemIndex)
						{
							this.mCurReadyMinItemIndex = loopStaggeredGridViewItem.ItemIndexInGroup;
							this.mNeedCheckNextMinItem = true;
						}
						int num4 = loopStaggeredGridViewItem.ItemIndexInGroup - 1;
						if (num4 >= this.mCurReadyMinItemIndex || this.mNeedCheckNextMinItem)
						{
							LoopStaggeredGridViewItem newItemByIndexInGroup2 = this.GetNewItemByIndexInGroup(num4);
							if (!(newItemByIndexInGroup2 == null))
							{
								if (this.mSupportScrollBar)
								{
									this.SetItemSize(num4, newItemByIndexInGroup2.CachedRectTransform.rect.height, newItemByIndexInGroup2.Padding);
								}
								this.mItemList.Insert(0, newItemByIndexInGroup2);
								float num5 = loopStaggeredGridViewItem.CachedRectTransform.anchoredPosition3D.y + newItemByIndexInGroup2.CachedRectTransform.rect.height + newItemByIndexInGroup2.Padding;
								newItemByIndexInGroup2.CachedRectTransform.anchoredPosition3D = new Vector3(newItemByIndexInGroup2.StartPosOffset, num5, 0f);
								this.CheckIfNeedUpdateItemPos();
								if (num4 < this.mCurReadyMinItemIndex)
								{
									this.mCurReadyMinItemIndex = num4;
								}
								return true;
							}
							this.mCurReadyMinItemIndex = loopStaggeredGridViewItem.ItemIndexInGroup;
							this.mNeedCheckNextMinItem = false;
						}
					}
					if (this.mViewPortRectLocalCorners[0].y - vector4.y < distanceForNew1)
					{
						if (loopStaggeredGridViewItem2.ItemIndexInGroup > this.mCurReadyMaxItemIndex)
						{
							this.mCurReadyMaxItemIndex = loopStaggeredGridViewItem2.ItemIndexInGroup;
							this.mNeedCheckNextMaxItem = true;
						}
						int num6 = loopStaggeredGridViewItem2.ItemIndexInGroup + 1;
						if (num6 >= this.mItemIndexMap.Count)
						{
							return false;
						}
						if (num6 <= this.mCurReadyMaxItemIndex || this.mNeedCheckNextMaxItem)
						{
							LoopStaggeredGridViewItem newItemByIndexInGroup3 = this.GetNewItemByIndexInGroup(num6);
							if (newItemByIndexInGroup3 == null)
							{
								this.mCurReadyMaxItemIndex = loopStaggeredGridViewItem2.ItemIndexInGroup;
								this.mNeedCheckNextMaxItem = false;
								this.CheckIfNeedUpdateItemPos();
								return false;
							}
							if (this.mSupportScrollBar)
							{
								this.SetItemSize(num6, newItemByIndexInGroup3.CachedRectTransform.rect.height, newItemByIndexInGroup3.Padding);
							}
							this.mItemList.Add(newItemByIndexInGroup3);
							float num7 = loopStaggeredGridViewItem2.CachedRectTransform.anchoredPosition3D.y - loopStaggeredGridViewItem2.CachedRectTransform.rect.height - loopStaggeredGridViewItem2.Padding;
							newItemByIndexInGroup3.CachedRectTransform.anchoredPosition3D = new Vector3(newItemByIndexInGroup3.StartPosOffset, num7, 0f);
							this.CheckIfNeedUpdateItemPos();
							if (num6 > this.mCurReadyMaxItemIndex)
							{
								this.mCurReadyMaxItemIndex = num6;
							}
							return true;
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
				LoopStaggeredGridViewItem newItemByIndexInGroup4 = this.GetNewItemByIndexInGroup(num9);
				if (newItemByIndexInGroup4 == null)
				{
					return false;
				}
				if (this.mSupportScrollBar)
				{
					this.SetItemSize(num9, newItemByIndexInGroup4.CachedRectTransform.rect.height, newItemByIndexInGroup4.Padding);
				}
				this.mItemList.Add(newItemByIndexInGroup4);
				newItemByIndexInGroup4.CachedRectTransform.anchoredPosition3D = new Vector3(newItemByIndexInGroup4.StartPosOffset, num10, 0f);
				return true;
			}
			else
			{
				LoopStaggeredGridViewItem loopStaggeredGridViewItem3 = this.mItemList[0];
				loopStaggeredGridViewItem3.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
				Vector3 vector5 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[1]);
				Vector3 vector6 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[0]);
				if (!this.IsDraging && loopStaggeredGridViewItem3.ItemCreatedCheckFrameCount != this.mListUpdateCheckFrameCount && this.mViewPortRectLocalCorners[0].y - vector5.y > distanceForRecycle0)
				{
					this.mItemList.RemoveAt(0);
					this.RecycleItemTmp(loopStaggeredGridViewItem3);
					if (!this.mSupportScrollBar)
					{
						this.CheckIfNeedUpdateItemPos();
					}
					return true;
				}
				LoopStaggeredGridViewItem loopStaggeredGridViewItem4 = this.mItemList[this.mItemList.Count - 1];
				loopStaggeredGridViewItem4.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
				Vector3 vector7 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[1]);
				Vector3 vector8 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[0]);
				if (!this.IsDraging && loopStaggeredGridViewItem4.ItemCreatedCheckFrameCount != this.mListUpdateCheckFrameCount && vector8.y - this.mViewPortRectLocalCorners[1].y > distanceForRecycle1)
				{
					this.mItemList.RemoveAt(this.mItemList.Count - 1);
					this.RecycleItemTmp(loopStaggeredGridViewItem4);
					if (!this.mSupportScrollBar)
					{
						this.CheckIfNeedUpdateItemPos();
					}
					return true;
				}
				if (this.mViewPortRectLocalCorners[0].y - vector6.y < distanceForNew0)
				{
					if (loopStaggeredGridViewItem3.ItemIndexInGroup < this.mCurReadyMinItemIndex)
					{
						this.mCurReadyMinItemIndex = loopStaggeredGridViewItem3.ItemIndexInGroup;
						this.mNeedCheckNextMinItem = true;
					}
					int num11 = loopStaggeredGridViewItem3.ItemIndexInGroup - 1;
					if (num11 >= this.mCurReadyMinItemIndex || this.mNeedCheckNextMinItem)
					{
						LoopStaggeredGridViewItem newItemByIndexInGroup5 = this.GetNewItemByIndexInGroup(num11);
						if (!(newItemByIndexInGroup5 == null))
						{
							if (this.mSupportScrollBar)
							{
								this.SetItemSize(num11, newItemByIndexInGroup5.CachedRectTransform.rect.height, newItemByIndexInGroup5.Padding);
							}
							this.mItemList.Insert(0, newItemByIndexInGroup5);
							float num12 = loopStaggeredGridViewItem3.CachedRectTransform.anchoredPosition3D.y - newItemByIndexInGroup5.CachedRectTransform.rect.height - newItemByIndexInGroup5.Padding;
							newItemByIndexInGroup5.CachedRectTransform.anchoredPosition3D = new Vector3(newItemByIndexInGroup5.StartPosOffset, num12, 0f);
							this.CheckIfNeedUpdateItemPos();
							if (num11 < this.mCurReadyMinItemIndex)
							{
								this.mCurReadyMinItemIndex = num11;
							}
							return true;
						}
						this.mCurReadyMinItemIndex = loopStaggeredGridViewItem3.ItemIndexInGroup;
						this.mNeedCheckNextMinItem = false;
					}
				}
				if (vector7.y - this.mViewPortRectLocalCorners[1].y < distanceForNew1)
				{
					if (loopStaggeredGridViewItem4.ItemIndexInGroup > this.mCurReadyMaxItemIndex)
					{
						this.mCurReadyMaxItemIndex = loopStaggeredGridViewItem4.ItemIndexInGroup;
						this.mNeedCheckNextMaxItem = true;
					}
					int num13 = loopStaggeredGridViewItem4.ItemIndexInGroup + 1;
					if (num13 >= this.mItemIndexMap.Count)
					{
						return false;
					}
					if (num13 <= this.mCurReadyMaxItemIndex || this.mNeedCheckNextMaxItem)
					{
						LoopStaggeredGridViewItem newItemByIndexInGroup6 = this.GetNewItemByIndexInGroup(num13);
						if (newItemByIndexInGroup6 == null)
						{
							this.mCurReadyMaxItemIndex = loopStaggeredGridViewItem4.ItemIndexInGroup;
							this.mNeedCheckNextMaxItem = false;
							this.CheckIfNeedUpdateItemPos();
							return false;
						}
						if (this.mSupportScrollBar)
						{
							this.SetItemSize(num13, newItemByIndexInGroup6.CachedRectTransform.rect.height, newItemByIndexInGroup6.Padding);
						}
						this.mItemList.Add(newItemByIndexInGroup6);
						float num14 = loopStaggeredGridViewItem4.CachedRectTransform.anchoredPosition3D.y + loopStaggeredGridViewItem4.CachedRectTransform.rect.height + loopStaggeredGridViewItem4.Padding;
						newItemByIndexInGroup6.CachedRectTransform.anchoredPosition3D = new Vector3(newItemByIndexInGroup6.StartPosOffset, num14, 0f);
						this.CheckIfNeedUpdateItemPos();
						if (num13 > this.mCurReadyMaxItemIndex)
						{
							this.mCurReadyMaxItemIndex = num13;
						}
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600426F RID: 17007 RVA: 0x001C919C File Offset: 0x001C739C
		public bool UpdateForVertListPart2(float distanceForRecycle0, float distanceForRecycle1, float distanceForNew0, float distanceForNew1)
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
					LoopStaggeredGridViewItem newItemByIndexInGroup = this.GetNewItemByIndexInGroup(num2);
					if (newItemByIndexInGroup == null)
					{
						return false;
					}
					if (this.mSupportScrollBar)
					{
						this.SetItemSize(num2, newItemByIndexInGroup.CachedRectTransform.rect.height, newItemByIndexInGroup.Padding);
					}
					this.mItemList.Add(newItemByIndexInGroup);
					newItemByIndexInGroup.CachedRectTransform.anchoredPosition3D = new Vector3(newItemByIndexInGroup.StartPosOffset, num3, 0f);
					return true;
				}
				else
				{
					LoopStaggeredGridViewItem loopStaggeredGridViewItem = this.mItemList[this.mItemList.Count - 1];
					loopStaggeredGridViewItem.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
					Vector3 vector = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[0]);
					if (this.mViewPortRectLocalCorners[0].y - vector.y < distanceForNew1)
					{
						if (loopStaggeredGridViewItem.ItemIndexInGroup > this.mCurReadyMaxItemIndex)
						{
							this.mCurReadyMaxItemIndex = loopStaggeredGridViewItem.ItemIndexInGroup;
							this.mNeedCheckNextMaxItem = true;
						}
						int num4 = loopStaggeredGridViewItem.ItemIndexInGroup + 1;
						if (num4 <= this.mCurReadyMaxItemIndex || this.mNeedCheckNextMaxItem)
						{
							LoopStaggeredGridViewItem newItemByIndexInGroup2 = this.GetNewItemByIndexInGroup(num4);
							if (newItemByIndexInGroup2 == null)
							{
								this.mCurReadyMaxItemIndex = loopStaggeredGridViewItem.ItemIndexInGroup;
								this.mNeedCheckNextMaxItem = false;
								this.CheckIfNeedUpdateItemPos();
								return false;
							}
							if (this.mSupportScrollBar)
							{
								this.SetItemSize(num4, newItemByIndexInGroup2.CachedRectTransform.rect.height, newItemByIndexInGroup2.Padding);
							}
							this.mItemList.Add(newItemByIndexInGroup2);
							float num5 = loopStaggeredGridViewItem.CachedRectTransform.anchoredPosition3D.y - loopStaggeredGridViewItem.CachedRectTransform.rect.height - loopStaggeredGridViewItem.Padding;
							newItemByIndexInGroup2.CachedRectTransform.anchoredPosition3D = new Vector3(newItemByIndexInGroup2.StartPosOffset, num5, 0f);
							this.CheckIfNeedUpdateItemPos();
							if (num4 > this.mCurReadyMaxItemIndex)
							{
								this.mCurReadyMaxItemIndex = num4;
							}
							return true;
						}
					}
				}
			}
			else if (this.mItemList.Count == 0)
			{
				float num6 = this.mContainerTrans.anchoredPosition3D.y;
				if (num6 > 0f)
				{
					num6 = 0f;
				}
				int num7 = 0;
				float num8 = -num6;
				if (this.mSupportScrollBar && !this.GetPlusItemIndexAndPosAtGivenPos(-num6, ref num7, ref num8))
				{
					return false;
				}
				LoopStaggeredGridViewItem newItemByIndexInGroup3 = this.GetNewItemByIndexInGroup(num7);
				if (newItemByIndexInGroup3 == null)
				{
					return false;
				}
				if (this.mSupportScrollBar)
				{
					this.SetItemSize(num7, newItemByIndexInGroup3.CachedRectTransform.rect.height, newItemByIndexInGroup3.Padding);
				}
				this.mItemList.Add(newItemByIndexInGroup3);
				newItemByIndexInGroup3.CachedRectTransform.anchoredPosition3D = new Vector3(newItemByIndexInGroup3.StartPosOffset, num8, 0f);
				return true;
			}
			else
			{
				LoopStaggeredGridViewItem loopStaggeredGridViewItem2 = this.mItemList[this.mItemList.Count - 1];
				loopStaggeredGridViewItem2.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
				if (this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[1]).y - this.mViewPortRectLocalCorners[1].y < distanceForNew1)
				{
					if (loopStaggeredGridViewItem2.ItemIndexInGroup > this.mCurReadyMaxItemIndex)
					{
						this.mCurReadyMaxItemIndex = loopStaggeredGridViewItem2.ItemIndexInGroup;
						this.mNeedCheckNextMaxItem = true;
					}
					int num9 = loopStaggeredGridViewItem2.ItemIndexInGroup + 1;
					if (num9 <= this.mCurReadyMaxItemIndex || this.mNeedCheckNextMaxItem)
					{
						LoopStaggeredGridViewItem newItemByIndexInGroup4 = this.GetNewItemByIndexInGroup(num9);
						if (newItemByIndexInGroup4 == null)
						{
							this.mCurReadyMaxItemIndex = loopStaggeredGridViewItem2.ItemIndexInGroup;
							this.mNeedCheckNextMaxItem = false;
							this.CheckIfNeedUpdateItemPos();
							return false;
						}
						if (this.mSupportScrollBar)
						{
							this.SetItemSize(num9, newItemByIndexInGroup4.CachedRectTransform.rect.height, newItemByIndexInGroup4.Padding);
						}
						this.mItemList.Add(newItemByIndexInGroup4);
						float num10 = loopStaggeredGridViewItem2.CachedRectTransform.anchoredPosition3D.y + loopStaggeredGridViewItem2.CachedRectTransform.rect.height + loopStaggeredGridViewItem2.Padding;
						newItemByIndexInGroup4.CachedRectTransform.anchoredPosition3D = new Vector3(newItemByIndexInGroup4.StartPosOffset, num10, 0f);
						this.CheckIfNeedUpdateItemPos();
						if (num9 > this.mCurReadyMaxItemIndex)
						{
							this.mCurReadyMaxItemIndex = num9;
						}
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06004270 RID: 17008 RVA: 0x001C963C File Offset: 0x001C783C
		public bool UpdateForHorizontalListPart1(float distanceForRecycle0, float distanceForRecycle1, float distanceForNew0, float distanceForNew1)
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
					LoopStaggeredGridViewItem newItemByIndexInGroup = this.GetNewItemByIndexInGroup(num2);
					if (newItemByIndexInGroup == null)
					{
						return false;
					}
					if (this.mSupportScrollBar)
					{
						this.SetItemSize(num2, newItemByIndexInGroup.CachedRectTransform.rect.width, newItemByIndexInGroup.Padding);
					}
					this.mItemList.Add(newItemByIndexInGroup);
					newItemByIndexInGroup.CachedRectTransform.anchoredPosition3D = new Vector3(num3, newItemByIndexInGroup.StartPosOffset, 0f);
					return true;
				}
				else
				{
					LoopStaggeredGridViewItem loopStaggeredGridViewItem = this.mItemList[0];
					loopStaggeredGridViewItem.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
					Vector3 vector = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[1]);
					Vector3 vector2 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[2]);
					if (!this.IsDraging && loopStaggeredGridViewItem.ItemCreatedCheckFrameCount != this.mListUpdateCheckFrameCount && this.mViewPortRectLocalCorners[1].x - vector2.x > distanceForRecycle0)
					{
						this.mItemList.RemoveAt(0);
						this.RecycleItemTmp(loopStaggeredGridViewItem);
						if (!this.mSupportScrollBar)
						{
							this.CheckIfNeedUpdateItemPos();
						}
						return true;
					}
					LoopStaggeredGridViewItem loopStaggeredGridViewItem2 = this.mItemList[this.mItemList.Count - 1];
					loopStaggeredGridViewItem2.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
					Vector3 vector3 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[1]);
					Vector3 vector4 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[2]);
					if (!this.IsDraging && loopStaggeredGridViewItem2.ItemCreatedCheckFrameCount != this.mListUpdateCheckFrameCount && vector3.x - this.mViewPortRectLocalCorners[2].x > distanceForRecycle1)
					{
						this.mItemList.RemoveAt(this.mItemList.Count - 1);
						this.RecycleItemTmp(loopStaggeredGridViewItem2);
						if (!this.mSupportScrollBar)
						{
							this.CheckIfNeedUpdateItemPos();
						}
						return true;
					}
					if (this.mViewPortRectLocalCorners[1].x - vector.x < distanceForNew0)
					{
						if (loopStaggeredGridViewItem.ItemIndexInGroup < this.mCurReadyMinItemIndex)
						{
							this.mCurReadyMinItemIndex = loopStaggeredGridViewItem.ItemIndexInGroup;
							this.mNeedCheckNextMinItem = true;
						}
						int num4 = loopStaggeredGridViewItem.ItemIndexInGroup - 1;
						if (num4 >= this.mCurReadyMinItemIndex || this.mNeedCheckNextMinItem)
						{
							LoopStaggeredGridViewItem newItemByIndexInGroup2 = this.GetNewItemByIndexInGroup(num4);
							if (!(newItemByIndexInGroup2 == null))
							{
								if (this.mSupportScrollBar)
								{
									this.SetItemSize(num4, newItemByIndexInGroup2.CachedRectTransform.rect.width, newItemByIndexInGroup2.Padding);
								}
								this.mItemList.Insert(0, newItemByIndexInGroup2);
								float num5 = loopStaggeredGridViewItem.CachedRectTransform.anchoredPosition3D.x - newItemByIndexInGroup2.CachedRectTransform.rect.width - newItemByIndexInGroup2.Padding;
								newItemByIndexInGroup2.CachedRectTransform.anchoredPosition3D = new Vector3(num5, newItemByIndexInGroup2.StartPosOffset, 0f);
								this.CheckIfNeedUpdateItemPos();
								if (num4 < this.mCurReadyMinItemIndex)
								{
									this.mCurReadyMinItemIndex = num4;
								}
								return true;
							}
							this.mCurReadyMinItemIndex = loopStaggeredGridViewItem.ItemIndexInGroup;
							this.mNeedCheckNextMinItem = false;
						}
					}
					if (vector4.x - this.mViewPortRectLocalCorners[2].x < distanceForNew1)
					{
						if (loopStaggeredGridViewItem2.ItemIndexInGroup > this.mCurReadyMaxItemIndex)
						{
							this.mCurReadyMaxItemIndex = loopStaggeredGridViewItem2.ItemIndexInGroup;
							this.mNeedCheckNextMaxItem = true;
						}
						int num6 = loopStaggeredGridViewItem2.ItemIndexInGroup + 1;
						if (num6 >= this.mItemIndexMap.Count)
						{
							return false;
						}
						if (num6 <= this.mCurReadyMaxItemIndex || this.mNeedCheckNextMaxItem)
						{
							LoopStaggeredGridViewItem newItemByIndexInGroup3 = this.GetNewItemByIndexInGroup(num6);
							if (!(newItemByIndexInGroup3 == null))
							{
								if (this.mSupportScrollBar)
								{
									this.SetItemSize(num6, newItemByIndexInGroup3.CachedRectTransform.rect.width, newItemByIndexInGroup3.Padding);
								}
								this.mItemList.Add(newItemByIndexInGroup3);
								float num7 = loopStaggeredGridViewItem2.CachedRectTransform.anchoredPosition3D.x + loopStaggeredGridViewItem2.CachedRectTransform.rect.width + loopStaggeredGridViewItem2.Padding;
								newItemByIndexInGroup3.CachedRectTransform.anchoredPosition3D = new Vector3(num7, newItemByIndexInGroup3.StartPosOffset, 0f);
								this.CheckIfNeedUpdateItemPos();
								if (num6 > this.mCurReadyMaxItemIndex)
								{
									this.mCurReadyMaxItemIndex = num6;
								}
								return true;
							}
							this.mCurReadyMaxItemIndex = loopStaggeredGridViewItem2.ItemIndexInGroup;
							this.mNeedCheckNextMaxItem = false;
							this.CheckIfNeedUpdateItemPos();
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
				LoopStaggeredGridViewItem newItemByIndexInGroup4 = this.GetNewItemByIndexInGroup(num9);
				if (newItemByIndexInGroup4 == null)
				{
					return false;
				}
				if (this.mSupportScrollBar)
				{
					this.SetItemSize(num9, newItemByIndexInGroup4.CachedRectTransform.rect.width, newItemByIndexInGroup4.Padding);
				}
				this.mItemList.Add(newItemByIndexInGroup4);
				newItemByIndexInGroup4.CachedRectTransform.anchoredPosition3D = new Vector3(num10, newItemByIndexInGroup4.StartPosOffset, 0f);
				return true;
			}
			else
			{
				LoopStaggeredGridViewItem loopStaggeredGridViewItem3 = this.mItemList[0];
				loopStaggeredGridViewItem3.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
				Vector3 vector5 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[1]);
				Vector3 vector6 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[2]);
				if (!this.IsDraging && loopStaggeredGridViewItem3.ItemCreatedCheckFrameCount != this.mListUpdateCheckFrameCount && vector5.x - this.mViewPortRectLocalCorners[2].x > distanceForRecycle0)
				{
					this.mItemList.RemoveAt(0);
					this.RecycleItemTmp(loopStaggeredGridViewItem3);
					if (!this.mSupportScrollBar)
					{
						this.CheckIfNeedUpdateItemPos();
					}
					return true;
				}
				LoopStaggeredGridViewItem loopStaggeredGridViewItem4 = this.mItemList[this.mItemList.Count - 1];
				loopStaggeredGridViewItem4.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
				Vector3 vector7 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[1]);
				Vector3 vector8 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[2]);
				if (!this.IsDraging && loopStaggeredGridViewItem4.ItemCreatedCheckFrameCount != this.mListUpdateCheckFrameCount && this.mViewPortRectLocalCorners[1].x - vector8.x > distanceForRecycle1)
				{
					this.mItemList.RemoveAt(this.mItemList.Count - 1);
					this.RecycleItemTmp(loopStaggeredGridViewItem4);
					if (!this.mSupportScrollBar)
					{
						this.CheckIfNeedUpdateItemPos();
					}
					return true;
				}
				if (vector6.x - this.mViewPortRectLocalCorners[2].x < distanceForNew0)
				{
					if (loopStaggeredGridViewItem3.ItemIndexInGroup < this.mCurReadyMinItemIndex)
					{
						this.mCurReadyMinItemIndex = loopStaggeredGridViewItem3.ItemIndexInGroup;
						this.mNeedCheckNextMinItem = true;
					}
					int num11 = loopStaggeredGridViewItem3.ItemIndexInGroup - 1;
					if (num11 >= this.mCurReadyMinItemIndex || this.mNeedCheckNextMinItem)
					{
						LoopStaggeredGridViewItem newItemByIndexInGroup5 = this.GetNewItemByIndexInGroup(num11);
						if (!(newItemByIndexInGroup5 == null))
						{
							if (this.mSupportScrollBar)
							{
								this.SetItemSize(num11, newItemByIndexInGroup5.CachedRectTransform.rect.width, newItemByIndexInGroup5.Padding);
							}
							this.mItemList.Insert(0, newItemByIndexInGroup5);
							float num12 = loopStaggeredGridViewItem3.CachedRectTransform.anchoredPosition3D.x + newItemByIndexInGroup5.CachedRectTransform.rect.width + newItemByIndexInGroup5.Padding;
							newItemByIndexInGroup5.CachedRectTransform.anchoredPosition3D = new Vector3(num12, newItemByIndexInGroup5.StartPosOffset, 0f);
							this.CheckIfNeedUpdateItemPos();
							if (num11 < this.mCurReadyMinItemIndex)
							{
								this.mCurReadyMinItemIndex = num11;
							}
							return true;
						}
						this.mCurReadyMinItemIndex = loopStaggeredGridViewItem3.ItemIndexInGroup;
						this.mNeedCheckNextMinItem = false;
					}
				}
				if (this.mViewPortRectLocalCorners[1].x - vector7.x < distanceForNew1)
				{
					if (loopStaggeredGridViewItem4.ItemIndexInGroup > this.mCurReadyMaxItemIndex)
					{
						this.mCurReadyMaxItemIndex = loopStaggeredGridViewItem4.ItemIndexInGroup;
						this.mNeedCheckNextMaxItem = true;
					}
					int num13 = loopStaggeredGridViewItem4.ItemIndexInGroup + 1;
					if (num13 >= this.mItemIndexMap.Count)
					{
						return false;
					}
					if (num13 <= this.mCurReadyMaxItemIndex || this.mNeedCheckNextMaxItem)
					{
						LoopStaggeredGridViewItem newItemByIndexInGroup6 = this.GetNewItemByIndexInGroup(num13);
						if (!(newItemByIndexInGroup6 == null))
						{
							if (this.mSupportScrollBar)
							{
								this.SetItemSize(num13, newItemByIndexInGroup6.CachedRectTransform.rect.width, newItemByIndexInGroup6.Padding);
							}
							this.mItemList.Add(newItemByIndexInGroup6);
							float num14 = loopStaggeredGridViewItem4.CachedRectTransform.anchoredPosition3D.x - loopStaggeredGridViewItem4.CachedRectTransform.rect.width - loopStaggeredGridViewItem4.Padding;
							newItemByIndexInGroup6.CachedRectTransform.anchoredPosition3D = new Vector3(num14, newItemByIndexInGroup6.StartPosOffset, 0f);
							this.CheckIfNeedUpdateItemPos();
							if (num13 > this.mCurReadyMaxItemIndex)
							{
								this.mCurReadyMaxItemIndex = num13;
							}
							return true;
						}
						this.mCurReadyMaxItemIndex = loopStaggeredGridViewItem4.ItemIndexInGroup;
						this.mNeedCheckNextMaxItem = false;
						this.CheckIfNeedUpdateItemPos();
					}
				}
			}
			return false;
		}

		// Token: 0x06004271 RID: 17009 RVA: 0x001C9FB4 File Offset: 0x001C81B4
		public bool UpdateForHorizontalListPart2(float distanceForRecycle0, float distanceForRecycle1, float distanceForNew0, float distanceForNew1)
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
					LoopStaggeredGridViewItem newItemByIndexInGroup = this.GetNewItemByIndexInGroup(num2);
					if (newItemByIndexInGroup == null)
					{
						return false;
					}
					if (this.mSupportScrollBar)
					{
						this.SetItemSize(num2, newItemByIndexInGroup.CachedRectTransform.rect.width, newItemByIndexInGroup.Padding);
					}
					this.mItemList.Add(newItemByIndexInGroup);
					newItemByIndexInGroup.CachedRectTransform.anchoredPosition3D = new Vector3(num3, newItemByIndexInGroup.StartPosOffset, 0f);
					return true;
				}
				else
				{
					LoopStaggeredGridViewItem loopStaggeredGridViewItem = this.mItemList[this.mItemList.Count - 1];
					loopStaggeredGridViewItem.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
					if (this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[2]).x - this.mViewPortRectLocalCorners[2].x < distanceForNew1)
					{
						if (loopStaggeredGridViewItem.ItemIndexInGroup > this.mCurReadyMaxItemIndex)
						{
							this.mCurReadyMaxItemIndex = loopStaggeredGridViewItem.ItemIndexInGroup;
							this.mNeedCheckNextMaxItem = true;
						}
						int num4 = loopStaggeredGridViewItem.ItemIndexInGroup + 1;
						if (num4 <= this.mCurReadyMaxItemIndex || this.mNeedCheckNextMaxItem)
						{
							LoopStaggeredGridViewItem newItemByIndexInGroup2 = this.GetNewItemByIndexInGroup(num4);
							if (!(newItemByIndexInGroup2 == null))
							{
								if (this.mSupportScrollBar)
								{
									this.SetItemSize(num4, newItemByIndexInGroup2.CachedRectTransform.rect.width, newItemByIndexInGroup2.Padding);
								}
								this.mItemList.Add(newItemByIndexInGroup2);
								float num5 = loopStaggeredGridViewItem.CachedRectTransform.anchoredPosition3D.x + loopStaggeredGridViewItem.CachedRectTransform.rect.width + loopStaggeredGridViewItem.Padding;
								newItemByIndexInGroup2.CachedRectTransform.anchoredPosition3D = new Vector3(num5, newItemByIndexInGroup2.StartPosOffset, 0f);
								this.CheckIfNeedUpdateItemPos();
								if (num4 > this.mCurReadyMaxItemIndex)
								{
									this.mCurReadyMaxItemIndex = num4;
								}
								return true;
							}
							this.mCurReadyMaxItemIndex = loopStaggeredGridViewItem.ItemIndexInGroup;
							this.mNeedCheckNextMaxItem = false;
							this.CheckIfNeedUpdateItemPos();
						}
					}
				}
			}
			else if (this.mItemList.Count == 0)
			{
				float num6 = this.mContainerTrans.anchoredPosition3D.x;
				if (num6 < 0f)
				{
					num6 = 0f;
				}
				int num7 = 0;
				float num8 = -num6;
				if (this.mSupportScrollBar)
				{
					if (!this.GetPlusItemIndexAndPosAtGivenPos(num6, ref num7, ref num8))
					{
						return false;
					}
					num8 = -num8;
				}
				LoopStaggeredGridViewItem newItemByIndexInGroup3 = this.GetNewItemByIndexInGroup(num7);
				if (newItemByIndexInGroup3 == null)
				{
					return false;
				}
				if (this.mSupportScrollBar)
				{
					this.SetItemSize(num7, newItemByIndexInGroup3.CachedRectTransform.rect.width, newItemByIndexInGroup3.Padding);
				}
				this.mItemList.Add(newItemByIndexInGroup3);
				newItemByIndexInGroup3.CachedRectTransform.anchoredPosition3D = new Vector3(num8, newItemByIndexInGroup3.StartPosOffset, 0f);
				return true;
			}
			else
			{
				LoopStaggeredGridViewItem loopStaggeredGridViewItem2 = this.mItemList[this.mItemList.Count - 1];
				loopStaggeredGridViewItem2.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
				Vector3 vector = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[1]);
				if (this.mViewPortRectLocalCorners[1].x - vector.x < distanceForNew1)
				{
					if (loopStaggeredGridViewItem2.ItemIndexInGroup > this.mCurReadyMaxItemIndex)
					{
						this.mCurReadyMaxItemIndex = loopStaggeredGridViewItem2.ItemIndexInGroup;
						this.mNeedCheckNextMaxItem = true;
					}
					int num9 = loopStaggeredGridViewItem2.ItemIndexInGroup + 1;
					if (num9 <= this.mCurReadyMaxItemIndex || this.mNeedCheckNextMaxItem)
					{
						LoopStaggeredGridViewItem newItemByIndexInGroup4 = this.GetNewItemByIndexInGroup(num9);
						if (!(newItemByIndexInGroup4 == null))
						{
							if (this.mSupportScrollBar)
							{
								this.SetItemSize(num9, newItemByIndexInGroup4.CachedRectTransform.rect.width, newItemByIndexInGroup4.Padding);
							}
							this.mItemList.Add(newItemByIndexInGroup4);
							float num10 = loopStaggeredGridViewItem2.CachedRectTransform.anchoredPosition3D.x - loopStaggeredGridViewItem2.CachedRectTransform.rect.width - loopStaggeredGridViewItem2.Padding;
							newItemByIndexInGroup4.CachedRectTransform.anchoredPosition3D = new Vector3(num10, newItemByIndexInGroup4.StartPosOffset, 0f);
							this.CheckIfNeedUpdateItemPos();
							if (num9 > this.mCurReadyMaxItemIndex)
							{
								this.mCurReadyMaxItemIndex = num9;
							}
							return true;
						}
						this.mCurReadyMaxItemIndex = loopStaggeredGridViewItem2.ItemIndexInGroup;
						this.mNeedCheckNextMaxItem = false;
						this.CheckIfNeedUpdateItemPos();
					}
				}
			}
			return false;
		}

		// Token: 0x06004272 RID: 17010 RVA: 0x001CA45C File Offset: 0x001C865C
		public float GetContentPanelSize()
		{
			float num = (this.mItemPosMgr.mTotalSize > 0f) ? (this.mItemPosMgr.mTotalSize - this.mLastItemPadding) : 0f;
			if (num < 0f)
			{
				num = 0f;
			}
			return num;
		}

		// Token: 0x06004273 RID: 17011 RVA: 0x001CA4A4 File Offset: 0x001C86A4
		public float GetShownItemPosMaxValue()
		{
			if (this.mItemList.Count == 0)
			{
				return 0f;
			}
			LoopStaggeredGridViewItem loopStaggeredGridViewItem = this.mItemList[this.mItemList.Count - 1];
			if (this.mArrangeType == ListItemArrangeType.TopToBottom)
			{
				return Mathf.Abs(loopStaggeredGridViewItem.BottomY);
			}
			if (this.mArrangeType == ListItemArrangeType.BottomToTop)
			{
				return Mathf.Abs(loopStaggeredGridViewItem.TopY);
			}
			if (this.mArrangeType == ListItemArrangeType.LeftToRight)
			{
				return Mathf.Abs(loopStaggeredGridViewItem.RightX);
			}
			if (this.mArrangeType == ListItemArrangeType.RightToLeft)
			{
				return Mathf.Abs(loopStaggeredGridViewItem.LeftX);
			}
			return 0f;
		}

		// Token: 0x06004274 RID: 17012 RVA: 0x001CA538 File Offset: 0x001C8738
		public void CheckIfNeedUpdateItemPos()
		{
			if (this.mItemList.Count == 0)
			{
				return;
			}
			if (this.mArrangeType == ListItemArrangeType.TopToBottom)
			{
				LoopStaggeredGridViewItem loopStaggeredGridViewItem = this.mItemList[0];
				LoopStaggeredGridViewItem loopStaggeredGridViewItem2 = this.mItemList[this.mItemList.Count - 1];
				if (loopStaggeredGridViewItem.TopY > 0f || (loopStaggeredGridViewItem.ItemIndexInGroup == this.mCurReadyMinItemIndex && loopStaggeredGridViewItem.TopY != 0f))
				{
					this.UpdateAllShownItemsPos();
					return;
				}
				float contentPanelSize = this.GetContentPanelSize();
				if (-loopStaggeredGridViewItem2.BottomY > contentPanelSize || (loopStaggeredGridViewItem2.ItemIndexInGroup == this.mCurReadyMaxItemIndex && -loopStaggeredGridViewItem2.BottomY != contentPanelSize))
				{
					this.UpdateAllShownItemsPos();
					return;
				}
			}
			else if (this.mArrangeType == ListItemArrangeType.BottomToTop)
			{
				LoopStaggeredGridViewItem loopStaggeredGridViewItem3 = this.mItemList[0];
				LoopStaggeredGridViewItem loopStaggeredGridViewItem4 = this.mItemList[this.mItemList.Count - 1];
				if (loopStaggeredGridViewItem3.BottomY < 0f || (loopStaggeredGridViewItem3.ItemIndexInGroup == this.mCurReadyMinItemIndex && loopStaggeredGridViewItem3.BottomY != 0f))
				{
					this.UpdateAllShownItemsPos();
					return;
				}
				float contentPanelSize2 = this.GetContentPanelSize();
				if (loopStaggeredGridViewItem4.TopY > contentPanelSize2 || (loopStaggeredGridViewItem4.ItemIndexInGroup == this.mCurReadyMaxItemIndex && loopStaggeredGridViewItem4.TopY != contentPanelSize2))
				{
					this.UpdateAllShownItemsPos();
					return;
				}
			}
			else if (this.mArrangeType == ListItemArrangeType.LeftToRight)
			{
				LoopStaggeredGridViewItem loopStaggeredGridViewItem5 = this.mItemList[0];
				LoopStaggeredGridViewItem loopStaggeredGridViewItem6 = this.mItemList[this.mItemList.Count - 1];
				if (loopStaggeredGridViewItem5.LeftX < 0f || (loopStaggeredGridViewItem5.ItemIndexInGroup == this.mCurReadyMinItemIndex && loopStaggeredGridViewItem5.LeftX != 0f))
				{
					this.UpdateAllShownItemsPos();
					return;
				}
				float contentPanelSize3 = this.GetContentPanelSize();
				if (loopStaggeredGridViewItem6.RightX > contentPanelSize3 || (loopStaggeredGridViewItem6.ItemIndexInGroup == this.mCurReadyMaxItemIndex && loopStaggeredGridViewItem6.RightX != contentPanelSize3))
				{
					this.UpdateAllShownItemsPos();
					return;
				}
			}
			else if (this.mArrangeType == ListItemArrangeType.RightToLeft)
			{
				LoopStaggeredGridViewItem loopStaggeredGridViewItem7 = this.mItemList[0];
				LoopStaggeredGridViewItem loopStaggeredGridViewItem8 = this.mItemList[this.mItemList.Count - 1];
				if (loopStaggeredGridViewItem7.RightX > 0f || (loopStaggeredGridViewItem7.ItemIndexInGroup == this.mCurReadyMinItemIndex && loopStaggeredGridViewItem7.RightX != 0f))
				{
					this.UpdateAllShownItemsPos();
					return;
				}
				float contentPanelSize4 = this.GetContentPanelSize();
				if (-loopStaggeredGridViewItem8.LeftX > contentPanelSize4 || (loopStaggeredGridViewItem8.ItemIndexInGroup == this.mCurReadyMaxItemIndex && -loopStaggeredGridViewItem8.LeftX != contentPanelSize4))
				{
					this.UpdateAllShownItemsPos();
					return;
				}
			}
		}

		// Token: 0x06004275 RID: 17013 RVA: 0x001CA7C4 File Offset: 0x001C89C4
		public void UpdateAllShownItemsPos()
		{
			int count = this.mItemList.Count;
			if (count == 0)
			{
				return;
			}
			if (this.mArrangeType == ListItemArrangeType.TopToBottom)
			{
				float num = 0f;
				if (this.mSupportScrollBar)
				{
					num = -this.GetItemPos(this.mItemList[0].ItemIndexInGroup);
				}
				float num2 = num;
				for (int i = 0; i < count; i++)
				{
					LoopStaggeredGridViewItem loopStaggeredGridViewItem = this.mItemList[i];
					loopStaggeredGridViewItem.CachedRectTransform.anchoredPosition3D = new Vector3(loopStaggeredGridViewItem.StartPosOffset, num2, 0f);
					num2 = num2 - loopStaggeredGridViewItem.CachedRectTransform.rect.height - loopStaggeredGridViewItem.Padding;
				}
				return;
			}
			if (this.mArrangeType == ListItemArrangeType.BottomToTop)
			{
				float num3 = 0f;
				if (this.mSupportScrollBar)
				{
					num3 = this.GetItemPos(this.mItemList[0].ItemIndexInGroup);
				}
				float num4 = num3;
				for (int j = 0; j < count; j++)
				{
					LoopStaggeredGridViewItem loopStaggeredGridViewItem2 = this.mItemList[j];
					loopStaggeredGridViewItem2.CachedRectTransform.anchoredPosition3D = new Vector3(loopStaggeredGridViewItem2.StartPosOffset, num4, 0f);
					num4 = num4 + loopStaggeredGridViewItem2.CachedRectTransform.rect.height + loopStaggeredGridViewItem2.Padding;
				}
				return;
			}
			if (this.mArrangeType == ListItemArrangeType.LeftToRight)
			{
				float num5 = 0f;
				if (this.mSupportScrollBar)
				{
					num5 = this.GetItemPos(this.mItemList[0].ItemIndexInGroup);
				}
				float num6 = num5;
				for (int k = 0; k < count; k++)
				{
					LoopStaggeredGridViewItem loopStaggeredGridViewItem3 = this.mItemList[k];
					loopStaggeredGridViewItem3.CachedRectTransform.anchoredPosition3D = new Vector3(num6, loopStaggeredGridViewItem3.StartPosOffset, 0f);
					num6 = num6 + loopStaggeredGridViewItem3.CachedRectTransform.rect.width + loopStaggeredGridViewItem3.Padding;
				}
				return;
			}
			if (this.mArrangeType == ListItemArrangeType.RightToLeft)
			{
				float num7 = 0f;
				if (this.mSupportScrollBar)
				{
					num7 = -this.GetItemPos(this.mItemList[0].ItemIndexInGroup);
				}
				float num8 = num7;
				for (int l = 0; l < count; l++)
				{
					LoopStaggeredGridViewItem loopStaggeredGridViewItem4 = this.mItemList[l];
					loopStaggeredGridViewItem4.CachedRectTransform.anchoredPosition3D = new Vector3(num8, loopStaggeredGridViewItem4.StartPosOffset, 0f);
					num8 = num8 - loopStaggeredGridViewItem4.CachedRectTransform.rect.width - loopStaggeredGridViewItem4.Padding;
				}
			}
		}

		// Token: 0x04003AC2 RID: 15042
		private LoopStaggeredGridView mParentGridView;

		// Token: 0x04003AC3 RID: 15043
		private ListItemArrangeType mArrangeType;

		// Token: 0x04003AC4 RID: 15044
		private List<LoopStaggeredGridViewItem> mItemList = new List<LoopStaggeredGridViewItem>();

		// Token: 0x04003AC5 RID: 15045
		private RectTransform mContainerTrans;

		// Token: 0x04003AC6 RID: 15046
		private ScrollRect mScrollRect;

		// Token: 0x04003AC7 RID: 15047
		public int mGroupIndex;

		// Token: 0x04003AC8 RID: 15048
		private GameObject mGameObject;

		// Token: 0x04003AC9 RID: 15049
		private List<int> mItemIndexMap = new List<int>();

		// Token: 0x04003ACA RID: 15050
		private RectTransform mScrollRectTransform;

		// Token: 0x04003ACB RID: 15051
		private RectTransform mViewPortRectTransform;

		// Token: 0x04003ACC RID: 15052
		private float mItemDefaultWithPaddingSize;

		// Token: 0x04003ACD RID: 15053
		private int mItemTotalCount;

		// Token: 0x04003ACE RID: 15054
		private bool mIsVertList;

		// Token: 0x04003ACF RID: 15055
		private Func<int, int, LoopStaggeredGridViewItem> mOnGetItemByIndex;

		// Token: 0x04003AD0 RID: 15056
		private Vector3[] mItemWorldCorners = new Vector3[4];

		// Token: 0x04003AD1 RID: 15057
		private Vector3[] mViewPortRectLocalCorners = new Vector3[4];

		// Token: 0x04003AD2 RID: 15058
		private int mCurReadyMinItemIndex;

		// Token: 0x04003AD3 RID: 15059
		private int mCurReadyMaxItemIndex;

		// Token: 0x04003AD4 RID: 15060
		private bool mNeedCheckNextMinItem = true;

		// Token: 0x04003AD5 RID: 15061
		private bool mNeedCheckNextMaxItem = true;

		// Token: 0x04003AD6 RID: 15062
		private ItemPosMgr mItemPosMgr;

		// Token: 0x04003AD7 RID: 15063
		private bool mSupportScrollBar = true;

		// Token: 0x04003AD8 RID: 15064
		private int mLastItemIndex;

		// Token: 0x04003AD9 RID: 15065
		private float mLastItemPadding;

		// Token: 0x04003ADA RID: 15066
		private Vector3 mLastFrameContainerPos = Vector3.zero;

		// Token: 0x04003ADB RID: 15067
		private int mListUpdateCheckFrameCount;
	}
}
