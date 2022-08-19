using System;
using UnityEngine;

// Token: 0x02000358 RID: 856
public class TabButton : MonoBehaviour
{
	// Token: 0x06001CED RID: 7405 RVA: 0x000CE387 File Offset: 0x000CC587
	public virtual void Awake()
	{
		this.Group.AddTab(this);
	}

	// Token: 0x06001CEE RID: 7406 RVA: 0x000CE395 File Offset: 0x000CC595
	public virtual void OnToggle()
	{
		this.IsOn = true;
	}

	// Token: 0x06001CEF RID: 7407 RVA: 0x000CE39E File Offset: 0x000CC59E
	public virtual void OnLose()
	{
		this.IsOn = false;
	}

	// Token: 0x06001CF0 RID: 7408 RVA: 0x000CE3A7 File Offset: 0x000CC5A7
	public virtual void OnButtonClick()
	{
		this.Group.TryToggle(this, false);
	}

	// Token: 0x04001774 RID: 6004
	public TabGroup Group;

	// Token: 0x04001775 RID: 6005
	public bool IsOn;

	// Token: 0x04001776 RID: 6006
	public bool IsFirst;
}
