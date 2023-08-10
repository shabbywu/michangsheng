using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using JSONClass;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YSGame.Fight;

public class TianJieEffectManager : MonoBehaviour
{
	public static TianJieEffectManager Inst;

	public Animator TianJieYunAnim;

	public Animator LightningAnim;

	public SpriteRenderer LightningBGSR;

	public Animator TianJieNameAnim;

	public Text TianJieNameText;

	public Text TianJieCountText;

	public Animator TianJieDescAnim;

	public TextMeshProUGUI TianJieDescText;

	public Transform PlayerTransform;

	private FightScreenShake shake;

	private float countCD = 1f;

	private ReplaceDuJieAnimSprite[] replaceSprites;

	public List<ReplaceDuJieAnimSprite> JieYunBGs = new List<ReplaceDuJieAnimSprite>();

	private ReplaceDuJieAnimSprite lastJieYunBG;

	private int jieYunBGIndex;

	private Action attackAnimAction;

	private List<string> tempAnimLeiList = new List<string> { "天雷劫", "罡雷劫", "乾天劫", "五行劫", "风火劫" };

	private void Awake()
	{
		Inst = this;
		shake = Object.FindObjectOfType<FightScreenShake>();
	}

	private void Update()
	{
		countCD -= Time.deltaTime;
		if (countCD < 0f)
		{
			countCD = 1f;
			if (PlayerEx.Player != null)
			{
				RefreshXuLiCount();
			}
		}
	}

	public void Init()
	{
		replaceSprites = ((Component)this).GetComponentsInChildren<ReplaceDuJieAnimSprite>();
	}

	public void SetLeiJieSprite(string leiJie)
	{
		if (replaceSprites != null)
		{
			ReplaceDuJieAnimSprite[] array = replaceSprites;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SetSprite(leiJie);
			}
		}
	}

	public void SetLeiJieBG(string leiJie)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)lastJieYunBG != (Object)null)
		{
			DOTweenModuleSprite.DOColor(lastJieYunBG.SR, Color.clear, 0.5f);
		}
		ReplaceDuJieAnimSprite replaceDuJieAnimSprite = JieYunBGs[jieYunBGIndex++];
		if (jieYunBGIndex > 1)
		{
			jieYunBGIndex = 0;
		}
		replaceDuJieAnimSprite.SetSprite(leiJie);
		DOTweenModuleSprite.DOColor(replaceDuJieAnimSprite.SR, Color.white, 0.5f);
		lastJieYunBG = replaceDuJieAnimSprite;
		Debug.Log((object)("切换劫云背景到 " + leiJie));
	}

	public void PlayXuLi()
	{
		((MonoBehaviour)this).StartCoroutine(XuLiCoroutine());
	}

	private IEnumerator XuLiCoroutine()
	{
		TianJieNameAnim.Play("Enter");
		TianJieNameText.text = TianJieManager.Inst.NowLeiJie;
		TianJieDescAnim.Play("Enter");
		TianJieLeiJieType tianJieLeiJieType = TianJieLeiJieType.DataDict[TianJieManager.Inst.NowLeiJie];
		((TMP_Text)TianJieDescText).text = tianJieLeiJieType.CuLue;
		RefreshXuLiCount();
		if (TianJieManager.Inst.LeiJieIndex > 0)
		{
			SetLeiJieSprite(TianJieManager.Inst.NowLeiJie);
			SetLeiJieBG(TianJieManager.Inst.NowLeiJie);
			yield return (object)new WaitForSeconds(0.5f);
		}
		TianJieYunAnim.Play("劫云出现");
		Debug.Log((object)"播放劫云出现动画和劫云蓄力动画");
	}

	public void PlayAttack(Action attackAction)
	{
		attackAnimAction = attackAction;
		((MonoBehaviour)this).StartCoroutine(AttackCoroutine());
	}

	private IEnumerator AttackCoroutine()
	{
		Debug.Log((object)$"[{Time.frameCount}]播放劫云攻击动画(云部分) 以及 播放劫云消散动画");
		TianJieYunAnim.Play("劫云攻击");
		int xuLi = TianJieManager.Inst.XuLiCount;
		yield return (object)new WaitForSeconds(2f);
		if (attackAnimAction != null)
		{
			attackAnimAction();
		}
		Debug.Log((object)$"[{Time.frameCount}] 启用血条刷新和伤害数字");
		UIFightPanel.Inst.PlayerStatus.NoRefresh = false;
		MessageMag.Instance.Send(MessageName.MSG_Fight_Effect_Special);
		if (xuLi < 100)
		{
			LightningAnim.Play("弱雷");
		}
		else if (tempAnimLeiList.Contains(TianJieManager.Inst.NowLeiJie))
		{
			LightningAnim.Play(TianJieManager.Inst.NowLeiJie);
		}
		else
		{
			LightningAnim.Play("天雷劫");
		}
		if ((Object)(object)shake != (Object)null)
		{
			float num = (float)xuLi / 100000f;
			shake.ShakePosition = new Vector3(0f, num, 0f);
			shake.Shake();
		}
		if ((Object)(object)LightningBGSR != (Object)null)
		{
			LightningBGSR.color = Color.white;
			DOTweenModuleSprite.DOColor(LightningBGSR, Color.clear, 0.3f);
		}
		yield return (object)new WaitForSeconds(1f);
		TianJieNameAnim.Play("Exit");
		TianJieDescAnim.Play("Exit");
		TianJieManager.Inst.LeiJieIndex++;
		if (TianJieManager.Inst.LeiJieIndex >= TianJieManager.Inst.LeiJieCount)
		{
			TianJieManager.Inst.DuJieSuccess(win: false);
		}
		yield return (object)new WaitForSeconds(1f);
		PlayerEx.Player.OtherAvatar.buffmag.RemoveBuff(3150);
	}

	public void RefreshXuLiCount()
	{
		TianJieCountText.text = TianJieManager.Inst.XuLiCount.ToString();
	}
}
