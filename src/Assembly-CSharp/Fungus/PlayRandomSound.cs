using UnityEngine;

namespace Fungus;

[CommandInfo("Audio", "Play Random Sound", "Plays a once-off sound effect from a list of available sound effects. Multiple sound effects can be played at the same time.", 0)]
[AddComponentMenu("")]
public class PlayRandomSound : Command
{
	[Tooltip("Sound effect clip to play")]
	[SerializeField]
	protected AudioClip[] soundClip;

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
		int num = Random.Range(0, soundClip.Length);
		if (soundClip == null)
		{
			Continue();
			return;
		}
		FungusManager.Instance.MusicManager.PlaySound(soundClip[num], volume);
		if (waitUntilFinished)
		{
			((MonoBehaviour)this).Invoke("DoWait", soundClip[num].length);
		}
		else
		{
			Continue();
		}
	}

	public override string GetSummary()
	{
		if (soundClip == null)
		{
			return "Error: No sound clip selected";
		}
		string text = "[";
		AudioClip[] array = soundClip;
		foreach (AudioClip val in array)
		{
			if ((Object)(object)val != (Object)null)
			{
				text = text + ((Object)val).name + " ,";
			}
		}
		text = text.TrimEnd(' ', ',');
		text += "]";
		return "Random sounds " + text;
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)242, (byte)209, (byte)176, byte.MaxValue));
	}
}
