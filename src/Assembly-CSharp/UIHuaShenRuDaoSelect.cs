using System;
using System.Collections.Generic;
using Fungus;
using GUIPackage;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200051D RID: 1309
public class UIHuaShenRuDaoSelect : MonoBehaviour, IESCClose
{
	// Token: 0x060021A7 RID: 8615 RVA: 0x00118894 File Offset: 0x00116A94
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

	// Token: 0x060021A8 RID: 8616 RVA: 0x00118948 File Offset: 0x00116B48
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

	// Token: 0x060021A9 RID: 8617 RVA: 0x0001BAC5 File Offset: 0x00019CC5
	public void Close()
	{
		GlobalValue.SetTalk(1, 0, "UIHuaShenRuDaoSelect.Close");
		ESCCloseManager.Inst.UnRegisterClose(this);
		this.ScaleObj.SetActive(false);
		this.IsShow = false;
	}

	// Token: 0x060021AA RID: 8618 RVA: 0x001189BC File Offset: 0x00116BBC
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

	// Token: 0x060021AB RID: 8619 RVA: 0x00118A3C File Offset: 0x00116C3C
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

	// Token: 0x060021AC RID: 8620 RVA: 0x00118B4C File Offset: 0x00116D4C
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

	// Token: 0x060021AD RID: 8621 RVA: 0x00118BF4 File Offset: 0x00116DF4
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
		StartFight.Do(10101, 1, StartFight.MonstarType.Normal, StartFight.FightEnumType.HuaShen, 0, 0, 0, 0, "战斗3", false, "", new List<StarttFightAddBuff>(), list);
	}

	// Token: 0x060021AE RID: 8622 RVA: 0x0001BAF1 File Offset: 0x00019CF1
	public bool TryEscClose()
	{
		this.Close();
		return true;
	}

	// Token: 0x04001D29 RID: 7465
	public static UIHuaShenRuDaoSelect Inst;

	// Token: 0x04001D2A RID: 7466
	public GameObject ScaleObj;

	// Token: 0x04001D2B RID: 7467
	public Transform ButtomListTransform;

	// Token: 0x04001D2C RID: 7468
	public GameObject HideObj;

	// Token: 0x04001D2D RID: 7469
	public Text Title;

	// Token: 0x04001D2E RID: 7470
	public Text Desc1;

	// Token: 0x04001D2F RID: 7471
	public Text Desc2;

	// Token: 0x04001D30 RID: 7472
	private List<FpBtn> btnList = new List<FpBtn>();

	// Token: 0x04001D31 RID: 7473
	private List<Transform> lightList = new List<Transform>();

	// Token: 0x04001D32 RID: 7474
	private List<Transform> darkList = new List<Transform>();

	// Token: 0x04001D33 RID: 7475
	[HideInInspector]
	public bool IsShow;

	// Token: 0x04001D34 RID: 7476
	private List<int> btnState;

	// Token: 0x04001D35 RID: 7477
	private int nowSelectDaDao;
}
