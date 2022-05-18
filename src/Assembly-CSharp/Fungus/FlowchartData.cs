using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001361 RID: 4961
	[Serializable]
	public class FlowchartData
	{
		// Token: 0x17000B47 RID: 2887
		// (get) Token: 0x06007867 RID: 30823 RVA: 0x00051CA9 File Offset: 0x0004FEA9
		// (set) Token: 0x06007868 RID: 30824 RVA: 0x00051CB1 File Offset: 0x0004FEB1
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

		// Token: 0x17000B48 RID: 2888
		// (get) Token: 0x06007869 RID: 30825 RVA: 0x00051CBA File Offset: 0x0004FEBA
		// (set) Token: 0x0600786A RID: 30826 RVA: 0x00051CC2 File Offset: 0x0004FEC2
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

		// Token: 0x17000B49 RID: 2889
		// (get) Token: 0x0600786B RID: 30827 RVA: 0x00051CCB File Offset: 0x0004FECB
		// (set) Token: 0x0600786C RID: 30828 RVA: 0x00051CD3 File Offset: 0x0004FED3
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

		// Token: 0x17000B4A RID: 2890
		// (get) Token: 0x0600786D RID: 30829 RVA: 0x00051CDC File Offset: 0x0004FEDC
		// (set) Token: 0x0600786E RID: 30830 RVA: 0x00051CE4 File Offset: 0x0004FEE4
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

		// Token: 0x17000B4B RID: 2891
		// (get) Token: 0x0600786F RID: 30831 RVA: 0x00051CED File Offset: 0x0004FEED
		// (set) Token: 0x06007870 RID: 30832 RVA: 0x00051CF5 File Offset: 0x0004FEF5
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

		// Token: 0x06007871 RID: 30833 RVA: 0x002B640C File Offset: 0x002B460C
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

		// Token: 0x06007872 RID: 30834 RVA: 0x002B6554 File Offset: 0x002B4754
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

		// Token: 0x0400686A RID: 26730
		[SerializeField]
		protected string flowchartName;

		// Token: 0x0400686B RID: 26731
		[SerializeField]
		protected List<StringVar> stringVars = new List<StringVar>();

		// Token: 0x0400686C RID: 26732
		[SerializeField]
		protected List<IntVar> intVars = new List<IntVar>();

		// Token: 0x0400686D RID: 26733
		[SerializeField]
		protected List<FloatVar> floatVars = new List<FloatVar>();

		// Token: 0x0400686E RID: 26734
		[SerializeField]
		protected List<BoolVar> boolVars = new List<BoolVar>();
	}
}
