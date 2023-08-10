using UnityEngine;

[ExecuteInEditMode]
public class AutoSetSize : MonoBehaviour
{
	public int width = 2340;

	public int higt = 1080;

	public int StartHigh;

	public int Startwidth;

	private void Start()
	{
	}

	private void Update()
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		float num = 2.1666667f;
		float num2 = (float)Screen.width / (float)Screen.height / num;
		((Component)this).transform.localScale = Vector3.one * num2;
		((Component)this).transform.localPosition = new Vector3((float)Startwidth, (float)StartHigh - (float)StartHigh * (1f - num2) * 2f, 0f);
	}
}
