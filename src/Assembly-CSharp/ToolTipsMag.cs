using System;
using System.Collections.Generic;
using Bag;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;
using WXB;

// Token: 0x020003C6 RID: 966
public class ToolTipsMag : MonoBehaviour
{
	// Token: 0x06001F7A RID: 8058 RVA: 0x000DD628 File Offset: 0x000DB828
	public void UpdateBottom()
	{
		foreach (RectTransform rectTransform in this.BottomRectList)
		{
			LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
		}
		LayoutRebuilder.ForceRebuildLayoutImmediate(this._rectList[0]);
	}

	// Token: 0x06001F7B RID: 8059 RVA: 0x000DD68C File Offset: 0x000DB88C
	public void UpdateLeftPanel()
	{
		for (int i = 0; i < this.LeftPanel.gameObject.transform.childCount; i++)
		{
			LayoutRebuilder.ForceRebuildLayoutImmediate(this.LeftPanel.GetChild(i).GetComponent<RectTransform>());
		}
		LayoutRebuilder.ForceRebuildLayoutImmediate(this.LeftPanel);
	}

	// Token: 0x06001F7C RID: 8060 RVA: 0x000DD6DC File Offset: 0x000DB8DC
	public void UpdateRightPanel()
	{
		for (int i = 0; i < this.RightPanel.gameObject.transform.childCount; i++)
		{
			LayoutRebuilder.ForceRebuildLayoutImmediate(this.RightPanel.GetChild(i).GetComponent<RectTransform>());
		}
		LayoutRebuilder.ForceRebuildLayoutImmediate(this.RightPanel);
	}

	// Token: 0x06001F7D RID: 8061 RVA: 0x000DD72C File Offset: 0x000DB92C
	public void InitCiTiaoPanel()
	{
		if (this._direction == ToolTipsMag.Direction.右)
		{
			this.RightPanel.gameObject.SetActive(true);
			this.LeftPanel.gameObject.SetActive(false);
			return;
		}
		this.RightPanel.gameObject.SetActive(false);
		this.LeftPanel.gameObject.SetActive(true);
	}

	// Token: 0x06001F7E RID: 8062 RVA: 0x000DD787 File Offset: 0x000DB987
	public void UpdateSize()
	{
		this.UpdateRightPanel();
		this.UpdateLeftPanel();
		this.UpdateBottom();
	}

	// Token: 0x06001F7F RID: 8063 RVA: 0x000DD79B File Offset: 0x000DB99B
	private void Update()
	{
		if (UToolTip.IsShouldCloseInput())
		{
			this.Close();
		}
	}

	// Token: 0x06001F80 RID: 8064 RVA: 0x000DD7AC File Offset: 0x000DB9AC
	private void Awake()
	{
		this._nameColordit = new Dictionary<int, string>();
		this._nameColordit.Add(1, "#d8d8ca");
		this._nameColordit.Add(2, "#b3d951");
		this._nameColordit.Add(3, "#71dbff");
		this._nameColordit.Add(4, "#ef6fff");
		this._nameColordit.Add(5, "#ff9d43");
		this._nameColordit.Add(6, "#ff744d");
		this._qualityNameColordit = new Dictionary<int, string>();
		this._qualityNameColordit.Add(1, "#d8d8ca");
		this._qualityNameColordit.Add(2, "#d7e281");
		this._qualityNameColordit.Add(3, "#acfffe");
		this._qualityNameColordit.Add(4, "#f1b7f8");
		this._qualityNameColordit.Add(5, "#ffb143");
		this._qualityNameColordit.Add(6, "#ffb28b");
		this._costIconDict = ResManager.inst.LoadSpriteAtlas("ToolTips/CastIcon");
		ToolTipsMag.Inst = this;
		base.gameObject.SetActive(false);
		this._rectList = new List<RectTransform>();
		this._rectList.Add(this.Panel.GetComponent<RectTransform>());
		for (int i = 0; i < this.Panel.transform.childCount; i++)
		{
			this._rectList.Add(this.Panel.transform.GetChild(i).GetComponent<RectTransform>());
		}
		this.BottomRectList = new List<RectTransform>();
		this.BottomRectList.Add(this.BottomPanel.GetComponent<RectTransform>());
		for (int j = 0; j < this.BottomPanel.transform.childCount; j++)
		{
			this.BottomRectList.Add(this.BottomPanel.transform.GetChild(j).GetComponent<RectTransform>());
		}
	}

