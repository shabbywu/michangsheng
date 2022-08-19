using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000407 RID: 1031
public class UI_CloseState : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
{
	// Token: 0x0600213E RID: 8510 RVA: 0x000E8211 File Offset: 0x000E6411
	private void Start()
	{
		this.state = base.transform.parent.GetComponent<UI_AvatarState>();
	}

	// Token: 0x0600213F RID: 8511 RVA: 0x000E8229 File Offset: 0x000E6429
	public void OnPointerDown(PointerEventData eventData)
	{
		if (eventData.button == null)
		{
			this.state.closeInventory();
		}
	}

	// Token: 0x04001ADD RID: 6877
	private UI_AvatarState state;
}
