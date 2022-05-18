using System;
using System.Collections.Generic;
using Bag;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace script.NewLianDan.DanFang
{
	// Token: 0x02000ACC RID: 2764
	public class BigDanFang : UIBase
	{
		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x0600468B RID: 18059 RVA: 0x0003250F File Offset: 0x0003070F
		public bool IsSelect
		{
			get
			{
				return this._isSelect;
			}
		}

		// Token: 0x0600468C RID: 18060 RVA: 0x001E26BC File Offset: 0x001E08BC
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

		// Token: 0x0600468D RID: 18061 RVA: 0x001E286C File Offset: 0x001E0A6C
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

		// Token: 0x0600468E RID: 18062 RVA: 0x00032517 File Offset: 0x00030717
		public float GetHeight()
		{
			return Math.Abs(this.ChildList[this.ChildList.Count - 1].GetTransform().localPosition.y) + 107f;
		}

		// Token: 0x0600468F RID: 18063 RVA: 0x001E2950 File Offset: 0x001E0B50
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

		// Token: 0x06004690 RID: 18064 RVA: 0x001E2A10 File Offset: 0x001E0C10
		public void Enter()
		{
			if (ToolTipsMag.Inst == null)
			{
				ResManager.inst.LoadPrefab("ToolTips").Inst(NewUICanvas.Inst.transform);
			}
			ToolTipsMag.Inst.Show(this.BaseItem, new Vector2(LianDanUIMag.Instance.Vector2.position.x, ToolTipsMag.Inst.GetMouseY()));
		}

		// Token: 0x06004691 RID: 18065 RVA: 0x0003254B File Offset: 0x0003074B
		public void Out()
		{
			ToolTipsMag.Inst.Close();
		}

		// Token: 0x06004692 RID: 18066 RVA: 0x00032557 File Offset: 0x00030757
		private void UnSelect()
		{
			this._isSelect = false;
			this._unSelect.SetActive(true);
			this._select.SetActive(false);
			LianDanUIMag.Instance.DanFangPanel.UpdatePosition();
		}

		// Token: 0x06004693 RID: 18067 RVA: 0x001E2A7C File Offset: 0x001E0C7C
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

		// Token: 0x06004694 RID: 18068 RVA: 0x001E2BF4 File Offset: 0x001E0DF4
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

		// Token: 0x06004695 RID: 18069 RVA: 0x001E2C94 File Offset: 0x001E0E94
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

		// Token: 0x06004696 RID: 18070 RVA: 0x00032587 File Offset: 0x00030787
		public void RemoveChild(SmallDanFang smallDanFang)
		{
			this.ChildList.Remove(smallDanFang);
			this.Data.DanFangBases.Remove(smallDanFang.DanFangData);
			smallDanFang.Destroy();
		}

		// Token: 0x06004697 RID: 18071 RVA: 0x000325B3 File Offset: 0x000307B3
		public void Destroy()
		{
			Object.Destroy(this._go);
		}

		// Token: 0x04003EA2 RID: 16034
		private bool _isSelect;

		// Token: 0x04003EA3 RID: 16035
		public DanFangData Data;

		// Token: 0x04003EA4 RID: 16036
		public BaseItem BaseItem;

		// Token: 0x04003EA5 RID: 16037
		private GameObject _unSelect;

		// Token: 0x04003EA6 RID: 16038
		private GameObject _canMade1;

		// Token: 0x04003EA7 RID: 16039
		private GameObject _select;

		// Token: 0x04003EA8 RID: 16040
		private GameObject _canMade2;

		// Token: 0x04003EA9 RID: 16041
		private Text _zhuYao1;

		// Token: 0x04003EAA RID: 16042
		private Text _fuYao1;

		// Token: 0x04003EAB RID: 16043
		public List<SmallDanFang> ChildList = new List<SmallDanFang>();
	}
}
