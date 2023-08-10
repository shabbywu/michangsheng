using UnityEngine;

public class UbrzanjeTest : MonoBehaviour
{
	private float force = 100f;

	private void Start()
	{
	}

	private void FixedUpdate()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		((Component)this).GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, force));
		force += 5f;
	}
}
