using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020004A3 RID: 1187
public class BaseDropdown : Dropdown
{
	// Token: 0x06001F88 RID: 8072 RVA: 0x0001A06E File Offset: 0x0001826E
	public override void OnPointerUp(PointerEventData eventData)
	{
		base.OnPointerUp(eventData);
		base.image.overrideSprite = null;
	}

	// Token: 0x06001F89 RID: 8073 RVA: 0x0001A083 File Offset: 0x00018283
	public override void OnPointerExit(PointerEventData eventData)
	{
		base.OnPointerExit(eventData);
	}
}
