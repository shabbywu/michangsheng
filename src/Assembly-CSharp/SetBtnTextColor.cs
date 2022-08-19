using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000421 RID: 1057
public class SetBtnTextColor : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x060021E2 RID: 8674 RVA: 0x000E9DFD File Offset: 0x000E7FFD
	private void Start()
	{
		this.label = base.GetComponentInChildren<Text>();
	}

	// Token: 0x060021E3 RID: 8675 RVA: 0x000E9E0B File Offset: 0x000E800B
	public void OnPointerEnter(PointerEventData eventData)
	{
		this.label.color = this.hoverColor;
	}

	// Token: 0x060021E4 RID: 8676 RVA: 0x000E9E1E File Offset: 0x000E801E
	public void OnPointerExit(PointerEventData eventData)
	{
		this.label.color = this.hoverColorStart;
	}

	// Token: 0x04001B57 RID: 6999
	public Color hoverColor;

	// Token: 0x04001B58 RID: 7000
	public Color hoverColorStart;

	// Token: 0x04001B59 RID: 7001
	private Text label;
}
