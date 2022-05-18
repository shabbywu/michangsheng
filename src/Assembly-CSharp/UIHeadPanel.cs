using System;
using System.Collections.Generic;
using DG.Tweening;
using GUIPackage;
using KBEngine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using YSGame.TuJian;

// Token: 0x02000402 RID: 1026
public class UIHeadPanel : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x06001BB3 RID: 7091 RVA: 0x0001738F File Offset: 0x0001558F
	private void Awake()
	{
		UIHeadPanel.Inst = this;
	}

	// Token: 0x06001BB4 RID: 7092 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x06001BB5 RID: 7093 RVA: 0x00017397 File Offset: 0x00015597
	private void Update()
	{
		if (PanelMamager.inst != null && PanelMamager.inst.UISceneGameObject != null)
		{
			this.CheckHongDian(false);
			return;
		}
		this.ScaleObj.SetActive(false);
	}

	// Token: 0x06001BB6 RID: 7094 RVA: 0x000F7E54 File Offset: 0x000F6054
	public void CheckHongDian(bool checkChuanYin = false)
	{
		Avatar player = Tools.instance.getPlayer();
		if (player == null)
		{
			return;
		}
		this.TieJianRedPoint.SetActive(player.TieJianHongDianList.Count > 0);
		this.TuJianPoint.SetActive(!TuJianManager.Inst.IsUnlockedHongDian(1));
		if (checkChuanYin)
		{
			this.ChuanYinFuPoint.SetActive(player.emailDateMag.newEmailDictionary.Count > 0);
		}
	}

	// Token: 0x06001BB7 RID: 7095 RVA: 0x000F7EC4 File Offset: 0x000F60C4
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

	// Token: 0x06001BB8 RID: 7096 RVA: 0x000173CC File Offset: 0x000155CC
	public bool BtnCanClick()
	{
		Tools.instance.canClick(false, true);
		return CanClickManager.Inst.ResultCount == 0 || (CanClickManager.Inst.ResultCount == 1 && CanClickManager.Inst.ResultCache[3]);
	}

	// Token: 0x06001BB9 RID: 7097 RVA: 0x00017407 File Offset: 0x00015607
	public void OnTieJianBtnClick()
	{
		if (this.BtnCanClick())
		{
			Object.Instantiate<GameObject>(Resources.Load<GameObject>("talkPrefab/TalkPrefab/talk" + 518));
		}
	}

	// Token: 0x06001BBA RID: 7098 RVA: 0x00017430 File Offset: 0x00015630
	public void OnChuanYinFuBtnClick()
	{
		if (this.BtnCanClick())
		{
			PanelMamager.inst.OpenPanel(PanelMamager.PanelType.传音符, 1);
		}
	}

	// Token: 0x06001BBB RID: 7099 RVA: 0x00017446 File Offset: 0x00015646
	public void OnTuJianBtnClick()
	{
		if (this.BtnCanClick())
		{
			PanelMamager.inst.OpenPanel(PanelMamager.PanelType.图鉴, 1);
		}
	}

	// Token: 0x06001BBC RID: 7100 RVA: 0x0001745C File Offset: 0x0001565C
	public void OnHeadBtnClick()
	{
		base.Invoke("OpenTab", 0.1f);
	}

	// Token: 0x06001BBD RID: 7101 RVA: 0x0001746E File Offset: 0x0001566E
	private void OpenTab()
	{
		this.IsMouseInUI = false;
		GameObject.Find("UI Root (2D)").GetComponent<Singleton>().ClickTab();
	}

	// Token: 0x06001BBE RID: 7102 RVA: 0x0001748B File Offset: 0x0001568B
	public void OnMapBtnClick()
	{
		if (this.BtnCanClick())
		{
			UIMapPanel.Inst.OpenDefaultMap();
		}
	}

	// Token: 0x06001BBF RID: 7103 RVA: 0x0001749F File Offset: 0x0001569F
	public void OnPointerEnter(PointerEventData eventData)
	{
		this.IsMouseInUI = true;
	}

	// Token: 0x06001BC0 RID: 7104 RVA: 0x000174A8 File Offset: 0x000156A8
	public void OnPointerExit(PointerEventData eventData)
	{
		this.IsMouseInUI = false;
	}

	// Token: 0x0400177E RID: 6014
	public static UIHeadPanel Inst;

	// Token: 0x0400177F RID: 6015
	public GameObject ScaleObj;

	// Token: 0x04001780 RID: 6016
	public Text NameText;

	// Token: 0x04001781 RID: 6017
	public Text HPText;

	// Token: 0x04001782 RID: 6018
	public Text ExpText;

	// Token: 0x04001783 RID: 6019
	public Slider HPSlider;

	// Token: 0x04001784 RID: 6020
	public Slider ExpSlider;

	// Token: 0x04001785 RID: 6021
	public PlayerSetRandomFace Face;

	// Token: 0x04001786 RID: 6022
	public Image LevelImage;

	// Token: 0x04001787 RID: 6023
	public GameObject TieJianRedPoint;

	// Token: 0x04001788 RID: 6024
	public GameObject ChuanYinFuPoint;

	// Token: 0x04001789 RID: 6025
	public GameObject TuJianPoint;

	// Token: 0x0400178A RID: 6026
	public GameObject TianJieObj;

	// Token: 0x0400178B RID: 6027
	public Text TianJieTime;

	// Token: 0x0400178C RID: 6028
	public GameObject CunDangObj;

	// Token: 0x0400178D RID: 6029
	public CanvasGroup CunDangAlpha;

	// Token: 0x0400178E RID: 6030
	[HideInInspector]
	public bool IsMouseInUI;

	// Token: 0x0400178F RID: 6031
	private bool saving;

	// Token: 0x04001790 RID: 6032
	public List<Sprite> LevelSprite;
}
