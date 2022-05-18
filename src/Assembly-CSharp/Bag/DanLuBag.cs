using System;
using System.Collections.Generic;
using Bag.Filter;
using SuperScrollView;
using UnityEngine;
using UnityEngine.Events;

namespace Bag
{
	// Token: 0x02000D47 RID: 3399
	public class DanLuBag : BaseBag2, IESCClose
	{
		// Token: 0x060050CA RID: 20682 RVA: 0x0003A28B File Offset: 0x0003848B
		public void Open()
		{
			if (!this._init)
			{
				this.Init(0, true);
			}
			ESCCloseManager.Inst.RegisterClose(this);
			base.gameObject.SetActive(true);
		}

		// Token: 0x060050CB RID: 20683 RVA: 0x0021AFFC File Offset: 0x002191FC
		public override void Init(int npcId, bool isPlayer = false)
		{
			this._player = Tools.instance.getPlayer();
			this.NpcId = npcId;
			this.IsPlayer = isPlayer;
			base.CreateTempList();
			this.MLoopListView.InitListView(base.GetCount(this.MItemTotalCount), new Func<LoopListView2, int, LoopListViewItem2>(base.OnGetItemByIndex), null);
			this.ItemType = ItemType.法宝;
			this.EquipType = EquipType.丹炉;
			this.CreateQualityFilter();
			this.SelectQualityCall(this.FilterList[0]);
			this._init = true;
		}

		// Token: 0x060050CC RID: 20684 RVA: 0x0021B080 File Offset: 0x00219280
		private void CreateQualityFilter()
		{
			Dictionary<int, string> qualityData = this.GetQualityData();
			float x = this.TempFilter.transform.localPosition.x;
			float num = this.TempFilter.transform.localPosition.y;
			foreach (int num2 in qualityData.Keys)
			{
				LianDanFilter component = this.TempFilter.Inst(this.TempFilter.transform.parent).GetComponent<LianDanFilter>();
				component.Init(num2, qualityData[num2], new UnityAction<LianDanFilter>(this.SelectQualityCall), x, num);
				this.FilterList.Add(component);
				num -= 43f;
			}
		}

		// Token: 0x060050CD RID: 20685 RVA: 0x0021B158 File Offset: 0x00219358
		public void SelectQualityCall(LianDanFilter filter)
		{
			foreach (LianDanFilter lianDanFilter in this.FilterList)
			{
				lianDanFilter.SetState(false);
			}
			filter.SetState(true);
			this.ItemQuality = (ItemQuality)filter.Value;
			base.UpdateItem(false);
		}

		// Token: 0x060050CE RID: 20686 RVA: 0x0021B1C4 File Offset: 0x002193C4
		private Dictionary<int, string> GetQualityData()
		{
			return new Dictionary<int, string>
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
		}

		// Token: 0x060050CF RID: 20687 RVA: 0x0001BCA3 File Offset: 0x00019EA3
		public void Close()
		{
			ESCCloseManager.Inst.UnRegisterClose(this);
			base.gameObject.SetActive(false);
		}

		// Token: 0x060050D0 RID: 20688 RVA: 0x0003A2B4 File Offset: 0x000384B4
		public bool TryEscClose()
		{
			this.Close();
			return true;
		}

		// Token: 0x040051FD RID: 20989
		private bool _init;

		// Token: 0x040051FE RID: 20990
		[SerializeField]
		private GameObject TempFilter;

		// Token: 0x040051FF RID: 20991
		public List<LianDanFilter> FilterList = new List<LianDanFilter>();
	}
}
