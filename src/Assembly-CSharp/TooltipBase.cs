using UnityEngine;

public class TooltipBase : MonoBehaviour
{
	public bool showTooltip;

	public UITexture childTexture;

	public int showType = 1;

	protected Vector3 NowClickPositon = Vector3.zero;

	public bool shoudSetPos = true;

	protected virtual void Start()
	{
		childTexture = ((Component)((Component)this).transform.GetChild(0)).GetComponent<UITexture>();
	}

	protected virtual void Update()
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		if (shoudSetPos)
		{
			if (showTooltip)
			{
				MobileSetPosition();
			}
			else
			{
				((Component)this).transform.position = new Vector3(0f, 10000f, 0f);
			}
		}
	}

	public virtual void MobileSetPosition()
	{
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		if (showType == 2)
		{
			PCSetPosition();
			return;
		}
		if (showType == 3)
		{
			float num = (float)(Screen.height / 2) * 1.24f;
			((Component)this).transform.position = UICamera.currentCamera.ScreenToWorldPoint(new Vector3((float)(Screen.width / 2), num, 0f));
			return;
		}
		int num2 = Screen.width / 2;
		float num3 = ((Input.mousePosition.x < (float)num2) ? ((float)num2 * 1.35f) : ((float)num2 * 0.52f));
		float num4 = (float)(Screen.height / 2) * 1.24f;
		((Component)this).transform.position = UICamera.currentCamera.ScreenToWorldPoint(new Vector3(num3, num4, 0f));
	}

	public Vector3 getMousePosition()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		_ = Input.mousePosition;
		return Input.mousePosition;
	}

	public virtual void PCSetPosition()
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		Vector3 val = default(Vector3);
		((Vector3)(ref val))._002Ector(getMousePosition().x, getMousePosition().y, getMousePosition().z);
		val.x += childTexture.width / 2;
		val.y -= childTexture.height / 2;
		if (Input.mousePosition.x > (float)(Screen.width / 2))
		{
			val.x -= childTexture.width;
		}
		if (Input.mousePosition.y < (float)(Screen.height / 2))
		{
			val.y += childTexture.height;
		}
		((Component)this).transform.position = UICamera.currentCamera.ScreenToWorldPoint(val);
	}
}
