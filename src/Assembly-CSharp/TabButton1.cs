using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020004D6 RID: 1238
public class TabButton1 : TabButton
{
	// Token: 0x0600205C RID: 8284 RVA: 0x0011316C File Offset: 0x0011136C
	public override void Awake()
	{
		this.Group.AddTab(this);
		this.OnToggleButton.onClick.AddListener(new UnityAction(this.OnButtonClick));
		this.LoseToggleButton.onClick.AddListener(new UnityAction(this.OnButtonClick));
	}

	// Token: 0x0600205D RID: 8285 RVA: 0x0001A91C File Offset: 0x00018B1C
	public override void OnToggle()
	{
		base.OnToggle();
		this.OnToggleObject.SetActive(true);
		this.LoseToggleObject.SetActive(false);
		this.Panel.OnPanelShow();
	}

	// Token: 0x0600205E RID: 8286 RVA: 0x0001A947 File Offset: 0x00018B47
	public override void OnLose()
	{
		base.OnLose();
		this.OnToggleObject.SetActive(false);
		this.LoseToggleObject.SetActive(true);
		this.Panel.OnPanelHide();
	}

	// Token: 0x04001BCF RID: 7119
	public GameObject OnToggleObject;

	// Token: 0x04001BD0 RID: 7120
	public GameObject LoseToggleObject;

	// Token: 0x04001BD1 RID: 7121
	public Button OnToggleButton;

	// Token: 0x04001BD2 RID: 7122
	public Button LoseToggleButton;

	// Token: 0x04001BD3 RID: 7123
	public TabPanelBase Panel;
}
