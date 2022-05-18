using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x02000909 RID: 2313
	[Serializable]
	public class Sway
	{
		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x06003B24 RID: 15140 RVA: 0x0002ADC9 File Offset: 0x00028FC9
		// (set) Token: 0x06003B25 RID: 15141 RVA: 0x0002ADD1 File Offset: 0x00028FD1
		public Vector2 Value { get; private set; }

		// Token: 0x06003B26 RID: 15142 RVA: 0x0002ADDA File Offset: 0x00028FDA
		public Sway GetClone()
		{
			return (Sway)base.MemberwiseClone();
		}

		// Token: 0x06003B27 RID: 15143 RVA: 0x001AB658 File Offset: 0x001A9858
		public void CalculateSway(Vector2 input, float deltaTime)
		{
			if (!this.Enabled)
			{
				return;
			}
			Vector2 magnitude = this.Magnitude;
			magnitude.Scale(input);
			this.Value = Vector2.Lerp(this.Value, magnitude, deltaTime * this.LerpSpeed);
		}

		// Token: 0x04003566 RID: 13670
		[SerializeField]
		private bool Enabled;

		// Token: 0x04003567 RID: 13671
		[SerializeField]
		private Vector2 Magnitude;

		// Token: 0x04003568 RID: 13672
		[SerializeField]
		private float LerpSpeed;
	}
}
