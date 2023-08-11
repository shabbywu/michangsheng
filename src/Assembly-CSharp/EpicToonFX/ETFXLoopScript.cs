using System.Collections;
using UnityEngine;

namespace EpicToonFX;

public class ETFXLoopScript : MonoBehaviour
{
	public GameObject chosenEffect;

	public float loopTimeLimit = 2f;

	[Header("Spawn without")]
	public bool spawnWithoutLight = true;

	public bool spawnWithoutSound = true;

	private void Start()
	{
		PlayEffect();
	}

	public void PlayEffect()
	{
		((MonoBehaviour)this).StartCoroutine("EffectLoop");
	}

	private IEnumerator EffectLoop()
	{
		GameObject effectPlayer = Object.Instantiate<GameObject>(chosenEffect, ((Component)this).transform.position, ((Component)this).transform.rotation);
		if (spawnWithoutLight = Object.op_Implicit((Object)(object)effectPlayer.GetComponent<Light>()))
		{
			((Behaviour)effectPlayer.GetComponent<Light>()).enabled = false;
		}
		if (spawnWithoutSound = Object.op_Implicit((Object)(object)effectPlayer.GetComponent<AudioSource>()))
		{
			((Behaviour)effectPlayer.GetComponent<AudioSource>()).enabled = false;
		}
		yield return (object)new WaitForSeconds(loopTimeLimit);
		Object.Destroy((Object)(object)effectPlayer);
		PlayEffect();
	}
}
