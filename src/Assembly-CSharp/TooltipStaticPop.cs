using UnityEngine;

public class TooltipStaticPop : TooltipBase
{
	public Vector3 pos;

	public Vector3 rotation;

	private new void Start()
	{
	}

	private new void Update()
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		if (shoudSetPos)
		{
			if (showTooltip)
			{
				((Component)this).transform.position = pos;
			}
			else
			{
				((Component)this).transform.position = new Vector3(0f, 10000f, 0f);
			}
		}
	}
}
