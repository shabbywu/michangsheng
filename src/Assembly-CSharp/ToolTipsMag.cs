using System.Collections.Generic;
using Bag;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;
using WXB;

public class ToolTipsMag : MonoBehaviour
{
	public enum Direction
	{
		左 = 1,
		右
	}

	public static ToolTipsMag Inst;

	private Dictionary<int, string> _nameColordit;

	private Dictionary<int, string> _qualityNameColordit;

	private Dictionary<string, Sprite> _costIconDict;

	private List<RectTransform> _rectList;

	private RectTransform _rectTransform;

	private Direction _direction;

	private int _row;

	[SerializeField]
	private VerticalLayoutGroup _group;

	public BaseItem BaseItem;

	public BaseSkill BaseSkill;

	public Image Icon;

	public Image QualityImage;

	public Image QualityUpImage;

	public GameObject SkillCostImg;

	public Text QualityName;

	public Text TypeName;

	public Text Name;

	public GameObject Panel;

	public GameObject SkillCost;

	public GameObject Desc1Panel;

	public Text Desc1;

	public GameObject ShuXingPanel;

	public Text ShuXingText;

	public GameObject Desc1Pane2;

	public Text Desc2;

	public GameObject PricePanl;

	public Text PriceText;

	public GameObject ShuXingCiTiao;

	public GameObject LingWuTiaoJian;

	public GameObject CiTiao;

	public Transform CiTiaoParent;

	public float JianGe = 10f;

	public Text ShengMingValue;

	public Text XiuWeiValue;

	public Text DanDuValue;

	public Text XingJingValue;

	public Text LingWuTimeText;

	public Text LingWuTiaoJianText;

	public GameObject BottomPanel;

	public List<RectTransform> BottomRectList;

	public RectTransform LeftPanel;

	public RectTransform RightPanel;

	public void UpdateBottom()
	{
		foreach (RectTransform bottomRect in BottomRectList)
		{
			LayoutRebuilder.ForceRebuildLayoutImmediate(bottomRect);
		}
		LayoutRebuilder.ForceRebuildLayoutImmediate(_rectList[0]);
	}

	public void UpdateLeftPanel()
	{
		for (int i = 0; i < ((Component)LeftPanel).gameObject.transform.childCount; i++)
		{
			LayoutRebuilder.ForceRebuildLayoutImmediate(((Component)((Transform)LeftPanel).GetChild(i)).GetComponent<RectTransform>());
		}
		LayoutRebuilder.ForceRebuildLayoutImmediate(LeftPanel);
	}

	public void UpdateRightPanel()
	{
		for (int i = 0; i < ((Component)RightPanel).gameObject.transform.childCount; i++)
		{
			LayoutRebuilder.ForceRebuildLayoutImmediate(((Component)((Transform)RightPanel).GetChild(i)).GetComponent<RectTransform>());
		}
		LayoutRebuilder.ForceRebuildLayoutImmediate(RightPanel);
	}

	public void InitCiTiaoPanel()
	{
		if (_direction == Direction.右)
		{
			((Component)RightPanel).gameObject.SetActive(true);
			((Component)LeftPanel).gameObject.SetActive(false);
		}
		else
		{
			((Component)RightPanel).gameObject.SetActive(false);
			((Component)LeftPanel).gameObject.SetActive(true);
		}
	}

	public void UpdateSize()
	{
		UpdateRightPanel();
		UpdateLeftPanel();
		UpdateBottom();
	}

	private void Update()
	{
		if (UToolTip.IsShouldCloseInput())
		{
			Close();
		}
	}

