using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x02000610 RID: 1552
	public class FootstepPlayer : PlayerBehaviour
	{
		// Token: 0x0600319F RID: 12703 RVA: 0x001608D0 File Offset: 0x0015EAD0
		private void Start()
		{
			base.Player.Jump.AddStartListener(new Action(this.OnStart_PlayerJump));
			base.Player.Land.AddListener(new Action<float>(this.On_PlayerLanded));
			this.m_LeftFootSource = GameController.Audio.CreateAudioSource("Left Foot Footstep", base.transform, new Vector3(-0.2f, 0f, 0f), false, 1f, 3f);
			this.m_RightFootSource = GameController.Audio.CreateAudioSource("Right Foot Footstep", base.transform, new Vector3(0.2f, 0f, 0f), false, 1f, 3f);
		}

		// Token: 0x060031A0 RID: 12704 RVA: 0x0016098C File Offset: 0x0015EB8C
		private void FixedUpdate()
		{
			if (!base.Player.IsGrounded.Get())
			{
				return;
			}
			this.m_AccumulatedDistance += base.Player.Velocity.Get().magnitude * Time.fixedDeltaTime;
			float stepLength = this.GetStepLength();
			if (this.m_AccumulatedDistance > stepLength)
			{
				this.PlayFootstep();
				this.m_AccumulatedDistance = 0f;
			}
		}

		// Token: 0x060031A1 RID: 12705 RVA: 0x001609F8 File Offset: 0x0015EBF8
		private void PlayFootstep()
		{
			SurfaceData dataFromBelow = this.GetDataFromBelow();
			if (dataFromBelow == null)
			{
				return;
			}
			AudioSource audioSource = (this.m_LastFroundedFoot == FootType.Left) ? this.m_RightFootSource : this.m_LeftFootSource;
			this.m_LastFroundedFoot = ((this.m_LastFroundedFoot == FootType.Left) ? FootType.Right : FootType.Left);
			float volumeFactor = this.GetVolumeFactor();
			dataFromBelow.PlaySound(this.m_FootstepSelectionMethod, SoundType.Footstep, volumeFactor, audioSource);
		}

		// Token: 0x060031A2 RID: 12706 RVA: 0x00160A54 File Offset: 0x0015EC54
		private float GetStepLength()
		{
			float result = this.m_WalkStepLength;
			if (base.Player.Run.Active)
			{
				result = this.m_RunStepLength;
			}
			return result;
		}

		// Token: 0x060031A3 RID: 12707 RVA: 0x00160A84 File Offset: 0x0015EC84
		private float GetVolumeFactor()
		{
			float result = this.m_WalkVolumeFactor;
			if (base.Player.Run.Active)
			{
				result = this.m_RunVolumeFactor;
			}
			return result;
		}

		// Token: 0x060031A4 RID: 12708 RVA: 0x00160AB4 File Offset: 0x0015ECB4
		private void OnStart_PlayerJump()
		{
			SurfaceData dataFromBelow = this.GetDataFromBelow();
			if (dataFromBelow == null)
			{
				return;
			}
			dataFromBelow.PlaySound(ItemSelectionMethod.RandomlyButExcludeLast, SoundType.Jump, 1f, this.m_AudioSource);
		}

		// Token: 0x060031A5 RID: 12709 RVA: 0x00160AE0 File Offset: 0x0015ECE0
		private void On_PlayerLanded(float landSpeed)
		{
			SurfaceData dataFromBelow = this.GetDataFromBelow();
			if (dataFromBelow == null)
			{
				return;
			}
			if (landSpeed >= this.m_LandSpeedThreeshold)
			{
				dataFromBelow.PlaySound(ItemSelectionMethod.RandomlyButExcludeLast, SoundType.Land, 1f, this.m_AudioSource);
				this.m_AccumulatedDistance = 0f;
			}
		}

		// Token: 0x060031A6 RID: 12710 RVA: 0x00160B24 File Offset: 0x0015ED24
		private SurfaceData GetDataFromBelow()
		{
			if (!GameController.SurfaceDatabase)
			{
				Debug.LogWarning("No surface database found! can't play any footsteps...", this);
				return null;
			}
			Ray ray;
			ray..ctor(base.transform.position + Vector3.up * 0.1f, Vector3.down);
			return GameController.SurfaceDatabase.GetSurfaceData(ray, 1f, this.m_Mask);
		}

		// Token: 0x04002BEE RID: 11246
		[Header("General")]
		[SerializeField]
		private ItemSelectionMethod m_FootstepSelectionMethod;

		// Token: 0x04002BEF RID: 11247
		[SerializeField]
		private float m_LandSpeedThreeshold = 3f;

		// Token: 0x04002BF0 RID: 11248
		[SerializeField]
		private LayerMask m_Mask;

		// Token: 0x04002BF1 RID: 11249
		[SerializeField]
		private AudioSource m_AudioSource;

		// Token: 0x04002BF2 RID: 11250
		[Header("Distance Between Steps")]
		[SerializeField]
		private float m_WalkStepLength = 1.7f;

		// Token: 0x04002BF3 RID: 11251
		[SerializeField]
		private float m_RunStepLength = 2f;

		// Token: 0x04002BF4 RID: 11252
		[Header("Volume Factors")]
		[SerializeField]
		private float m_WalkVolumeFactor = 0.5f;

		// Token: 0x04002BF5 RID: 11253
		[SerializeField]
		private float m_RunVolumeFactor = 1f;

		// Token: 0x04002BF6 RID: 11254
		private AudioSource m_LeftFootSource;

		// Token: 0x04002BF7 RID: 11255
		private AudioSource m_RightFootSource;

		// Token: 0x04002BF8 RID: 11256
		private FootType m_LastFroundedFoot;

		// Token: 0x04002BF9 RID: 11257
		private float m_AccumulatedDistance;
	}
}
