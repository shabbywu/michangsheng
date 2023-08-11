using UnityEngine;

namespace UltimateSurvival;

public class FootstepPlayer : PlayerBehaviour
{
	[Header("General")]
	[SerializeField]
	private ItemSelectionMethod m_FootstepSelectionMethod;

	[SerializeField]
	private float m_LandSpeedThreeshold = 3f;

	[SerializeField]
	private LayerMask m_Mask;

	[SerializeField]
	private AudioSource m_AudioSource;

	[Header("Distance Between Steps")]
	[SerializeField]
	private float m_WalkStepLength = 1.7f;

	[SerializeField]
	private float m_RunStepLength = 2f;

	[Header("Volume Factors")]
	[SerializeField]
	private float m_WalkVolumeFactor = 0.5f;

	[SerializeField]
	private float m_RunVolumeFactor = 1f;

	private AudioSource m_LeftFootSource;

	private AudioSource m_RightFootSource;

	private FootType m_LastFroundedFoot;

	private float m_AccumulatedDistance;

	private void Start()
	{
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		base.Player.Jump.AddStartListener(OnStart_PlayerJump);
		base.Player.Land.AddListener(On_PlayerLanded);
		m_LeftFootSource = GameController.Audio.CreateAudioSource("Left Foot Footstep", ((Component)this).transform, new Vector3(-0.2f, 0f, 0f), is2D: false, 1f, 3f);
		m_RightFootSource = GameController.Audio.CreateAudioSource("Right Foot Footstep", ((Component)this).transform, new Vector3(0.2f, 0f, 0f), is2D: false, 1f, 3f);
	}

	private void FixedUpdate()
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		if (base.Player.IsGrounded.Get())
		{
			float accumulatedDistance = m_AccumulatedDistance;
			Vector3 val = base.Player.Velocity.Get();
			m_AccumulatedDistance = accumulatedDistance + ((Vector3)(ref val)).magnitude * Time.fixedDeltaTime;
			float stepLength = GetStepLength();
			if (m_AccumulatedDistance > stepLength)
			{
				PlayFootstep();
				m_AccumulatedDistance = 0f;
			}
		}
	}

	private void PlayFootstep()
	{
		SurfaceData dataFromBelow = GetDataFromBelow();
		if (dataFromBelow != null)
		{
			AudioSource val = null;
			val = ((m_LastFroundedFoot == FootType.Left) ? m_RightFootSource : m_LeftFootSource);
			m_LastFroundedFoot = ((m_LastFroundedFoot == FootType.Left) ? FootType.Right : FootType.Left);
			float volumeFactor = GetVolumeFactor();
			dataFromBelow.PlaySound(m_FootstepSelectionMethod, SoundType.Footstep, volumeFactor, val);
		}
	}

	private float GetStepLength()
	{
		float result = m_WalkStepLength;
		if (base.Player.Run.Active)
		{
			result = m_RunStepLength;
		}
		return result;
	}

	private float GetVolumeFactor()
	{
		float result = m_WalkVolumeFactor;
		if (base.Player.Run.Active)
		{
			result = m_RunVolumeFactor;
		}
		return result;
	}

	private void OnStart_PlayerJump()
	{
		GetDataFromBelow()?.PlaySound(ItemSelectionMethod.RandomlyButExcludeLast, SoundType.Jump, 1f, m_AudioSource);
	}

	private void On_PlayerLanded(float landSpeed)
	{
		SurfaceData dataFromBelow = GetDataFromBelow();
		if (dataFromBelow != null && landSpeed >= m_LandSpeedThreeshold)
		{
			dataFromBelow.PlaySound(ItemSelectionMethod.RandomlyButExcludeLast, SoundType.Land, 1f, m_AudioSource);
			m_AccumulatedDistance = 0f;
		}
	}

	private SurfaceData GetDataFromBelow()
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		if (!Object.op_Implicit((Object)(object)GameController.SurfaceDatabase))
		{
			Debug.LogWarning((object)"No surface database found! can't play any footsteps...", (Object)(object)this);
			return null;
		}
		Ray ray = default(Ray);
		((Ray)(ref ray))._002Ector(((Component)this).transform.position + Vector3.up * 0.1f, Vector3.down);
		return GameController.SurfaceDatabase.GetSurfaceData(ray, 1f, m_Mask);
	}
}
