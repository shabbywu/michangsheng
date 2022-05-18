using System;
using System.Collections;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020008F5 RID: 2293
	public class PlayerDeathHandler : PlayerBehaviour
	{
		// Token: 0x06003ACA RID: 15050 RVA: 0x001AA134 File Offset: 0x001A8334
		private void Awake()
		{
			if (this.m_EnableRagdoll && !this.m_Ragdoll)
			{
				Debug.LogError("The ragdoll option has been enabled but no ragdoll object is assigned!", this);
			}
			base.Player.Health.AddChangeListener(new Action(this.OnChanged_Health));
			this.m_CamStartPos = this.m_Camera.transform.localPosition;
			this.m_CamStartRot = this.m_Camera.transform.localRotation;
		}

		// Token: 0x06003ACB RID: 15051 RVA: 0x001AA1AC File Offset: 0x001A83AC
		private void OnChanged_Health()
		{
			if (base.Player.Health.Is(0f))
			{
				this.On_Death();
				RaycastHit raycastHit;
				if (Physics.Raycast(new Ray(base.transform.position + Vector3.up, Vector3.down), ref raycastHit, 1.5f, -1))
				{
					this.m_Camera.transform.position = raycastHit.point + Vector3.up * 0.1f;
					this.m_Camera.transform.rotation = Quaternion.Euler(-30f, Random.Range(-180f, 180f), 0f);
				}
			}
		}

		// Token: 0x06003ACC RID: 15052 RVA: 0x000042DD File Offset: 0x000024DD
		private void On_Death()
		{
		}

		// Token: 0x06003ACD RID: 15053 RVA: 0x0002AB02 File Offset: 0x00028D02
		private IEnumerator C_Respawn()
		{
			yield return new WaitForSeconds(this.m_RespawnDuration);
			if (this.m_EnableRagdoll && this.m_Ragdoll)
			{
				this.m_Ragdoll.Disable();
			}
			this.m_Camera.transform.localPosition = this.m_CamStartPos;
			this.m_Camera.transform.localRotation = this.m_CamStartRot;
			if (base.Player.LastSleepPosition.Get() != Vector3.zero)
			{
				base.transform.position = base.Player.LastSleepPosition.Get();
				base.transform.rotation = Quaternion.Euler(Vector3.up * Random.Range(-180f, 180f));
			}
			else
			{
				GameObject[] array = GameObject.FindGameObjectsWithTag("Spawn Point");
				if (array != null && array.Length != 0)
				{
					GameObject gameObject = array[Random.Range(0, array.Length)];
					base.transform.position = gameObject.transform.position;
					base.transform.rotation = Quaternion.Euler(Vector3.up * Random.Range(-180f, 180f));
				}
			}
			yield return new WaitForSeconds(this.m_RespawnBlockTime);
			GameObject[] objectsToDisable = this.m_ObjectsToDisable;
			for (int i = 0; i < objectsToDisable.Length; i++)
			{
				objectsToDisable[i].SetActive(true);
			}
			Behaviour[] behavioursToDisable = this.m_BehavioursToDisable;
			for (int i = 0; i < behavioursToDisable.Length; i++)
			{
				behavioursToDisable[i].enabled = true;
			}
			Collider[] collidersToDisable = this.m_CollidersToDisable;
			for (int i = 0; i < collidersToDisable.Length; i++)
			{
				collidersToDisable[i].enabled = true;
			}
			base.Player.Health.Set(100f);
			base.Player.Thirst.Set(100f);
			base.Player.Hunger.Set(100f);
			base.Player.Stamina.Set(100f);
			base.Player.Respawn.Send();
			yield break;
		}

		// Token: 0x0400350F RID: 13583
		[SerializeField]
		private GameObject m_Camera;

		// Token: 0x04003510 RID: 13584
		[Header("Audio")]
		[SerializeField]
		private AudioSource m_AudioSource;

		// Token: 0x04003511 RID: 13585
		[SerializeField]
		private SoundPlayer m_DeathAudio;

		// Token: 0x04003512 RID: 13586
		[Header("Stuff To Disable On Death")]
		[SerializeField]
		private GameObject[] m_ObjectsToDisable;

		// Token: 0x04003513 RID: 13587
		[SerializeField]
		private Behaviour[] m_BehavioursToDisable;

		// Token: 0x04003514 RID: 13588
		[SerializeField]
		private Collider[] m_CollidersToDisable;

		// Token: 0x04003515 RID: 13589
		[Header("Ragdoll")]
		[SerializeField]
		private bool m_EnableRagdoll;

		// Token: 0x04003516 RID: 13590
		[SerializeField]
		[Tooltip("A Ragdoll component, usually attached to the armature of the character.")]
		private Ragdoll m_Ragdoll;

		// Token: 0x04003517 RID: 13591
		[Header("Respawn")]
		[SerializeField]
		private bool m_AutoRespawn = true;

		// Token: 0x04003518 RID: 13592
		[SerializeField]
		private float m_RespawnDuration = 10f;

		// Token: 0x04003519 RID: 13593
		[SerializeField]
		private float m_RespawnBlockTime = 3f;

		// Token: 0x0400351A RID: 13594
		private Vector3 m_CamStartPos;

		// Token: 0x0400351B RID: 13595
		private Quaternion m_CamStartRot;
	}
}
