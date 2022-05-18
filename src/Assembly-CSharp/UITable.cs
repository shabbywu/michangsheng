using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000A6 RID: 166
[AddComponentMenu("NGUI/Interaction/Table")]
public class UITable : UIWidgetContainer
{
	// Token: 0x170000B9 RID: 185
	// (set) Token: 0x06000655 RID: 1621 RVA: 0x00009A34 File Offset: 0x00007C34
	public bool repositionNow
	{
		set
		{
			if (value)
			{
				this.mReposition = true;
				base.enabled = true;
			}
		}
	}

	// Token: 0x170000BA RID: 186
	// (get) Token: 0x06000656 RID: 1622 RVA: 0x00077264 File Offset: 0x00075464
	public List<Transform> children
	{
		get
		{
			if (this.mChildren.Count == 0)
			{
				Transform transform = base.transform;
				for (int i = 0; i < transform.childCount; i++)
				{
					Transform child = transform.GetChild(i);
					if (child && child.gameObject && (!this.hideInactive || NGUITools.GetActive(child.gameObject)))
					{
						this.mChildren.Add(child);
					}
				}
				if (this.sorting != UITable.Sorting.None || this.sorted)
				{
					if (this.sorting == UITable.Sorting.Alphabetic)
					{
						this.mChildren.Sort(new Comparison<Transform>(UIGrid.SortByName));
					}
					else if (this.sorting == UITable.Sorting.Horizontal)
					{
						this.mChildren.Sort(new Comparison<Transform>(UIGrid.SortHorizontal));
					}
					else if (this.sorting == UITable.Sorting.Vertical)
					{
						this.mChildren.Sort(new Comparison<Transform>(UIGrid.SortVertical));
					}
					else
					{
						this.Sort(this.mChildren);
					}
				}
			}
			return this.mChildren;
		}
	}

	// Token: 0x06000657 RID: 1623 RVA: 0x00009A47 File Offset: 0x00007C47
	protected virtual void Sort(List<Transform> list)
	{
		list.Sort(new Comparison<Transform>(UIGrid.SortByName));
	}

	// Token: 0x06000658 RID: 1624 RVA: 0x00077360 File Offset: 0x00075560
	protected void RepositionVariableSize(List<Transform> children)
	{
		float num = 0f;
		float num2 = 0f;
		object obj = (this.columns > 0) ? (children.Count / this.columns + 1) : 1;
		int num3 = (this.columns > 0) ? this.columns : children.Count;
		object obj2 = obj;
		Bounds[,] array = new Bounds[obj2, num3];
		Bounds[] array2 = new Bounds[num3];
		Bounds[] array3 = new Bounds[obj2];
		int num4 = 0;
		int num5 = 0;
		int i = 0;
		int count = children.Count;
		while (i < count)
		{
			Transform transform = children[i];
			Bounds bounds = NGUIMath.CalculateRelativeWidgetBounds(transform, !this.hideInactive);
			Vector3 localScale = transform.localScale;
			bounds.min = Vector3.Scale(bounds.min, localScale);
			bounds.max = Vector3.Scale(bounds.max, localScale);
			array[num5, num4] = bounds;
			array2[num4].Encapsulate(bounds);
			array3[num5].Encapsulate(bounds);
			if (++num4 >= this.columns && this.columns > 0)
			{
				num4 = 0;
				num5++;
			}
			i++;
		}
		num4 = 0;
		num5 = 0;
		int j = 0;
		int count2 = children.Count;
		while (j < count2)
		{
			Transform transform2 = children[j];
			Bounds bounds2 = array[num5, num4];
			Bounds bounds3 = array2[num4];
			Bounds bounds4 = array3[num5];
			Vector3 localPosition = transform2.localPosition;
			localPosition.x = num + bounds2.extents.x - bounds2.center.x;
			localPosition.x += bounds2.min.x - bounds3.min.x + this.padding.x;
			if (this.direction == UITable.Direction.Down)
			{
				localPosition.y = -num2 - bounds2.extents.y - bounds2.center.y;
				localPosition.y += (bounds2.max.y - bounds2.min.y - bounds4.max.y + bounds4.min.y) * 0.5f - this.padding.y;
			}
			else
			{
				localPosition.y = num2 + (bounds2.extents.y - bounds2.center.y);
				localPosition.y -= (bounds2.max.y - bounds2.min.y - bounds4.max.y + bounds4.min.y) * 0.5f - this.padding.y;
			}
			num += bounds3.max.x - bounds3.min.x + this.padding.x * 2f;
			transform2.localPosition = localPosition;
			if (++num4 >= this.columns && this.columns > 0)
			{
				num4 = 0;
				num5++;
				num = 0f;
				num2 += bounds4.size.y + this.padding.y * 2f;
			}
			j++;
		}
	}

