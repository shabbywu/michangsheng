using UnityEngine;

namespace UltimateSurvival.AI;

public class AISettings : AIBehaviour
{
	[SerializeField]
	private EntityMovement m_Movement;

	[SerializeField]
	private EntityDetection m_Detection;

	[SerializeField]
	private EntityVitals m_Vitals;

	[Header("Combat")]
	[SerializeField]
	[Clamp(0f, 500f)]
	private float m_HitDamage = 25f;

	[SerializeField]
	[Clamp(0f, 3f)]
	private float m_MaxAttackDistance = 2f;

	[Header("Audio")]
	[SerializeField]
	private AudioSource m_AudioSource;

	[SerializeField]
	private SoundPlayer m_AttackSounds;

	private EntityAnimation m_Animation;

	private AIBrain m_Brain;

	public EntityMovement Movement => m_Movement;

	public EntityDetection Detection => m_Detection;

	public EntityVitals Vitals => m_Vitals;

	public EntityAnimation Animation => m_Animation;

	public AudioSource AudioSource => m_AudioSource;

	public void OnAnimationDamage()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		EntityEventHandler component = m_Detection.LastChasedTarget.GetComponent<EntityEventHandler>();
		bool flag = Vector3.Distance(((Component)component).transform.position, ((Component)this).transform.position) < m_MaxAttackDistance;
		bool flag2 = Vector3.Angle(m_Detection.LastChasedTarget.transform.position - ((Component)this).transform.position, ((Component)this).transform.forward) < 60f;
		if ((Object)(object)component != (Object)null && flag && flag2)
		{
			component.ChangeHealth.Try(new HealthEventData(0f - m_HitDamage, base.Entity, ((Component)this).transform.position + Vector3.up + ((Component)this).transform.forward * 0.5f, ((Component)component).transform.position - ((Component)this).transform.position));
			Collider component2 = ((Component)component).GetComponent<Collider>();
			if ((Object)(object)component2 != (Object)null)
			{
				ScriptableSingleton<SurfaceDatabase>.Instance.GetSurfaceData(component2, ((Component)component).transform.position + Vector3.up, 0)?.PlaySound(ItemSelectionMethod.RandomlyButExcludeLast, SoundType.Hit, 1f, ((Component)component).transform.position + Vector3.up * 1.5f);
			}
		}
	}

	public void PlayAttackSounds()
	{
		m_AttackSounds.Play(ItemSelectionMethod.RandomlyButExcludeLast, m_AudioSource);
	}

	private void Start()
	{
		m_Brain = ((Component)this).GetComponent<AIBrain>();
		m_Movement.Initialize(m_Brain);
		m_Detection.Initialize(((Component)this).transform);
		m_Animation = new EntityAnimation();
		m_Animation.Initialize(m_Brain);
	}

	private void Update()
	{
		m_Movement.Update(((Component)this).transform);
		m_Detection.Update(m_Brain);
		m_Vitals.Update(m_Brain);
	}
}
