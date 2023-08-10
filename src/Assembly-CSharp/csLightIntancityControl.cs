using UnityEngine;

public class csLightIntancityControl : MonoBehaviour
{
	public Light _light;

	private float _time;

	public float Delay = 0.5f;

	public float Down = 1f;

	private void Update()
	{
		_time += Time.deltaTime;
		if (_time > Delay)
		{
			if (_light.intensity > 0f)
			{
				Light light = _light;
				light.intensity -= Time.deltaTime * Down;
			}
			if (_light.intensity <= 0f)
			{
				_light.intensity = 0f;
			}
		}
	}
}
