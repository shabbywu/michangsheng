using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x0200033D RID: 829
public class UIMapHighlight : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x06001C70 RID: 7280 RVA: 0x000CBB46 File Offset: 0x000C9D46
	private void Awake()
	{
		this.image = base.GetComponent<Image>();
	}

	// Token: 0x06001C71 RID: 7281 RVA: 0x000CBB54 File Offset: 0x000C9D54
	void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
	{
		UIMapPanel.Inst.OnMouseEnterHighlightBlock(this);
	}

	// Token: 0x06001C72 RID: 7282 RVA: 0x000CBB61 File Offset: 0x000C9D61
	void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
	{
		UIMapPanel.Inst.OnMouseExitHighlightBlock(this);
	}

	// Token: 0x06001C73 RID: 7283 RVA: 0x000CBB6E File Offset: 0x000C9D6E
	public void SetLight(bool light)
	{
		if (light)
		{
			this.image.color = Color.white;
			return;
		}
		this.image.color = Color.clear;
	}

	// Token: 0x040016EB RID: 5867
	public int ID;

	// Token: 0x040016EC RID: 5868
	private Image image;
}
