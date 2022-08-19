using System;
using UnityEngine;

namespace UltimateSurvival.InputSystem
{
	// Token: 0x02000631 RID: 1585
	[Serializable]
	public class Axis
	{
		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06003248 RID: 12872 RVA: 0x00165327 File Offset: 0x00163527
		// (set) Token: 0x06003249 RID: 12873 RVA: 0x0016532F File Offset: 0x0016352F
		public string AxisName
		{
			get
			{
				return this.m_AxisName;
			}
			set
			{
				this.m_AxisName = value;
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x0600324A RID: 12874 RVA: 0x00165338 File Offset: 0x00163538
		public ET.StandaloneAxisType AxisType
		{
			get
			{
				return this.m_AxisType;
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x0600324B RID: 12875 RVA: 0x00165340 File Offset: 0x00163540
		public string UnityAxisName
		{
			get
			{
				return this.m_UnityAxisName;
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x0600324C RID: 12876 RVA: 0x00165348 File Offset: 0x00163548
		public KeyCode NegativeKey
		{
			get
			{
				return this.m_NegativeKey;
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x0600324D RID: 12877 RVA: 0x00165350 File Offset: 0x00163550
		public KeyCode PositiveKey
		{
			get
			{
				return this.m_PositiveKey;
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x0600324E RID: 12878 RVA: 0x00165358 File Offset: 0x00163558
		public bool Normalize
		{
			get
			{
				return this.m_Normalize;
			}
		}

		// Token: 0x0600324F RID: 12879 RVA: 0x00165360 File Offset: 0x00163560
		public Axis(string name, ET.StandaloneAxisType axisType)
		{
			this.m_AxisName = name;
			this.m_AxisType = axisType;
		}

		// Token: 0x06003250 RID: 12880 RVA: 0x00165360 File Offset: 0x00163560
		public Axis(string name, ET.StandaloneAxisType axisType, Joystick joystick)
		{
			this.m_AxisName = name;
			this.m_AxisType = axisType;
		}

		// Token: 0x06003251 RID: 12881 RVA: 0x00165376 File Offset: 0x00163576
		public Axis(string name, ET.StandaloneAxisType axisType, string unityAxisName)
		{
			this.m_AxisName = name;
			this.m_AxisType = axisType;
			this.m_UnityAxisName = unityAxisName;
		}

		// Token: 0x06003252 RID: 12882 RVA: 0x00165393 File Offset: 0x00163593
		public Axis(string name, ET.StandaloneAxisType axisType, KeyCode positiveKey, KeyCode negativeKey, string unityAxisName)
		{
			this.m_AxisName = name;
			this.m_AxisType = axisType;
			this.m_PositiveKey = positiveKey;
			this.m_NegativeKey = negativeKey;
			this.m_UnityAxisName = unityAxisName;
		}

		// Token: 0x04002CE2 RID: 11490
		[SerializeField]
		private string m_AxisName;

		// Token: 0x04002CE3 RID: 11491
		[SerializeField]
		private bool m_Normalize;

		// Token: 0x04002CE4 RID: 11492
		[SerializeField]
		private ET.StandaloneAxisType m_AxisType;

		// Token: 0x04002CE5 RID: 11493
		[SerializeField]
		private string m_UnityAxisName;

		// Token: 0x04002CE6 RID: 11494
		[SerializeField]
		private KeyCode m_PositiveKey;

		// Token: 0x04002CE7 RID: 11495
		[SerializeField]
		private KeyCode m_NegativeKey;
	}
}
