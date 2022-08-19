using System;
using System.Collections.Generic;
using DG.Tweening;
using GUIPackage;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using YSGame.TuJian;

// Token: 0x020002C2 RID: 706
public class UIHeadPanel : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x060018B8 RID: 6328 RVA: 0x000B1826 File Offset: 0x000AFA26
	private void Awake()
	{
		UIHeadPanel.Inst = this;
	}

	// Token: 0x060018B9 RID: 6329 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x060018BA RID: 6330 RVA: 0x000B182E File Offset: 0x000AFA2E
	private void Update()
	{
		if (PanelMamager.inst != null && PanelMamager.inst.UISceneGameObject != null)
		{
			this.CheckHongDian(false);
			return;
		}
		this.ScaleObj.SetActive(false);
	}

	// Token: 0x060018BB RID: 6331 RVA: 0x000B1864 File Offset: 0x000AFA64
	public void CheckHongDian(bool checkChuanYin = false)
	{
		Avatar player = Tools.instance.getPlayer();
		if (player == null)
		{
			return;
		}
		this.RefreshTieJianRedPoint();
		this.TuJianPoint.SetActive(!TuJianManager.Inst.IsUnlockedHongDian(1));
		if (checkChuanYin)
		{
			this.ChuanYinFuPoint.SetActive(player.emailDateMag.newEmailDictionary.Count > 0);
		}
	}

	// Token: 0x060018BC RID: 6332 RVA: 0x000B18C0 File Offset: 0x000AFAC0
	public void RefreshUI()
	{
		Avatar player = Tools.instance.getPlayer();
		if (player == null)
		{
			return;
		}
		this.NameText.text = player.name;
		this.HPSlider.value = (float)player.HP / (float)player.HP_Max;
		this.HPText.text = string.Format("{0}/{1}", player.HP, player.HP_Max);
		float n = jsonData.instance.LevelUpDataJsonData[player.level.ToString()]["MaxExp"].n;
		this.ExpSlider.value = player.exp / n;
		this.ExpText.text = string.Format("{0}/{1}", player.exp, (int)n);
		this.LevelImage.sprite = this.LevelSprite[(int)(player.level - 1)];
		bool flag = player.TianJie.HasField("ShowTianJieCD") && player.TianJie["ShowTianJieCD"].b;
		if (this.TianJieObj.activeSelf)
		{
			if (!flag)
			{
				this.TianJieObj.SetActive(false);
			}
			else
			{
				this.TianJieTime.text = player.TianJie["ShengYuTime"].Str;
			}
		}
		else if (flag)
		{
			this.TianJieObj.SetActive(true);
		}
		bool flag2 = jsonData.instance.saveState == 1;
		if (this.CunDangObj.activeSelf && this.saving)
		{
			if (!flag2)
			{
				this.saving = false;
				DOTween.To(() => this.CunDangAlpha.alpha, delegate(float x)
				{
					this.CunDangAlpha.alpha = x;
				}, 0f, 0.5f).onComplete = delegate()
				{
					this.CunDangObj.SetActive(false);
				};
				return;
			}
		}
		else if (flag2)
		{
			this.saving = true;
			this.CunDangAlpha.alpha = 1f;
			this.CunDangObj.SetActive(true);
		}
	}

	// Token: 0x060018BD RID: 6333 RVA: 0x000B1ABC File Offset: 0x000AFCBC
	public void RefreshTieJianRedPoint()
	{
		JianLingManager jianLingManager = PlayerEx.Player.jianLingManager;
		bool active = false;
		int jiYiHuiFuDu = jianLingManager.GetJiYiHuiFuDu();
		foreach (JianLingQingJiao jianLingQingJiao in JianLingQingJiao.DataList)
		{
			if (jiYiHuiFuDu < jianLingQingJiao.JiYi)
			{
				break;
			}
			bool flag = false;
			if (jianLingQingJiao.SkillID > 0)
			{
				flag = PlayerEx.HasSkill(jianLingQingJiao.SkillID);
			}
			else if (jianLingQingJiao.StaticSkillID > 0)
			{
				flag = PlayerEx.HasStaticSkill(jianLingQingJiao.StaticSkillID);
			}
			if (!flag)
			{
				active = true;
				break;
			}
		}
		this.TieJianRedPoint.SetActive(active);
	}

	// Token: 0x060018BE RID: 6334 RVA: 0x000B1B68 File Offset: 0x000AFD68
	public bool BtnCanClick()
	{
		Tools.instance.canClick(false, true);
		return CanClickManager.Inst.ResultCount == 0 || (CanClickManager.Inst.ResultCount == 1 && CanClickManager.Inst.ResultCache[3]);
	}

	// Token: 0x060018BF RID: 6335 RVA: 0x000B1BA3 File Offset: 0x000AFDA3
	public void OnTieJianBtnClick()
	{
		if (this.BtnCanClick())
		{
			UIJianLingPanel.OpenPanel();
		}
	}

	// Token: 0x060018C0 RID: 6336 RVA: 0x000B1BB2 File Offset: 0x000AFDB2
	public void OnChuanYinFuBtnClick()
	{
		if (this.BtnCanClick())
		{
			PanelMamager.inst.OpenPanel(PanelMamager.PanelType.传音符, 1);
		}
	}

	// Token: 0x060018C1 RID: 6337 RVA: 0x000B1BC8 File Offset: 0x000AFDC8
	public void OnTuJianBtnClick()
	{
		if (this.BtnCanClick())
		{
			PanelMamager.inst.OpenPanel(PanelMamager.PanelType.图鉴, 1);
		}
	}

	// Token: 0x060018C2 RID: 6338 RVA: 0x000B1BDE File Offset: 0x000AFDDE
	public void OnHeadBtnClick()
	{
		base.Invoke("OpenTab", 0.1f);
	}

	// Token: 0x060018C3 RID: 6339 RVA: 0x000B1BF0 File Offset: 0x000AFDF0
	private void OpenTab()
	{
		this.IsMouseInUI = false;
		GameObject.Find("UI Root (2D)").GetComponent<Singleton>().ClickTab();
	}

	// Token: 0x060018C4 RID: 6340 RVA: 0x000B1C0D File Offset: 0x000AFE0D
	public void OnMapBtnClick()
	{
		if (this.BtnCanClick())
		{
			UIMapPanel.Inst.OpenDefaultMap();
		}
	}

	// Token: 0x060018C5 RID: 6341 RVA: 0x000B1C21 File Offset: 0x000AFE21
	public void OnPointerEnter(PointerEventData eventData)
	{
		this.IsMouseInUI = true;
	}

	// Token: 0x060018C6 RID: 6342 RVA: 0x000B1C2A File Offset: 0x000AFE2A
	public void OnPointerExit(PointerEventData eventData)
	{
		this.IsMouseInUI = false;
	}

	// Token: 0x040013CE RID: 5070
	public static UIHeadPanel Inst;

	// Token: 0x040013CF RID: 5071
	public GameObject ScaleObj;

	// Token: 0x040013D0 RID: 5072
	public Text NameText;

	// Token: 0x040013D1 RID: 5073
	public Text HPText;

	// Token: 0x040013D2 RID: 5074
	public Text ExpText;

	// Token: 0x040013D3 RID: 5075
	public Slider HPSlider;

	// Token: 0x040013D4 RID: 5076
	public Slider ExpSlider;

	// Token: 0x040013D5 RID: 5077
	public PlayerSetRandomFace Face;

	// Token: 0x040013D6 RID: 5078
	public Image LevelImage;

	// Token: 0x040013D7 RID: 5079
	public GameObject TieJianRedPoint;

	// Token: 0x040013D8 RID: 5080
	public GameObject ChuanYinFuPoint;

	// Token: 0x040013D9 RID: 5081
	public GameObject TuJianPoint;

	// Token: 0x040013DA RID: 5082
	public GameObject TianJieObj;

	// Token: 0x040013DB RID: 5083
	public Text TianJieTime;

	// Token: 0x040013DC RID: 5084
	public GameObject CunDangObj;

	// Token: 0x040013DD RID: 5085
	public CanvasGroup CunDangAlpha;

	// Token: 0x040013DE RID: 5086
	[HideInInspector]
	public bool IsMouseInUI;

	// Token: 0x040013DF RID: 5087
	private bool saving;

	// Token: 0x040013E0 RID: 5088
	public List<Sprite> LevelSprite;
}
