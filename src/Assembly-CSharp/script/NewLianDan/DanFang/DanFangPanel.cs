using System;
using System.Collections.Generic;
using Bag;
using JSONClass;
using script.NewLianDan.DanFang.Filter;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace script.NewLianDan.DanFang
{
	// Token: 0x02000ACF RID: 2767
	public class DanFangPanel : UIBase
	{
		// Token: 0x0600469A RID: 18074 RVA: 0x001E2D00 File Offset: 0x001E0F00
		public DanFangPanel(GameObject go)
		{
			this._go = go;
			this.FilterDataDict = this.GetFilterData();
			this.DanFangFilter = new DanFangFilter(base.Get("品阶选择界面", true));
			this._curQuality = base.Get<Text>("品阶筛选按钮/Value");
			this.DanFangTemp = base.Get("丹方列表/Mask/Content/Temp", true);
			base.Get<FpBtn>("品阶筛选按钮").mouseUpEvent.AddListener(new UnityAction(this.DanFangFilter.Show));
			base.Get<FpBtn>("置顶丹方").mouseUpEvent.AddListener(new UnityAction(this.SetTop));
			base.Get<FpBtn>("删除丹方").mouseUpEvent.AddListener(new UnityAction(this.Delete));
		}

		// Token: 0x0600469B RID: 18075 RVA: 0x000325FF File Offset: 0x000307FF
		public void UpdateFilter(int quality)
		{
			this.DanFangFilter.Hide();
			this.CurQuality = quality;
			this._curQuality.SetText(this.FilterDataDict[quality]);
			this.UpdateDanFangList();
		}

		// Token: 0x0600469C RID: 18076 RVA: 0x001E2DCC File Offset: 0x001E0FCC
		public void UpdateDanFangList()
		{
			this.Clear();
			this.CurBigDanFang = null;
			this.CurSmallDanFang = null;
			Dictionary<int, DanFangData> dictionary = new Dictionary<int, DanFangData>();
			this.DanFangList = new List<BigDanFang>();
			foreach (JSONObject jsonobject in Tools.instance.getPlayer().DanFang.list)
			{
				int i = jsonobject["ID"].I;
				if (this.CurQuality == 0 || _ItemJsonData.DataDict[i].quality == this.CurQuality)
				{
					DanFangBase danFangBase = new DanFangBase();
					if (jsonobject["Type"][0].I > 0)
					{
						danFangBase.YaoYin.Add(jsonobject["Type"][0].I, jsonobject["Num"][0].I);
					}
					if (jsonobject["Type"][1].I > 0)
					{
						danFangBase.ZhuYao1.Add(jsonobject["Type"][1].I, jsonobject["Num"][1].I);
						danFangBase.ZhuYaoYaoXin1 = _ItemJsonData.DataDict[jsonobject["Type"][1].I].yaoZhi2;
					}
					if (jsonobject["Type"][2].I > 0)
					{
						danFangBase.ZhuYao2.Add(jsonobject["Type"][2].I, jsonobject["Num"][2].I);
						danFangBase.ZhuYaoYaoXin2 = _ItemJsonData.DataDict[jsonobject["Type"][2].I].yaoZhi2;
					}
					if (jsonobject["Type"][3].I > 0)
					{
						danFangBase.FuYao1.Add(jsonobject["Type"][3].I, jsonobject["Num"][3].I);
						danFangBase.FuYaoYaoXin1 = _ItemJsonData.DataDict[jsonobject["Type"][3].I].yaoZhi3;
					}
					if (jsonobject["Type"][4].I > 0)
					{
						danFangBase.FuYao2.Add(jsonobject["Type"][4].I, jsonobject["Num"][4].I);
						danFangBase.FuYaoYaoXin2 = _ItemJsonData.DataDict[jsonobject["Type"][4].I].yaoZhi3;
					}
					danFangBase.Json = jsonobject.Copy();
					if (!dictionary.ContainsKey(i))
					{
						dictionary.Add(i, new DanFangData
						{
							Id = i,
							Name = _ItemJsonData.DataDict[i].name,
							DanFangBases = new List<DanFangBase>()
						});
					}
					dictionary[i].DanFangBases.Add(danFangBase);
				}
			}
			foreach (int num in dictionary.Keys)
			{
				this.DanFangList.Add(new BigDanFang(this.DanFangTemp.Inst(this.DanFangTemp.transform.parent), dictionary[num], BaseItem.Create(num, 1, Tools.getUUID(), Tools.CreateItemSeid(num))));
			}
			this.UpdatePosition();
		}

		// Token: 0x0600469D RID: 18077 RVA: 0x001E31D8 File Offset: 0x001E13D8
		public void UpdatePosition()
		{
			if (this.DanFangList == null || this.DanFangList.Count < 1)
			{
				return;
			}
			float x = this.DanFangTemp.transform.localPosition.x;
			float num = this.DanFangTemp.transform.localPosition.y;
			foreach (BigDanFang bigDanFang in this.DanFangList)
			{
				bigDanFang.GetTransform().localPosition = new Vector2(x, num);
				if (bigDanFang.IsSelect)
				{
					num -= bigDanFang.GetHeight();
				}
				else
				{
					num -= 61f;
				}
			}
			this.DanFangTemp.transform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(464f, -num);
		}

		// Token: 0x0600469E RID: 18078 RVA: 0x001E32C0 File Offset: 0x001E14C0
		public void Clear()
		{
			if (this.DanFangList == null)
			{
				return;
			}
			foreach (BigDanFang bigDanFang in this.DanFangList)
			{
				bigDanFang.Destroy();
			}
		}

		// Token: 0x0600469F RID: 18079 RVA: 0x001E331C File Offset: 0x001E151C
		private Dictionary<int, string> GetFilterData()
		{
			return new Dictionary<int, string>
			{
				{
					0,
					"所有品阶"
				},
				{
					1,
					"一品"
				},
				{
					2,
					"二品"
				},
				{
					3,
					"三品"
				},
				{
					4,
					"四品"
				},
				{
					5,
					"五品"
				},
				{
					6,
					"六品"
				}
			};
		}

		// Token: 0x060046A0 RID: 18080 RVA: 0x00032630 File Offset: 0x00030830
		public void SetBigDanFangCallBack(BigDanFang bigDanFang)
		{
			this.CurBigDanFang = bigDanFang;
			if (this.CurSmallDanFang != null && !this.CurSmallDanFang.IsNull())
			{
				this.CurSmallDanFang.CancelSelect();
			}
			this.UpdatePosition();
		}

		// Token: 0x060046A1 RID: 18081 RVA: 0x0003265F File Offset: 0x0003085F
		public void SetSmallDanFangCallBack(SmallDanFang smallDanFang)
		{
			if (this.CurSmallDanFang != null && smallDanFang != this.CurSmallDanFang)
			{
				if (this.CurSmallDanFang.IsDestroy)
				{
					this.CurSmallDanFang = null;
				}
				else
				{
					this.CurSmallDanFang.CancelSelect();
				}
			}
			this.CurSmallDanFang = smallDanFang;
		}

		// Token: 0x060046A2 RID: 18082 RVA: 0x001E3384 File Offset: 0x001E1584
		public void PutCaoYaoByDanFang()
		{
			LianDanUIMag.Instance.LianDanPanel.BackAllCaoYao();
			DanFangBase danFangData = this.CurSmallDanFang.DanFangData;
			foreach (int num in danFangData.YaoYin.Keys)
			{
				LianDanUIMag.Instance.LianDanPanel.PutCaoYao(0, num, danFangData.YaoYin[num]);
			}
			foreach (int num2 in danFangData.ZhuYao1.Keys)
			{
				LianDanUIMag.Instance.LianDanPanel.PutCaoYao(1, num2, danFangData.ZhuYao1[num2]);
			}
			foreach (int num3 in danFangData.ZhuYao2.Keys)
			{
				LianDanUIMag.Instance.LianDanPanel.PutCaoYao(2, num3, danFangData.ZhuYao2[num3]);
			}
			foreach (int num4 in danFangData.FuYao1.Keys)
			{
				LianDanUIMag.Instance.LianDanPanel.PutCaoYao(3, num4, danFangData.FuYao1[num4]);
			}
			foreach (int num5 in danFangData.FuYao2.Keys)
			{
				LianDanUIMag.Instance.LianDanPanel.PutCaoYao(4, num5, danFangData.FuYao2[num5]);
			}
			LianDanUIMag.Instance.LianDanPanel.CheckCanMade();
		}

		// Token: 0x060046A3 RID: 18083 RVA: 0x001E359C File Offset: 0x001E179C
		public void SetTop()
		{
			if (this.CurBigDanFang == null || this.CurBigDanFang.IsNull() || this.DanFangList == null || this.DanFangList.Count < 2)
			{
				UIPopTip.Inst.Pop("请选择要置顶的丹方", PopTipIconType.叹号);
				return;
			}
			BigDanFang value = this.DanFangList[0];
			int index = this.DanFangList.IndexOf(this.CurBigDanFang);
			this.DanFangList[index] = value;
			this.DanFangList[0] = this.CurBigDanFang;
			this.UpdatePosition();
		}

		// Token: 0x060046A4 RID: 18084 RVA: 0x001E362C File Offset: 0x001E182C
		public void Delete()
		{
			if (this.CurSmallDanFang == null || this.CurSmallDanFang.IsNull() || this.CurSmallDanFang.Parent == null || this.CurSmallDanFang.Parent.IsNull())
			{
				UIPopTip.Inst.Pop("请选择要删除的丹方", PopTipIconType.叹号);
				return;
			}
			USelectBox.Show("确定要删除" + this.CurSmallDanFang.Parent.Data.Name + "吗", delegate
			{
				int num = -1;
				this.IsSame(this.CurSmallDanFang.DanFangData.Json, ref num);
				if (num > -1)
				{
					BigDanFang parent = this.CurSmallDanFang.Parent;
					Tools.instance.getPlayer().DanFang.list.RemoveAt(num);
					this.CurSmallDanFang.Parent.RemoveChild(this.CurSmallDanFang);
					this.CurSmallDanFang = null;
					if (parent.ChildList == null || parent.ChildList.Count < 1)
					{
						this.DanFangList.Remove(parent);
						parent.Destroy();
						this.CurBigDanFang = null;
					}
					else
					{
						parent.CreateChild();
					}
					this.UpdatePosition();
					UIPopTip.Inst.Pop("删除成功", PopTipIconType.叹号);
					return;
				}
				UIPopTip.Inst.Pop("数据异常,删除失败", PopTipIconType.叹号);
			}, null);
		}

		// Token: 0x060046A5 RID: 18085 RVA: 0x000FCE8C File Offset: 0x000FB08C
		public void IsSame(JSONObject obj, ref int index)
		{
			List<JSONObject> list = Tools.instance.getPlayer().DanFang.list;
			JSONObject jsonobject = obj["Type"];
			JSONObject jsonobject2 = obj["Num"];
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i]["ID"].I == obj["ID"].I)
				{
					int num = 0;
					for (int j = 0; j < jsonobject.Count; j++)
					{
						if (list[i]["Type"][j].I == jsonobject[j].I && list[i]["Num"][j].I == jsonobject2[j].I)
						{
							num++;
						}
					}
					if (num == 5)
					{
						index = i;
						return;
					}
				}
			}
			index = -1;
		}

		// Token: 0x060046A6 RID: 18086 RVA: 0x001E36B4 File Offset: 0x001E18B4
		public void AddDanFang(JSONObject json)
		{
			int num = -1;
			this.IsSame(json, ref num);
			if (num == -1)
			{
				Dictionary<int, DanFangData> dictionary = new Dictionary<int, DanFangData>();
				Tools.instance.getPlayer().DanFang.list.Add(json);
				bool flag = false;
				int i = json["ID"].I;
				if (this.CurQuality != 0 && _ItemJsonData.DataDict[i].quality != this.CurQuality)
				{
					return;
				}
				DanFangBase danFangBase = new DanFangBase();
				if (json["Type"][0].I > 0)
				{
					danFangBase.YaoYin.Add(json["Type"][0].I, json["Num"][0].I);
				}
				if (json["Type"][1].I > 0)
				{
					danFangBase.ZhuYao1.Add(json["Type"][1].I, json["Num"][1].I);
					danFangBase.ZhuYaoYaoXin1 = _ItemJsonData.DataDict[json["Type"][1].I].yaoZhi2;
				}
				if (json["Type"][2].I > 0)
				{
					danFangBase.ZhuYao2.Add(json["Type"][2].I, json["Num"][2].I);
					danFangBase.ZhuYaoYaoXin2 = _ItemJsonData.DataDict[json["Type"][2].I].yaoZhi2;
				}
				if (json["Type"][3].I > 0)
				{
					danFangBase.FuYao1.Add(json["Type"][3].I, json["Num"][3].I);
					danFangBase.FuYaoYaoXin1 = _ItemJsonData.DataDict[json["Type"][3].I].yaoZhi3;
				}
				if (json["Type"][4].I > 0)
				{
					danFangBase.FuYao2.Add(json["Type"][4].I, json["Num"][4].I);
					danFangBase.FuYaoYaoXin2 = _ItemJsonData.DataDict[json["Type"][4].I].yaoZhi3;
				}
				danFangBase.Json = json.Copy();
				if (this.CurSmallDanFang != null && !this.CurSmallDanFang.IsDestroy)
				{
					this.CurSmallDanFang.CancelSelect();
				}
				foreach (BigDanFang bigDanFang in this.DanFangList)
				{
					if (i == bigDanFang.Data.Id)
					{
						flag = true;
						bigDanFang.Data.DanFangBases.Add(danFangBase);
						this.CurSmallDanFang = null;
						bigDanFang.CreateChild();
						bigDanFang.UpdateState();
						break;
					}
				}
				if (!flag)
				{
					dictionary.Add(i, new DanFangData
					{
						Id = i,
						Name = _ItemJsonData.DataDict[i].name,
						DanFangBases = new List<DanFangBase>()
					});
					dictionary[i].DanFangBases.Add(danFangBase);
					this.DanFangList.Add(new BigDanFang(this.DanFangTemp.Inst(this.DanFangTemp.transform.parent), dictionary[i], BaseItem.Create(i, 1, Tools.getUUID(), Tools.CreateItemSeid(i))));
				}
				this.UpdatePosition();
			}
		}

		// Token: 0x04003EB9 RID: 16057
		public DanFangFilter DanFangFilter;

		// Token: 0x04003EBA RID: 16058
		public Dictionary<int, string> FilterDataDict;

		// Token: 0x04003EBB RID: 16059
		public List<BigDanFang> DanFangList;

		// Token: 0x04003EBC RID: 16060
		public GameObject DanFangTemp;

		// Token: 0x04003EBD RID: 16061
		public int CurQuality;

		// Token: 0x04003EBE RID: 16062
		public SmallDanFang CurSmallDanFang;

		// Token: 0x04003EBF RID: 16063
		public BigDanFang CurBigDanFang;

		// Token: 0x04003EC0 RID: 16064
		private Text _curQuality;
	}
}
