using System;
using UnityEngine;

// Token: 0x0200007B RID: 123
[AddComponentMenu("NGUI/Interaction/Wrap Content")]
public class UIWrapContent : MonoBehaviour
{
	// Token: 0x0600060A RID: 1546 RVA: 0x000221DC File Offset: 0x000203DC
	protected virtual void Start()
	{
		this.SortBasedOnScrollMovement();
		this.WrapContent();
		if (this.mScroll != null)
		{
			this.mScroll.GetComponent<UIPanel>().onClipMove = new UIPanel.OnClippingMoved(this.OnMove);
			this.mScroll.restrictWithinPanel = false;
			if (this.mScroll.dragEffect == UIScrollView.DragEffect.MomentumAndSpring)
			{
				this.mScroll.dragEffect = UIScrollView.DragEffect.Momentum;
			}
		}
		this.mFirstTime = false;
	}

	// Token: 0x0600060B RID: 1547 RVA: 0x0002224D File Offset: 0x0002044D
	protected virtual void OnMove(UIPanel panel)
	{
		this.WrapContent();
	}

	// Token: 0x0600060C RID: 1548 RVA: 0x00022258 File Offset: 0x00020458
	[ContextMenu("Sort Based on Scroll Movement")]
	public void SortBasedOnScrollMovement()
	{
		if (!this.CacheScrollView())
		{
			return;
		}
		this.mChildren.Clear();
		for (int i = 0; i < this.mTrans.childCount; i++)
		{
			this.mChildren.Add(this.mTrans.GetChild(i));
		}
		if (this.mHorizontal)
		{
			this.mChildren.Sort(new BetterList<Transform>.CompareFunc(UIGrid.SortHorizontal));
		}
		else
		{
			this.mChildren.Sort(new BetterList<Transform>.CompareFunc(UIGrid.SortVertical));
		}
		this.ResetChildPositions();
	}

	// Token: 0x0600060D RID: 1549 RVA: 0x000222E4 File Offset: 0x000204E4
	[ContextMenu("Sort Alphabetically")]
	public void SortAlphabetically()
	{
		if (!this.CacheScrollView())
		{
			return;
		}
		this.mChildren.Clear();
		for (int i = 0; i < this.mTrans.childCount; i++)
		{
			this.mChildren.Add(this.mTrans.GetChild(i));
		}
		this.mChildren.Sort(new BetterList<Transform>.CompareFunc(UIGrid.SortByName));
		this.ResetChildPositions();
	}

	// Token: 0x0600060E RID: 1550 RVA: 0x00022350 File Offset: 0x00020550
	protected bool CacheScrollView()
	{
		this.mTrans = base.transform;
		this.mPanel = NGUITools.FindInParents<UIPanel>(base.gameObject);
		this.mScroll = this.mPanel.GetComponent<UIScrollView>();
		if (this.mScroll == null)
		{
			return false;
		}
		if (this.mScroll.movement == UIScrollView.Movement.Horizontal)
		{
			this.mHorizontal = true;
		}
		else
		{
			if (this.mScroll.movement != UIScrollView.Movement.Vertical)
			{
				return false;
			}
			this.mHorizontal = false;
		}
		return true;
	}

	// Token: 0x0600060F RID: 1551 RVA: 0x000223CC File Offset: 0x000205CC
	private void ResetChildPositions()
	{
		for (int i = 0; i < this.mChildren.size; i++)
		{
			this.mChildren[i].localPosition = (this.mHorizontal ? new Vector3((float)(i * this.itemSize), 0f, 0f) : new Vector3(0f, (float)(-(float)i * this.itemSize), 0f));
		}
	}

