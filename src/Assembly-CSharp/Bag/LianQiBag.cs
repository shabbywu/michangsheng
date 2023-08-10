using KBEngine;
using UnityEngine;

namespace Bag;

public class LianQiBag : BaseBag2, IESCClose
{
	private bool _init;

	public void Open()
	{
		if (!_init)
		{
			Init(1, isPlayer: true);
			_init = true;
		}
		ESCCloseManager.Inst.RegisterClose(this);
		UpdateItem();
		((Component)this).gameObject.SetActive(true);
	}

	public void Close()
	{
		LianQiTotalManager.inst.ToSlot = null;
		ESCCloseManager.Inst.UnRegisterClose(this);
		((Component)this).gameObject.SetActive(false);
	}

	public BaseItem GetItem(int id)
	{
		foreach (ITEM_INFO tempBag in TempBagList)
		{
			if (tempBag.itemId == id)
			{
				return BaseItem.Create(id, (int)tempBag.itemCount, tempBag.uuid, tempBag.Seid);
			}
		}
		return null;
	}

	public override void Init(int npcId, bool isPlayer = false)
	{
		_player = Tools.instance.getPlayer();
		NpcId = npcId;
		IsPlayer = isPlayer;
		CreateTempList();
		MLoopListView.InitListView(GetCount(MItemTotalCount), base.OnGetItemByIndex);
		ItemType = ItemType.材料;
		ItemQuality = ItemQuality.全部;
		LianQiCaiLiaoYinYang = LianQiCaiLiaoYinYang.全部;
		LianQiCaiLiaoType = LianQiCaiLiaoType.全部;
		UpdateTopFilter();
		UpdateItem();
	}

	public override void UpdateTopFilter()
	{
		OpenQuality();
		OpenType();
		OpenShuXing();
	}

	public bool TryEscClose()
	{
		Close();
		return true;
	}
}
