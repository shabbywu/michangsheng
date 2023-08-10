using System.Collections;
using UnityEngine;

namespace UltimateSurvival;

public class FPHitscan : FPWeaponBase
{
	public enum ShellSpawnMethod
	{
		Auto,
		OnAnimationEvent
	}

	[Header("Gun Setup")]
	[SerializeField]
	private ET.FireMode m_FireMode;

	[SerializeField]
	[Tooltip("The layers that will be affected when you fire.")]
	private LayerMask m_Mask;

	[SerializeField]
	[Tooltip("If something is farther than this distance threeshold, it will not be affected by the shot.")]
	private float m_MaxDistance = 150f;

	[Header("Fire Mode.Semi Auto")]
	[SerializeField]
	[Tooltip("The minimum time that can pass between consecutive shots.")]
	private float m_FireDuration = 0.22f;

	[Header("Fire Mode.Burst")]
	[SerializeField]
	[Tooltip("How many shots will the gun fire when in Burst-mode.")]
	private int m_BurstLength = 3;

	[SerializeField]
	[Tooltip("How much time it takes to fire all the shots.")]
	private float m_BurstDuration = 0.3f;

	[SerializeField]
	[Tooltip("The minimum time that can pass between consecutive bursts.")]
	private float m_BurstPause = 0.35f;

	[Header("Fire Mode.Full Auto")]
	[SerializeField]
	[Tooltip("The maximum amount of shots that can be executed in a minute.")]
	private int m_RoundsPerMinute = 450;

	[Header("Gun Settings")]
	[Range(1f, 20f)]
	[SerializeField]
	[Tooltip("The amount of rays that will be sent in the world (basically the amount of projectiles / bullets that will be fired at a time).")]
	private int m_RayCount = 1;

	[Range(0f, 30f)]
	[SerializeField]
	[Tooltip("When NOT aiming, how much the projectiles will spread (in angles).")]
	private float m_NormalSpread = 0.8f;

	[SerializeField]
	[Range(0f, 30f)]
	[Tooltip("When aiming, how much the projectiles will spread (in angles).")]
	private float m_AimSpread = 0.95f;

	[SerializeField]
	[Tooltip("")]
	private RayImpact m_RayImpact;

	[Header("Audio")]
	[SerializeField]
	private AudioSource m_AudioSource;

	[SerializeField]
	[Tooltip("Sounds that will play when firing the gun.")]
	private SoundPlayer m_FireAudio;

	[Header("Effects")]
	[SerializeField]
	private ParticleSystem m_MuzzleFlash;

	[SerializeField]
	private GameObject m_Tracer;

	[SerializeField]
	private Vector3 m_TracerOffset;

	[Header("Shell")]
	[SerializeField]
	private Rigidbody m_ShellPrefab;

	[SerializeField]
	private ShellSpawnMethod m_ShellSpawnMethod;

	[SerializeField]
	private FPHitscanEventHandler m_AnimEventHandler;

	[SerializeField]
	private Transform m_WeaponRoot;

	[SerializeField]
	private Vector3 m_ShellSpawnOffset;

	[SerializeField]
	private Vector3 m_ShellSpawnVelocity = new Vector3(3f, 2f, 0.3f);

	[SerializeField]
	private float m_ShellSpin = 0.3f;

	private float m_MinTimeBetweenShots;

	private float m_NextTimeCanFire;

	private WaitForSeconds m_BurstWait;

	public override bool TryAttackOnce(Camera camera)
	{
		if (Time.time < m_NextTimeCanFire || !base.IsEnabled)
		{
			return false;
		}
		m_NextTimeCanFire = Time.time + m_MinTimeBetweenShots;
		if (m_FireMode == ET.FireMode.Burst)
		{
			((MonoBehaviour)this).StartCoroutine(C_DoBurst(camera));
		}
		else
		{
			Shoot(camera);
		}
		return true;
	}

	public override bool TryAttackContinuously(Camera camera)
	{
		if (m_FireMode == ET.FireMode.SemiAuto)
		{
			return false;
		}
		return TryAttackOnce(camera);
	}

	protected IEnumerator C_DoBurst(Camera camera)
	{
		for (int i = 0; i < m_BurstLength; i++)
		{
			Shoot(camera);
			yield return m_BurstWait;
		}
	}

	protected void Shoot(Camera camera)
	{
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		m_FireAudio.Play(ItemSelectionMethod.Randomly, m_AudioSource);
		if (Object.op_Implicit((Object)(object)m_MuzzleFlash))
		{
			m_MuzzleFlash.Play(true);
		}
		for (int i = 0; i < m_RayCount; i++)
		{
			DoHitscan(camera);
		}
		base.Attack.Send();
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
		GameController.Audio.LastGunshot.Set(new Gunshot(((Component)this).transform.position, base.Player));
	}

