using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class CFX_AutoDestructShuriken : MonoBehaviour
{
	public bool OnlyDeactivate;

	private void OnEnable()
	{
		((MonoBehaviour)this).StartCoroutine("CheckIfAlive");
	}

	private IEnumerator CheckIfAlive()
	{
		do
		{
			yield return (object)new WaitForSeconds(0.5f);
		}
		while (((Component)this).GetComponent<ParticleSystem>().IsAlive(true));
		if (OnlyDeactivate)
		{
			((Component)this).gameObject.SetActive(false);
		}
		else
		{
			Object.Destroy((Object)(object)((Component)this).gameObject);
		}
	}
}
