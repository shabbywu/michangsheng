using System;
using System.Collections.Generic;
using Bag;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace script.NewLianDan.DanFang
{
	// Token: 0x020009FD RID: 2557
	public class BigDanFang : UIBase
	{
		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x060046DB RID: 18139 RVA: 0x001DFFB0 File Offset: 0x001DE1B0
		public bool IsSelect
		{
			get
			{
				return this._isSelect;
			}
		}

		// Token: 0x060046DC RID: 18140 RVA: 0x001DFFB8 File Offset: 0x001DE1B8
		public BigDanFang(GameObject go, DanFangData data, BaseItem baseItem)
		{
			this._go = go;
			this.Data = data;
			this.BaseItem = baseItem;
			base.Get<FpBtn>("未选中").mouseUpEvent.AddListener(new UnityAction(this.Select));
			base.Get<FpBtn>("未选中").mouseEnterEvent.AddListener(new UnityAction(this.Enter));
			base.Get<FpBtn>("未选中").mouseOutEvent.AddListener(new UnityAction(this.Out));
			this._unSelect = base.Get("未选中", true);
			this._canMade1 = base.Get("未选中/小丹炉/可炼制标记", true);
			base.Get<Text>("未选中/名称").SetText(data.Name);
			base.Get<FpBtn>("已选中").mouseUpEvent.AddListener(new UnityAction(this.UnSelect));
			base.Get<FpBtn>("已选中").mouseEnterEvent.AddListener(new UnityAction(this.Enter));
			base.Get<FpBtn>("已选中").mouseOutEvent.AddListener(new UnityAction(this.Out));
			this._select = base.Get("已选中", true);
			base.Get<Text>("已选中/名称").SetText(data.Name);
			this._canMade2 = base.Get("已选中/小丹炉/可炼制标记", true);
			this._zhuYao1 = base.Get<Text>("已选中/药性/主药");
			this._fuYao1 = base.Get<Text>("已选中/药性/辅药");
			this._isSelect = false;
			this._go.SetActive(true);
			this.CreateChild();
			this.UpdateState();
		}

		// Token: 0x060046DD RID: 18141 RVA: 0x001E0168 File Offset: 0x001DE368
		public void CreateChild()
		{
			this.Clear();
			GameObject gameObject = base.Get("已选中/Temp", true);
			float x = gameObject.transform.localPosition.x;
			float num = gameObject.transform.localPosition.y;
			for (int i = 0; i < this.Data.DanFangBases.Count; i++)
			{
				DanFangBase data = this.Data.DanFangBases[i];
				GameObject gameObject2 = gameObject.Inst(gameObject.transform.parent);
				gameObject2.transform.localPosition = new Vector2(x, num);
				this.ChildList.Add(new SmallDanFang(gameObject2, data, this));
				num -= 112f;
			}
			if (this.ChildList.Count > 0)
			{
				this.ChildList[this.ChildList.Count - 1].HideLine();
			}
		}

		// Token: 0x060046DE RID: 18142 RVA: 0x001E024B File Offset: 0x001DE44B
		public float GetHeight()
		{
			return Math.Abs(this.ChildList[this.ChildList.Count - 1].GetTransform().localPosition.y) + 107f;
		}

		// Token: 0x060046DF RID: 18143 RVA: 0x001E0280 File Offset: 0x001DE480
		private void Select()
		{
			this._isSelect = true;
			this._unSelect.SetActive(false);
			this._select.SetActive(true);
			this.CreateChild();
			foreach (SmallDanFang smallDanFang in this.ChildList)
			{
				if (LianDanUIMag.Instance.CheckCanLianZhi(smallDanFang.DanFangData.Json))
				{
					smallDanFang.CanMade = true;
					smallDanFang.SmallDanLu.SetActive(true);
				}
				else
				{
					smallDanFang.CanMade = false;
					smallDanFang.SmallDanLu.SetActive(false);
				}
			}
			LianDanUIMag.Instance.DanFangPanel.SetBigDanFangCallBack(this);
		}

		// Token: 0x060046E0 RID: 18144 RVA: 0x001E0340 File Offset: 0x001DE540
		public void Enter()
		{
			if (ToolTipsMag.Inst == null)
			{
				ResManager.inst.LoadPrefab("ToolTips").Inst(NewUICanvas.Inst.transform);
			}
			ToolTipsMag.Inst.Show(this.BaseItem, new Vector2(LianDanUIMag.Instance.Vector2.position.x, ToolTipsMag.Inst.GetMouseY()));
		}

		// Token: 0x060046E1 RID: 18145 RVA: 0x001E03AC File Offset: 0x001DE5AC
		public void Out()
		{
			ToolTipsMag.Inst.Close();
		}

		// Token: 0x060046E2 RID: 18146 RVA: 0x001E03B8 File Offset: 0x001DE5B8
		private void UnSelect()
		{
			this._isSelect = false;
			this._unSelect.SetActive(true);
			this._select.SetActive(false);
			LianDanUIMag.Instance.DanFangPanel.UpdatePosition();
		}

		// Token: 0x060046E3 RID: 18147 RVA: 0x001E03E8 File Offset: 0x001DE5E8
		public void UpdateState()
		{
			DanFangBase danFangData = this.ChildList[0].DanFangData;
			this._zhuYao1.SetText("");
			if (danFangData.ZhuYaoYaoXin1 > 0)
			{
				this._zhuYao1.SetText(Tools.getLiDanLeiXinStr(danFangData.ZhuYaoYaoXin1));
			}
			if (danFangData.ZhuYaoYaoXin2 > 0)
			{
				string liDanLeiXinStr = Tools.getLiDanLeiXinStr(danFangData.ZhuYaoYaoXin2);
				if (!this._zhuYao1.text.Contains(liDanLeiXinStr))
				{
					if (this._zhuYao1.text.Length > 0)
					{
						this._zhuYao1.AddText(",");
					}
					this._zhuYao1.AddText(Tools.getLiDanLeiXinStr(danFangData.ZhuYaoYaoXin2));
				}
			}
			this._fuYao1.SetText("");
			if (danFangData.FuYaoYaoXin1 > 0)
			{
				this._fuYao1.SetText(Tools.getLiDanLeiXinStr(danFangData.FuYaoYaoXin1));
			}
			if (danFangData.FuYaoYaoXin2 > 0)
			{
				string liDanLeiXinStr2 = Tools.getLiDanLeiXinStr(danFangData.FuYaoYaoXin2);
				if (!this._fuYao1.text.Contains(liDanLeiXinStr2))
				{
					if (this._fuYao1.text.Length > 0)
					{
						this._fuYao1.AddText(",");
					}
					this._fuYao1.AddText(Tools.getLiDanLeiXinStr(danFangData.FuYaoYaoXin2));
				}
			}
			if (this.CheckCanMade())
			{
				this._canMade1.SetActive(true);
				this._canMade2.SetActive(true);
				return;
			}
			this._canMade1.SetActive(false);
			this._canMade2.SetActive(false);
		}

		// Token: 0x060046E4 RID: 18148 RVA: 0x001E0560 File Offset: 0x001DE760
		public bool CheckCanMade()
		{
			if (this.ChildList.Count == 0)
			{
				return false;
			}
			bool result = false;
			foreach (SmallDanFang smallDanFang in this.ChildList)
			{
				if (LianDanUIMag.Instance.CheckCanLianZhi(smallDanFang.DanFangData.Json))
				{
					smallDanFang.CanMade = true;
					result = true;
					smallDanFang.SmallDanLu.SetActive(true);
				}
				else
				{
					smallDanFang.CanMade = false;
					smallDanFang.SmallDanLu.SetActive(false);
				}
			}
			return result;
		}

		// Token: 0x060046E5 RID: 18149 RVA: 0x001E0600 File Offset: 0x001DE800
		private void Clear()
		{
			if (this.ChildList.Count > 0)
			{
				foreach (SmallDanFang smallDanFang in this.ChildList)
				{
					smallDanFang.Destroy();
				}
			}
			this.ChildList = new List<SmallDanFang>();
		}

		// Token: 0x060046E6 RID: 18150 RVA: 0x001E066C File Offset: 0x001DE86C
		public void RemoveChild(SmallDanFang smallDanFang)
		{
			this.ChildList.Remove(smallDanFang);
			this.Data.DanFangBases.Remove(smallDanFang.DanFangData);
			smallDanFang.Destroy();
		}

		// Token: 0x060046E7 RID: 18151 RVA: 0x001E0698 File Offset: 0x001DE898
		public void Destroy()
		{
			Object.Destroy(this._go);
		}

		// Token: 0x04004823 RID: 18467
		private bool _isSelect;

		// Token: 0x04004824 RID: 18468
		public DanFangData Data;

		// Token: 0x04004825 RID: 18469
		public BaseItem BaseItem;

		// Token: 0x04004826 RID: 18470
		private GameObject _unSelect;

		// Token: 0x04004827 RID: 18471
		private GameObject _canMade1;

		// Token: 0x04004828 RID: 18472
		private GameObject _select;

		// Token: 0x04004829 RID: 18473
		private GameObject _canMade2;

		// Token: 0x0400482A RID: 18474
		private Text _zhuYao1;

		// Token: 0x0400482B RID: 18475
		private Text _fuYao1;

		// Token: 0x0400482C RID: 18476
		public List<SmallDanFang> ChildList = new List<SmallDanFang>();
	}
}
