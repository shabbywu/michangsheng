using System;
using System.Collections.Generic;
using Bag;
using GUIPackage;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000423 RID: 1059
public class UIJianLingQingJiaoPanel : MonoBehaviour, IESCClose
{
	// Token: 0x06001C55 RID: 7253 RVA: 0x00017B12 File Offset: 0x00015D12
	private void Start()
	{
		UIJianLingQingJiaoPanel.Inst = this;
		ESCCloseManager.Inst.RegisterClose(this);
		this.BackBtn.mouseUpEvent.AddListener(new UnityAction(this.Close));
		this.Refresh();
	}

	// Token: 0x06001C56 RID: 7254 RVA: 0x000FB1C4 File Offset: 0x000F93C4
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
				this.QingJiaoSkills[i].SetName("？？？", Color.gray);
			}
			if (yiLingWu)
			{
				this.QingJiaoSkills[i].SetJiaoBiao(JiaoBiaoType.悟);
			}
		}
	}

	// Token: 0x06001C57 RID: 7255 RVA: 0x000FB480 File Offset: 0x000F9680
	public void LaoYeYeSay(string msg)
	{
		string text = msg.ReplaceTalkWord();
		this.LaoYeYeTalkText.text = text;
		this.LaoYeYeTalkObj.SetActive(true);
	}

	// Token: 0x06001C58 RID: 7256 RVA: 0x000FB4AC File Offset: 0x000F96AC
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

	// Token: 0x06001C59 RID: 7257 RVA: 0x00017B47 File Offset: 0x00015D47
	bool IESCClose.TryEscClose()
	{
		this.Close();
		return true;
	}

	// Token: 0x06001C5A RID: 7258 RVA: 0x00017B50 File Offset: 0x00015D50
	public void Close()
	{
		ESCCloseManager.Inst.UnRegisterClose(this);
		Object.Destroy(base.gameObject);
		UIJianLingPanel.OpenPanel();
	}

	// Token: 0x04001849 RID: 6217
	public static UIJianLingQingJiaoPanel Inst;

	// Token: 0x0400184A RID: 6218
	public Text HuiFuDuText;

	// Token: 0x0400184B RID: 6219
	public List<Image> HuiFuDuSliders;

	// Token: 0x0400184C RID: 6220
	public List<SlotBase> QingJiaoSkills;

	// Token: 0x0400184D RID: 6221
	public FpBtn BackBtn;

	// Token: 0x0400184E RID: 6222
	public GameObject LaoYeYeTalkObj;

	// Token: 0x0400184F RID: 6223
	public Text LaoYeYeTalkText;
}
