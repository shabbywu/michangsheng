using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x0200092A RID: 2346
	public class MessageDisplayer : MonoSingleton<MessageDisplayer>
	{
		// Token: 0x06003BC5 RID: 15301 RVA: 0x001AEFBC File Offset: 0x001AD1BC
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

		// Token: 0x06003BC6 RID: 15302 RVA: 0x0002B393 File Offset: 0x00029593
		private void Start()
		{
			this.m_MessageTemplate.SetActive(false);
		}

		// Token: 0x06003BC7 RID: 15303 RVA: 0x0002B3A1 File Offset: 0x000295A1
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

		// Token: 0x0400364E RID: 13902
		[SerializeField]
		private GameObject m_MessageTemplate;

		// Token: 0x0400364F RID: 13903
		[SerializeField]
		private Color m_BaseMessageColor = Color.yellow;

		// Token: 0x04003650 RID: 13904
		[SerializeField]
		private float m_FadeDelay = 3f;

		// Token: 0x04003651 RID: 13905
		[SerializeField]
		private float m_FadeSpeed = 0.3f;
	}
}
