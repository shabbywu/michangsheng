using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

namespace UltimateSurvival;

public class SceneReflection : MonoBehaviour
{
	[SerializeField]
	private ReflectionProbe m_ReflectionProbe;

	private IEnumerator Start()
	{
		WaitForSeconds waitInterval = new WaitForSeconds(0.2f);
		while (true)
		{
			m_ReflectionProbe.refreshMode = (ReflectionProbeRefreshMode)2;
			m_ReflectionProbe.RenderProbe();
			yield return waitInterval;
		}
	}
}
