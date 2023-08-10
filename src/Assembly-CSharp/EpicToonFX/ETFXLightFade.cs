using UnityEngine;

namespace EpicToonFX;

public class ETFXLightFade : MonoBehaviour
{
	[Header("Seconds to dim the light")]
	public float life = 0.2f;

	public bool killAfterLife = true;

	private Light li;

	private float initIntensity;

	private void Start()
	{
		if (Object.op_Implicit((Object)(object)((Component)this).gameObject.GetComponent<Light>()))
		{
			li = ((Component)this).gameObject.GetComponent<Light>();
			initIntensity = li.intensity;
		}
		else
		{
			MonoBehaviour.print((object)("No light object found on " + ((Object)((Component)this).gameObject).name));
		}
	}

	private void Update()
	{
		if (Object.op_Implicit((Object)(object)((Component)this).gameObject.GetComponent<Light>()))
		{
			Light obj = li;
			obj.intensity -= initIntensity * (Time.deltaTime / life);
			if (killAfterLife && li.intensity <= 0f)
			{
				Object.Destroy((Object)(object)((Component)this).gameObject.GetComponent<Light>());
			}
		}
	}
}
