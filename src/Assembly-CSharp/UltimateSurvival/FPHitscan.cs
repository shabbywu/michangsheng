using System;
using System.Collections;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005E8 RID: 1512
	public class FPHitscan : FPWeaponBase
	{
		// Token: 0x060030A1 RID: 12449 RVA: 0x0015BF18 File Offset: 0x0015A118
		public override bool TryAttackOnce(Camera camera)
		{
			if (Time.time < this.m_NextTimeCanFire || !base.IsEnabled)
			{
				return false;
			}
			this.m_NextTimeCanFire = Time.time + this.m_MinTimeBetweenShots;
			if (this.m_FireMode == ET.FireMode.Burst)
			{
				base.StartCoroutine(this.C_DoBurst(camera));
			}
			else
			{
				this.Shoot(camera);
			}
			return true;
		}

		// Token: 0x060030A2 RID: 12450 RVA: 0x0015BF6F File Offset: 0x0015A16F
		public override bool TryAttackContinuously(Camera camera)
		{
			return this.m_FireMode != ET.FireMode.SemiAuto && this.TryAttackOnce(camera);
		}

		// Token: 0x060030A3 RID: 12451 RVA: 0x0015BF82 File Offset: 0x0015A182
		protected IEnumerator C_DoBurst(Camera camera)
		{
			int num;
			for (int i = 0; i < this.m_BurstLength; i = num + 1)
			{
				this.Shoot(camera);
				yield return this.m_BurstWait;
				num = i;
			}
			yield break;
		}

		// Token: 0x060030A4 RID: 12452 RVA: 0x0015BF98 File Offset: 0x0015A198
		protected void Shoot(Camera camera)
		{
			this.m_FireAudio.Play(ItemSelectionMethod.Randomly, this.m_AudioSource, 1f);
			if (this.m_MuzzleFlash)
			{
				this.m_MuzzleFlash.Play(true);
			}
			for (int i = 0; i < this.m_RayCount; i++)
			{
				this.DoHitscan(camera);
			}
			base.Attack.Send();
			if (this.m_Durability != null)
			{
				ItemProperty.Float @float = this.m_Durability.Float;
				float num = @float.Current;
				@float.Current = num - 1f;
				this.m_Durability.SetValue(ItemProperty.Type.Float, @float);
				if (@float.Current == 0f)
				{
					base.Player.DestroyEquippedItem.Try();
				}
			}
			GameController.Audio.LastGunshot.Set(new Gunshot(base.transform.position, base.Player));
		}

		// Token: 0x060030A5 RID: 12453 RVA: 0x0015C078 File Offset: 0x0015A278
		protected void DoHitscan(Camera camera)
		{
			float num = base.Player.Aim.Active ? this.m_AimSpread : this.m_NormalSpread;
			Ray ray = camera.ViewportPointToRay(Vector2.one * 0.5f);
			Vector3 vector = camera.transform.TransformVector(new Vector3(Random.Range(-num, num), Random.Range(-num, num), 0f));
			ray.direction = Quaternion.Euler(vector) * ray.direction;
			RaycastHit hitInfo;
			if (Physics.Raycast(ray, ref hitInfo, this.m_MaxDistance, this.m_Mask, 1))
			{
				float impulseAtDistance = this.m_RayImpact.GetImpulseAtDistance(hitInfo.distance, this.m_MaxDistance);
				float damageAtDistance = this.m_RayImpact.GetDamageAtDistance(hitInfo.distance, this.m_MaxDistance);
				IDamageable component = hitInfo.collider.GetComponent<IDamageable>();
				if (component != null)
				{
					HealthEventData damageData = new HealthEventData(-damageAtDistance, base.Player, hitInfo.point, ray.direction, impulseAtDistance);
					component.ReceiveDamage(damageData);
				}
				else if (hitInfo.rigidbody)
				{
					hitInfo.rigidbody.AddForceAtPosition(ray.direction * impulseAtDistance, hitInfo.point, 1);
				}
				if (GameController.SurfaceDatabase)
				{
					SurfaceData surfaceData = GameController.SurfaceDatabase.GetSurfaceData(hitInfo);
					surfaceData.CreateBulletDecal(hitInfo.point + hitInfo.normal * 0.01f, Quaternion.LookRotation(hitInfo.normal), hitInfo.collider.transform);
					surfaceData.CreateBulletImpactFX(hitInfo.point + hitInfo.normal * 0.01f, Quaternion.LookRotation(hitInfo.normal), null);
					surfaceData.PlaySound(ItemSelectionMethod.Randomly, SoundType.BulletImpact, 1f, hitInfo.point);
				}
			}
			if (this.m_Tracer)
			{
				Object.Instantiate<GameObject>(this.m_Tracer, base.transform.position + base.transform.TransformVector(this.m_TracerOffset), Quaternion.LookRotation(ray.direction));
			}
			if (this.m_ShellPrefab && this.m_ShellSpawnMethod == FPHitscan.ShellSpawnMethod.Auto)
			{
				this.SpawnShell();
			}
		}

		// Token: 0x060030A6 RID: 12454 RVA: 0x0015C2BC File Offset: 0x0015A4BC
		private void Start()
		{
			this.m_BurstWait = new WaitForSeconds(this.m_BurstDuration / (float)this.m_BurstLength);
			if (this.m_FireMode == ET.FireMode.SemiAuto)
			{
				this.m_MinTimeBetweenShots = this.m_FireDuration;
			}
			else if (this.m_FireMode == ET.FireMode.Burst)
			{
				this.m_MinTimeBetweenShots = this.m_BurstDuration + this.m_BurstPause;
			}
			else
			{
				this.m_MinTimeBetweenShots = 60f / (float)this.m_RoundsPerMinute;
			}
			if (this.m_ShellSpawnMethod == FPHitscan.ShellSpawnMethod.OnAnimationEvent && this.m_AnimEventHandler != null)
			{
				this.m_AnimEventHandler.AnimEvent_SpawnObject.AddListener(new Action<string>(this.On_AnimEvent_SpawnObject));
			}
		}

		// Token: 0x060030A7 RID: 12455 RVA: 0x0015C35C File Offset: 0x0015A55C
		private void OnValidate()
		{
			this.m_BurstWait = new WaitForSeconds(this.m_BurstDuration / (float)this.m_BurstLength);
			if (this.m_FireMode == ET.FireMode.SemiAuto)
			{
				this.m_MinTimeBetweenShots = this.m_FireDuration;
				return;
			}
			if (this.m_FireMode == ET.FireMode.Burst)
			{
				this.m_MinTimeBetweenShots = this.m_BurstDuration + this.m_BurstPause;
				return;
			}
			this.m_MinTimeBetweenShots = 60f / (float)this.m_RoundsPerMinute;
		}

		// Token: 0x060030A8 RID: 12456 RVA: 0x0015C3C7 File Offset: 0x0015A5C7
		private void On_AnimEvent_SpawnObject(string name)
		{
			if (name == "Shell")
			{
				this.SpawnShell();
			}
		}

		// Token: 0x060030A9 RID: 12457 RVA: 0x0015C3DC File Offset: 0x0015A5DC
		private void SpawnShell()
		{
			Rigidbody component = Object.Instantiate<Rigidbody>(this.m_ShellPrefab, this.m_WeaponRoot.position + this.m_WeaponRoot.TransformVector(this.m_ShellSpawnOffset), Random.rotation).GetComponent<Rigidbody>();
			component.angularVelocity = new Vector3(Random.Range(-this.m_ShellSpin, this.m_ShellSpin), Random.Range(-this.m_ShellSpin, this.m_ShellSpin), Random.Range(-this.m_ShellSpin, this.m_ShellSpin));
			component.velocity = base.transform.TransformVector(this.m_ShellSpawnVelocity);
		}

		// Token: 0x04002AC2 RID: 10946
		[Header("Gun Setup")]
		[SerializeField]
		private ET.FireMode m_FireMode;

		// Token: 0x04002AC3 RID: 10947
		[SerializeField]
		[Tooltip("The layers that will be affected when you fire.")]
		private LayerMask m_Mask;

		// Token: 0x04002AC4 RID: 10948
		[SerializeField]
		[Tooltip("If something is farther than this distance threeshold, it will not be affected by the shot.")]
		private float m_MaxDistance = 150f;

		// Token: 0x04002AC5 RID: 10949
		[Header("Fire Mode.Semi Auto")]
		[SerializeField]
		[Tooltip("The minimum time that can pass between consecutive shots.")]
		private float m_FireDuration = 0.22f;

		// Token: 0x04002AC6 RID: 10950
		[Header("Fire Mode.Burst")]
		[SerializeField]
		[Tooltip("How many shots will the gun fire when in Burst-mode.")]
		private int m_BurstLength = 3;

		// Token: 0x04002AC7 RID: 10951
		[SerializeField]
		[Tooltip("How much time it takes to fire all the shots.")]
		private float m_BurstDuration = 0.3f;

		// Token: 0x04002AC8 RID: 10952
		[SerializeField]
		[Tooltip("The minimum time that can pass between consecutive bursts.")]
		private float m_BurstPause = 0.35f;

		// Token: 0x04002AC9 RID: 10953
		[Header("Fire Mode.Full Auto")]
		[SerializeField]
		[Tooltip("The maximum amount of shots that can be executed in a minute.")]
		private int m_RoundsPerMinute = 450;

		// Token: 0x04002ACA RID: 10954
		[Header("Gun Settings")]
		[Range(1f, 20f)]
		[SerializeField]
		[Tooltip("The amount of rays that will be sent in the world (basically the amount of projectiles / bullets that will be fired at a time).")]
		private int m_RayCount = 1;

		// Token: 0x04002ACB RID: 10955
		[Range(0f, 30f)]
		[SerializeField]
		[Tooltip("When NOT aiming, how much the projectiles will spread (in angles).")]
		private float m_NormalSpread = 0.8f;

		// Token: 0x04002ACC RID: 10956
		[SerializeField]
		[Range(0f, 30f)]
		[Tooltip("When aiming, how much the projectiles will spread (in angles).")]
		private float m_AimSpread = 0.95f;

		// Token: 0x04002ACD RID: 10957
		[SerializeField]
		[Tooltip("")]
		private RayImpact m_RayImpact;

		// Token: 0x04002ACE RID: 10958
		[Header("Audio")]
		[SerializeField]
		private AudioSource m_AudioSource;

		// Token: 0x04002ACF RID: 10959
		[SerializeField]
		[Tooltip("Sounds that will play when firing the gun.")]
		private SoundPlayer m_FireAudio;

		// Token: 0x04002AD0 RID: 10960
		[Header("Effects")]
		[SerializeField]
		private ParticleSystem m_MuzzleFlash;

		// Token: 0x04002AD1 RID: 10961
		[SerializeField]
		private GameObject m_Tracer;

		// Token: 0x04002AD2 RID: 10962
		[SerializeField]
		private Vector3 m_TracerOffset;

		// Token: 0x04002AD3 RID: 10963
		[Header("Shell")]
		[SerializeField]
		private Rigidbody m_ShellPrefab;

		// Token: 0x04002AD4 RID: 10964
		[SerializeField]
		private FPHitscan.ShellSpawnMethod m_ShellSpawnMethod;

		// Token: 0x04002AD5 RID: 10965
		[SerializeField]
		private FPHitscanEventHandler m_AnimEventHandler;

		// Token: 0x04002AD6 RID: 10966
		[SerializeField]
		private Transform m_WeaponRoot;

		// Token: 0x04002AD7 RID: 10967
		[SerializeField]
		private Vector3 m_ShellSpawnOffset;

		// Token: 0x04002AD8 RID: 10968
		[SerializeField]
		private Vector3 m_ShellSpawnVelocity = new Vector3(3f, 2f, 0.3f);

		// Token: 0x04002AD9 RID: 10969
		[SerializeField]
		private float m_ShellSpin = 0.3f;

		// Token: 0x04002ADA RID: 10970
		private float m_MinTimeBetweenShots;

		// Token: 0x04002ADB RID: 10971
		private float m_NextTimeCanFire;

		// Token: 0x04002ADC RID: 10972
		private WaitForSeconds m_BurstWait;

		// Token: 0x020014BA RID: 5306
		public enum ShellSpawnMethod
		{
			// Token: 0x04006D0A RID: 27914
			Auto,
			// Token: 0x04006D0B RID: 27915
			OnAnimationEvent
		}
	}
}
