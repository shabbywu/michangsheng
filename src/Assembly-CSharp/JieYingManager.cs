using System;
using KBEngine;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;
using YSGame.Fight;

// Token: 0x0200047D RID: 1149
public class JieYingManager : MonoBehaviour
{
	// Token: 0x060023E0 RID: 9184 RVA: 0x000F52B0 File Offset: 0x000F34B0
	private void Awake()
	{
		this.avatar = Tools.instance.getPlayer();
		this.avatar.jieyin.Init();
		this.SuiDanStart();
	}

	// Token: 0x060023E1 RID: 9185 RVA: 0x000F52D8 File Offset: 0x000F34D8
	private void SuiDanStart()
	{
		this.JinDanAnimator = this.JinDan.GetComponent<Animator>();
		this.State = 1;
	}

	// Token: 0x060023E2 RID: 9186 RVA: 0x000F52F2 File Offset: 0x000F34F2
	public void JinDanAttacked()
	{
		this.JinDanAnimator.Play("hit");
	}

	// Token: 0x060023E3 RID: 9187 RVA: 0x000F5304 File Offset: 0x000F3504
	public void HuaYingCallBack()
	{
		this.JinDanAnimator.Play("break");
		UIFightPanel.Inst.BanSkillAndWeapon = true;
		base.Invoke("HuaYingStart", 2.73f);
	}

	// Token: 0x060023E4 RID: 9188 RVA: 0x000F5331 File Offset: 0x000F3531
	private void HuaYingStart()
	{
		this.XinMoSpine.SetActive(true);
		this.HuaYingPanel.SetActive(true);
		this.JinDan.SetActive(false);
		this.State = 2;
		UIFightPanel.Inst.BanSkillAndWeapon = false;
	}

	// Token: 0x060023E5 RID: 9189 RVA: 0x000F5369 File Offset: 0x000F3569
	private void HideZhuaJi()
	{
		this.yizhizhuaji.gameObject.SetActive(false);
	}

	// Token: 0x060023E6 RID: 9190 RVA: 0x000F537C File Offset: 0x000F357C
	public void XinMoAttack(int target = 0)
	{
		this.XinMoSpine.GetComponent<Animator>().Play("attank");
		this.yizhizhuaji.AnimationName = "animation";
		this.yizhizhuaji.gameObject.SetActive(true);
		base.Invoke("HideZhuaJi", 1.167f);
	}

	// Token: 0x060023E7 RID: 9191 RVA: 0x000F53D0 File Offset: 0x000F35D0
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

	// Token: 0x060023E8 RID: 9192 RVA: 0x000F5420 File Offset: 0x000F3620
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

	// Token: 0x060023E9 RID: 9193 RVA: 0x000F5520 File Offset: 0x000F3720
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

	// Token: 0x04001CA7 RID: 7335
	private Avatar avatar;

	// Token: 0x04001CA8 RID: 7336
	public GameObject JinDan;

	// Token: 0x04001CA9 RID: 7337
	private Animator JinDanAnimator;

	// Token: 0x04001CAA RID: 7338
	public GameObject XinMoSpine;

	// Token: 0x04001CAB RID: 7339
	public GameObject HuaYingPanel;

	// Token: 0x04001CAC RID: 7340
	public Text CurYiZhi;

	// Token: 0x04001CAD RID: 7341
	public Text YiZhi_Max;

	// Token: 0x04001CAE RID: 7342
	public Text CurJingMai;

	// Token: 0x04001CAF RID: 7343
	public Text JingMai_Max;

	// Token: 0x04001CB0 RID: 7344
	public Text CurJingDanHp;

	// Token: 0x04001CB1 RID: 7345
	public Text JingDanHp_Max;

	// Token: 0x04001CB2 RID: 7346
	public Text CurHuaYingJinDu;

	// Token: 0x04001CB3 RID: 7347
	public Text HuaYingJinDu_Max;

	// Token: 0x04001CB4 RID: 7348
	public Text CurMoNian;

	// Token: 0x04001CB5 RID: 7349
	public Text CurMoNianDesc;

	// Token: 0x04001CB6 RID: 7350
	public Text NextMoNian;

	// Token: 0x04001CB7 RID: 7351
	public Text CurState;

	// Token: 0x04001CB8 RID: 7352
	public SkeletonAnimation yizhizhuaji;

	// Token: 0x04001CB9 RID: 7353
	public SkeletonAnimation jingmaizhuaji;

	// Token: 0x04001CBA RID: 7354
	public AvatarShowHpDamage JinDanDamage;

	// Token: 0x04001CBB RID: 7355
	public AvatarShowHpDamage YiZhiDamage;

	// Token: 0x04001CBC RID: 7356
	public AvatarShowHpDamage JingMaiDamage;

	// Token: 0x04001CBD RID: 7357
	private int State;
}
