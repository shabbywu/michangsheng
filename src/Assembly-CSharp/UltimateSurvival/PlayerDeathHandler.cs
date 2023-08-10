using System.Collections;
using UnityEngine;

namespace UltimateSurvival;

public class PlayerDeathHandler : PlayerBehaviour
{
	[SerializeField]
	private GameObject m_Camera;

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
	private bool m_EnableRagdoll;

	[SerializeField]
	[Tooltip("A Ragdoll component, usually attached to the armature of the character.")]
	private Ragdoll m_Ragdoll;

	[Header("Respawn")]
	[SerializeField]
	private bool m_AutoRespawn = true;

	[SerializeField]
	private float m_RespawnDuration = 10f;

	[SerializeField]
	private float m_RespawnBlockTime = 3f;

	private Vector3 m_CamStartPos;

	private Quaternion m_CamStartRot;

	private void Awake()
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		if (m_EnableRagdoll && !Object.op_Implicit((Object)(object)m_Ragdoll))
		{
			Debug.LogError((object)"The ragdoll option has been enabled but no ragdoll object is assigned!", (Object)(object)this);
		}
		base.Player.Health.AddChangeListener(OnChanged_Health);
		m_CamStartPos = m_Camera.transform.localPosition;
		m_CamStartRot = m_Camera.transform.localRotation;
	}

	private void OnChanged_Health()
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		if (base.Player.Health.Is(0f))
		{
			On_Death();
			RaycastHit val = default(RaycastHit);
			if (Physics.Raycast(new Ray(((Component)this).transform.position + Vector3.up, Vector3.down), ref val, 1.5f, -1))
			{
				m_Camera.transform.position = ((RaycastHit)(ref val)).point + Vector3.up * 0.1f;
				m_Camera.transform.rotation = Quaternion.Euler(-30f, Random.Range(-180f, 180f), 0f);
			}
		}
	}

	private void On_Death()
	{
	}

	private IEnumerator C_Respawn()
	{
		yield return (object)new WaitForSeconds(m_RespawnDuration);
		if (m_EnableRagdoll && Object.op_Implicit((Object)(object)m_Ragdoll))
		{
			m_Ragdoll.Disable();
		}
		m_Camera.transform.localPosition = m_CamStartPos;
		m_Camera.transform.localRotation = m_CamStartRot;
		if (base.Player.LastSleepPosition.Get() != Vector3.zero)
		{
			((Component)this).transform.position = base.Player.LastSleepPosition.Get();
			((Component)this).transform.rotation = Quaternion.Euler(Vector3.up * Random.Range(-180f, 180f));
		}
		else
		{
			GameObject[] array = GameObject.FindGameObjectsWithTag("Spawn Point");
			if (array != null && array.Length != 0)
			{
				GameObject val = array[Random.Range(0, array.Length)];
				((Component)this).transform.position = val.transform.position;
				((Component)this).transform.rotation = Quaternion.Euler(Vector3.up * Random.Range(-180f, 180f));
			}
		}
		yield return (object)new WaitForSeconds(m_RespawnBlockTime);
		GameObject[] objectsToDisable = m_ObjectsToDisable;
		for (int i = 0; i < objectsToDisable.Length; i++)
		{
			objectsToDisable[i].SetActive(true);
		}
		Behaviour[] behavioursToDisable = m_BehavioursToDisable;
		for (int i = 0; i < behavioursToDisable.Length; i++)
		{
			behavioursToDisable[i].enabled = true;
		}
		Collider[] collidersToDisable = m_CollidersToDisable;
		for (int i = 0; i < collidersToDisable.Length; i++)
		{
			collidersToDisable[i].enabled = true;
		}
		base.Player.Health.Set(100f);
		base.Player.Thirst.Set(100f);
		base.Player.Hunger.Set(100f);
		base.Player.Stamina.Set(100f);
		base.Player.Respawn.Send();
	}
}
