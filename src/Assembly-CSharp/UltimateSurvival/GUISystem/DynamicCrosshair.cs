using System;
using UnityEngine;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x02000653 RID: 1619
	public class DynamicCrosshair : MonoBehaviour
	{
		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x0600337E RID: 13182 RVA: 0x00169A3C File Offset: 0x00167C3C
		public float Distance
		{
			get
			{
				return this.m_Distance;
			}
		}

		// Token: 0x0600337F RID: 13183 RVA: 0x00169A44 File Offset: 0x00167C44
		public void SetActive(bool active)
		{
			Behaviour left = this.m_Left;
			Behaviour right = this.m_Right;
			Behaviour down = this.m_Down;
			this.m_Up.enabled = active;
			down.enabled = active;
			right.enabled = active;
			left.enabled = active;
		}

		// Token: 0x06003380 RID: 13184 RVA: 0x00169A88 File Offset: 0x00167C88
		public void SetDistance(float distance)
		{
			this.m_Left.rectTransform.anchoredPosition = new Vector2(-distance, 0f);
			this.m_Right.rectTransform.anchoredPosition = new Vector2(distance, 0f);
			this.m_Down.rectTransform.anchoredPosition = new Vector2(0f, -distance);
			this.m_Up.rectTransform.anchoredPosition = new Vector2(0f, distance);
			this.m_Distance = distance;
		}

		// Token: 0x06003381 RID: 13185 RVA: 0x00169B0C File Offset: 0x00167D0C
		public void SetColor(Color color)
		{
			Graphic left = this.m_Left;
			Graphic right = this.m_Right;
			Graphic down = this.m_Down;
			this.m_Up.color = color;
			down.color = color;
			right.color = color;
			left.color = color;
		}

		// Token: 0x06003382 RID: 13186 RVA: 0x00169B4F File Offset: 0x00167D4F
		private void OnValidate()
		{
			if (!Application.isPlaying)
			{
				this.SetDistance(this.m_Distance);
			}
		}

		// Token: 0x04002DC1 RID: 11713
		[SerializeField]
		[Clamp(0f, 256f)]
		private float m_Distance = 32f;

		// Token: 0x04002DC2 RID: 11714
		[Header("Crosshair Parts")]
		[SerializeField]
		private Image m_Left;

		// Token: 0x04002DC3 RID: 11715
		[SerializeField]
		private Image m_Right;

		// Token: 0x04002DC4 RID: 11716
		[SerializeField]
		private Image m_Down;

		// Token: 0x04002DC5 RID: 11717
		[SerializeField]
		private Image m_Up;
	}
}
