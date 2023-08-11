using UnityEngine;

namespace UltimateSurvival;

public class EntityDeathHandler : EntityBehaviour
{
	[Header("Audio")]
	[SerializeField]
	private AudioSource m_AudioSource;

	[SerializeField]
	private SoundPlayer m_DeathAudio;

	[Header("Stuff To Disable On Death")]
	[SerializeField]
	private GameObject[] m_ObjectsToDisable;

	[SerializeField]
	private Behaviour[] m_BehavioursToDisable;

	[SerializeField]
	private Collider[] m_CollidersToDisable;

	[Header("Ragdoll")]
	[SerializeField]
	[Tooltip("On death, you can either have a ragdoll, or an animation to play.")]
	private bool m_EnableRagdoll;

	[SerializeField]
	[Tooltip("A Ragdoll component, usually attached to the armature of the character.")]
	private Ragdoll m_Ragdoll;

	[Header("Death Animation")]
	[SerializeField]
	[Tooltip("On death, you can either have a ragdoll, or an animation to play.")]
	private bool m_EnableDeathAnim;

	[SerializeField]
	private Animator m_Animator;

	[Header("Destroy Timer")]
	[SerializeField]
	[Clamp(0f, 1000f)]
	[Tooltip("")]
	private float m_DestroyTimer;

	private Vector3 m_CamStartPos;

	private Quaternion m_CamStartRot;

	private void Awake()
	{
		if (m_EnableRagdoll && !Object.op_Implicit((Object)(object)m_Ragdoll))
		{
			Debug.LogError((object)"The ragdoll option has been enabled but no ragdoll object is assigned!", (Object)(object)this);
		}
		base.Entity.Health.AddChangeListener(OnChanged_Health);
	}

	private void OnChanged_Health()
	{
		if (base.Entity.Health.Is(0f))
		{
			On_Death();
		}
	}

	private void On_Death()
	{
		m_DeathAudio.Play(ItemSelectionMethod.Randomly, m_AudioSource);
		if (m_EnableRagdoll && Object.op_Implicit((Object)(object)m_Ragdoll))
		{
			m_Ragdoll.Enable();
		}
		if (m_EnableDeathAnim && Object.op_Implicit((Object)(object)m_Animator))
		{
			m_Animator.SetTrigger("Die");
		}
		GameObject[] objectsToDisable = m_ObjectsToDisable;
		for (int i = 0; i < objectsToDisable.Length; i++)
		{
			objectsToDisable[i].SetActive(false);
		}
		Behaviour[] behavioursToDisable = m_BehavioursToDisable;
		foreach (Behaviour val in behavioursToDisable)
		{
			Animator val2 = (Animator)(object)((val is Animator) ? val : null);
			if ((Object)(object)val2 != (Object)null)
			{
				Object.Destroy((Object)(object)val2);
			}
			else
			{
				val.enabled = false;
			}
		}
		Collider[] collidersToDisable = m_CollidersToDisable;
		for (int i = 0; i < collidersToDisable.Length; i++)
		{
			collidersToDisable[i].enabled = false;
		}
		Object.Destroy((Object)(object)((Component)this).gameObject, m_DestroyTimer);
		base.Entity.Death.Send();
	}
}
