using System;
using System.Collections;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005F2 RID: 1522
	public class FPSpear : FPWeaponBase
	{
		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x060030F2 RID: 12530 RVA: 0x0015D8F2 File Offset: 0x0015BAF2
		public bool CanThrow
		{
			get
			{
				return Time.time > this.m_NextTimeCanThrow;
			}
		}

		// Token: 0x060030F3 RID: 12531 RVA: 0x0015D904 File Offset: 0x0015BB04
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

		// Token: 0x060030F4 RID: 12532 RVA: 0x0015D976 File Offset: 0x0015BB76
		private void Start()
		{
			base.Player.Aim.AddStartTryer(new TryerDelegate(this.OnTryStart_Aim));
		}

		// Token: 0x060030F5 RID: 12533 RVA: 0x0015D994 File Offset: 0x0015BB94
		private bool OnTryStart_Aim()
		{
			return !base.IsEnabled || this.CanThrow;
		}

		// Token: 0x060030F6 RID: 12534 RVA: 0x0015D9A6 File Offset: 0x0015BBA6
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

		// Token: 0x04002B29 RID: 11049
		[Header("Setup")]
		[SerializeField]
		private LayerMask m_Mask;

		// Token: 0x04002B2A RID: 11050
		[SerializeField]
		private float m_MaxDistance = 50f;

		// Token: 0x04002B2B RID: 11051
		[Header("Settings")]
		[SerializeField]
		private float m_MinTimeBetweenThrows = 1.5f;

		// Token: 0x04002B2C RID: 11052
		[Header("Object To Throw")]
		[SerializeField]
		private ShaftedProjectile m_SpearPrefab;

		// Token: 0x04002B2D RID: 11053
		[SerializeField]
		private Vector3 m_SpawnOffset;

		// Token: 0x04002B2E RID: 11054
		[SerializeField]
		private float m_SpawnDelay = 0.3f;

		// Token: 0x04002B2F RID: 11055
		[Header("Audio")]
		[SerializeField]
		private AudioSource m_AudioSource;

		// Token: 0x04002B30 RID: 11056
		[SerializeField]
		private SoundPlayer m_ThrowAudio;

		// Token: 0x04002B31 RID: 11057
		private float m_NextTimeCanThrow;
	}
}
