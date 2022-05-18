using System;
using System.Collections;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020008C6 RID: 2246
	public class FPSpear : FPWeaponBase
	{
		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x060039D0 RID: 14800 RVA: 0x00029FF0 File Offset: 0x000281F0
		public bool CanThrow
		{
			get
			{
				return Time.time > this.m_NextTimeCanThrow;
			}
		}

		// Token: 0x060039D1 RID: 14801 RVA: 0x001A6DBC File Offset: 0x001A4FBC
		public override bool TryAttackOnce(Camera camera)
		{
			if (!base.Player.Aim.Active || !this.CanThrow)
			{
				return false;
			}
			this.m_ThrowAudio.Play(ItemSelectionMethod.Randomly, this.m_AudioSource, 1f);
			base.StartCoroutine(this.C_ThrowWithDelay(camera, this.m_SpawnDelay));
			this.m_NextTimeCanThrow = Time.time + this.m_MinTimeBetweenThrows;
			base.Attack.Send();
			return true;
		}

		// Token: 0x060039D2 RID: 14802 RVA: 0x00029FFF File Offset: 0x000281FF
		private void Start()
		{
			base.Player.Aim.AddStartTryer(new TryerDelegate(this.OnTryStart_Aim));
		}

		// Token: 0x060039D3 RID: 14803 RVA: 0x0002A01D File Offset: 0x0002821D
		private bool OnTryStart_Aim()
		{
			return !base.IsEnabled || this.CanThrow;
		}

		// Token: 0x060039D4 RID: 14804 RVA: 0x0002A02F File Offset: 0x0002822F
		private IEnumerator C_ThrowWithDelay(Camera camera, float delay)
		{
			if (!this.m_SpearPrefab)
			{
				Debug.LogErrorFormat(this, "The spear prefab is not assigned in the inspector!.", new object[]
				{
					base.name
				});
				yield break;
			}
			yield return new WaitForSeconds(delay);
			RaycastHit raycastHit;
			Vector3 vector;
			if (Physics.Raycast(camera.ViewportPointToRay(Vector3.one * 0.5f), ref raycastHit, this.m_MaxDistance, this.m_Mask, 1))
			{
				vector = raycastHit.point;
			}
			else
			{
				vector = camera.transform.position + camera.transform.forward * this.m_MaxDistance;
			}
			Vector3 vector2 = base.transform.position + camera.transform.TransformVector(this.m_SpawnOffset);
			Quaternion quaternion = Quaternion.LookRotation(vector - vector2);
			Object.Instantiate<GameObject>(this.m_SpearPrefab.gameObject, vector2, quaternion).GetComponent<ShaftedProjectile>().Launch(base.Player);
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
			yield break;
		}

		// Token: 0x040033FF RID: 13311
		[Header("Setup")]
		[SerializeField]
		private LayerMask m_Mask;

		// Token: 0x04003400 RID: 13312
		[SerializeField]
		private float m_MaxDistance = 50f;

		// Token: 0x04003401 RID: 13313
		[Header("Settings")]
		[SerializeField]
		private float m_MinTimeBetweenThrows = 1.5f;

		// Token: 0x04003402 RID: 13314
		[Header("Object To Throw")]
		[SerializeField]
		private ShaftedProjectile m_SpearPrefab;

		// Token: 0x04003403 RID: 13315
		[SerializeField]
		private Vector3 m_SpawnOffset;

		// Token: 0x04003404 RID: 13316
		[SerializeField]
		private float m_SpawnDelay = 0.3f;

		// Token: 0x04003405 RID: 13317
		[Header("Audio")]
		[SerializeField]
		private AudioSource m_AudioSource;

		// Token: 0x04003406 RID: 13318
		[SerializeField]
		private SoundPlayer m_ThrowAudio;

		// Token: 0x04003407 RID: 13319
		private float m_NextTimeCanThrow;
	}
}
