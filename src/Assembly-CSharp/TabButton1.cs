using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000359 RID: 857
public class TabButton1 : TabButton
{
	// Token: 0x06001CF2 RID: 7410 RVA: 0x000CE3B8 File Offset: 0x000CC5B8
	public override void Awake()
	{
		this.Group.AddTab(this);
		this.OnToggleButton.onClick.AddListener(new UnityAction(this.OnButtonClick));
		this.LoseToggleButton.onClick.AddListener(new UnityAction(this.OnButtonClick));
	}

	// Token: 0x06001CF3 RID: 7411 RVA: 0x000CE40B File Offset: 0x000CC60B
	public override void OnToggle()
	{
		base.OnToggle();
		this.OnToggleObject.SetActive(true);
		this.LoseToggleObject.SetActive(false);
		this.Panel.OnPanelShow();
	}

	// Token: 0x06001CF4 RID: 7412 RVA: 0x000CE436 File Offset: 0x000CC636
	public override void OnLose()
	{
		base.OnLose();
		this.OnToggleObject.SetActive(false);
		this.LoseToggleObject.SetActive(true);
		this.Panel.OnPanelHide();
	}

	// Token: 0x04001777 RID: 6007
	public GameObject OnToggleObject;

	// Token: 0x04001778 RID: 6008
	public GameObject LoseToggleObject;

	// Token: 0x04001779 RID: 6009
	public Button OnToggleButton;

	// Token: 0x0400177A RID: 6010
	public Button LoseToggleButton;

	// Token: 0x0400177B RID: 6011
	public TabPanelBase Panel;
}
