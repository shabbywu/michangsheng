using System;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival.InputSystem
{
	// Token: 0x02000926 RID: 2342
	public class InputData : ScriptableObject
	{
		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06003B9B RID: 15259 RVA: 0x0002B18B File Offset: 0x0002938B
		public ET.InputType InputType
		{
			get
			{
				return this.m_InputType;
			}
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06003B9C RID: 15260 RVA: 0x0002B193 File Offset: 0x00029393
		public List<Button> Buttons
		{
			get
			{
				return this.m_Buttons;
			}
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06003B9D RID: 15261 RVA: 0x0002B19B File Offset: 0x0002939B
		public List<Axis> Axes
		{
			get
			{
				return this.m_Axes;
			}
		}

		// Token: 0x0400363D RID: 13885
		[SerializeField]
		private ET.InputType m_InputType;

		// Token: 0x0400363E RID: 13886
		[SerializeField]
		private List<Button> m_Buttons = new List<Button>();

		// Token: 0x0400363F RID: 13887
		[SerializeField]
		private List<Axis> m_Axes = new List<Axis>();
	}
}
