using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020004ED RID: 1261
public class PaiMaiSay : MonoBehaviour
{
	// Token: 0x060020DC RID: 8412 RVA: 0x001145A0 File Offset: 0x001127A0
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

	// Token: 0x04001C5A RID: 7258
	[SerializeField]
	private Text SayContent;

	// Token: 0x04001C5B RID: 7259
	private TweenerCore<float, float, FloatOptions> _do;

	// Token: 0x04001C5C RID: 7260
	private float _duringTime;
}
