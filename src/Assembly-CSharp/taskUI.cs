using System.Collections.Generic;
using GUIPackage;
using UnityEngine;
using UnityEngine.UI;

public class taskUI : MonoBehaviour, IESCClose
{
	[SerializeField]
	private Canvas panelCanvas;

	[SerializeField]
	private UIWidget uIWidget;

	[SerializeField]
	private Canvas bgCanvas;

	private GameObject Head;

	[SerializeField]
	private List<Sprite> TasksToggleIcon = new List<Sprite>();

	[SerializeField]
	private List<Toggle> Tasks = new List<Toggle>();

	[SerializeField]
	private TaskUIManager taskUIManager;

	private void Awake()
	{
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		panelCanvas.worldCamera = UI_Manager.inst.RootCamera;
		bgCanvas.worldCamera = UI_Manager.inst.RootCamera;
		((Component)this).transform.parent = ((Component)UI_Manager.inst).gameObject.transform;
		((Component)this).transform.localScale = new Vector3(0.752f, 0.752f, 1f);
		((Component)this).transform.localPosition = Vector3.zero;
		Head = ((Component)UI_Manager.inst.headMag).gameObject;
		((Behaviour)uIWidget).enabled = true;
		uIWidget.SetAnchor(((Component)UI_Manager.inst).gameObject);
		uIWidget.updateAnchors = UIRect.AnchorUpdate.OnUpdate;
		uIWidget.leftAnchor.relative = 0.5f;
		uIWidget.rightAnchor.relative = 0.5f;
		uIWidget.bottomAnchor.relative = 1f;
		uIWidget.topAnchor.relative = 1f;
		uIWidget.leftAnchor.absolute = -1061;
		uIWidget.rightAnchor.absolute = -761;
		uIWidget.bottomAnchor.absolute = -134;
		uIWidget.topAnchor.absolute = 16;
		openTaskPanel();
		((MonoBehaviour)this).Invoke("lateAction", 1f);
	}

	public void lateAction()
	{
		uIWidget.updateAnchors = UIRect.AnchorUpdate.OnEnable;
	}

	public void selectTab(int index)
	{
		switch (index)
		{
		case 0:
			if (Tasks[index].isOn)
			{
				((Component)((Component)Tasks[index]).transform.GetChild(1)).GetComponent<Image>().sprite = TasksToggleIcon[0];
			}
			else
			{
				((Component)((Component)Tasks[index]).transform.GetChild(1)).GetComponent<Image>().sprite = TasksToggleIcon[1];
			}
			taskUIManager.initTaskList();
			break;
		case 1:
			if (Tasks[index].isOn)
			{
				((Component)((Component)Tasks[index]).transform.GetChild(1)).GetComponent<Image>().sprite = TasksToggleIcon[2];
			}
			else
			{
				((Component)((Component)Tasks[index]).transform.GetChild(1)).GetComponent<Image>().sprite = TasksToggleIcon[3];
			}
			taskUIManager.initTaskList(0);
			break;
		case 2:
			if (Tasks[index].isOn)
			{
				((Component)((Component)Tasks[index]).transform.GetChild(1)).GetComponent<Image>().sprite = TasksToggleIcon[4];
			}
			else
			{
				((Component)((Component)Tasks[index]).transform.GetChild(1)).GetComponent<Image>().sprite = TasksToggleIcon[5];
			}
			taskUIManager.initTaskList(2);
			break;
		}
	}

	public void openTaskPanel()
	{
		if (Tools.instance.canClick())
		{
			Tools.canClickFlag = false;
			Tasks[0].isOn = true;
			selectTab(0);
			ESCCloseManager.Inst.RegisterClose(this);
		}
	}

	public void closeTaskPanel()
	{
		Tools.canClickFlag = true;
		PanelMamager.inst.closePanel(PanelMamager.PanelType.任务);
		ESCCloseManager.Inst.UnRegisterClose(this);
	}

	public void OnDestroy()
	{
		PanelMamager.inst.closePanel(PanelMamager.PanelType.任务, 1);
	}

	public bool TryEscClose()
	{
		closeTaskPanel();
		return true;
	}
}
