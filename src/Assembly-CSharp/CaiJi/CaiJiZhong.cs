using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

namespace CaiJi
{
	// Token: 0x02000A9F RID: 2719
	public class CaiJiZhong : MonoBehaviour
	{
		// Token: 0x060045B7 RID: 17847 RVA: 0x00031E05 File Offset: 0x00030005
		private void Awake()
		{
			MessageMag.Instance.Register("MSG_Npc_JieSuan_COMPLETE", new Action<MessageData>(this.Complete));
		}

		// Token: 0x060045B8 RID: 17848 RVA: 0x001DCEB0 File Offset: 0x001DB0B0
		public void ShowSlider()
		{
			base.gameObject.SetActive(true);
			if (jsonData.instance.saveState == 1)
			{
				Debug.Log("正在存档中播放假进度");
				TweenExtensions.Play<TweenerCore<float, float, FloatOptions>>(TweenSettingsExtensions.OnComplete<TweenerCore<float, float, FloatOptions>>(DOTweenModuleUI.DOFillAmount(this.Fill, 1f, 2.5f), delegate()
				{
					base.gameObject.SetActive(false);
					this.Fill.fillAmount = 0f;
					if (CaiJiUIMag.inst != null)
					{
						CaiJiUIMag.inst.CaiJiComplete();
					}
					if (LingHeCaiJiUIMag.inst != null)
					{
						LingHeCaiJiUIMag.inst.CaiJiComplete();
					}
				}));
				return;
			}
			this.obj1 = TweenExtensions.Play<TweenerCore<float, float, FloatOptions>>(TweenSettingsExtensions.OnComplete<TweenerCore<float, float, FloatOptions>>(DOTweenModuleUI.DOFillAmount(this.Fill, 0.8f, 1f), delegate()
			{
				this.obj2 = TweenExtensions.Play<TweenerCore<float, float, FloatOptions>>(DOTweenModuleUI.DOFillAmount(this.Fill, 0.95f, 20f));
			}));
		}

		// Token: 0x060045B9 RID: 17849 RVA: 0x001DCF40 File Offset: 0x001DB140
		private void Complete(MessageData data)
		{
			if (this.obj1 != null)
			{
				TweenExtensions.Kill(this.obj1, false);
			}
			if (this.obj2 != null)
			{
				TweenExtensions.Kill(this.obj2, false);
			}
			TweenExtensions.Play<TweenerCore<float, float, FloatOptions>>(TweenSettingsExtensions.OnComplete<TweenerCore<float, float, FloatOptions>>(DOTweenModuleUI.DOFillAmount(this.Fill, 1f, 0.3f), delegate()
			{
				base.gameObject.SetActive(false);
				this.Fill.fillAmount = 0f;
				if (CaiJiUIMag.inst != null)
				{
					CaiJiUIMag.inst.CaiJiComplete();
				}
				if (LingHeCaiJiUIMag.inst != null)
				{
					LingHeCaiJiUIMag.inst.CaiJiComplete();
				}
			}));
		}

		// Token: 0x060045BA RID: 17850 RVA: 0x00031E22 File Offset: 0x00030022
		private void OnDestroy()
		{
			MessageMag.Instance.Remove("MSG_Npc_JieSuan_COMPLETE", new Action<MessageData>(this.Complete));
		}

		// Token: 0x04003DE4 RID: 15844
		[SerializeField]
		private Image Fill;

		// Token: 0x04003DE5 RID: 15845
		private TweenerCore<float, float, FloatOptions> obj1;

		// Token: 0x04003DE6 RID: 15846
		private TweenerCore<float, float, FloatOptions> obj2;
	}
}
