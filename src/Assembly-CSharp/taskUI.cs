using System;
using System.Collections.Generic;
using GUIPackage;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002A7 RID: 679
public class taskUI : MonoBehaviour, IESCClose
{
	// Token: 0x060014AE RID: 5294 RVA: 0x000BB414 File Offset: 0x000B9614
	private void Awake()
	{
		this.panelCanvas.worldCamera = UI_Manager.inst.RootCamera;
		this.bgCanvas.worldCamera = UI_Manager.inst.RootCamera;
		base.transform.parent = UI_Manager.inst.gameObject.transform;
		base.transform.localScale = new Vector3(0.752f, 0.752f, 1f);
		base.transform.localPosition = Vector3.zero;
		this.Head = UI_Manager.inst.headMag.gameObject;
		this.uIWidget.enabled = true;
		this.uIWidget.SetAnchor(UI_Manager.inst.gameObject);
		this.uIWidget.updateAnchors = UIRect.AnchorUpdate.OnUpdate;
		this.uIWidget.leftAnchor.relative = 0.5f;
		this.uIWidget.rightAnchor.relative = 0.5f;
		this.uIWidget.bottomAnchor.relative = 1f;
		this.uIWidget.topAnchor.relative = 1f;
		this.uIWidget.leftAnchor.absolute = -1061;
		this.uIWidget.rightAnchor.absolute = -761;
		this.uIWidget.bottomAnchor.absolute = -134;
		this.uIWidget.topAnchor.absolute = 16;
		this.openTaskPanel();
		base.Invoke("lateAction", 1f);
	}

	// Token: 0x060014AF RID: 5295 RVA: 0x00012FFD File Offset: 0x000111FD
	public void lateAction()
	{
		this.uIWidget.updateAnchors = UIRect.AnchorUpdate.OnEnable;
	}

	// Token: 0x060014B0 RID: 5296 RVA: 0x000BB594 File Offset: 0x000B9794
	public void selectTab(int index)
	{
		switch (index)
		{
		case 0:
			if (this.Tasks[index].isOn)
			{
				this.Tasks[index].transform.GetChild(1).GetComponent<Image>().sprite = this.TasksToggleIcon[0];
			}
			else
			{
				this.Tasks[index].transform.GetChild(1).GetComponent<Image>().sprite = this.TasksToggleIcon[1];
			}
			this.taskUIManager.initTaskList(1);
			return;
		case 1:
			if (this.Tasks[index].isOn)
			{
				this.Tasks[index].transform.GetChild(1).GetComponent<Image>().sprite = this.TasksToggleIcon[2];
			}
			else
			{
				this.Tasks[index].transform.GetChild(1).GetComponent<Image>().sprite = this.TasksToggleIcon[3];
			}
			this.taskUIManager.initTaskList(0);
			return;
		case 2:
			if (this.Tasks[index].isOn)
			{
				this.Tasks[index].transform.GetChild(1).GetComponent<Image>().sprite = this.TasksToggleIcon[4];
			}
			else
			{
				this.Tasks[index].transform.GetChild(1).GetComponent<Image>().sprite = this.TasksToggleIcon[5];
			}
			this.taskUIManager.initTaskList(2);
			return;
		default:
			return;
		}
	}

	// Token: 0x060014B1 RID: 5297 RVA: 0x0001300B File Offset: 0x0001120B
	public void openTaskPanel()
	{
		if (!Tools.instance.canClick(false, true))
		{
			return;
		}
		Tools.canClickFlag = false;
		this.Tasks[0].isOn = true;
		this.selectTab(0);
		ESCCloseManager.Inst.RegisterClose(this);
	}

	// Token: 0x060014B2 RID: 5298 RVA: 0x00013046 File Offset: 0x00011246
	public void closeTaskPanel()
	{
		Tools.canClickFlag = true;
		PanelMamager.inst.closePanel(PanelMamager.PanelType.任务, 0);
		ESCCloseManager.Inst.UnRegisterClose(this);
	}

	// Token: 0x060014B3 RID: 5299 RVA: 0x00013065 File Offset: 0x00011265
	public void OnDestroy()
	{
		PanelMamager.inst.closePanel(PanelMamager.PanelType.任务, 1);
	}

	// Token: 0x060014B4 RID: 5300 RVA: 0x00013073 File Offset: 0x00011273
	public bool TryEscClose()
	{
		this.closeTaskPanel();
		return true;
	}

	// Token: 0x04000FED RID: 4077
	[SerializeField]
	private Canvas panelCanvas;

	// Token: 0x04000FEE RID: 4078
	[SerializeField]
	private UIWidget uIWidget;

	// Token: 0x04000FEF RID: 4079
	[SerializeField]
	private Canvas bgCanvas;

	// Token: 0x04000FF0 RID: 4080
	private GameObject Head;

	// Token: 0x04000FF1 RID: 4081
	[SerializeField]
	private List<Sprite> TasksToggleIcon = new List<Sprite>();

	// Token: 0x04000FF2 RID: 4082
	[SerializeField]
	private List<Toggle> Tasks = new List<Toggle>();

	// Token: 0x04000FF3 RID: 4083
	[SerializeField]
	private TaskUIManager taskUIManager;
}