	// Token: 0x06001F81 RID: 8065 RVA: 0x000DD97C File Offset: 0x000DBB7C
	public void ResetUI()
	{
		this.BaseSkill = null;
		this.BaseItem = null;
		this.BottomPanel.SetActive(false);
		this.CiTiaoParent.gameObject.SetActive(false);
		this.LingWuTiaoJian.SetActive(false);
		this.ShuXingCiTiao.SetActive(false);
		this.SkillCost.SetActive(false);
		this.Desc1Panel.SetActive(false);
		this.ShuXingPanel.SetActive(false);
		this.Desc1Pane2.SetActive(false);
		this.PricePanl.SetActive(false);
		this._group.padding.bottom = 30;
		this._row = 0;
		global::Tools.ClearChild(this.RightPanel);
		global::Tools.ClearChild(this.LeftPanel);
	}

	// Token: 0x06001F82 RID: 8066 RVA: 0x000DDA37 File Offset: 0x000DBC37
	private void Show(GameObject obj)
	{
		obj.SetActive(true);
	}

	// Token: 0x06001F83 RID: 8067 RVA: 0x000DDA40 File Offset: 0x000DBC40
	public void Show(BaseItem baseItem)
	{
		this.ResetUI();
		this.BaseItem = baseItem;
		this.Icon.sprite = baseItem.GetIconSprite();
		this.QualityImage.sprite = baseItem.GetQualitySprite();
		this.QualityUpImage.sprite = baseItem.GetQualityUpSprite();
		this.TypeName.SetText(StrTextJsonData.DataDict["ItemType" + baseItem.Type].ChinaText);
		this.Name.SetText(baseItem.GetName(), this._nameColordit[baseItem.GetImgQuality()]);
		this.QualityName.SetText(baseItem.GetQualityName(), this._qualityNameColordit[baseItem.GetImgQuality()]);
		if (baseItem is MiJiItem)
		{
			this.CreateSkillCostImg((MiJiItem)baseItem);
		}
		this.Show(this.Desc1Panel);
		this.Desc1.SetText(this.AddCiTiaoColor(baseItem.GetDesc1()));
		this.Desc1.SetText(this.AddDesc(baseItem, this.Desc1.text));
		this.CreateShuXing(baseItem);
		this.Show(this.Desc1Pane2);
		this.Desc2.SetText(baseItem.GetDesc2() ?? "");
		if (baseItem.MaxNum > 1)
		{
			this.Desc2.AddText(string.Format("\n已有:{0}", global::Tools.instance.getPlayer().getItemNum(baseItem.Id)));
		}
		if (baseItem.CanSale)
		{
			this.PricePanl.SetActive(true);
			this.PriceText.SetText(baseItem.GetPlayerPrice());
			this._group.padding.bottom = 50;
		}
		this.SetPosition();
		this.InitCiTiaoPanel();
		List<int> ciZhui = baseItem.GetCiZhui();
		if (ciZhui.Count > 0)
		{
			this.CiTiaoParent.gameObject.SetActive(true);
			foreach (int id in ciZhui)
			{
				this.AddCiTiaoMessage(id);
			}
		}
		if (this.BaseItem is MiJiItem)
		{
			MiJiItem miJiItem = (MiJiItem)this.BaseItem;
			if (miJiItem.MiJiType == MiJiType.技能 || miJiItem.MiJiType == MiJiType.功法)
			{
				this.AddLingWuMessage(this.BaseItem.Id);
				this.CiTiaoParent.gameObject.SetActive(true);
			}
		}
		else if (this.BaseItem is DanYaoItem)
		{
			this.CiTiaoParent.gameObject.SetActive(true);
			this.AddShuXingMessage();
		}
		else if (this.BaseItem is CaoYaoItem)
		{
			this.CiTiaoParent.gameObject.SetActive(true);
			Avatar player = global::Tools.instance.getPlayer();
			if (player.GetHasZhuYaoShuXin(this.BaseItem.Id, this.BaseItem.GetImgQuality()))
			{
				this.AddZhuYaoMessage();
			}
			if (player.GetHasFuYaoShuXin(this.BaseItem.Id, this.BaseItem.GetImgQuality()))
			{
				this.AddFuYaoMessage();
			}
		}
		this.UpdateSize();
	}

