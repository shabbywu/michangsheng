using UnityEngine;

namespace UltimateSurvival;

public class FPMelee : FPWeaponBase
{
	public enum HitInvokeMethod
	{
		ByTimer,
		ByAnimationEvent
	}

	[Header("Melee Setup")]
	[SerializeField]
	[Tooltip("The animation event handler - that picks up animation events from Mecanim.")]
	private FPMeleeEventHandler m_EventHandler;

	[Header("Melee Settings")]
	[SerializeField]
	[Tooltip("From how far can this object hit stuff?")]
	private float m_MaxReach = 0.5f;

	[SerializeField]
	[Tooltip("Useful for limiting the number of hits you can do in a period of time.")]
	private float m_TimeBetweenAttacks = 0.85f;

	[SerializeField]
	[Range(0f, 1000f)]
	private float m_DamagePerHit = 15f;

	[Range(0f, 1000f)]
	[SerializeField]
	private float m_ImpactForce = 15f;

	[Header("Audio")]
	[SerializeField]
	private AudioSource m_AudioSource;

	[SerializeField]
	private SoundPlayer m_AttackAudio;

	[SerializeField]
	private SoundType m_SoundType;

	private Message<bool> m_MeleeAttack = new Message<bool>();

	private Message m_Miss = new Message();

	private Message m_Hit = new Message();

	private float m_NextUseTime;

	public Message<bool> MeleeAttack => m_MeleeAttack;

	public Message Miss => m_Miss;

	public Message Hit => m_Hit;

	public float MaxReach => m_MaxReach;

	public float DamagePerHit => m_DamagePerHit;

	public override bool TryAttackOnce(Camera camera)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		if (Time.time < m_NextUseTime)
		{
			return false;
		}
		RaycastData raycastData = base.Player.RaycastData.Get();
		if ((bool)raycastData)
		{
			RaycastHit hitInfo = raycastData.HitInfo;
			bool message = ((RaycastHit)(ref hitInfo)).distance < m_MaxReach;
			MeleeAttack.Send(message);
		}
		else
		{
			MeleeAttack.Send(message: false);
		}
		base.Attack.Send();
		m_NextUseTime = Time.time + m_TimeBetweenAttacks;
		return true;
	}

	public override bool TryAttackContinuously(Camera camera)
	{
		return TryAttackOnce(camera);
	}

	protected virtual void Start()
	{
		m_EventHandler.Hit.AddListener(On_Hit);
		m_EventHandler.Woosh.AddListener(On_Woosh);
	}

	protected virtual void On_Hit()
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		RaycastData raycastData = base.Player.RaycastData.Get();
		if (!raycastData)
		{
			return;
		}
		if (Object.op_Implicit((Object)(object)GameController.SurfaceDatabase))
		{
			SurfaceData surfaceData = GameController.SurfaceDatabase.GetSurfaceData(raycastData.HitInfo);
			SoundType soundType = m_SoundType;
			RaycastHit hitInfo = raycastData.HitInfo;
			surfaceData.PlaySound(ItemSelectionMethod.Randomly, soundType, 1f, ((RaycastHit)(ref hitInfo)).point);
			if (m_SoundType == SoundType.Hit)
			{
				hitInfo = raycastData.HitInfo;
				Vector3 point = ((RaycastHit)(ref hitInfo)).point;
				hitInfo = raycastData.HitInfo;
				surfaceData.CreateHitFX(point, Quaternion.LookRotation(((RaycastHit)(ref hitInfo)).normal));
			}
			else if (m_SoundType == SoundType.Chop)
			{
				hitInfo = raycastData.HitInfo;
				Vector3 point2 = ((RaycastHit)(ref hitInfo)).point;
				hitInfo = raycastData.HitInfo;
				surfaceData.CreateChopFX(point2, Quaternion.LookRotation(((RaycastHit)(ref hitInfo)).normal));
			}
		}
		IDamageable component = raycastData.GameObject.GetComponent<IDamageable>();
		if (component != null)
		{
			HealthEventData damageData = new HealthEventData(0f - m_DamagePerHit, base.Player, ((Component)this).transform.position, ((Component)GameController.WorldCamera).transform.forward, m_ImpactForce);
			component.ReceiveDamage(damageData);
		}
		if (m_Durability != null)
		{
			ItemProperty.Float @float = m_Durability.Float;
			@float.Current--;
			m_Durability.SetValue(ItemProperty.Type.Float, @float);
			if (@float.Current == 0f)
			{
				base.Player.DestroyEquippedItem.Try();
			}
		}
		Hit.Send();
	}

	private void On_Woosh()
	{
		m_AttackAudio.Play(ItemSelectionMethod.Randomly, m_AudioSource, 0.6f);
		Miss.Send();
	}
}
