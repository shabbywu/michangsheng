using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus;

[Serializable]
public class FlowchartData
{
	[SerializeField]
	protected string flowchartName;

	[SerializeField]
	protected List<StringVar> stringVars = new List<StringVar>();

	[SerializeField]
	protected List<IntVar> intVars = new List<IntVar>();

	[SerializeField]
	protected List<FloatVar> floatVars = new List<FloatVar>();

	[SerializeField]
	protected List<BoolVar> boolVars = new List<BoolVar>();

	public string FlowchartName
	{
		get
		{
			return flowchartName;
		}
		set
		{
			flowchartName = value;
		}
	}

	public List<StringVar> StringVars
	{
		get
		{
			return stringVars;
		}
		set
		{
			stringVars = value;
		}
	}

	public List<IntVar> IntVars
	{
		get
		{
			return intVars;
		}
		set
		{
			intVars = value;
		}
	}

	public List<FloatVar> FloatVars
	{
		get
		{
			return floatVars;
		}
		set
		{
			floatVars = value;
		}
	}

	public List<BoolVar> BoolVars
	{
		get
		{
			return boolVars;
		}
		set
		{
			boolVars = value;
		}
	}

	public static FlowchartData Encode(Flowchart flowchart)
	{
		FlowchartData flowchartData = new FlowchartData();
		flowchartData.FlowchartName = ((Object)flowchart).name;
		for (int i = 0; i < flowchart.Variables.Count; i++)
		{
			Variable variable = flowchart.Variables[i];
			StringVariable stringVariable = variable as StringVariable;
			if ((Object)(object)stringVariable != (Object)null)
			{
				StringVar item = new StringVar
				{
					Key = stringVariable.Key,
					Value = stringVariable.Value
				};
				flowchartData.StringVars.Add(item);
			}
			IntegerVariable integerVariable = variable as IntegerVariable;
			if ((Object)(object)integerVariable != (Object)null)
			{
				IntVar item2 = new IntVar
				{
					Key = integerVariable.Key,
					Value = integerVariable.Value
				};
				flowchartData.IntVars.Add(item2);
			}
			FloatVariable floatVariable = variable as FloatVariable;
			if ((Object)(object)floatVariable != (Object)null)
			{
				FloatVar item3 = new FloatVar
				{
					Key = floatVariable.Key,
					Value = floatVariable.Value
				};
				flowchartData.FloatVars.Add(item3);
			}
			BooleanVariable booleanVariable = variable as BooleanVariable;
			if ((Object)(object)booleanVariable != (Object)null)
			{
				BoolVar boolVar = new BoolVar();
				boolVar.Key = booleanVariable.Key;
				boolVar.Value = booleanVariable.Value;
				flowchartData.BoolVars.Add(boolVar);
			}
		}
		return flowchartData;
	}

	public static void Decode(FlowchartData flowchartData)
	{
		GameObject val = GameObject.Find(flowchartData.FlowchartName);
		if ((Object)(object)val == (Object)null)
		{
			Debug.LogError((object)"Failed to find flowchart object specified in save data");
			return;
		}
		Flowchart component = val.GetComponent<Flowchart>();
		if ((Object)(object)component == (Object)null)
		{
			Debug.LogError((object)"Failed to find flowchart object specified in save data");
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
}
