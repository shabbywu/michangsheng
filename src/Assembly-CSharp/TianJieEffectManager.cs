using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using JSONClass;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YSGame.Fight;

// Token: 0x02000486 RID: 1158
public class TianJieEffectManager : MonoBehaviour
{
	// Token: 0x06002460 RID: 9312 RVA: 0x000FB6F8 File Offset: 0x000F98F8
	private void Awake()
	{
		TianJieEffectManager.Inst = this;
		this.shake = Object.FindObjectOfType<FightScreenShake>();
	}

	// Token: 0x06002461 RID: 9313 RVA: 0x000FB70B File Offset: 0x000F990B
	private void Update()
	{
		this.countCD -= Time.deltaTime;
		if (this.countCD < 0f)
		{
			this.countCD = 1f;
			if (PlayerEx.Player != null)
			{
				this.RefreshXuLiCount();
			}
		}
	}

	// Token: 0x06002462 RID: 9314 RVA: 0x000FB744 File Offset: 0x000F9944
	public void Init()
	{
		this.replaceSprites = base.GetComponentsInChildren<ReplaceDuJieAnimSprite>();
	}

	// Token: 0x06002463 RID: 9315 RVA: 0x000FB754 File Offset: 0x000F9954
	public void SetLeiJieSprite(string leiJie)
	{
		if (this.replaceSprites != null)
		{
			ReplaceDuJieAnimSprite[] array = this.replaceSprites;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SetSprite(leiJie);
			}
		}
	}

	// Token: 0x06002464 RID: 9316 RVA: 0x000FB788 File Offset: 0x000F9988
	public void SetLeiJieBG(string leiJie)
	{
		if (this.lastJieYunBG != null)
		{
			DOTweenModuleSprite.DOColor(this.lastJieYunBG.SR, Color.clear, 0.5f);
		}
		List<ReplaceDuJieAnimSprite> jieYunBGs = this.JieYunBGs;
		int num = this.jieYunBGIndex;
		this.jieYunBGIndex = num + 1;
		ReplaceDuJieAnimSprite replaceDuJieAnimSprite = jieYunBGs[num];
		if (this.jieYunBGIndex > 1)
		{
			this.jieYunBGIndex = 0;
		}
		replaceDuJieAnimSprite.SetSprite(leiJie);
		DOTweenModuleSprite.DOColor(replaceDuJieAnimSprite.SR, Color.white, 0.5f);
		this.lastJieYunBG = replaceDuJieAnimSprite;
		Debug.Log("切换劫云背景到 " + leiJie);
	}

	// Token: 0x06002465 RID: 9317 RVA: 0x000FB81F File Offset: 0x000F9A1F
	public void PlayXuLi()
	{
		base.StartCoroutine(this.XuLiCoroutine());
	}

	// Token: 0x06002466 RID: 9318 RVA: 0x000FB82E File Offset: 0x000F9A2E
	private IEnumerator XuLiCoroutine()
	{
		this.TianJieNameAnim.Play("Enter");
		this.TianJieNameText.text = TianJieManager.Inst.NowLeiJie;
		this.TianJieDescAnim.Play("Enter");
		TianJieLeiJieType tianJieLeiJieType = TianJieLeiJieType.DataDict[TianJieManager.Inst.NowLeiJie];
		this.TianJieDescText.text = tianJieLeiJieType.CuLue;
		this.RefreshXuLiCount();
		if (TianJieManager.Inst.LeiJieIndex > 0)
		{
			this.SetLeiJieSprite(TianJieManager.Inst.NowLeiJie);
			this.SetLeiJieBG(TianJieManager.Inst.NowLeiJie);
			yield return new WaitForSeconds(0.5f);
		}
		this.TianJieYunAnim.Play("劫云出现");
		Debug.Log("播放劫云出现动画和劫云蓄力动画");
		yield break;
	}

	// Token: 0x06002467 RID: 9319 RVA: 0x000FB83D File Offset: 0x000F9A3D
	public void PlayAttack(Action attackAction)
	{
		this.attackAnimAction = attackAction;
		base.StartCoroutine(this.AttackCoroutine());
	}

	// Token: 0x06002468 RID: 9320 RVA: 0x000FB853 File Offset: 0x000F9A53
	private IEnumerator AttackCoroutine()
	{
		Debug.Log(string.Format("[{0}]播放劫云攻击动画(云部分) 以及 播放劫云消散动画", Time.frameCount));
		this.TianJieYunAnim.Play("劫云攻击");
		int xuLi = TianJieManager.Inst.XuLiCount;
		yield return new WaitForSeconds(2f);
		if (this.attackAnimAction != null)
		{
			this.attackAnimAction();
		}
		Debug.Log(string.Format("[{0}] 启用血条刷新和伤害数字", Time.frameCount));
		UIFightPanel.Inst.PlayerStatus.NoRefresh = false;
		MessageMag.Instance.Send(MessageName.MSG_Fight_Effect_Special, null);
		if (xuLi < 100)
		{
			this.LightningAnim.Play("弱雷");
		}
		else if (this.tempAnimLeiList.Contains(TianJieManager.Inst.NowLeiJie))
		{
			this.LightningAnim.Play(TianJieManager.Inst.NowLeiJie);
		}
		else
		{
			this.LightningAnim.Play("天雷劫");
		}
		if (this.shake != null)
		{
			float num = (float)xuLi / 100000f;
			this.shake.ShakePosition = new Vector3(0f, num, 0f);
			this.shake.Shake();
		}
		if (this.LightningBGSR != null)
		{
			this.LightningBGSR.color = Color.white;
			DOTweenModuleSprite.DOColor(this.LightningBGSR, Color.clear, 0.3f);
		}
		yield return new WaitForSeconds(1f);
		this.TianJieNameAnim.Play("Exit");
		this.TianJieDescAnim.Play("Exit");
		TianJieManager.Inst.LeiJieIndex++;
		if (TianJieManager.Inst.LeiJieIndex >= TianJieManager.Inst.LeiJieCount)
		{
			TianJieManager.Inst.DuJieSuccess(false);
		}
		yield return new WaitForSeconds(1f);
		PlayerEx.Player.OtherAvatar.buffmag.RemoveBuff(3150);
		yield break;
	}

	// Token: 0x06002469 RID: 9321 RVA: 0x000FB864 File Offset: 0x000F9A64
	public void RefreshXuLiCount()
	{
		this.TianJieCountText.text = TianJieManager.Inst.XuLiCount.ToString();
	}

	// Token: 0x04001D07 RID: 7431
	public static TianJieEffectManager Inst;

	// Token: 0x04001D08 RID: 7432
	public Animator TianJieYunAnim;

	// Token: 0x04001D09 RID: 7433
	public Animator LightningAnim;

	// Token: 0x04001D0A RID: 7434
	public SpriteRenderer LightningBGSR;

	// Token: 0x04001D0B RID: 7435
	public Animator TianJieNameAnim;

	// Token: 0x04001D0C RID: 7436
	public Text TianJieNameText;

	// Token: 0x04001D0D RID: 7437
	public Text TianJieCountText;

	// Token: 0x04001D0E RID: 7438
	public Animator TianJieDescAnim;

	// Token: 0x04001D0F RID: 7439
	public TextMeshProUGUI TianJieDescText;

	// Token: 0x04001D10 RID: 7440
	public Transform PlayerTransform;

	// Token: 0x04001D11 RID: 7441
	private FightScreenShake shake;

	// Token: 0x04001D12 RID: 7442
	private float countCD = 1f;

	// Token: 0x04001D13 RID: 7443
	private ReplaceDuJieAnimSprite[] replaceSprites;

	// Token: 0x04001D14 RID: 7444
	public List<ReplaceDuJieAnimSprite> JieYunBGs = new List<ReplaceDuJieAnimSprite>();

	// Token: 0x04001D15 RID: 7445
	private ReplaceDuJieAnimSprite lastJieYunBG;

	// Token: 0x04001D16 RID: 7446
	private int jieYunBGIndex;

	// Token: 0x04001D17 RID: 7447
	private Action attackAnimAction;

	// Token: 0x04001D18 RID: 7448
	private List<string> tempAnimLeiList = new List<string>
	{
		"天雷劫",
		"罡雷劫",
		"乾天劫",
		"五行劫",
		"风火劫"
	};
}
