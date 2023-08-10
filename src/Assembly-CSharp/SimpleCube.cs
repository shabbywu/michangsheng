using EIU;
using UnityEngine;

public class SimpleCube : MonoBehaviour
{
	public float moveSpeed = 5f;

	public float jumpLimit = 5f;

	private void Start()
	{
	}

	private void Update()
	{
		if (Object.op_Implicit((Object)(object)EasyInputUtility.instance))
		{
			((Component)this).transform.Translate(moveSpeed * EasyInputUtility.instance.GetAxis("Horizontal") * Time.deltaTime, 0f, moveSpeed * EasyInputUtility.instance.GetAxis("Vertical") * Time.deltaTime);
		}
		else
		{
			((Component)this).transform.Translate(moveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime, 0f, moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime);
		}
	}
}
