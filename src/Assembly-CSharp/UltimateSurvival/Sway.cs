using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x02000622 RID: 1570
	[Serializable]
	public class Sway
	{
		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x060031F0 RID: 12784 RVA: 0x00161DA5 File Offset: 0x0015FFA5
		// (set) Token: 0x060031F1 RID: 12785 RVA: 0x00161DAD File Offset: 0x0015FFAD
		public Vector2 Value { get; private set; }

		// Token: 0x060031F2 RID: 12786 RVA: 0x00161DB6 File Offset: 0x0015FFB6
		public Sway GetClone()
		{
			return (Sway)base.MemberwiseClone();
		}

		// Token: 0x060031F3 RID: 12787 RVA: 0x00161DC4 File Offset: 0x0015FFC4
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

		// Token: 0x04002C3F RID: 11327
		[SerializeField]
		private bool Enabled;

		// Token: 0x04002C40 RID: 11328
		[SerializeField]
		private Vector2 Magnitude;

		// Token: 0x04002C41 RID: 11329
		[SerializeField]
		private float LerpSpeed;
	}
}
