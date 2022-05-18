using System;
using System.Collections;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020008E5 RID: 2277
	[RequireComponent(typeof(Collider))]
	[RequireComponent(typeof(Rigidbody))]
	public class ShaftedProjectile : MonoBehaviour
	{
		// Token: 0x06003A72 RID: 14962 RVA: 0x001A8294 File Offset: 0x001A6494
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

		// Token: 0x06003A73 RID: 14963 RVA: 0x0002A79D File Offset: 0x0002899D
		private void Awake()
		{
			this.m_Collider = base.GetComponent<Collider>();
			this.m_Rigidbody = base.GetComponent<Rigidbody>();
			this.m_Collider.enabled = false;
		}

		// Token: 0x06003A74 RID: 14964 RVA: 0x001A82F4 File Offset: 0x001A64F4
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

		// Token: 0x06003A75 RID: 14965 RVA: 0x0002A7C3 File Offset: 0x000289C3
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

		// Token: 0x04003485 RID: 13445
		[Header("Setup")]
		[SerializeField]
		private Transform m_Model;

		// Token: 0x04003486 RID: 13446
		[SerializeField]
		private Transform m_Pivot;

		// Token: 0x04003487 RID: 13447
		[SerializeField]
		private LayerMask m_Mask;

		// Token: 0x04003488 RID: 13448
		[SerializeField]
		private float m_MaxDistance = 2f;

		// Token: 0x04003489 RID: 13449
		[Header("Launch")]
		[SerializeField]
		private float m_LaunchSpeed = 50f;

		// Token: 0x0400348A RID: 13450
		[Header("Damage")]
		[SerializeField]
		private float m_MaxDamage = 100f;

		// Token: 0x0400348B RID: 13451
		[SerializeField]
		[Tooltip("How the damage changes, when the speed gets lower.")]
		private AnimationCurve m_DamageCurve = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 1f),
			new Keyframe(0.8f, 0.5f),
			new Keyframe(1f, 0f)
		});

		// Token: 0x0400348C RID: 13452
		[Header("Penetration")]
		[SerializeField]
		[Tooltip("The speed under which the projectile will not penetrate objects.")]
		private float m_PenetrationThreeshold = 20f;

		// Token: 0x0400348D RID: 13453
		[SerializeField]
		private float m_PenetrationOffset = 0.2f;

		// Token: 0x0400348E RID: 13454
		[SerializeField]
		private Vector2 m_RandomRotation;

		// Token: 0x0400348F RID: 13455
		[Header("Twang")]
		[SerializeField]
		private float m_TwangDuration = 1f;

		// Token: 0x04003490 RID: 13456
		[SerializeField]
		private float m_TwangRange = 18f;

		// Token: 0x04003491 RID: 13457
		[Header("Audio")]
		[SerializeField]
		private AudioSource m_AudioSource;

		// Token: 0x04003492 RID: 13458
		[SerializeField]
		private SoundPlayer m_HitAudio;

		// Token: 0x04003493 RID: 13459
		[SerializeField]
		private SoundPlayer m_TwangAudio;

		// Token: 0x04003494 RID: 13460
		[SerializeField]
		private SoundType m_PenetrationType;

		// Token: 0x04003495 RID: 13461
		private EntityEventHandler m_EntityThatLaunched;

		// Token: 0x04003496 RID: 13462
		private Collider m_Collider;

		// Token: 0x04003497 RID: 13463
		private Rigidbody m_Rigidbody;

		// Token: 0x04003498 RID: 13464
		private bool m_Done;

		// Token: 0x04003499 RID: 13465
		private bool m_Launched;
	}
}
