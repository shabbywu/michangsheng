using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020002DF RID: 735
public class JueSuanAnimation : MonoBehaviour
{
	// Token: 0x06001983 RID: 6531 RVA: 0x000B66A0 File Offset: 0x000B48A0
	public void Play(string content, UnityAction action)
	{
		this.isNeedJieSuan = false;
		base.gameObject.SetActive(true);
		this.call = action;
		Tools.canClickFlag = false;
		NpcJieSuanManager.inst.JieSuanAnimation = false;
		this.content.text = content;
		TweenCallback <>9__1;
		this.obj = TweenExtensions.Play<TweenerCore<float, float, FloatOptions>>(TweenSettingsExtensions.OnComplete<TweenerCore<float, float, FloatOptions>>(DOTweenModuleUI.DOFillAmount(this.slider, 0.2f, 1.5f), delegate()
		{
			if (NpcJieSuanManager.inst.isCanJieSuan)
			{
				TweenerCore<float, float, FloatOptions> tweenerCore = DOTweenModuleUI.DOFillAmount(this.slider, 1f, 0.5f);
				TweenCallback tweenCallback;
				if ((tweenCallback = <>9__1) == null)
				{
					tweenCallback = (<>9__1 = delegate()
					{
						Tools.canClickFlag = true;
						if (action != null)
						{
							action.Invoke();
						}
						Object.Destroy(this.gameObject);
					});
				}
				TweenExtensions.Play<TweenerCore<float, float, FloatOptions>>(TweenSettingsExtensions.OnComplete<TweenerCore<float, float, FloatOptions>>(tweenerCore, tweenCallback));
				return;
			}
			this.isNeedJieSuan = true;
			this.obj = DOTweenModuleUI.DOFillAmount(this.slider, 0.98f, 20f);
		}));
	}

	// Token: 0x06001984 RID: 6532 RVA: 0x000B6730 File Offset: 0x000B4930
	private void CallBack()
	{
		if (this.obj != null)
		{
			TweenExtensions.Kill(this.obj, false);
		}
		TweenExtensions.Play<TweenerCore<float, float, FloatOptions>>(TweenSettingsExtensions.OnComplete<TweenerCore<float, float, FloatOptions>>(DOTweenModuleUI.DOFillAmount(this.slider, 1f, 0.5f * (1f - this.slider.fillAmount)), delegate()
		{
			Tools.canClickFlag = true;
			if (this.call != null)
			{
				this.call.Invoke();
			}
			Object.Destroy(base.gameObject);
		}));
	}

	// Token: 0x06001985 RID: 6533 RVA: 0x000B678F File Offset: 0x000B498F
	private void Update()
	{
		if (this.isNeedJieSuan && NpcJieSuanManager.inst.JieSuanAnimation)
		{
			NpcJieSuanManager.inst.JieSuanAnimation = false;
			this.CallBack();
		}
	}

	// Token: 0x040014B7 RID: 5303
	public Text content;

	// Token: 0x040014B8 RID: 5304
	public Image slider;

	// Token: 0x040014B9 RID: 5305
	public Animator animator;

	// Token: 0x040014BA RID: 5306
	private TweenerCore<float, float, FloatOptions> obj;

	// Token: 0x040014BB RID: 5307
	private bool isNeedJieSuan;

	// Token: 0x040014BC RID: 5308
	private UnityAction call;
}
