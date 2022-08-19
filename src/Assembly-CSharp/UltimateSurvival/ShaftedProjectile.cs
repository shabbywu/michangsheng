using System;
using System.Collections;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x02000607 RID: 1543
	[RequireComponent(typeof(Collider))]
	[RequireComponent(typeof(Rigidbody))]
	public class ShaftedProjectile : MonoBehaviour
	{
		// Token: 0x06003169 RID: 12649 RVA: 0x0015EE9C File Offset: 0x0015D09C
		public void Launch(EntityEventHandler entityThatLaunched)
		{
			if (this.m_Launched)
			{
				Debug.LogWarningFormat(this, "Already launched this projectile!", new object[]
				{
					base.name
				});
				return;
			}
			this.m_EntityThatLaunched = entityThatLaunched;
			this.m_Rigidbody.velocity = base.transform.forward * this.m_LaunchSpeed;
			this.m_Launched = true;
		}

		// Token: 0x0600316A RID: 12650 RVA: 0x0015EEFB File Offset: 0x0015D0FB
		private void Awake()
		{
			this.m_Collider = base.GetComponent<Collider>();
			this.m_Rigidbody = base.GetComponent<Rigidbody>();
			this.m_Collider.enabled = false;
		}

		// Token: 0x0600316B RID: 12651 RVA: 0x0015EF24 File Offset: 0x0015D124
		private void FixedUpdate()
		{
			if (this.m_Done)
			{
				return;
			}
			Ray ray;
			ray..ctor(base.transform.position, base.transform.forward);
			RaycastHit hitInfo;
			if (Physics.Raycast(ray, ref hitInfo, this.m_MaxDistance, this.m_Mask, 1))
			{
				SurfaceData surfaceData = ScriptableSingleton<SurfaceDatabase>.Instance.GetSurfaceData(hitInfo);
				float magnitude = this.m_Rigidbody.velocity.magnitude;
				if (magnitude >= this.m_PenetrationThreeshold && surfaceData != null)
				{
					surfaceData.PlaySound(ItemSelectionMethod.RandomlyButExcludeLast, this.m_PenetrationType, 1f, this.m_AudioSource);
					base.transform.SetParent(hitInfo.collider.transform, true);
					base.transform.position = hitInfo.point + base.transform.forward * this.m_PenetrationOffset;
					this.m_Pivot.localPosition = Vector3.back * this.m_PenetrationOffset;
					this.m_Model.SetParent(this.m_Pivot, true);
					float num = this.m_Rigidbody.mass * magnitude;
					IDamageable component = hitInfo.collider.GetComponent<IDamageable>();
					if (component != null)
					{
						HealthEventData damageData = new HealthEventData(-(this.m_MaxDamage * this.m_DamageCurve.Evaluate(1f - magnitude / this.m_LaunchSpeed)), this.m_EntityThatLaunched, hitInfo.point, ray.direction, num);
						component.ReceiveDamage(damageData);
					}
					else if (hitInfo.rigidbody)
					{
						hitInfo.rigidbody.AddForceAtPosition(base.transform.forward * num, base.transform.position, 1);
					}
					base.StartCoroutine(this.C_DoTwang());
					if (hitInfo.collider.GetComponent<HitBox>())
					{
						Collider[] array = Physics.OverlapSphere(base.transform.position, 1.5f, this.m_Mask, 1);
						for (int i = 0; i < array.Length; i++)
						{
							HitBox component2 = array[i].GetComponent<HitBox>();
							if (component2)
							{
								Physics.IgnoreCollision(component2.Collider, this.m_Collider);
							}
						}
					}
					Physics.IgnoreCollision(this.m_Collider, hitInfo.collider);
					this.m_Rigidbody.isKinematic = true;
				}
				else
				{
					this.m_HitAudio.Play(ItemSelectionMethod.Randomly, this.m_AudioSource, 1f);
					this.m_Rigidbody.isKinematic = false;
				}
				this.m_Collider.enabled = true;
				this.m_Collider.isTrigger = true;
				this.m_Done = true;
			}
		}

		// Token: 0x0600316C RID: 12652 RVA: 0x0015F1B4 File Offset: 0x0015D3B4
		private IEnumerator C_DoTwang()
		{
			float stopTime = Time.time + this.m_TwangDuration;
			float range = this.m_TwangRange;
			float currentVelocity = 0f;
			Quaternion randomRotation = Quaternion.Euler(new Vector2(Random.Range(-this.m_RandomRotation.x, this.m_RandomRotation.x), Random.Range(-this.m_RandomRotation.y, this.m_RandomRotation.y)));
			while (Time.time < stopTime)
			{
				this.m_Pivot.localRotation = randomRotation * Quaternion.Euler(Random.Range(-range, range), Random.Range(-range, range), 0f);
				range = Mathf.SmoothDamp(range, 0f, ref currentVelocity, stopTime - Time.time);
				yield return null;
			}
			yield break;
		}

		// Token: 0x04002B87 RID: 11143
		[Header("Setup")]
		[SerializeField]
		private Transform m_Model;

		// Token: 0x04002B88 RID: 11144
		[SerializeField]
		private Transform m_Pivot;

		// Token: 0x04002B89 RID: 11145
		[SerializeField]
		private LayerMask m_Mask;

		// Token: 0x04002B8A RID: 11146
		[SerializeField]
		private float m_MaxDistance = 2f;

		// Token: 0x04002B8B RID: 11147
		[Header("Launch")]
		[SerializeField]
		private float m_LaunchSpeed = 50f;

		// Token: 0x04002B8C RID: 11148
		[Header("Damage")]
		[SerializeField]
		private float m_MaxDamage = 100f;

		// Token: 0x04002B8D RID: 11149
		[SerializeField]
		[Tooltip("How the damage changes, when the speed gets lower.")]
		private AnimationCurve m_DamageCurve = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 1f),
			new Keyframe(0.8f, 0.5f),
			new Keyframe(1f, 0f)
		});

		// Token: 0x04002B8E RID: 11150
		[Header("Penetration")]
		[SerializeField]
		[Tooltip("The speed under which the projectile will not penetrate objects.")]
		private float m_PenetrationThreeshold = 20f;

		// Token: 0x04002B8F RID: 11151
		[SerializeField]
		private float m_PenetrationOffset = 0.2f;

		// Token: 0x04002B90 RID: 11152
		[SerializeField]
		private Vector2 m_RandomRotation;

		// Token: 0x04002B91 RID: 11153
		[Header("Twang")]
		[SerializeField]
		private float m_TwangDuration = 1f;

		// Token: 0x04002B92 RID: 11154
		[SerializeField]
		private float m_TwangRange = 18f;

		// Token: 0x04002B93 RID: 11155
		[Header("Audio")]
		[SerializeField]
		private AudioSource m_AudioSource;

		// Token: 0x04002B94 RID: 11156
		[SerializeField]
		private SoundPlayer m_HitAudio;

		// Token: 0x04002B95 RID: 11157
		[SerializeField]
		private SoundPlayer m_TwangAudio;

		// Token: 0x04002B96 RID: 11158
		[SerializeField]
		private SoundType m_PenetrationType;

		// Token: 0x04002B97 RID: 11159
		private EntityEventHandler m_EntityThatLaunched;

		// Token: 0x04002B98 RID: 11160
		private Collider m_Collider;

		// Token: 0x04002B99 RID: 11161
		private Rigidbody m_Rigidbody;

		// Token: 0x04002B9A RID: 11162
		private bool m_Done;

		// Token: 0x04002B9B RID: 11163
		private bool m_Launched;
	}
}