	// Token: 0x06000610 RID: 1552 RVA: 0x0002243C File Offset: 0x0002063C
	public void WrapContent()
	{
		float num = (float)(this.itemSize * this.mChildren.size) * 0.5f;
		Vector3[] worldCorners = this.mPanel.worldCorners;
		for (int i = 0; i < 4; i++)
		{
			Vector3 vector = worldCorners[i];
			vector = this.mTrans.InverseTransformPoint(vector);
			worldCorners[i] = vector;
		}
		Vector3 vector2 = Vector3.Lerp(worldCorners[0], worldCorners[2], 0.5f);
		if (this.mHorizontal)
		{
			float num2 = worldCorners[0].x - (float)this.itemSize;
			float num3 = worldCorners[2].x + (float)this.itemSize;
			for (int j = 0; j < this.mChildren.size; j++)
			{
				Transform transform = this.mChildren[j];
				float num4 = transform.localPosition.x - vector2.x;
				if (num4 < -num)
				{
					transform.localPosition += new Vector3(num * 2f, 0f, 0f);
					num4 = transform.localPosition.x - vector2.x;
					this.UpdateItem(transform, j);
				}
				else if (num4 > num)
				{
					transform.localPosition -= new Vector3(num * 2f, 0f, 0f);
					num4 = transform.localPosition.x - vector2.x;
					this.UpdateItem(transform, j);
				}
				else if (this.mFirstTime)
				{
					this.UpdateItem(transform, j);
				}
				if (this.cullContent)
				{
					num4 += this.mPanel.clipOffset.x - this.mTrans.localPosition.x;
					if (!UICamera.IsPressed(transform.gameObject))
					{
						NGUITools.SetActive(transform.gameObject, num4 > num2 && num4 < num3, false);
					}
				}
			}
			return;
		}
		float num5 = worldCorners[0].y - (float)this.itemSize;
		float num6 = worldCorners[2].y + (float)this.itemSize;
		for (int k = 0; k < this.mChildren.size; k++)
		{
			Transform transform2 = this.mChildren[k];
			float num7 = transform2.localPosition.y - vector2.y;
			if (num7 < -num)
			{
				transform2.localPosition += new Vector3(0f, num * 2f, 0f);
				num7 = transform2.localPosition.y - vector2.y;
				this.UpdateItem(transform2, k);
			}
			else if (num7 > num)
			{
				transform2.localPosition -= new Vector3(0f, num * 2f, 0f);
				num7 = transform2.localPosition.y - vector2.y;
				this.UpdateItem(transform2, k);
			}
			else if (this.mFirstTime)
			{
				this.UpdateItem(transform2, k);
			}
			if (this.cullContent)
			{
				num7 += this.mPanel.clipOffset.y - this.mTrans.localPosition.y;
				if (!UICamera.IsPressed(transform2.gameObject))
				{
					NGUITools.SetActive(transform2.gameObject, num7 > num5 && num7 < num6, false);
				}
			}
		}
	}

	// Token: 0x06000611 RID: 1553 RVA: 0x000227B0 File Offset: 0x000209B0
	protected virtual void UpdateItem(Transform item, int index)
	{
		if (this.onInitializeItem != null)
		{
			int realIndex = (this.mScroll.movement == UIScrollView.Movement.Vertical) ? Mathf.RoundToInt(item.localPosition.y / (float)this.itemSize) : Mathf.RoundToInt(item.localPosition.x / (float)this.itemSize);
			this.onInitializeItem(item.gameObject, index, realIndex);
		}
	}

	// Token: 0x04000413 RID: 1043
	public int itemSize = 100;

	// Token: 0x04000414 RID: 1044
	public bool cullContent = true;

	// Token: 0x04000415 RID: 1045
	public UIWrapContent.OnInitializeItem onInitializeItem;

	// Token: 0x04000416 RID: 1046
	private Transform mTrans;

	// Token: 0x04000417 RID: 1047
	private UIPanel mPanel;

	// Token: 0x04000418 RID: 1048
	private UIScrollView mScroll;

	// Token: 0x04000419 RID: 1049
	private bool mHorizontal;

	// Token: 0x0400041A RID: 1050
	private bool mFirstTime = true;

	// Token: 0x0400041B RID: 1051
	private BetterList<Transform> mChildren = new BetterList<Transform>();

	// Token: 0x020011F7 RID: 4599
	// (Invoke) Token: 0x06007823 RID: 30755
	public delegate void OnInitializeItem(GameObject go, int wrapIndex, int realIndex);
}
