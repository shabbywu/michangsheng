using System.Collections;
using UnityEngine;

namespace UltimateSurvival;

public class ParticleDestroyer : MonoBehaviour
{
	[SerializeField]
	private float m_MinDuration = 8f;

	[SerializeField]
	private float m_MaxDuration = 10f;

	private float m_MaxLifetime;

	private bool m_EarlyStop;

	private IEnumerator Start()
	{
		ParticleSystem[] systems = ((Component)this).GetComponentsInChildren<ParticleSystem>();
		ParticleSystem[] array = systems;
		foreach (ParticleSystem val in array)
		{
			ParticleDestroyer particleDestroyer = this;
			MainModule main = val.main;
			particleDestroyer.m_MaxLifetime = Mathf.Max(((MainModule)(ref main)).startLifetimeMultiplier, m_MaxLifetime);
		}
		float stopTime = Time.time + Random.Range(m_MinDuration, m_MaxDuration);
		while (Time.time < stopTime || m_EarlyStop)
		{
			yield return null;
		}
		array = systems;
		for (int i = 0; i < array.Length; i++)
		{
			EmissionModule emission = array[i].emission;
			((EmissionModule)(ref emission)).enabled = false;
		}
		yield return (object)new WaitForSeconds(m_MaxLifetime);
		Object.Destroy((Object)(object)((Component)this).gameObject);
	}

	public void Stop()
	{
		m_EarlyStop = true;
	}
}
