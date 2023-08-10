using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

namespace YSGame.Fight;

public class UIFightRoundCount : MonoBehaviour
{
	public Image RoundCountBG;

	public Text RoundCountText;

	private bool nowMoving;

	private RectTransform moveRT;

	private RectTransform rootRT;

	private float tweenTime = 0.5f;

	private float moveOffset = 1f;

	private void Awake()
	{
		rootRT = ((Component)this).GetComponent<RectTransform>();
		moveRT = ((Component)RoundCountBG).GetComponent<RectTransform>();
	}

	public void ShowRuond(int i)
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Expected O, but got Unknown
		if (RoundManager.TuPoTypeList.Contains(Tools.instance.monstarMag.FightType) || nowMoving)
		{
			return;
		}
		nowMoving = true;
		((Graphic)RoundCountBG).color = new Color(1f, 1f, 1f, 0f);
		((Graphic)RoundCountText).color = new Color(((Graphic)RoundCountText).color.r, ((Graphic)RoundCountText).color.g, ((Graphic)RoundCountText).color.b, 0f);
		RoundCountText.text = $"第{i}回合";
		((Transform)moveRT).position = ((Transform)rootRT).position - new Vector3(moveOffset, 0f, 0f);
		TweenerCore<Vector3, Vector3, VectorOptions> obj = TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOMove((Transform)(object)moveRT, ((Transform)rootRT).position, tweenTime, false), (Ease)21);
		TweenSettingsExtensions.SetEase<TweenerCore<Color, Color, ColorOptions>>(DOTweenModuleUI.DOColor(RoundCountBG, Color.white, tweenTime), (Ease)21);
		TweenSettingsExtensions.SetEase<TweenerCore<Color, Color, ColorOptions>>(DOTweenModuleUI.DOColor(RoundCountText, new Color(((Graphic)RoundCountText).color.r, ((Graphic)RoundCountText).color.g, ((Graphic)RoundCountText).color.b, 1f), tweenTime), (Ease)21);
		((Tween)obj).onComplete = (TweenCallback)delegate
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_002d: Expected O, but got Unknown
			((Tween)ShortcutExtensions.DOMove((Transform)(object)moveRT, ((Transform)rootRT).position, 0.5f, false)).onComplete = (TweenCallback)delegate
			{
				//IL_000c: Unknown result type (might be due to invalid IL or missing references)
				//IL_0021: Unknown result type (might be due to invalid IL or missing references)
				//IL_0026: Unknown result type (might be due to invalid IL or missing references)
				//IL_0058: Unknown result type (might be due to invalid IL or missing references)
				//IL_007c: Unknown result type (might be due to invalid IL or missing references)
				//IL_008c: Unknown result type (might be due to invalid IL or missing references)
				//IL_009c: Unknown result type (might be due to invalid IL or missing references)
				//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
				//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
				//IL_00d4: Expected O, but got Unknown
				TweenerCore<Vector3, Vector3, VectorOptions> obj2 = TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOMove((Transform)(object)moveRT, ((Transform)rootRT).position + new Vector3(moveOffset, 0f, 0f), tweenTime, false), (Ease)20);
				TweenSettingsExtensions.SetEase<TweenerCore<Color, Color, ColorOptions>>(DOTweenModuleUI.DOColor(RoundCountBG, new Color(1f, 1f, 1f, 0f), tweenTime), (Ease)20);
				TweenSettingsExtensions.SetEase<TweenerCore<Color, Color, ColorOptions>>(DOTweenModuleUI.DOColor(RoundCountText, new Color(((Graphic)RoundCountText).color.r, ((Graphic)RoundCountText).color.g, ((Graphic)RoundCountText).color.b, 0f), tweenTime), (Ease)20);
				((Tween)obj2).onComplete = (TweenCallback)delegate
				{
					nowMoving = false;
				};
			};
		};
	}
}
