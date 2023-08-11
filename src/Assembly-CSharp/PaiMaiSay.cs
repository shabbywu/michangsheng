using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PaiMaiSay : MonoBehaviour
{
	[SerializeField]
	private Text SayContent;

	private TweenerCore<float, float, FloatOptions> _do;

	private float _duringTime;

	public void SayWord(string msg, UnityAction complete = null, float time = 1f)
	{
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Expected O, but got Unknown
		if (_do != null)
		{
			TweenExtensions.Kill((Tween)(object)_do, false);
		}
		_duringTime = time;
		SayContent.text = msg;
		((Component)this).gameObject.SetActive(true);
		_do = TweenSettingsExtensions.OnComplete<TweenerCore<float, float, FloatOptions>>(DOTween.To((DOGetter<float>)(() => _duringTime), (DOSetter<float>)delegate(float x)
		{
			_duringTime = x;
		}, 0f, _duringTime), (TweenCallback)delegate
		{
			((Component)this).gameObject.SetActive(false);
			if (complete != null)
			{
				complete.Invoke();
			}
		});
	}
}
