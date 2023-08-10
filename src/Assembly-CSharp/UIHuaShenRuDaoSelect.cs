using System.Collections.Generic;
using Fungus;
using GUIPackage;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIHuaShenRuDaoSelect : MonoBehaviour, IESCClose
{
	public static UIHuaShenRuDaoSelect Inst;

	public GameObject ScaleObj;

	public Transform ButtomListTransform;

	public GameObject HideObj;

	public Text Title;

	public Text Desc1;

	public Text Desc2;

	private List<FpBtn> btnList = new List<FpBtn>();

	private List<Transform> lightList = new List<Transform>();

	private List<Transform> darkList = new List<Transform>();

	[HideInInspector]
	public bool IsShow;

	private List<int> btnState;

	private int nowSelectDaDao;

	private void Awake()
	{
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Expected O, but got Unknown
		Inst = this;
		btnState = new List<int>();
		for (int i = 0; i < 9; i++)
		{
			Transform child = ButtomListTransform.GetChild(i);
			FpBtn component = ((Component)child.GetChild(0)).GetComponent<FpBtn>();
			int daDao = i + 1;
			component.mouseUpEvent.AddListener((UnityAction)delegate
			{
				OnDaDaoClick(daDao);
			});
			btnList.Add(component);
			lightList.Add(child.GetChild(1));
			darkList.Add(child.GetChild(2));
			btnState.Add(0);
		}
	}

	public void Show()
	{
		HideObj.SetActive(false);
		ScaleObj.SetActive(true);
		ESCCloseManager.Inst.RegisterClose(this);
		nowSelectDaDao = 0;
		RefreshBtnState();
		Title.text = "";
		Desc1.text = "";
		Desc2.text = "";
		IsShow = true;
	}

	public void Close()
	{
		GlobalValue.SetTalk(1, 0, "UIHuaShenRuDaoSelect.Close");
		ESCCloseManager.Inst.UnRegisterClose(this);
		ScaleObj.SetActive(false);
		IsShow = false;
	}

	public void RefreshBtnState()
	{
		Avatar player = PlayerEx.Player;
		for (int i = 1; i <= 9; i++)
		{
			if (player.wuDaoMag.getWuDaoLevelByType(i) < 5)
			{
				btnState[i - 1] = -1;
			}
			else if (nowSelectDaDao == i)
			{
				btnState[i - 1] = 1;
			}
			else
			{
				btnState[i - 1] = 0;
			}
			SetBtnShow(i - 1, btnState[i - 1]);
		}
	}

	public void SetBtnShow(int btnIndex, int btnState)
	{
		switch (btnState)
		{
		case -1:
			((Component)btnList[btnIndex]).gameObject.SetActive(false);
			((Component)lightList[btnIndex]).gameObject.SetActive(false);
			((Component)darkList[btnIndex]).gameObject.SetActive(true);
			break;
		case 0:
			((Component)btnList[btnIndex]).gameObject.SetActive(true);
			((Component)lightList[btnIndex]).gameObject.SetActive(false);
			((Component)darkList[btnIndex]).gameObject.SetActive(false);
			break;
		case 1:
			btnList[btnIndex].targetImage.sprite = btnList[btnIndex].nomalSprite;
			((Component)btnList[btnIndex]).gameObject.SetActive(false);
			((Component)lightList[btnIndex]).gameObject.SetActive(true);
			((Component)darkList[btnIndex]).gameObject.SetActive(false);
			break;
		}
	}

	public void OnDaDaoClick(int daDaoID)
	{
		nowSelectDaDao = daDaoID;
		RefreshBtnState();
		HuaShenData huaShenData = HuaShenData.DataDict[daDaoID];
		Title.text = huaShenData.Name;
		_BuffJsonData buffJsonData = _BuffJsonData.DataDict[huaShenData.Buff];
		GUIPackage.Skill skill = SkillDatebase.instence.Dict[huaShenData.Skill][1];
		Desc1.text = "突破化神时，" + buffJsonData.descr;
		Desc2.text = skill.skill_Desc ?? "";
		HideObj.SetActive(true);
	}

	public void OnOkClick()
	{
		if (nowSelectDaDao == 0)
		{
			UIPopTip.Inst.Pop("请选择一个大道");
			return;
		}
		Debug.Log((object)$"进入突破化神，选择了大道{nowSelectDaDao}");
		Close();
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
		HuaShenData huaShenData = HuaShenData.DataDict[nowSelectDaDao];
		list.Add(new StarttFightAddBuff
		{
			buffID = huaShenData.Buff,
			BuffNum = 1
		});
		player.HuaShenLingYuSkill = new JSONObject(huaShenData.Skill);
		player.HuaShenWuDao = new JSONObject(nowSelectDaDao);
		Tools.instance.monstarMag.HeroAddBuff.Clear();
		StartFight.Do(10101, 1, StartFight.MonstarType.Normal, StartFight.FightEnumType.HuaShen, 0, 0, 0, 0, "战斗3", SeaRemoveNPCFlag: false, "", new List<StarttFightAddBuff>(), list);
	}

	public bool TryEscClose()
	{
		Close();
		return true;
	}
}
