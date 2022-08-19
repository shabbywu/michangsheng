using System;
using System.Collections.Generic;
using Fungus;
using GUIPackage;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000395 RID: 917
public class UIHuaShenRuDaoSelect : MonoBehaviour, IESCClose
{
	// Token: 0x06001E28 RID: 7720 RVA: 0x000D4DAC File Offset: 0x000D2FAC
	private void Awake()
	{
		UIHuaShenRuDaoSelect.Inst = this;
		this.btnState = new List<int>();
		for (int i = 0; i < 9; i++)
		{
			Transform child = this.ButtomListTransform.GetChild(i);
			FpBtn component = child.GetChild(0).GetComponent<FpBtn>();
			int daDao = i + 1;
			component.mouseUpEvent.AddListener(delegate()
			{
				this.OnDaDaoClick(daDao);
			});
			this.btnList.Add(component);
			this.lightList.Add(child.GetChild(1));
			this.darkList.Add(child.GetChild(2));
			this.btnState.Add(0);
		}
	}

	// Token: 0x06001E29 RID: 7721 RVA: 0x000D4E60 File Offset: 0x000D3060
	public void Show()
	{
		this.HideObj.SetActive(false);
		this.ScaleObj.SetActive(true);
		ESCCloseManager.Inst.RegisterClose(this);
		this.nowSelectDaDao = 0;
		this.RefreshBtnState();
		this.Title.text = "";
		this.Desc1.text = "";
		this.Desc2.text = "";
		this.IsShow = true;
	}

	// Token: 0x06001E2A RID: 7722 RVA: 0x000D4ED4 File Offset: 0x000D30D4
	public void Close()
	{
		GlobalValue.SetTalk(1, 0, "UIHuaShenRuDaoSelect.Close");
		ESCCloseManager.Inst.UnRegisterClose(this);
		this.ScaleObj.SetActive(false);
		this.IsShow = false;
	}

	// Token: 0x06001E2B RID: 7723 RVA: 0x000D4F00 File Offset: 0x000D3100
	public void RefreshBtnState()
	{
		Avatar player = PlayerEx.Player;
		for (int i = 1; i <= 9; i++)
		{
			if (player.wuDaoMag.getWuDaoLevelByType(i) < 5)
			{
				this.btnState[i - 1] = -1;
			}
			else if (this.nowSelectDaDao == i)
			{
				this.btnState[i - 1] = 1;
			}
			else
			{
				this.btnState[i - 1] = 0;
			}
			this.SetBtnShow(i - 1, this.btnState[i - 1]);
		}
	}

	// Token: 0x06001E2C RID: 7724 RVA: 0x000D4F80 File Offset: 0x000D3180
	public void SetBtnShow(int btnIndex, int btnState)
	{
		if (btnState == -1)
		{
			this.btnList[btnIndex].gameObject.SetActive(false);
			this.lightList[btnIndex].gameObject.SetActive(false);
			this.darkList[btnIndex].gameObject.SetActive(true);
			return;
		}
		if (btnState == 0)
		{
			this.btnList[btnIndex].gameObject.SetActive(true);
			this.lightList[btnIndex].gameObject.SetActive(false);
			this.darkList[btnIndex].gameObject.SetActive(false);
			return;
		}
		if (btnState == 1)
		{
			this.btnList[btnIndex].targetImage.sprite = this.btnList[btnIndex].nomalSprite;
			this.btnList[btnIndex].gameObject.SetActive(false);
			this.lightList[btnIndex].gameObject.SetActive(true);
			this.darkList[btnIndex].gameObject.SetActive(false);
		}
	}

	// Token: 0x06001E2D RID: 7725 RVA: 0x000D5090 File Offset: 0x000D3290
	public void OnDaDaoClick(int daDaoID)
	{
		this.nowSelectDaDao = daDaoID;
		this.RefreshBtnState();
		HuaShenData huaShenData = HuaShenData.DataDict[daDaoID];
		this.Title.text = huaShenData.Name;
		_BuffJsonData buffJsonData = _BuffJsonData.DataDict[huaShenData.Buff];
		GUIPackage.Skill skill = SkillDatebase.instence.Dict[huaShenData.Skill][1];
		this.Desc1.text = "突破化神时，" + buffJsonData.descr;
		this.Desc2.text = (skill.skill_Desc ?? "");
		this.HideObj.SetActive(true);
	}

	// Token: 0x06001E2E RID: 7726 RVA: 0x000D5138 File Offset: 0x000D3338
	public void OnOkClick()
	{
		if (this.nowSelectDaDao == 0)
		{
			UIPopTip.Inst.Pop("请选择一个大道", PopTipIconType.叹号);
			return;
		}
		Debug.Log(string.Format("进入突破化神，选择了大道{0}", this.nowSelectDaDao));
		this.Close();
		List<StarttFightAddBuff> list = new List<StarttFightAddBuff>();
		list.Add(new StarttFightAddBuff
		{
			buffID = 3130,
			BuffNum = 1
		});
		list.Add(new StarttFightAddBuff
		{
			buffID = 3141,
			BuffNum = 1
		});
		list.Add(new StarttFightAddBuff
		{
			buffID = 3142,
			BuffNum = 1
		});
		Avatar player = PlayerEx.Player;
		if (player.HuaShenStartXianXing.I > 0)
		{
			list.Add(new StarttFightAddBuff
			{
				buffID = 3133,
				BuffNum = player.HuaShenStartXianXing.I
			});
		}
		HuaShenData huaShenData = HuaShenData.DataDict[this.nowSelectDaDao];
		list.Add(new StarttFightAddBuff
		{
			buffID = huaShenData.Buff,
			BuffNum = 1
		});
		player.HuaShenLingYuSkill = new JSONObject(huaShenData.Skill);
		player.HuaShenWuDao = new JSONObject(this.nowSelectDaDao);
		Tools.instance.monstarMag.HeroAddBuff.Clear();
		StartFight.Do(10101, 1, StartFight.MonstarType.Normal, StartFight.FightEnumType.HuaShen, 0, 0, 0, 0, "战斗3", false, "", new List<StarttFightAddBuff>(), list);
	}

	// Token: 0x06001E2F RID: 7727 RVA: 0x000D529E File Offset: 0x000D349E
	public bool TryEscClose()
	{
		this.Close();
		return true;
	}

	// Token: 0x040018C2 RID: 6338
	public static UIHuaShenRuDaoSelect Inst;

	// Token: 0x040018C3 RID: 6339
	public GameObject ScaleObj;

	// Token: 0x040018C4 RID: 6340
	public Transform ButtomListTransform;

	// Token: 0x040018C5 RID: 6341
	public GameObject HideObj;

	// Token: 0x040018C6 RID: 6342
	public Text Title;

	// Token: 0x040018C7 RID: 6343
	public Text Desc1;

	// Token: 0x040018C8 RID: 6344
	public Text Desc2;

	// Token: 0x040018C9 RID: 6345
	private List<FpBtn> btnList = new List<FpBtn>();

	// Token: 0x040018CA RID: 6346
	private List<Transform> lightList = new List<Transform>();

	// Token: 0x040018CB RID: 6347
	private List<Transform> darkList = new List<Transform>();

	// Token: 0x040018CC RID: 6348
	[HideInInspector]
	public bool IsShow;

	// Token: 0x040018CD RID: 6349
	private List<int> btnState;

	// Token: 0x040018CE RID: 6350
	private int nowSelectDaDao;
}