	private void Awake()
	{
		_nameColordit = new Dictionary<int, string>();
		_nameColordit.Add(1, "#d8d8ca");
		_nameColordit.Add(2, "#b3d951");
		_nameColordit.Add(3, "#71dbff");
		_nameColordit.Add(4, "#ef6fff");
		_nameColordit.Add(5, "#ff9d43");
		_nameColordit.Add(6, "#ff744d");
		_qualityNameColordit = new Dictionary<int, string>();
		_qualityNameColordit.Add(1, "#d8d8ca");
		_qualityNameColordit.Add(2, "#d7e281");
		_qualityNameColordit.Add(3, "#acfffe");
		_qualityNameColordit.Add(4, "#f1b7f8");
		_qualityNameColordit.Add(5, "#ffb143");
		_qualityNameColordit.Add(6, "#ffb28b");
		_costIconDict = ResManager.inst.LoadSpriteAtlas("ToolTips/CastIcon");
		Inst = this;
		((Component)this).gameObject.SetActive(false);
		_rectList = new List<RectTransform>();
		_rectList.Add(Panel.GetComponent<RectTransform>());
		for (int i = 0; i < Panel.transform.childCount; i++)
		{
			_rectList.Add(((Component)Panel.transform.GetChild(i)).GetComponent<RectTransform>());
		}
		BottomRectList = new List<RectTransform>();
		BottomRectList.Add(BottomPanel.GetComponent<RectTransform>());
		for (int j = 0; j < BottomPanel.transform.childCount; j++)
		{
			BottomRectList.Add(((Component)BottomPanel.transform.GetChild(j)).GetComponent<RectTransform>());
		}
	}

	public void ResetUI()
	{
		BaseSkill = null;
		BaseItem = null;
		BottomPanel.SetActive(false);
		((Component)CiTiaoParent).gameObject.SetActive(false);
		LingWuTiaoJian.SetActive(false);
		ShuXingCiTiao.SetActive(false);
		SkillCost.SetActive(false);
		Desc1Panel.SetActive(false);
		ShuXingPanel.SetActive(false);
		Desc1Pane2.SetActive(false);
		PricePanl.SetActive(false);
		((LayoutGroup)_group).padding.bottom = 30;
		_row = 0;
		Tools.ClearChild((Transform)(object)RightPanel);
		Tools.ClearChild((Transform)(object)LeftPanel);
	}

	private void Show(GameObject obj)
	{
		obj.SetActive(true);
	}

	public void Show(BaseItem baseItem)
	{
		ResetUI();
		BaseItem = baseItem;
		Icon.sprite = baseItem.GetIconSprite();
		QualityImage.sprite = baseItem.GetQualitySprite();
		QualityUpImage.sprite = baseItem.GetQualityUpSprite();
		TypeName.SetText(StrTextJsonData.DataDict["ItemType" + baseItem.Type].ChinaText);
		Name.SetText(baseItem.GetName(), _nameColordit[baseItem.GetImgQuality()]);
		QualityName.SetText(baseItem.GetQualityName(), _qualityNameColordit[baseItem.GetImgQuality()]);
		if (baseItem is MiJiItem)
		{
			CreateSkillCostImg((MiJiItem)baseItem);
		}
		Show(Desc1Panel);
		Desc1.SetText(AddCiTiaoColor(baseItem.GetDesc1()));
		Desc1.SetText(AddDesc(baseItem, Desc1.text));
		CreateShuXing(baseItem);
		Show(Desc1Pane2);
		Desc2.SetText(baseItem.GetDesc2() ?? "");
		if (baseItem.MaxNum > 1)
		{
			Desc2.AddText($"\n已有:{Tools.instance.getPlayer().getItemNum(baseItem.Id)}");
		}
		if (baseItem.CanSale)
		{
			PricePanl.SetActive(true);
			PriceText.SetText(baseItem.GetPlayerPrice());
			((LayoutGroup)_group).padding.bottom = 50;
		}
		SetPosition();
		InitCiTiaoPanel();
		List<int> ciZhui = baseItem.GetCiZhui();
		if (ciZhui.Count > 0)
		{
			((Component)CiTiaoParent).gameObject.SetActive(true);
			foreach (int item in ciZhui)
			{
				AddCiTiaoMessage(item);
			}
		}
		if (BaseItem is MiJiItem)
		{
			MiJiItem miJiItem = (MiJiItem)BaseItem;
			if (miJiItem.MiJiType == MiJiType.技能 || miJiItem.MiJiType == MiJiType.功法)
			{
				AddLingWuMessage(BaseItem.Id);
				((Component)CiTiaoParent).gameObject.SetActive(true);
			}
		}
		else if (BaseItem is DanYaoItem)
		{
			((Component)CiTiaoParent).gameObject.SetActive(true);
			AddShuXingMessage();
		}
		else if (BaseItem is CaoYaoItem)
		{
			((Component)CiTiaoParent).gameObject.SetActive(true);
			Avatar player = Tools.instance.getPlayer();
			if (player.GetHasZhuYaoShuXin(BaseItem.Id, BaseItem.GetImgQuality()))
			{
				AddZhuYaoMessage();
			}
			if (player.GetHasFuYaoShuXin(BaseItem.Id, BaseItem.GetImgQuality()))
			{
				AddFuYaoMessage();
			}
		}
		UpdateSize();
	}

