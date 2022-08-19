using System;
using UnityEngine;

// Token: 0x02000420 RID: 1056
public class setBtnLabelShawColor : MonoBehaviour
{
	// Token: 0x060021DF RID: 8671 RVA: 0x000E9DC7 File Offset: 0x000E7FC7
	private void Start()
	{
		this.label = base.GetComponentInChildren<UILabel>();
	}

	// Token: 0x060021E0 RID: 8672 RVA: 0x000E9DD5 File Offset: 0x000E7FD5
	public void OnHover(bool isOver)
	{
		if (isOver)
		{
			this.label.effectColor = this.hoverColor;
			return;
		}
		this.label.effectColor = this.hoverColorStart;
	}

	// Token: 0x04001B54 RID: 6996
	public Color hoverColor;

	// Token: 0x04001B55 RID: 6997
	public Color hoverColorStart;

	// Token: 0x04001B56 RID: 6998
	private UILabel label;
}
