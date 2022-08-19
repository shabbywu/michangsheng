using System;
using GUIPackage;
using UnityEngine;

// Token: 0x020002CA RID: 714
public class UIInventoryGridData
{
	// Token: 0x0600191F RID: 6431 RVA: 0x000B4BD5 File Offset: 0x000B2DD5
	public void IconShowInit(UIIconShow iconShow)
	{
		if (this.IconShowInitAction != null)
		{
			this.BindShow = iconShow;
			(this.BindShow.transform as RectTransform).anchoredPosition = this.Pos;
			this.IconShowInitAction(iconShow);
		}
	}

	// Token: 0x0400145A RID: 5210
	public Vector2 Pos;

	// Token: 0x0400145B RID: 5211
	public int Index;

	// Token: 0x0400145C RID: 5212
	public item Item;

	// Token: 0x0400145D RID: 5213
	public int ItemCount;

	// Token: 0x0400145E RID: 5214
	public Action<UIIconShow> IconShowInitAction;

	// Token: 0x0400145F RID: 5215
	public UIIconShow BindShow;
}
