using UnityEngine;

public class SubPanelPosition : MonoBehaviour
{
	public enum ScreenDirection
	{
		horizontal,
		vertical
	}

	public ScreenDirection screenDirection;

	public float size;

	private Transform parent;

	private Transform child;

	private float ScaleSize;

	private float rateX;

	private float rateY;

	private UIPanel PanelScript;

	private void Start()
	{
		((MonoBehaviour)this).Invoke("SetPanel", 0.5f);
	}

	private void SetPanel()
	{
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		parent = ((Component)this).transform.parent;
		child = ((Component)this).transform.GetChild(0);
		PanelScript = ((Component)((Component)this).transform).GetComponent<UIPanel>();
		((Component)this).transform.parent = null;
		child.parent = null;
		if (screenDirection == ScreenDirection.vertical)
		{
			rateX = (float)Screen.width / size;
			rateY = 1f;
			ScaleSize = ((Component)this).transform.localScale.y;
		}
		else if (screenDirection == ScreenDirection.horizontal)
		{
			rateX = 1f;
			rateY = (float)Screen.height / size;
			ScaleSize = ((Component)this).transform.localScale.x;
		}
		((Component)this).transform.localScale = Vector4.op_Implicit(new Vector4(ScaleSize, ScaleSize, ScaleSize, ScaleSize));
		((Component)this).transform.parent = parent;
		child.parent = ((Component)this).transform;
		PanelScript.clipRange = new Vector4(PanelScript.clipRange.x, PanelScript.clipRange.y, PanelScript.clipRange.z * rateX, PanelScript.clipRange.w * rateY);
	}
}
