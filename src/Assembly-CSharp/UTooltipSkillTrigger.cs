using System;
using GUIPackage;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x0200036C RID: 876
public class UTooltipSkillTrigger : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x06001D5E RID: 7518 RVA: 0x000CFF40 File Offset: 0x000CE140
	private void Awake()
	{
		MessageMag.Instance.Register(MessageName.MSG_APP_OnFocusChanged, new Action<MessageData>(this.OnFocusChanged));
	}

	// Token: 0x06001D5F RID: 7519 RVA: 0x000CFF5D File Offset: 0x000CE15D
	private void OnDestroy()
	{
		MessageMag.Instance.Remove(MessageName.MSG_APP_OnFocusChanged, new Action<MessageData>(this.OnFocusChanged));
	}

	// Token: 0x06001D60 RID: 7520 RVA: 0x000CFF7A File Offset: 0x000CE17A
	public void OnFocusChanged(MessageData data)
	{
		if (this.isShow)
		{
			this.isShow = false;
			UToolTip.Close();
		}
	}

	// Token: 0x06001D61 RID: 7521 RVA: 0x000CFF90 File Offset: 0x000CE190
	public void OnPointerEnter(PointerEventData eventData)
	{
		if (this.SkillID > 0)
		{
			UToolTip.Show(SkillDatebase.instence.Dict[this.SkillID][this.Level].skill_Desc, 600f, 200f);
			this.isShow = true;
		}
	}

	// Token: 0x06001D62 RID: 7522 RVA: 0x000CFFE1 File Offset: 0x000CE1E1
	public void OnPointerExit(PointerEventData eventData)
	{
		this.isShow = false;
		UToolTip.Close();
	}

	// Token: 0x040017FB RID: 6139
	public int SkillID = 1;

	// Token: 0x040017FC RID: 6140
	public int Level = 1;

	// Token: 0x040017FD RID: 6141
	private bool isShow;
}
