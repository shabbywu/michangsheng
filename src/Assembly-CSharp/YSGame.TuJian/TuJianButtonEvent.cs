using GUIPackage;
using UnityEngine;

namespace YSGame.TuJian;

public class TuJianButtonEvent : MonoBehaviour
{
	public void CloseTuJian()
	{
		TuJianManager.Inst.CloseTuJian();
	}

	public void ReturnLastPage()
	{
		TuJianManager.Inst.ReturnHyperlink();
	}

	public void BeiBaoToTuJian()
	{
		Transform parent = ((Component)this).transform.parent;
		if (((Component)parent.Find("Panel/属性")).gameObject.activeInHierarchy)
		{
			TuJianManager.Inst.OnHyperlink("2_503_1");
		}
		if (((Component)parent.Find("Panel/悟道")).gameObject.activeInHierarchy)
		{
			TuJianManager.Inst.OnHyperlink("2_503_2");
		}
		if (((Component)parent.Find("Panel/功法")).gameObject.activeInHierarchy)
		{
			TuJianManager.Inst.OnHyperlink("2_503_3");
		}
		if (((Component)parent.Find("Panel/神通")).gameObject.activeInHierarchy)
		{
			TuJianManager.Inst.OnHyperlink("2_503_4");
		}
		if (((Component)parent.Find("Panel/物品")).gameObject.activeInHierarchy)
		{
			TuJianManager.Inst.OnHyperlink("2_503_5");
		}
		if (((Component)parent.Find("Panel/声望")).gameObject.activeInHierarchy)
		{
			TuJianManager.Inst.OnHyperlink("2_503_6");
		}
		if (((Component)parent.Find("Panel/系统")).gameObject.activeInHierarchy)
		{
			TuJianManager.Inst.OnHyperlink("2_104_0");
		}
		Singleton.ints.ClickTab();
	}

	public void FightLink()
	{
		TuJianManager.Inst.OnHyperlink("2_502_0");
	}

	public void JiaoYiLink()
	{
		TuJianManager.Inst.OnHyperlink("2_505_4");
	}

	public void ZhuJiLink()
	{
		TuJianManager.Inst.OnHyperlink("2_507_1");
	}

	public void JieDanLink()
	{
		TuJianManager.Inst.OnHyperlink("2_507_2");
	}

	public void JieYingLink()
	{
		TuJianManager.Inst.OnHyperlink("2_507_3");
	}

	public void HuaShenLink()
	{
		TuJianManager.Inst.OnHyperlink("2_507_4");
	}

	public void NPCInfoLink()
	{
		TuJianManager.Inst.OnHyperlink("2_109_0");
	}

	public void QingJiaoLink()
	{
		TuJianManager.Inst.OnHyperlink("2_505_6");
	}
}
