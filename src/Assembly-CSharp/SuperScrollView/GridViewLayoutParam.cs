using UnityEngine;

namespace SuperScrollView;

public class GridViewLayoutParam
{
	public int mColumnOrRowCount;

	public float mItemWidthOrHeight;

	public float mPadding1;

	public float mPadding2;

	public float[] mCustomColumnOrRowOffsetArray;

	public bool CheckParam()
	{
		if (mColumnOrRowCount <= 0)
		{
			Debug.LogError((object)"mColumnOrRowCount shoud be > 0");
			return false;
		}
		if (mItemWidthOrHeight <= 0f)
		{
			Debug.LogError((object)"mItemWidthOrHeight shoud be > 0");
			return false;
		}
		if (mCustomColumnOrRowOffsetArray != null && mCustomColumnOrRowOffsetArray.Length != mColumnOrRowCount)
		{
			Debug.LogError((object)"mGroupOffsetArray.Length != mColumnOrRowCount");
			return false;
		}
		return true;
	}
}
