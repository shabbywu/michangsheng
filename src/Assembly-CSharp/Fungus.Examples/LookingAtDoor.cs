using UnityEngine;

namespace Fungus.Examples;

public class LookingAtDoor : MonoBehaviour
{
	public Collider doorCol;

	public float gazeTime = 0.2f;

	private float gazeCounter;

	public BlockReference runBlockWhenGazed;

	public Transform eye;

	public VariableReference fungusBoolHasGazed;

	public void ActivateNow()
	{
		((Behaviour)this).enabled = true;
	}

	private void Update()
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		float num = gazeCounter;
		RaycastHit val = default(RaycastHit);
		if (Physics.Raycast(eye.position, eye.forward, ref val))
		{
			if ((Object)(object)((RaycastHit)(ref val)).collider == (Object)(object)doorCol)
			{
				gazeCounter += Time.deltaTime;
			}
			else
			{
				gazeCounter = 0f;
			}
		}
		else
		{
			gazeCounter = 0f;
		}
		if (gazeCounter >= gazeTime && num <= gazeTime)
		{
			runBlockWhenGazed.Execute();
			fungusBoolHasGazed.Set(val: true);
		}
	}
}