	// Token: 0x06000659 RID: 1625 RVA: 0x00077694 File Offset: 0x00075894
	[ContextMenu("Execute")]
	public virtual void Reposition()
	{
		if (Application.isPlaying && !this.mInitDone && NGUITools.GetActive(this))
		{
			this.mReposition = true;
			return;
		}
		if (!this.mInitDone)
		{
			this.Init();
		}
		this.mReposition = false;
		Transform transform = base.transform;
		this.mChildren.Clear();
		List<Transform> children = this.children;
		if (children.Count > 0)
		{
			this.RepositionVariableSize(children);
		}
		if (this.keepWithinPanel && this.mPanel != null)
		{
			this.mPanel.ConstrainTargetToBounds(transform, true);
			UIScrollView component = this.mPanel.GetComponent<UIScrollView>();
			if (component != null)
			{
				component.UpdateScrollbars(true);
			}
		}
		if (this.onReposition != null)
		{
			this.onReposition();
		}
	}

	// Token: 0x0600065A RID: 1626 RVA: 0x00009A5B File Offset: 0x00007C5B
	protected virtual void Start()
	{
		this.Init();
		this.Reposition();
		base.enabled = false;
	}

	// Token: 0x0600065B RID: 1627 RVA: 0x00009A70 File Offset: 0x00007C70
	protected virtual void Init()
	{
		this.mInitDone = true;
		this.mPanel = NGUITools.FindInParents<UIPanel>(base.gameObject);
	}

	// Token: 0x0600065C RID: 1628 RVA: 0x00009A8A File Offset: 0x00007C8A
	protected virtual void LateUpdate()
	{
		if (this.mReposition)
		{
			this.Reposition();
		}
		base.enabled = false;
	}

	// Token: 0x040004B6 RID: 1206
	public int columns;

	// Token: 0x040004B7 RID: 1207
	public UITable.Direction direction;

	// Token: 0x040004B8 RID: 1208
	public UITable.Sorting sorting;

	// Token: 0x040004B9 RID: 1209
	public bool hideInactive = true;

	// Token: 0x040004BA RID: 1210
	public bool keepWithinPanel;

	// Token: 0x040004BB RID: 1211
	public Vector2 padding = Vector2.zero;

	// Token: 0x040004BC RID: 1212
	public UITable.OnReposition onReposition;

	// Token: 0x040004BD RID: 1213
	protected UIPanel mPanel;

	// Token: 0x040004BE RID: 1214
	protected bool mInitDone;

	// Token: 0x040004BF RID: 1215
	protected bool mReposition;

	// Token: 0x040004C0 RID: 1216
	protected List<Transform> mChildren = new List<Transform>();

	// Token: 0x040004C1 RID: 1217
	[HideInInspector]
	[SerializeField]
	private bool sorted;

	// Token: 0x020000A7 RID: 167
	// (Invoke) Token: 0x0600065F RID: 1631
	public delegate void OnReposition();

	// Token: 0x020000A8 RID: 168
	public enum Direction
	{
		// Token: 0x040004C3 RID: 1219
		Down,
		// Token: 0x040004C4 RID: 1220
		Up
	}

	// Token: 0x020000A9 RID: 169
	public enum Sorting
	{
		// Token: 0x040004C6 RID: 1222
		None,
		// Token: 0x040004C7 RID: 1223
		Alphabetic,
		// Token: 0x040004C8 RID: 1224
		Horizontal,
		// Token: 0x040004C9 RID: 1225
		Vertical,
		// Token: 0x040004CA RID: 1226
		Custom
	}
}
