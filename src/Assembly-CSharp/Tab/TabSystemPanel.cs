using System;
using System.Collections.Generic;
using KBEngine;
using script.NewLianDan;
using script.Submit;
using UnityEngine;
using UnityEngine.Events;
using YSGame;

namespace Tab
{
	// Token: 0x02000703 RID: 1795
	[Serializable]
	public class TabSystemPanel : ITabPanelBase
	{
		// Token: 0x06003996 RID: 14742 RVA: 0x0018A378 File Offset: 0x00188578
		public TabSystemPanel(GameObject gameObject)
		{
			this.PanelList = new List<ISysPanelBase>();
			this._go = gameObject;
			this._isInit = false;
		}

		// Token: 0x06003997 RID: 14743 RVA: 0x0018A39C File Offset: 0x0018859C
		private void Init()
		{
			this.SavePanel = new TabSavePanel(base.Get("Select/Panel/保存", true));
			this.LoadPanel = new TabLoadPanel(base.Get("Select/Panel/读取", true));
			this.SetPanel = new TabSetPanel(base.Get("Select/Panel/设置", true));
			this.SelectMag = new SysSelectMag(base.Get("Select", true));
			base.Get<FpBtn>("Select/返回标题/UnSelect").mouseUpEvent.AddListener(new UnityAction(this.ReturnTittle));
			base.Get<FpBtn>("Select/退出/UnSelect").mouseUpEvent.AddListener(new UnityAction(this.QuitGame));
			this._isInit = true;
			this.SelectMag.SetDeafultSelect(0);
		}

		// Token: 0x06003998 RID: 14744 RVA: 0x0018A45A File Offset: 0x0018865A
		public override void Show()
		{
			if (!this._isInit)
			{
				this.Init();
				this._isInit = true;
			}
			this._go.SetActive(true);
		}

		// Token: 0x06003999 RID: 14745 RVA: 0x0018A47D File Offset: 0x0018867D
		public void ReturnTittle()
		{
			TySelect.inst.Show("是否要返回主界面？", delegate
			{
				if (FpUIMag.inst != null)
				{
					Object.Destroy(FpUIMag.inst.gameObject);
				}
				if (TpUIMag.inst != null)
				{
					Object.Destroy(TpUIMag.inst.gameObject);
				}
				if (SubmitUIMag.Inst != null)
				{
					SubmitUIMag.Inst.Close();
				}
				if (LianDanUIMag.Instance != null)
				{
					Object.Destroy(LianDanUIMag.Instance.gameObject);
				}
				if (LianQiTotalManager.inst != null)
				{
					Object.Destroy(LianQiTotalManager.inst.gameObject);
				}
				SingletonMono<TabUIMag>.Instance.TryEscClose();
				YSSaveGame.Reset();
				KBEngineApp.app.entities[10] = null;
				KBEngineApp.app.entities.Remove(10);
				Tools.instance.loadOtherScenes("MainMenu");
			}, null, true);
		}

		// Token: 0x0600399A RID: 14746 RVA: 0x0018A4AF File Offset: 0x001886AF
		public void QuitGame()
		{
			TySelect.inst.Show("是否要退出游戏？", new UnityAction(Application.Quit), null, true);
		}

		// Token: 0x040031AF RID: 12719
		private bool _isInit;

		// Token: 0x040031B0 RID: 12720
		public TabSavePanel SavePanel;

		// Token: 0x040031B1 RID: 12721
		public TabLoadPanel LoadPanel;

		// Token: 0x040031B2 RID: 12722
		public TabSetPanel SetPanel;

		// Token: 0x040031B3 RID: 12723
		public SysSelectMag SelectMag;

		// Token: 0x040031B4 RID: 12724
		public List<ISysPanelBase> PanelList;
	}
}
