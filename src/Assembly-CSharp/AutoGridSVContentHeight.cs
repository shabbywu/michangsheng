using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200034B RID: 843
public class AutoGridSVContentHeight : MonoBehaviour
{
	// Token: 0x06001CC1 RID: 7361 RVA: 0x000CDB27 File Offset: 0x000CBD27
	private void Start()
	{
		this.rt = (base.transform as RectTransform);
		this.grid = base.GetComponent<GridLayoutGroup>();
	}

	// Token: 0x06001CC2 RID: 7362 RVA: 0x000CDB48 File Offset: 0x000CBD48
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

	// Token: 0x04001746 RID: 5958
	private GridLayoutGroup grid;

	// Token: 0x04001747 RID: 5959
	private RectTransform rt;

	// Token: 0x04001748 RID: 5960
	private int lastChildCount;

	// Token: 0x04001749 RID: 5961
	[Header("每行个数")]
	public int ColCount;

	// Token: 0x0400174A RID: 5962
	[Header("额外高度")]
	public int ExHeigt;

	// Token: 0x0400174B RID: 5963
	[Header("是否现在滑动")]
	[Tooltip("当子物体高度小于SV高度时，禁止滑动")]
	public bool IsLimitSliding;

	// Token: 0x0400174C RID: 5964
	public ScrollRect SR;

	// Token: 0x0400174D RID: 5965
	[Tooltip("根据物体数量的变动进行高度刷新。关闭的话会一直检测")]
	public bool ChildCountMode = true;
}
