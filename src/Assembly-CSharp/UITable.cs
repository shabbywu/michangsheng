using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000076 RID: 118
[AddComponentMenu("NGUI/Interaction/Table")]
public class UITable : UIWidgetContainer
{
	// Token: 0x170000A9 RID: 169
	// (set) Token: 0x060005ED RID: 1517 RVA: 0x0002165B File Offset: 0x0001F85B
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

	// Token: 0x170000AA RID: 170
	// (get) Token: 0x060005EE RID: 1518 RVA: 0x00021670 File Offset: 0x0001F870
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

	// Token: 0x060005EF RID: 1519 RVA: 0x0002176B File Offset: 0x0001F96B
	protected virtual void Sort(List<Transform> list)
	{
		list.Sort(new Comparison<Transform>(UIGrid.SortByName));
	}

	// Token: 0x060005F0 RID: 1520 RVA: 0x00021780 File Offset: 0x0001F980
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

	// Token: 0x060005F1 RID: 1521 RVA: 0x00021AB4 File Offset: 0x0001FCB4
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

	// Token: 0x060005F2 RID: 1522 RVA: 0x00021B71 File Offset: 0x0001FD71
	protected virtual void Start()
	{
		this.Init();
		this.Reposition();
		base.enabled = false;
	}

	// Token: 0x060005F3 RID: 1523 RVA: 0x00021B86 File Offset: 0x0001FD86
	protected virtual void Init()
	{
		this.mInitDone = true;
		this.mPanel = NGUITools.FindInParents<UIPanel>(base.gameObject);
	}

	// Token: 0x060005F4 RID: 1524 RVA: 0x00021BA0 File Offset: 0x0001FDA0
	protected virtual void LateUpdate()
	{
		if (this.mReposition)
		{
			this.Reposition();
		}
		base.enabled = false;
	}

	// Token: 0x040003EF RID: 1007
	public int columns;

	// Token: 0x040003F0 RID: 1008
	public UITable.Direction direction;

	// Token: 0x040003F1 RID: 1009
	public UITable.Sorting sorting;

	// Token: 0x040003F2 RID: 1010
	public bool hideInactive = true;

	// Token: 0x040003F3 RID: 1011
	public bool keepWithinPanel;

	// Token: 0x040003F4 RID: 1012
	public Vector2 padding = Vector2.zero;

	// Token: 0x040003F5 RID: 1013
	public UITable.OnReposition onReposition;

	// Token: 0x040003F6 RID: 1014
	protected UIPanel mPanel;

	// Token: 0x040003F7 RID: 1015
	protected bool mInitDone;

	// Token: 0x040003F8 RID: 1016
	protected bool mReposition;

	// Token: 0x040003F9 RID: 1017
	protected List<Transform> mChildren = new List<Transform>();

	// Token: 0x040003FA RID: 1018
	[HideInInspector]
	[SerializeField]
	private bool sorted;

	// Token: 0x020011F4 RID: 4596
	// (Invoke) Token: 0x0600781F RID: 30751
	public delegate void OnReposition();

	// Token: 0x020011F5 RID: 4597
	public enum Direction
	{
		// Token: 0x04006417 RID: 25623
		Down,
		// Token: 0x04006418 RID: 25624
		Up
	}

	// Token: 0x020011F6 RID: 4598
	public enum Sorting
	{
		// Token: 0x0400641A RID: 25626
		None,
		// Token: 0x0400641B RID: 25627
		Alphabetic,
		// Token: 0x0400641C RID: 25628
		Horizontal,
		// Token: 0x0400641D RID: 25629
		Vertical,
		// Token: 0x0400641E RID: 25630
		Custom
	}
}
