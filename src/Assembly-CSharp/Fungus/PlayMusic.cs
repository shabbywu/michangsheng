using UnityEngine;
using YSGame;

namespace Fungus;

[CommandInfo("Audio", "Play Music", "Plays looping game music. If any game music is already playing, it is stopped. Game music will continue playing across scene loads.", 0)]
[AddComponentMenu("")]
public class PlayMusic : Command
{
	[Tooltip("Music sound clip to play")]
	[SerializeField]
	protected AudioClip musicClip;

	[Tooltip("Time to begin playing in seconds. If the audio file is compressed, the time index may be inaccurate.")]
	[SerializeField]
	protected float atTime;

	[Tooltip("The music will start playing again at end.")]
	[SerializeField]
	protected bool loop = true;

	[Tooltip("Length of time to fade out previous playing music.")]
	[SerializeField]
	protected float fadeDuration = 1f;

	public override void OnEnter()
	{
		MusicManager musicManager = FungusManager.Instance.MusicManager;
		if ((Object)(object)MusicMag.instance != (Object)null)
		{
			MusicMag.instance.setFunguseMusice();
		}
		musicManager.PlayMusic(atTime: Mathf.Max(0f, atTime), musicClip: musicClip, loop: loop, fadeDuration: fadeDuration);
		Continue();
	}

	public override string GetSummary()
	{
		if ((Object)(object)musicClip == (Object)null)
		{
			return "Error: No music clip selected";
		}
		return ((Object)musicClip).name;
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)242, (byte)209, (byte)176, byte.MaxValue));
	}
}
