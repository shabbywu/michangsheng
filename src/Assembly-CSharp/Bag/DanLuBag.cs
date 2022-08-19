using System;
using System.Collections.Generic;
using Bag.Filter;
using SuperScrollView;
using UnityEngine;
using UnityEngine.Events;

namespace Bag
{
	// Token: 0x020009BE RID: 2494
	public class DanLuBag : BaseBag2, IESCClose
	{
		// Token: 0x06004565 RID: 17765 RVA: 0x001D700E File Offset: 0x001D520E
		public void Open()
		{
			if (!this._init)
			{
				this.Init(0, true);
			}
			ESCCloseManager.Inst.RegisterClose(this);
			base.gameObject.SetActive(true);
		}

		// Token: 0x06004566 RID: 17766 RVA: 0x001D7038 File Offset: 0x001D5238
		public override void Init(int npcId, bool isPlayer = false)
		{
			this._player = Tools.instance.getPlayer();
			this.NpcId = npcId;
			this.IsPlayer = isPlayer;
			this.CreateTempList();
			this.MLoopListView.InitListView(base.GetCount(this.MItemTotalCount), new Func<LoopListView2, int, LoopListViewItem2>(base.OnGetItemByIndex), null);
			this.ItemType = ItemType.法宝;
			this.EquipType = EquipType.丹炉;
			this.CreateQualityFilter();
			this.SelectQualityCall(this.FilterList[0]);
			this._init = true;
		}

		// Token: 0x06004567 RID: 17767 RVA: 0x001D70BC File Offset: 0x001D52BC
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

		// Token: 0x06004568 RID: 17768 RVA: 0x001D7194 File Offset: 0x001D5394
		public void SelectQualityCall(LianDanFilter filter)
		{
			foreach (LianDanFilter lianDanFilter in this.FilterList)
			{
				lianDanFilter.SetState(false);
			}
			filter.SetState(true);
			this.ItemQuality = (ItemQuality)filter.Value;
			this.UpdateItem(false);
		}

		// Token: 0x06004569 RID: 17769 RVA: 0x001D7200 File Offset: 0x001D5400
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

		// Token: 0x0600456A RID: 17770 RVA: 0x000D5AD3 File Offset: 0x000D3CD3
		public void Close()
		{
			ESCCloseManager.Inst.UnRegisterClose(this);
			base.gameObject.SetActive(false);
		}

		// Token: 0x0600456B RID: 17771 RVA: 0x001D7266 File Offset: 0x001D5466
		public bool TryEscClose()
		{
			this.Close();
			return true;
		}

		// Token: 0x040046F9 RID: 18169
		private bool _init;

		// Token: 0x040046FA RID: 18170
		[SerializeField]
		private GameObject TempFilter;

		// Token: 0x040046FB RID: 18171
		public List<LianDanFilter> FilterList = new List<LianDanFilter>();
	}
}
