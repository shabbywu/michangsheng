using System;
using System.Collections.Generic;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200020B RID: 523
public class UIJuLingZhenPanel : MonoBehaviour, IESCClose
{
	// Token: 0x060014F3 RID: 5363 RVA: 0x00085F20 File Offset: 0x00084120
	private void Awake()
	{
		UIJuLingZhenPanel.Inst = this;
		this.ShengJiLeftHighlight.mouseUpEvent.AddListener(new UnityAction(this.OnShengJiLeftHighlightClick));
		this.ShengJiRightHighlight.mouseUpEvent.AddListener(new UnityAction(this.OnShengJiRightHighlightClick));
		this.ShengJiReturnBtn.mouseUpEvent.AddListener(new UnityAction(this.OnShengJiReturnClick));
	}

	// Token: 0x060014F4 RID: 5364 RVA: 0x00085F88 File Offset: 0x00084188
	public void RefreshUI()
	{
		UIJuLingZhenPanel.<>c__DisplayClass26_0 CS$<>8__locals1 = new UIJuLingZhenPanel.<>c__DisplayClass26_0();
		CS$<>8__locals1.<>4__this = this;
		UIDongFu.Inst.InitData();
		this.NormalPanel.SetActive(true);
		this.ShengJiPanel.SetActive(false);
		CS$<>8__locals1.julingZhenLevel = UIDongFu.Inst.DongFu.JuLingZhenLevel;
		int lingYanLevel = UIDongFu.Inst.DongFu.LingYanLevel;
		this.JuLingZhenLevel.text = CS$<>8__locals1.julingZhenLevel.ToCNNumber() + "阶聚灵阵";
		this.LingYanLevel.text = UIJuLingZhenPanel.szx[lingYanLevel - 1] + "品灵眼";
		this.LingYanImage.SpritePath = this.LingYanSpriteList[lingYanLevel - 1];
		this.LingYanImage.Refresh();
		this.ShengJiText.text = "";
		if (CS$<>8__locals1.julingZhenLevel >= 6)
		{
			this.ShengJiBtn.SetActive(false);
			this.ShengJiText.text = "已满级";
			return;
		}
		this.ShengJiBtn.SetActive(true);
		Avatar player = PlayerEx.Player;
		int wuDaoLevelByType = player.wuDaoMag.getWuDaoLevelByType(10);
		this.nowSelectShengJiType = 0;
		this.ShengJiOkBtn.SetGrey(true);
		this.ShengJiOkBtn.enabled = false;
		this.ShengJiLeftHighlight.GetComponent<Image>().sprite = this.ShengJiLeftHighlight.nomalSprite;
		this.ShengJiRightHighlight.GetComponent<Image>().sprite = this.ShengJiLeftHighlight.nomalSprite;
		this.ShengJiLeftLevel.text = CS$<>8__locals1.julingZhenLevel.ToCNNumber() + "阶聚灵阵";
		this.ShengJiRightLevel.text = (CS$<>8__locals1.julingZhenLevel + 1).ToCNNumber() + "阶聚灵阵";
		int wudaolevel = DFZhenYanLevel.DataDict[CS$<>8__locals1.julingZhenLevel + 1].wudaolevel;
		int cost = DFZhenYanLevel.DataDict[CS$<>8__locals1.julingZhenLevel + 1].buzhenxiaohao;
		this.ShengJiLeftDesc.text = string.Format("       {0}布阵升级（需阵道-{1}）", cost, WuDaoJinJieJson.DataDict[wudaolevel].Text);
		this.canUseZhenDaoShengJi = (wuDaoLevelByType >= wudaolevel && player.money >= (ulong)((long)cost));
		Action leftAction = null;
		if (this.canUseZhenDaoShengJi)
		{
			this.ShengJiLeftDaCheng.text = "<color=#fffec0>已达成</color>";
			this.ShengJiLeftHighlight.enabled = true;
			leftAction = delegate()
			{
				player.AddMoney(-cost);
				player.DongFuData[string.Format("DongFu{0}", DongFuManager.NowDongFuID)].SetField("JuLingZhenLevel", CS$<>8__locals1.julingZhenLevel + 1);
				UIDongFu.Inst.ShowJuLingZhenPanel();
				DongFuScene.Inst.RefreshShow();
			};
		}
		else
		{
			this.ShengJiLeftDaCheng.text = "<color=#7ea99b>未达成</color>";
			this.ShengJiLeftHighlight.enabled = false;
		}
		Action rightAction = null;
		int id = 30001 + CS$<>8__locals1.julingZhenLevel;
		this.ShengJiRightItemIcon.SetItem(id);
		this.ShengJiRightDesc.text = "用阵旗升级（需" + (CS$<>8__locals1.julingZhenLevel + 1).ToCNNumber() + "品阵旗）";
		this.canUseItemShengJi = player.hasItem(id);
		if (this.canUseItemShengJi)
		{
			this.ShengJiRightDaCheng.text = "<color=#fffec0>已达成</color>";
			this.ShengJiRightHighlight.enabled = true;
			rightAction = delegate()
			{
				player.removeItem(id, 1);
				player.DongFuData[string.Format("DongFu{0}", DongFuManager.NowDongFuID)].SetField("JuLingZhenLevel", CS$<>8__locals1.julingZhenLevel + 1);
				UIDongFu.Inst.ShowJuLingZhenPanel();
				DongFuScene.Inst.RefreshShow();
			};
		}
		else
		{
			this.ShengJiRightDaCheng.text = "<color=#7ea99b>未达成</color>";
			this.ShengJiRightHighlight.enabled = false;
		}
		this.ShengJiOkBtn.mouseUpEvent.RemoveAllListeners();
		this.ShengJiOkBtn.mouseUpEvent.AddListener(delegate()
		{
			if (CS$<>8__locals1.<>4__this.nowSelectShengJiType == 1)
			{
				if (leftAction != null)
				{
					leftAction();
					return;
				}
			}
			else if (CS$<>8__locals1.<>4__this.nowSelectShengJiType == 2 && rightAction != null)
			{
				rightAction();
			}
		});
	}

