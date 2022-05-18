using System;
using UnityEngine;

// Token: 0x020005D3 RID: 1491
public class SetBtnLabelColor : MonoBehaviour
{
	// Token: 0x06002595 RID: 9621 RVA: 0x0001E1B4 File Offset: 0x0001C3B4
	private void Start()
	{
		this.label = base.GetComponentInChildren<UILabel>();
	}

	// Token: 0x06002596 RID: 9622 RVA: 0x0001E1C2 File Offset: 0x0001C3C2
	public void OnHover(bool isOver)
	{
		if (isOver)
		{
			this.label.color = this.hoverColor;
			return;
		}
		this.label.color = this.hoverColorStart;
	}

	// Token: 0x06002597 RID: 9623 RVA: 0x0001E1EA File Offset: 0x0001C3EA
	private void Update()
	{
		if (UICamera.GetMouse(0).current != base.gameObject)
		{
			this.label.color = this.hoverColorStart;
		}
	}

	// Token: 0x04002017 RID: 8215
	public Color hoverColor;

	// Token: 0x04002018 RID: 8216
	public Color hoverColorStart;

	// Token: 0x04002019 RID: 8217
	private UILabel label;
}