	protected void DoHitscan(Camera camera)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		float num = (base.Player.Aim.Active ? m_AimSpread : m_NormalSpread);
		Ray val = camera.ViewportPointToRay(Vector2.op_Implicit(Vector2.one * 0.5f));
		Vector3 val2 = ((Component)camera).transform.TransformVector(new Vector3(Random.Range(0f - num, num), Random.Range(0f - num, num), 0f));
		((Ray)(ref val)).direction = Quaternion.Euler(val2) * ((Ray)(ref val)).direction;
		RaycastHit hitInfo = default(RaycastHit);
		if (Physics.Raycast(val, ref hitInfo, m_MaxDistance, LayerMask.op_Implicit(m_Mask), (QueryTriggerInteraction)1))
		{
			float impulseAtDistance = m_RayImpact.GetImpulseAtDistance(((RaycastHit)(ref hitInfo)).distance, m_MaxDistance);
			float damageAtDistance = m_RayImpact.GetDamageAtDistance(((RaycastHit)(ref hitInfo)).distance, m_MaxDistance);
			IDamageable component = ((Component)((RaycastHit)(ref hitInfo)).collider).GetComponent<IDamageable>();
			if (component != null)
			{
				HealthEventData damageData = new HealthEventData(0f - damageAtDistance, base.Player, ((RaycastHit)(ref hitInfo)).point, ((Ray)(ref val)).direction, impulseAtDistance);
				component.ReceiveDamage(damageData);
			}
			else if (Object.op_Implicit((Object)(object)((RaycastHit)(ref hitInfo)).rigidbody))
			{
				((RaycastHit)(ref hitInfo)).rigidbody.AddForceAtPosition(((Ray)(ref val)).direction * impulseAtDistance, ((RaycastHit)(ref hitInfo)).point, (ForceMode)1);
			}
			if (Object.op_Implicit((Object)(object)GameController.SurfaceDatabase))
			{
				SurfaceData surfaceData = GameController.SurfaceDatabase.GetSurfaceData(hitInfo);
				surfaceData.CreateBulletDecal(((RaycastHit)(ref hitInfo)).point + ((RaycastHit)(ref hitInfo)).normal * 0.01f, Quaternion.LookRotation(((RaycastHit)(ref hitInfo)).normal), ((Component)((RaycastHit)(ref hitInfo)).collider).transform);
				surfaceData.CreateBulletImpactFX(((RaycastHit)(ref hitInfo)).point + ((RaycastHit)(ref hitInfo)).normal * 0.01f, Quaternion.LookRotation(((RaycastHit)(ref hitInfo)).normal));
				surfaceData.PlaySound(ItemSelectionMethod.Randomly, SoundType.BulletImpact, 1f, ((RaycastHit)(ref hitInfo)).point);
			}
		}
		if (Object.op_Implicit((Object)(object)m_Tracer))
		{
			Object.Instantiate<GameObject>(m_Tracer, ((Component)this).transform.position + ((Component)this).transform.TransformVector(m_TracerOffset), Quaternion.LookRotation(((Ray)(ref val)).direction));
		}
		if (Object.op_Implicit((Object)(object)m_ShellPrefab) && m_ShellSpawnMethod == ShellSpawnMethod.Auto)
		{
			SpawnShell();
		}
	}

	private void Start()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Expected O, but got Unknown
		m_BurstWait = new WaitForSeconds(m_BurstDuration / (float)m_BurstLength);
		if (m_FireMode == ET.FireMode.SemiAuto)
		{
			m_MinTimeBetweenShots = m_FireDuration;
		}
		else if (m_FireMode == ET.FireMode.Burst)
		{
			m_MinTimeBetweenShots = m_BurstDuration + m_BurstPause;
		}
		else
		{
			m_MinTimeBetweenShots = 60f / (float)m_RoundsPerMinute;
		}
		if (m_ShellSpawnMethod == ShellSpawnMethod.OnAnimationEvent && (Object)(object)m_AnimEventHandler != (Object)null)
		{
			m_AnimEventHandler.AnimEvent_SpawnObject.AddListener(On_AnimEvent_SpawnObject);
		}
	}

	private void OnValidate()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Expected O, but got Unknown
		m_BurstWait = new WaitForSeconds(m_BurstDuration / (float)m_BurstLength);
		if (m_FireMode == ET.FireMode.SemiAuto)
		{
			m_MinTimeBetweenShots = m_FireDuration;
		}
		else if (m_FireMode == ET.FireMode.Burst)
		{
			m_MinTimeBetweenShots = m_BurstDuration + m_BurstPause;
		}
		else
		{
			m_MinTimeBetweenShots = 60f / (float)m_RoundsPerMinute;
		}
	}

	private void On_AnimEvent_SpawnObject(string name)
	{
		if (name == "Shell")
		{
			SpawnShell();
		}
	}

	private void SpawnShell()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		Rigidbody component = ((Component)Object.Instantiate<Rigidbody>(m_ShellPrefab, m_WeaponRoot.position + m_WeaponRoot.TransformVector(m_ShellSpawnOffset), Random.rotation)).GetComponent<Rigidbody>();
		component.angularVelocity = new Vector3(Random.Range(0f - m_ShellSpin, m_ShellSpin), Random.Range(0f - m_ShellSpin, m_ShellSpin), Random.Range(0f - m_ShellSpin, m_ShellSpin));
		component.velocity = ((Component)this).transform.TransformVector(m_ShellSpawnVelocity);
	}
}
