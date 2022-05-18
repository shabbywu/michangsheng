using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000431 RID: 1073
public class JueSuanAnimation : MonoBehaviour
{
	// Token: 0x06001C99 RID: 7321 RVA: 0x000FC54C File Offset: 0x000FA74C
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

	// Token: 0x06001C9A RID: 7322 RVA: 0x000FC5DC File Offset: 0x000FA7DC
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

	// Token: 0x06001C9B RID: 7323 RVA: 0x00017E1C File Offset: 0x0001601C
	private void Update()
	{
		if (this.isNeedJieSuan && NpcJieSuanManager.inst.JieSuanAnimation)
		{
			NpcJieSuanManager.inst.JieSuanAnimation = false;
			this.CallBack();
		}
	}

	// Token: 0x04001897 RID: 6295
	public Text content;

	// Token: 0x04001898 RID: 6296
	public Image slider;

	// Token: 0x04001899 RID: 6297
	public Animator animator;

	// Token: 0x0400189A RID: 6298
	private TweenerCore<float, float, FloatOptions> obj;

	// Token: 0x0400189B RID: 6299
	private bool isNeedJieSuan;

	// Token: 0x0400189C RID: 6300
	private UnityAction call;
}
