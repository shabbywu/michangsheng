using System;
using UnityEngine;

namespace Fungus;

[EventHandlerInfo("MonoBehaviour", "Application", "The block will execute when the desired OnApplication message for the monobehaviour is received.")]
[AddComponentMenu("")]
public class ApplicationState : EventHandler
{
	[Flags]
	public enum ApplicationMessageFlags
	{
		OnApplicationGetFocus = 1,
		OnApplicationLoseFocus = 2,
		OnApplicationPause = 4,
		OnApplicationResume = 8,
		OnApplicationQuit = 0x10
	}

	[Tooltip("Which of the Application messages to trigger on.")]
	[SerializeField]
	[EnumFlag]
	protected ApplicationMessageFlags FireOn = ApplicationMessageFlags.OnApplicationQuit;

	private void OnApplicationFocus(bool focus)
	{
		if ((focus && (FireOn & ApplicationMessageFlags.OnApplicationGetFocus) != 0) || (!focus && (FireOn & ApplicationMessageFlags.OnApplicationLoseFocus) != 0))
		{
			ExecuteBlock();
		}
	}

	private void OnApplicationPause(bool pause)
	{
		if ((pause && (FireOn & ApplicationMessageFlags.OnApplicationPause) != 0) || (!pause && (FireOn & ApplicationMessageFlags.OnApplicationResume) != 0))
		{
			ExecuteBlock();
		}
	}

	private void OnApplicationQuit()
	{
		if ((FireOn & ApplicationMessageFlags.OnApplicationQuit) != 0)
		{
			ExecuteBlock();
		}
	}
}