	public void Show(BaseItem baseItem, Vector2 vector2)
	{
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		ResetUI();
		BaseItem = baseItem;
		Icon.sprite = baseItem.GetIconSprite();
		QualityImage.sprite = baseItem.GetQualitySprite();
		QualityUpImage.sprite = baseItem.GetQualityUpSprite();
		TypeName.SetText(StrTextJsonData.DataDict["ItemType" + baseItem.Type].ChinaText);
		Name.SetText(baseItem.GetName(), _nameColordit[baseItem.GetImgQuality()]);
		QualityName.SetText(baseItem.GetQualityName(), _qualityNameColordit[baseItem.GetImgQuality()]);
		if (baseItem is MiJiItem)
		{
			CreateSkillCostImg((MiJiItem)baseItem);
		}
		Show(Desc1Panel);
		Desc1.SetText(AddCiTiaoColor(baseItem.GetDesc1()));
		Desc1.SetText(AddDesc(baseItem, Desc1.text));
		CreateShuXing(baseItem);
		Show(Desc1Pane2);
		Desc2.SetText(baseItem.GetDesc2() ?? "");
		if (baseItem.MaxNum > 1)
		{
			Desc2.AddText($"\n已有:{Tools.instance.getPlayer().getItemNum(baseItem.Id)}");
		}
		if (baseItem.CanSale)
		{
			PricePanl.SetActive(true);
			PriceText.SetText(baseItem.GetPlayerPrice());
			((LayoutGroup)_group).padding.bottom = 50;
		}
		Panel.transform.position = Vector2.op_Implicit(vector2);
		((Component)this).gameObject.SetActive(true);
		((Component)this).transform.SetAsLastSibling();
		foreach (RectTransform rect in _rectList)
		{
			LayoutRebuilder.ForceRebuildLayoutImmediate(rect);
		}
		LayoutRebuilder.ForceRebuildLayoutImmediate(_rectList[0]);
		InitCiTiaoPanel();
		List<int> ciZhui = baseItem.GetCiZhui();
		if (ciZhui.Count > 0)
		{
			((Component)CiTiaoParent).gameObject.SetActive(true);
			foreach (int item in ciZhui)
			{
				AddCiTiaoMessage(item);
			}
		}
		if (BaseItem is MiJiItem)
		{
			MiJiItem miJiItem = (MiJiItem)BaseItem;
			if (miJiItem.MiJiType == MiJiType.技能 || miJiItem.MiJiType == MiJiType.功法)
			{
				AddLingWuMessage(BaseItem.Id);
				((Component)CiTiaoParent).gameObject.SetActive(true);
			}
		}
		else if (BaseItem is DanYaoItem)
		{
			((Component)CiTiaoParent).gameObject.SetActive(true);
			AddShuXingMessage();
		}
		else if (BaseItem is CaoYaoItem)
		{
			((Component)CiTiaoParent).gameObject.SetActive(true);
			Avatar player = Tools.instance.getPlayer();
			if (player.GetHasZhuYaoShuXin(BaseItem.Id, BaseItem.GetImgQuality()))
			{
				AddZhuYaoMessage();
			}
			if (player.GetHasFuYaoShuXin(BaseItem.Id, BaseItem.GetImgQuality()))
			{
				AddFuYaoMessage();
			}
		}
		UpdateSize();
	}

