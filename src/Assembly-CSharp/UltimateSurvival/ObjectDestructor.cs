using UnityEngine;

namespace UltimateSurvival;

public class ObjectDestructor : MonoBehaviour
{
	[SerializeField]
	private float m_TimeOut = 1f;

	[SerializeField]
	private bool m_DetachChildren;

	private void Awake()
	{
		((MonoBehaviour)this).Invoke("DestroyNow", m_TimeOut);
	}

	private void DestroyNow()
	{
		if (m_DetachChildren)
		{
			((Component)this).transform.DetachChildren();
		}
		Object.DestroyObject((Object)(object)((Component)this).gameObject);
	}
}
