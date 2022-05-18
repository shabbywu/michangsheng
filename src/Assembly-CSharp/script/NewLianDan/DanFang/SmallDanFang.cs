using System;
using System.Collections.Generic;
using Bag;
using JSONClass;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace script.NewLianDan.DanFang
{
	// Token: 0x02000AD0 RID: 2768
	public class SmallDanFang : UIBase
	{
		// Token: 0x060046A8 RID: 18088 RVA: 0x001E3B8C File Offset: 0x001E1D8C
		public SmallDanFang(GameObject go, DanFangBase data, BigDanFang bigDanFang)
		{
			this.Parent = bigDanFang;
			this._go = go;
			this.CanMade = false;
			this._go.SetActive(true);
			this.IsSelect = false;
			this.DanFangData = data;
			this.SmallDanLu = base.Get("小丹炉/可炼制标记", true);
			this._selectImg = base.Get("选中", true);
			this._go.GetComponent<FpBtn>().mouseUpEvent.AddListener(new UnityAction(this.Click));
			this._line = base.Get("Line", true);
			Text text = base.Get<Text>("主药/草药");
			text.SetText("");
			foreach (int key in data.ZhuYao1.Keys)
			{
				text.AddText(string.Format("{0}x{1}", _ItemJsonData.DataDict[key].name, data.ZhuYao1[key]));
			}
			foreach (int key2 in data.ZhuYao2.Keys)
			{
				if (text.text.Length > 0)
				{
					text.AddText(",");
				}
				text.AddText(string.Format("{0}x{1}", _ItemJsonData.DataDict[key2].name, data.ZhuYao2[key2]));
			}
			Text text2 = base.Get<Text>("辅药/草药");
			text2.SetText("");
			foreach (int key3 in data.FuYao1.Keys)
			{
				text2.AddText(string.Format("{0}x{1}", _ItemJsonData.DataDict[key3].name, data.FuYao1[key3]));
			}
			foreach (int key4 in data.FuYao2.Keys)
			{
				if (text2.text.Length > 0)
				{
					text2.AddText(",");
				}
				text2.AddText(string.Format("{0}x{1}", _ItemJsonData.DataDict[key4].name, data.FuYao2[key4]));
			}
			if (text2.text.Length < 1)
			{
				text2.SetText("无");
			}
			Text text3 = base.Get<Text>("药引/草药");
			text3.SetText("");
			foreach (int key5 in data.YaoYin.Keys)
			{
				text3.AddText(string.Format("{0}x{1}", _ItemJsonData.DataDict[key5].name, data.YaoYin[key5]));
			}
			if (text3.text.Length < 1)
			{
				text3.SetText("无");
			}
		}

		// Token: 0x060046A9 RID: 18089 RVA: 0x001E3F18 File Offset: 0x001E2118
		public void Click()
		{
			if (!this.IsSelect)
			{
				this.IsSelect = true;
				this._selectImg.SetActive(true);
			}
			LianDanUIMag.Instance.DanFangPanel.SetSmallDanFangCallBack(this);
			if (!this.CanMade)
			{
				return;
			}
			if (LianDanUIMag.Instance.LianDanPanel.DanLu.IsNull())
			{
				UIPopTip.Inst.Pop("请先放入丹炉", PopTipIconType.叹号);
				return;
			}
			if (!this.CanPut())
			{
				UIPopTip.Inst.Pop("丹炉品阶不足", PopTipIconType.叹号);
				return;
			}
			LianDanUIMag.Instance.DanFangPanel.PutCaoYaoByDanFang();
		}

		// Token: 0x060046AA RID: 18090 RVA: 0x001E3FA8 File Offset: 0x001E21A8
		public bool CanPut()
		{
			List<LianDanSlot> caoYaoList = LianDanUIMag.Instance.LianDanPanel.CaoYaoList;
			int num = 0;
			if (this.DanFangData.ZhuYao1.Keys.Count > 0)
			{
				num++;
			}
			if (this.DanFangData.ZhuYao2.Keys.Count > 0)
			{
				num++;
			}
			int num2 = 0;
			if (!caoYaoList[1].IsLock)
			{
				num2++;
			}
			if (!caoYaoList[2].IsLock)
			{
				num2++;
			}
			if (num2 < num)
			{
				return false;
			}
			int num3 = 0;
			if (this.DanFangData.FuYao1.Keys.Count > 0)
			{
				num3++;
			}
			if (this.DanFangData.FuYao2.Keys.Count > 0)
			{
				num3++;
			}
			int num4 = 0;
			if (!caoYaoList[3].IsLock)
			{
				num4++;
			}
			if (!caoYaoList[4].IsLock)
			{
				num4++;
			}
			return num4 >= num3;
		}

		// Token: 0x060046AB RID: 18091 RVA: 0x0003269A File Offset: 0x0003089A
		public void CancelSelect()
		{
			this.IsSelect = false;
			this._selectImg.SetActive(false);
		}

		// Token: 0x060046AC RID: 18092 RVA: 0x000326AF File Offset: 0x000308AF
		public void HideLine()
		{
			this._line.SetActive(false);
		}

		// Token: 0x060046AD RID: 18093 RVA: 0x000326BD File Offset: 0x000308BD
		public void Destroy()
		{
			this.IsDestroy = true;
			Object.Destroy(this._go);
		}

		// Token: 0x04003EC1 RID: 16065
		public DanFangBase DanFangData;

		// Token: 0x04003EC2 RID: 16066
		public GameObject SmallDanLu;

		// Token: 0x04003EC3 RID: 16067
		private GameObject _line;

		// Token: 0x04003EC4 RID: 16068
		private GameObject _selectImg;

		// Token: 0x04003EC5 RID: 16069
		public BigDanFang Parent;

		// Token: 0x04003EC6 RID: 16070
		public bool CanMade;

		// Token: 0x04003EC7 RID: 16071
		public bool IsSelect;

		// Token: 0x04003EC8 RID: 16072
		public bool IsDestroy;
	}
}
