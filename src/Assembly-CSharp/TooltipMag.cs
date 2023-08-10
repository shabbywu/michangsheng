using UnityEngine;

public class TooltipMag : MonoBehaviour
{
	public static TooltipMag inst;

	private void Awake()
	{
		inst = this;
	}
}