	// Token: 0x06001F84 RID: 8068 RVA: 0x000DDD54 File Offset: 0x000DBF54
	public void Show(BaseItem baseItem, Vector2 vector2)
	{
		this.ResetUI();
		this.BaseItem = baseItem;
		this.Icon.sprite = baseItem.GetIconSprite();
		this.QualityImage.sprite = baseItem.GetQualitySprite();
		this.QualityUpImage.sprite = baseItem.GetQualityUpSprite();
		this.TypeName.SetText(StrTextJsonData.DataDict["ItemType" + baseItem.Type].ChinaText);
		this.Name.SetText(baseItem.GetName(), this._nameColordit[baseItem.GetImgQuality()]);
		this.QualityName.SetText(baseItem.GetQualityName(), this._qualityNameColordit[baseItem.GetImgQuality()]);
		if (baseItem is MiJiItem)
		{
			this.CreateSkillCostImg((MiJiItem)baseItem);
		}
		this.Show(this.Desc1Panel);
		this.Desc1.SetText(this.AddCiTiaoColor(baseItem.GetDesc1()));
		this.Desc1.SetText(this.AddDesc(baseItem, this.Desc1.text));
		this.CreateShuXing(baseItem);
		this.Show(this.Desc1Pane2);
		this.Desc2.SetText(baseItem.GetDesc2() ?? "");
		if (baseItem.MaxNum > 1)
		{
			this.Desc2.AddText(string.Format("\n已有:{0}", global::Tools.instance.getPlayer().getItemNum(baseItem.Id)));
		}
		if (baseItem.CanSale)
		{
			this.PricePanl.SetActive(true);
			this.PriceText.SetText(baseItem.GetPlayerPrice());
			this._group.padding.bottom = 50;
		}
		this.Panel.transform.position = vector2;
		base.gameObject.SetActive(true);
		base.transform.SetAsLastSibling();
		foreach (RectTransform rectTransform in this._rectList)
		{
			LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
		}
		LayoutRebuilder.ForceRebuildLayoutImmediate(this._rectList[0]);
		this.InitCiTiaoPanel();
		List<int> ciZhui = baseItem.GetCiZhui();
		if (ciZhui.Count > 0)
		{
			this.CiTiaoParent.gameObject.SetActive(true);
			foreach (int id in ciZhui)
			{
				this.AddCiTiaoMessage(id);
			}
		}
		if (this.BaseItem is MiJiItem)
		{
			MiJiItem miJiItem = (MiJiItem)this.BaseItem;
			if (miJiItem.MiJiType == MiJiType.技能 || miJiItem.MiJiType == MiJiType.功法)
			{
				this.AddLingWuMessage(this.BaseItem.Id);
				this.CiTiaoParent.gameObject.SetActive(true);
			}
		}
		else if (this.BaseItem is DanYaoItem)
		{
			this.CiTiaoParent.gameObject.SetActive(true);
			this.AddShuXingMessage();
		}
		else if (this.BaseItem is CaoYaoItem)
		{
			this.CiTiaoParent.gameObject.SetActive(true);
			Avatar player = global::Tools.instance.getPlayer();
			if (player.GetHasZhuYaoShuXin(this.BaseItem.Id, this.BaseItem.GetImgQuality()))
			{
				this.AddZhuYaoMessage();
			}
			if (player.GetHasFuYaoShuXin(this.BaseItem.Id, this.BaseItem.GetImgQuality()))
			{
				this.AddFuYaoMessage();
			}
		}
		this.UpdateSize();
	}

