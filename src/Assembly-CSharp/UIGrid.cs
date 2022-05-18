using System;
using UnityEngine;

// Token: 0x02000086 RID: 134
[AddComponentMenu("NGUI/Interaction/Grid")]
public class UIGrid : UIWidgetContainer
{
	// Token: 0x17000093 RID: 147
	// (set) Token: 0x0600056F RID: 1391 RVA: 0x00008F1A File Offset: 0x0000711A
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

	// Token: 0x06000570 RID: 1392 RVA: 0x00072724 File Offset: 0x00070924
	public BetterList<Transform> GetChildList()
	{
		Transform transform = base.transform;
		BetterList<Transform> betterList = new BetterList<Transform>();
		for (int i = 0; i < transform.childCount; i++)
		{
			Transform child = transform.GetChild(i);
			if (!this.hideInactive || (child && NGUITools.GetActive(child.gameObject)))
			{
				betterList.Add(child);
			}
		}
		if (this.sorting != UIGrid.Sorting.None)
		{
			if (this.sorting == UIGrid.Sorting.Alphabetic)
			{
				betterList.Sort(new BetterList<Transform>.CompareFunc(UIGrid.SortByName));
			}
			else if (this.sorting == UIGrid.Sorting.Horizontal)
			{
				betterList.Sort(new BetterList<Transform>.CompareFunc(UIGrid.SortHorizontal));
			}
			else if (this.sorting == UIGrid.Sorting.Vertical)
			{
				betterList.Sort(new BetterList<Transform>.CompareFunc(UIGrid.SortVertical));
			}
			else if (this.onCustomSort != null)
			{
				betterList.Sort(this.onCustomSort);
			}
			else
			{
				this.Sort(betterList);
			}
		}
		return betterList;
	}

	// Token: 0x06000571 RID: 1393 RVA: 0x000727F8 File Offset: 0x000709F8
	public Transform GetChild(int index)
	{
		BetterList<Transform> childList = this.GetChildList();
		if (index >= childList.size)
		{
			return null;
		}
		return childList[index];
	}

	// Token: 0x06000572 RID: 1394 RVA: 0x00008F2D File Offset: 0x0000712D
	public int GetIndex(Transform trans)
	{
		return this.GetChildList().IndexOf(trans);
	}

	// Token: 0x06000573 RID: 1395 RVA: 0x00008F3B File Offset: 0x0000713B
	public void AddChild(Transform trans)
	{
		this.AddChild(trans, true);
	}

	// Token: 0x06000574 RID: 1396 RVA: 0x00008F45 File Offset: 0x00007145
	public void AddChild(Transform trans, bool sort)
	{
		if (trans != null)
		{
			trans.parent = base.transform;
			this.ResetPosition(this.GetChildList());
		}
	}

	// Token: 0x06000575 RID: 1397 RVA: 0x00072820 File Offset: 0x00070A20
	public bool RemoveChild(Transform t)
	{
		BetterList<Transform> childList = this.GetChildList();
		if (childList.Remove(t))
		{
			this.ResetPosition(childList);
			return true;
		}
		return false;
	}

	// Token: 0x06000576 RID: 1398 RVA: 0x00008F68 File Offset: 0x00007168
	protected virtual void Init()
	{
		this.mInitDone = true;
		this.mPanel = NGUITools.FindInParents<UIPanel>(base.gameObject);
	}

	// Token: 0x06000577 RID: 1399 RVA: 0x00072848 File Offset: 0x00070A48
	protected virtual void Start()
	{
		if (!this.mInitDone)
		{
			this.Init();
		}
		bool flag = this.animateSmoothly;
		this.animateSmoothly = false;
		this.Reposition();
		this.animateSmoothly = flag;
		base.enabled = false;
	}

	// Token: 0x06000578 RID: 1400 RVA: 0x00008F82 File Offset: 0x00007182
	protected virtual void Update()
	{
		if (this.mReposition)
		{
			this.Reposition();
		}
		base.enabled = false;
	}

	// Token: 0x06000579 RID: 1401 RVA: 0x00008F99 File Offset: 0x00007199
	public static int SortByName(Transform a, Transform b)
	{
		return string.Compare(a.name, b.name);
	}

	// Token: 0x0600057A RID: 1402 RVA: 0x00072888 File Offset: 0x00070A88
	public static int SortHorizontal(Transform a, Transform b)
	{
		return a.localPosition.x.CompareTo(b.localPosition.x);
	}

	// Token: 0x0600057B RID: 1403 RVA: 0x000728B4 File Offset: 0x00070AB4
	public static int SortVertical(Transform a, Transform b)
	{
		return b.localPosition.y.CompareTo(a.localPosition.y);
	}

	// Token: 0x0600057C RID: 1404 RVA: 0x000042DD File Offset: 0x000024DD
	protected virtual void Sort(BetterList<Transform> list)
	{
	}

	// Token: 0x0600057D RID: 1405 RVA: 0x000728E0 File Offset: 0x00070AE0
	[ContextMenu("Execute")]
	public virtual void Reposition()
	{
		if (Application.isPlaying && !this.mInitDone && NGUITools.GetActive(this))
		{
			this.mReposition = true;
			return;
		}
		if (this.sorted)
		{
			this.sorted = false;
			if (this.sorting == UIGrid.Sorting.None)
			{
				this.sorting = UIGrid.Sorting.Alphabetic;
			}
			NGUITools.SetDirty(this);
		}
		if (!this.mInitDone)
		{
			this.Init();
		}
		BetterList<Transform> childList = this.GetChildList();
		this.ResetPosition(childList);
		if (this.keepWithinPanel)
		{
			this.ConstrainWithinPanel();
		}
		if (this.onReposition != null)
		{
			this.onReposition();
		}
	}

