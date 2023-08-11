using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

namespace CaiJi;

public class CaiJiZhong : MonoBehaviour
{
	[SerializeField]
	private Image Fill;

	private TweenerCore<float, float, FloatOptions> obj1;

	private TweenerCore<float, float, FloatOptions> obj2;

	private void Awake()
	{
		MessageMag.Instance.Register("MSG_Npc_JieSuan_COMPLETE", Complete);
	}

	public void ShowSlider()
	{
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Expected O, but got Unknown
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Expected O, but got Unknown
		((Component)this).gameObject.SetActive(true);
		if (jsonData.instance.saveState == 1)
		{
			Debug.Log((object)"正在存档中播放假进度");
			TweenExtensions.Play<TweenerCore<float, float, FloatOptions>>(TweenSettingsExtensions.OnComplete<TweenerCore<float, float, FloatOptions>>(DOTweenModuleUI.DOFillAmount(Fill, 1f, 2.5f), (TweenCallback)delegate
			{
				((Component)this).gameObject.SetActive(false);
				Fill.fillAmount = 0f;
				if ((Object)(object)CaiJiUIMag.inst != (Object)null)
				{
					CaiJiUIMag.inst.CaiJiComplete();
				}
				if ((Object)(object)LingHeCaiJiUIMag.inst != (Object)null)
				{
					LingHeCaiJiUIMag.inst.CaiJiComplete();
				}
			}));
		}
		else
		{
			obj1 = TweenExtensions.Play<TweenerCore<float, float, FloatOptions>>(TweenSettingsExtensions.OnComplete<TweenerCore<float, float, FloatOptions>>(DOTweenModuleUI.DOFillAmount(Fill, 0.8f, 1f), (TweenCallback)delegate
			{
				obj2 = TweenExtensions.Play<TweenerCore<float, float, FloatOptions>>(DOTweenModuleUI.DOFillAmount(Fill, 0.95f, 20f));
			}));
		}
	}

	private void Complete(MessageData data)
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Expected O, but got Unknown
		if (obj1 != null)
		{
			TweenExtensions.Kill((Tween)(object)obj1, false);
		}
		if (obj2 != null)
		{
			TweenExtensions.Kill((Tween)(object)obj2, false);
		}
		TweenExtensions.Play<TweenerCore<float, float, FloatOptions>>(TweenSettingsExtensions.OnComplete<TweenerCore<float, float, FloatOptions>>(DOTweenModuleUI.DOFillAmount(Fill, 1f, 0.3f), (TweenCallback)delegate
		{
			((Component)this).gameObject.SetActive(false);
			Fill.fillAmount = 0f;
			if ((Object)(object)CaiJiUIMag.inst != (Object)null)
			{
				CaiJiUIMag.inst.CaiJiComplete();
			}
			if ((Object)(object)LingHeCaiJiUIMag.inst != (Object)null)
			{
				LingHeCaiJiUIMag.inst.CaiJiComplete();
			}
		}));
	}

	private void OnDestroy()
	{
		MessageMag.Instance.Remove("MSG_Npc_JieSuan_COMPLETE", Complete);
	}
}
