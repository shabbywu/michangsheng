using UnityEngine;

public class Orbit : MonoBehaviour
{
	public SphericalVector Data = new SphericalVector(0f, 0f, 1f);

	protected virtual void Update()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		((Component)this).gameObject.transform.position = Data.Position;
	}
}
