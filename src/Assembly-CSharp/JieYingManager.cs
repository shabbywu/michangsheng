using System;
using KBEngine;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;
using YSGame.Fight;

// Token: 0x0200063E RID: 1598
public class JieYingManager : MonoBehaviour
{
	// Token: 0x060027A4 RID: 10148 RVA: 0x0001F50A File Offset: 0x0001D70A
	private void Awake()
	{
		this.avatar = Tools.instance.getPlayer();
		this.avatar.jieyin.Init();
		this.SuiDanStart();
	}

	// Token: 0x060027A5 RID: 10149 RVA: 0x0001F532 File Offset: 0x0001D732
	private void SuiDanStart()
	{
		this.JinDanAnimator = this.JinDan.GetComponent<Animator>();
		this.State = 1;
	}

	// Token: 0x060027A6 RID: 10150 RVA: 0x0001F54C File Offset: 0x0001D74C
	public void JinDanAttacked()
	{
		this.JinDanAnimator.Play("hit");
	}

	// Token: 0x060027A7 RID: 10151 RVA: 0x0001F55E File Offset: 0x0001D75E
	public void HuaYingCallBack()
	{
		this.JinDanAnimator.Play("break");
		UIFightPanel.Inst.BanSkillAndWeapon = true;
		base.Invoke("HuaYingStart", 2.73f);
	}

	// Token: 0x060027A8 RID: 10152 RVA: 0x0001F58B File Offset: 0x0001D78B
	private void HuaYingStart()
	{
		this.XinMoSpine.SetActive(true);
		this.HuaYingPanel.SetActive(true);
		this.JinDan.SetActive(false);
		this.State = 2;
		UIFightPanel.Inst.BanSkillAndWeapon = false;
	}

	// Token: 0x060027A9 RID: 10153 RVA: 0x0001F5C3 File Offset: 0x0001D7C3
	private void HideZhuaJi()
	{
		this.yizhizhuaji.gameObject.SetActive(false);
	}

	// Token: 0x060027AA RID: 10154 RVA: 0x00135218 File Offset: 0x00133418
	public void XinMoAttack(int target = 0)
	{
		this.XinMoSpine.GetComponent<Animator>().Play("attank");
		this.yizhizhuaji.AnimationName = "animation";
		this.yizhizhuaji.gameObject.SetActive(true);
		base.Invoke("HideZhuaJi", 1.167f);
	}

	// Token: 0x060027AB RID: 10155 RVA: 0x0013526C File Offset: 0x0013346C
	public void showDamage(int num, int target = 0)
	{
		num = -num;
		switch (target)
		{
		case 0:
			this.JinDanDamage.show(num, 0);
			return;
		case 1:
			this.YiZhiDamage.show(num, 0);
			return;
		case 2:
			this.JingMaiDamage.show(num, 0);
			return;
		default:
			return;
		}
	}

	// Token: 0x060027AC RID: 10156 RVA: 0x001352BC File Offset: 0x001334BC
	private void getCurMoNian()
	{
		if (this.avatar.buffmag.getBuffBySeid(212).Count > 0)
		{
			string text = string.Concat(this.avatar.buffmag.getBuffBySeid(212)[0][2]);
			this.CurMoNian.text = jsonData.instance.BuffJsonData[text]["name"].Str;
			this.CurMoNianDesc.text = jsonData.instance.BuffJsonData[text]["descr"].Str;
			string key = jsonData.instance.BuffSeidJsonData[5][text]["value1"][0].ToString();
			this.NextMoNian.text = jsonData.instance.BuffJsonData[key]["name"].Str;
		}
	}

	// Token: 0x060027AD RID: 10157 RVA: 0x001353BC File Offset: 0x001335BC
	private void Update()
	{
		if (this.avatar != null)
		{
			this.CurYiZhi.text = this.avatar.jieyin.YiZhi.ToString();
			this.YiZhi_Max.text = "/" + this.avatar.jieyin.YiZhi_Max.ToString();
			this.CurJingMai.text = this.avatar.jieyin.JinMai.ToString();
			this.JingMai_Max.text = "/" + this.avatar.jieyin.JinMai_Max.ToString();
			if (this.State == 1)
			{
				this.CurState.text = "碎丹";
				this.CurJingDanHp.text = this.avatar.jieyin.JinDanHP.ToString();
				this.JingDanHp_Max.text = "/" + this.avatar.jieyin.JinDanHP_Max.ToString();
				return;
			}
			if (this.State == 2)
			{
				this.CurState.text = "化婴";
				this.CurHuaYingJinDu.text = this.avatar.jieyin.HuaYing.ToString();
				this.HuaYingJinDu_Max.text = "/" + this.avatar.jieyin.HuaYing_Max.ToString();
				this.getCurMoNian();
			}
		}
	}

	// Token: 0x0400218A RID: 8586
	private Avatar avatar;

	// Token: 0x0400218B RID: 8587
	public GameObject JinDan;

	// Token: 0x0400218C RID: 8588
	private Animator JinDanAnimator;

	// Token: 0x0400218D RID: 8589
	public GameObject XinMoSpine;

	// Token: 0x0400218E RID: 8590
	public GameObject HuaYingPanel;

	// Token: 0x0400218F RID: 8591
	public Text CurYiZhi;

	// Token: 0x04002190 RID: 8592
	public Text YiZhi_Max;

	// Token: 0x04002191 RID: 8593
	public Text CurJingMai;

	// Token: 0x04002192 RID: 8594
	public Text JingMai_Max;

	// Token: 0x04002193 RID: 8595
	public Text CurJingDanHp;

	// Token: 0x04002194 RID: 8596
	public Text JingDanHp_Max;

	// Token: 0x04002195 RID: 8597
	public Text CurHuaYingJinDu;

	// Token: 0x04002196 RID: 8598
	public Text HuaYingJinDu_Max;

	// Token: 0x04002197 RID: 8599
	public Text CurMoNian;

	// Token: 0x04002198 RID: 8600
	public Text CurMoNianDesc;

	// Token: 0x04002199 RID: 8601
	public Text NextMoNian;

	// Token: 0x0400219A RID: 8602
	public Text CurState;

	// Token: 0x0400219B RID: 8603
	public SkeletonAnimation yizhizhuaji;

	// Token: 0x0400219C RID: 8604
	public SkeletonAnimation jingmaizhuaji;

	// Token: 0x0400219D RID: 8605
	public AvatarShowHpDamage JinDanDamage;

	// Token: 0x0400219E RID: 8606
	public AvatarShowHpDamage YiZhiDamage;

	// Token: 0x0400219F RID: 8607
	public AvatarShowHpDamage JingMaiDamage;

	// Token: 0x040021A0 RID: 8608
	private int State;
}
