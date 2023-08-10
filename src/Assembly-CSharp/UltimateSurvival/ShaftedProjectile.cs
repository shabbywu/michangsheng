using System.Collections;
using UnityEngine;

namespace UltimateSurvival;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class ShaftedProjectile : MonoBehaviour
{
	[Header("Setup")]
	[SerializeField]
	private Transform m_Model;

	[SerializeField]
	private Transform m_Pivot;

	[SerializeField]
	private LayerMask m_Mask;

	[SerializeField]
	private float m_MaxDistance = 2f;

	[Header("Launch")]
	[SerializeField]
	private float m_LaunchSpeed = 50f;

	[Header("Damage")]
	[SerializeField]
	private float m_MaxDamage = 100f;

	[SerializeField]
	[Tooltip("How the damage changes, when the speed gets lower.")]
	private AnimationCurve m_DamageCurve = new AnimationCurve((Keyframe[])(object)new Keyframe[3]
	{
		new Keyframe(0f, 1f),
		new Keyframe(0.8f, 0.5f),
		new Keyframe(1f, 0f)
	});

	[Header("Penetration")]
	[SerializeField]
	[Tooltip("The speed under which the projectile will not penetrate objects.")]
	private float m_PenetrationThreeshold = 20f;

	[SerializeField]
	private float m_PenetrationOffset = 0.2f;

	[SerializeField]
	private Vector2 m_RandomRotation;

	[Header("Twang")]
	[SerializeField]
	private float m_TwangDuration = 1f;

	[SerializeField]
	private float m_TwangRange = 18f;

	[Header("Audio")]
	[SerializeField]
	private AudioSource m_AudioSource;

	[SerializeField]
	private SoundPlayer m_HitAudio;

	[SerializeField]
	private SoundPlayer m_TwangAudio;

	[SerializeField]
	private SoundType m_PenetrationType;

	private EntityEventHandler m_EntityThatLaunched;

	private Collider m_Collider;

	private Rigidbody m_Rigidbody;

	private bool m_Done;

	private bool m_Launched;

	public void Launch(EntityEventHandler entityThatLaunched)
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		if (m_Launched)
		{
			Debug.LogWarningFormat((Object)(object)this, "Already launched this projectile!", new object[1] { ((Object)this).name });
		}
		else
		{
			m_EntityThatLaunched = entityThatLaunched;
			m_Rigidbody.velocity = ((Component)this).transform.forward * m_LaunchSpeed;
			m_Launched = true;
		}
	}

	private void Awake()
	{
		m_Collider = ((Component)this).GetComponent<Collider>();
		m_Rigidbody = ((Component)this).GetComponent<Rigidbody>();
		m_Collider.enabled = false;
	}

	private void FixedUpdate()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		if (m_Done)
		{
			return;
		}
		Ray val = default(Ray);
		((Ray)(ref val))._002Ector(((Component)this).transform.position, ((Component)this).transform.forward);
		RaycastHit hitInfo = default(RaycastHit);
		if (!Physics.Raycast(val, ref hitInfo, m_MaxDistance, LayerMask.op_Implicit(m_Mask), (QueryTriggerInteraction)1))
		{
			return;
		}
		SurfaceData surfaceData = ScriptableSingleton<SurfaceDatabase>.Instance.GetSurfaceData(hitInfo);
		Vector3 velocity = m_Rigidbody.velocity;
		float magnitude = ((Vector3)(ref velocity)).magnitude;
		if (magnitude >= m_PenetrationThreeshold && surfaceData != null)
		{
			surfaceData.PlaySound(ItemSelectionMethod.RandomlyButExcludeLast, m_PenetrationType, 1f, m_AudioSource);
			((Component)this).transform.SetParent(((Component)((RaycastHit)(ref hitInfo)).collider).transform, true);
			((Component)this).transform.position = ((RaycastHit)(ref hitInfo)).point + ((Component)this).transform.forward * m_PenetrationOffset;
			m_Pivot.localPosition = Vector3.back * m_PenetrationOffset;
			m_Model.SetParent(m_Pivot, true);
			float num = m_Rigidbody.mass * magnitude;
			IDamageable component = ((Component)((RaycastHit)(ref hitInfo)).collider).GetComponent<IDamageable>();
			if (component != null)
			{
				HealthEventData damageData = new HealthEventData(0f - m_MaxDamage * m_DamageCurve.Evaluate(1f - magnitude / m_LaunchSpeed), m_EntityThatLaunched, ((RaycastHit)(ref hitInfo)).point, ((Ray)(ref val)).direction, num);
				component.ReceiveDamage(damageData);
			}
			else if (Object.op_Implicit((Object)(object)((RaycastHit)(ref hitInfo)).rigidbody))
			{
				((RaycastHit)(ref hitInfo)).rigidbody.AddForceAtPosition(((Component)this).transform.forward * num, ((Component)this).transform.position, (ForceMode)1);
			}
			((MonoBehaviour)this).StartCoroutine(C_DoTwang());
			if (Object.op_Implicit((Object)(object)((Component)((RaycastHit)(ref hitInfo)).collider).GetComponent<HitBox>()))
			{
				Collider[] array = Physics.OverlapSphere(((Component)this).transform.position, 1.5f, LayerMask.op_Implicit(m_Mask), (QueryTriggerInteraction)1);
				for (int i = 0; i < array.Length; i++)
				{
					HitBox component2 = ((Component)array[i]).GetComponent<HitBox>();
					if (Object.op_Implicit((Object)(object)component2))
					{
						Physics.IgnoreCollision(component2.Collider, m_Collider);
					}
				}
			}
			Physics.IgnoreCollision(m_Collider, ((RaycastHit)(ref hitInfo)).collider);
			m_Rigidbody.isKinematic = true;
		}
		else
		{
			m_HitAudio.Play(ItemSelectionMethod.Randomly, m_AudioSource);
			m_Rigidbody.isKinematic = false;
		}
		m_Collider.enabled = true;
		m_Collider.isTrigger = true;
		m_Done = true;
	}

	private IEnumerator C_DoTwang()
	{
		float stopTime = Time.time + m_TwangDuration;
		float range = m_TwangRange;
		float currentVelocity = 0f;
		Quaternion randomRotation = Quaternion.Euler(Vector2.op_Implicit(new Vector2(Random.Range(0f - m_RandomRotation.x, m_RandomRotation.x), Random.Range(0f - m_RandomRotation.y, m_RandomRotation.y))));
		while (Time.time < stopTime)
		{
			m_Pivot.localRotation = randomRotation * Quaternion.Euler(Random.Range(0f - range, range), Random.Range(0f - range, range), 0f);
			range = Mathf.SmoothDamp(range, 0f, ref currentVelocity, stopTime - Time.time);
			yield return null;
		}
	}
}
