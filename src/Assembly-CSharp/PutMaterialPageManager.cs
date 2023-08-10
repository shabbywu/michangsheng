using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PutMaterialPageManager : MonoBehaviour
{
	public WuWeiManager wuWeiManager;

	public ChuShiLingLiManager chuShiLingLiManager;

	public ZhongLingLiManager zhongLingLiManager;

	public LingWenManager lingWenManager;

	public ShowXiaoGuoManager showXiaoGuoManager;

	public LianQiPageManager lianQiPageManager;

	[SerializeField]
	private Button lianQiBtn;

	[SerializeField]
	private List<Sprite> lianQiBtnSprites;

	[SerializeField]
	private Image lianQiBtntext;

	public void init()
	{
		((Component)this).gameObject.SetActive(false);
		wuWeiManager.init();
		chuShiLingLiManager.init();
		lianQiPageManager.init();
		lingWenManager.init();
		showXiaoGuoManager.init();
		zhongLingLiManager.init();
	}

	public void openPutMaterialPage()
	{
		((Component)this).gameObject.SetActive(true);
		lianQiPageManager.updateEuipImage();
		lingWenManager.init();
		((MonoBehaviour)this).Invoke("updateShuJu", 0.1f);
		LianQiTotalManager.inst.putMaterialPageManager.lianQiPageManager.putCaiLiaoCell.UpdateCell();
	}

	private void updateShuJu()
	{
		wuWeiManager.updateWuWei();
		LianQiTotalManager.inst.putCaiLiaoCallBack();
	}

	private bool CheckXiangKe()
	{
		Avatar player = Tools.instance.getPlayer();
		List<PutMaterialCell> hasItemSlot = lianQiPageManager.GetHasItemSlot();
		if (hasItemSlot.Count == 0)
		{
			return false;
		}
		if (player.checkHasStudyWuDaoSkillByID(2213))
		{
			return false;
		}
		string text = "";
		for (int i = 0; i < hasItemSlot.Count; i++)
		{
			if (hasItemSlot[i].shuXingTypeID != 0)
			{
				text += jsonData.instance.LianQiHeCheng[hasItemSlot[i].shuXingTypeID.ToString()]["ZhuShi1"].str;
			}
		}
		text = Tools.Code64(text);
		if (text.Contains("金") && text.Contains("木"))
		{
			return true;
		}
		if (text.Contains("水") && text.Contains("火"))
		{
			return true;
		}
		if (text.Contains("木") && text.Contains("土"))
		{
			return true;
		}
		if (text.Contains("土") && text.Contains("水"))
		{
			return true;
		}
		if (text.Contains("火") && text.Contains("金"))
		{
			return true;
		}
		return false;
	}

	public void updateWuXingXiangKeTips()
	{
		if (CheckXiangKe())
		{
			UIPopTip.Inst.Pop("五行相克不能成器");
		}
	}

	public void updateCaiLiaoSum()
	{
		if (lianQiPageManager.GetHasItemSlot().Count < 10)
		{
			((Selectable)lianQiBtn).interactable = false;
			((Selectable)lianQiBtn).image.sprite = lianQiBtnSprites[1];
			lianQiBtntext.sprite = lianQiBtnSprites[3];
		}
		else
		{
			((Selectable)lianQiBtn).interactable = true;
			((Selectable)lianQiBtn).image.sprite = lianQiBtnSprites[0];
			lianQiBtntext.sprite = lianQiBtnSprites[2];
		}
	}

	public void lianQiBtnOnclick()
	{
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Expected O, but got Unknown
		//IL_00d7: Expected O, but got Unknown
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Expected O, but got Unknown
		//IL_011a: Expected O, but got Unknown
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Expected O, but got Unknown
		//IL_015d: Expected O, but got Unknown
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Expected O, but got Unknown
		//IL_01d1: Expected O, but got Unknown
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Expected O, but got Unknown
		//IL_019b: Expected O, but got Unknown
		if (lianQiPageManager.GetHasItemSlot().Count >= 10)
		{
			int costTime = LianQiTotalManager.inst.lianQiResultManager.getCostTime();
			string text = "尚需花费";
			text = ((costTime % 12 == 0) ? (text + $"{costTime / 12}年") : ((costTime / 12 <= 0) ? (text + $"{costTime}月") : (text + $"{costTime / 12}年{costTime % 12}月")));
			LianQiTotalManager.inst.OpenBlack();
			if (CheckXiangKe())
			{
				USelectBox.Show("你能感受到这个器胚之中，五行灵力相互对撞几近失控，似乎就要爆炸开来，" + text + ",是否确定要炼器?", new UnityAction(LianQiTotalManager.inst.lianQiFaile), new UnityAction(LianQiTotalManager.inst.CloseBlack));
			}
			else if (!showXiaoGuoManager.checkHasOneChiTiao())
			{
				USelectBox.Show("你能感受到这个器胚之中，灵力狂暴失控连最基础的灵纹都没有组成，" + text + ",是否确定要炼器?", new UnityAction(LianQiTotalManager.inst.lianQiFaile), new UnityAction(LianQiTotalManager.inst.CloseBlack));
			}
			else if (wuWeiManager.checkIsHasWuWeiZero())
			{
				USelectBox.Show("你能感受到材料之间并不稳定，似乎难以组成器胚，" + text + ",是否确定要炼器?", new UnityAction(LianQiTotalManager.inst.lianQiFaile), new UnityAction(LianQiTotalManager.inst.CloseBlack));
			}
			else if (!checkWuDaoLevel())
			{
				USelectBox.Show("你能感受到这个器胚之中，材料品阶过高其中蕴含的能量过于庞大，以你目前的炼器手法难以驾驭，材料之间不能彻底融合难以成器," + text + ",是否确定要炼器?", new UnityAction(LianQiTotalManager.inst.lianQiFaile), new UnityAction(LianQiTotalManager.inst.CloseBlack));
			}
			else
			{
				USelectBox.Show("你能感受到这个器胚之中，灵力相互兼容浑然一体，" + text + ",是否确定要炼器?", new UnityAction(LianQiTotalManager.inst.lianQiSuccess), new UnityAction(LianQiTotalManager.inst.CloseBlack));
			}
		}
	}

	public bool checkWuDaoLevel()
	{
		int wuDaoLevelByType = Tools.instance.getPlayer().wuDaoMag.getWuDaoLevelByType(22);
		if ((int)LianQiTotalManager.inst.getcurEquipQualityDate()[(object)"quality"] > wuDaoLevelByType)
		{
			return false;
		}
		return true;
	}
}
