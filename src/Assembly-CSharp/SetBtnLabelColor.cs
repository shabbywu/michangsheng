using UnityEngine;

public class SetBtnLabelColor : MonoBehaviour
{
	public Color hoverColor;

	public Color hoverColorStart;

	private UILabel label;

	private void Start()
	{
		label = ((Component)this).GetComponentInChildren<UILabel>();
	}

	public void OnHover(bool isOver)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		if (isOver)
		{
			label.color = hoverColor;
		}
		else
		{
			label.color = hoverColorStart;
		}
	}

	private void Update()
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)UICamera.GetMouse(0).current != (Object)(object)((Component)this).gameObject)
		{
			label.color = hoverColorStart;
		}
	}
}
