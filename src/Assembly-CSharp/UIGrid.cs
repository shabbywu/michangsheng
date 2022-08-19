using System;
using UnityEngine;

// Token: 0x02000068 RID: 104
[AddComponentMenu("NGUI/Interaction/Grid")]
public class UIGrid : UIWidgetContainer
{
	// Token: 0x17000085 RID: 133
	// (set) Token: 0x0600051D RID: 1309 RVA: 0x0001C0CD File Offset: 0x0001A2CD
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

	// Token: 0x0600051E RID: 1310 RVA: 0x0001C0E0 File Offset: 0x0001A2E0
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

	// Token: 0x0600051F RID: 1311 RVA: 0x0001C1B4 File Offset: 0x0001A3B4
	public Transform GetChild(int index)
	{
		BetterList<Transform> childList = this.GetChildList();
		if (index >= childList.size)
		{
			return null;
		}
		return childList[index];
	}

	// Token: 0x06000520 RID: 1312 RVA: 0x0001C1DA File Offset: 0x0001A3DA
	public int GetIndex(Transform trans)
	{
		return this.GetChildList().IndexOf(trans);
	}

	// Token: 0x06000521 RID: 1313 RVA: 0x0001C1E8 File Offset: 0x0001A3E8
	public void AddChild(Transform trans)
	{
		this.AddChild(trans, true);
	}

	// Token: 0x06000522 RID: 1314 RVA: 0x0001C1F2 File Offset: 0x0001A3F2
	public void AddChild(Transform trans, bool sort)
	{
		if (trans != null)
		{
			trans.parent = base.transform;
			this.ResetPosition(this.GetChildList());
		}
	}

	// Token: 0x06000523 RID: 1315 RVA: 0x0001C218 File Offset: 0x0001A418
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

	// Token: 0x06000524 RID: 1316 RVA: 0x0001C23F File Offset: 0x0001A43F
	protected virtual void Init()
	{
		this.mInitDone = true;
		this.mPanel = NGUITools.FindInParents<UIPanel>(base.gameObject);
	}

	// Token: 0x06000525 RID: 1317 RVA: 0x0001C25C File Offset: 0x0001A45C
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

	// Token: 0x06000526 RID: 1318 RVA: 0x0001C299 File Offset: 0x0001A499
	protected virtual void Update()
	{
		if (this.mReposition)
		{
			this.Reposition();
		}
		base.enabled = false;
	}

	// Token: 0x06000527 RID: 1319 RVA: 0x0001C2B0 File Offset: 0x0001A4B0
	public static int SortByName(Transform a, Transform b)
	{
		return string.Compare(a.name, b.name);
	}

	// Token: 0x06000528 RID: 1320 RVA: 0x0001C2C4 File Offset: 0x0001A4C4
	public static int SortHorizontal(Transform a, Transform b)
	{
		return a.localPosition.x.CompareTo(b.localPosition.x);
	}

	// Token: 0x06000529 RID: 1321 RVA: 0x0001C2F0 File Offset: 0x0001A4F0
	public static int SortVertical(Transform a, Transform b)
	{
		return b.localPosition.y.CompareTo(a.localPosition.y);
	}

	// Token: 0x0600052A RID: 1322 RVA: 0x00004095 File Offset: 0x00002295
	protected virtual void Sort(BetterList<Transform> list)
	{
	}

	// Token: 0x0600052B RID: 1323 RVA: 0x0001C31C File Offset: 0x0001A51C
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

	// Token: 0x0600052C RID: 1324 RVA: 0x0001C3A9 File Offset: 0x0001A5A9
	public void ConstrainWithinPanel()
	{
		if (this.mPanel != null)
		{
			this.mPanel.ConstrainTargetToBounds(base.transform, true);
		}
	}

	// Token: 0x0600052D RID: 1325 RVA: 0x0001C3CC File Offset: 0x0001A5CC
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

	// Token: 0x04000349 RID: 841
	public UIGrid.Arrangement arrangement;

	// Token: 0x0400034A RID: 842
	public UIGrid.Sorting sorting;

	// Token: 0x0400034B RID: 843
	public UIWidget.Pivot pivot;

	// Token: 0x0400034C RID: 844
	public int maxPerLine;

	// Token: 0x0400034D RID: 845
	public float cellWidth = 200f;

	// Token: 0x0400034E RID: 846
	public float cellHeight = 200f;

	// Token: 0x0400034F RID: 847
	public bool animateSmoothly;

	// Token: 0x04000350 RID: 848
	public bool hideInactive = true;

	// Token: 0x04000351 RID: 849
	public bool keepWithinPanel;

	// Token: 0x04000352 RID: 850
	public UIGrid.OnReposition onReposition;

	// Token: 0x04000353 RID: 851
	public BetterList<Transform>.CompareFunc onCustomSort;

	// Token: 0x04000354 RID: 852
	[HideInInspector]
	[SerializeField]
	private bool sorted;

	// Token: 0x04000355 RID: 853
	protected bool mReposition;

	// Token: 0x04000356 RID: 854
	protected UIPanel mPanel;

	// Token: 0x04000357 RID: 855
	protected bool mInitDone;

	// Token: 0x020011E2 RID: 4578
	// (Invoke) Token: 0x06007809 RID: 30729
	public delegate void OnReposition();

	// Token: 0x020011E3 RID: 4579
	public enum Arrangement
	{
		// Token: 0x040063D8 RID: 25560
		Horizontal,
		// Token: 0x040063D9 RID: 25561
		Vertical
	}

	// Token: 0x020011E4 RID: 4580
	public enum Sorting
	{
		// Token: 0x040063DB RID: 25563
		None,
		// Token: 0x040063DC RID: 25564
		Alphabetic,
		// Token: 0x040063DD RID: 25565
		Horizontal,
		// Token: 0x040063DE RID: 25566
		Vertical,
		// Token: 0x040063DF RID: 25567
		Custom
	}
}
