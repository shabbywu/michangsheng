using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

namespace YSGame.Fight
{
	// Token: 0x02000E06 RID: 3590
	public class UIFightRoundCount : MonoBehaviour
	{
		// Token: 0x060056B3 RID: 22195 RVA: 0x0003DF71 File Offset: 0x0003C171
		private void Awake()
		{
			this.rootRT = base.GetComponent<RectTransform>();
			this.moveRT = this.RoundCountBG.GetComponent<RectTransform>();
		}

		// Token: 0x060056B4 RID: 22196 RVA: 0x00241844 File Offset: 0x0023FA44
		public void ShowRuond(int i)
		{
			if (RoundManager.TuPoTypeList.Contains(Tools.instance.monstarMag.FightType))
			{
				return;
			}
			if (!this.nowMoving)
			{
				this.nowMoving = true;
				this.RoundCountBG.color = new Color(1f, 1f, 1f, 0f);
				this.RoundCountText.color = new Color(this.RoundCountText.color.r, this.RoundCountText.color.g, this.RoundCountText.color.b, 0f);
				this.RoundCountText.text = string.Format("第{0}回合", i);
				this.moveRT.position = this.rootRT.position - new Vector3(this.moveOffset, 0f, 0f);
				Tween tween = TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOMove(this.moveRT, this.rootRT.position, this.tweenTime, false), 21);
				TweenSettingsExtensions.SetEase<TweenerCore<Color, Color, ColorOptions>>(DOTweenModuleUI.DOColor(this.RoundCountBG, Color.white, this.tweenTime), 21);
				TweenSettingsExtensions.SetEase<TweenerCore<Color, Color, ColorOptions>>(DOTweenModuleUI.DOColor(this.RoundCountText, new Color(this.RoundCountText.color.r, this.RoundCountText.color.g, this.RoundCountText.color.b, 1f), this.tweenTime), 21);
				tween.onComplete = delegate()
				{
					ShortcutExtensions.DOMove(this.moveRT, this.rootRT.position, 0.5f, false).onComplete = delegate()
					{
						Tween tween2 = TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOMove(this.moveRT, this.rootRT.position + new Vector3(this.moveOffset, 0f, 0f), this.tweenTime, false), 20);
						TweenSettingsExtensions.SetEase<TweenerCore<Color, Color, ColorOptions>>(DOTweenModuleUI.DOColor(this.RoundCountBG, new Color(1f, 1f, 1f, 0f), this.tweenTime), 20);
						TweenSettingsExtensions.SetEase<TweenerCore<Color, Color, ColorOptions>>(DOTweenModuleUI.DOColor(this.RoundCountText, new Color(this.RoundCountText.color.r, this.RoundCountText.color.g, this.RoundCountText.color.b, 0f), this.tweenTime), 20);
						tween2.onComplete = delegate()
						{
							this.nowMoving = false;
						};
					};
				};
			}
		}

		// Token: 0x0400565D RID: 22109
		public Image RoundCountBG;

		// Token: 0x0400565E RID: 22110
		public Text RoundCountText;

		// Token: 0x0400565F RID: 22111
		private bool nowMoving;

		// Token: 0x04005660 RID: 22112
		private RectTransform moveRT;

		// Token: 0x04005661 RID: 22113
		private RectTransform rootRT;

		// Token: 0x04005662 RID: 22114
		private float tweenTime = 0.5f;

		// Token: 0x04005663 RID: 22115
		private float moveOffset = 1f;
	}
}
