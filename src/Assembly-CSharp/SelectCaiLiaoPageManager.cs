using Bag;
using UnityEngine;

public class SelectCaiLiaoPageManager : MonoBehaviour, IESCClose
{
	public LianQiBag bag;

	private int curClickCaiLiaoItem;

	public void init()
	{
		bag.Init(1, isPlayer: true);
	}

	public void OpenBag()
	{
		bag.UpdateItem();
		ESCCloseManager.Inst.RegisterClose(this);
	}

	public void CloseBag()
	{
		ESCCloseManager.Inst.UnRegisterClose(this);
	}

	public void setCurClickCaiLiaoItem(int index)
	{
		curClickCaiLiaoItem = index;
	}

	public int getCurClickCaiLiaoItem()
	{
		return curClickCaiLiaoItem;
	}

	public bool TryEscClose()
	{
		CloseBag();
		return true;
	}
}
