using System;
using GUIPackage;
using UnityEngine;

// Token: 0x02000415 RID: 1045
public class UIInventoryGridData
{
	// Token: 0x06001C27 RID: 7207 RVA: 0x00017897 File Offset: 0x00015A97
	public void IconShowInit(UIIconShow iconShow)
	{
		if (this.IconShowInitAction != null)
		{
			this.BindShow = iconShow;
			(this.BindShow.transform as RectTransform).anchoredPosition = this.Pos;
			this.IconShowInitAction(iconShow);
		}
	}

	// Token: 0x04001824 RID: 6180
	public Vector2 Pos;

	// Token: 0x04001825 RID: 6181
	public int Index;

	// Token: 0x04001826 RID: 6182
	public item Item;

	// Token: 0x04001827 RID: 6183
	public int ItemCount;

	// Token: 0x04001828 RID: 6184
	public Action<UIIconShow> IconShowInitAction;

	// Token: 0x04001829 RID: 6185
	public UIIconShow BindShow;
}
