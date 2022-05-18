using System;
using System.Collections.Generic;
using Bag;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;
using WXB;

// Token: 0x02000559 RID: 1369
public class ToolTipsMag : MonoBehaviour
{
	// Token: 0x060022ED RID: 8941 RVA: 0x0012020C File Offset: 0x0011E40C
	public void UpdateBottom()
	{
		foreach (RectTransform rectTransform in this.BottomRectList)
		{
			LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
		}
		LayoutRebuilder.ForceRebuildLayoutImmediate(this._rectList[0]);
	}

	// Token: 0x060022EE RID: 8942 RVA: 0x00120270 File Offset: 0x0011E470
	public void UpdateLeftPanel()
	{
		for (int i = 0; i < this.LeftPanel.gameObject.transform.childCount; i++)
		{
			LayoutRebuilder.ForceRebuildLayoutImmediate(this.LeftPanel.GetChild(i).GetComponent<RectTransform>());
		}
		LayoutRebuilder.ForceRebuildLayoutImmediate(this.LeftPanel);
	}

	// Token: 0x060022EF RID: 8943 RVA: 0x001202C0 File Offset: 0x0011E4C0
	public void UpdateRightPanel()
	{
		for (int i = 0; i < this.RightPanel.gameObject.transform.childCount; i++)
		{
			LayoutRebuilder.ForceRebuildLayoutImmediate(this.RightPanel.GetChild(i).GetComponent<RectTransform>());
		}
		LayoutRebuilder.ForceRebuildLayoutImmediate(this.RightPanel);
	}

	// Token: 0x060022F0 RID: 8944 RVA: 0x00120310 File Offset: 0x0011E510
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

	// Token: 0x060022F1 RID: 8945 RVA: 0x0001C7B9 File Offset: 0x0001A9B9
	public void UpdateSize()
	{
		this.UpdateRightPanel();
		this.UpdateLeftPanel();
		this.UpdateBottom();
	}

	// Token: 0x060022F2 RID: 8946 RVA: 0x0001C7CD File Offset: 0x0001A9CD
	private void Update()
	{
		if (UToolTip.IsShouldCloseInput())
		{
			this.Close();
		}
	}

	// Token: 0x060022F3 RID: 8947 RVA: 0x0012036C File Offset: 0x0011E56C
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

	// Token: 0x060022F4 RID: 8948 RVA: 0x0012053C File Offset: 0x0011E73C
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

	// Token: 0x060022F5 RID: 8949 RVA: 0x0001C7DC File Offset: 0x0001A9DC
	private void Show(GameObject obj)
	{
		obj.SetActive(true);
	}

	// Token: 0x060022F6 RID: 8950 RVA: 0x001205F8 File Offset: 0x0011E7F8
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

	// Token: 0x060022F7 RID: 8951 RVA: 0x0012090C File Offset: 0x0011EB0C
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

	// Token: 0x060022F8 RID: 8952 RVA: 0x00120C9C File Offset: 0x0011EE9C
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

	// Token: 0x060022F9 RID: 8953 RVA: 0x00120CF8 File Offset: 0x0011EEF8
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

	// Token: 0x060022FA RID: 8954 RVA: 0x00120DAC File Offset: 0x0011EFAC
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

	// Token: 0x060022FB RID: 8955 RVA: 0x00120DFC File Offset: 0x0011EFFC
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

	// Token: 0x060022FC RID: 8956 RVA: 0x00120F70 File Offset: 0x0011F170
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

	// Token: 0x060022FD RID: 8957 RVA: 0x00121044 File Offset: 0x0011F244
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

	// Token: 0x060022FE RID: 8958 RVA: 0x00121124 File Offset: 0x0011F324
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

	// Token: 0x060022FF RID: 8959 RVA: 0x001211D0 File Offset: 0x0011F3D0
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

	// Token: 0x06002300 RID: 8960 RVA: 0x0001C7E5 File Offset: 0x0001A9E5
	private string AddCiTiaoColor(string desc1)
	{
		desc1 = desc1.ToCN();
		desc1 = desc1.Replace("【", "<color=#42e395>【");
		desc1 = desc1.Replace("】", "】</color>");
		return desc1;
	}

	// Token: 0x06002301 RID: 8961 RVA: 0x00121464 File Offset: 0x0011F664
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

	// Token: 0x06002302 RID: 8962 RVA: 0x001214CC File Offset: 0x0011F6CC
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

	// Token: 0x06002303 RID: 8963 RVA: 0x0012154C File Offset: 0x0011F74C
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

	// Token: 0x06002304 RID: 8964 RVA: 0x00121618 File Offset: 0x0011F818
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

