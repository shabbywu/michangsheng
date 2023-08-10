using UnityEngine;

namespace EpicToonFX;

public class ETFXPitchRandomizer : MonoBehaviour
{
	public float randomPercent = 10f;

	private void Start()
	{
		AudioSource component = ((Component)((Component)this).transform).GetComponent<AudioSource>();
		component.pitch *= 1f + Random.Range((0f - randomPercent) / 100f, randomPercent / 100f);
	}
}
