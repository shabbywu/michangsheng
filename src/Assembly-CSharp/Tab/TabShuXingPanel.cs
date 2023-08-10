using System;
using System.Collections.Generic;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Tab;

[Serializable]
public class TabShuXingPanel : ITabPanelBase
{
	private Dictionary<string, Sprite> _levelImgDict1;

	private Dictionary<string, Sprite> _levelImgDict2;

	private Dictionary<string, Sprite> _xinJingImgDict;

	private Dictionary<int, Color32> _xinJingColor;

	private Dictionary<string, Sprite> _danDuImgDict;

	private Dictionary<int, Color32> _danDuImgColor;

	private Dictionary<string, Sprite> _lingGanImgDict;

	private Dictionary<int, Color32> _lingGanColor;

	private Avatar _player;

	private bool _isInit;

	private Text _levelName;

	private Image _levelImg1;

	private Image _levelImg2;

	private FpBtn _levelBtn;

	private Text _age;

	private Text _shouYuan;

	private Text _ziZi;

	private Text _shenShi;

	private Text _wuXing;

	private Text _dunSu;

	private Text _xiuLianSpeed;

	private Text _xinJingValueName;

	private Text _xinJingValue;

	private Image _xinJingImg;

	private Text _danduValueName;

	private Text _danduValue;

	private Image _danduImg;

	private Text _lingganValueName;

	private Text _lingganValue;

	private Image _lingganImg;

	private Text _menPaiValue;

	private Text _menPaiShengWangValue;

	private Text _zhiWeiValue;

	private Text _fengLuValue;

	private Text _jin;

	private Text _mu;

	private Text _shui;

	private Text _huo;

	private Text _tu;

	public TabShuXingPanel(GameObject gameObject)
	{
		HasHp = true;
		_go = gameObject;
		_player = Tools.instance.getPlayer();
	}

