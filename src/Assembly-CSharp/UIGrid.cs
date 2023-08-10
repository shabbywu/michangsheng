using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Grid")]
public class UIGrid : UIWidgetContainer
{
	public delegate void OnReposition();

	public enum Arrangement
	{
		Horizontal,
		Vertical
	}

	public enum Sorting
	{
		None,
		Alphabetic,
		Horizontal,
		Vertical,
		Custom
	}

	public Arrangement arrangement;

	public Sorting sorting;

	public UIWidget.Pivot pivot;

	public int maxPerLine;

	public float cellWidth = 200f;

	public float cellHeight = 200f;

	public bool animateSmoothly;

	public bool hideInactive = true;

	public bool keepWithinPanel;

	public OnReposition onReposition;

	public BetterList<Transform>.CompareFunc onCustomSort;

	[HideInInspector]
	[SerializeField]
	private bool sorted;

	protected bool mReposition;

	protected UIPanel mPanel;

	protected bool mInitDone;

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

	public BetterList<Transform> GetChildList()
	{
		Transform transform = ((Component)this).transform;
		BetterList<Transform> betterList = new BetterList<Transform>();
		for (int i = 0; i < transform.childCount; i++)
		{
			Transform child = transform.GetChild(i);
			if (!hideInactive || (Object.op_Implicit((Object)(object)child) && NGUITools.GetActive(((Component)child).gameObject)))
			{
				betterList.Add(child);
			}
		}
		if (sorting != 0)
		{
			if (sorting == Sorting.Alphabetic)
			{
				betterList.Sort(SortByName);
			}
			else if (sorting == Sorting.Horizontal)
			{
				betterList.Sort(SortHorizontal);
			}
			else if (sorting == Sorting.Vertical)
			{
				betterList.Sort(SortVertical);
			}
			else if (onCustomSort != null)
			{
				betterList.Sort(onCustomSort);
			}
			else
			{
				Sort(betterList);
			}
		}
		return betterList;
	}

	public Transform GetChild(int index)
	{
		BetterList<Transform> childList = GetChildList();
		if (index >= childList.size)
		{
			return null;
		}
		return childList[index];
	}

	public int GetIndex(Transform trans)
	{
		return GetChildList().IndexOf(trans);
	}

	public void AddChild(Transform trans)
	{
		AddChild(trans, sort: true);
	}

	public void AddChild(Transform trans, bool sort)
	{
		if ((Object)(object)trans != (Object)null)
		{
			trans.parent = ((Component)this).transform;
			ResetPosition(GetChildList());
		}
	}

	public bool RemoveChild(Transform t)
	{
		BetterList<Transform> childList = GetChildList();
		if (childList.Remove(t))
		{
			ResetPosition(childList);
			return true;
		}
		return false;
	}

	protected virtual void Init()
	{
		mInitDone = true;
		mPanel = NGUITools.FindInParents<UIPanel>(((Component)this).gameObject);
	}

	protected virtual void Start()
	{
		if (!mInitDone)
		{
			Init();
		}
		bool flag = animateSmoothly;
		animateSmoothly = false;
		Reposition();
		animateSmoothly = flag;
		((Behaviour)this).enabled = false;
	}

	protected virtual void Update()
	{
		if (mReposition)
		{
			Reposition();
		}
		((Behaviour)this).enabled = false;
	}

	public static int SortByName(Transform a, Transform b)
	{
		return string.Compare(((Object)a).name, ((Object)b).name);
	}

	public static int SortHorizontal(Transform a, Transform b)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		return a.localPosition.x.CompareTo(b.localPosition.x);
	}

	public static int SortVertical(Transform a, Transform b)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		return b.localPosition.y.CompareTo(a.localPosition.y);
	}

	protected virtual void Sort(BetterList<Transform> list)
	{
	}

	[ContextMenu("Execute")]
	public virtual void Reposition()
	{
		if (Application.isPlaying && !mInitDone && NGUITools.GetActive((Behaviour)(object)this))
		{
			mReposition = true;
			return;
		}
		if (sorted)
		{
			sorted = false;
			if (sorting == Sorting.None)
			{
				sorting = Sorting.Alphabetic;
			}
			NGUITools.SetDirty((Object)(object)this);
		}
		if (!mInitDone)
		{
			Init();
		}
		BetterList<Transform> childList = GetChildList();
		ResetPosition(childList);
		if (keepWithinPanel)
		{
			ConstrainWithinPanel();
		}
		if (onReposition != null)
		{
			onReposition();
		}
	}

	public void ConstrainWithinPanel()
	{
		if ((Object)(object)mPanel != (Object)null)
		{
			mPanel.ConstrainTargetToBounds(((Component)this).transform, immediate: true);
		}
	}

	protected void ResetPosition(BetterList<Transform> list)
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		mReposition = false;
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		Transform transform = ((Component)this).transform;
		int i = 0;
		for (int size = list.size; i < size; i++)
		{
			Transform val = list[i];
			float z = val.localPosition.z;
			Vector3 val2 = ((arrangement == Arrangement.Horizontal) ? new Vector3(cellWidth * (float)num, (0f - cellHeight) * (float)num2, z) : new Vector3(cellWidth * (float)num2, (0f - cellHeight) * (float)num, z));
			if (animateSmoothly && Application.isPlaying)
			{
				SpringPosition springPosition = SpringPosition.Begin(((Component)val).gameObject, val2, 15f);
				springPosition.updateScrollView = true;
				springPosition.ignoreTimeScale = true;
			}
			else
			{
				val.localPosition = val2;
			}
			num3 = Mathf.Max(num3, num);
			num4 = Mathf.Max(num4, num2);
			if (++num >= maxPerLine && maxPerLine > 0)
			{
				num = 0;
				num2++;
			}
		}
		if (pivot == UIWidget.Pivot.TopLeft)
		{
			return;
		}
		Vector2 pivotOffset = NGUIMath.GetPivotOffset(pivot);
		float num5;
		float num6;
		if (arrangement == Arrangement.Horizontal)
		{
			num5 = Mathf.Lerp(0f, (float)num3 * cellWidth, pivotOffset.x);
			num6 = Mathf.Lerp((float)(-num4) * cellHeight, 0f, pivotOffset.y);
		}
		else
		{
			num5 = Mathf.Lerp(0f, (float)num4 * cellWidth, pivotOffset.x);
			num6 = Mathf.Lerp((float)(-num3) * cellHeight, 0f, pivotOffset.y);
		}
		for (int j = 0; j < transform.childCount; j++)
		{
			Transform child = transform.GetChild(j);
			SpringPosition component = ((Component)child).GetComponent<SpringPosition>();
			if ((Object)(object)component != (Object)null)
			{
				component.target.x -= num5;
				component.target.y -= num6;
				continue;
			}
			Vector3 localPosition = child.localPosition;
			localPosition.x -= num5;
			localPosition.y -= num6;
			child.localPosition = localPosition;
		}
	}
}
