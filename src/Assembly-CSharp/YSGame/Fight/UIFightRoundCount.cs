using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

namespace YSGame.Fight
{
	// Token: 0x02000AC8 RID: 2760
	public class UIFightRoundCount : MonoBehaviour
	{
		// Token: 0x06004D64 RID: 19812 RVA: 0x0021149F File Offset: 0x0020F69F
		private void Awake()
		{
			this.rootRT = base.GetComponent<RectTransform>();
			this.moveRT = this.RoundCountBG.GetComponent<RectTransform>();
		}

		// Token: 0x06004D65 RID: 19813 RVA: 0x002114C0 File Offset: 0x0020F6C0
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

		// Token: 0x04004C83 RID: 19587
		public Image RoundCountBG;

		// Token: 0x04004C84 RID: 19588
		public Text RoundCountText;

		// Token: 0x04004C85 RID: 19589
		private bool nowMoving;

		// Token: 0x04004C86 RID: 19590
		private RectTransform moveRT;

		// Token: 0x04004C87 RID: 19591
		private RectTransform rootRT;

		// Token: 0x04004C88 RID: 19592
		private float tweenTime = 0.5f;

		// Token: 0x04004C89 RID: 19593
		private float moveOffset = 1f;
	}
}
