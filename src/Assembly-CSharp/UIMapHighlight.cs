using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020004AE RID: 1198
public class UIMapHighlight : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x06001FBB RID: 8123 RVA: 0x0001A1F1 File Offset: 0x000183F1
	private void Awake()
	{
		this.image = base.GetComponent<Image>();
	}

	// Token: 0x06001FBC RID: 8124 RVA: 0x0001A1FF File Offset: 0x000183FF
	void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
	{
		UIMapPanel.Inst.OnMouseEnterHighlightBlock(this);
	}

	// Token: 0x06001FBD RID: 8125 RVA: 0x0001A20C File Offset: 0x0001840C
	void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
	{
		UIMapPanel.Inst.OnMouseExitHighlightBlock(this);
	}

	// Token: 0x06001FBE RID: 8126 RVA: 0x0001A219 File Offset: 0x00018419
	public void SetLight(bool light)
	{
		if (light)
		{
			this.image.color = Color.white;
			return;
		}
		this.image.color = Color.clear;
	}

	// Token: 0x04001B26 RID: 6950
	public int ID;

	// Token: 0x04001B27 RID: 6951
	private Image image;
}