	public void Show(BaseItem baseItem, bool isPlayer)
	{
		Show(baseItem);
		if (baseItem.CanSale)
		{
			PricePanl.SetActive(true);
			PriceText.SetText(isPlayer ? baseItem.GetPlayerPrice() : baseItem.GetPrice());
			((LayoutGroup)_group).padding.bottom = 50;
		}
	}

	public void Show(BaseItem baseItem, int price, bool isPlayer)
	{
		Show(baseItem);
		if (!baseItem.CanSale)
		{
			return;
		}
		PriceText.SetText(price);
		if (isPlayer)
		{
			if (price > baseItem.GetPlayerPrice())
			{
				PriceText.SetText(price, "#D55D21");
			}
			else if (price < baseItem.GetPlayerPrice())
			{
				PriceText.SetText(price, "#75C0AE");
			}
		}
		else if (price > baseItem.GetPrice())
		{
			PriceText.SetText(price, "#D55D21");
		}
		else if (price < baseItem.GetPrice())
		{
			PriceText.SetText(price, "#75C0AE");
		}
	}

	public void Show(BaseItem baseItem, int price, string color = "无")
	{
		Show(baseItem);
		if (baseItem.CanSale)
		{
			if (color != "无")
			{
				PriceText.SetText(price, color);
			}
			else
			{
				PriceText.SetText(price);
			}
		}
	}

	public void Show(BaseSkill baseSkill)
	{
		ResetUI();
		BaseSkill = baseSkill;
		Icon.sprite = baseSkill.GetIconSprite();
		QualityImage.sprite = baseSkill.GetQualitySprite();
		QualityUpImage.sprite = baseSkill.GetQualityUpSprite();
		TypeName.SetText(baseSkill.GetTypeName());
		Name.SetText(baseSkill.Name, _nameColordit[baseSkill.GetImgQuality()]);
		QualityName.SetText(baseSkill.GetQualityName(), _qualityNameColordit[baseSkill.GetImgQuality()]);
		if (baseSkill is ActiveSkill)
		{
			CreateSkillCostImg((ActiveSkill)baseSkill);
		}
		Show(Desc1Panel);
		Desc1.SetText(AddCiTiaoColor(baseSkill.GetDesc1()));
		CreateShuXing(baseSkill);
		Show(Desc1Pane2);
		Desc2.SetText(baseSkill.GetDesc2());
		SetPosition();
		InitCiTiaoPanel();
		List<int> ciZhuiList = baseSkill.GetCiZhuiList();
		if (ciZhuiList.Count > 0)
		{
			((Component)CiTiaoParent).gameObject.SetActive(true);
			foreach (int item in ciZhuiList)
			{
				AddCiTiaoMessage(item);
			}
		}
		UpdateSize();
	}

	private void CreateSkillCostImg(ActiveSkill activeSkill)
	{
		List<SkillCost> skillCost = activeSkill.GetSkillCost();
		if (skillCost.Count < 1)
		{
			return;
		}
		Show(SkillCost);
		Transform parent = SkillCostImg.transform.parent;
		Tools.ClearChild(parent);
		foreach (SkillCost item in skillCost)
		{
			Image component = SkillCostImg.Inst(parent).GetComponent<Image>();
			((Component)component).GetComponentInChildren<Text>().SetText($"x<size=26>{item.Num}</size>");
			component.sprite = _costIconDict[item.Id.ToString()];
			((Component)component).gameObject.SetActive(true);
		}
	}

