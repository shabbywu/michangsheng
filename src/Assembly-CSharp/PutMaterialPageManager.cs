using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020002FE RID: 766
public class PutMaterialPageManager : MonoBehaviour
{
	// Token: 0x06001AB4 RID: 6836 RVA: 0x000BE330 File Offset: 0x000BC530
	public void init()
	{
		base.gameObject.SetActive(false);
		this.wuWeiManager.init();
		this.chuShiLingLiManager.init();
		this.lianQiPageManager.init();
		this.lingWenManager.init();
		this.showXiaoGuoManager.init();
		this.zhongLingLiManager.init();
	}

	// Token: 0x06001AB5 RID: 6837 RVA: 0x000BE38C File Offset: 0x000BC58C
	public void openPutMaterialPage()
	{
		base.gameObject.SetActive(true);
		this.lianQiPageManager.updateEuipImage();
		this.lingWenManager.init();
		base.Invoke("updateShuJu", 0.1f);
		LianQiTotalManager.inst.putMaterialPageManager.lianQiPageManager.putCaiLiaoCell.UpdateCell();
	}

	// Token: 0x06001AB6 RID: 6838 RVA: 0x000BE3E4 File Offset: 0x000BC5E4
	private void updateShuJu()
	{
		this.wuWeiManager.updateWuWei();
		LianQiTotalManager.inst.putCaiLiaoCallBack();
	}

	// Token: 0x06001AB7 RID: 6839 RVA: 0x000BE3FC File Offset: 0x000BC5FC
	private bool CheckXiangKe()
	{
		Avatar player = Tools.instance.getPlayer();
		List<PutMaterialCell> hasItemSlot = this.lianQiPageManager.GetHasItemSlot();
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
		return (text.Contains("金") && text.Contains("木")) || (text.Contains("水") && text.Contains("火")) || (text.Contains("木") && text.Contains("土")) || (text.Contains("土") && text.Contains("水")) || (text.Contains("火") && text.Contains("金"));
	}

	// Token: 0x06001AB8 RID: 6840 RVA: 0x000BE528 File Offset: 0x000BC728
	public void updateWuXingXiangKeTips()
	{
		if (this.CheckXiangKe())
		{
			UIPopTip.Inst.Pop("五行相克不能成器", PopTipIconType.叹号);
		}
	}

	// Token: 0x06001AB9 RID: 6841 RVA: 0x000BE544 File Offset: 0x000BC744
	public void updateCaiLiaoSum()
	{
		if (this.lianQiPageManager.GetHasItemSlot().Count < 10)
		{
			this.lianQiBtn.interactable = false;
			this.lianQiBtn.image.sprite = this.lianQiBtnSprites[1];
			this.lianQiBtntext.sprite = this.lianQiBtnSprites[3];
			return;
		}
		this.lianQiBtn.interactable = true;
		this.lianQiBtn.image.sprite = this.lianQiBtnSprites[0];
		this.lianQiBtntext.sprite = this.lianQiBtnSprites[2];
	}

	// Token: 0x06001ABA RID: 6842 RVA: 0x000BE5E4 File Offset: 0x000BC7E4
	public void lianQiBtnOnclick()
	{
		if (this.lianQiPageManager.GetHasItemSlot().Count < 10)
		{
			return;
		}
		int costTime = LianQiTotalManager.inst.lianQiResultManager.getCostTime();
		string text = "尚需花费";
		if (costTime % 12 == 0)
		{
			text += string.Format("{0}年", costTime / 12);
		}
		else if (costTime / 12 > 0)
		{
			text += string.Format("{0}年{1}月", costTime / 12, costTime % 12);
		}
		else
		{
			text += string.Format("{0}月", costTime);
		}
		LianQiTotalManager.inst.OpenBlack();
		if (this.CheckXiangKe())
		{
			USelectBox.Show("你能感受到这个器胚之中，五行灵力相互对撞几近失控，似乎就要爆炸开来，" + text + ",是否确定要炼器?", new UnityAction(LianQiTotalManager.inst.lianQiFaile), new UnityAction(LianQiTotalManager.inst.CloseBlack));
			return;
		}
		if (!this.showXiaoGuoManager.checkHasOneChiTiao())
		{
			USelectBox.Show("你能感受到这个器胚之中，灵力狂暴失控连最基础的灵纹都没有组成，" + text + ",是否确定要炼器?", new UnityAction(LianQiTotalManager.inst.lianQiFaile), new UnityAction(LianQiTotalManager.inst.CloseBlack));
			return;
		}
		if (this.wuWeiManager.checkIsHasWuWeiZero())
		{
			USelectBox.Show("你能感受到材料之间并不稳定，似乎难以组成器胚，" + text + ",是否确定要炼器?", new UnityAction(LianQiTotalManager.inst.lianQiFaile), new UnityAction(LianQiTotalManager.inst.CloseBlack));
			return;
		}
		if (!this.checkWuDaoLevel())
		{
			USelectBox.Show("你能感受到这个器胚之中，材料品阶过高其中蕴含的能量过于庞大，以你目前的炼器手法难以驾驭，材料之间不能彻底融合难以成器," + text + ",是否确定要炼器?", new UnityAction(LianQiTotalManager.inst.lianQiFaile), new UnityAction(LianQiTotalManager.inst.CloseBlack));
			return;
		}
		USelectBox.Show("你能感受到这个器胚之中，灵力相互兼容浑然一体，" + text + ",是否确定要炼器?", new UnityAction(LianQiTotalManager.inst.lianQiSuccess), new UnityAction(LianQiTotalManager.inst.CloseBlack));
	}

	// Token: 0x06001ABB RID: 6843 RVA: 0x000BE7C4 File Offset: 0x000BC9C4
	public bool checkWuDaoLevel()
	{
		int wuDaoLevelByType = Tools.instance.getPlayer().wuDaoMag.getWuDaoLevelByType(22);
		return (int)LianQiTotalManager.inst.getcurEquipQualityDate()["quality"] <= wuDaoLevelByType;
	}

	// Token: 0x04001575 RID: 5493
	public WuWeiManager wuWeiManager;

	// Token: 0x04001576 RID: 5494
	public ChuShiLingLiManager chuShiLingLiManager;

	// Token: 0x04001577 RID: 5495
	public ZhongLingLiManager zhongLingLiManager;

	// Token: 0x04001578 RID: 5496
	public LingWenManager lingWenManager;

	// Token: 0x04001579 RID: 5497
	public ShowXiaoGuoManager showXiaoGuoManager;

	// Token: 0x0400157A RID: 5498
	public LianQiPageManager lianQiPageManager;

	// Token: 0x0400157B RID: 5499
	[SerializeField]
	private Button lianQiBtn;

	// Token: 0x0400157C RID: 5500
	[SerializeField]
	private List<Sprite> lianQiBtnSprites;

	// Token: 0x0400157D RID: 5501
	[SerializeField]
	private Image lianQiBtntext;
}
