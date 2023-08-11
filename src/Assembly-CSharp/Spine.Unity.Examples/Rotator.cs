using UnityEngine;

namespace Spine.Unity.Examples;

public class Rotator : MonoBehaviour
{
	public Vector3 direction = new Vector3(0f, 0f, 1f);

	public float speed = 1f;

	private void Update()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		((Component)this).transform.Rotate(direction * (speed * Time.deltaTime * 100f));
	}
}
