using UnityEngine;

namespace EpicToonFX;

public class ETFXRotation : MonoBehaviour
{
	public enum spaceEnum
	{
		Local,
		World
	}

	[Header("Rotate axises by degrees per second")]
	public Vector3 rotateVector = Vector3.zero;

	public spaceEnum rotateSpace;

	private void Start()
	{
	}

	private void Update()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		if (rotateSpace == spaceEnum.Local)
		{
			((Component)this).transform.Rotate(rotateVector * Time.deltaTime);
		}
		if (rotateSpace == spaceEnum.World)
		{
			((Component)this).transform.Rotate(rotateVector * Time.deltaTime, (Space)0);
		}
	}
}
