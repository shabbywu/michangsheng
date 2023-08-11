using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Fungus;

[CommandInfo("UI", "Fade UI", "Fades a UI object", 0)]
public class FadeUI : TweenUI
{
	[SerializeField]
	protected FadeMode fadeMode;

	[SerializeField]
	protected ColorData targetColor = new ColorData(Color.white);

	[SerializeField]
	protected FloatData targetAlpha = new FloatData(1f);

	protected override void ApplyTween(GameObject go)
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0364: Unknown result type (might be due to invalid IL or missing references)
		//IL_037f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0428: Unknown result type (might be due to invalid IL or missing references)
		//IL_0433: Unknown result type (might be due to invalid IL or missing references)
		//IL_0398: Unknown result type (might be due to invalid IL or missing references)
		//IL_0530: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d9: Unknown result type (might be due to invalid IL or missing references)
		Image[] componentsInChildren = go.GetComponentsInChildren<Image>();
		foreach (Image val in componentsInChildren)
		{
			if (Mathf.Approximately((float)duration, 0f))
			{
				switch (fadeMode)
				{
				case FadeMode.Alpha:
				{
					Color color = ((Graphic)val).color;
					color.a = targetAlpha;
					((Graphic)val).color = color;
					break;
				}
				case FadeMode.Color:
					((Graphic)val).color = targetColor;
					break;
				}
			}
			else
			{
				switch (fadeMode)
				{
				case FadeMode.Alpha:
					LeanTween.alpha(((Graphic)val).rectTransform, targetAlpha, duration).setEase(tweenType).setEase(tweenType);
					break;
				case FadeMode.Color:
					LeanTween.color(((Graphic)val).rectTransform, targetColor, duration).setEase(tweenType).setEase(tweenType);
					break;
				}
			}
		}
		Text[] componentsInChildren2 = go.GetComponentsInChildren<Text>();
		foreach (Text val2 in componentsInChildren2)
		{
			if (Mathf.Approximately((float)duration, 0f))
			{
				switch (fadeMode)
				{
				case FadeMode.Alpha:
				{
					Color color2 = ((Graphic)val2).color;
					color2.a = targetAlpha;
					((Graphic)val2).color = color2;
					break;
				}
				case FadeMode.Color:
					((Graphic)val2).color = targetColor;
					break;
				}
			}
			else
			{
				switch (fadeMode)
				{
				case FadeMode.Alpha:
					LeanTween.textAlpha(((Graphic)val2).rectTransform, targetAlpha, duration).setEase(tweenType);
					break;
				case FadeMode.Color:
					LeanTween.textColor(((Graphic)val2).rectTransform, targetColor, duration).setEase(tweenType);
					break;
				}
			}
		}
		TextMesh[] componentsInChildren3 = go.GetComponentsInChildren<TextMesh>();
		foreach (TextMesh val3 in componentsInChildren3)
		{
			if (Mathf.Approximately((float)duration, 0f))
			{
				switch (fadeMode)
				{
				case FadeMode.Alpha:
				{
					Color color3 = val3.color;
					color3.a = targetAlpha;
					val3.color = color3;
					break;
				}
				case FadeMode.Color:
					val3.color = targetColor;
					break;
				}
			}
			else
			{
				switch (fadeMode)
				{
				case FadeMode.Alpha:
					LeanTween.alpha(go, targetAlpha, duration).setEase(tweenType);
					break;
				case FadeMode.Color:
					LeanTween.color(go, targetColor, duration).setEase(tweenType);
					break;
				}
			}
		}
		TMP_Text[] componentsInChildren4 = go.GetComponentsInChildren<TMP_Text>();
		foreach (TMP_Text tmpro in componentsInChildren4)
		{
			if (Mathf.Approximately((float)duration, 0f))
			{
				switch (fadeMode)
				{
				case FadeMode.Alpha:
				{
					Color color4 = ((Graphic)tmpro).color;
					color4.a = targetAlpha;
					((Graphic)tmpro).color = color4;
					break;
				}
				case FadeMode.Color:
					((Graphic)tmpro).color = targetColor;
					break;
				}
				continue;
			}
			switch (fadeMode)
			{
			case FadeMode.Alpha:
				LeanTween.value(((Component)tmpro).gameObject, ((Graphic)tmpro).color.a, targetAlpha.Value, duration).setEase(tweenType).setOnUpdate(delegate(float alphaValue)
				{
					//IL_0006: Unknown result type (might be due to invalid IL or missing references)
					//IL_000b: Unknown result type (might be due to invalid IL or missing references)
					//IL_001a: Unknown result type (might be due to invalid IL or missing references)
					Color color5 = ((Graphic)tmpro).color;
					color5.a = alphaValue;
					((Graphic)tmpro).color = color5;
				});
				break;
			case FadeMode.Color:
				LeanTween.value(((Component)tmpro).gameObject, ((Graphic)tmpro).color, targetColor.Value, duration).setEase(tweenType).setOnUpdate(delegate(Color colorValue)
				{
					//IL_0006: Unknown result type (might be due to invalid IL or missing references)
					((Graphic)tmpro).color = colorValue;
				});
				break;
			}
		}
		CanvasGroup[] componentsInChildren5 = go.GetComponentsInChildren<CanvasGroup>();
		foreach (CanvasGroup val4 in componentsInChildren5)
		{
			if (Mathf.Approximately((float)duration, 0f))
			{
				switch (fadeMode)
				{
				case FadeMode.Alpha:
					val4.alpha = targetAlpha.Value;
					break;
				case FadeMode.Color:
					val4.alpha = targetColor.Value.a;
					break;
				}
			}
			else
			{
				switch (fadeMode)
				{
				case FadeMode.Alpha:
					LeanTween.alphaCanvas(val4, targetAlpha, duration).setEase(tweenType);
					break;
				case FadeMode.Color:
					LeanTween.alphaCanvas(val4, targetColor.Value.a, duration).setEase(tweenType);
					break;
				}
			}
		}
	}

	protected override string GetSummaryValue()
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		if (fadeMode == FadeMode.Alpha)
		{
			return targetAlpha.Value + " alpha";
		}
		if (fadeMode == FadeMode.Color)
		{
			Color value = targetColor.Value;
			return ((object)(Color)(ref value)).ToString() + " color";
		}
		return "";
	}

	public override bool IsPropertyVisible(string propertyName)
	{
		if (fadeMode == FadeMode.Alpha && propertyName == "targetColor")
		{
			return false;
		}
		if (fadeMode == FadeMode.Color && propertyName == "targetAlpha")
		{
			return false;
		}
		return true;
	}

	public override bool HasReference(Variable variable)
	{
		if (!((Object)(object)targetColor.colorRef == (Object)(object)variable) && !((Object)(object)targetAlpha.floatRef == (Object)(object)variable))
		{
			return base.HasReference(variable);
		}
		return true;
	}
}