	private void CreateSkillCostImg(MiJiItem miJiItem)
	{
		if (miJiItem.MiJiType != MiJiType.技能)
		{
			return;
		}
		List<SkillCost> miJiCost = miJiItem.GetMiJiCost();
		if (miJiCost.Count < 1)
		{
			return;
		}
		Show(SkillCost);
		Transform parent = SkillCostImg.transform.parent;
		Tools.ClearChild(parent);
		foreach (SkillCost item in miJiCost)
		{
			Image component = SkillCostImg.Inst(parent).GetComponent<Image>();
			((Component)component).GetComponentInChildren<Text>().SetText($"x<size=26>{item.Num}</size>");
			component.sprite = _costIconDict[item.Id.ToString()];
			((Component)component).gameObject.SetActive(true);
		}
	}

	private void CreateShuXing(BaseSkill baseSkill)
	{
		if (baseSkill is PassiveSkill)
		{
			PassiveSkill passiveSkill = (PassiveSkill)baseSkill;
			ShuXingText.SetText("【进度】", "#c09e5c");
			ShuXingText.AddText("第" + Tools.getStr("cengshu" + passiveSkill.Level) + "   ");
			ShuXingText.AddText("【修炼速度】", "#c09e5c");
			ShuXingText.AddText(passiveSkill.GetSpeed());
			Show(ShuXingPanel);
		}
		else
		{
			ShuXingPanel.SetActive(false);
		}
	}

	private void CreateShuXing(BaseItem baseItem)
	{
		if (baseItem is EquipItem)
		{
			EquipItem equipItem = (EquipItem)baseItem;
			switch (equipItem.EquipType)
			{
			default:
				return;
			case EquipType.武器:
				ShuXingText.SetText("【冷却】", "#c09e5c");
				ShuXingText.AddText($"{equipItem.GetCd()}回合   ");
				ShuXingText.AddText("【属性】", "#c09e5c");
				ShuXingText.AddText(equipItem.GetShuXing());
				break;
			case EquipType.灵舟:
			case EquipType.丹炉:
				ShuXingText.SetText("【耐久】", "#c09e5c");
				ShuXingText.AddText($"{equipItem.GetCurNaiJiu()}/{equipItem.GetMaxNaiJiu()}");
				break;
			}
		}
		else if (baseItem is DanYaoItem && baseItem.Type == 5)
		{
			DanYaoItem danYaoItem = (DanYaoItem)baseItem;
			ShuXingText.SetText("【耐药】", "#c09e5c");
			ShuXingText.AddText($"{danYaoItem.GetHasUse()}/{danYaoItem.GetMaxUseNum()}   ");
			ShuXingText.AddText("【丹毒】", "#dd61ff");
			ShuXingText.AddText(danYaoItem.DanDu);
		}
		else if (baseItem is CaoYaoItem)
		{
			CaoYaoItem caoYaoItem = (CaoYaoItem)baseItem;
			ShuXingText.SetText("【主药】", "#c09e5c");
			ShuXingText.AddText(caoYaoItem.GetZhuYao() ?? "");
			ShuXingText.AddText("【辅药】", "#c09e5c");
			ShuXingText.AddText(caoYaoItem.GetFuYao() + "\n");
			ShuXingText.AddText("【药引】", "#c09e5c");
			ShuXingText.AddText(caoYaoItem.GetYaoYin() ?? "");
		}
		else
		{
			if (!(baseItem is CaiLiaoItem))
			{
				ShuXingPanel.SetActive(false);
				return;
			}
			CaiLiaoItem caiLiaoItem = (CaiLiaoItem)baseItem;
			ShuXingText.SetText("【种类】", "#c09e5c");
			ShuXingText.AddText(caiLiaoItem.GetZhongLei() + "  ");
			ShuXingText.AddText("【属性】", "#c09e5c");
			ShuXingText.AddText(caiLiaoItem.GetShuXing() ?? "");
		}
		Show(ShuXingPanel);
	}

