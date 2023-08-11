using script.Submit;

namespace Bag;

public class SubmitBag : BaseBag2
{
	public void Open()
	{
		Init(0, isPlayer: true);
		UpdateItem(flag: true);
	}

	public override void Init(int npcId, bool isPlayer = false)
	{
		_player = Tools.instance.getPlayer();
		NpcId = npcId;
		IsPlayer = isPlayer;
		MLoopListView.InitListView(GetCount(MItemTotalCount), base.OnGetItemByIndex);
		CreateTempList();
	}

	protected override bool FiddlerItem(BaseItem baseItem)
	{
		if (base.FiddlerItem(baseItem))
		{
			return SubmitUIMag.Inst.CanPut(baseItem);
		}
		return false;
	}
}