	// Token: 0x06001F85 RID: 8069 RVA: 0x000DE0E4 File Offset: 0x000DC2E4
	public void Show(BaseItem baseItem, bool isPlayer)
	{
		this.Show(baseItem);
		if (baseItem.CanSale)
		{
			this.PricePanl.SetActive(true);
			this.PriceText.SetText(isPlayer ? baseItem.GetPlayerPrice() : baseItem.GetPrice());
			this._group.padding.bottom = 50;
		}
	}

	// Token: 0x06001F86 RID: 8070 RVA: 0x000DE140 File Offset: 0x000DC340
	public void Show(BaseItem baseItem, int price, bool isPlayer)
	{
		this.Show(baseItem);
		if (baseItem.CanSale)
		{
			this.PriceText.SetText(price);
			if (isPlayer)
			{
				if (price > baseItem.GetPlayerPrice())
				{
					this.PriceText.SetText(price, "#D55D21");
					return;
				}
				if (price < baseItem.GetPlayerPrice())
				{
					this.PriceText.SetText(price, "#75C0AE");
					return;
				}
			}
			else
			{
				if (price > baseItem.GetPrice())
				{
					this.PriceText.SetText(price, "#D55D21");
					return;
				}
				if (price < baseItem.GetPrice())
				{
					this.PriceText.SetText(price, "#75C0AE");
				}
			}
		}
	}

	// Token: 0x06001F87 RID: 8071 RVA: 0x000DE1F4 File Offset: 0x000DC3F4
	public void Show(BaseItem baseItem, int price, string color = "无")
	{
		this.Show(baseItem);
		if (baseItem.CanSale)
		{
			if (color != "无")
			{
				this.PriceText.SetText(price, color);
				return;
			}
			this.PriceText.SetText(price);
		}
	}

	// Token: 0x06001F88 RID: 8072 RVA: 0x000DE244 File Offset: 0x000DC444
	public void Show(BaseSkill baseSkill)
	{
		this.ResetUI();
		this.BaseSkill = baseSkill;
		this.Icon.sprite = baseSkill.GetIconSprite();
		this.QualityImage.sprite = baseSkill.GetQualitySprite();
		this.QualityUpImage.sprite = baseSkill.GetQualityUpSprite();
		this.TypeName.SetText(baseSkill.GetTypeName());
		this.Name.SetText(baseSkill.Name, this._nameColordit[baseSkill.GetImgQuality()]);
		this.QualityName.SetText(baseSkill.GetQualityName(), this._qualityNameColordit[baseSkill.GetImgQuality()]);
		if (baseSkill is ActiveSkill)
		{
			this.CreateSkillCostImg((ActiveSkill)baseSkill);
		}
		this.Show(this.Desc1Panel);
		this.Desc1.SetText(this.AddCiTiaoColor(baseSkill.GetDesc1()));
		this.CreateShuXing(baseSkill);
		this.Show(this.Desc1Pane2);
		this.Desc2.SetText(baseSkill.GetDesc2());
		this.SetPosition();
		this.InitCiTiaoPanel();
		List<int> ciZhuiList = baseSkill.GetCiZhuiList();
		if (ciZhuiList.Count > 0)
		{
			this.CiTiaoParent.gameObject.SetActive(true);
			foreach (int id in ciZhuiList)
			{
				this.AddCiTiaoMessage(id);
			}
		}
		this.UpdateSize();
	}

	// Token: 0x06001F89 RID: 8073 RVA: 0x000DE3B8 File Offset: 0x000DC5B8
	private void CreateSkillCostImg(ActiveSkill activeSkill)
	{
		List<SkillCost> skillCost = activeSkill.GetSkillCost();
		if (skillCost.Count < 1)
		{
			return;
		}
		this.Show(this.SkillCost);
		Transform parent = this.SkillCostImg.transform.parent;
		global::Tools.ClearChild(parent);
		foreach (SkillCost skillCost2 in skillCost)
		{
			Image component = this.SkillCostImg.Inst(parent).GetComponent<Image>();
			component.GetComponentInChildren<Text>().SetText(string.Format("x<size=26>{0}</size>", skillCost2.Num));
			component.sprite = this._costIconDict[skillCost2.Id.ToString()];
			component.gameObject.SetActive(true);
		}
	}

