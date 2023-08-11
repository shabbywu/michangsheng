using UnityEngine;

public class SelectLianDanCaiLiaoPage : MonoBehaviour, IESCClose
{
	public static SelectLianDanCaiLiaoPage Inst;

	[SerializeField]
	private SelectLianDanPage pageManager;

	public bool isInSelectPage;

	private int curSelectIndex;

	private void Awake()
	{
		Inst = this;
	}

	public void openCaiLiaoPackge(int type)
	{
		isInSelectPage = true;
		LianDanSystemManager.inst.inventory.selectType = type;
		pageManager.resetObj();
		((Component)this).gameObject.SetActive(true);
		ESCCloseManager.Inst.RegisterClose(this);
	}

	public void CloseCaiLiaoPackge()
	{
		((Component)this).gameObject.SetActive(false);
		isInSelectPage = false;
		curSelectIndex = -1;
		((MonoBehaviour)this).Invoke("ResetClick", 0.25f);
		ESCCloseManager.Inst.UnRegisterClose(this);
	}

	public void clickMask()
	{
		((Component)this).gameObject.SetActive(false);
		((MonoBehaviour)this).Invoke("ResetClick", 0.25f);
	}

	private void ResetClick()
	{
		LianDanSystemManager.inst.lianDanPageManager.CanClick = true;
	}

	public void setCurSelectIndex(int index)
	{
		curSelectIndex = index;
	}

	public int getCurSelectIndex()
	{
		return curSelectIndex;
	}

	public bool TryEscClose()
	{
		clickMask();
		return true;
	}
}
