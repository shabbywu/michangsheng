using UnityEngine;
using YSGame;

namespace Fungus;

[CommandInfo("Audio", "Play Sound", "Plays a once-off sound effect. Multiple sound effects can be played at the same time.", 0)]
[AddComponentMenu("")]
public class PlaySound : Command
{
	[Tooltip("Sound effect clip to play")]
	[SerializeField]
	protected AudioClip soundClip;

	[Range(0f, 1f)]
	[Tooltip("Volume level of the sound effect")]
	[SerializeField]
	protected float volume = 1f;

	[Tooltip("Wait until the sound has finished playing before continuing execution.")]
	[SerializeField]
	protected bool waitUntilFinished;

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
		FungusManager.Instance.MusicManager.PlaySound(soundClip, volume);
		if ((Object)(object)MusicMag.instance != (Object)null)
		{
			MusicMag.instance.setFunguseMusice();
		}
		if (waitUntilFinished)
		{
			((MonoBehaviour)this).Invoke("DoWait", soundClip.length);
		}
		else
		{
			Continue();
		}
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
