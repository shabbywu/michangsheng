using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000371 RID: 881
public class PaiMaiSay : MonoBehaviour
{
	// Token: 0x06001D7B RID: 7547 RVA: 0x000D04B0 File Offset: 0x000CE6B0
	public void SayWord(string msg, UnityAction complete = null, float time = 1f)
	{
		if (this._do != null)
		{
			TweenExtensions.Kill(this._do, false);
		}
		this._duringTime = time;
		this.SayContent.text = msg;
		base.gameObject.SetActive(true);
		this._do = TweenSettingsExtensions.OnComplete<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this._duringTime, delegate(float x)
		{
			this._duringTime = x;
		}, 0f, this._duringTime), delegate()
		{
			this.gameObject.SetActive(false);
			if (complete != null)
			{
				complete.Invoke();
			}
		});
	}

	// Token: 0x0400180F RID: 6159
	[SerializeField]
	private Text SayContent;

	// Token: 0x04001810 RID: 6160
	private TweenerCore<float, float, FloatOptions> _do;

	// Token: 0x04001811 RID: 6161
	private float _duringTime;
}
