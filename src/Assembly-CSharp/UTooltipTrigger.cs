using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x020004EB RID: 1259
public class UTooltipTrigger : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x060020D3 RID: 8403 RVA: 0x0001B0BF File Offset: 0x000192BF
	private void Awake()
	{
		MessageMag.Instance.Register(MessageName.MSG_APP_OnFocusChanged, new Action<MessageData>(this.OnFocusChanged));
	}

	// Token: 0x060020D4 RID: 8404 RVA: 0x0001B0DC File Offset: 0x000192DC
	private void OnDestroy()
	{
		MessageMag.Instance.Remove(MessageName.MSG_APP_OnFocusChanged, new Action<MessageData>(this.OnFocusChanged));
	}

	// Token: 0x060020D5 RID: 8405 RVA: 0x0001B0F9 File Offset: 0x000192F9
	public void OnFocusChanged(MessageData data)
	{
		if (this.isShow)
		{
			this.isShow = false;
			UToolTip.Close();
		}
	}

	// Token: 0x060020D6 RID: 8406 RVA: 0x0001B10F File Offset: 0x0001930F
	public void OnPointerEnter(PointerEventData eventData)
	{
		if (this.UseCustomWidth)
		{
			UToolTip.Show(this.Tooltip, (float)this.CustomWidth, 200f);
		}
		else
		{
			UToolTip.Show(this.Tooltip, 600f, 200f);
		}
		this.isShow = true;
	}

	// Token: 0x060020D7 RID: 8407 RVA: 0x0001B14E File Offset: 0x0001934E
	public void OnPointerExit(PointerEventData eventData)
	{
		this.isShow = false;
		UToolTip.Close();
	}

	// Token: 0x04001C54 RID: 7252
	[Multiline]
	public string Tooltip;

	// Token: 0x04001C55 RID: 7253
	private bool isShow;

	// Token: 0x04001C56 RID: 7254
	public bool UseCustomWidth;

	// Token: 0x04001C57 RID: 7255
	public int CustomWidth;
}
