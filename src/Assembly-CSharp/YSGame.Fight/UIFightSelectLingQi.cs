using System;
using UnityEngine;
using UnityEngine.Events;

namespace YSGame.Fight;

public class UIFightSelectLingQi : MonoBehaviour
{
	public FpBtn Jin;

	public FpBtn Mu;

	public FpBtn Shui;

	public FpBtn Huo;

	public FpBtn Tu;

	private Action<LingQiType> onSelectLingQi;

	private void Awake()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Expected O, but got Unknown
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Expected O, but got Unknown
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Expected O, but got Unknown
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Expected O, but got Unknown
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Expected O, but got Unknown
		Jin.mouseUpEvent.AddListener((UnityAction)delegate
		{
			OnSelectLingQi(LingQiType.金);
		});
		Mu.mouseUpEvent.AddListener((UnityAction)delegate
		{
			OnSelectLingQi(LingQiType.木);
		});
		Shui.mouseUpEvent.AddListener((UnityAction)delegate
		{
			OnSelectLingQi(LingQiType.水);
		});
		Huo.mouseUpEvent.AddListener((UnityAction)delegate
		{
			OnSelectLingQi(LingQiType.火);
		});
		Tu.mouseUpEvent.AddListener((UnityAction)delegate
		{
			OnSelectLingQi(LingQiType.土);
		});
	}

	public void SetSelectAction(Action<LingQiType> callback)
	{
		onSelectLingQi = callback;
	}

	private void OnSelectLingQi(LingQiType lingQiType)
	{
		((Component)this).gameObject.SetActive(false);
		if (onSelectLingQi != null)
		{
			onSelectLingQi(lingQiType);
		}
	}
}
