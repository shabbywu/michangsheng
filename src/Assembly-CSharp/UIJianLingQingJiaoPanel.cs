using System;
using System.Collections.Generic;
using Bag;
using GUIPackage;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020002D7 RID: 727
public class UIJianLingQingJiaoPanel : MonoBehaviour, IESCClose
{
	// Token: 0x0600194E RID: 6478 RVA: 0x000B53E7 File Offset: 0x000B35E7
	private void Start()
	{
		UIJianLingQingJiaoPanel.Inst = this;
		ESCCloseManager.Inst.RegisterClose(this);
		this.BackBtn.mouseUpEvent.AddListener(new UnityAction(this.Close));
		this.Refresh();
	}

	// Token: 0x0600194F RID: 6479 RVA: 0x000B541C File Offset: 0x000B361C
	public void Refresh()
	{
		this.LaoYeYeTalkObj.SetActive(false);
		Avatar player = PlayerEx.Player;
		JianLingManager jianLingManager = player.jianLingManager;
		int huiFuDu = jianLingManager.GetJiYiHuiFuDu();
		huiFuDu = Mathf.Clamp(huiFuDu, 0, 100);
		this.HuiFuDuText.text = string.Format("{0}%", huiFuDu);
		this.SetHuiFuDuSlider(huiFuDu);
		UnityAction <>9__2;
		UnityAction <>9__5;
		for (int i = 0; i < this.QingJiaoSkills.Count; i++)
		{
			JianLingQingJiao qingJiao = JianLingQingJiao.DataList[i];
			this.QingJiaoSkills[i].OnLeftClick.RemoveAllListeners();
			bool yiLingWu = false;
			if (qingJiao.SkillID > 0)
			{
				yiLingWu = PlayerEx.HasSkill(qingJiao.SkillID);
				ActiveSkill skill = new ActiveSkill();
				skill.SetSkill(qingJiao.SkillID, Tools.instance.getPlayer().getLevelType());
				this.QingJiaoSkills[i].SetSlotData(skill);
				UnityAction <>9__1;
				this.QingJiaoSkills[i].OnLeftClick.AddListener(delegate()
				{
					if (huiFuDu < qingJiao.JiYi)
					{
						this.LaoYeYeSay(qingJiao.QingJiaoDuiHuaQian);
						return;
					}
					if (yiLingWu)
					{
						this.LaoYeYeSay(qingJiao.QingJiaoDuiHuaHou);
						return;
					}
					string text = "是否请教" + skill.Name + "？";
					UnityAction onOK;
					if ((onOK = <>9__1) == null)
					{
						onOK = (<>9__1 = delegate()
						{
							player.addHasSkillList(qingJiao.SkillID);
							UIPopTip.Inst.Pop("领悟了" + skill.Name, PopTipIconType.叹号);
							this.Refresh();
							this.LaoYeYeSay(qingJiao.QingJiaoDuiHuaZhong);
						});
					}
					UnityAction onClose;
					if ((onClose = <>9__2) == null)
					{
						onClose = (<>9__2 = delegate()
						{
							this.LaoYeYeTalkObj.SetActive(false);
						});
					}
					USelectBox.Show(text, onOK, onClose);
				});
			}
			else if (qingJiao.StaticSkillID > 0)
			{
				yiLingWu = PlayerEx.HasStaticSkill(qingJiao.StaticSkillID);
				PassiveSkill staticSkill = new PassiveSkill();
				staticSkill.SetSkill(qingJiao.StaticSkillID, 1);
				this.QingJiaoSkills[i].SetSlotData(staticSkill);
				UnityAction <>9__4;
				this.QingJiaoSkills[i].OnLeftClick.AddListener(delegate()
				{
					if (huiFuDu < qingJiao.JiYi)
					{
						this.LaoYeYeSay(qingJiao.QingJiaoDuiHuaQian);
						return;
					}
					if (yiLingWu)
					{
						this.LaoYeYeSay(qingJiao.QingJiaoDuiHuaHou);
						return;
					}
					string text = "是否请教" + staticSkill.Name + "？";
					UnityAction onOK;
					if ((onOK = <>9__4) == null)
					{
						onOK = (<>9__4 = delegate()
						{
							player.addHasStaticSkillList(qingJiao.StaticSkillID, 1);
							new StaticSkill(Tools.instance.getStaticSkillKeyByID(qingJiao.StaticSkillID), 0, 5).Puting(player, player, 3);
							UIPopTip.Inst.Pop("领悟了" + staticSkill.Name, PopTipIconType.叹号);
							this.Refresh();
							this.LaoYeYeSay(qingJiao.QingJiaoDuiHuaZhong);
						});
					}
					UnityAction onClose;
					if ((onClose = <>9__5) == null)
					{
						onClose = (<>9__5 = delegate()
						{
							this.LaoYeYeTalkObj.SetActive(false);
						});
					}
					USelectBox.Show(text, onOK, onClose);
				});
			}
			if (huiFuDu < qingJiao.JiYi && !yiLingWu)
			{
				this.QingJiaoSkills[i].SetGrey(true);
				this.QingJiaoSkills[i].HideTooltip = true;
				this.QingJiaoSkills[i].SetName(" ？？？", Color.white);
			}
			if (yiLingWu)
			{
				this.QingJiaoSkills[i].SetJiaoBiao(JiaoBiaoType.悟);
			}
		}
	}

