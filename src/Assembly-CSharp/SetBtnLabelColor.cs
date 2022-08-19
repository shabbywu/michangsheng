using System;
using UnityEngine;

// Token: 0x0200041F RID: 1055
public class SetBtnLabelColor : MonoBehaviour
{
	// Token: 0x060021DB RID: 8667 RVA: 0x000E9D66 File Offset: 0x000E7F66
	private void Start()
	{
		this.label = base.GetComponentInChildren<UILabel>();
	}

	// Token: 0x060021DC RID: 8668 RVA: 0x000E9D74 File Offset: 0x000E7F74
	public void OnHover(bool isOver)
	{
		if (isOver)
		{
			this.label.color = this.hoverColor;
			return;
		}
		this.label.color = this.hoverColorStart;
	}

	// Token: 0x060021DD RID: 8669 RVA: 0x000E9D9C File Offset: 0x000E7F9C
	private void Update()
	{
		if (UICamera.GetMouse(0).current != base.gameObject)
		{
			this.label.color = this.hoverColorStart;
		}
	}

	// Token: 0x04001B51 RID: 6993
	public Color hoverColor;

	// Token: 0x04001B52 RID: 6994
	public Color hoverColorStart;

	// Token: 0x04001B53 RID: 6995
	private UILabel label;
}
