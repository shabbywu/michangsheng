using System;
using System.Collections.Generic;
using KBEngine;
using script.YarnEditor.Manager;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace GUIPackage
{
	// Token: 0x02000A72 RID: 2674
	public class UI_Manager : MonoBehaviour
	{
		// Token: 0x06004B24 RID: 19236 RVA: 0x001FF430 File Offset: 0x001FD630
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

		// Token: 0x06004B25 RID: 19237 RVA: 0x001FF601 File Offset: 0x001FD801
		private void Awake()
		{
			UI_Manager.inst = this;
			if (this.checkTool == null)
			{
				this.checkTool = base.GetComponent<CheckTool>();
			}
		}

		// Token: 0x06004B26 RID: 19238 RVA: 0x001FF624 File Offset: 0x001FD824
		private void Start()
		{
			this.UI.Add(this.equipment);
			this.UI.Add(this.inventory);
			this.UI.Add(this.skill);
			UI_Manager.inst = this;
			base.Invoke("openPanel", 0.3f);
		}

		// Token: 0x06004B27 RID: 19239 RVA: 0x001FF67A File Offset: 0x001FD87A
		private void showBtn()
		{
			Object.Instantiate<GameObject>(ResManager.inst.LoadPrefab("threeScreenUI"), UI_Manager.inst.gameObject.transform);
		}

		// Token: 0x06004B28 RID: 19240 RVA: 0x001FF6A0 File Offset: 0x001FD8A0
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

		// Token: 0x06004B29 RID: 19241 RVA: 0x001FF77C File Offset: 0x001FD97C
		private void UIOrder()
		{
			for (int i = 0; i < this.UI.Count; i++)
			{
				this.SetDepth(this.UI[i], i + 1);
			}
		}

		// Token: 0x06004B2A RID: 19242 RVA: 0x001FF7B4 File Offset: 0x001FD9B4
		private void UI_Click(GameObject ui, bool isPress)
		{
			if (Input.GetMouseButtonDown(0))
			{
				this.UI_Top(ui.transform.parent.parent);
			}
		}

		// Token: 0x06004B2B RID: 19243 RVA: 0x001FF7D4 File Offset: 0x001FD9D4
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

		// Token: 0x06004B2C RID: 19244 RVA: 0x001FF835 File Offset: 0x001FDA35
		private void UI_Event(Transform ui)
		{
			UIEventListener.Get(ui.Find("Win/BG").gameObject).onPress = new UIEventListener.BoolDelegate(this.UI_Click);
		}

		// Token: 0x06004B2D RID: 19245 RVA: 0x001FF860 File Offset: 0x001FDA60
		private void SetDepth(Transform ui, int depth)
		{
			if (ui.name == "store" || ui.name == "skill_UI")
			{
				ui.Find("Win/Scroll View").GetComponent<UIPanel>().depth = depth;
			}
			ui.Find("Win").GetComponent<UIPanel>().depth = depth;
		}

		// Token: 0x06004B2E RID: 19246 RVA: 0x001FF8BD File Offset: 0x001FDABD
		private int GetDepth(Transform ui)
		{
			return ui.GetComponentInChildren<UIPanel>().depth;
		}

		// Token: 0x06004B2F RID: 19247 RVA: 0x001FF8CA File Offset: 0x001FDACA
		public void PlayeJieSuanAnimation(string content, UnityAction action)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(ResManager.inst.LoadPrefab("BigGuanDongHua"), this.JieSuanDongHuaParent);
			gameObject.SetActive(false);
			gameObject.GetComponent<JueSuanAnimation>().Play(content, action);
		}

		// Token: 0x06004B30 RID: 19248 RVA: 0x001FF8F9 File Offset: 0x001FDAF9
		public void showBlack()
		{
			if (this.BlackGameObject != null)
			{
				base.Invoke("hideBlack", 0.5f);
			}
		}

		// Token: 0x06004B31 RID: 19249 RVA: 0x001FF919 File Offset: 0x001FDB19
		public void hideBlack()
		{
			this.BlackGameObject.SetActive(false);
			this.BlackGameObject.SetActive(true);
			Tools.canClickFlag = true;
		}

		// Token: 0x04004A48 RID: 19016
		public Transform equipment;

		// Token: 0x04004A49 RID: 19017
		public Transform inventory;

		// Token: 0x04004A4A RID: 19018
		public Transform short_cut;

		// Token: 0x04004A4B RID: 19019
		public Transform skill;

		// Token: 0x04004A4C RID: 19020
		public Transform store;

		// Token: 0x04004A4D RID: 19021
		public Transform temp;

		// Token: 0x04004A4E RID: 19022
		public Transform tooltip;

		// Token: 0x04004A4F RID: 19023
		public List<Transform> UI = new List<Transform>();

		// Token: 0x04004A50 RID: 19024
		public static UI_Manager inst;

		// Token: 0x04004A51 RID: 19025
		public Camera RootCamera;

		// Token: 0x04004A52 RID: 19026
		public headMag headMag;

		// Token: 0x04004A53 RID: 19027
		public Shop_UI shop_UI;

		// Token: 0x04004A54 RID: 19028
		public Inventory2 inventory2;

		// Token: 0x04004A55 RID: 19029
		public GameObject BlackGameObject;

		// Token: 0x04004A56 RID: 19030
		public GameObject CraftingList;

		// Token: 0x04004A57 RID: 19031
		public GameObject BlackImage;

		// Token: 0x04004A58 RID: 19032
		public GameObject TotalTalk;

		// Token: 0x04004A59 RID: 19033
		public CheckTool checkTool;

		// Token: 0x04004A5A RID: 19034
		public GameObject xialian;

		// Token: 0x04004A5B RID: 19035
		public GameObject exchangeUI;

		// Token: 0x04004A5C RID: 19036
		public Transform JieSuanDongHuaParent;
	}
}
