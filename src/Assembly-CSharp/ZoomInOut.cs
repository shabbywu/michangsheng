using UnityEngine;

public class ZoomInOut : MonoBehaviour
{
	public float distance = 50f;

	public float sensitivityDistance = 50f;

	public float damping = 50f;

	public float minFOV = 5f;

	public float maxFOV = 100f;

	private void Start()
	{
		distance = ((Component)this).GetComponent<Camera>().fieldOfView;
	}

	private void Update()
	{
		distance -= Input.GetAxis("Mouse ScrollWheel") * sensitivityDistance;
		distance = Mathf.Clamp(distance, minFOV, maxFOV);
		((Component)this).GetComponent<Camera>().fieldOfView = Mathf.Lerp(((Component)this).GetComponent<Camera>().fieldOfView, distance, Time.deltaTime * damping);
	}
}