	private void Init()
	{
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0348: Unknown result type (might be due to invalid IL or missing references)
		//IL_0352: Expected O, but got Unknown
		//IL_0364: Unknown result type (might be due to invalid IL or missing references)
		//IL_036e: Expected O, but got Unknown
		//IL_0509: Unknown result type (might be due to invalid IL or missing references)
		//IL_050e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0521: Unknown result type (might be due to invalid IL or missing references)
		//IL_052b: Expected O, but got Unknown
		//IL_0547: Unknown result type (might be due to invalid IL or missing references)
		//IL_0551: Expected O, but got Unknown
		_levelImgDict1 = ResManager.inst.LoadSpriteAtlas("NewTab/LevelImg1");
		_levelImgDict2 = ResManager.inst.LoadSpriteAtlas("NewTab/LevelImg2");
		_xinJingImgDict = ResManager.inst.LoadSpriteAtlas("NewTab/XinJingImg");
		_xinJingColor = new Dictionary<int, Color32>();
		_xinJingColor.Add(1, new Color32(byte.MaxValue, (byte)142, (byte)100, byte.MaxValue));
		_xinJingColor.Add(2, new Color32((byte)227, (byte)199, (byte)108, byte.MaxValue));
		_xinJingColor.Add(3, new Color32((byte)218, (byte)227, (byte)108, byte.MaxValue));
		_xinJingColor.Add(4, new Color32((byte)197, (byte)224, (byte)91, byte.MaxValue));
		_xinJingColor.Add(5, new Color32((byte)124, (byte)234, (byte)150, byte.MaxValue));
		_xinJingColor.Add(6, new Color32((byte)124, (byte)234, (byte)232, byte.MaxValue));
		_xinJingColor.Add(7, new Color32((byte)159, (byte)201, byte.MaxValue, byte.MaxValue));
		_danDuImgDict = ResManager.inst.LoadSpriteAtlas("NewTab/DanDuImg");
		_danDuImgColor = new Dictionary<int, Color32>();
		_danDuImgColor.Add(1, new Color32((byte)233, (byte)228, (byte)164, byte.MaxValue));
		_danDuImgColor.Add(2, new Color32((byte)233, (byte)228, (byte)164, byte.MaxValue));
		_danDuImgColor.Add(3, new Color32((byte)233, (byte)211, (byte)164, byte.MaxValue));
		_danDuImgColor.Add(4, new Color32((byte)244, (byte)180, (byte)123, byte.MaxValue));
		_danDuImgColor.Add(5, new Color32((byte)211, (byte)135, (byte)178, byte.MaxValue));
		_lingGanImgDict = ResManager.inst.LoadSpriteAtlas("NewTab/LingGanImg");
		_lingGanColor = new Dictionary<int, Color32>();
		_lingGanColor.Add(4, new Color32((byte)170, (byte)171, (byte)150, byte.MaxValue));
		_lingGanColor.Add(3, new Color32((byte)233, (byte)228, (byte)164, byte.MaxValue));
		_lingGanColor.Add(2, new Color32((byte)100, (byte)201, (byte)122, byte.MaxValue));
		_lingGanColor.Add(1, new Color32((byte)141, (byte)241, (byte)236, byte.MaxValue));
		Get<Text>("PlayerName/Name_Text").text = _player.name;
		_levelName = Get<Text>("Level/Level_Name");
		_levelImg1 = Get<Image>("Level/Level_Name/Level_Img1");
		_levelImg2 = Get<Image>("Level/Level_Img2");
		_levelBtn = Get<FpBtn>("Level/Level_Img2/Listener");
		LevelTips levelTips = new LevelTips(Get("Level/LevelTips"));
		_levelBtn.mouseEnterEvent.AddListener(new UnityAction(levelTips.Show));
		_levelBtn.mouseOutEvent.AddListener(new UnityAction(levelTips.Hide));
		_age = Get<Text>("BaseData/年龄/Value");
		_shouYuan = Get<Text>("BaseData/寿元/Value");
		_ziZi = Get<Text>("BaseData/资质/Value");
		_shenShi = Get<Text>("BaseData/神识/Value");
		_wuXing = Get<Text>("BaseData/悟性/Value");
		_dunSu = Get<Text>("BaseData/遁速/Value");
		_xiuLianSpeed = Get<Text>("BaseData/修炼速度/Value");
		_xinJingImg = Get<Image>("BaseData/心境/BG");
		_xinJingValueName = Get<Text>("BaseData/心境/ValueName");
		_xinJingValue = Get<Text>("BaseData/心境/Value");
		_danduImg = Get<Image>("BaseData/丹毒/BG");
		_danduValueName = Get<Text>("BaseData/丹毒/ValueName");
		_danduValue = Get<Text>("BaseData/丹毒/Value");
		_lingganImg = Get<Image>("BaseData/灵感/BG");
		_lingganValueName = Get<Text>("BaseData/灵感/ValueName");
		_lingganValue = Get<Text>("BaseData/灵感/Value");
		BaseDataTips dataTips = new BaseDataTips(Get("BaseData/BaseDataTips"));
		GameObject val = null;
		foreach (TianFuDescJsonData data in TianFuDescJsonData.DataList)
		{
			val = Get("BaseData/" + data.Title, showError: false);
			if ((Object)(object)val != (Object)null)
			{
				UIListener uIListener = val.AddComponent<UIListener>();
				Vector3 position = val.transform.position;
				uIListener.mouseEnterEvent.AddListener((UnityAction)delegate
				{
					//IL_0021: Unknown result type (might be due to invalid IL or missing references)
					dataTips.Show(data.Desc, position);
				});
				uIListener.mouseOutEvent.AddListener(new UnityAction(dataTips.Hide));
			}
		}
		_menPaiValue = Get<Text>("BaseData/门派/Value");
		_menPaiShengWangValue = Get<Text>("BaseData/门派声望/Value");
		_zhiWeiValue = Get<Text>("BaseData/职位/Value");
		_fengLuValue = Get<Text>("BaseData/俸禄/Value");
		_jin = Get<Text>("LingGen/金灵根/Value");
		_mu = Get<Text>("LingGen/木灵根/Value");
		_shui = Get<Text>("LingGen/水灵根/Value");
		_huo = Get<Text>("LingGen/火灵根/Value");
		_tu = Get<Text>("LingGen/土灵根/Value");
	}

