using UnityEngine;

namespace Fungus;

[CommandInfo("Audio", "Set Audio Volume", "Sets the global volume level for audio played with Play Music and Play Sound commands.", 0)]
[AddComponentMenu("")]
public class SetAudioVolume : Command
{
	[Range(0f, 1f)]
	[Tooltip("Global volume level for audio played using Play Music and Play Sound")]
	[SerializeField]
	protected float volume = 1f;

	[Range(0f, 30f)]
	[Tooltip("Time to fade between current volume level and target volume level.")]
	[SerializeField]
	protected float fadeDuration = 1f;

	[Tooltip("Wait until the volume fade has completed before continuing.")]
	[SerializeField]
	protected bool waitUntilFinished = true;

	public override void OnEnter()
	{
		FungusManager.Instance.MusicManager.SetAudioVolume(volume, fadeDuration, delegate
		{
			if (waitUntilFinished)
			{
				Continue();
			}
		});
		if (!waitUntilFinished)
		{
			Continue();
		}
	}

	public override string GetSummary()
	{
		return "Set to " + volume + " over " + fadeDuration + " seconds.";
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)242, (byte)209, (byte)176, byte.MaxValue));
	}
}