	// Token: 0x06001950 RID: 6480 RVA: 0x000B56D8 File Offset: 0x000B38D8
	public void LaoYeYeSay(string msg)
	{
		string text = msg.ReplaceTalkWord();
		this.LaoYeYeTalkText.text = text;
		this.LaoYeYeTalkObj.SetActive(true);
	}

	// Token: 0x06001951 RID: 6481 RVA: 0x000B5704 File Offset: 0x000B3904
	public void SetHuiFuDuSlider(int huiFuDu)
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		foreach (JianLingQingJiao jianLingQingJiao in JianLingQingJiao.DataList)
		{
			if (huiFuDu >= jianLingQingJiao.JiYi)
			{
				num++;
				num2 = jianLingQingJiao.JiYi;
			}
			else
			{
				num3 = jianLingQingJiao.JiYi;
			}
		}
		for (int i = 0; i < 5; i++)
		{
			if (i <= num)
			{
				this.HuiFuDuSliders[i].fillAmount = 1f;
			}
			else
			{
				this.HuiFuDuSliders[i].fillAmount = 0f;
			}
		}
		if (num < this.HuiFuDuSliders.Count)
		{
			this.HuiFuDuSliders[num].fillAmount = (float)(huiFuDu - num2) / (float)(num3 - num2);
		}
	}

	// Token: 0x06001952 RID: 6482 RVA: 0x000B57E4 File Offset: 0x000B39E4
	bool IESCClose.TryEscClose()
	{
		this.Close();
		return true;
	}

	// Token: 0x06001953 RID: 6483 RVA: 0x000B57ED File Offset: 0x000B39ED
	public void Close()
	{
		ESCCloseManager.Inst.UnRegisterClose(this);
		Object.Destroy(base.gameObject);
		UIJianLingPanel.OpenPanel();
	}

	// Token: 0x0400147C RID: 5244
	public static UIJianLingQingJiaoPanel Inst;

	// Token: 0x0400147D RID: 5245
	public Text HuiFuDuText;

	// Token: 0x0400147E RID: 5246
	public List<Image> HuiFuDuSliders;

	// Token: 0x0400147F RID: 5247
	public List<SlotBase> QingJiaoSkills;

	// Token: 0x04001480 RID: 5248
	public FpBtn BackBtn;

	// Token: 0x04001481 RID: 5249
	public GameObject LaoYeYeTalkObj;

	// Token: 0x04001482 RID: 5250
	public Text LaoYeYeTalkText;
}