	// Token: 0x0600057E RID: 1406 RVA: 0x00008FAC File Offset: 0x000071AC
	public void ConstrainWithinPanel()
	{
		if (this.mPanel != null)
		{
			this.mPanel.ConstrainTargetToBounds(base.transform, true);
		}
	}

	// Token: 0x0600057F RID: 1407 RVA: 0x00072970 File Offset: 0x00070B70
	protected void ResetPosition(BetterList<Transform> list)
	{
		this.mReposition = false;
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		Transform transform = base.transform;
		int i = 0;
		int size = list.size;
		while (i < size)
		{
			Transform transform2 = list[i];
			float z = transform2.localPosition.z;
			Vector3 vector = (this.arrangement == UIGrid.Arrangement.Horizontal) ? new Vector3(this.cellWidth * (float)num, -this.cellHeight * (float)num2, z) : new Vector3(this.cellWidth * (float)num2, -this.cellHeight * (float)num, z);
			if (this.animateSmoothly && Application.isPlaying)
			{
				SpringPosition springPosition = SpringPosition.Begin(transform2.gameObject, vector, 15f);
				springPosition.updateScrollView = true;
				springPosition.ignoreTimeScale = true;
			}
			else
			{
				transform2.localPosition = vector;
			}
			num3 = Mathf.Max(num3, num);
			num4 = Mathf.Max(num4, num2);
			if (++num >= this.maxPerLine && this.maxPerLine > 0)
			{
				num = 0;
				num2++;
			}
			i++;
		}
		if (this.pivot != UIWidget.Pivot.TopLeft)
		{
			Vector2 pivotOffset = NGUIMath.GetPivotOffset(this.pivot);
			float num5;
			float num6;
			if (this.arrangement == UIGrid.Arrangement.Horizontal)
			{
				num5 = Mathf.Lerp(0f, (float)num3 * this.cellWidth, pivotOffset.x);
				num6 = Mathf.Lerp((float)(-(float)num4) * this.cellHeight, 0f, pivotOffset.y);
			}
			else
			{
				num5 = Mathf.Lerp(0f, (float)num4 * this.cellWidth, pivotOffset.x);
				num6 = Mathf.Lerp((float)(-(float)num3) * this.cellHeight, 0f, pivotOffset.y);
			}
			for (int j = 0; j < transform.childCount; j++)
			{
				Transform child = transform.GetChild(j);
				SpringPosition component = child.GetComponent<SpringPosition>();
				if (component != null)
				{
					SpringPosition springPosition2 = component;
					springPosition2.target.x = springPosition2.target.x - num5;
					SpringPosition springPosition3 = component;
					springPosition3.target.y = springPosition3.target.y - num6;
				}
				else
				{
					Vector3 localPosition = child.localPosition;
					localPosition.x -= num5;
					localPosition.y -= num6;
					child.localPosition = localPosition;
				}
			}
		}
	}

	// Token: 0x040003D1 RID: 977
	public UIGrid.Arrangement arrangement;

	// Token: 0x040003D2 RID: 978
	public UIGrid.Sorting sorting;

	// Token: 0x040003D3 RID: 979
	public UIWidget.Pivot pivot;

	// Token: 0x040003D4 RID: 980
	public int maxPerLine;

	// Token: 0x040003D5 RID: 981
	public float cellWidth = 200f;

	// Token: 0x040003D6 RID: 982
	public float cellHeight = 200f;

	// Token: 0x040003D7 RID: 983
	public bool animateSmoothly;

	// Token: 0x040003D8 RID: 984
	public bool hideInactive = true;

	// Token: 0x040003D9 RID: 985
	public bool keepWithinPanel;

	// Token: 0x040003DA RID: 986
	public UIGrid.OnReposition onReposition;

	// Token: 0x040003DB RID: 987
	public BetterList<Transform>.CompareFunc onCustomSort;

	// Token: 0x040003DC RID: 988
	[HideInInspector]
	[SerializeField]
	private bool sorted;

	// Token: 0x040003DD RID: 989
	protected bool mReposition;

	// Token: 0x040003DE RID: 990
	protected UIPanel mPanel;

	// Token: 0x040003DF RID: 991
	protected bool mInitDone;

	// Token: 0x02000087 RID: 135
	// (Invoke) Token: 0x06000582 RID: 1410
	public delegate void OnReposition();

	// Token: 0x02000088 RID: 136
	public enum Arrangement
	{
		// Token: 0x040003E1 RID: 993
		Horizontal,
		// Token: 0x040003E2 RID: 994
		Vertical
	}

	// Token: 0x02000089 RID: 137
	public enum Sorting
	{
		// Token: 0x040003E4 RID: 996
		None,
		// Token: 0x040003E5 RID: 997
		Alphabetic,
		// Token: 0x040003E6 RID: 998
		Horizontal,
		// Token: 0x040003E7 RID: 999
		Vertical,
		// Token: 0x040003E8 RID: 1000
		Custom
	}
}
