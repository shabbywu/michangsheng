using UnityEngine;

public class SetSkillBan : MonoBehaviour
{
	private void Start()
	{
	}

	public void OnValueChenge(bool b)
	{
		if (b)
		{
			Show();
		}
		else
		{
			Hide();
		}
	}

	public void Show()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		RectTransform component = ((Component)((Component)this).transform.parent).GetComponent<RectTransform>();
		component.sizeDelta = new Vector2(component.sizeDelta.x, 100f);
	}

	public void Hide()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		RectTransform component = ((Component)((Component)this).transform.parent).GetComponent<RectTransform>();
		component.sizeDelta = new Vector2(component.sizeDelta.x, -70.7f);
	}
}
