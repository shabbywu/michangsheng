using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020001DC RID: 476
public class PEButtonScript : MonoBehaviour, IEventSystemHandler, IPointerEnterHandler, IPointerExitHandler
{
	// Token: 0x06000F60 RID: 3936 RVA: 0x0000F9C2 File Offset: 0x0000DBC2
	private void Start()
	{
		this.myButton = base.gameObject.GetComponent<Button>();
	}

	// Token: 0x06000F61 RID: 3937 RVA: 0x0000F9D5 File Offset: 0x0000DBD5
	public void OnPointerEnter(PointerEventData eventData)
	{
		UICanvasManager.GlobalAccess.MouseOverButton = true;
		UICanvasManager.GlobalAccess.UpdateToolTip(this.ButtonType);
	}

	// Token: 0x06000F62 RID: 3938 RVA: 0x0000F9F2 File Offset: 0x0000DBF2
	public void OnPointerExit(PointerEventData eventData)
	{
		UICanvasManager.GlobalAccess.MouseOverButton = false;
		UICanvasManager.GlobalAccess.ClearToolTip();
	}

	// Token: 0x06000F63 RID: 3939 RVA: 0x0000FA09 File Offset: 0x0000DC09
	public void OnButtonClicked()
	{
		UICanvasManager.GlobalAccess.UIButtonClick(this.ButtonType);
	}

	// Token: 0x04000C17 RID: 3095
	private Button myButton;

	// Token: 0x04000C18 RID: 3096
	public ButtonTypes ButtonType;
}
