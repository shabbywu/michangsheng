using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000EC3 RID: 3779
	[Serializable]
	public class FlowchartData
	{
		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x06006ACC RID: 27340 RVA: 0x00293FA5 File Offset: 0x002921A5
		// (set) Token: 0x06006ACD RID: 27341 RVA: 0x00293FAD File Offset: 0x002921AD
		public string FlowchartName
		{
			get
			{
				return this.flowchartName;
			}
			set
			{
				this.flowchartName = value;
			}
		}

		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x06006ACE RID: 27342 RVA: 0x00293FB6 File Offset: 0x002921B6
		// (set) Token: 0x06006ACF RID: 27343 RVA: 0x00293FBE File Offset: 0x002921BE
		public List<StringVar> StringVars
		{
			get
			{
				return this.stringVars;
			}
			set
			{
				this.stringVars = value;
			}
		}

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x06006AD0 RID: 27344 RVA: 0x00293FC7 File Offset: 0x002921C7
		// (set) Token: 0x06006AD1 RID: 27345 RVA: 0x00293FCF File Offset: 0x002921CF
		public List<IntVar> IntVars
		{
			get
			{
				return this.intVars;
			}
			set
			{
				this.intVars = value;
			}
		}

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x06006AD2 RID: 27346 RVA: 0x00293FD8 File Offset: 0x002921D8
		// (set) Token: 0x06006AD3 RID: 27347 RVA: 0x00293FE0 File Offset: 0x002921E0
		public List<FloatVar> FloatVars
		{
			get
			{
				return this.floatVars;
			}
			set
			{
				this.floatVars = value;
			}
		}

		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x06006AD4 RID: 27348 RVA: 0x00293FE9 File Offset: 0x002921E9
		// (set) Token: 0x06006AD5 RID: 27349 RVA: 0x00293FF1 File Offset: 0x002921F1
		public List<BoolVar> BoolVars
		{
			get
			{
				return this.boolVars;
			}
			set
			{
				this.boolVars = value;
			}
		}

		// Token: 0x06006AD6 RID: 27350 RVA: 0x00293FFC File Offset: 0x002921FC
		public static FlowchartData Encode(Flowchart flowchart)
		{
			FlowchartData flowchartData = new FlowchartData();
			flowchartData.FlowchartName = flowchart.name;
			for (int i = 0; i < flowchart.Variables.Count; i++)
			{
				Variable variable = flowchart.Variables[i];
				StringVariable stringVariable = variable as StringVariable;
				if (stringVariable != null)
				{
					StringVar stringVar = new StringVar();
					stringVar.Key = stringVariable.Key;
					stringVar.Value = stringVariable.Value;
					flowchartData.StringVars.Add(stringVar);
				}
				IntegerVariable integerVariable = variable as IntegerVariable;
				if (integerVariable != null)
				{
					IntVar intVar = new IntVar();
					intVar.Key = integerVariable.Key;
					intVar.Value = integerVariable.Value;
					flowchartData.IntVars.Add(intVar);
				}
				FloatVariable floatVariable = variable as FloatVariable;
				if (floatVariable != null)
				{
					FloatVar floatVar = new FloatVar();
					floatVar.Key = floatVariable.Key;
					floatVar.Value = floatVariable.Value;
					flowchartData.FloatVars.Add(floatVar);
				}
				BooleanVariable booleanVariable = variable as BooleanVariable;
				if (booleanVariable != null)
				{
					BoolVar boolVar = new BoolVar();
					boolVar.Key = booleanVariable.Key;
					boolVar.Value = booleanVariable.Value;
					flowchartData.BoolVars.Add(boolVar);
				}
			}
			return flowchartData;
		}

		// Token: 0x06006AD7 RID: 27351 RVA: 0x00294144 File Offset: 0x00292344
		public static void Decode(FlowchartData flowchartData)
		{
			GameObject gameObject = GameObject.Find(flowchartData.FlowchartName);
			if (gameObject == null)
			{
				Debug.LogError("Failed to find flowchart object specified in save data");
				return;
			}
			Flowchart component = gameObject.GetComponent<Flowchart>();
			if (component == null)
			{
				Debug.LogError("Failed to find flowchart object specified in save data");
				return;
			}
			for (int i = 0; i < flowchartData.BoolVars.Count; i++)
			{
				BoolVar boolVar = flowchartData.BoolVars[i];
				component.SetBooleanVariable(boolVar.Key, boolVar.Value);
			}
			for (int j = 0; j < flowchartData.IntVars.Count; j++)
			{
				IntVar intVar = flowchartData.IntVars[j];
				component.SetIntegerVariable(intVar.Key, intVar.Value);
			}
			for (int k = 0; k < flowchartData.FloatVars.Count; k++)
			{
				FloatVar floatVar = flowchartData.FloatVars[k];
				component.SetFloatVariable(floatVar.Key, floatVar.Value);
			}
			for (int l = 0; l < flowchartData.StringVars.Count; l++)
			{
				StringVar stringVar = flowchartData.StringVars[l];
				component.SetStringVariable(stringVar.Key, stringVar.Value);
			}
		}

		// Token: 0x04005A0B RID: 23051
		[SerializeField]
		protected string flowchartName;

		// Token: 0x04005A0C RID: 23052
		[SerializeField]
		protected List<StringVar> stringVars = new List<StringVar>();

		// Token: 0x04005A0D RID: 23053
		[SerializeField]
		protected List<IntVar> intVars = new List<IntVar>();

		// Token: 0x04005A0E RID: 23054
		[SerializeField]
		protected List<FloatVar> floatVars = new List<FloatVar>();

		// Token: 0x04005A0F RID: 23055
		[SerializeField]
		protected List<BoolVar> boolVars = new List<BoolVar>();
	}
}
