using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005E6 RID: 1510
	public class FPBow : FPWeaponBase
	{
		// Token: 0x06003096 RID: 12438 RVA: 0x0015BB58 File Offset: 0x00159D58
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

		// Token: 0x06003097 RID: 12439 RVA: 0x0015BBD2 File Offset: 0x00159DD2
		protected override void Awake()
		{
			base.Awake();
			base.Player.Aim.AddStartTryer(new TryerDelegate(this.OnTryStart_Aim));
		}

		// Token: 0x06003098 RID: 12440 RVA: 0x0015BBF6 File Offset: 0x00159DF6
		private bool OnTryStart_Aim()
		{
			bool flag = Time.time > this.m_NextTimeCanShoot || !base.IsEnabled;
			if (flag && base.IsEnabled)
			{
				this.m_StretchAudio.Play(ItemSelectionMethod.Randomly, this.m_AudioSource, 1f);
			}
			return flag;
		}

		// Token: 0x06003099 RID: 12441 RVA: 0x0015BC34 File Offset: 0x00159E34
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

		// Token: 0x04002AB6 RID: 10934
		[Header("Bow Setup")]
		[SerializeField]
		private LayerMask m_Mask;

		// Token: 0x04002AB7 RID: 10935
		[SerializeField]
		private float m_MaxDistance = 50f;

		// Token: 0x04002AB8 RID: 10936
		[Header("Bow Settings")]
		[SerializeField]
		private float m_MinTimeBetweenShots = 1f;

		// Token: 0x04002AB9 RID: 10937
		[Header("Arrow")]
		[SerializeField]
		private ShaftedProjectile m_ArrowPrefab;

		// Token: 0x04002ABA RID: 10938
		[SerializeField]
		private Vector3 m_SpawnOffset;

		// Token: 0x04002ABB RID: 10939
		[Header("Audio")]
		[SerializeField]
		private AudioSource m_AudioSource;

		// Token: 0x04002ABC RID: 10940
		[SerializeField]
		private SoundPlayer m_ReleaseAudio;

		// Token: 0x04002ABD RID: 10941
		[SerializeField]
		private SoundPlayer m_StretchAudio;

		// Token: 0x04002ABE RID: 10942
		private float m_NextTimeCanShoot;
	}
}
