using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000333 RID: 819
public class BaseDropdown : Dropdown
{
	// Token: 0x06001C38 RID: 7224 RVA: 0x000CA484 File Offset: 0x000C8684
	public override void OnPointerUp(PointerEventData eventData)
	{
		base.OnPointerUp(eventData);
		base.image.overrideSprite = null;
	}

	// Token: 0x06001C39 RID: 7225 RVA: 0x000CA499 File Offset: 0x000C8699
	public override void OnPointerExit(PointerEventData eventData)
	{
		base.OnPointerExit(eventData);
	}
}
