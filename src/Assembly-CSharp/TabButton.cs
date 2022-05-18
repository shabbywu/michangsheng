using System;
using UnityEngine;

// Token: 0x020004D5 RID: 1237
public class TabButton : MonoBehaviour
{
	// Token: 0x06002057 RID: 8279 RVA: 0x0001A8ED File Offset: 0x00018AED
	public virtual void Awake()
	{
		this.Group.AddTab(this);
	}

	// Token: 0x06002058 RID: 8280 RVA: 0x0001A8FB File Offset: 0x00018AFB
	public virtual void OnToggle()
	{
		this.IsOn = true;
	}

	// Token: 0x06002059 RID: 8281 RVA: 0x0001A904 File Offset: 0x00018B04
	public virtual void OnLose()
	{
		this.IsOn = false;
	}

	// Token: 0x0600205A RID: 8282 RVA: 0x0001A90D File Offset: 0x00018B0D
	public virtual void OnButtonClick()
	{
		this.Group.TryToggle(this, false);
	}

	// Token: 0x04001BCC RID: 7116
	public TabGroup Group;

	// Token: 0x04001BCD RID: 7117
	public bool IsOn;

	// Token: 0x04001BCE RID: 7118
	public bool IsFirst;
}