	private void UpdateUI()
	{
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		//IL_034c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03da: Unknown result type (might be due to invalid IL or missing references)
		//IL_043d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0442: Unknown result type (might be due to invalid IL or missing references)
		_levelName.text = LevelUpDataJsonData.DataDict[_player.level].Name.Insert(2, "   ");
		_levelImg1.sprite = _levelImgDict1[_player.level.ToString()];
		if (_player.level < 13)
		{
			string text = $"{_player.level}-";
			_levelImg2.sprite = _levelImgDict2[text + "1"];
			_levelBtn.nomalSprite = _levelImgDict2[text + "1"];
			_levelBtn.mouseEnterSprite = _levelImgDict2[text + "2"];
		}
		else
		{
			string arg = ((_player.Sex == 1) ? "huashen-nan" : "huashen-nv");
			int i = _player.HuaShenWuDao.I;
			i = Mathf.Max(i, 1);
			string text2 = $"{arg}-{i}-";
			_levelImg2.sprite = _levelImgDict2[text2 + "1"];
			_levelBtn.nomalSprite = _levelImgDict2[text2 + "1"];
			_levelBtn.mouseEnterSprite = _levelImgDict2[text2 + "2"];
		}
		_age.text = _player.age.ToString();
		_shouYuan.text = _player.shouYuan.ToString();
		_ziZi.text = _player.ZiZhi.ToString();
		_shenShi.text = _player.shengShi.ToString();
		_wuXing.text = _player.wuXin.ToString();
		_dunSu.text = _player.dunSu.ToString();
		_xiuLianSpeed.text = $"{(int)_player.getTimeExpSpeed()}/月";
		int xinJingLevel = _player.GetXinJingLevel();
		if (XinJinJsonData.DataDict[xinJingLevel].Max > 0)
		{
			if (xinJingLevel == 7)
			{
				_xinJingValue.text = $"{_player.xinjin}/-";
			}
			else
			{
				_xinJingValue.text = $"{_player.xinjin}/{XinJinJsonData.DataDict[xinJingLevel].Max}";
			}
		}
		else
		{
			_xinJingValue.text = $"{_player.xinjin}/0";
		}
		_xinJingValueName.text = XinJinJsonData.DataDict[xinJingLevel].Text;
		((Graphic)_xinJingValueName).color = Color32.op_Implicit(_xinJingColor[xinJingLevel]);
		_xinJingImg.sprite = _xinJingImgDict[xinJingLevel.ToString()];
		int danDuLevel = _player.GetDanDuLevel();
		_danduValue.text = $"{_player.Dandu}/120";
		_danduImg.sprite = _danDuImgDict[(danDuLevel + 1).ToString()];
		((Graphic)_danduValueName).color = Color32.op_Implicit(_danDuImgColor[danDuLevel + 1]);
		_danduValueName.text = DanduMiaoShu.DataDict[danDuLevel + 1].name;
		_lingganValueName.text = LunDaoStateData.DataDict[_player.LunDaoState].ZhuangTaiInfo;
		((Graphic)_lingganValueName).color = Color32.op_Implicit(_lingGanColor[_player.LunDaoState]);
		_lingganValue.text = $"{_player.LingGan}/{_player.GetLingGanMax()}";
		_lingganImg.sprite = _lingGanImgDict[_player.LunDaoState.ToString()];
		_menPaiValue.text = Tools.getStr("menpai" + _player.menPai);
		_menPaiShengWangValue.text = PlayerEx.GetMenPaiShengWang().ToString();
		_zhiWeiValue.text = PlayerEx.GetMenPaiChengHao();
		_fengLuValue.text = _player.chenghaomag.GetOneYearAddMoney() + "灵石/年";
		_jin.text = _player.GetLingGeng[0].ToString();
		_mu.text = _player.GetLingGeng[1].ToString();
		_shui.text = _player.GetLingGeng[2].ToString();
		_huo.text = _player.GetLingGeng[3].ToString();
		_tu.text = _player.GetLingGeng[4].ToString();
	}

	public override void Show()
	{
		if (!_isInit)
		{
			Init();
			_isInit = true;
		}
		_go.SetActive(true);
		UpdateUI();
	}
}
