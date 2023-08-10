using UnityEngine;

public class MovementScript : MonoBehaviour
{
	public float speed = 10f;

	private CharacterController cc;

	private void Awake()
	{
		cc = ((Component)this).GetComponent<CharacterController>();
	}

	private void FixedUpdate()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		Vector3 zero = Vector3.zero;
		zero.x = Input.GetAxis("Horizontal") * speed;
		zero.z = Input.GetAxis("Vertical") * speed;
		cc.SimpleMove(zero);
	}
}
