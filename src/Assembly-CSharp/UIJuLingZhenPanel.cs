using System;
using System.Collections.Generic;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000321 RID: 801
public class UIJuLingZhenPanel : MonoBehaviour, IESCClose
{
	// Token: 0x0600179F RID: 6047 RVA: 0x000CE634 File Offset: 0x000CC834
	private void Awake()
	{
		UIJuLingZhenPanel.Inst = this;
		this.ShengJiLeftHighlight.mouseUpEvent.AddListener(new UnityAction(this.OnShengJiLeftHighlightClick));
		this.ShengJiRightHighlight.mouseUpEvent.AddListener(new UnityAction(this.OnShengJiRightHighlightClick));
		this.ShengJiReturnBtn.mouseUpEvent.AddListener(new UnityAction(this.OnShengJiReturnClick));
	}

	// Token: 0x060017A0 RID: 6048 RVA: 0x000CE69C File Offset: 0x000CC89C
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

	// Token: 0x060017A1 RID: 6049 RVA: 0x000CEA50 File Offset: 0x000CCC50
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

	// Token: 0x060017A2 RID: 6050 RVA: 0x000CEAD4 File Offset: 0x000CCCD4
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

	// Token: 0x060017A3 RID: 6051 RVA: 0x00014E57 File Offset: 0x00013057
	private void OnShengJiReturnClick()
	{
		this.NormalPanel.SetActive(true);
		this.ShengJiPanel.SetActive(false);
	}

	// Token: 0x060017A4 RID: 6052 RVA: 0x00014E71 File Offset: 0x00013071
	public void OnShengJiBtnClick()
	{
		this.NormalPanel.SetActive(false);
		this.ShengJiPanel.SetActive(true);
	}

	// Token: 0x060017A5 RID: 6053 RVA: 0x00014E8B File Offset: 0x0001308B
	public void OnCloseBtnClick()
	{
		UIDongFu.Inst.HideJuLingZhenPanel();
	}

	// Token: 0x060017A6 RID: 6054 RVA: 0x00014E97 File Offset: 0x00013097
	public bool TryEscClose()
	{
		UIDongFu.Inst.HideJuLingZhenPanel();
		return true;
	}

	// Token: 0x040012EF RID: 4847
	public static UIJuLingZhenPanel Inst;

	// Token: 0x040012F0 RID: 4848
	public List<string> LingYanSpriteList;

	// Token: 0x040012F1 RID: 4849
	public GameObject NormalPanel;

	// Token: 0x040012F2 RID: 4850
	public GameObject ShengJiBtn;

	// Token: 0x040012F3 RID: 4851
	public ModImage LingYanImage;

	// Token: 0x040012F4 RID: 4852
	public Text JuLingZhenLevel;

	// Token: 0x040012F5 RID: 4853
	public Text LingYanLevel;

	// Token: 0x040012F6 RID: 4854
	public Text ShengJiText;

	// Token: 0x040012F7 RID: 4855
	public GameObject ShengJiPanel;

	// Token: 0x040012F8 RID: 4856
	public Text ShengJiLeftLevel;

	// Token: 0x040012F9 RID: 4857
	public Text ShengJiLeftDaCheng;

	// Token: 0x040012FA RID: 4858
	public Text ShengJiLeftDesc;

	// Token: 0x040012FB RID: 4859
	public FpBtn ShengJiLeftHighlight;

	// Token: 0x040012FC RID: 4860
	public Image ShengJiLeftZhenImage;

	// Token: 0x040012FD RID: 4861
	public Text ShengJiRightLevel;

	// Token: 0x040012FE RID: 4862
	public Text ShengJiRightDaCheng;

	// Token: 0x040012FF RID: 4863
	public Text ShengJiRightDesc;

	// Token: 0x04001300 RID: 4864
	public UIIconShow ShengJiRightItemIcon;

	// Token: 0x04001301 RID: 4865
	public FpBtn ShengJiRightHighlight;

	// Token: 0x04001302 RID: 4866
	public FpBtn ShengJiReturnBtn;

	// Token: 0x04001303 RID: 4867
	public FpBtn ShengJiOkBtn;

	// Token: 0x04001304 RID: 4868
	private int nowSelectShengJiType;

	// Token: 0x04001305 RID: 4869
	private bool canUseZhenDaoShengJi;

	// Token: 0x04001306 RID: 4870
	private bool canUseItemShengJi;

	// Token: 0x04001307 RID: 4871
	private static string[] szx = new string[]
	{
		"下",
		"中",
		"上"
	};
}
