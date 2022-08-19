using System;
using System.Collections.Generic;
using JSONClass;
using KBEngine;
using PaiMai;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Tab
{
	// Token: 0x02000708 RID: 1800
	public class TabUIMag : SingletonMono<TabUIMag>, IESCClose
	{
		// Token: 0x060039B6 RID: 14774 RVA: 0x0018B0EC File Offset: 0x001892EC
		private void Awake()
		{
			SingletonMono<TabUIMag>._instance = this;
		}

		// Token: 0x060039B7 RID: 14775 RVA: 0x0018B0F4 File Offset: 0x001892F4
		private void Init()
		{
			ESCCloseManager.Inst.RegisterClose(this);
			this.BaseDataPanel = base.transform.Find("CommonUI/基本属性").gameObject;
			this.HPText = base.transform.Find("CommonUI/基本属性/Hp/HP_Text").GetComponent<Text>();
			this.Fire1 = base.transform.Find("CommonUI/基本属性/Hp/Bg/Bg2");
			this.ExpText = base.transform.Find("CommonUI/基本属性/Exp/Exp_Text").GetComponent<Text>();
			this.Fire2 = base.transform.Find("CommonUI/基本属性/Exp/Bg/Bg2");
			this.XingPanel = new TabShuXingPanel(base.transform.Find("TabSelect/Panel/属性").gameObject);
			this.WuDaoPanel = new TabWuDaoPanel(base.transform.Find("TabSelect/Panel/悟道").gameObject);
			this.GongFaPanel = new TabGongFaPanel(base.transform.Find("TabSelect/Panel/功法").gameObject);
			this.ShenTongPanel = new TabShenTongPanel(base.transform.Find("TabSelect/Panel/神通").gameObject);
			this.WuPingPanel = new TabWuPingPanel(base.transform.Find("TabSelect/Panel/物品").gameObject);
			this.ShengWangPanel = new TabShengWangPanel(base.transform.Find("TabSelect/Panel/声望").gameObject);
			this.SystemPanel = new TabSystemPanel(base.transform.Find("TabSelect/Panel/系统").gameObject);
			this.TabSelect = new TabSelectMag(base.transform.Find("TabSelect").gameObject);
			this.TabBag = new TabBag(base.transform.Find("Bag").gameObject);
			this.TabFangAnPanel = new TabFangAnPanel(base.transform.Find("CommonUI/切换").gameObject);
			base.transform.Find("TabSelect/Panel/Close").GetComponent<FpBtn>().mouseUpEvent.AddListener(new UnityAction(this.Close));
			this.TabSelect.SetDeafultSelect(0);
			MessageMag.Instance.Register(MessageName.MSG_PLAYER_USE_ITEM, new Action<MessageData>(this.UpdateBaseUI));
		}

		// Token: 0x060039B8 RID: 14776 RVA: 0x0018B31C File Offset: 0x0018951C
		public static void OpenTab(int index = 0, UnityAction action = null)
		{
			if (RoundManager.instance != null)
			{
				return;
			}
			if (SingletonMono<PaiMaiUiMag>.Instance != null)
			{
				return;
			}
			if (!Tools.instance.canClick(false, false))
			{
				return;
			}
			if (LianQiTotalManager.inst != null)
			{
				LianQiTotalManager.inst.gameObject.SetActive(false);
				action = delegate()
				{
					if (LianQiTotalManager.inst != null)
					{
						LianQiTotalManager.inst.gameObject.SetActive(true);
					}
				};
			}
			else if (LianDanSystemManager.inst != null)
			{
				LianDanSystemManager.inst.gameObject.SetActive(false);
				action = delegate()
				{
					if (LianDanSystemManager.inst != null)
					{
						LianDanSystemManager.inst.gameObject.SetActive(true);
					}
				};
			}
			ResManager.inst.LoadPrefab("TabPanel").Inst(NewUICanvas.Inst.gameObject.transform).AddComponent<TabUIMag>();
			SingletonMono<TabUIMag>.Instance.transform.localPosition = Vector3.zero;
			SingletonMono<TabUIMag>.Instance.Init();
			SingletonMono<TabUIMag>.Instance.transform.localScale = Vector3.one;
			SingletonMono<TabUIMag>.Instance.TabSelect.SetDeafultSelect(index);
			SingletonMono<TabUIMag>.Instance.CloseAction = action;
		}

		// Token: 0x060039B9 RID: 14777 RVA: 0x0018B448 File Offset: 0x00189648
		public static void OpenTab2(int index = 0)
		{
			if (RoundManager.instance != null)
			{
				return;
			}
			if (SingletonMono<PaiMaiUiMag>.Instance != null)
			{
				return;
			}
			ResManager.inst.LoadPrefab("TabPanel").Inst(NewUICanvas.Inst.gameObject.transform).AddComponent<TabUIMag>();
			SingletonMono<TabUIMag>.Instance.transform.localPosition = Vector3.zero;
			SingletonMono<TabUIMag>.Instance.Init();
			SingletonMono<TabUIMag>.Instance.transform.localScale = Vector3.one;
			SingletonMono<TabUIMag>.Instance.TabSelect.SetDeafultSelect(index);
		}

		// Token: 0x060039BA RID: 14778 RVA: 0x0018B4DC File Offset: 0x001896DC
		private void Close()
		{
			if (this.CloseAction != null)
			{
				this.CloseAction.Invoke();
				this.CloseAction = null;
			}
			Object.Destroy(base.gameObject);
		}

		// Token: 0x060039BB RID: 14779 RVA: 0x0018B503 File Offset: 0x00189703
		public bool TryEscClose()
		{
			this.Close();
			return true;
		}

		// Token: 0x060039BC RID: 14780 RVA: 0x0018B50C File Offset: 0x0018970C
		public void ShowBaseData()
		{
			this.BaseDataPanel.SetActive(true);
			this.UpdateBaseUI(new MessageData(0));
		}

		// Token: 0x060039BD RID: 14781 RVA: 0x0018B528 File Offset: 0x00189728
		public void UpdateBaseUI(MessageData data)
		{
			Avatar player = Tools.instance.getPlayer();
			this.HPText.SetText(string.Format("{0}/{1}", player.HP, player.HP_Max));
			float num = (float)player.HP / (float)player.HP_Max;
			this.Fire1.localScale = new Vector3(num * 0.6f + 0.4f, num * 0.6f + 0.4f, 1f);
			int maxExp = LevelUpDataJsonData.DataDict[(int)player.level].MaxExp;
			float num2 = player.exp / (float)maxExp;
			this.Fire2.localScale = new Vector3(num2 * 0.6f + 0.4f, num2 * 0.6f + 0.4f, 1f);
			this.ExpText.SetText(player.exp.ToCNNumberWithUnit() + "/" + ((ulong)((long)maxExp)).ToCNNumberWithUnit());
		}

		// Token: 0x060039BE RID: 14782 RVA: 0x0018B622 File Offset: 0x00189822
		public void HideBaseData()
		{
			this.BaseDataPanel.SetActive(false);
		}

		// Token: 0x060039BF RID: 14783 RVA: 0x0018B630 File Offset: 0x00189830
		private void OnDestroy()
		{
			ESCCloseManager.Inst.UnRegisterClose(this);
			MessageMag.Instance.Remove(MessageName.MSG_PLAYER_USE_ITEM, new Action<MessageData>(this.UpdateBaseUI));
			MessageMag.Instance.Remove(MessageName.MSG_PLAYER_USE_ITEM, new Action<MessageData>(SingletonMono<TabUIMag>.Instance.TabBag.UseItemCallBack));
			UToolTip.Close();
			SingletonMono<TabUIMag>._instance = null;
		}

		// Token: 0x040031C7 RID: 12743
		public TabSelectMag TabSelect;

		// Token: 0x040031C8 RID: 12744
		public TabShuXingPanel XingPanel;

		// Token: 0x040031C9 RID: 12745
		public TabWuDaoPanel WuDaoPanel;

		// Token: 0x040031CA RID: 12746
		public TabGongFaPanel GongFaPanel;

		// Token: 0x040031CB RID: 12747
		public TabShenTongPanel ShenTongPanel;

		// Token: 0x040031CC RID: 12748
		public TabWuPingPanel WuPingPanel;

		// Token: 0x040031CD RID: 12749
		public TabShengWangPanel ShengWangPanel;

		// Token: 0x040031CE RID: 12750
		public TabSystemPanel SystemPanel;

		// Token: 0x040031CF RID: 12751
		public TabFangAnPanel TabFangAnPanel;

		// Token: 0x040031D0 RID: 12752
		public GameObject BaseDataPanel;

		// Token: 0x040031D1 RID: 12753
		public UnityAction CloseAction;

		// Token: 0x040031D2 RID: 12754
		public Text HPText;

		// Token: 0x040031D3 RID: 12755
		public Transform Fire1;

		// Token: 0x040031D4 RID: 12756
		public Transform Fire2;

		// Token: 0x040031D5 RID: 12757
		public Text ExpText;

		// Token: 0x040031D6 RID: 12758
		public TabBag TabBag;

		// Token: 0x040031D7 RID: 12759
		public List<ITabPanelBase> PanelList = new List<ITabPanelBase>();
	}
}
