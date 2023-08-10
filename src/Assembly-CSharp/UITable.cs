using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Table")]
public class UITable : UIWidgetContainer
{
	public delegate void OnReposition();

	public enum Direction
	{
		Down,
		Up
	}

	public enum Sorting
	{
		None,
		Alphabetic,
		Horizontal,
		Vertical,
		Custom
	}

	public int columns;

	public Direction direction;

	public Sorting sorting;

	public bool hideInactive = true;

	public bool keepWithinPanel;

	public Vector2 padding = Vector2.zero;

	public OnReposition onReposition;

	protected UIPanel mPanel;

	protected bool mInitDone;

	protected bool mReposition;

	protected List<Transform> mChildren = new List<Transform>();

	[HideInInspector]
	[SerializeField]
	private bool sorted;

	public bool repositionNow
	{
		set
		{
			if (value)
			{
				mReposition = true;
				((Behaviour)this).enabled = true;
			}
		}
	}

	public List<Transform> children
	{
		get
		{
			if (mChildren.Count == 0)
			{
				Transform transform = ((Component)this).transform;
				for (int i = 0; i < transform.childCount; i++)
				{
					Transform child = transform.GetChild(i);
					if (Object.op_Implicit((Object)(object)child) && Object.op_Implicit((Object)(object)((Component)child).gameObject) && (!hideInactive || NGUITools.GetActive(((Component)child).gameObject)))
					{
						mChildren.Add(child);
					}
				}
				if (sorting != 0 || sorted)
				{
					if (sorting == Sorting.Alphabetic)
					{
						mChildren.Sort(UIGrid.SortByName);
					}
					else if (sorting == Sorting.Horizontal)
					{
						mChildren.Sort(UIGrid.SortHorizontal);
					}
					else if (sorting == Sorting.Vertical)
					{
						mChildren.Sort(UIGrid.SortVertical);
					}
					else
					{
						Sort(mChildren);
					}
				}
			}
			return mChildren;
		}
	}

	protected virtual void Sort(List<Transform> list)
	{
		list.Sort(UIGrid.SortByName);
	}

	protected void RepositionVariableSize(List<Transform> children)
	{
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		float num = 0f;
		float num2 = 0f;
		int num3 = ((columns <= 0) ? 1 : (children.Count / columns + 1));
		int num4 = ((columns > 0) ? columns : children.Count);
		Bounds[,] array = new Bounds[num3, num4];
		Bounds[] array2 = (Bounds[])(object)new Bounds[num4];
		Bounds[] array3 = (Bounds[])(object)new Bounds[num3];
		int num5 = 0;
		int num6 = 0;
		int i = 0;
		for (int count = children.Count; i < count; i++)
		{
			Transform obj = children[i];
			Bounds val = NGUIMath.CalculateRelativeWidgetBounds(obj, !hideInactive);
			Vector3 localScale = obj.localScale;
			((Bounds)(ref val)).min = Vector3.Scale(((Bounds)(ref val)).min, localScale);
			((Bounds)(ref val)).max = Vector3.Scale(((Bounds)(ref val)).max, localScale);
			array[num6, num5] = val;
			((Bounds)(ref array2[num5])).Encapsulate(val);
			((Bounds)(ref array3[num6])).Encapsulate(val);
			if (++num5 >= columns && columns > 0)
			{
				num5 = 0;
				num6++;
			}
		}
		num5 = 0;
		num6 = 0;
		int j = 0;
		for (int count2 = children.Count; j < count2; j++)
		{
			Transform obj2 = children[j];
			Bounds val2 = array[num6, num5];
			Bounds val3 = array2[num5];
			Bounds val4 = array3[num6];
			Vector3 localPosition = obj2.localPosition;
			localPosition.x = num + ((Bounds)(ref val2)).extents.x - ((Bounds)(ref val2)).center.x;
			localPosition.x += ((Bounds)(ref val2)).min.x - ((Bounds)(ref val3)).min.x + padding.x;
			if (direction == Direction.Down)
			{
				localPosition.y = 0f - num2 - ((Bounds)(ref val2)).extents.y - ((Bounds)(ref val2)).center.y;
				localPosition.y += (((Bounds)(ref val2)).max.y - ((Bounds)(ref val2)).min.y - ((Bounds)(ref val4)).max.y + ((Bounds)(ref val4)).min.y) * 0.5f - padding.y;
			}
			else
			{
				localPosition.y = num2 + (((Bounds)(ref val2)).extents.y - ((Bounds)(ref val2)).center.y);
				localPosition.y -= (((Bounds)(ref val2)).max.y - ((Bounds)(ref val2)).min.y - ((Bounds)(ref val4)).max.y + ((Bounds)(ref val4)).min.y) * 0.5f - padding.y;
			}
			num += ((Bounds)(ref val3)).max.x - ((Bounds)(ref val3)).min.x + padding.x * 2f;
			obj2.localPosition = localPosition;
			if (++num5 >= columns && columns > 0)
			{
				num5 = 0;
				num6++;
				num = 0f;
				num2 += ((Bounds)(ref val4)).size.y + padding.y * 2f;
			}
		}
	}

	[ContextMenu("Execute")]
	public virtual void Reposition()
	{
		if (Application.isPlaying && !mInitDone && NGUITools.GetActive((Behaviour)(object)this))
		{
			mReposition = true;
			return;
		}
		if (!mInitDone)
		{
			Init();
		}
		mReposition = false;
		Transform transform = ((Component)this).transform;
		mChildren.Clear();
		List<Transform> list = children;
		if (list.Count > 0)
		{
			RepositionVariableSize(list);
		}
		if (keepWithinPanel && (Object)(object)mPanel != (Object)null)
		{
			mPanel.ConstrainTargetToBounds(transform, immediate: true);
			UIScrollView component = ((Component)mPanel).GetComponent<UIScrollView>();
			if ((Object)(object)component != (Object)null)
			{
				component.UpdateScrollbars(recalculateBounds: true);
			}
		}
		if (onReposition != null)
		{
			onReposition();
		}
	}

	protected virtual void Start()
	{
		Init();
		Reposition();
		((Behaviour)this).enabled = false;
	}

	protected virtual void Init()
	{
		mInitDone = true;
		mPanel = NGUITools.FindInParents<UIPanel>(((Component)this).gameObject);
	}

	protected virtual void LateUpdate()
	{
		if (mReposition)
		{
			Reposition();
		}
		((Behaviour)this).enabled = false;
	}
}
