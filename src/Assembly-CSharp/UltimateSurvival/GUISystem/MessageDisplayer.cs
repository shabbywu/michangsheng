using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x02000638 RID: 1592
	public class MessageDisplayer : MonoSingleton<MessageDisplayer>
	{
		// Token: 0x0600328B RID: 12939 RVA: 0x00165B6C File Offset: 0x00163D6C
		public void PushMessage(string message, Color color = default(Color), int lineHeight = 16)
		{
			if (color == default(Color))
			{
				color = this.m_BaseMessageColor;
			}
			GameObject gameObject = Object.Instantiate<GameObject>(this.m_MessageTemplate, this.m_MessageTemplate.transform.parent, false);
			gameObject.SetActive(true);
			gameObject.transform.SetAsLastSibling();
			Text component = gameObject.GetComponent<Text>();
			CanvasGroup component2 = component.GetComponent<CanvasGroup>();
			if (component && component2)
			{
				component.text = message;
				component.color = new Color(color.r, color.g, color.b, 1f);
				component.GetComponent<LayoutElement>().preferredHeight = (float)lineHeight;
				component2.alpha = color.a;
				base.StartCoroutine(this.FadeMessage(component2));
			}
		}

		// Token: 0x0600328C RID: 12940 RVA: 0x00165C2D File Offset: 0x00163E2D
		private void Start()
		{
			this.m_MessageTemplate.SetActive(false);
		}

		// Token: 0x0600328D RID: 12941 RVA: 0x00165C3B File Offset: 0x00163E3B
		private IEnumerator FadeMessage(CanvasGroup group)
		{
			if (!group)
			{
				yield break;
			}
			yield return new WaitForSeconds(this.m_FadeDelay);
			while (group.alpha > Mathf.Epsilon)
			{
				group.alpha = Mathf.MoveTowards(group.alpha, 0f, Time.deltaTime * this.m_FadeSpeed);
				yield return null;
			}
			Object.Destroy(group.gameObject);
			yield break;
		}

		// Token: 0x04002CFD RID: 11517
		[SerializeField]
		private GameObject m_MessageTemplate;

		// Token: 0x04002CFE RID: 11518
		[SerializeField]
		private Color m_BaseMessageColor = Color.yellow;

		// Token: 0x04002CFF RID: 11519
		[SerializeField]
		private float m_FadeDelay = 3f;

		// Token: 0x04002D00 RID: 11520
		[SerializeField]
		private float m_FadeSpeed = 0.3f;
	}
}
