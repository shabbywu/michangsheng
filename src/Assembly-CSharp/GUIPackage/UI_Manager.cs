using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using script.YarnEditor.Manager;

namespace GUIPackage;

public class UI_Manager : MonoBehaviour
{
	public Transform equipment;

	public Transform inventory;

	public Transform short_cut;

	public Transform skill;

	public Transform store;

	public Transform temp;

	public Transform tooltip;

	public List<Transform> UI = new List<Transform>();

	public static UI_Manager inst;

	public Camera RootCamera;

	public headMag headMag;

	public Shop_UI shop_UI;

	public Inventory2 inventory2;

	public GameObject BlackGameObject;

	public GameObject CraftingList;

	public GameObject BlackImage;

	public GameObject TotalTalk;

	public CheckTool checkTool;

	public GameObject xialian;

	public GameObject exchangeUI;

	public Transform JieSuanDongHuaParent;

	private void Update()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Expected O, but got Unknown
		if (!Tools.instance.isNeedSetTalk)
		{
			return;
		}
		Scene activeScene = SceneManager.GetActiveScene();
		string name = ((Scene)(ref activeScene)).name;
		if (Tools.instance.loadSceneType == 0)
		{
			if (name.Equals(Tools.instance.ohtherSceneName))
			{
				Tools.instance.isNeedSetTalk = false;
				if ((Object)(object)PanelMamager.inst.UIBlackMaskGameObject != (Object)null)
				{
					PanelMamager.inst.UIBlackMaskGameObject.SetActive(false);
					PanelMamager.inst.UIBlackMaskGameObject.SetActive(true);
				}
				Object.Instantiate<GameObject>(ResManager.inst.LoadPrefab("threeScreenUI"), ((Component)inst).gameObject.transform);
				Object.Instantiate<GameObject>(ResManager.inst.LoadPrefab("NPCView"), ((Component)inst).gameObject.transform);
				if ((Object)(object)checkTool != (Object)null)
				{
					checkTool.Init();
				}
			}
		}
		else
		{
			if (!name.Equals(Tools.jumpToName))
			{
				return;
			}
			Tools.instance.isNeedSetTalk = false;
			if ((Object)(object)PanelMamager.inst.UIBlackMaskGameObject != (Object)null)
			{
				PanelMamager.inst.UIBlackMaskGameObject.SetActive(false);
				PanelMamager.inst.UIBlackMaskGameObject.SetActive(true);
			}
			Object.Instantiate<GameObject>(ResManager.inst.LoadPrefab("NPCView"), ((Component)inst).gameObject.transform);
			((MonoBehaviour)this).Invoke("showBtn", 0.2f);
			if (!StoryManager.Inst.IsEnd)
			{
				return;
			}
			if (StoryManager.Inst.CheckTrigger(Tools.jumpToName))
			{
				if ((Object)(object)checkTool != (Object)null)
				{
					StoryManager.Inst.OldTalk = new UnityAction(checkTool.Init);
				}
			}
			else if ((Object)(object)checkTool != (Object)null)
			{
				checkTool.Init();
			}
		}
	}

	private void Awake()
	{
		inst = this;
		if ((Object)(object)checkTool == (Object)null)
		{
			checkTool = ((Component)this).GetComponent<CheckTool>();
		}
	}

	private void Start()
	{
		UI.Add(equipment);
		UI.Add(inventory);
		UI.Add(skill);
		inst = this;
		((MonoBehaviour)this).Invoke("openPanel", 0.3f);
	}

	private void showBtn()
	{
		Object.Instantiate<GameObject>(ResManager.inst.LoadPrefab("threeScreenUI"), ((Component)inst).gameObject.transform);
	}

	public void openPanel()
	{
		Avatar player = Tools.instance.getPlayer();
		if (player.openPanelList.Count <= 0)
		{
			return;
		}
		List<string> list = new List<string>();
		foreach (string key in player.openPanelList.keys)
		{
			if (player.openPanelList[key].b)
			{
				list.Add(key);
			}
		}
		for (int i = 0; i < list.Count; i++)
		{
			if (int.Parse(list[i]) == 0 || 4 == int.Parse(list[i]))
			{
				PanelMamager.inst.OpenPanel((PanelMamager.PanelType)int.Parse(list[i]));
			}
		}
	}

	private void UIOrder()
	{
		for (int i = 0; i < UI.Count; i++)
		{
			SetDepth(UI[i], i + 1);
		}
	}

	private void UI_Click(GameObject ui, bool isPress)
	{
		if (Input.GetMouseButtonDown(0))
		{
			UI_Top(ui.transform.parent.parent);
		}
	}

	public void UI_Top(Transform ui)
	{
		for (int i = 0; i < UI.Count; i++)
		{
			if ((Object)(object)UI[i] == (Object)(object)ui)
			{
				UI.Add(UI[i]);
				UI.RemoveAt(i);
				UIOrder();
				break;
			}
		}
	}

	private void UI_Event(Transform ui)
	{
		UIEventListener.Get(((Component)ui.Find("Win/BG")).gameObject).onPress = UI_Click;
	}

	private void SetDepth(Transform ui, int depth)
	{
		if (((Object)ui).name == "store" || ((Object)ui).name == "skill_UI")
		{
			((Component)ui.Find("Win/Scroll View")).GetComponent<UIPanel>().depth = depth;
		}
		((Component)ui.Find("Win")).GetComponent<UIPanel>().depth = depth;
	}

	private int GetDepth(Transform ui)
	{
		return ((Component)ui).GetComponentInChildren<UIPanel>().depth;
	}

	public void PlayeJieSuanAnimation(string content, UnityAction action)
	{
		GameObject obj = Object.Instantiate<GameObject>(ResManager.inst.LoadPrefab("BigGuanDongHua"), JieSuanDongHuaParent);
		obj.SetActive(false);
		obj.GetComponent<JueSuanAnimation>().Play(content, action);
	}

	public void showBlack()
	{
		if ((Object)(object)BlackGameObject != (Object)null)
		{
			((MonoBehaviour)this).Invoke("hideBlack", 0.5f);
		}
	}

	public void hideBlack()
	{
		BlackGameObject.SetActive(false);
		BlackGameObject.SetActive(true);
		Tools.canClickFlag = true;
	}
}
