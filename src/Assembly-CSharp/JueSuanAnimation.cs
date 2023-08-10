using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class JueSuanAnimation : MonoBehaviour
{
	public Text content;

	public Image slider;

	public Animator animator;

	private TweenerCore<float, float, FloatOptions> obj;

	private bool isNeedJieSuan;

	private UnityAction call;

	public void Play(string content, UnityAction action)
	{
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Expected O, but got Unknown
		isNeedJieSuan = false;
		((Component)this).gameObject.SetActive(true);
		call = action;
		Tools.canClickFlag = false;
		NpcJieSuanManager.inst.JieSuanAnimation = false;
		this.content.text = content;
		TweenCallback val = default(TweenCallback);
		obj = TweenExtensions.Play<TweenerCore<float, float, FloatOptions>>(TweenSettingsExtensions.OnComplete<TweenerCore<float, float, FloatOptions>>(DOTweenModuleUI.DOFillAmount(slider, 0.2f, 1.5f), (TweenCallback)delegate
		{
			//IL_0038: Unknown result type (might be due to invalid IL or missing references)
			//IL_003d: Unknown result type (might be due to invalid IL or missing references)
			//IL_003f: Expected O, but got Unknown
			//IL_0044: Expected O, but got Unknown
			if (NpcJieSuanManager.inst.isCanJieSuan)
			{
				TweenerCore<float, float, FloatOptions> obj = DOTweenModuleUI.DOFillAmount(slider, 1f, 0.5f);
				TweenCallback obj2 = val;
				if (obj2 == null)
				{
					TweenCallback val2 = delegate
					{
						Tools.canClickFlag = true;
						if (action != null)
						{
							action.Invoke();
						}
						Object.Destroy((Object)(object)((Component)this).gameObject);
					};
					TweenCallback val3 = val2;
					val = val2;
					obj2 = val3;
				}
				TweenExtensions.Play<TweenerCore<float, float, FloatOptions>>(TweenSettingsExtensions.OnComplete<TweenerCore<float, float, FloatOptions>>(obj, obj2));
			}
			else
			{
				isNeedJieSuan = true;
				this.obj = DOTweenModuleUI.DOFillAmount(slider, 0.98f, 20f);
			}
		}));
	}

	private void CallBack()
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Expected O, but got Unknown
		if (obj != null)
		{
			TweenExtensions.Kill((Tween)(object)obj, false);
		}
		TweenExtensions.Play<TweenerCore<float, float, FloatOptions>>(TweenSettingsExtensions.OnComplete<TweenerCore<float, float, FloatOptions>>(DOTweenModuleUI.DOFillAmount(slider, 1f, 0.5f * (1f - slider.fillAmount)), (TweenCallback)delegate
		{
			Tools.canClickFlag = true;
			if (call != null)
			{
				call.Invoke();
			}
			Object.Destroy((Object)(object)((Component)this).gameObject);
		}));
	}

	private void Update()
	{
		if (isNeedJieSuan && NpcJieSuanManager.inst.JieSuanAnimation)
		{
			NpcJieSuanManager.inst.JieSuanAnimation = false;
			CallBack();
		}
	}
}
