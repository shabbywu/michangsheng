using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020004C7 RID: 1223
public class AutoGridSVContentHeight : MonoBehaviour
{
	// Token: 0x06002028 RID: 8232 RVA: 0x0001A5DC File Offset: 0x000187DC
	private void Start()
	{
		this.rt = (base.transform as RectTransform);
		this.grid = base.GetComponent<GridLayoutGroup>();
	}

	// Token: 0x06002029 RID: 8233 RVA: 0x00112C00 File Offset: 0x00110E00
	private void Update()
	{
		int childCount = this.rt.childCount;
		if (this.ChildCountMode && this.lastChildCount == childCount)
		{
			return;
		}
		int num = childCount / this.ColCount;
		if (childCount % this.ColCount > 0)
		{
			num++;
		}
		float num2 = (float)num * (this.grid.cellSize.y + this.grid.spacing.y);
		this.lastChildCount = childCount;
		if (this.rt.sizeDelta.y != num2 + (float)this.ExHeigt)
		{
			this.rt.sizeDelta = new Vector2(this.rt.sizeDelta.x, num2 + (float)this.ExHeigt);
			this.rt.anchoredPosition = new Vector2(this.rt.anchoredPosition.x, 0f);
			if (this.grid != null && this.IsLimitSliding)
			{
				if (num2 < (this.SR.transform as RectTransform).sizeDelta.y)
				{
					this.SR.vertical = false;
					return;
				}
				this.SR.vertical = true;
			}
		}
	}

	// Token: 0x04001B9C RID: 7068
	private GridLayoutGroup grid;

	// Token: 0x04001B9D RID: 7069
	private RectTransform rt;

	// Token: 0x04001B9E RID: 7070
	private int lastChildCount;

	// Token: 0x04001B9F RID: 7071
	[Header("每行个数")]
	public int ColCount;

	// Token: 0x04001BA0 RID: 7072
	[Header("额外高度")]
	public int ExHeigt;

	// Token: 0x04001BA1 RID: 7073
	[Header("是否现在滑动")]
	[Tooltip("当子物体高度小于SV高度时，禁止滑动")]
	public bool IsLimitSliding;

	// Token: 0x04001BA2 RID: 7074
	public ScrollRect SR;

	// Token: 0x04001BA3 RID: 7075
	[Tooltip("根据物体数量的变动进行高度刷新。关闭的话会一直检测")]
	public bool ChildCountMode = true;
}
