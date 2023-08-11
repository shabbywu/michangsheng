using UnityEngine;

public class FlowmapAnimator : MonoBehaviour
{
	public float flowSpeed;

	private Material currentMaterial;

	private float cycle;

	private float halfCycle;

	private float flowMapOffset0;

	private float flowMapOffset1;

	private bool hasTide;

	private void Reset()
	{
		flowSpeed = 0.25f;
	}

	private void Start()
	{
		currentMaterial = ((Component)this).GetComponent<Renderer>().material;
		cycle = 6f;
		halfCycle = cycle * 0.5f;
		flowMapOffset0 = 0f;
		flowMapOffset1 = halfCycle;
		currentMaterial.SetFloat("halfCycle", halfCycle);
	}

	private void Update()
	{
		flowMapOffset0 += flowSpeed * Time.deltaTime;
		flowMapOffset1 += flowSpeed * Time.deltaTime;
		while (flowMapOffset0 >= cycle)
		{
			flowMapOffset0 -= cycle;
		}
		while (flowMapOffset1 >= cycle)
		{
			flowMapOffset1 -= cycle;
		}
		currentMaterial.SetFloat("flowMapOffset0", flowMapOffset0);
		currentMaterial.SetFloat("flowMapOffset1", flowMapOffset1);
	}
}
