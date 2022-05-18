using System;
using UnityEngine;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x02000953 RID: 2387
	public class DynamicCrosshair : MonoBehaviour
	{
		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06003CFA RID: 15610 RVA: 0x0002BF16 File Offset: 0x0002A116
		public float Distance
		{
			get
			{
				return this.m_Distance;
			}
		}

		// Token: 0x06003CFB RID: 15611 RVA: 0x001B2B0C File Offset: 0x001B0D0C
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

		// Token: 0x06003CFC RID: 15612 RVA: 0x001B2B50 File Offset: 0x001B0D50
		public void SetDistance(float distance)
		{
			this.m_Left.rectTransform.anchoredPosition = new Vector2(-distance, 0f);
			this.m_Right.rectTransform.anchoredPosition = new Vector2(distance, 0f);
			this.m_Down.rectTransform.anchoredPosition = new Vector2(0f, -distance);
			this.m_Up.rectTransform.anchoredPosition = new Vector2(0f, distance);
			this.m_Distance = distance;
		}

		// Token: 0x06003CFD RID: 15613 RVA: 0x001B2BD4 File Offset: 0x001B0DD4
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

		// Token: 0x06003CFE RID: 15614 RVA: 0x0002BF1E File Offset: 0x0002A11E
		private void OnValidate()
		{
			if (!Application.isPlaying)
			{
				this.SetDistance(this.m_Distance);
			}
		}

		// Token: 0x0400373C RID: 14140
		[SerializeField]
		[Clamp(0f, 256f)]
		private float m_Distance = 32f;

		// Token: 0x0400373D RID: 14141
		[Header("Crosshair Parts")]
		[SerializeField]
		private Image m_Left;

		// Token: 0x0400373E RID: 14142
		[SerializeField]
		private Image m_Right;

		// Token: 0x0400373F RID: 14143
		[SerializeField]
		private Image m_Down;

		// Token: 0x04003740 RID: 14144
		[SerializeField]
		private Image m_Up;
	}
}
