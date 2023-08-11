using UnityEngine;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem;

public class RequiredItemRow : MonoBehaviour
{
	[SerializeField]
	private Color m_HaveEnoughColor = Color.white;

	[SerializeField]
	private Color m_DontHaveEnoughColor = Color.red;

	[SerializeField]
	private Text m_Amount;

	[SerializeField]
	private Text m_Type;

	[SerializeField]
	private Text m_Total;

	[SerializeField]
	private Text m_Have;

	public void Set(int amount, string type, int total, int have)
	{
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		bool flag = !string.IsNullOrEmpty(type);
		m_Amount.text = (flag ? amount.ToString() : "");
		m_Type.text = (flag ? type : "");
		m_Total.text = (flag ? total.ToString() : "");
		m_Have.text = (flag ? have.ToString() : "");
		if (flag)
		{
			((Graphic)m_Have).color = ((have >= total) ? m_HaveEnoughColor : m_DontHaveEnoughColor);
		}
	}
}
