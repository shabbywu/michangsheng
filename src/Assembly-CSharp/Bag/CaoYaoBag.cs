using System;
using System.Collections.Generic;
using Bag.Filter;
using JSONClass;
using KBEngine;
using SuperScrollView;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Bag
{
	// Token: 0x02000D45 RID: 3397
	public class CaoYaoBag : BaseBag2, IESCClose
	{
		// Token: 0x060050B6 RID: 20662 RVA: 0x0003A18F File Offset: 0x0003838F
		public void Open()
		{
			if (!this._init)
			{
				this.Init(0, true);
			}
			ESCCloseManager.Inst.RegisterClose(this);
			base.gameObject.SetActive(true);
			base.UpdateItem(true);
		}

		// Token: 0x060050B7 RID: 20663 RVA: 0x0021A860 File Offset: 0x00218A60
		public override void Init(int npcId, bool isPlayer = false)
		{
			this._player = Tools.instance.getPlayer();
			this.NpcId = npcId;
			this.IsPlayer = isPlayer;
			this.ZhuYaoDict.Add(0, "全部");
			this.FuYaoDict.Add(0, "全部");
			this.YaoYinDict.Add(0, "全部");
			this.AllYaoXinDict.Add(0, "全部");
			this.ZhuYaoDict.Add(-1, "未知");
			this.FuYaoDict.Add(-1, "未知");
			this.YaoYinDict.Add(-1, "未知");
			this.AllYaoXinDict.Add(-1, "未知");
			this.MLoopListView.InitListView(base.GetCount(this.MItemTotalCount), new Func<LoopListView2, int, LoopListViewItem2>(base.OnGetItemByIndex), null);
			foreach (LianDanItemLeiXin lianDanItemLeiXin in LianDanItemLeiXin.DataList)
			{
				if (lianDanItemLeiXin.desc.Contains("主药"))
				{
					this.ZhuYaoDict.Add(lianDanItemLeiXin.id, lianDanItemLeiXin.name);
				}
				else if (lianDanItemLeiXin.desc.Contains("辅药"))
				{
					this.FuYaoDict.Add(lianDanItemLeiXin.id, lianDanItemLeiXin.name);
				}
				else
				{
					this.YaoYinDict.Add(lianDanItemLeiXin.id, lianDanItemLeiXin.name);
				}
				this.AllYaoXinDict.Add(lianDanItemLeiXin.id, lianDanItemLeiXin.name);
			}
			base.CreateTempList();
			this.CreateQualityFilter();
			this.CreateWeiZhiFilter();
			this.WeiZhiFilterList[0].Click();
			this.ItemType = ItemType.草药;
			this._init = true;
		}

		// Token: 0x060050B8 RID: 20664 RVA: 0x0021AA30 File Offset: 0x00218C30
		private void CreateWeiZhiFilter()
		{
			this.WeiZhi = 1;
			Dictionary<int, string> weiZhiData = this.GetWeiZhiData();
			float x = this.TempWeiZhiFilter.transform.localPosition.x;
			float num = this.TempWeiZhiFilter.transform.localPosition.y;
			foreach (int num2 in weiZhiData.Keys)
			{
				LianDanFilter component = this.TempWeiZhiFilter.Inst(this.TempWeiZhiFilter.transform.parent).GetComponent<LianDanFilter>();
				component.Init(num2, weiZhiData[num2], new UnityAction<LianDanFilter>(this.SelectWeiZhi), x, num);
				this.WeiZhiFilterList.Add(component);
				num -= 43f;
			}
		}

		// Token: 0x060050B9 RID: 20665 RVA: 0x0021AB10 File Offset: 0x00218D10
		private void CreateYaoXinFilter()
		{
			Tools.ClearObj(this.TempYaoXinFilter.transform);
			this.YaoXinFilterList = new List<LianDanFilter>();
			this.YaoXin = 0;
			Dictionary<int, string> yaoXinData = this.GetYaoXinData();
			float x = this.TempYaoXinFilter.transform.localPosition.x;
			float num = this.TempYaoXinFilter.transform.localPosition.y;
			foreach (int num2 in yaoXinData.Keys)
			{
				LianDanFilter component = this.TempYaoXinFilter.Inst(this.TempYaoXinFilter.transform.parent).GetComponent<LianDanFilter>();
				component.Init(num2, yaoXinData[num2], new UnityAction<LianDanFilter>(this.SelectYaoXin), x, num);
				this.YaoXinFilterList.Add(component);
				num -= 51f;
			}
			this.TempYaoXinFilter.transform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(156f, -num);
			this.YaoXinFilterList[0].Click();
		}

		// Token: 0x060050BA RID: 20666 RVA: 0x0021AC40 File Offset: 0x00218E40
		private void CreateQualityFilter()
		{
			this.ItemQuality = ItemQuality.全部;
			using (List<FpBtn>.Enumerator enumerator = this.QualityFilterList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					FpBtn btn = enumerator.Current;
					btn.mouseUpEvent.AddListener(delegate()
					{
						this.SelectQuality(int.Parse(btn.gameObject.name));
					});
				}
			}
		}

		// Token: 0x060050BB RID: 20667 RVA: 0x0003A1BF File Offset: 0x000383BF
		public void ShowQualityFilter()
		{
			this.QualityPanel.SetActive(true);
		}

		// Token: 0x060050BC RID: 20668 RVA: 0x0003A1CD File Offset: 0x000383CD
		public void CloseQualityFilter()
		{
			this.QualityPanel.SetActive(false);
		}

		// Token: 0x060050BD RID: 20669 RVA: 0x0021ACC4 File Offset: 0x00218EC4
		public void SelectWeiZhi(int value)
		{
			this.WeiZhi = value;
			foreach (LianDanFilter lianDanFilter in this.WeiZhiFilterList)
			{
				if (lianDanFilter.Value == this.WeiZhi)
				{
					lianDanFilter.SetState(true);
				}
				else
				{
					lianDanFilter.SetState(false);
				}
			}
			this.CreateYaoXinFilter();
		}

		// Token: 0x060050BE RID: 20670 RVA: 0x0021AD3C File Offset: 0x00218F3C
		private void SelectWeiZhi(LianDanFilter filter)
		{
			foreach (LianDanFilter lianDanFilter in this.WeiZhiFilterList)
			{
				lianDanFilter.SetState(false);
			}
			filter.SetState(true);
			this.WeiZhi = filter.Value;
			this.CreateYaoXinFilter();
		}

		// Token: 0x060050BF RID: 20671 RVA: 0x0021ADA8 File Offset: 0x00218FA8
		private void SelectYaoXin(LianDanFilter filter)
		{
			foreach (LianDanFilter lianDanFilter in this.YaoXinFilterList)
			{
				lianDanFilter.SetState(false);
			}
			filter.SetState(true);
			this.YaoXin = filter.Value;
			base.UpdateItem(true);
		}

		// Token: 0x060050C0 RID: 20672 RVA: 0x0003A1DB File Offset: 0x000383DB
		private void SelectQuality(int value)
		{
			this.ItemQuality = (ItemQuality)value;
			this.CurQuality.SetText(this.ItemQuality.ToString());
			base.UpdateItem(true);
			this.CloseQualityFilter();
		}

		// Token: 0x060050C1 RID: 20673 RVA: 0x0003A20D File Offset: 0x0003840D
		private Dictionary<int, string> GetWeiZhiData()
		{
			return new Dictionary<int, string>
			{
				{
					1,
					"主药"
				},
				{
					2,
					"辅药"
				},
				{
					3,
					"药引"
				}
			};
		}

		// Token: 0x060050C2 RID: 20674 RVA: 0x0003A238 File Offset: 0x00038438
		private Dictionary<int, string> GetYaoXinData()
		{
			if (this.WeiZhi == 1)
			{
				return this.ZhuYaoDict;
			}
			if (this.WeiZhi == 2)
			{
				return this.FuYaoDict;
			}
			return this.YaoYinDict;
		}

		// Token: 0x060050C3 RID: 20675 RVA: 0x0021AE14 File Offset: 0x00219014
		protected override bool FiddlerItem(BaseItem baseItem)
		{
			if (this.ItemQuality != ItemQuality.全部 && baseItem.GetImgQuality() != (int)this.ItemQuality)
			{
				return false;
			}
			if (baseItem.ItemType != ItemType.草药)
			{
				return false;
			}
			CaoYaoItem caoYaoItem = (CaoYaoItem)baseItem;
			if (this.YaoXin != 0)
			{
				if (this.WeiZhi == 1 && this.YaoXin != caoYaoItem.GetZhuYaoId())
				{
					return false;
				}
				if (this.WeiZhi == 2 && this.YaoXin != caoYaoItem.GetFuYaoId())
				{
					return false;
				}
				if (this.WeiZhi == 3 && this.YaoXin != caoYaoItem.GetYaoYinId())
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060050C4 RID: 20676 RVA: 0x0001BCA3 File Offset: 0x00019EA3
		public void Close()
		{
			ESCCloseManager.Inst.UnRegisterClose(this);
			base.gameObject.SetActive(false);
		}

		// Token: 0x060050C5 RID: 20677 RVA: 0x0003A260 File Offset: 0x00038460
		public bool TryEscClose()
		{
			this.Close();
			return true;
		}

		// Token: 0x060050C6 RID: 20678 RVA: 0x0021AEA0 File Offset: 0x002190A0
		public BaseItem GetTempItemById(int id)
		{
			if (this.TempBagList.Count < 1)
			{
				this.Init(0, true);
			}
			foreach (ITEM_INFO item_INFO in this.TempBagList)
			{
				if (item_INFO.itemId == id)
				{
					return BaseItem.Create(id, (int)item_INFO.itemCount, item_INFO.uuid, item_INFO.Seid);
				}
			}
			Debug.LogError("错误,背包不存在此物品");
			return null;
		}

		// Token: 0x040051EA RID: 20970
		private bool _init;

		// Token: 0x040051EB RID: 20971
		[SerializeField]
		private GameObject TempWeiZhiFilter;

		// Token: 0x040051EC RID: 20972
		[SerializeField]
		private GameObject TempYaoXinFilter;

		// Token: 0x040051ED RID: 20973
		[SerializeField]
		private Text CurQuality;

		// Token: 0x040051EE RID: 20974
		[SerializeField]
		private GameObject QualityPanel;

		// Token: 0x040051EF RID: 20975
		public List<LianDanFilter> FilterList = new List<LianDanFilter>();

		// Token: 0x040051F0 RID: 20976
		public List<LianDanFilter> WeiZhiFilterList = new List<LianDanFilter>();

		// Token: 0x040051F1 RID: 20977
		public List<LianDanFilter> YaoXinFilterList = new List<LianDanFilter>();

		// Token: 0x040051F2 RID: 20978
		public List<FpBtn> QualityFilterList;

		// Token: 0x040051F3 RID: 20979
		public Dictionary<int, string> AllYaoXinDict = new Dictionary<int, string>();

		// Token: 0x040051F4 RID: 20980
		public Dictionary<int, string> ZhuYaoDict = new Dictionary<int, string>();

		// Token: 0x040051F5 RID: 20981
		public Dictionary<int, string> FuYaoDict = new Dictionary<int, string>();

		// Token: 0x040051F6 RID: 20982
		public Dictionary<int, string> YaoYinDict = new Dictionary<int, string>();

		// Token: 0x040051F7 RID: 20983
		public Dictionary<int, string> QualityDict = new Dictionary<int, string>
		{
			{
				0,
				"全部"
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

		// Token: 0x040051F8 RID: 20984
		public int WeiZhi = 1;

		// Token: 0x040051F9 RID: 20985
		public int YaoXin;

		// Token: 0x040051FA RID: 20986
		public LianDanSlot ToSlot;
	}
}