	// Token: 0x06002305 RID: 8965 RVA: 0x001216D8 File Offset: 0x0011F8D8
	private void AddCiTiaoMessage(int id)
	{
		TuJianChunWenBen tuJianChunWenBen = TuJianChunWenBen.DataDict[id];
		Transform parent = (this._direction == ToolTipsMag.Direction.右) ? this.RightPanel.transform : this.LeftPanel.transform;
		GameObject gameObject = this.CiTiao.Inst(parent);
		gameObject.SetActive(true);
		gameObject.GetComponentInChildren<SymbolText>().text = "#c42e395【" + tuJianChunWenBen.name2 + "】#n" + tuJianChunWenBen.descr;
	}

	// Token: 0x06002306 RID: 8966 RVA: 0x0012174C File Offset: 0x0011F94C
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

	// Token: 0x06002307 RID: 8967 RVA: 0x00121894 File Offset: 0x0011FA94
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

	// Token: 0x06002308 RID: 8968 RVA: 0x00121AC4 File Offset: 0x0011FCC4
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

	// Token: 0x06002309 RID: 8969 RVA: 0x00121B54 File Offset: 0x0011FD54
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

	// Token: 0x0600230A RID: 8970 RVA: 0x00017C2D File Offset: 0x00015E2D
	public void Close()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001DF5 RID: 7669
	public static ToolTipsMag Inst;

	// Token: 0x04001DF6 RID: 7670
	private Dictionary<int, string> _nameColordit;

	// Token: 0x04001DF7 RID: 7671
	private Dictionary<int, string> _qualityNameColordit;

	// Token: 0x04001DF8 RID: 7672
	private Dictionary<string, Sprite> _costIconDict;

	// Token: 0x04001DF9 RID: 7673
	private List<RectTransform> _rectList;

	// Token: 0x04001DFA RID: 7674
	private RectTransform _rectTransform;

	// Token: 0x04001DFB RID: 7675
	private ToolTipsMag.Direction _direction;

	// Token: 0x04001DFC RID: 7676
	private int _row;

	// Token: 0x04001DFD RID: 7677
	[SerializeField]
	private VerticalLayoutGroup _group;

	// Token: 0x04001DFE RID: 7678
	public BaseItem BaseItem;

	// Token: 0x04001DFF RID: 7679
	public BaseSkill BaseSkill;

	// Token: 0x04001E00 RID: 7680
	public Image Icon;

	// Token: 0x04001E01 RID: 7681
	public Image QualityImage;

	// Token: 0x04001E02 RID: 7682
	public Image QualityUpImage;

	// Token: 0x04001E03 RID: 7683
	public GameObject SkillCostImg;

	// Token: 0x04001E04 RID: 7684
	public Text QualityName;

	// Token: 0x04001E05 RID: 7685
	public Text TypeName;

	// Token: 0x04001E06 RID: 7686
	public Text Name;

	// Token: 0x04001E07 RID: 7687
	public GameObject Panel;

	// Token: 0x04001E08 RID: 7688
	public GameObject SkillCost;

	// Token: 0x04001E09 RID: 7689
	public GameObject Desc1Panel;

	// Token: 0x04001E0A RID: 7690
	public Text Desc1;

	// Token: 0x04001E0B RID: 7691
	public GameObject ShuXingPanel;

	// Token: 0x04001E0C RID: 7692
	public Text ShuXingText;

	// Token: 0x04001E0D RID: 7693
	public GameObject Desc1Pane2;

	// Token: 0x04001E0E RID: 7694
	public Text Desc2;

	// Token: 0x04001E0F RID: 7695
	public GameObject PricePanl;

	// Token: 0x04001E10 RID: 7696
	public Text PriceText;

	// Token: 0x04001E11 RID: 7697
	public GameObject ShuXingCiTiao;

	// Token: 0x04001E12 RID: 7698
	public GameObject LingWuTiaoJian;

	// Token: 0x04001E13 RID: 7699
	public GameObject CiTiao;

	// Token: 0x04001E14 RID: 7700
	public Transform CiTiaoParent;

	// Token: 0x04001E15 RID: 7701
	public float JianGe = 10f;

	// Token: 0x04001E16 RID: 7702
	public Text ShengMingValue;

	// Token: 0x04001E17 RID: 7703
	public Text XiuWeiValue;

	// Token: 0x04001E18 RID: 7704
	public Text DanDuValue;

	// Token: 0x04001E19 RID: 7705
	public Text XingJingValue;

	// Token: 0x04001E1A RID: 7706
	public Text LingWuTimeText;

	// Token: 0x04001E1B RID: 7707
	public Text LingWuTiaoJianText;

	// Token: 0x04001E1C RID: 7708
	public GameObject BottomPanel;

	// Token: 0x04001E1D RID: 7709
	public List<RectTransform> BottomRectList;

	// Token: 0x04001E1E RID: 7710
	public RectTransform LeftPanel;

	// Token: 0x04001E1F RID: 7711
	public RectTransform RightPanel;

	// Token: 0x0200055A RID: 1370
	public enum Direction
	{
		// Token: 0x04001E21 RID: 7713
		左 = 1,
		// Token: 0x04001E22 RID: 7714
		右
	}
}
