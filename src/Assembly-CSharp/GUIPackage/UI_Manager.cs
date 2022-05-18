using System;
using System.Collections.Generic;
using KBEngine;
using script.YarnEditor.Manager;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace GUIPackage
{
	// Token: 0x02000DA1 RID: 3489
	public class UI_Manager : MonoBehaviour
	{
		// Token: 0x06005438 RID: 21560 RVA: 0x002310DC File Offset: 0x0022F2DC
		private void Update()
		{
			if (Tools.instance.isNeedSetTalk)
			{
				string name = SceneManager.GetActiveScene().name;
				if (Tools.instance.loadSceneType == 0)
				{
					if (name.Equals(Tools.instance.ohtherSceneName))
					{
						Tools.instance.isNeedSetTalk = false;
						if (PanelMamager.inst.UIBlackMaskGameObject != null)
						{
							PanelMamager.inst.UIBlackMaskGameObject.SetActive(false);
							PanelMamager.inst.UIBlackMaskGameObject.SetActive(true);
						}
						Object.Instantiate<GameObject>(ResManager.inst.LoadPrefab("threeScreenUI"), UI_Manager.inst.gameObject.transform);
						Object.Instantiate<GameObject>(ResManager.inst.LoadPrefab("NPCView"), UI_Manager.inst.gameObject.transform);
						if (this.checkTool != null)
						{
							this.checkTool.Init();
							return;
						}
					}
				}
				else if (name.Equals(Tools.jumpToName))
				{
					Tools.instance.isNeedSetTalk = false;
					if (PanelMamager.inst.UIBlackMaskGameObject != null)
					{
						PanelMamager.inst.UIBlackMaskGameObject.SetActive(false);
						PanelMamager.inst.UIBlackMaskGameObject.SetActive(true);
					}
					Object.Instantiate<GameObject>(ResManager.inst.LoadPrefab("NPCView"), UI_Manager.inst.gameObject.transform);
					base.Invoke("showBtn", 0.2f);
					if (StoryManager.Inst.IsEnd)
					{
						if (StoryManager.Inst.CheckTrigger(Tools.jumpToName))
						{
							if (this.checkTool != null)
							{
								StoryManager.Inst.OldTalk = new UnityAction(this.checkTool.Init);
								return;
							}
						}
						else if (this.checkTool != null)
						{
							this.checkTool.Init();
						}
					}
				}
			}
		}

		// Token: 0x06005439 RID: 21561 RVA: 0x0003C45D File Offset: 0x0003A65D
		private void Awake()
		{
			UI_Manager.inst = this;
			if (this.checkTool == null)
			{
				this.checkTool = base.GetComponent<CheckTool>();
			}
		}

		// Token: 0x0600543A RID: 21562 RVA: 0x002312B0 File Offset: 0x0022F4B0
		private void Start()
		{
			this.UI.Add(this.equipment);
			this.UI.Add(this.inventory);
			this.UI.Add(this.skill);
			UI_Manager.inst = this;
			base.Invoke("openPanel", 0.3f);
		}

		// Token: 0x0600543B RID: 21563 RVA: 0x0003C47F File Offset: 0x0003A67F
		private void showBtn()
		{
			Object.Instantiate<GameObject>(ResManager.inst.LoadPrefab("threeScreenUI"), UI_Manager.inst.gameObject.transform);
		}

		// Token: 0x0600543C RID: 21564 RVA: 0x00231308 File Offset: 0x0022F508
		public void openPanel()
		{
			Avatar player = Tools.instance.getPlayer();
			if (player.openPanelList.Count > 0)
			{
				List<string> list = new List<string>();
				foreach (string text in player.openPanelList.keys)
				{
					if (player.openPanelList[text].b)
					{
						list.Add(text);
					}
				}
				for (int i = 0; i < list.Count; i++)
				{
					if (int.Parse(list[i]) == 0 || 4 == int.Parse(list[i]))
					{
						PanelMamager.inst.OpenPanel((PanelMamager.PanelType)int.Parse(list[i]), 0);
					}
				}
			}
		}

		// Token: 0x0600543D RID: 21565 RVA: 0x002313E4 File Offset: 0x0022F5E4
		private void UIOrder()
		{
			for (int i = 0; i < this.UI.Count; i++)
			{
				this.SetDepth(this.UI[i], i + 1);
			}
		}

		// Token: 0x0600543E RID: 21566 RVA: 0x0003C4A5 File Offset: 0x0003A6A5
		private void UI_Click(GameObject ui, bool isPress)
		{
			if (Input.GetMouseButtonDown(0))
			{
				this.UI_Top(ui.transform.parent.parent);
			}
		}

		// Token: 0x0600543F RID: 21567 RVA: 0x0023141C File Offset: 0x0022F61C
		public void UI_Top(Transform ui)
		{
			for (int i = 0; i < this.UI.Count; i++)
			{
				if (this.UI[i] == ui)
				{
					this.UI.Add(this.UI[i]);
					this.UI.RemoveAt(i);
					this.UIOrder();
					return;
				}
			}
		}

		// Token: 0x06005440 RID: 21568 RVA: 0x0003C4C5 File Offset: 0x0003A6C5
		private void UI_Event(Transform ui)
		{
			UIEventListener.Get(ui.Find("Win/BG").gameObject).onPress = new UIEventListener.BoolDelegate(this.UI_Click);
		}

		// Token: 0x06005441 RID: 21569 RVA: 0x00231480 File Offset: 0x0022F680
		private void SetDepth(Transform ui, int depth)
		{
			if (ui.name == "store" || ui.name == "skill_UI")
			{
				ui.Find("Win/Scroll View").GetComponent<UIPanel>().depth = depth;
			}
			ui.Find("Win").GetComponent<UIPanel>().depth = depth;
		}

		// Token: 0x06005442 RID: 21570 RVA: 0x0003C4ED File Offset: 0x0003A6ED
		private int GetDepth(Transform ui)
		{
			return ui.GetComponentInChildren<UIPanel>().depth;
		}

		// Token: 0x06005443 RID: 21571 RVA: 0x0003C4FA File Offset: 0x0003A6FA
		public void PlayeJieSuanAnimation(string content, UnityAction action)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(ResManager.inst.LoadPrefab("BigGuanDongHua"), this.JieSuanDongHuaParent);
			gameObject.SetActive(false);
			gameObject.GetComponent<JueSuanAnimation>().Play(content, action);
		}

		// Token: 0x06005444 RID: 21572 RVA: 0x0003C529 File Offset: 0x0003A729
		public void showBlack()
		{
			if (this.BlackGameObject != null)
			{
				base.Invoke("hideBlack", 0.5f);
			}
		}

		// Token: 0x06005445 RID: 21573 RVA: 0x0003C549 File Offset: 0x0003A749
		public void hideBlack()
		{
			this.BlackGameObject.SetActive(false);
			this.BlackGameObject.SetActive(true);
			Tools.canClickFlag = true;
		}

		// Token: 0x040053ED RID: 21485
		public Transform equipment;

		// Token: 0x040053EE RID: 21486
		public Transform inventory;

		// Token: 0x040053EF RID: 21487
		public Transform short_cut;

		// Token: 0x040053F0 RID: 21488
		public Transform skill;

		// Token: 0x040053F1 RID: 21489
		public Transform store;

		// Token: 0x040053F2 RID: 21490
		public Transform temp;

		// Token: 0x040053F3 RID: 21491
		public Transform tooltip;

		// Token: 0x040053F4 RID: 21492
		public List<Transform> UI = new List<Transform>();

		// Token: 0x040053F5 RID: 21493
		public static UI_Manager inst;

		// Token: 0x040053F6 RID: 21494
		public Camera RootCamera;

		// Token: 0x040053F7 RID: 21495
		public headMag headMag;

		// Token: 0x040053F8 RID: 21496
		public Shop_UI shop_UI;

		// Token: 0x040053F9 RID: 21497
		public Inventory2 inventory2;

		// Token: 0x040053FA RID: 21498
		public GameObject BlackGameObject;

		// Token: 0x040053FB RID: 21499
		public GameObject CraftingList;

		// Token: 0x040053FC RID: 21500
		public GameObject BlackImage;

		// Token: 0x040053FD RID: 21501
		public GameObject TotalTalk;

		// Token: 0x040053FE RID: 21502
		public CheckTool checkTool;

		// Token: 0x040053FF RID: 21503
		public GameObject xialian;

		// Token: 0x04005400 RID: 21504
		public GameObject exchangeUI;

		// Token: 0x04005401 RID: 21505
		public Transform JieSuanDongHuaParent;
	}
}
