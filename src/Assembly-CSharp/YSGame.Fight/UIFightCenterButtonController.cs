using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

namespace YSGame.Fight;

public class UIFightCenterButtonController : MonoBehaviour
{
	[Serializable]
	[CompilerGenerated]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9 = new _003C_003Ec();

		public static UnityAction _003C_003E9__10_0;

		internal void _003Cset_ButtonType_003Eb__10_0()
		{
			RoundManager.instance.PlayerEndRound();
		}
	}

	public GameObject EndRoundBtnObj;

	public GameObject OnlyOKBtnObj;

	public GameObject OkCancelBtnObj;

	public FpBtn EndRoundBtn;

	public FpBtn OnlyOKBtn;

	public FpBtn OkBtn;

	public FpBtn CancelBtn;

	private UIFightCenterButtonType buttonType;

	public UIFightCenterButtonType ButtonType
	{
		get
		{
			return buttonType;
		}
		set
		{
			//IL_0051: Unknown result type (might be due to invalid IL or missing references)
			//IL_0056: Unknown result type (might be due to invalid IL or missing references)
			//IL_005c: Expected O, but got Unknown
			buttonType = value;
			AllHide();
			if (buttonType == UIFightCenterButtonType.EndRound)
			{
				EndRoundBtnObj.SetActive(true);
				((UnityEventBase)EndRoundBtn.mouseUpEvent).RemoveAllListeners();
				UnityEvent mouseUpEvent = EndRoundBtn.mouseUpEvent;
				object obj = _003C_003Ec._003C_003E9__10_0;
				if (obj == null)
				{
					UnityAction val = delegate
					{
						RoundManager.instance.PlayerEndRound();
					};
					_003C_003Ec._003C_003E9__10_0 = val;
					obj = (object)val;
				}
				mouseUpEvent.AddListener((UnityAction)obj);
			}
			else if (buttonType == UIFightCenterButtonType.OnlyOK)
			{
				OnlyOKBtnObj.SetActive(true);
			}
			else if (buttonType == UIFightCenterButtonType.OkCancel)
			{
				OkCancelBtnObj.SetActive(true);
			}
		}
	}

	private void Update()
	{
		if (Input.GetKeyUp((KeyCode)27))
		{
			if (ButtonType == UIFightCenterButtonType.OkCancel)
			{
				CancelBtn.mouseUpEvent.Invoke();
			}
			else if (ButtonType == UIFightCenterButtonType.EndRound)
			{
				EndRoundBtn.mouseUpEvent.Invoke();
			}
		}
		if (Input.GetKeyUp((KeyCode)32))
		{
			if (ButtonType == UIFightCenterButtonType.OkCancel)
			{
				OkBtn.mouseUpEvent.Invoke();
			}
			else if (ButtonType == UIFightCenterButtonType.OnlyOK)
			{
				OnlyOKBtn.mouseUpEvent.Invoke();
			}
		}
	}

	private void AllHide()
	{
		EndRoundBtnObj.SetActive(false);
		OnlyOKBtnObj.SetActive(false);
		OkCancelBtnObj.SetActive(false);
	}

	public void SetOkCancelEvent(UnityAction ok, UnityAction cancel)
	{
		((UnityEventBase)OkBtn.mouseUpEvent).RemoveAllListeners();
		OkBtn.mouseUpEvent.AddListener(ok);
		((UnityEventBase)CancelBtn.mouseUpEvent).RemoveAllListeners();
		CancelBtn.mouseUpEvent.AddListener(cancel);
	}

	public void SetOnlyOKEvent(UnityAction ok)
	{
		((UnityEventBase)OnlyOKBtn.mouseUpEvent).RemoveAllListeners();
		OnlyOKBtn.mouseUpEvent.AddListener(ok);
	}
}
