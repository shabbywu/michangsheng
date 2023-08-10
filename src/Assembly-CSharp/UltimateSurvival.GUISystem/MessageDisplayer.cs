using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem;

public class MessageDisplayer : MonoSingleton<MessageDisplayer>
{
	[SerializeField]
	private GameObject m_MessageTemplate;

	[SerializeField]
	private Color m_BaseMessageColor = Color.yellow;

	[SerializeField]
	private float m_FadeDelay = 3f;

	[SerializeField]
	private float m_FadeSpeed = 0.3f;

	public void PushMessage(string message, Color color = default(Color), int lineHeight = 16)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		if (color == default(Color))
		{
			color = m_BaseMessageColor;
		}
		GameObject obj = Object.Instantiate<GameObject>(m_MessageTemplate, m_MessageTemplate.transform.parent, false);
		obj.SetActive(true);
		obj.transform.SetAsLastSibling();
		Text component = obj.GetComponent<Text>();
		CanvasGroup component2 = ((Component)component).GetComponent<CanvasGroup>();
		if (Object.op_Implicit((Object)(object)component) && Object.op_Implicit((Object)(object)component2))
		{
			component.text = message;
			((Graphic)component).color = new Color(color.r, color.g, color.b, 1f);
			((Component)component).GetComponent<LayoutElement>().preferredHeight = lineHeight;
			component2.alpha = color.a;
			((MonoBehaviour)this).StartCoroutine(FadeMessage(component2));
		}
	}

	private void Start()
	{
		m_MessageTemplate.SetActive(false);
	}

	private IEnumerator FadeMessage(CanvasGroup group)
	{
		if (Object.op_Implicit((Object)(object)group))
		{
			yield return (object)new WaitForSeconds(m_FadeDelay);
			while (group.alpha > Mathf.Epsilon)
			{
				group.alpha = Mathf.MoveTowards(group.alpha, 0f, Time.deltaTime * m_FadeSpeed);
				yield return null;
			}
			Object.Destroy((Object)(object)((Component)group).gameObject);
		}
	}
}
