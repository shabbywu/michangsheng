using System.Collections;
using UnityEngine;

namespace UltimateSurvival.Building;

public class Door : InteractableObject
{
	[SerializeField]
	private Transform m_Model;

	[SerializeField]
	private Collider m_Collider;

	[Header("Functionality")]
	[SerializeField]
	private Vector3 m_ClosedRotation;

	[SerializeField]
	private Vector3 m_OpenRotation;

	[SerializeField]
	[Range(0.1f, 30f)]
	private float m_AnimationSpeed = 1f;

	[SerializeField]
	[Range(0.3f, 3f)]
	private float m_InteractionThreeshold = 1f;

	[Header("Audio")]
	[SerializeField]
	private SoundPlayer m_DoorOpen;

	[SerializeField]
	private SoundPlayer m_DoorClose;

	private bool m_Open;

	private float m_NextTimeCanInteract;

	public bool Open => m_Open;

	public override void OnInteract(PlayerEventHandler player)
	{
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		if (Time.time > m_NextTimeCanInteract)
		{
			((MonoBehaviour)this).StopAllCoroutines();
			((MonoBehaviour)this).StartCoroutine(C_DoAnimation(!m_Open));
			m_NextTimeCanInteract = Time.time + m_InteractionThreeshold;
			if (m_Open)
			{
				m_DoorOpen.PlayAtPosition(ItemSelectionMethod.RandomlyButExcludeLast, ((Component)this).transform.position);
			}
			else
			{
				m_DoorClose.PlayAtPosition(ItemSelectionMethod.RandomlyButExcludeLast, ((Component)this).transform.position);
			}
		}
	}

	private IEnumerator C_DoAnimation(bool open)
	{
		m_Collider.enabled = false;
		m_Open = open;
		Quaternion targetRotation = Quaternion.Euler(open ? m_OpenRotation : m_ClosedRotation);
		while (Quaternion.Angle(targetRotation, ((Component)m_Model).transform.localRotation) > 0.5f)
		{
			((Component)m_Model).transform.localRotation = Quaternion.Lerp(((Component)m_Model).transform.localRotation, targetRotation, Time.deltaTime * m_AnimationSpeed);
			yield return null;
		}
		m_Collider.enabled = true;
	}
}
