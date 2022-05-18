using System;
using GUIPackage;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x020004EA RID: 1258
public class UTooltipSkillTrigger : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x060020CD RID: 8397 RVA: 0x0001B04B File Offset: 0x0001924B
	private void Awake()
	{
		MessageMag.Instance.Register(MessageName.MSG_APP_OnFocusChanged, new Action<MessageData>(this.OnFocusChanged));
	}

	// Token: 0x060020CE RID: 8398 RVA: 0x0001B068 File Offset: 0x00019268
	private void OnDestroy()
	{
		MessageMag.Instance.Remove(MessageName.MSG_APP_OnFocusChanged, new Action<MessageData>(this.OnFocusChanged));
	}

	// Token: 0x060020CF RID: 8399 RVA: 0x0001B085 File Offset: 0x00019285
	public void OnFocusChanged(MessageData data)
	{
		if (this.isShow)
		{
			this.isShow = false;
			UToolTip.Close();
		}
	}

	// Token: 0x060020D0 RID: 8400 RVA: 0x0011454C File Offset: 0x0011274C
	public void OnPointerEnter(PointerEventData eventData)
	{
		if (this.SkillID > 0)
		{
			UToolTip.Show(SkillDatebase.instence.Dict[this.SkillID][this.Level].skill_Desc, 600f, 200f);
			this.isShow = true;
		}
	}

	// Token: 0x060020D1 RID: 8401 RVA: 0x0001B09B File Offset: 0x0001929B
	public void OnPointerExit(PointerEventData eventData)
	{
		this.isShow = false;
		UToolTip.Close();
	}

	// Token: 0x04001C51 RID: 7249
	public int SkillID = 1;

	// Token: 0x04001C52 RID: 7250
	public int Level = 1;

	// Token: 0x04001C53 RID: 7251
	private bool isShow;
}
