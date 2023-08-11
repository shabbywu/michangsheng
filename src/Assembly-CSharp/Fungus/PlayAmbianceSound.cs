using UnityEngine;

namespace Fungus;

[CommandInfo("Audio", "Play Ambiance Sound", "Plays a background sound to be overlayed on top of the music. Only one Ambiance can be played at a time.", 0)]
[AddComponentMenu("")]
public class PlayAmbianceSound : Command
{
	[Tooltip("Sound effect clip to play")]
	[SerializeField]
	protected AudioClip soundClip;

	[Range(0f, 1f)]
	[Tooltip("Volume level of the sound effect")]
	[SerializeField]
	protected float volume = 1f;

	[Tooltip("Sound effect clip to play")]
	[SerializeField]
	protected bool loop;

	protected virtual void DoWait()
	{
		Continue();
	}

	public override void OnEnter()
	{
		if ((Object)(object)soundClip == (Object)null)
		{
			Continue();
			return;
		}
		FungusManager.Instance.MusicManager.PlayAmbianceSound(soundClip, loop, volume);
		Continue();
	}

	public override string GetSummary()
	{
		if ((Object)(object)soundClip == (Object)null)
		{
			return "Error: No sound clip selected";
		}
		return ((Object)soundClip).name;
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)242, (byte)209, (byte)176, byte.MaxValue));
	}
}
