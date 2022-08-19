using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x0200011A RID: 282
public class PEButtonScript : MonoBehaviour, IEventSystemHandler, IPointerEnterHandler, IPointerExitHandler
{
	// Token: 0x06000D87 RID: 3463 RVA: 0x00051266 File Offset: 0x0004F466
	private void Start()
	{
		this.myButton = base.gameObject.GetComponent<Button>();
	}

	// Token: 0x06000D88 RID: 3464 RVA: 0x00051279 File Offset: 0x0004F479
	public void OnPointerEnter(PointerEventData eventData)
	{
		UICanvasManager.GlobalAccess.MouseOverButton = true;
		UICanvasManager.GlobalAccess.UpdateToolTip(this.ButtonType);
	}

	// Token: 0x06000D89 RID: 3465 RVA: 0x00051296 File Offset: 0x0004F496
	public void OnPointerExit(PointerEventData eventData)
	{
		UICanvasManager.GlobalAccess.MouseOverButton = false;
		UICanvasManager.GlobalAccess.ClearToolTip();
	}

	// Token: 0x06000D8A RID: 3466 RVA: 0x000512AD File Offset: 0x0004F4AD
	public void OnButtonClicked()
	{
		UICanvasManager.GlobalAccess.UIButtonClick(this.ButtonType);
	}

	// Token: 0x04000993 RID: 2451
	private Button myButton;

	// Token: 0x04000994 RID: 2452
	public ButtonTypes ButtonType;
}
