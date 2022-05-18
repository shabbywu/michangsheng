using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Fungus
{
	// Token: 0x0200120B RID: 4619
	[CommandInfo("UI", "Fade UI", "Fades a UI object", 0)]
	public class FadeUI : TweenUI
	{
		// Token: 0x06007106 RID: 28934 RVA: 0x002A3D08 File Offset: 0x002A1F08
		protected override void ApplyTween(GameObject go)
		{
			foreach (Image image in go.GetComponentsInChildren<Image>())
			{
				if (Mathf.Approximately(this.duration, 0f))
				{
					FadeMode fadeMode = this.fadeMode;
					if (fadeMode != FadeMode.Alpha)
					{
						if (fadeMode == FadeMode.Color)
						{
							image.color = this.targetColor;
						}
					}
					else
					{
						Color color = image.color;
						color.a = this.targetAlpha;
						image.color = color;
					}
				}
				else
				{
					FadeMode fadeMode = this.fadeMode;
					if (fadeMode != FadeMode.Alpha)
					{
						if (fadeMode == FadeMode.Color)
						{
							LeanTween.color(image.rectTransform, this.targetColor, this.duration).setEase(this.tweenType).setEase(this.tweenType);
						}
					}
					else
					{
						LeanTween.alpha(image.rectTransform, this.targetAlpha, this.duration).setEase(this.tweenType).setEase(this.tweenType);
					}
				}
			}
			foreach (Text text in go.GetComponentsInChildren<Text>())
			{
				if (Mathf.Approximately(this.duration, 0f))
				{
					FadeMode fadeMode = this.fadeMode;
					if (fadeMode != FadeMode.Alpha)
					{
						if (fadeMode == FadeMode.Color)
						{
							text.color = this.targetColor;
						}
					}
					else
					{
						Color color2 = text.color;
						color2.a = this.targetAlpha;
						text.color = color2;
					}
				}
				else
				{
					FadeMode fadeMode = this.fadeMode;
					if (fadeMode != FadeMode.Alpha)
					{
						if (fadeMode == FadeMode.Color)
						{
							LeanTween.textColor(text.rectTransform, this.targetColor, this.duration).setEase(this.tweenType);
						}
					}
					else
					{
						LeanTween.textAlpha(text.rectTransform, this.targetAlpha, this.duration).setEase(this.tweenType);
					}
				}
			}
			foreach (TextMesh textMesh in go.GetComponentsInChildren<TextMesh>())
			{
				if (Mathf.Approximately(this.duration, 0f))
				{
					FadeMode fadeMode = this.fadeMode;
					if (fadeMode != FadeMode.Alpha)
					{
						if (fadeMode == FadeMode.Color)
						{
							textMesh.color = this.targetColor;
						}
					}
					else
					{
						Color color3 = textMesh.color;
						color3.a = this.targetAlpha;
						textMesh.color = color3;
					}
				}
				else
				{
					FadeMode fadeMode = this.fadeMode;
					if (fadeMode != FadeMode.Alpha)
					{
						if (fadeMode == FadeMode.Color)
						{
							LeanTween.color(go, this.targetColor, this.duration).setEase(this.tweenType);
						}
					}
					else
					{
						LeanTween.alpha(go, this.targetAlpha, this.duration).setEase(this.tweenType);
					}
				}
			}
			TMP_Text[] componentsInChildren4 = go.GetComponentsInChildren<TMP_Text>();
			for (int l = 0; l < componentsInChildren4.Length; l++)
			{
				TMP_Text tmpro = componentsInChildren4[l];
				if (Mathf.Approximately(this.duration, 0f))
				{
					FadeMode fadeMode = this.fadeMode;
					if (fadeMode != FadeMode.Alpha)
					{
						if (fadeMode == FadeMode.Color)
						{
							tmpro.color = this.targetColor;
						}
					}
					else
					{
						Color color4 = tmpro.color;
						color4.a = this.targetAlpha;
						tmpro.color = color4;
					}
				}
				else
				{
					FadeMode fadeMode = this.fadeMode;
					if (fadeMode != FadeMode.Alpha)
					{
						if (fadeMode == FadeMode.Color)
						{
							LeanTween.value(tmpro.gameObject, tmpro.color, this.targetColor.Value, this.duration).setEase(this.tweenType).setOnUpdate(delegate(Color colorValue)
							{
								tmpro.color = colorValue;
							});
						}
					}
					else
					{
						LeanTween.value(tmpro.gameObject, tmpro.color.a, this.targetAlpha.Value, this.duration).setEase(this.tweenType).setOnUpdate(delegate(float alphaValue)
						{
							Color color5 = tmpro.color;
							color5.a = alphaValue;
							tmpro.color = color5;
						});
					}
				}
			}
			foreach (CanvasGroup canvasGroup in go.GetComponentsInChildren<CanvasGroup>())
			{
				if (Mathf.Approximately(this.duration, 0f))
				{
					FadeMode fadeMode = this.fadeMode;
					if (fadeMode != FadeMode.Alpha)
					{
						if (fadeMode == FadeMode.Color)
						{
							canvasGroup.alpha = this.targetColor.Value.a;
						}
					}
					else
					{
						canvasGroup.alpha = this.targetAlpha.Value;
					}
				}
				else
				{
					FadeMode fadeMode = this.fadeMode;
					if (fadeMode != FadeMode.Alpha)
					{
						if (fadeMode == FadeMode.Color)
						{
							LeanTween.alphaCanvas(canvasGroup, this.targetColor.Value.a, this.duration).setEase(this.tweenType);
						}
					}
					else
					{
						LeanTween.alphaCanvas(canvasGroup, this.targetAlpha, this.duration).setEase(this.tweenType);
					}
				}
			}
		}

		// Token: 0x06007107 RID: 28935 RVA: 0x002A427C File Offset: 0x002A247C
		protected override string GetSummaryValue()
		{
			if (this.fadeMode == FadeMode.Alpha)
			{
				return this.targetAlpha.Value.ToString() + " alpha";
			}
			if (this.fadeMode == FadeMode.Color)
			{
				return this.targetColor.Value.ToString() + " color";
			}
			return "";
		}

		// Token: 0x06007108 RID: 28936 RVA: 0x0004CC61 File Offset: 0x0004AE61
		public override bool IsPropertyVisible(string propertyName)
		{
			return (this.fadeMode != FadeMode.Alpha || !(propertyName == "targetColor")) && (this.fadeMode != FadeMode.Color || !(propertyName == "targetAlpha"));
		}

		// Token: 0x06007109 RID: 28937 RVA: 0x0004CC93 File Offset: 0x0004AE93
		public override bool HasReference(Variable variable)
		{
			return this.targetColor.colorRef == variable || this.targetAlpha.floatRef == variable || base.HasReference(variable);
		}

		// Token: 0x04006362 RID: 25442
		[SerializeField]
		protected FadeMode fadeMode;

		// Token: 0x04006363 RID: 25443
		[SerializeField]
		protected ColorData targetColor = new ColorData(Color.white);

		// Token: 0x04006364 RID: 25444
		[SerializeField]
		protected FloatData targetAlpha = new FloatData(1f);
	}
}
