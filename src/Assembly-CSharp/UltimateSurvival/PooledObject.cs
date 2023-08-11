using System.Collections;
using UnityEngine;

namespace UltimateSurvival;

public class PooledObject : MonoBehaviour
{
	public Message<PooledObject> Released = new Message<PooledObject>();

	[SerializeField]
	private bool m_ReleaseOnTimer = true;

	[SerializeField]
	private float m_ReleaseTimer = 20f;

	[SerializeField]
	private ParticleSystem[] m_ToResetParticles;

	private WaitForSeconds m_WaitInterval;

	public virtual void OnUse(Vector3 position = default(Vector3), Quaternion rotation = default(Quaternion), Transform parent = null)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			((Component)this).gameObject.SetActive(true);
			((Component)this).transform.position = position;
			((Component)this).transform.rotation = rotation;
			if (Object.op_Implicit((Object)(object)((Component)this).transform.parent))
			{
				((Component)this).transform.SetParent(parent);
			}
			for (int i = 0; i < m_ToResetParticles.Length; i++)
			{
				m_ToResetParticles[i].Play(true);
			}
			if (m_ReleaseOnTimer)
			{
				((MonoBehaviour)this).StopAllCoroutines();
				((MonoBehaviour)this).StartCoroutine(ReleaseWithDelay());
			}
		}
		catch
		{
		}
	}

	public virtual void OnRelease()
	{
		((Component)this).gameObject.SetActive(false);
		Released.Send(this);
	}

	private void Awake()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Expected O, but got Unknown
		if (m_ReleaseOnTimer)
		{
			m_WaitInterval = new WaitForSeconds(m_ReleaseTimer);
		}
	}

	private IEnumerator ReleaseWithDelay()
	{
		yield return m_WaitInterval;
		OnRelease();
	}
}
