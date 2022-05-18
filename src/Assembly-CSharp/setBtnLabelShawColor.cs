using System;
using UnityEngine;

// Token: 0x020005D4 RID: 1492
public class setBtnLabelShawColor : MonoBehaviour
{
	// Token: 0x06002599 RID: 9625 RVA: 0x0001E215 File Offset: 0x0001C415
	private void Start()
	{
		this.label = base.GetComponentInChildren<UILabel>();
	}

	// Token: 0x0600259A RID: 9626 RVA: 0x0001E223 File Offset: 0x0001C423
	public void OnHover(bool isOver)
	{
		if (isOver)
		{
			this.label.effectColor = this.hoverColor;
			return;
		}
		this.label.effectColor = this.hoverColorStart;
	}

	// Token: 0x0400201A RID: 8218
	public Color hoverColor;

	// Token: 0x0400201B RID: 8219
	public Color hoverColorStart;

	// Token: 0x0400201C RID: 8220
	private UILabel label;
}
