using System;
using UnityEngine;

namespace Fungus;

[CommandInfo("Audio", "Set Audio Pitch", "Sets the global pitch level for audio played with Play Music and Play Sound commands.", 0)]
[AddComponentMenu("")]
public class SetAudioPitch : Command
{
	[Range(0f, 1f)]
	[Tooltip("Global pitch level for audio played using the Play Music and Play Sound commands")]
	[SerializeField]
	protected float pitch = 1f;

	[Range(0f, 30f)]
	[Tooltip("Time to fade between current pitch level and target pitch level.")]
	[SerializeField]
	protected float fadeDuration;

	[Tooltip("Wait until the pitch change has finished before executing next command")]
	[SerializeField]
	protected bool waitUntilFinished = true;

	public override void OnEnter()
	{
		Action onComplete = delegate
		{
			if (waitUntilFinished)
			{
				Continue();
			}
		};
		FungusManager.Instance.MusicManager.SetAudioPitch(pitch, fadeDuration, onComplete);
		if (!waitUntilFinished)
		{
			Continue();
		}
	}

	public override string GetSummary()
	{
		return "Set to " + pitch + " over " + fadeDuration + " seconds.";
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)242, (byte)209, (byte)176, byte.MaxValue));
	}
}
