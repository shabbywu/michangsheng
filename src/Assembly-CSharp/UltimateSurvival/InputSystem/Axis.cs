using System;
using UnityEngine;

namespace UltimateSurvival.InputSystem
{
	// Token: 0x02000923 RID: 2339
	[Serializable]
	public class Axis
	{
		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06003B82 RID: 15234 RVA: 0x0002B07E File Offset: 0x0002927E
		// (set) Token: 0x06003B83 RID: 15235 RVA: 0x0002B086 File Offset: 0x00029286
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

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x06003B84 RID: 15236 RVA: 0x0002B08F File Offset: 0x0002928F
		public ET.StandaloneAxisType AxisType
		{
			get
			{
				return this.m_AxisType;
			}
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06003B85 RID: 15237 RVA: 0x0002B097 File Offset: 0x00029297
		public string UnityAxisName
		{
			get
			{
				return this.m_UnityAxisName;
			}
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06003B86 RID: 15238 RVA: 0x0002B09F File Offset: 0x0002929F
		public KeyCode NegativeKey
		{
			get
			{
				return this.m_NegativeKey;
			}
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06003B87 RID: 15239 RVA: 0x0002B0A7 File Offset: 0x000292A7
		public KeyCode PositiveKey
		{
			get
			{
				return this.m_PositiveKey;
			}
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06003B88 RID: 15240 RVA: 0x0002B0AF File Offset: 0x000292AF
		public bool Normalize
		{
			get
			{
				return this.m_Normalize;
			}
		}

		// Token: 0x06003B89 RID: 15241 RVA: 0x0002B0B7 File Offset: 0x000292B7
		public Axis(string name, ET.StandaloneAxisType axisType)
		{
			this.m_AxisName = name;
			this.m_AxisType = axisType;
		}

		// Token: 0x06003B8A RID: 15242 RVA: 0x0002B0B7 File Offset: 0x000292B7
		public Axis(string name, ET.StandaloneAxisType axisType, Joystick joystick)
		{
			this.m_AxisName = name;
			this.m_AxisType = axisType;
		}

		// Token: 0x06003B8B RID: 15243 RVA: 0x0002B0CD File Offset: 0x000292CD
		public Axis(string name, ET.StandaloneAxisType axisType, string unityAxisName)
		{
			this.m_AxisName = name;
			this.m_AxisType = axisType;
			this.m_UnityAxisName = unityAxisName;
		}

		// Token: 0x06003B8C RID: 15244 RVA: 0x0002B0EA File Offset: 0x000292EA
		public Axis(string name, ET.StandaloneAxisType axisType, KeyCode positiveKey, KeyCode negativeKey, string unityAxisName)
		{
			this.m_AxisName = name;
			this.m_AxisType = axisType;
			this.m_PositiveKey = positiveKey;
			this.m_NegativeKey = negativeKey;
			this.m_UnityAxisName = unityAxisName;
		}

		// Token: 0x04003633 RID: 13875
		[SerializeField]
		private string m_AxisName;

		// Token: 0x04003634 RID: 13876
		[SerializeField]
		private bool m_Normalize;

		// Token: 0x04003635 RID: 13877
		[SerializeField]
		private ET.StandaloneAxisType m_AxisType;

		// Token: 0x04003636 RID: 13878
		[SerializeField]
		private string m_UnityAxisName;

		// Token: 0x04003637 RID: 13879
		[SerializeField]
		private KeyCode m_PositiveKey;

		// Token: 0x04003638 RID: 13880
		[SerializeField]
		private KeyCode m_NegativeKey;
	}
}
