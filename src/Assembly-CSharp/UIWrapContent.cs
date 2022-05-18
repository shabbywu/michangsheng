using System;
using UnityEngine;

// Token: 0x020000AE RID: 174
[AddComponentMenu("NGUI/Interaction/Wrap Content")]
public class UIWrapContent : MonoBehaviour
{
	// Token: 0x06000676 RID: 1654 RVA: 0x00077C80 File Offset: 0x00075E80
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

	// Token: 0x06000677 RID: 1655 RVA: 0x00009B9B File Offset: 0x00007D9B
	protected virtual void OnMove(UIPanel panel)
	{
		this.WrapContent();
	}

	// Token: 0x06000678 RID: 1656 RVA: 0x00077CF4 File Offset: 0x00075EF4
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

	// Token: 0x06000679 RID: 1657 RVA: 0x00077D80 File Offset: 0x00075F80
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

	// Token: 0x0600067A RID: 1658 RVA: 0x00077DEC File Offset: 0x00075FEC
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

	// Token: 0x0600067B RID: 1659 RVA: 0x00077E68 File Offset: 0x00076068
	private void ResetChildPositions()
	{
		for (int i = 0; i < this.mChildren.size; i++)
		{
			this.mChildren[i].localPosition = (this.mHorizontal ? new Vector3((float)(i * this.itemSize), 0f, 0f) : new Vector3(0f, (float)(-(float)i * this.itemSize), 0f));
		}
	}

	// Token: 0x0600067C RID: 1660 RVA: 0x00077ED8 File Offset: 0x000760D8
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

	// Token: 0x0600067D RID: 1661 RVA: 0x0007824C File Offset: 0x0007644C
	protected virtual void UpdateItem(Transform item, int index)
	{
		if (this.onInitializeItem != null)
		{
			int realIndex = (this.mScroll.movement == UIScrollView.Movement.Vertical) ? Mathf.RoundToInt(item.localPosition.y / (float)this.itemSize) : Mathf.RoundToInt(item.localPosition.x / (float)this.itemSize);
			this.onInitializeItem(item.gameObject, index, realIndex);
		}
	}

	// Token: 0x040004E3 RID: 1251
	public int itemSize = 100;

	// Token: 0x040004E4 RID: 1252
	public bool cullContent = true;

	// Token: 0x040004E5 RID: 1253
	public UIWrapContent.OnInitializeItem onInitializeItem;

	// Token: 0x040004E6 RID: 1254
	private Transform mTrans;

	// Token: 0x040004E7 RID: 1255
	private UIPanel mPanel;

	// Token: 0x040004E8 RID: 1256
	private UIScrollView mScroll;

	// Token: 0x040004E9 RID: 1257
	private bool mHorizontal;

	// Token: 0x040004EA RID: 1258
	private bool mFirstTime = true;

	// Token: 0x040004EB RID: 1259
	private BetterList<Transform> mChildren = new BetterList<Transform>();

	// Token: 0x020000AF RID: 175
	// (Invoke) Token: 0x06000680 RID: 1664
	public delegate void OnInitializeItem(GameObject go, int wrapIndex, int realIndex);
}
