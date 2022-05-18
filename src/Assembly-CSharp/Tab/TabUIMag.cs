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
	// Token: 0x02000A55 RID: 2645
	public class TabUIMag : SingletonMono<TabUIMag>, IESCClose
	{
		// Token: 0x0600443A RID: 17466 RVA: 0x00030D7B File Offset: 0x0002EF7B
		private void Awake()
		{
			SingletonMono<TabUIMag>._instance = this;
		}

		// Token: 0x0600443B RID: 17467 RVA: 0x001D2840 File Offset: 0x001D0A40
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

		// Token: 0x0600443C RID: 17468 RVA: 0x001D2A68 File Offset: 0x001D0C68
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

		// Token: 0x0600443D RID: 17469 RVA: 0x001D2B94 File Offset: 0x001D0D94
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

		// Token: 0x0600443E RID: 17470 RVA: 0x00030D83 File Offset: 0x0002EF83
		private void Close()
		{
			if (this.CloseAction != null)
			{
				this.CloseAction.Invoke();
				this.CloseAction = null;
			}
			Object.Destroy(base.gameObject);
		}

		// Token: 0x0600443F RID: 17471 RVA: 0x00030DAA File Offset: 0x0002EFAA
		public bool TryEscClose()
		{
			this.Close();
			return true;
		}

		// Token: 0x06004440 RID: 17472 RVA: 0x00030DB3 File Offset: 0x0002EFB3
		public void ShowBaseData()
		{
			this.BaseDataPanel.SetActive(true);
			this.UpdateBaseUI(new MessageData(0));
		}

		// Token: 0x06004441 RID: 17473 RVA: 0x001D2C28 File Offset: 0x001D0E28
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

		// Token: 0x06004442 RID: 17474 RVA: 0x00030DCD File Offset: 0x0002EFCD
		public void HideBaseData()
		{
			this.BaseDataPanel.SetActive(false);
		}

		// Token: 0x06004443 RID: 17475 RVA: 0x001D2D24 File Offset: 0x001D0F24
		private void OnDestroy()
		{
			ESCCloseManager.Inst.UnRegisterClose(this);
			MessageMag.Instance.Remove(MessageName.MSG_PLAYER_USE_ITEM, new Action<MessageData>(this.UpdateBaseUI));
			MessageMag.Instance.Remove(MessageName.MSG_PLAYER_USE_ITEM, new Action<MessageData>(SingletonMono<TabUIMag>.Instance.TabBag.UseItemCallBack));
			UToolTip.Close();
			SingletonMono<TabUIMag>._instance = null;
		}

		// Token: 0x04003C43 RID: 15427
		public TabSelectMag TabSelect;

		// Token: 0x04003C44 RID: 15428
		public TabShuXingPanel XingPanel;

		// Token: 0x04003C45 RID: 15429
		public TabWuDaoPanel WuDaoPanel;

		// Token: 0x04003C46 RID: 15430
		public TabGongFaPanel GongFaPanel;

		// Token: 0x04003C47 RID: 15431
		public TabShenTongPanel ShenTongPanel;

		// Token: 0x04003C48 RID: 15432
		public TabWuPingPanel WuPingPanel;

		// Token: 0x04003C49 RID: 15433
		public TabShengWangPanel ShengWangPanel;

		// Token: 0x04003C4A RID: 15434
		public TabSystemPanel SystemPanel;

		// Token: 0x04003C4B RID: 15435
		public TabFangAnPanel TabFangAnPanel;

		// Token: 0x04003C4C RID: 15436
		public GameObject BaseDataPanel;

		// Token: 0x04003C4D RID: 15437
		public UnityAction CloseAction;

		// Token: 0x04003C4E RID: 15438
		public Text HPText;

		// Token: 0x04003C4F RID: 15439
		public Transform Fire1;

		// Token: 0x04003C50 RID: 15440
		public Transform Fire2;

		// Token: 0x04003C51 RID: 15441
		public Text ExpText;

		// Token: 0x04003C52 RID: 15442
		public TabBag TabBag;

		// Token: 0x04003C53 RID: 15443
		public List<ITabPanelBase> PanelList = new List<ITabPanelBase>();
	}
}
