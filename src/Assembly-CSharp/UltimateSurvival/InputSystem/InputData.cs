using System;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival.InputSystem
{
	// Token: 0x02000634 RID: 1588
	public class InputData : ScriptableObject
	{
		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06003261 RID: 12897 RVA: 0x001654A2 File Offset: 0x001636A2
		public ET.InputType InputType
		{
			get
			{
				return this.m_InputType;
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06003262 RID: 12898 RVA: 0x001654AA File Offset: 0x001636AA
		public List<Button> Buttons
		{
			get
			{
				return this.m_Buttons;
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06003263 RID: 12899 RVA: 0x001654B2 File Offset: 0x001636B2
		public List<Axis> Axes
		{
			get
			{
				return this.m_Axes;
			}
		}

		// Token: 0x04002CEC RID: 11500
		[SerializeField]
		private ET.InputType m_InputType;

		// Token: 0x04002CED RID: 11501
		[SerializeField]
		private List<Button> m_Buttons = new List<Button>();

		// Token: 0x04002CEE RID: 11502
		[SerializeField]
		private List<Axis> m_Axes = new List<Axis>();
	}
}
