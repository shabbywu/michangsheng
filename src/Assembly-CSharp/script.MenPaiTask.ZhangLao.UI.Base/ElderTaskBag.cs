using System;
using Bag;
using KBEngine;
using UnityEngine;

namespace script.MenPaiTask.ZhangLao.UI.Base;

public class ElderTaskBag : BaseBag2, IESCClose
{
	private bool _init;

	public ElderTaskSlot ToSlot;

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

	public override void CreateTempList()
	{
		foreach (int canNeedItem in _player.ElderTaskMag.GetCanNeedItemList())
		{
			try
			{
				ITEM_INFO item = new ITEM_INFO
				{
					itemCount = 1u,
					itemId = canNeedItem,
					uuid = Tools.getUUID(),
					Seid = Tools.CreateItemSeid(canNeedItem)
				};
				TempBagList.Add(item);
			}
			catch (Exception)
			{
				Debug.LogError((object)("物品id:" + canNeedItem + "不存在"));
			}
		}
	}

	public void Open()
	{
		if (!_init)
		{
			Init(0);
			_init = true;
		}
		UpdateItem();
		ESCCloseManager.Inst.RegisterClose(this);
		((Component)this).gameObject.SetActive(true);
	}

	public void Hide()
	{
		ESCCloseManager.Inst.UnRegisterClose(this);
		((Component)this).gameObject.SetActive(false);
	}

	public bool TryEscClose()
	{
		Hide();
		return true;
	}
}
