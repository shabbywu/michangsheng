using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020005D5 RID: 1493
public class SetBtnTextColor : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x0600259C RID: 9628 RVA: 0x0001E24B File Offset: 0x0001C44B
	private void Start()
	{
		this.label = base.GetComponentInChildren<Text>();
	}

	// Token: 0x0600259D RID: 9629 RVA: 0x0001E259 File Offset: 0x0001C459
	public void OnPointerEnter(PointerEventData eventData)
	{
		this.label.color = this.hoverColor;
	}

	// Token: 0x0600259E RID: 9630 RVA: 0x0001E26C File Offset: 0x0001C46C
	public void OnPointerExit(PointerEventData eventData)
	{
		this.label.color = this.hoverColorStart;
	}

	// Token: 0x0400201D RID: 8221
	public Color hoverColor;

	// Token: 0x0400201E RID: 8222
	public Color hoverColorStart;

	// Token: 0x0400201F RID: 8223
	private Text label;
}
