using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

public class UIPopTipItem : MonoBehaviour
{
	public Image IconImage;

	public Text MsgText;

	private int msgIndex;

	private RectTransform RT;

	private static float tweenDur = 0.5f;

	private static float scaleNum = 0.9f;

	public int MsgIndex
	{
		get
		{
			return msgIndex;
		}
		set
		{
			msgIndex = value;
			if (msgIndex >= 9)
			{
				Object.Destroy((Object)(object)((Component)this).gameObject);
			}
			else
			{
				MovePos();
			}
		}
	}

	private void Awake()
	{
		ref RectTransform rT = ref RT;
		Transform transform = ((Component)this).transform;
		rT = (RectTransform)(object)((transform is RectTransform) ? transform : null);
	}

	private void MovePos()
	{
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		if (MsgIndex == 0)
		{
			TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale((Transform)(object)RT, Vector3.one, tweenDur), (Ease)18);
			TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTweenModuleUI.DOAnchorPosX(RT, 0f, tweenDur, false), (Ease)18);
		}
		else
		{
			float num = 0f - (57f + (float)(MsgIndex - 1) * (57f * scaleNum));
			ShortcutExtensions.DOScale((Transform)(object)RT, new Vector3(scaleNum, scaleNum, 1f), tweenDur);
			DOTweenModuleUI.DOAnchorPosY(RT, num, tweenDur, false);
		}
	}

	public void TweenDestory()
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		((Tween)TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTweenModuleUI.DOAnchorPosX(RT, 500f, 1f, false), (Ease)14)).onComplete = (TweenCallback)delegate
		{
			Object.Destroy((Object)(object)((Component)this).gameObject);
		};
	}
}
