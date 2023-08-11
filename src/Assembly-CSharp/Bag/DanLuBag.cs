using System.Collections.Generic;
using Bag.Filter;
using UnityEngine;

namespace Bag;

public class DanLuBag : BaseBag2, IESCClose
{
	private bool _init;

	[SerializeField]
	private GameObject TempFilter;

	public List<LianDanFilter> FilterList = new List<LianDanFilter>();

	public void Open()
	{
		if (!_init)
		{
			Init(0, isPlayer: true);
		}
		ESCCloseManager.Inst.RegisterClose(this);
		((Component)this).gameObject.SetActive(true);
	}

	public override void Init(int npcId, bool isPlayer = false)
	{
		_player = Tools.instance.getPlayer();
		NpcId = npcId;
		IsPlayer = isPlayer;
		CreateTempList();
		MLoopListView.InitListView(GetCount(MItemTotalCount), base.OnGetItemByIndex);
		ItemType = ItemType.法宝;
		EquipType = EquipType.丹炉;
		CreateQualityFilter();
		SelectQualityCall(FilterList[0]);
		_init = true;
	}

	private void CreateQualityFilter()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		Dictionary<int, string> qualityData = GetQualityData();
		float x = TempFilter.transform.localPosition.x;
		float num = TempFilter.transform.localPosition.y;
		foreach (int key in qualityData.Keys)
		{
			LianDanFilter component = TempFilter.Inst(TempFilter.transform.parent).GetComponent<LianDanFilter>();
			component.Init(key, qualityData[key], SelectQualityCall, x, num);
			FilterList.Add(component);
			num -= 43f;
		}
	}

	public void SelectQualityCall(LianDanFilter filter)
	{
		foreach (LianDanFilter filter2 in FilterList)
		{
			filter2.SetState(selected: false);
		}
		filter.SetState(selected: true);
		ItemQuality = (ItemQuality)filter.Value;
		UpdateItem();
	}

	private Dictionary<int, string> GetQualityData()
	{
		return new Dictionary<int, string>
		{
			{ 0, "全部" },
			{ 1, "一品" },
			{ 2, "二品" },
			{ 3, "三品" },
			{ 4, "四品" },
			{ 5, "五品" },
			{ 6, "六品" }
		};
	}

	public void Close()
	{
		ESCCloseManager.Inst.UnRegisterClose(this);
		((Component)this).gameObject.SetActive(false);
	}

	public bool TryEscClose()
	{
		Close();
		return true;
	}
}
