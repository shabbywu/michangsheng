using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

namespace YSGame.Fight;

public class UIFightCenterTip : MonoBehaviour
{
	public RectTransform Move;

	public RectTransform StartPoint;

	public RectTransform EndPoint;

	public GameObject YiSanObj;

	public Text YiSanCountText;

	public Text NormalTipText;

	private bool isYiSan;

	private bool isShow;

	public void ShowYiSan(int count)
	{
		YiSanCountText.text = count.ToString();
		if (!isYiSan)
		{
			isYiSan = true;
			((Component)NormalTipText).gameObject.SetActive(false);
			YiSanObj.SetActive(true);
			ShowTip();
		}
		else if (!isShow)
		{
			ShowTip();
		}
	}

	public void ShowNormalTip(string tip)
	{
		NormalTipText.text = tip;
		if (isYiSan)
		{
			isYiSan = false;
			((Component)NormalTipText).gameObject.SetActive(true);
			YiSanObj.SetActive(false);
		}
		ShowTip();
	}

	private void ShowTip()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		isShow = true;
		((Transform)Move).position = ((Transform)StartPoint).position;
		TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTweenModuleUI.DOAnchorPos(Move, Vector2.zero, 0.5f, false), (Ease)18);
	}

	public void HideTip()
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		if (isShow)
		{
			isShow = false;
			TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOMove((Transform)(object)Move, ((Transform)EndPoint).position, 0.5f, false), (Ease)18);
		}
	}
}