	private string AddCiTiaoColor(string desc1)
	{
		desc1 = desc1.ToCN();
		desc1 = desc1.Replace("【", "<color=#42e395>【");
		desc1 = desc1.Replace("】", "】</color>");
		return desc1;
	}

	private string AddDesc(BaseItem baseItem, string desc1)
	{
		if (baseItem is EquipItem)
		{
			EquipItem equipItem = (EquipItem)baseItem;
			if (equipItem.EquipType == EquipType.武器)
			{
				desc1 = desc1.Replace("主动：", "");
				desc1 = "<color=#f28125>【主动】</color>" + desc1;
				return desc1;
			}
			if (equipItem.EquipType == EquipType.防具 || equipItem.EquipType == EquipType.饰品)
			{
				desc1 = "<color=#32b0ea>【被动】</color>" + desc1;
				return desc1;
			}
		}
		return desc1;
	}

	private void SetPosition()
	{
		PCSetPosition();
		((Component)this).gameObject.SetActive(true);
		((Component)this).transform.SetAsLastSibling();
		foreach (RectTransform rect in _rectList)
		{
			LayoutRebuilder.ForceRebuildLayoutImmediate(rect);
		}
		LayoutRebuilder.ForceRebuildLayoutImmediate(_rectList[0]);
	}

