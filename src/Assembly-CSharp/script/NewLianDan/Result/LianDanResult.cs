using System;
using System.Collections.Generic;
using Bag;
using script.NewLianDan.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace script.NewLianDan.Result
{
	// Token: 0x020009F9 RID: 2553
	public class LianDanResult : BasePanel, IESCClose
	{
		// Token: 0x060046B3 RID: 18099 RVA: 0x001DE3E3 File Offset: 0x001DC5E3
		public override void Show()
		{
			ESCCloseManager.Inst.RegisterClose(this);
			base.Show();
		}

		// Token: 0x060046B4 RID: 18100 RVA: 0x001DE3F6 File Offset: 0x001DC5F6
		public override void Hide()
		{
			ESCCloseManager.Inst.UnRegisterClose(this);
			this.Clear();
			base.Hide();
		}

		// Token: 0x060046B5 RID: 18101 RVA: 0x001DE410 File Offset: 0x001DC610
		public LianDanResult(GameObject go)
		{
			this._go = go;
			this.Desc = base.Get<Text>("描述");
			base.Get<FpBtn>("OkBtn").mouseUpEvent.AddListener(new UnityAction(this.Hide));
			for (int i = 1; i <= 6; i++)
			{
				this.SlotList.Add(base.Get<BaseSlot>(string.Format("ItemList/{0}", i)));
			}
			this.Clear();
		}

		// Token: 0x060046B6 RID: 18102 RVA: 0x001DE49C File Offset: 0x001DC69C
		public void Success(int index, int num)
		{
			this.Show();
			LianDanUIMag.Instance.CaoYaoBag.CreateTempList();
			LianDanUIMag.Instance.CaoYaoBag.UpdateItem(false);
			this.Desc.SetText(Tools.Code64("  " + jsonData.instance.LianDanSuccessItemLeiXin[index.ToString()]["desc"].str).Replace("{Num}", num.ToString()));
			LianDanUIMag.Instance.LianDanCallBack();
		}

		// Token: 0x060046B7 RID: 18103 RVA: 0x001DE528 File Offset: 0x001DC728
		public void Fail(int index, int num)
		{
			this.Show();
			LianDanUIMag.Instance.CaoYaoBag.CreateTempList();
			LianDanUIMag.Instance.CaoYaoBag.UpdateItem(false);
			this.Desc.SetText(Tools.Code64("  " + jsonData.instance.LianDanSuccessItemLeiXin[index.ToString()]["desc"].str).Replace("{Num}", num.ToString()));
			LianDanUIMag.Instance.LianDanCallBack();
		}

		// Token: 0x060046B8 RID: 18104 RVA: 0x001DE5B4 File Offset: 0x001DC7B4
		public void ZhaLuLianDan()
		{
			this.Show();
			this.Desc.text = Tools.Code64("  " + jsonData.instance.LianDanSuccessItemLeiXin[16.ToString()]["desc"].str);
			LianDanUIMag.Instance.CaoYaoBag.CreateTempList();
			LianDanUIMag.Instance.CaoYaoBag.UpdateItem(false);
			LianDanUIMag.Instance.LianDanCallBack();
			LianDanUIMag.Instance.LianDanPanel.ZhaLuCallBack();
		}

		// Token: 0x060046B9 RID: 18105 RVA: 0x001DE644 File Offset: 0x001DC844
		private void Clear()
		{
			foreach (BaseSlot baseSlot in this.SlotList)
			{
				baseSlot.SetNull();
			}
		}

		// Token: 0x060046BA RID: 18106 RVA: 0x001DE694 File Offset: 0x001DC894
		public bool TryEscClose()
		{
			this.Hide();
			return true;
		}

		// Token: 0x04004815 RID: 18453
		public List<BaseSlot> SlotList = new List<BaseSlot>();

		// Token: 0x04004816 RID: 18454
		public Text Desc;
	}
}
