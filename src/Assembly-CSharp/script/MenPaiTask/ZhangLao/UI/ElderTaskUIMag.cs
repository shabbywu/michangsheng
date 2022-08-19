using System;
using script.MenPaiTask.ZhangLao.UI.Base;
using script.MenPaiTask.ZhangLao.UI.UI;
using UnityEngine;

namespace script.MenPaiTask.ZhangLao.UI
{
	// Token: 0x02000A0D RID: 2573
	public class ElderTaskUIMag : MonoBehaviour, IESCClose
	{
		// Token: 0x0600473F RID: 18239 RVA: 0x001E2B74 File Offset: 0x001E0D74
		public static void Open()
		{
			ResManager.inst.LoadPrefab("ElderTaskUI").Inst(NewUICanvas.Inst.transform);
		}

		// Token: 0x06004740 RID: 18240 RVA: 0x001E2B98 File Offset: 0x001E0D98
		private void Awake()
		{
			ElderTaskUIMag.Inst = this;
			this.CreateElderTaskUI = new CreateElderTaskUI(base.transform.Find("发布任务界面").gameObject);
			this.ElderTaskUI = new ElderTaskUI(base.transform.Find("任务浏览界面").gameObject);
			this.Bag = base.transform.Find("背包").GetComponent<ElderTaskBag>();
			ESCCloseManager.Inst.RegisterClose(ElderTaskUIMag.Inst);
			this.OpenElderTaskUI();
		}

		// Token: 0x06004741 RID: 18241 RVA: 0x001E2C1B File Offset: 0x001E0E1B
		public void OpenCreateElderTaskUI()
		{
			if (!this.CreateElderTaskUI.IsActive())
			{
				this.CreateElderTaskUI.Show();
				this.ElderTaskUI.Hide();
			}
		}

		// Token: 0x06004742 RID: 18242 RVA: 0x001E2C40 File Offset: 0x001E0E40
		public void OpenElderTaskUI()
		{
			if (!this.ElderTaskUI.IsActive())
			{
				this.ElderTaskUI.Show();
				this.CreateElderTaskUI.Hide();
			}
		}

		// Token: 0x06004743 RID: 18243 RVA: 0x001E2C65 File Offset: 0x001E0E65
		public void Close()
		{
			Object.Destroy(base.gameObject);
			ESCCloseManager.Inst.UnRegisterClose(ElderTaskUIMag.Inst);
			ElderTaskUIMag.Inst = null;
		}

		// Token: 0x06004744 RID: 18244 RVA: 0x001E2C87 File Offset: 0x001E0E87
		public bool TryEscClose()
		{
			this.Close();
			return true;
		}

		// Token: 0x04004864 RID: 18532
		public static ElderTaskUIMag Inst;

		// Token: 0x04004865 RID: 18533
		public CreateElderTaskUI CreateElderTaskUI;

		// Token: 0x04004866 RID: 18534
		public ElderTaskBag Bag;

		// Token: 0x04004867 RID: 18535
		public ElderTaskUI ElderTaskUI;
	}
}
