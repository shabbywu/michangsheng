using System;
using System.Collections;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x02000612 RID: 1554
	public class PlayerDeathHandler : PlayerBehaviour
	{
		// Token: 0x060031AD RID: 12717 RVA: 0x00160C14 File Offset: 0x0015EE14
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

		// Token: 0x060031AE RID: 12718 RVA: 0x00160C8C File Offset: 0x0015EE8C
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

		// Token: 0x060031AF RID: 12719 RVA: 0x00004095 File Offset: 0x00002295
		private void On_Death()
		{
		}

		// Token: 0x060031B0 RID: 12720 RVA: 0x00160D40 File Offset: 0x0015EF40
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

		// Token: 0x04002BFB RID: 11259
		[SerializeField]
		private GameObject m_Camera;

		// Token: 0x04002BFC RID: 11260
		[Header("Audio")]
		[SerializeField]
		private AudioSource m_AudioSource;

		// Token: 0x04002BFD RID: 11261
		[SerializeField]
		private SoundPlayer m_DeathAudio;

		// Token: 0x04002BFE RID: 11262
		[Header("Stuff To Disable On Death")]
		[SerializeField]
		private GameObject[] m_ObjectsToDisable;

		// Token: 0x04002BFF RID: 11263
		[SerializeField]
		private Behaviour[] m_BehavioursToDisable;

		// Token: 0x04002C00 RID: 11264
		[SerializeField]
		private Collider[] m_CollidersToDisable;

		// Token: 0x04002C01 RID: 11265
		[Header("Ragdoll")]
		[SerializeField]
		private bool m_EnableRagdoll;

		// Token: 0x04002C02 RID: 11266
		[SerializeField]
		[Tooltip("A Ragdoll component, usually attached to the armature of the character.")]
		private Ragdoll m_Ragdoll;

		// Token: 0x04002C03 RID: 11267
		[Header("Respawn")]
		[SerializeField]
		private bool m_AutoRespawn = true;

		// Token: 0x04002C04 RID: 11268
		[SerializeField]
		private float m_RespawnDuration = 10f;

		// Token: 0x04002C05 RID: 11269
		[SerializeField]
		private float m_RespawnBlockTime = 3f;

		// Token: 0x04002C06 RID: 11270
		private Vector3 m_CamStartPos;

		// Token: 0x04002C07 RID: 11271
		private Quaternion m_CamStartRot;
	}
}
