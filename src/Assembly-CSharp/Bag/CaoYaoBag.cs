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
	// Token: 0x020009BD RID: 2493
	public class CaoYaoBag : BaseBag2, IESCClose
	{
		// Token: 0x06004553 RID: 17747 RVA: 0x001D6798 File Offset: 0x001D4998
		public void Open()
		{
			if (!this._init)
			{
				this.Init(0, true);
			}
			ESCCloseManager.Inst.RegisterClose(this);
			base.gameObject.SetActive(true);
			this.UpdateItem(true);
		}

		// Token: 0x06004554 RID: 17748 RVA: 0x001D67C8 File Offset: 0x001D49C8
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
			this.CreateTempList();
			this.CreateQualityFilter();
			this.CreateWeiZhiFilter();
			this.WeiZhiFilterList[0].Click();
			this.ItemType = ItemType.草药;
			this._init = true;
		}

		// Token: 0x06004555 RID: 17749 RVA: 0x001D6998 File Offset: 0x001D4B98
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

		// Token: 0x06004556 RID: 17750 RVA: 0x001D6A78 File Offset: 0x001D4C78
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

		// Token: 0x06004557 RID: 17751 RVA: 0x001D6BA8 File Offset: 0x001D4DA8
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

		// Token: 0x06004558 RID: 17752 RVA: 0x001D6C2C File Offset: 0x001D4E2C
		public void ShowQualityFilter()
		{
			this.QualityPanel.SetActive(true);
		}

		// Token: 0x06004559 RID: 17753 RVA: 0x001D6C3A File Offset: 0x001D4E3A
		public void CloseQualityFilter()
		{
			this.QualityPanel.SetActive(false);
		}

		// Token: 0x0600455A RID: 17754 RVA: 0x001D6C48 File Offset: 0x001D4E48
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

		// Token: 0x0600455B RID: 17755 RVA: 0x001D6CC0 File Offset: 0x001D4EC0
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

		// Token: 0x0600455C RID: 17756 RVA: 0x001D6D2C File Offset: 0x001D4F2C
		private void SelectYaoXin(LianDanFilter filter)
		{
			foreach (LianDanFilter lianDanFilter in this.YaoXinFilterList)
			{
				lianDanFilter.SetState(false);
			}
			filter.SetState(true);
			this.YaoXin = filter.Value;
			this.UpdateItem(true);
		}

		// Token: 0x0600455D RID: 17757 RVA: 0x001D6D98 File Offset: 0x001D4F98
		private void SelectQuality(int value)
		{
			this.ItemQuality = (ItemQuality)value;
			this.CurQuality.SetText(this.ItemQuality.ToString());
			this.UpdateItem(true);
			this.CloseQualityFilter();
		}

		// Token: 0x0600455E RID: 17758 RVA: 0x001D6DCA File Offset: 0x001D4FCA
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

		// Token: 0x0600455F RID: 17759 RVA: 0x001D6DF5 File Offset: 0x001D4FF5
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

		// Token: 0x06004560 RID: 17760 RVA: 0x001D6E20 File Offset: 0x001D5020
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

		// Token: 0x06004561 RID: 17761 RVA: 0x000D5AD3 File Offset: 0x000D3CD3
		public void Close()
		{
			ESCCloseManager.Inst.UnRegisterClose(this);
			base.gameObject.SetActive(false);
		}

		// Token: 0x06004562 RID: 17762 RVA: 0x001D6EAB File Offset: 0x001D50AB
		public bool TryEscClose()
		{
			this.Close();
			return true;
		}

		// Token: 0x06004563 RID: 17763 RVA: 0x001D6EB4 File Offset: 0x001D50B4
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

		// Token: 0x040046E8 RID: 18152
		private bool _init;

		// Token: 0x040046E9 RID: 18153
		[SerializeField]
		private GameObject TempWeiZhiFilter;

		// Token: 0x040046EA RID: 18154
		[SerializeField]
		private GameObject TempYaoXinFilter;

		// Token: 0x040046EB RID: 18155
		[SerializeField]
		private Text CurQuality;

		// Token: 0x040046EC RID: 18156
		[SerializeField]
		private GameObject QualityPanel;

		// Token: 0x040046ED RID: 18157
		public List<LianDanFilter> FilterList = new List<LianDanFilter>();

		// Token: 0x040046EE RID: 18158
		public List<LianDanFilter> WeiZhiFilterList = new List<LianDanFilter>();

		// Token: 0x040046EF RID: 18159
		public List<LianDanFilter> YaoXinFilterList = new List<LianDanFilter>();

		// Token: 0x040046F0 RID: 18160
		public List<FpBtn> QualityFilterList;

		// Token: 0x040046F1 RID: 18161
		public Dictionary<int, string> AllYaoXinDict = new Dictionary<int, string>();

		// Token: 0x040046F2 RID: 18162
		public Dictionary<int, string> ZhuYaoDict = new Dictionary<int, string>();

		// Token: 0x040046F3 RID: 18163
		public Dictionary<int, string> FuYaoDict = new Dictionary<int, string>();

		// Token: 0x040046F4 RID: 18164
		public Dictionary<int, string> YaoYinDict = new Dictionary<int, string>();

		// Token: 0x040046F5 RID: 18165
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

		// Token: 0x040046F6 RID: 18166
		public int WeiZhi = 1;

		// Token: 0x040046F7 RID: 18167
		public int YaoXin;

		// Token: 0x040046F8 RID: 18168
		public LianDanSlot ToSlot;
	}
}
