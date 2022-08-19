using System;
using System.Collections.Generic;
using GUIPackage;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001AA RID: 426
public class taskUI : MonoBehaviour, IESCClose
{
	// Token: 0x06001207 RID: 4615 RVA: 0x0006D4F4 File Offset: 0x0006B6F4
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

	// Token: 0x06001208 RID: 4616 RVA: 0x0006D671 File Offset: 0x0006B871
	public void lateAction()
	{
		this.uIWidget.updateAnchors = UIRect.AnchorUpdate.OnEnable;
	}

	// Token: 0x06001209 RID: 4617 RVA: 0x0006D680 File Offset: 0x0006B880
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

	// Token: 0x0600120A RID: 4618 RVA: 0x0006D813 File Offset: 0x0006BA13
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

	// Token: 0x0600120B RID: 4619 RVA: 0x0006D84E File Offset: 0x0006BA4E
	public void closeTaskPanel()
	{
		Tools.canClickFlag = true;
		PanelMamager.inst.closePanel(PanelMamager.PanelType.任务, 0);
		ESCCloseManager.Inst.UnRegisterClose(this);
	}

	// Token: 0x0600120C RID: 4620 RVA: 0x0006D86D File Offset: 0x0006BA6D
	public void OnDestroy()
	{
		PanelMamager.inst.closePanel(PanelMamager.PanelType.任务, 1);
	}

	// Token: 0x0600120D RID: 4621 RVA: 0x0006D87B File Offset: 0x0006BA7B
	public bool TryEscClose()
	{
		this.closeTaskPanel();
		return true;
	}

	// Token: 0x04000CC5 RID: 3269
	[SerializeField]
	private Canvas panelCanvas;

	// Token: 0x04000CC6 RID: 3270
	[SerializeField]
	private UIWidget uIWidget;

	// Token: 0x04000CC7 RID: 3271
	[SerializeField]
	private Canvas bgCanvas;

	// Token: 0x04000CC8 RID: 3272
	private GameObject Head;

	// Token: 0x04000CC9 RID: 3273
	[SerializeField]
	private List<Sprite> TasksToggleIcon = new List<Sprite>();

	// Token: 0x04000CCA RID: 3274
	[SerializeField]
	private List<Toggle> Tasks = new List<Toggle>();

	// Token: 0x04000CCB RID: 3275
	[SerializeField]
	private TaskUIManager taskUIManager;
}
