using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Wrap Content")]
public class UIWrapContent : MonoBehaviour
{
	public delegate void OnInitializeItem(GameObject go, int wrapIndex, int realIndex);

	public int itemSize = 100;

	public bool cullContent = true;

	public OnInitializeItem onInitializeItem;

	private Transform mTrans;

	private UIPanel mPanel;

	private UIScrollView mScroll;

	private bool mHorizontal;

	private bool mFirstTime = true;

	private BetterList<Transform> mChildren = new BetterList<Transform>();

	protected virtual void Start()
	{
		SortBasedOnScrollMovement();
		WrapContent();
		if ((Object)(object)mScroll != (Object)null)
		{
			((Component)mScroll).GetComponent<UIPanel>().onClipMove = OnMove;
			mScroll.restrictWithinPanel = false;
			if (mScroll.dragEffect == UIScrollView.DragEffect.MomentumAndSpring)
			{
				mScroll.dragEffect = UIScrollView.DragEffect.Momentum;
			}
		}
		mFirstTime = false;
	}

	protected virtual void OnMove(UIPanel panel)
	{
		WrapContent();
	}

	[ContextMenu("Sort Based on Scroll Movement")]
	public void SortBasedOnScrollMovement()
	{
		if (CacheScrollView())
		{
			mChildren.Clear();
			for (int i = 0; i < mTrans.childCount; i++)
			{
				mChildren.Add(mTrans.GetChild(i));
			}
			if (mHorizontal)
			{
				mChildren.Sort(UIGrid.SortHorizontal);
			}
			else
			{
				mChildren.Sort(UIGrid.SortVertical);
			}
			ResetChildPositions();
		}
	}

	[ContextMenu("Sort Alphabetically")]
	public void SortAlphabetically()
	{
		if (CacheScrollView())
		{
			mChildren.Clear();
			for (int i = 0; i < mTrans.childCount; i++)
			{
				mChildren.Add(mTrans.GetChild(i));
			}
			mChildren.Sort(UIGrid.SortByName);
			ResetChildPositions();
		}
	}

	protected bool CacheScrollView()
	{
		mTrans = ((Component)this).transform;
		mPanel = NGUITools.FindInParents<UIPanel>(((Component)this).gameObject);
		mScroll = ((Component)mPanel).GetComponent<UIScrollView>();
		if ((Object)(object)mScroll == (Object)null)
		{
			return false;
		}
		if (mScroll.movement == UIScrollView.Movement.Horizontal)
		{
			mHorizontal = true;
		}
		else
		{
			if (mScroll.movement != UIScrollView.Movement.Vertical)
			{
				return false;
			}
			mHorizontal = false;
		}
		return true;
	}

	private void ResetChildPositions()
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < mChildren.size; i++)
		{
			mChildren[i].localPosition = (mHorizontal ? new Vector3((float)(i * itemSize), 0f, 0f) : new Vector3(0f, (float)(-i * itemSize), 0f));
		}
	}

	public void WrapContent()
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		float num = (float)(itemSize * mChildren.size) * 0.5f;
		Vector3[] worldCorners = mPanel.worldCorners;
		for (int i = 0; i < 4; i++)
		{
			Vector3 val = worldCorners[i];
			val = mTrans.InverseTransformPoint(val);
			worldCorners[i] = val;
		}
		Vector3 val2 = Vector3.Lerp(worldCorners[0], worldCorners[2], 0.5f);
		if (mHorizontal)
		{
			float num2 = worldCorners[0].x - (float)itemSize;
			float num3 = worldCorners[2].x + (float)itemSize;
			for (int j = 0; j < mChildren.size; j++)
			{
				Transform val3 = mChildren[j];
				float num4 = val3.localPosition.x - val2.x;
				if (num4 < 0f - num)
				{
					val3.localPosition += new Vector3(num * 2f, 0f, 0f);
					num4 = val3.localPosition.x - val2.x;
					UpdateItem(val3, j);
				}
				else if (num4 > num)
				{
					val3.localPosition -= new Vector3(num * 2f, 0f, 0f);
					num4 = val3.localPosition.x - val2.x;
					UpdateItem(val3, j);
				}
				else if (mFirstTime)
				{
					UpdateItem(val3, j);
				}
				if (cullContent)
				{
					num4 += mPanel.clipOffset.x - mTrans.localPosition.x;
					if (!UICamera.IsPressed(((Component)val3).gameObject))
					{
						NGUITools.SetActive(((Component)val3).gameObject, num4 > num2 && num4 < num3, compatibilityMode: false);
					}
				}
			}
			return;
		}
		float num5 = worldCorners[0].y - (float)itemSize;
		float num6 = worldCorners[2].y + (float)itemSize;
		for (int k = 0; k < mChildren.size; k++)
		{
			Transform val4 = mChildren[k];
			float num7 = val4.localPosition.y - val2.y;
			if (num7 < 0f - num)
			{
				val4.localPosition += new Vector3(0f, num * 2f, 0f);
				num7 = val4.localPosition.y - val2.y;
				UpdateItem(val4, k);
			}
			else if (num7 > num)
			{
				val4.localPosition -= new Vector3(0f, num * 2f, 0f);
				num7 = val4.localPosition.y - val2.y;
				UpdateItem(val4, k);
			}
			else if (mFirstTime)
			{
				UpdateItem(val4, k);
			}
			if (cullContent)
			{
				num7 += mPanel.clipOffset.y - mTrans.localPosition.y;
				if (!UICamera.IsPressed(((Component)val4).gameObject))
				{
					NGUITools.SetActive(((Component)val4).gameObject, num7 > num5 && num7 < num6, compatibilityMode: false);
				}
			}
		}
	}

	protected virtual void UpdateItem(Transform item, int index)
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		if (onInitializeItem != null)
		{
			int realIndex = ((mScroll.movement == UIScrollView.Movement.Vertical) ? Mathf.RoundToInt(item.localPosition.y / (float)itemSize) : Mathf.RoundToInt(item.localPosition.x / (float)itemSize));
			onInitializeItem(((Component)item).gameObject, index, realIndex);
		}
	}
}
