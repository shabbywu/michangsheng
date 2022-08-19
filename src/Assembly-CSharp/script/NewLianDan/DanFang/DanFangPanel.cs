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
	// Token: 0x02000A00 RID: 2560
	public class DanFangPanel : UIBase
	{
		// Token: 0x060046EA RID: 18154 RVA: 0x001E06E4 File Offset: 0x001DE8E4
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

		// Token: 0x060046EB RID: 18155 RVA: 0x001E07AD File Offset: 0x001DE9AD
		public void UpdateFilter(int quality)
		{
			this.DanFangFilter.Hide();
			this.CurQuality = quality;
			this._curQuality.SetText(this.FilterDataDict[quality]);
			this.UpdateDanFangList();
		}

		// Token: 0x060046EC RID: 18156 RVA: 0x001E07E0 File Offset: 0x001DE9E0
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

		// Token: 0x060046ED RID: 18157 RVA: 0x001E0BEC File Offset: 0x001DEDEC
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

		// Token: 0x060046EE RID: 18158 RVA: 0x001E0CD4 File Offset: 0x001DEED4
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

		// Token: 0x060046EF RID: 18159 RVA: 0x001E0D30 File Offset: 0x001DEF30
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

		// Token: 0x060046F0 RID: 18160 RVA: 0x001E0D96 File Offset: 0x001DEF96
		public void SetBigDanFangCallBack(BigDanFang bigDanFang)
		{
			this.CurBigDanFang = bigDanFang;
			if (this.CurSmallDanFang != null && !this.CurSmallDanFang.IsNull())
			{
				this.CurSmallDanFang.CancelSelect();
			}
			this.UpdatePosition();
		}

		// Token: 0x060046F1 RID: 18161 RVA: 0x001E0DC5 File Offset: 0x001DEFC5
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

		// Token: 0x060046F2 RID: 18162 RVA: 0x001E0E00 File Offset: 0x001DF000
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

		// Token: 0x060046F3 RID: 18163 RVA: 0x001E1018 File Offset: 0x001DF218
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

		// Token: 0x060046F4 RID: 18164 RVA: 0x001E10A8 File Offset: 0x001DF2A8
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

		// Token: 0x060046F5 RID: 18165 RVA: 0x001E1130 File Offset: 0x001DF330
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

		// Token: 0x060046F6 RID: 18166 RVA: 0x001E122C File Offset: 0x001DF42C
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

		// Token: 0x0400483A RID: 18490
		public DanFangFilter DanFangFilter;

		// Token: 0x0400483B RID: 18491
		public Dictionary<int, string> FilterDataDict;

		// Token: 0x0400483C RID: 18492
		public List<BigDanFang> DanFangList;

		// Token: 0x0400483D RID: 18493
		public GameObject DanFangTemp;

		// Token: 0x0400483E RID: 18494
		public int CurQuality;

		// Token: 0x0400483F RID: 18495
		public SmallDanFang CurSmallDanFang;

		// Token: 0x04004840 RID: 18496
		public BigDanFang CurBigDanFang;

		// Token: 0x04004841 RID: 18497
		private Text _curQuality;
	}
}
