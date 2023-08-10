using UnityEngine;

[RequireComponent(typeof(OrbitGameObject))]
public class ShakeCamera : MonoBehaviour
{
	public float Magnitude = 1f;

	public float Duration = 1f;

	public static ShakeCamera Shake(float magnitude, float duration)
	{
		ShakeCamera shakeCamera = ((Component)Camera.main).gameObject.AddComponent<ShakeCamera>();
		shakeCamera.Magnitude = magnitude;
		shakeCamera.Duration = duration;
		return shakeCamera;
	}

	private void Update()
	{
		Duration -= Time.deltaTime;
		if (Duration < 0f)
		{
			Object.Destroy((Object)(object)this);
		}
		((Component)this).gameObject.GetComponent<OrbitGameObject>().ArmOffset.y = Mathf.Sin(1000f * Time.time) * Duration * Magnitude;
	}
}
