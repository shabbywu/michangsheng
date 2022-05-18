using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x020005B7 RID: 1463
public class UI_CloseState : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
{
	// Token: 0x060024F0 RID: 9456 RVA: 0x0001DAA3 File Offset: 0x0001BCA3
	private void Start()
	{
		this.state = base.transform.parent.GetComponent<UI_AvatarState>();
	}

	// Token: 0x060024F1 RID: 9457 RVA: 0x0001DABB File Offset: 0x0001BCBB
	public void OnPointerDown(PointerEventData eventData)
	{
		if (eventData.button == null)
		{
			this.state.closeInventory();
		}
	}

	// Token: 0x04001F99 RID: 8089
	private UI_AvatarState state;
}
