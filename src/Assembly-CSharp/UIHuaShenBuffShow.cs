using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

public class UIHuaShenBuffShow : MonoBehaviour
{
	public enum UIHuaShenBuffShowType
	{
		变换高度,
		闪烁
	}

	public Text NumText;

	public Image EffectImage;

	public UIHuaShenBuffShowType ShowType;

	private float zeroHeight = -117f;

	private int nowNum;

	private AvatarShowHpDamage showHpDamage;

	private void Awake()
	{
		showHpDamage = ((Component)this).GetComponent<AvatarShowHpDamage>();
		NumText.text = "0";
	}

	public void SetNumber(int num)
	{
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Expected O, but got Unknown
		if (num == nowNum)
		{
			return;
		}
		int num2 = num - nowNum;
		showHpDamage.show(-num2);
		nowNum = num;
		NumText.text = num.ToString();
		if (ShowType == UIHuaShenBuffShowType.变换高度)
		{
			float num3 = (0f - zeroHeight) / 100f * (float)num + zeroHeight;
			DOTweenModuleUI.DOAnchorPosY(((Graphic)EffectImage).rectTransform, num3, 1f, false);
		}
		else
		{
			((Tween)TweenSettingsExtensions.SetEase<TweenerCore<Color, Color, ColorOptions>>(DOTweenModuleUI.DOColor(EffectImage, Color.white, 1f), (Ease)18)).onComplete = (TweenCallback)delegate
			{
				//IL_001a: Unknown result type (might be due to invalid IL or missing references)
				TweenSettingsExtensions.SetEase<TweenerCore<Color, Color, ColorOptions>>(DOTweenModuleUI.DOColor(EffectImage, new Color(1f, 1f, 1f, 0f), 1f), (Ease)20);
			};
		}
	}
}
