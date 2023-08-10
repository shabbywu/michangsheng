using System;
using UnityEngine;

namespace Fungus;

[CommandInfo("Transform", "Property", "Get or Set a property of a transform component", 0)]
[AddComponentMenu("")]
public class TransformProperty : Command
{
	public enum GetSet
	{
		Get,
		Set
	}

	public enum Property
	{
		ChildCount,
		EulerAngles,
		Forward,
		HasChanged,
		HierarchyCapacity,
		HierarchyCount,
		LocalEulerAngles,
		LocalPosition,
		LocalScale,
		LossyScale,
		Parent,
		Position,
		Right,
		Root,
		Up
	}

	public GetSet getOrSet;

	[SerializeField]
	protected Property property = Property.Position;

	[SerializeField]
	protected TransformData transformData;

	[SerializeField]
	[VariableProperty(new Type[]
	{
		typeof(BooleanVariable),
		typeof(IntegerVariable),
		typeof(Vector3Variable),
		typeof(TransformVariable)
	})]
	protected Variable inOutVar;

	public override void OnEnter()
	{
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		BooleanVariable booleanVariable = inOutVar as BooleanVariable;
		IntegerVariable integerVariable = inOutVar as IntegerVariable;
		Vector3Variable vector3Variable = inOutVar as Vector3Variable;
		TransformVariable transformVariable = inOutVar as TransformVariable;
		Transform value = transformData.Value;
		switch (getOrSet)
		{
		case GetSet.Get:
			switch (property)
			{
			case Property.ChildCount:
				integerVariable.Value = value.childCount;
				break;
			case Property.EulerAngles:
				vector3Variable.Value = value.eulerAngles;
				break;
			case Property.Forward:
				vector3Variable.Value = value.forward;
				break;
			case Property.HasChanged:
				booleanVariable.Value = value.hasChanged;
				break;
			case Property.HierarchyCapacity:
				integerVariable.Value = value.hierarchyCapacity;
				break;
			case Property.HierarchyCount:
				integerVariable.Value = value.hierarchyCount;
				break;
			case Property.LocalEulerAngles:
				vector3Variable.Value = value.localEulerAngles;
				break;
			case Property.LocalPosition:
				vector3Variable.Value = value.localPosition;
				break;
			case Property.LocalScale:
				vector3Variable.Value = value.localScale;
				break;
			case Property.LossyScale:
				vector3Variable.Value = value.lossyScale;
				break;
			case Property.Parent:
				transformVariable.Value = value.parent;
				break;
			case Property.Position:
				vector3Variable.Value = value.position;
				break;
			case Property.Right:
				vector3Variable.Value = value.right;
				break;
			case Property.Root:
				transformVariable.Value = value.parent;
				break;
			case Property.Up:
				vector3Variable.Value = value.up;
				break;
			}
			break;
		case GetSet.Set:
			switch (property)
			{
			case Property.ChildCount:
				Debug.LogWarning((object)"Cannot Set childCount, it is read only");
				break;
			case Property.EulerAngles:
				value.eulerAngles = vector3Variable.Value;
				break;
			case Property.Forward:
				value.forward = vector3Variable.Value;
				break;
			case Property.HasChanged:
				value.hasChanged = booleanVariable.Value;
				break;
			case Property.HierarchyCapacity:
				value.hierarchyCapacity = integerVariable.Value;
				break;
			case Property.HierarchyCount:
				Debug.LogWarning((object)"Cannot Set HierarchyCount, it is read only");
				break;
			case Property.LocalEulerAngles:
				value.localEulerAngles = vector3Variable.Value;
				break;
			case Property.LocalPosition:
				value.localPosition = vector3Variable.Value;
				break;
			case Property.LocalScale:
				value.localScale = vector3Variable.Value;
				break;
			case Property.LossyScale:
				Debug.LogWarning((object)"Cannot Set LossyScale, it is read only");
				break;
			case Property.Parent:
				value.parent = transformVariable.Value;
				break;
			case Property.Position:
				value.position = vector3Variable.Value;
				break;
			case Property.Right:
				value.right = vector3Variable.Value;
				break;
			case Property.Root:
				Debug.LogWarning((object)"Cannot Set Root, it is read only");
				break;
			case Property.Up:
				value.up = vector3Variable.Value;
				break;
			}
			break;
		}
		Continue();
	}

	public override string GetSummary()
	{
		if ((Object)(object)transformData.Value == (Object)null)
		{
			return "Error: no transform set";
		}
		BooleanVariable obj = inOutVar as BooleanVariable;
		IntegerVariable integerVariable = inOutVar as IntegerVariable;
		Vector3Variable vector3Variable = inOutVar as Vector3Variable;
		TransformVariable transformVariable = inOutVar as TransformVariable;
		if ((Object)(object)obj == (Object)null && (Object)(object)integerVariable == (Object)null && (Object)(object)vector3Variable == (Object)null && (Object)(object)transformVariable == (Object)null)
		{
			return "Error: no variable set to push or pull data to or from";
		}
		return getOrSet.ToString() + " " + property;
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)235, (byte)191, (byte)217, byte.MaxValue));
	}

	public override bool HasReference(Variable variable)
	{
		if ((Object)(object)transformData.transformRef == (Object)(object)variable || (Object)(object)inOutVar == (Object)(object)variable)
		{
			return true;
		}
		return false;
	}
}
