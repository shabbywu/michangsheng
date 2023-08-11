using UnityEngine;

public class RotationInfinity : MonoBehaviour
{
	private bool jump;

	private bool landed = true;

	private void Update()
	{
		((Component)this).transform.GetChild(0).Rotate(0f, 500f * Time.deltaTime, 0f);
		if (Random.Range(1, 100) < 5 && landed)
		{
			jump = true;
			landed = false;
		}
	}

	private void FixedUpdate()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		if (jump)
		{
			((Component)this).GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 15000f));
			jump = false;
		}
	}

	private void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Footer")
		{
			landed = true;
		}
	}
}
