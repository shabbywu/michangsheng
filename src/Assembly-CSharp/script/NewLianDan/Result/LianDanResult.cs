using System;
using System.Collections.Generic;
using Bag;
using script.NewLianDan.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace script.NewLianDan.Result
{
	// Token: 0x02000AC3 RID: 2755
	public class LianDanResult : BasePanel, IESCClose
	{
		// Token: 0x06004659 RID: 18009 RVA: 0x000323E8 File Offset: 0x000305E8
		public override void Show()
		{
			ESCCloseManager.Inst.RegisterClose(this);
			base.Show();
		}

		// Token: 0x0600465A RID: 18010 RVA: 0x000323FB File Offset: 0x000305FB
		public override void Hide()
		{
			ESCCloseManager.Inst.UnRegisterClose(this);
			this.Clear();
			base.Hide();
		}

		// Token: 0x0600465B RID: 18011 RVA: 0x001DFD3C File Offset: 0x001DDF3C
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

		// Token: 0x0600465C RID: 18012 RVA: 0x001DFDC8 File Offset: 0x001DDFC8
		public void Success(int index, int num)
		{
			this.Show();
			LianDanUIMag.Instance.CaoYaoBag.CreateTempList();
			LianDanUIMag.Instance.CaoYaoBag.UpdateItem(false);
			this.Desc.SetText(Tools.Code64("  " + jsonData.instance.LianDanSuccessItemLeiXin[index.ToString()]["desc"].str).Replace("{Num}", num.ToString()));
			LianDanUIMag.Instance.LianDanCallBack();
		}

		// Token: 0x0600465D RID: 18013 RVA: 0x001DFDC8 File Offset: 0x001DDFC8
		public void Fail(int index, int num)
		{
			this.Show();
			LianDanUIMag.Instance.CaoYaoBag.CreateTempList();
			LianDanUIMag.Instance.CaoYaoBag.UpdateItem(false);
			this.Desc.SetText(Tools.Code64("  " + jsonData.instance.LianDanSuccessItemLeiXin[index.ToString()]["desc"].str).Replace("{Num}", num.ToString()));
			LianDanUIMag.Instance.LianDanCallBack();
		}

		// Token: 0x0600465E RID: 18014 RVA: 0x001DFE54 File Offset: 0x001DE054
		public void ZhaLuLianDan()
		{
			this.Show();
			this.Desc.text = Tools.Code64("  " + jsonData.instance.LianDanSuccessItemLeiXin[16.ToString()]["desc"].str);
			LianDanUIMag.Instance.CaoYaoBag.CreateTempList();
			LianDanUIMag.Instance.CaoYaoBag.UpdateItem(false);
			LianDanUIMag.Instance.LianDanCallBack();
			LianDanUIMag.Instance.LianDanPanel.ZhaLuCallBack();
		}

		// Token: 0x0600465F RID: 18015 RVA: 0x001DFEE4 File Offset: 0x001DE0E4
		private void Clear()
		{
			foreach (BaseSlot baseSlot in this.SlotList)
			{
				baseSlot.SetNull();
			}
		}

		// Token: 0x06004660 RID: 18016 RVA: 0x00032414 File Offset: 0x00030614
		public bool TryEscClose()
		{
			this.Hide();
			return true;
		}

		// Token: 0x04003E7F RID: 15999
		public List<BaseSlot> SlotList = new List<BaseSlot>();

		// Token: 0x04003E80 RID: 16000
		public Text Desc;
	}
}