	// Token: 0x060014F5 RID: 5365 RVA: 0x0008633C File Offset: 0x0008453C
	private void OnShengJiLeftHighlightClick()
	{
		this.nowSelectShengJiType = 1;
		this.ShengJiLeftHighlight.enabled = false;
		this.ShengJiLeftHighlight.GetComponent<Image>().sprite = this.ShengJiRightHighlight.mouseEnterSprite;
		this.ShengJiRightHighlight.GetComponent<Image>().sprite = this.ShengJiRightHighlight.nomalSprite;
		if (this.canUseItemShengJi)
		{
			this.ShengJiRightHighlight.enabled = true;
		}
		this.ShengJiOkBtn.SetGrey(false);
		this.ShengJiOkBtn.enabled = true;
	}

	// Token: 0x060014F6 RID: 5366 RVA: 0x000863C0 File Offset: 0x000845C0
	private void OnShengJiRightHighlightClick()
	{
		this.nowSelectShengJiType = 2;
		this.ShengJiRightHighlight.enabled = false;
		this.ShengJiRightHighlight.GetComponent<Image>().sprite = this.ShengJiRightHighlight.mouseEnterSprite;
		this.ShengJiLeftHighlight.GetComponent<Image>().sprite = this.ShengJiLeftHighlight.nomalSprite;
		if (this.canUseZhenDaoShengJi)
		{
			this.ShengJiLeftHighlight.enabled = true;
		}
		this.ShengJiOkBtn.SetGrey(false);
		this.ShengJiOkBtn.enabled = true;
	}

	// Token: 0x060014F7 RID: 5367 RVA: 0x00086442 File Offset: 0x00084642
	private void OnShengJiReturnClick()
	{
		this.NormalPanel.SetActive(true);
		this.ShengJiPanel.SetActive(false);
	}

	// Token: 0x060014F8 RID: 5368 RVA: 0x0008645C File Offset: 0x0008465C
	public void OnShengJiBtnClick()
	{
		this.NormalPanel.SetActive(false);
		this.ShengJiPanel.SetActive(true);
	}

	// Token: 0x060014F9 RID: 5369 RVA: 0x00086476 File Offset: 0x00084676
	public void OnCloseBtnClick()
	{
		UIDongFu.Inst.HideJuLingZhenPanel();
	}

	// Token: 0x060014FA RID: 5370 RVA: 0x00086482 File Offset: 0x00084682
	public bool TryEscClose()
	{
		UIDongFu.Inst.HideJuLingZhenPanel();
		return true;
	}

	// Token: 0x04000FA7 RID: 4007
	public static UIJuLingZhenPanel Inst;

	// Token: 0x04000FA8 RID: 4008
	public List<string> LingYanSpriteList;

	// Token: 0x04000FA9 RID: 4009
	public GameObject NormalPanel;

	// Token: 0x04000FAA RID: 4010
	public GameObject ShengJiBtn;

	// Token: 0x04000FAB RID: 4011
	public ModImage LingYanImage;

	// Token: 0x04000FAC RID: 4012
	public Text JuLingZhenLevel;

	// Token: 0x04000FAD RID: 4013
	public Text LingYanLevel;

	// Token: 0x04000FAE RID: 4014
	public Text ShengJiText;

	// Token: 0x04000FAF RID: 4015
	public GameObject ShengJiPanel;

	// Token: 0x04000FB0 RID: 4016
	public Text ShengJiLeftLevel;

	// Token: 0x04000FB1 RID: 4017
	public Text ShengJiLeftDaCheng;

	// Token: 0x04000FB2 RID: 4018
	public Text ShengJiLeftDesc;

	// Token: 0x04000FB3 RID: 4019
	public FpBtn ShengJiLeftHighlight;

	// Token: 0x04000FB4 RID: 4020
	public Image ShengJiLeftZhenImage;

	// Token: 0x04000FB5 RID: 4021
	public Text ShengJiRightLevel;

	// Token: 0x04000FB6 RID: 4022
	public Text ShengJiRightDaCheng;

	// Token: 0x04000FB7 RID: 4023
	public Text ShengJiRightDesc;

	// Token: 0x04000FB8 RID: 4024
	public UIIconShow ShengJiRightItemIcon;

	// Token: 0x04000FB9 RID: 4025
	public FpBtn ShengJiRightHighlight;

	// Token: 0x04000FBA RID: 4026
	public FpBtn ShengJiReturnBtn;

	// Token: 0x04000FBB RID: 4027
	public FpBtn ShengJiOkBtn;

	// Token: 0x04000FBC RID: 4028
	private int nowSelectShengJiType;

	// Token: 0x04000FBD RID: 4029
	private bool canUseZhenDaoShengJi;

	// Token: 0x04000FBE RID: 4030
	private bool canUseItemShengJi;

	// Token: 0x04000FBF RID: 4031
	private static string[] szx = new string[]
	{
		"下",
		"中",
		"上"
	};
}
