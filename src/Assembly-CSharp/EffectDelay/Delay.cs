using UnityEngine;

namespace EffectDelay;

public class Delay : MonoBehaviour
{
	public float delayTime = 1f;

	private void Start()
	{
		((Component)this).gameObject.SetActiveRecursively(false);
		((MonoBehaviour)this).Invoke("DelayFunc", delayTime);
	}

	private void DelayFunc()
	{
		((Component)this).gameObject.SetActiveRecursively(true);
	}
}
