using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020008F1 RID: 2289
	public class EntityDeathHandler : EntityBehaviour
	{
		// Token: 0x06003AB8 RID: 15032 RVA: 0x0002AA1D File Offset: 0x00028C1D
		private void Awake()
		{
			if (this.m_EnableRagdoll && !this.m_Ragdoll)
			{
				Debug.LogError("The ragdoll option has been enabled but no ragdoll object is assigned!", this);
			}
			base.Entity.Health.AddChangeListener(new Action(this.OnChanged_Health));
		}

		// Token: 0x06003AB9 RID: 15033 RVA: 0x0002AA5B File Offset: 0x00028C5B
		private void OnChanged_Health()
		{
			if (base.Entity.Health.Is(0f))
			{
				this.On_Death();
			}
		}

		// Token: 0x06003ABA RID: 15034 RVA: 0x001A9D74 File Offset: 0x001A7F74
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

		// Token: 0x040034F3 RID: 13555
		[Header("Audio")]
		[SerializeField]
		private AudioSource m_AudioSource;

		// Token: 0x040034F4 RID: 13556
		[SerializeField]
		private SoundPlayer m_DeathAudio;

		// Token: 0x040034F5 RID: 13557
		[Header("Stuff To Disable On Death")]
		[SerializeField]
		private GameObject[] m_ObjectsToDisable;

		// Token: 0x040034F6 RID: 13558
		[SerializeField]
		private Behaviour[] m_BehavioursToDisable;

		// Token: 0x040034F7 RID: 13559
		[SerializeField]
		private Collider[] m_CollidersToDisable;

		// Token: 0x040034F8 RID: 13560
		[Header("Ragdoll")]
		[SerializeField]
		[Tooltip("On death, you can either have a ragdoll, or an animation to play.")]
		private bool m_EnableRagdoll;

		// Token: 0x040034F9 RID: 13561
		[SerializeField]
		[Tooltip("A Ragdoll component, usually attached to the armature of the character.")]
		private Ragdoll m_Ragdoll;

		// Token: 0x040034FA RID: 13562
		[Header("Death Animation")]
		[SerializeField]
		[Tooltip("On death, you can either have a ragdoll, or an animation to play.")]
		private bool m_EnableDeathAnim;

		// Token: 0x040034FB RID: 13563
		[SerializeField]
		private Animator m_Animator;

		// Token: 0x040034FC RID: 13564
		[Header("Destroy Timer")]
		[SerializeField]
		[Clamp(0f, 1000f)]
		[Tooltip("")]
		private float m_DestroyTimer;

		// Token: 0x040034FD RID: 13565
		private Vector3 m_CamStartPos;

		// Token: 0x040034FE RID: 13566
		private Quaternion m_CamStartRot;
	}
}
