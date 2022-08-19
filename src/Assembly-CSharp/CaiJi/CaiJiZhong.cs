using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

namespace CaiJi
{
	// Token: 0x02000737 RID: 1847
	public class CaiJiZhong : MonoBehaviour
	{
		// Token: 0x06003AE8 RID: 15080 RVA: 0x001952B9 File Offset: 0x001934B9
		private void Awake()
		{
			MessageMag.Instance.Register("MSG_Npc_JieSuan_COMPLETE", new Action<MessageData>(this.Complete));
		}

		// Token: 0x06003AE9 RID: 15081 RVA: 0x001952D8 File Offset: 0x001934D8
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

		// Token: 0x06003AEA RID: 15082 RVA: 0x00195368 File Offset: 0x00193568
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

		// Token: 0x06003AEB RID: 15083 RVA: 0x001953C9 File Offset: 0x001935C9
		private void OnDestroy()
		{
			MessageMag.Instance.Remove("MSG_Npc_JieSuan_COMPLETE", new Action<MessageData>(this.Complete));
		}

		// Token: 0x04003313 RID: 13075
		[SerializeField]
		private Image Fill;

		// Token: 0x04003314 RID: 13076
		private TweenerCore<float, float, FloatOptions> obj1;

		// Token: 0x04003315 RID: 13077
		private TweenerCore<float, float, FloatOptions> obj2;
	}
}
