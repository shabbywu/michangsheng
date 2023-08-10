using System.Collections.Generic;
using Bag;
using UnityEngine;

namespace JiaoYi;

public class JiaoBag : BaseBag2
{
	public List<JiaoYiSlot> SellList;

	public override void Init(int npcId, bool isPlayer = false)
	{
		_player = Tools.instance.getPlayer();
		ItemType = Bag.ItemType.丹药;
		NpcId = npcId;
		IsPlayer = isPlayer;
		CreateTempList();
		MLoopListView.InitListView(GetCount(MItemTotalCount), base.OnGetItemByIndex);
		ItemType = Bag.ItemType.全部;
		ItemQuality = ItemQuality.全部;
		JiaoYiSkillType = JiaoYiSkillType.全部;
		LianQiCaiLiaoYinYang = LianQiCaiLiaoYinYang.全部;
		LianQiCaiLiaoType = LianQiCaiLiaoType.全部;
		InitLeftFilter();
		foreach (JiaoYiSlot sell in SellList)
		{
			sell.SetNull();
		}
		UpdateLeftFilter(LeftList[0]);
	}

	private void UpdateLeftFilter(BaseFilterLeft filterLeft)
	{
		foreach (BaseFilterLeft left in LeftList)
		{
			if ((Object)(object)left == (Object)(object)filterLeft)
			{
				left.IsSelect = true;
				ItemType = left.ItemType;
				UpdateItem();
				UpdateTopFilter();
			}
			else
			{
				left.IsSelect = false;
			}
			left.UpdateState();
		}
	}

	public JiaoYiSlot GetNullSellList(string uuid)
	{
		foreach (JiaoYiSlot sell in SellList)
		{
			if (sell.IsNull())
			{
				return sell;
			}
			if (sell.Item.Uid == uuid && sell.Item.MaxNum > 1)
			{
				return sell;
			}
		}
		return null;
	}

	public void JiaoYiCallBack()
	{
		foreach (JiaoYiSlot sell in SellList)
		{
			sell.SetNull();
		}
		UpdateMoney();
		CreateTempList();
		UpdateItem();
	}
}
