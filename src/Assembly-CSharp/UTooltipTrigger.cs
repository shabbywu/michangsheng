using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x0200036D RID: 877
public class UTooltipTrigger : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x06001D64 RID: 7524 RVA: 0x000D0005 File Offset: 0x000CE205
	private void Awake()
	{
		MessageMag.Instance.Register(MessageName.MSG_APP_OnFocusChanged, new Action<MessageData>(this.OnFocusChanged));
	}

	// Token: 0x06001D65 RID: 7525 RVA: 0x000D0022 File Offset: 0x000CE222
	private void OnDestroy()
	{
		MessageMag.Instance.Remove(MessageName.MSG_APP_OnFocusChanged, new Action<MessageData>(this.OnFocusChanged));
	}

	// Token: 0x06001D66 RID: 7526 RVA: 0x000D003F File Offset: 0x000CE23F
	public void OnFocusChanged(MessageData data)
	{
		if (this.isShow)
		{
			this.isShow = false;
			UToolTip.Close();
		}
	}

	// Token: 0x06001D67 RID: 7527 RVA: 0x000D0055 File Offset: 0x000CE255
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

	// Token: 0x06001D68 RID: 7528 RVA: 0x000D0094 File Offset: 0x000CE294
	public void OnPointerExit(PointerEventData eventData)
	{
		this.isShow = false;
		UToolTip.Close();
	}

	// Token: 0x040017FE RID: 6142
	[Multiline]
	public string Tooltip;

	// Token: 0x040017FF RID: 6143
	private bool isShow;

	// Token: 0x04001800 RID: 6144
	public bool UseCustomWidth;

	// Token: 0x04001801 RID: 6145
	public int CustomWidth;
}
