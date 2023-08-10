using UnityEngine;

public class setBtnLabelShawColor : MonoBehaviour
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
			label.effectColor = hoverColor;
		}
		else
		{
			label.effectColor = hoverColorStart;
		}
	}
}
