using UnityEngine;

namespace UltimateSurvival;

public class Lamp : InteractableObject
{
	public enum Mode
	{
		TimeOfDay_Dependent = 0,
		Manual = 5,
		Both = 10
	}

	[SerializeField]
	private Mode m_Mode;

	[SerializeField]
	private Light m_Light;

	[Header("Audio")]
	[SerializeField]
	private AudioSource m_AudioSource;

	[SerializeField]
	private SoundPlayer m_ToggleAudio;

	public bool State
	{
		get
		{
			if (!Object.op_Implicit((Object)(object)m_Light))
			{
				return false;
			}
			return ((Behaviour)m_Light).enabled;
		}
	}

	public override void OnInteract(PlayerEventHandler player)
	{
		if ((Object)(object)m_Light != (Object)null)
		{
			((Behaviour)m_Light).enabled = !((Behaviour)m_Light).enabled;
			m_ToggleAudio.Play(ItemSelectionMethod.RandomlyButExcludeLast, m_AudioSource);
		}
		else
		{
			Debug.LogError((object)"No Light component assigned to this Lamp!", (Object)(object)this);
		}
	}

	private void Start()
	{
		if (m_Mode == Mode.Both || m_Mode == Mode.TimeOfDay_Dependent)
		{
			MonoSingleton<TimeOfDay>.Instance.State.AddChangeListener(OnChanged_TimeOfDay_State);
		}
		OnChanged_TimeOfDay_State();
	}

	private void OnChanged_TimeOfDay_State()
	{
		if ((Object)(object)m_Light != (Object)null)
		{
			((Behaviour)m_Light).enabled = MonoSingleton<TimeOfDay>.Instance.State.Is(ET.TimeOfDay.Night);
			m_ToggleAudio.Play(ItemSelectionMethod.RandomlyButExcludeLast, m_AudioSource);
		}
		else
		{
			Debug.LogError((object)"No Light component assigned to this Lamp!", (Object)(object)this);
		}
	}

	private void OnDestroy()
	{
		if ((m_Mode == Mode.Both || m_Mode == Mode.TimeOfDay_Dependent) && (Object)(object)MonoSingleton<TimeOfDay>.Instance != (Object)null)
		{
			MonoSingleton<TimeOfDay>.Instance.State.RemoveChangeListener(OnChanged_TimeOfDay_State);
		}
	}
}
