using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020008B6 RID: 2230
	public class FPBow : FPWeaponBase
	{
		// Token: 0x06003968 RID: 14696 RVA: 0x001A547C File Offset: 0x001A367C
		public override bool TryAttackOnce(Camera camera)
		{
			if (!base.Player.Aim.Active || Time.time < this.m_NextTimeCanShoot)
			{
				return false;
			}
			this.m_ReleaseAudio.Play(ItemSelectionMethod.Randomly, this.m_AudioSource, 1f);
			this.SpawnArrow(camera);
			this.m_NextTimeCanShoot = Time.time + this.m_MinTimeBetweenShots;
			base.Player.Aim.ForceStop();
			base.Attack.Send();
			return true;
		}

		// Token: 0x06003969 RID: 14697 RVA: 0x00029A50 File Offset: 0x00027C50
		protected override void Awake()
		{
			base.Awake();
			base.Player.Aim.AddStartTryer(new TryerDelegate(this.OnTryStart_Aim));
		}

		// Token: 0x0600396A RID: 14698 RVA: 0x00029A74 File Offset: 0x00027C74
		private bool OnTryStart_Aim()
		{
			bool flag = Time.time > this.m_NextTimeCanShoot || !base.IsEnabled;
			if (flag && base.IsEnabled)
			{
				this.m_StretchAudio.Play(ItemSelectionMethod.Randomly, this.m_AudioSource, 1f);
			}
			return flag;
		}

		// Token: 0x0600396B RID: 14699 RVA: 0x001A54F8 File Offset: 0x001A36F8
		private void SpawnArrow(Camera camera)
		{
			if (!this.m_ArrowPrefab)
			{
				Debug.LogErrorFormat("[{0}.FPBow] - No arrow prefab assigned in the inspector! Please assign one.", new object[]
				{
					base.name
				});
				return;
			}
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
			Object.Instantiate<GameObject>(this.m_ArrowPrefab.gameObject, vector2, quaternion).GetComponent<ShaftedProjectile>().Launch(base.Player);
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
		}

		// Token: 0x0400337D RID: 13181
		[Header("Bow Setup")]
		[SerializeField]
		private LayerMask m_Mask;

		// Token: 0x0400337E RID: 13182
		[SerializeField]
		private float m_MaxDistance = 50f;

		// Token: 0x0400337F RID: 13183
		[Header("Bow Settings")]
		[SerializeField]
		private float m_MinTimeBetweenShots = 1f;

		// Token: 0x04003380 RID: 13184
		[Header("Arrow")]
		[SerializeField]
		private ShaftedProjectile m_ArrowPrefab;

		// Token: 0x04003381 RID: 13185
		[SerializeField]
		private Vector3 m_SpawnOffset;

		// Token: 0x04003382 RID: 13186
		[Header("Audio")]
		[SerializeField]
		private AudioSource m_AudioSource;

		// Token: 0x04003383 RID: 13187
		[SerializeField]
		private SoundPlayer m_ReleaseAudio;

		// Token: 0x04003384 RID: 13188
		[SerializeField]
		private SoundPlayer m_StretchAudio;

		// Token: 0x04003385 RID: 13189
		private float m_NextTimeCanShoot;
	}
}
