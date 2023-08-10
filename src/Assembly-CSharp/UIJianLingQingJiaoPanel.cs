using System.Collections.Generic;
using Bag;
using GUIPackage;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIJianLingQingJiaoPanel : MonoBehaviour, IESCClose
{
	public static UIJianLingQingJiaoPanel Inst;

	public Text HuiFuDuText;

	public List<Image> HuiFuDuSliders;

	public List<SlotBase> QingJiaoSkills;

	public FpBtn BackBtn;

	public GameObject LaoYeYeTalkObj;

	public Text LaoYeYeTalkText;

	private void Start()
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Expected O, but got Unknown
		Inst = this;
		ESCCloseManager.Inst.RegisterClose(this);
		BackBtn.mouseUpEvent.AddListener(new UnityAction(Close));
		Refresh();
	}

	public void Refresh()
	{
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Expected O, but got Unknown
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Expected O, but got Unknown
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		LaoYeYeTalkObj.SetActive(false);
		Avatar player = PlayerEx.Player;
		JianLingManager jianLingManager = player.jianLingManager;
		int huiFuDu = jianLingManager.GetJiYiHuiFuDu();
		huiFuDu = Mathf.Clamp(huiFuDu, 0, 100);
		HuiFuDuText.text = $"{huiFuDu}%";
		SetHuiFuDuSlider(huiFuDu);
		for (int i = 0; i < QingJiaoSkills.Count; i++)
		{
			JianLingQingJiao qingJiao = JianLingQingJiao.DataList[i];
			((UnityEventBase)QingJiaoSkills[i].OnLeftClick).RemoveAllListeners();
			bool yiLingWu = false;
			if (qingJiao.SkillID > 0)
			{
				yiLingWu = PlayerEx.HasSkill(qingJiao.SkillID);
				ActiveSkill skill = new ActiveSkill();
				skill.SetSkill(qingJiao.SkillID, Tools.instance.getPlayer().getLevelType());
				QingJiaoSkills[i].SetSlotData(skill);
				UnityAction val = default(UnityAction);
				QingJiaoSkills[i].OnLeftClick.AddListener((UnityAction)delegate
				{
					//IL_0084: Unknown result type (might be due to invalid IL or missing references)
					//IL_0089: Unknown result type (might be due to invalid IL or missing references)
					//IL_008b: Expected O, but got Unknown
					//IL_0090: Expected O, but got Unknown
					//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
					//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
					//IL_00c8: Expected O, but got Unknown
					//IL_00cd: Expected O, but got Unknown
					if (huiFuDu >= qingJiao.JiYi)
					{
						if (yiLingWu)
						{
							LaoYeYeSay(qingJiao.QingJiaoDuiHuaHou);
						}
						else
						{
							string text2 = "是否请教" + skill.Name + "？";
							UnityAction obj3 = val;
							if (obj3 == null)
							{
								UnityAction val8 = delegate
								{
									player.addHasSkillList(qingJiao.SkillID);
									UIPopTip.Inst.Pop("领悟了" + skill.Name);
									Refresh();
									LaoYeYeSay(qingJiao.QingJiaoDuiHuaZhong);
								};
								UnityAction val9 = val8;
								val = val8;
								obj3 = val9;
							}
							UnityAction val2 = default(UnityAction);
							UnityAction obj4 = val2;
							if (obj4 == null)
							{
								UnityAction val10 = delegate
								{
									LaoYeYeTalkObj.SetActive(false);
								};
								UnityAction val9 = val10;
								val2 = val10;
								obj4 = val9;
							}
							USelectBox.Show(text2, obj3, obj4);
						}
					}
					else
					{
						LaoYeYeSay(qingJiao.QingJiaoDuiHuaQian);
					}
				});
			}
			else if (qingJiao.StaticSkillID > 0)
			{
				yiLingWu = PlayerEx.HasStaticSkill(qingJiao.StaticSkillID);
				PassiveSkill staticSkill = new PassiveSkill();
				staticSkill.SetSkill(qingJiao.StaticSkillID, 1);
				QingJiaoSkills[i].SetSlotData(staticSkill);
				UnityAction val3 = default(UnityAction);
				QingJiaoSkills[i].OnLeftClick.AddListener((UnityAction)delegate
				{
					//IL_0084: Unknown result type (might be due to invalid IL or missing references)
					//IL_0089: Unknown result type (might be due to invalid IL or missing references)
					//IL_008b: Expected O, but got Unknown
					//IL_0090: Expected O, but got Unknown
					//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
					//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
					//IL_00c8: Expected O, but got Unknown
					//IL_00cd: Expected O, but got Unknown
					if (huiFuDu >= qingJiao.JiYi)
					{
						if (yiLingWu)
						{
							LaoYeYeSay(qingJiao.QingJiaoDuiHuaHou);
						}
						else
						{
							string text = "是否请教" + staticSkill.Name + "？";
							UnityAction obj = val3;
							if (obj == null)
							{
								UnityAction val5 = delegate
								{
									player.addHasStaticSkillList(qingJiao.StaticSkillID);
									new StaticSkill(Tools.instance.getStaticSkillKeyByID(qingJiao.StaticSkillID), 0, 5).Puting(player, player, 3);
									UIPopTip.Inst.Pop("领悟了" + staticSkill.Name);
									Refresh();
									LaoYeYeSay(qingJiao.QingJiaoDuiHuaZhong);
								};
								UnityAction val6 = val5;
								val3 = val5;
								obj = val6;
							}
							UnityAction val4 = default(UnityAction);
							UnityAction obj2 = val4;
							if (obj2 == null)
							{
								UnityAction val7 = delegate
								{
									LaoYeYeTalkObj.SetActive(false);
								};
								UnityAction val6 = val7;
								val4 = val7;
								obj2 = val6;
							}
							USelectBox.Show(text, obj, obj2);
						}
					}
					else
					{
						LaoYeYeSay(qingJiao.QingJiaoDuiHuaQian);
					}
				});
			}
			if (huiFuDu < qingJiao.JiYi && !yiLingWu)
			{
				QingJiaoSkills[i].SetGrey(grey: true);
				QingJiaoSkills[i].HideTooltip = true;
				QingJiaoSkills[i].SetName(" ？？？", Color.white);
			}
			if (yiLingWu)
			{
				QingJiaoSkills[i].SetJiaoBiao(JiaoBiaoType.悟);
			}
		}
	}

	public void LaoYeYeSay(string msg)
	{
		string text = msg.ReplaceTalkWord();
		LaoYeYeTalkText.text = text;
		LaoYeYeTalkObj.SetActive(true);
	}

	public void SetHuiFuDuSlider(int huiFuDu)
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		foreach (JianLingQingJiao data in JianLingQingJiao.DataList)
		{
			if (huiFuDu >= data.JiYi)
			{
				num++;
				num2 = data.JiYi;
			}
			else
			{
				num3 = data.JiYi;
			}
		}
		for (int i = 0; i < 5; i++)
		{
			if (i <= num)
			{
				HuiFuDuSliders[i].fillAmount = 1f;
			}
			else
			{
				HuiFuDuSliders[i].fillAmount = 0f;
			}
		}
		if (num < HuiFuDuSliders.Count)
		{
			HuiFuDuSliders[num].fillAmount = (float)(huiFuDu - num2) / (float)(num3 - num2);
		}
	}

	bool IESCClose.TryEscClose()
	{
		Close();
		return true;
	}

	public void Close()
	{
		ESCCloseManager.Inst.UnRegisterClose(this);
		Object.Destroy((Object)(object)((Component)this).gameObject);
		UIJianLingPanel.OpenPanel();
	}
}
