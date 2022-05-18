using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020008F3 RID: 2291
	public class FootstepPlayer : PlayerBehaviour
	{
		// Token: 0x06003ABC RID: 15036 RVA: 0x001A9E78 File Offset: 0x001A8078
		private void Start()
		{
			base.Player.Jump.AddStartListener(new Action(this.OnStart_PlayerJump));
			base.Player.Land.AddListener(new Action<float>(this.On_PlayerLanded));
			this.m_LeftFootSource = GameController.Audio.CreateAudioSource("Left Foot Footstep", base.transform, new Vector3(-0.2f, 0f, 0f), false, 1f, 3f);
			this.m_RightFootSource = GameController.Audio.CreateAudioSource("Right Foot Footstep", base.transform, new Vector3(0.2f, 0f, 0f), false, 1f, 3f);
		}

		// Token: 0x06003ABD RID: 15037 RVA: 0x001A9F34 File Offset: 0x001A8134
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

		// Token: 0x06003ABE RID: 15038 RVA: 0x001A9FA0 File Offset: 0x001A81A0
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

		// Token: 0x06003ABF RID: 15039 RVA: 0x001A9FFC File Offset: 0x001A81FC
		private float GetStepLength()
		{
			float result = this.m_WalkStepLength;
			if (base.Player.Run.Active)
			{
				result = this.m_RunStepLength;
			}
			return result;
		}

		// Token: 0x06003AC0 RID: 15040 RVA: 0x001AA02C File Offset: 0x001A822C
		private float GetVolumeFactor()
		{
			float result = this.m_WalkVolumeFactor;
			if (base.Player.Run.Active)
			{
				result = this.m_RunVolumeFactor;
			}
			return result;
		}

		// Token: 0x06003AC1 RID: 15041 RVA: 0x001AA05C File Offset: 0x001A825C
		private void OnStart_PlayerJump()
		{
			SurfaceData dataFromBelow = this.GetDataFromBelow();
			if (dataFromBelow == null)
			{
				return;
			}
			dataFromBelow.PlaySound(ItemSelectionMethod.RandomlyButExcludeLast, SoundType.Jump, 1f, this.m_AudioSource);
		}

		// Token: 0x06003AC2 RID: 15042 RVA: 0x001AA088 File Offset: 0x001A8288
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

		// Token: 0x06003AC3 RID: 15043 RVA: 0x001AA0CC File Offset: 0x001A82CC
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

		// Token: 0x04003502 RID: 13570
		[Header("General")]
		[SerializeField]
		private ItemSelectionMethod m_FootstepSelectionMethod;

		// Token: 0x04003503 RID: 13571
		[SerializeField]
		private float m_LandSpeedThreeshold = 3f;

		// Token: 0x04003504 RID: 13572
		[SerializeField]
		private LayerMask m_Mask;

		// Token: 0x04003505 RID: 13573
		[SerializeField]
		private AudioSource m_AudioSource;

		// Token: 0x04003506 RID: 13574
		[Header("Distance Between Steps")]
		[SerializeField]
		private float m_WalkStepLength = 1.7f;

		// Token: 0x04003507 RID: 13575
		[SerializeField]
		private float m_RunStepLength = 2f;

		// Token: 0x04003508 RID: 13576
		[Header("Volume Factors")]
		[SerializeField]
		private float m_WalkVolumeFactor = 0.5f;

		// Token: 0x04003509 RID: 13577
		[SerializeField]
		private float m_RunVolumeFactor = 1f;

		// Token: 0x0400350A RID: 13578
		private AudioSource m_LeftFootSource;

		// Token: 0x0400350B RID: 13579
		private AudioSource m_RightFootSource;

		// Token: 0x0400350C RID: 13580
		private FootType m_LastFroundedFoot;

		// Token: 0x0400350D RID: 13581
		private float m_AccumulatedDistance;
	}
}