	private void PCSetPosition()
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)_rectTransform == (Object)null)
		{
			_rectTransform = Panel.GetComponent<RectTransform>();
		}
		Rect rect = _rectTransform.rect;
		float x = Input.mousePosition.x;
		float num = (float)Screen.height * 4f / 5f;
		if (x > (float)Screen.width / 2f)
		{
			x = ((float)Screen.width - ((Rect)(ref rect)).width) / 2f;
			_direction = Direction.左;
		}
		else
		{
			x = ((float)Screen.width + ((Rect)(ref rect)).width) / 2f;
			_direction = Direction.右;
		}
		Panel.transform.position = NewUICanvas.Inst.Camera.ScreenToWorldPoint(Vector2.op_Implicit(new Vector2(x, num)));
	}

	public float GetMouseY()
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)_rectTransform == (Object)null)
		{
			_rectTransform = Panel.GetComponent<RectTransform>();
		}
		Rect rect = _rectTransform.rect;
		float x = Input.mousePosition.x;
		float num = (float)Screen.height * 4f / 5f;
		if (x > (float)Screen.width / 2f)
		{
			x = ((float)Screen.width - ((Rect)(ref rect)).width) / 2f;
			_direction = Direction.左;
		}
		else
		{
			x = ((float)Screen.width + ((Rect)(ref rect)).width) / 2f;
			_direction = Direction.右;
		}
		return NewUICanvas.Inst.Camera.ScreenToWorldPoint(Vector2.op_Implicit(new Vector2(x, num))).y;
	}

	private void AddCiTiaoMessage(int id)
	{
		TuJianChunWenBen tuJianChunWenBen = TuJianChunWenBen.DataDict[id];
		Transform parent = ((_direction == Direction.右) ? ((Component)RightPanel).transform : ((Component)LeftPanel).transform);
		GameObject obj = CiTiao.Inst(parent);
		obj.SetActive(true);
		((Text)obj.GetComponentInChildren<SymbolText>()).text = "#c42e395【" + tuJianChunWenBen.name2 + "】#n" + tuJianChunWenBen.descr;
	}

	private void AddShuXingMessage()
	{
		Avatar player = Tools.instance.getPlayer();
		ShengMingValue.SetText(player.HP);
		ShengMingValue.AddText($"/{player.HP_Max}", "#acab74");
		XiuWeiValue.SetText(player.exp);
		XiuWeiValue.AddText($"/{LevelUpDataJsonData.DataDict[player.level].MaxExp}", "#acab74");
		DanDuValue.SetText(player.Dandu);
		DanDuValue.AddText("/120", "#acab74");
		XingJingValue.SetText(player.xinjin);
		XingJingValue.AddText($"/{XinJinJsonData.DataDict[player.GetXinJingLevel()].Max}", "#acab74");
		BottomPanel.SetActive(true);
		ShuXingCiTiao.SetActive(true);
		ShuXingCiTiao.transform.SetParent(BottomPanel.transform);
	}

	private void AddLingWuMessage(int bookItemID)
	{
		int day = Tools.CalcLingWuTime(bookItemID);
		LingWuTimeText.SetText("领悟时间:", "d3b068");
		LingWuTimeText.AddText(Tools.getStr("xiaohaoshijian").Replace("{Y}", string.Concat(Tools.DayToYear(day))).Replace("{M}", string.Concat(Tools.DayToMonth(day)))
			.Replace("{D}", string.Concat(Tools.DayToDay(day)))
			.Replace("消耗时间：", ""));
		LingWuTiaoJianText.SetText("领悟条件:", "#d3b068");
		string text = "";
		int num = 0;
		string text2 = "";
		foreach (int key in BaseItem.WuDaoDict.Keys)
		{
			text2 = text2 + WuDaoAllTypeJson.DataDict[key].name + ",";
			if (!WuDaoJinJieJson.DataDict[BaseItem.WuDaoDict[key]].Text.Contains("一窍不通"))
			{
				num++;
				text = text + "对" + WuDaoAllTypeJson.DataDict[key].name1 + "的领悟达到" + WuDaoJinJieJson.DataDict[BaseItem.WuDaoDict[key]].Text + ",";
			}
		}
		if (num == 0)
		{
			text += "无,";
		}
		text2 = text2.TrimEnd(",".ToCharArray());
		text = text + "领悟后提升" + text2 + "大道感悟";
		LingWuTiaoJianText.AddText(text);
		BottomPanel.SetActive(true);
		LingWuTiaoJian.SetActive(true);
		LingWuTiaoJian.transform.SetParent(BottomPanel.transform);
	}

	private void AddZhuYaoMessage()
	{
		CaoYaoItem caoYaoItem = (CaoYaoItem)BaseItem;
		if (caoYaoItem.ZhuYao != 0)
		{
			LianDanItemLeiXin lianDanItemLeiXin = LianDanItemLeiXin.DataDict[caoYaoItem.ZhuYao];
			Transform parent = ((_direction == Direction.右) ? ((Component)RightPanel).transform : ((Component)LeftPanel).transform);
			GameObject obj = CiTiao.Inst(parent);
			obj.SetActive(true);
			((Text)obj.GetComponentInChildren<SymbolText>()).text = "#c42e395【" + lianDanItemLeiXin.name + "】#n" + lianDanItemLeiXin.desc;
		}
	}

	private void AddFuYaoMessage()
	{
		CaoYaoItem caoYaoItem = (CaoYaoItem)BaseItem;
		if (caoYaoItem.FuYao != 0)
		{
			LianDanItemLeiXin lianDanItemLeiXin = LianDanItemLeiXin.DataDict[caoYaoItem.FuYao];
			Transform parent = ((_direction == Direction.右) ? ((Component)RightPanel).transform : ((Component)LeftPanel).transform);
			GameObject obj = CiTiao.Inst(parent);
			obj.SetActive(true);
			((Text)obj.GetComponentInChildren<SymbolText>()).text = "#c42e395【" + lianDanItemLeiXin.name + "】#n" + lianDanItemLeiXin.desc;
		}
	}

	public void Close()
	{
		((Component)this).gameObject.SetActive(false);
	}
}
