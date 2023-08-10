using System.Collections.Generic;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

public class UIBiGuanGanWuPanel : TabPanelBase
{
	public static UIBiGuanGanWuPanel Inst;

	public RectTransform SVContent;

	public Dropdown Fillter1;

	public Dropdown Filter2;

	public Text RightTitle;

	public Text XiaoGuoText;

	public Text TimeText;

	public Text XiaoHaoText;

	public Text ShuoMingText;

	public ToggleGroup ToggleGroup;

	public GameObject SiXuPrefab;

	public Image BtnImage1;

	public Image BtnImage2;

	public Material GreyMat;

	public List<Sprite> WuDaoTypeSpriteList;

	[HideInInspector]
	public SiXuData NowSiXu;

	private void Awake()
	{
		Inst = this;
	}

	public override void OnPanelShow()
	{
		base.OnPanelShow();
		RefreshUI();
	}

	public void RefreshUI()
	{
		RefreshInventory();
		SetNull();
		UIBiGuanPanel.Inst.RefreshKeFangTime();
	}

	public void RefreshInventory()
	{
		((Transform)(object)SVContent).DestoryAllChild();
		Avatar player = PlayerEx.Player;
		int value = Fillter1.value;
		int value2 = Filter2.value;
		foreach (JSONObject item in player.LingGuang.list)
		{
			SiXuData siXuData = new SiXuData(item);
			if ((value2 == 0 || siXuData.PinJie == value2) && (value == 0 || siXuData.wuDaoFilter == value))
			{
				UISiXuItem component = Object.Instantiate<GameObject>(SiXuPrefab, (Transform)(object)SVContent).GetComponent<UISiXuItem>();
				component.SetData(siXuData);
				((Component)component).gameObject.GetComponent<Toggle>().group = ToggleGroup;
			}
		}
	}

	public void SetNull()
	{
		RightTitle.text = "感悟";
		XiaoGuoText.text = "";
		TimeText.text = "";
		XiaoHaoText.text = "";
		ShuoMingText.text = "";
		((Graphic)BtnImage1).material = GreyMat;
		((Graphic)BtnImage2).material = GreyMat;
		NowSiXu = null;
	}

	public void SetGanWu(SiXuData data)
	{
		if (data == null)
		{
			SetNull();
			return;
		}
		NowSiXu = data;
		RightTitle.text = data.WuDaoTypeStr;
		XiaoGuoText.text = data.XiaoGuo;
		TimeText.text = data.ShengYuTimeFull;
		XiaoHaoText.text = data.XiaoHao;
		ShuoMingText.text = data.ShuoMing;
		((Graphic)BtnImage1).material = null;
		((Graphic)BtnImage2).material = null;
	}

	public void OnGanWuButtonClick()
	{
		GanWu();
	}

	public void GanWu()
	{
		if (NowSiXu != null)
		{
			ResManager.inst.LoadPrefab("GanWuSelect").Inst().GetComponent<GanWuSelect>()
				.Init(NowSiXu.info["uuid"].str, WuDaoAllTypeJson.DataDict[NowSiXu.WuDaoType].name1, Tools.instance.getPlayer().wuDaoMag.CalcGanWuTime(NowSiXu.info));
		}
	}

	public void OnFilterChanged()
	{
		RefreshUI();
	}
}
