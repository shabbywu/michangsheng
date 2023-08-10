using UnityEngine;

public class testrotaion : MonoBehaviour
{
	public float x;

	public float y;

	public float z;

	private void Start()
	{
	}

	private void Update()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		((Component)this).transform.localEulerAngles = new Vector3(x, y, z);
	}
}
