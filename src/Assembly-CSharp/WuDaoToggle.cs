using UnityEngine;
using UnityEngine.UI;

public class WuDaoToggle : MonoBehaviour
{
	private Toggle toggle;

	public Text wuDaoname;

	public Image iconType;

	private void Start()
	{
		toggle = ((Component)this).GetComponent<Toggle>();
	}

	public void OnClick()
	{
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		if (toggle.isOn)
		{
			((Graphic)wuDaoname).color = new Color(255f, 252f, 167f);
			((Graphic)iconType).color = new Color(255f, 252f, 167f);
		}
		else
		{
			((Graphic)wuDaoname).color = new Color(173f, 87f, 35f);
			((Graphic)iconType).color = new Color(189f, 101f, 33f);
		}
	}

	private void Update()
	{
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)toggle != (Object)null)
		{
			if (toggle.isOn)
			{
				((Graphic)wuDaoname).color = new Color(1f, 84f / 85f, 0.654902f);
				((Graphic)iconType).color = new Color(1f, 84f / 85f, 0.654902f);
			}
			else
			{
				((Graphic)wuDaoname).color = new Color(0.6784314f, 29f / 85f, 7f / 51f);
				((Graphic)iconType).color = new Color(63f / 85f, 0.39607844f, 11f / 85f);
			}
		}
	}
}
