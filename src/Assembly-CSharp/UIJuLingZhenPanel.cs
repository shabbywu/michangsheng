using System;
using System.Collections.Generic;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIJuLingZhenPanel : MonoBehaviour, IESCClose
{
	public static UIJuLingZhenPanel Inst;

	public List<string> LingYanSpriteList;

	public GameObject NormalPanel;

	public GameObject ShengJiBtn;

	public ModImage LingYanImage;

	public Text JuLingZhenLevel;

	public Text LingYanLevel;

	public Text ShengJiText;

	public GameObject ShengJiPanel;

	public Text ShengJiLeftLevel;

	public Text ShengJiLeftDaCheng;

	public Text ShengJiLeftDesc;

	public FpBtn ShengJiLeftHighlight;

	public Image ShengJiLeftZhenImage;

	public Text ShengJiRightLevel;

	public Text ShengJiRightDaCheng;

	public Text ShengJiRightDesc;

	public UIIconShow ShengJiRightItemIcon;

	public FpBtn ShengJiRightHighlight;

	public FpBtn ShengJiReturnBtn;

	public FpBtn ShengJiOkBtn;

	private int nowSelectShengJiType;

	private bool canUseZhenDaoShengJi;

	private bool canUseItemShengJi;

	private static string[] szx = new string[3] { "下", "中", "上" };

	private void Awake()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Expected O, but got Unknown
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Expected O, but got Unknown
		Inst = this;
		ShengJiLeftHighlight.mouseUpEvent.AddListener(new UnityAction(OnShengJiLeftHighlightClick));
		ShengJiRightHighlight.mouseUpEvent.AddListener(new UnityAction(OnShengJiRightHighlightClick));
		ShengJiReturnBtn.mouseUpEvent.AddListener(new UnityAction(OnShengJiReturnClick));
	}

	public void RefreshUI()
	{
		//IL_039d: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a7: Expected O, but got Unknown
		UIDongFu.Inst.InitData();
		NormalPanel.SetActive(true);
		ShengJiPanel.SetActive(false);
		int julingZhenLevel = UIDongFu.Inst.DongFu.JuLingZhenLevel;
		int lingYanLevel = UIDongFu.Inst.DongFu.LingYanLevel;
		JuLingZhenLevel.text = julingZhenLevel.ToCNNumber() + "阶聚灵阵";
		LingYanLevel.text = szx[lingYanLevel - 1] + "品灵眼";
		LingYanImage.SpritePath = LingYanSpriteList[lingYanLevel - 1];
		LingYanImage.Refresh();
		ShengJiText.text = "";
		if (julingZhenLevel >= 6)
		{
			ShengJiBtn.SetActive(false);
			ShengJiText.text = "已满级";
			return;
		}
		ShengJiBtn.SetActive(true);
		Avatar player = PlayerEx.Player;
		int wuDaoLevelByType = player.wuDaoMag.getWuDaoLevelByType(10);
		nowSelectShengJiType = 0;
		ShengJiOkBtn.SetGrey(isGrey: true);
		((Behaviour)ShengJiOkBtn).enabled = false;
		((Component)ShengJiLeftHighlight).GetComponent<Image>().sprite = ShengJiLeftHighlight.nomalSprite;
		((Component)ShengJiRightHighlight).GetComponent<Image>().sprite = ShengJiLeftHighlight.nomalSprite;
		ShengJiLeftLevel.text = julingZhenLevel.ToCNNumber() + "阶聚灵阵";
		ShengJiRightLevel.text = (julingZhenLevel + 1).ToCNNumber() + "阶聚灵阵";
		int wudaolevel = DFZhenYanLevel.DataDict[julingZhenLevel + 1].wudaolevel;
		int cost = DFZhenYanLevel.DataDict[julingZhenLevel + 1].buzhenxiaohao;
		ShengJiLeftDesc.text = $"       {cost}布阵升级（需阵道-{WuDaoJinJieJson.DataDict[wudaolevel].Text}）";
		canUseZhenDaoShengJi = wuDaoLevelByType >= wudaolevel && player.money >= (ulong)cost;
		Action leftAction = null;
		if (canUseZhenDaoShengJi)
		{
			ShengJiLeftDaCheng.text = "<color=#fffec0>已达成</color>";
			((Behaviour)ShengJiLeftHighlight).enabled = true;
			leftAction = delegate
			{
				player.AddMoney(-cost);
				player.DongFuData[$"DongFu{DongFuManager.NowDongFuID}"].SetField("JuLingZhenLevel", julingZhenLevel + 1);
				UIDongFu.Inst.ShowJuLingZhenPanel();
				DongFuScene.Inst.RefreshShow();
			};
		}
		else
		{
			ShengJiLeftDaCheng.text = "<color=#7ea99b>未达成</color>";
			((Behaviour)ShengJiLeftHighlight).enabled = false;
		}
		Action rightAction = null;
		int id = 30001 + julingZhenLevel;
		ShengJiRightItemIcon.SetItem(id);
		ShengJiRightDesc.text = "用阵旗升级（需" + (julingZhenLevel + 1).ToCNNumber() + "品阵旗）";
		canUseItemShengJi = player.hasItem(id);
		if (canUseItemShengJi)
		{
			ShengJiRightDaCheng.text = "<color=#fffec0>已达成</color>";
			((Behaviour)ShengJiRightHighlight).enabled = true;
			rightAction = delegate
			{
				player.removeItem(id, 1);
				player.DongFuData[$"DongFu{DongFuManager.NowDongFuID}"].SetField("JuLingZhenLevel", julingZhenLevel + 1);
				UIDongFu.Inst.ShowJuLingZhenPanel();
				DongFuScene.Inst.RefreshShow();
			};
		}
		else
		{
			ShengJiRightDaCheng.text = "<color=#7ea99b>未达成</color>";
			((Behaviour)ShengJiRightHighlight).enabled = false;
		}
		((UnityEventBase)ShengJiOkBtn.mouseUpEvent).RemoveAllListeners();
		ShengJiOkBtn.mouseUpEvent.AddListener((UnityAction)delegate
		{
			if (nowSelectShengJiType == 1)
			{
				if (leftAction != null)
				{
					leftAction();
				}
			}
			else if (nowSelectShengJiType == 2 && rightAction != null)
			{
				rightAction();
			}
		});
	}

	private void OnShengJiLeftHighlightClick()
	{
		nowSelectShengJiType = 1;
		((Behaviour)ShengJiLeftHighlight).enabled = false;
		((Component)ShengJiLeftHighlight).GetComponent<Image>().sprite = ShengJiRightHighlight.mouseEnterSprite;
		((Component)ShengJiRightHighlight).GetComponent<Image>().sprite = ShengJiRightHighlight.nomalSprite;
		if (canUseItemShengJi)
		{
			((Behaviour)ShengJiRightHighlight).enabled = true;
		}
		ShengJiOkBtn.SetGrey(isGrey: false);
		((Behaviour)ShengJiOkBtn).enabled = true;
	}

	private void OnShengJiRightHighlightClick()
	{
		nowSelectShengJiType = 2;
		((Behaviour)ShengJiRightHighlight).enabled = false;
		((Component)ShengJiRightHighlight).GetComponent<Image>().sprite = ShengJiRightHighlight.mouseEnterSprite;
		((Component)ShengJiLeftHighlight).GetComponent<Image>().sprite = ShengJiLeftHighlight.nomalSprite;
		if (canUseZhenDaoShengJi)
		{
			((Behaviour)ShengJiLeftHighlight).enabled = true;
		}
		ShengJiOkBtn.SetGrey(isGrey: false);
		((Behaviour)ShengJiOkBtn).enabled = true;
	}

	private void OnShengJiReturnClick()
	{
		NormalPanel.SetActive(true);
		ShengJiPanel.SetActive(false);
	}

	public void OnShengJiBtnClick()
	{
		NormalPanel.SetActive(false);
		ShengJiPanel.SetActive(true);
	}

	public void OnCloseBtnClick()
	{
		UIDongFu.Inst.HideJuLingZhenPanel();
	}

	public bool TryEscClose()
	{
		UIDongFu.Inst.HideJuLingZhenPanel();
		return true;
	}
}