	// Token: 0x06001F8A RID: 8074 RVA: 0x000DE48C File Offset: 0x000DC68C
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
		this.Show(this.SkillCost);
		Transform parent = this.SkillCostImg.transform.parent;
		global::Tools.ClearChild(parent);
		foreach (SkillCost skillCost in miJiCost)
		{
			Image component = this.SkillCostImg.Inst(parent).GetComponent<Image>();
			component.GetComponentInChildren<Text>().SetText(string.Format("x<size=26>{0}</size>", skillCost.Num));
			component.sprite = this._costIconDict[skillCost.Id.ToString()];
			component.gameObject.SetActive(true);
		}
	}

	// Token: 0x06001F8B RID: 8075 RVA: 0x000DE56C File Offset: 0x000DC76C
	private void CreateShuXing(BaseSkill baseSkill)
	{
		if (baseSkill is PassiveSkill)
		{
			PassiveSkill passiveSkill = (PassiveSkill)baseSkill;
			this.ShuXingText.SetText("【进度】", "#c09e5c");
			this.ShuXingText.AddText("第" + global::Tools.getStr("cengshu" + passiveSkill.Level) + "   ");
			this.ShuXingText.AddText("【修炼速度】", "#c09e5c");
			this.ShuXingText.AddText(passiveSkill.GetSpeed());
			this.Show(this.ShuXingPanel);
			return;
		}
		this.ShuXingPanel.SetActive(false);
	}

	// Token: 0x06001F8C RID: 8076 RVA: 0x000DE618 File Offset: 0x000DC818
	private void CreateShuXing(BaseItem baseItem)
	{
		if (baseItem is EquipItem)
		{
			EquipItem equipItem = (EquipItem)baseItem;
			EquipType equipType = equipItem.EquipType;
			if (equipType != EquipType.武器)
			{
				if (equipType - EquipType.灵舟 > 1)
				{
					return;
				}
				this.ShuXingText.SetText("【耐久】", "#c09e5c");
				this.ShuXingText.AddText(string.Format("{0}/{1}", equipItem.GetCurNaiJiu(), equipItem.GetMaxNaiJiu()));
			}
			else
			{
				this.ShuXingText.SetText("【冷却】", "#c09e5c");
				this.ShuXingText.AddText(string.Format("{0}回合   ", equipItem.GetCd()));
				this.ShuXingText.AddText("【属性】", "#c09e5c");
				this.ShuXingText.AddText(equipItem.GetShuXing());
			}
		}
		else if (baseItem is DanYaoItem && baseItem.Type == 5)
		{
			DanYaoItem danYaoItem = (DanYaoItem)baseItem;
			this.ShuXingText.SetText("【耐药】", "#c09e5c");
			this.ShuXingText.AddText(string.Format("{0}/{1}   ", danYaoItem.GetHasUse(), danYaoItem.GetMaxUseNum()));
			this.ShuXingText.AddText("【丹毒】", "#dd61ff");
			this.ShuXingText.AddText(danYaoItem.DanDu);
		}
		else if (baseItem is CaoYaoItem)
		{
			CaoYaoItem caoYaoItem = (CaoYaoItem)baseItem;
			this.ShuXingText.SetText("【主药】", "#c09e5c");
			this.ShuXingText.AddText(caoYaoItem.GetZhuYao() ?? "");
			this.ShuXingText.AddText("【辅药】", "#c09e5c");
			this.ShuXingText.AddText(caoYaoItem.GetFuYao() + "\n");
			this.ShuXingText.AddText("【药引】", "#c09e5c");
			this.ShuXingText.AddText(caoYaoItem.GetYaoYin() ?? "");
		}
		else
		{
			if (!(baseItem is CaiLiaoItem))
			{
				this.ShuXingPanel.SetActive(false);
				return;
			}
			CaiLiaoItem caiLiaoItem = (CaiLiaoItem)baseItem;
			this.ShuXingText.SetText("【种类】", "#c09e5c");
			this.ShuXingText.AddText(caiLiaoItem.GetZhongLei() + "  ");
			this.ShuXingText.AddText("【属性】", "#c09e5c");
			this.ShuXingText.AddText(caiLiaoItem.GetShuXing() ?? "");
		}
		this.Show(this.ShuXingPanel);
	}

	// Token: 0x06001F8D RID: 8077 RVA: 0x000DE8AA File Offset: 0x000DCAAA
	private string AddCiTiaoColor(string desc1)
	{
		desc1 = desc1.ToCN();
		desc1 = desc1.Replace("【", "<color=#42e395>【");
		desc1 = desc1.Replace("】", "】</color>");
		return desc1;
	}

	// Token: 0x06001F8E RID: 8078 RVA: 0x000DE8DC File Offset: 0x000DCADC
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

	// Token: 0x06001F8F RID: 8079 RVA: 0x000DE944 File Offset: 0x000DCB44
	private void SetPosition()
	{
		this.PCSetPosition();
		base.gameObject.SetActive(true);
		base.transform.SetAsLastSibling();
		foreach (RectTransform rectTransform in this._rectList)
		{
			LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
		}
		LayoutRebuilder.ForceRebuildLayoutImmediate(this._rectList[0]);
	}

	// Token: 0x06001F90 RID: 8080 RVA: 0x000DE9C4 File Offset: 0x000DCBC4
	private void PCSetPosition()
	{
		if (this._rectTransform == null)
		{
			this._rectTransform = this.Panel.GetComponent<RectTransform>();
		}
		Rect rect = this._rectTransform.rect;
		float num = Input.mousePosition.x;
		float num2 = (float)Screen.height * 4f / 5f;
		if (num > (float)Screen.width / 2f)
		{
			num = ((float)Screen.width - rect.width) / 2f;
			this._direction = ToolTipsMag.Direction.左;
		}
		else
		{
			num = ((float)Screen.width + rect.width) / 2f;
			this._direction = ToolTipsMag.Direction.右;
		}
		this.Panel.transform.position = NewUICanvas.Inst.Camera.ScreenToWorldPoint(new Vector2(num, num2));
	}

	// Token: 0x06001F91 RID: 8081 RVA: 0x000DEA90 File Offset: 0x000DCC90
	public float GetMouseY()
	{
		if (this._rectTransform == null)
		{
			this._rectTransform = this.Panel.GetComponent<RectTransform>();
		}
		Rect rect = this._rectTransform.rect;
		float num = Input.mousePosition.x;
		float num2 = (float)Screen.height * 4f / 5f;
		if (num > (float)Screen.width / 2f)
		{
			num = ((float)Screen.width - rect.width) / 2f;
			this._direction = ToolTipsMag.Direction.左;
		}
		else
		{
			num = ((float)Screen.width + rect.width) / 2f;
			this._direction = ToolTipsMag.Direction.右;
		}
		return NewUICanvas.Inst.Camera.ScreenToWorldPoint(new Vector2(num, num2)).y;
	}

	// Token: 0x06001F92 RID: 8082 RVA: 0x000DEB50 File Offset: 0x000DCD50
	private void AddCiTiaoMessage(int id)
	{
		TuJianChunWenBen tuJianChunWenBen = TuJianChunWenBen.DataDict[id];
		Transform parent = (this._direction == ToolTipsMag.Direction.右) ? this.RightPanel.transform : this.LeftPanel.transform;
		GameObject gameObject = this.CiTiao.Inst(parent);
		gameObject.SetActive(true);
		gameObject.GetComponentInChildren<SymbolText>().text = "#c42e395【" + tuJianChunWenBen.name2 + "】#n" + tuJianChunWenBen.descr;
	}

	// Token: 0x06001F93 RID: 8083 RVA: 0x000DEBC4 File Offset: 0x000DCDC4
	private void AddShuXingMessage()
	{
		Avatar player = global::Tools.instance.getPlayer();
		this.ShengMingValue.SetText(player.HP);
		this.ShengMingValue.AddText(string.Format("/{0}", player.HP_Max), "#acab74");
		this.XiuWeiValue.SetText(player.exp);
		this.XiuWeiValue.AddText(string.Format("/{0}", LevelUpDataJsonData.DataDict[(int)player.level].MaxExp), "#acab74");
		this.DanDuValue.SetText(player.Dandu);
		this.DanDuValue.AddText("/120", "#acab74");
		this.XingJingValue.SetText(player.xinjin);
		this.XingJingValue.AddText(string.Format("/{0}", XinJinJsonData.DataDict[player.GetXinJingLevel()].Max), "#acab74");
		this.BottomPanel.SetActive(true);
		this.ShuXingCiTiao.SetActive(true);
		this.ShuXingCiTiao.transform.SetParent(this.BottomPanel.transform);
	}

	// Token: 0x06001F94 RID: 8084 RVA: 0x000DED0C File Offset: 0x000DCF0C
	private void AddLingWuMessage(int bookItemID)
	{
		int day = global::Tools.CalcLingWuTime(bookItemID);
		this.LingWuTimeText.SetText("领悟时间:", "d3b068");
		this.LingWuTimeText.AddText(global::Tools.getStr("xiaohaoshijian").Replace("{Y}", string.Concat(global::Tools.DayToYear(day))).Replace("{M}", string.Concat(global::Tools.DayToMonth(day))).Replace("{D}", string.Concat(global::Tools.DayToDay(day))).Replace("消耗时间：", ""));
		this.LingWuTiaoJianText.SetText("领悟条件:", "#d3b068");
		string text = "";
		int num = 0;
		string text2 = "";
		foreach (int key in this.BaseItem.WuDaoDict.Keys)
		{
			text2 = text2 + WuDaoAllTypeJson.DataDict[key].name + ",";
			if (!WuDaoJinJieJson.DataDict[this.BaseItem.WuDaoDict[key]].Text.Contains("一窍不通"))
			{
				num++;
				text = string.Concat(new string[]
				{
					text,
					"对",
					WuDaoAllTypeJson.DataDict[key].name1,
					"的领悟达到",
					WuDaoJinJieJson.DataDict[this.BaseItem.WuDaoDict[key]].Text,
					","
				});
			}
		}
		if (num == 0)
		{
			text += "无,";
		}
		text2 = text2.TrimEnd(",".ToCharArray());
		text = text + "领悟后提升" + text2 + "大道感悟";
		this.LingWuTiaoJianText.AddText(text);
		this.BottomPanel.SetActive(true);
		this.LingWuTiaoJian.SetActive(true);
		this.LingWuTiaoJian.transform.SetParent(this.BottomPanel.transform);
	}

	// Token: 0x06001F95 RID: 8085 RVA: 0x000DEF3C File Offset: 0x000DD13C
	private void AddZhuYaoMessage()
	{
		CaoYaoItem caoYaoItem = (CaoYaoItem)this.BaseItem;
		if (caoYaoItem.ZhuYao == 0)
		{
			return;
		}
		LianDanItemLeiXin lianDanItemLeiXin = LianDanItemLeiXin.DataDict[caoYaoItem.ZhuYao];
		Transform parent = (this._direction == ToolTipsMag.Direction.右) ? this.RightPanel.transform : this.LeftPanel.transform;
		GameObject gameObject = this.CiTiao.Inst(parent);
		gameObject.SetActive(true);
		gameObject.GetComponentInChildren<SymbolText>().text = "#c42e395【" + lianDanItemLeiXin.name + "】#n" + lianDanItemLeiXin.desc;
	}

	// Token: 0x06001F96 RID: 8086 RVA: 0x000DEFCC File Offset: 0x000DD1CC
	private void AddFuYaoMessage()
	{
		CaoYaoItem caoYaoItem = (CaoYaoItem)this.BaseItem;
		if (caoYaoItem.FuYao == 0)
		{
			return;
		}
		LianDanItemLeiXin lianDanItemLeiXin = LianDanItemLeiXin.DataDict[caoYaoItem.FuYao];
		Transform parent = (this._direction == ToolTipsMag.Direction.右) ? this.RightPanel.transform : this.LeftPanel.transform;
		GameObject gameObject = this.CiTiao.Inst(parent);
		gameObject.SetActive(true);
		gameObject.GetComponentInChildren<SymbolText>().text = "#c42e395【" + lianDanItemLeiXin.name + "】#n" + lianDanItemLeiXin.desc;
	}

	// Token: 0x06001F97 RID: 8087 RVA: 0x000B5E62 File Offset: 0x000B4062
	public void Close()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x0400197A RID: 6522
	public static ToolTipsMag Inst;

	// Token: 0x0400197B RID: 6523
	private Dictionary<int, string> _nameColordit;

	// Token: 0x0400197C RID: 6524
	private Dictionary<int, string> _qualityNameColordit;

	// Token: 0x0400197D RID: 6525
	private Dictionary<string, Sprite> _costIconDict;

	// Token: 0x0400197E RID: 6526
	private List<RectTransform> _rectList;

	// Token: 0x0400197F RID: 6527
	private RectTransform _rectTransform;

	// Token: 0x04001980 RID: 6528
	private ToolTipsMag.Direction _direction;

	// Token: 0x04001981 RID: 6529
	private int _row;

	// Token: 0x04001982 RID: 6530
	[SerializeField]
	private VerticalLayoutGroup _group;

	// Token: 0x04001983 RID: 6531
	public BaseItem BaseItem;

	// Token: 0x04001984 RID: 6532
	public BaseSkill BaseSkill;

	// Token: 0x04001985 RID: 6533
	public Image Icon;

	// Token: 0x04001986 RID: 6534
	public Image QualityImage;

	// Token: 0x04001987 RID: 6535
	public Image QualityUpImage;

	// Token: 0x04001988 RID: 6536
	public GameObject SkillCostImg;

	// Token: 0x04001989 RID: 6537
	public Text QualityName;

	// Token: 0x0400198A RID: 6538
	public Text TypeName;

	// Token: 0x0400198B RID: 6539
	public Text Name;

	// Token: 0x0400198C RID: 6540
	public GameObject Panel;

	// Token: 0x0400198D RID: 6541
	public GameObject SkillCost;

	// Token: 0x0400198E RID: 6542
	public GameObject Desc1Panel;

	// Token: 0x0400198F RID: 6543
	public Text Desc1;

	// Token: 0x04001990 RID: 6544
	public GameObject ShuXingPanel;

	// Token: 0x04001991 RID: 6545
	public Text ShuXingText;

	// Token: 0x04001992 RID: 6546
	public GameObject Desc1Pane2;

	// Token: 0x04001993 RID: 6547
	public Text Desc2;

	// Token: 0x04001994 RID: 6548
	public GameObject PricePanl;

	// Token: 0x04001995 RID: 6549
	public Text PriceText;

	// Token: 0x04001996 RID: 6550
	public GameObject ShuXingCiTiao;

	// Token: 0x04001997 RID: 6551
	public GameObject LingWuTiaoJian;

	// Token: 0x04001998 RID: 6552
	public GameObject CiTiao;

	// Token: 0x04001999 RID: 6553
	public Transform CiTiaoParent;

	// Token: 0x0400199A RID: 6554
	public float JianGe = 10f;

	// Token: 0x0400199B RID: 6555
	public Text ShengMingValue;

	// Token: 0x0400199C RID: 6556
	public Text XiuWeiValue;

	// Token: 0x0400199D RID: 6557
	public Text DanDuValue;

	// Token: 0x0400199E RID: 6558
	public Text XingJingValue;

	// Token: 0x0400199F RID: 6559
	public Text LingWuTimeText;

	// Token: 0x040019A0 RID: 6560
	public Text LingWuTiaoJianText;

	// Token: 0x040019A1 RID: 6561
	public GameObject BottomPanel;

	// Token: 0x040019A2 RID: 6562
	public List<RectTransform> BottomRectList;

	// Token: 0x040019A3 RID: 6563
	public RectTransform LeftPanel;

	// Token: 0x040019A4 RID: 6564
	public RectTransform RightPanel;

	// Token: 0x02001376 RID: 4982
	public enum Direction
	{
		// Token: 0x0400688E RID: 26766
		左 = 1,
		// Token: 0x0400688F RID: 26767
		右
	}
}
