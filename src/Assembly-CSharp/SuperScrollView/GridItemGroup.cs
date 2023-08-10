using UnityEngine;

namespace SuperScrollView;

public class GridItemGroup
{
	private int mCount;

	private int mGroupIndex = -1;

	private LoopGridViewItem mFirst;

	private LoopGridViewItem mLast;

	public int Count => mCount;

	public LoopGridViewItem First => mFirst;

	public LoopGridViewItem Last => mLast;

	public int GroupIndex
	{
		get
		{
			return mGroupIndex;
		}
		set
		{
			mGroupIndex = value;
		}
	}

	public LoopGridViewItem GetItemByColumn(int column)
	{
		LoopGridViewItem nextItem = mFirst;
		while ((Object)(object)nextItem != (Object)null)
		{
			if (nextItem.Column == column)
			{
				return nextItem;
			}
			nextItem = nextItem.NextItem;
		}
		return null;
	}

	public LoopGridViewItem GetItemByRow(int row)
	{
		LoopGridViewItem nextItem = mFirst;
		while ((Object)(object)nextItem != (Object)null)
		{
			if (nextItem.Row == row)
			{
				return nextItem;
			}
			nextItem = nextItem.NextItem;
		}
		return null;
	}

	public void ReplaceItem(LoopGridViewItem curItem, LoopGridViewItem newItem)
	{
		newItem.PrevItem = curItem.PrevItem;
		newItem.NextItem = curItem.NextItem;
		if ((Object)(object)newItem.PrevItem != (Object)null)
		{
			newItem.PrevItem.NextItem = newItem;
		}
		if ((Object)(object)newItem.NextItem != (Object)null)
		{
			newItem.NextItem.PrevItem = newItem;
		}
		if ((Object)(object)mFirst == (Object)(object)curItem)
		{
			mFirst = newItem;
		}
		if ((Object)(object)mLast == (Object)(object)curItem)
		{
			mLast = newItem;
		}
	}

	public void AddFirst(LoopGridViewItem newItem)
	{
		newItem.PrevItem = null;
		newItem.NextItem = null;
		if ((Object)(object)mFirst == (Object)null)
		{
			mFirst = newItem;
			mLast = newItem;
			mFirst.PrevItem = null;
			mFirst.NextItem = null;
			mCount++;
		}
		else
		{
			mFirst.PrevItem = newItem;
			newItem.PrevItem = null;
			newItem.NextItem = mFirst;
			mFirst = newItem;
			mCount++;
		}
	}

	public void AddLast(LoopGridViewItem newItem)
	{
		newItem.PrevItem = null;
		newItem.NextItem = null;
		if ((Object)(object)mFirst == (Object)null)
		{
			mFirst = newItem;
			mLast = newItem;
			mFirst.PrevItem = null;
			mFirst.NextItem = null;
			mCount++;
		}
		else
		{
			mLast.NextItem = newItem;
			newItem.PrevItem = mLast;
			newItem.NextItem = null;
			mLast = newItem;
			mCount++;
		}
	}

	public LoopGridViewItem RemoveFirst()
	{
		LoopGridViewItem result = mFirst;
		if ((Object)(object)mFirst == (Object)null)
		{
			return result;
		}
		if ((Object)(object)mFirst == (Object)(object)mLast)
		{
			mFirst = null;
			mLast = null;
			mCount--;
			return result;
		}
		mFirst = mFirst.NextItem;
		mFirst.PrevItem = null;
		mCount--;
		return result;
	}

	public LoopGridViewItem RemoveLast()
	{
		LoopGridViewItem result = mLast;
		if ((Object)(object)mFirst == (Object)null)
		{
			return result;
		}
		if ((Object)(object)mFirst == (Object)(object)mLast)
		{
			mFirst = null;
			mLast = null;
			mCount--;
			return result;
		}
		mLast = mLast.PrevItem;
		mLast.NextItem = null;
		mCount--;
		return result;
	}

	public void Clear()
	{
		LoopGridViewItem nextItem = mFirst;
		while ((Object)(object)nextItem != (Object)null)
		{
			nextItem.PrevItem = null;
			nextItem.NextItem = null;
			nextItem = nextItem.NextItem;
		}
		mFirst = null;
		mLast = null;
		mCount = 0;
	}
}
