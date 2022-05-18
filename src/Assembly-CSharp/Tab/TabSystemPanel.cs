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
	// Token: 0x02000A4F RID: 2639
	[Serializable]
	public class TabSystemPanel : ITabPanelBase
	{
		// Token: 0x06004417 RID: 17431 RVA: 0x00030C37 File Offset: 0x0002EE37
		public TabSystemPanel(GameObject gameObject)
		{
			this.PanelList = new List<ISysPanelBase>();
			this._go = gameObject;
			this._isInit = false;
		}

		// Token: 0x06004418 RID: 17432 RVA: 0x001D1B34 File Offset: 0x001CFD34
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

		// Token: 0x06004419 RID: 17433 RVA: 0x00030C58 File Offset: 0x0002EE58
		public override void Show()
		{
			if (!this._isInit)
			{
				this.Init();
				this._isInit = true;
			}
			this._go.SetActive(true);
		}

		// Token: 0x0600441A RID: 17434 RVA: 0x00030C7B File Offset: 0x0002EE7B
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

		// Token: 0x0600441B RID: 17435 RVA: 0x00030CAD File Offset: 0x0002EEAD
		public void QuitGame()
		{
			TySelect.inst.Show("是否要退出游戏？", new UnityAction(Application.Quit), null, true);
		}

		// Token: 0x04003C29 RID: 15401
		private bool _isInit;

		// Token: 0x04003C2A RID: 15402
		public TabSavePanel SavePanel;

		// Token: 0x04003C2B RID: 15403
		public TabLoadPanel LoadPanel;

		// Token: 0x04003C2C RID: 15404
		public TabSetPanel SetPanel;

		// Token: 0x04003C2D RID: 15405
		public SysSelectMag SelectMag;

		// Token: 0x04003C2E RID: 15406
		public List<ISysPanelBase> PanelList;
	}
}
