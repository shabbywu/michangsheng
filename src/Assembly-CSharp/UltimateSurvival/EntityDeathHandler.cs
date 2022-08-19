using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x0200060E RID: 1550
	public class EntityDeathHandler : EntityBehaviour
	{
		// Token: 0x0600319B RID: 12699 RVA: 0x0016076F File Offset: 0x0015E96F
		private void Awake()
		{
			if (this.m_EnableRagdoll && !this.m_Ragdoll)
			{
				Debug.LogError("The ragdoll option has been enabled but no ragdoll object is assigned!", this);
			}
			base.Entity.Health.AddChangeListener(new Action(this.OnChanged_Health));
		}

		// Token: 0x0600319C RID: 12700 RVA: 0x001607AD File Offset: 0x0015E9AD
		private void OnChanged_Health()
		{
			if (base.Entity.Health.Is(0f))
			{
				this.On_Death();
			}
		}

		// Token: 0x0600319D RID: 12701 RVA: 0x001607CC File Offset: 0x0015E9CC
		private void On_Death()
		{
			this.m_DeathAudio.Play(ItemSelectionMethod.Randomly, this.m_AudioSource, 1f);
			if (this.m_EnableRagdoll && this.m_Ragdoll)
			{
				this.m_Ragdoll.Enable();
			}
			if (this.m_EnableDeathAnim && this.m_Animator)
			{
				this.m_Animator.SetTrigger("Die");
			}
			GameObject[] objectsToDisable = this.m_ObjectsToDisable;
			for (int i = 0; i < objectsToDisable.Length; i++)
			{
				objectsToDisable[i].SetActive(false);
			}
			foreach (Behaviour behaviour in this.m_BehavioursToDisable)
			{
				Animator animator = behaviour as Animator;
				if (animator != null)
				{
					Object.Destroy(animator);
				}
				else
				{
					behaviour.enabled = false;
				}
			}
			Collider[] collidersToDisable = this.m_CollidersToDisable;
			for (int i = 0; i < collidersToDisable.Length; i++)
			{
				collidersToDisable[i].enabled = false;
			}
			Object.Destroy(base.gameObject, this.m_DestroyTimer);
			base.Entity.Death.Send();
		}

		// Token: 0x04002BDF RID: 11231
		[Header("Audio")]
		[SerializeField]
		private AudioSource m_AudioSource;

		// Token: 0x04002BE0 RID: 11232
		[SerializeField]
		private SoundPlayer m_DeathAudio;

		// Token: 0x04002BE1 RID: 11233
		[Header("Stuff To Disable On Death")]
		[SerializeField]
		private GameObject[] m_ObjectsToDisable;

		// Token: 0x04002BE2 RID: 11234
		[SerializeField]
		private Behaviour[] m_BehavioursToDisable;

		// Token: 0x04002BE3 RID: 11235
		[SerializeField]
		private Collider[] m_CollidersToDisable;

		// Token: 0x04002BE4 RID: 11236
		[Header("Ragdoll")]
		[SerializeField]
		[Tooltip("On death, you can either have a ragdoll, or an animation to play.")]
		private bool m_EnableRagdoll;

		// Token: 0x04002BE5 RID: 11237
		[SerializeField]
		[Tooltip("A Ragdoll component, usually attached to the armature of the character.")]
		private Ragdoll m_Ragdoll;

		// Token: 0x04002BE6 RID: 11238
		[Header("Death Animation")]
		[SerializeField]
		[Tooltip("On death, you can either have a ragdoll, or an animation to play.")]
		private bool m_EnableDeathAnim;

		// Token: 0x04002BE7 RID: 11239
		[SerializeField]
		private Animator m_Animator;

		// Token: 0x04002BE8 RID: 11240
		[Header("Destroy Timer")]
		[SerializeField]
		[Clamp(0f, 1000f)]
		[Tooltip("")]
		private float m_DestroyTimer;

		// Token: 0x04002BE9 RID: 11241
		private Vector3 m_CamStartPos;

		// Token: 0x04002BEA RID: 11242
		private Quaternion m_CamStartRot;
	}
}
