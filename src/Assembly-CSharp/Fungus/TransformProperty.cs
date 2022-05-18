using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020012A7 RID: 4775
	[CommandInfo("Transform", "Property", "Get or Set a property of a transform component", 0)]
	[AddComponentMenu("")]
	public class TransformProperty : Command
	{
		// Token: 0x060073A3 RID: 29603 RVA: 0x002AB56C File Offset: 0x002A976C
		public override void OnEnter()
		{
			BooleanVariable booleanVariable = this.inOutVar as BooleanVariable;
			IntegerVariable integerVariable = this.inOutVar as IntegerVariable;
			Vector3Variable vector3Variable = this.inOutVar as Vector3Variable;
			TransformVariable transformVariable = this.inOutVar as TransformVariable;
			Transform value = this.transformData.Value;
			TransformProperty.GetSet getSet = this.getOrSet;
			if (getSet != TransformProperty.GetSet.Get)
			{
				if (getSet == TransformProperty.GetSet.Set)
				{
					switch (this.property)
					{
					case TransformProperty.Property.ChildCount:
						Debug.LogWarning("Cannot Set childCount, it is read only");
						break;
					case TransformProperty.Property.EulerAngles:
						value.eulerAngles = vector3Variable.Value;
						break;
					case TransformProperty.Property.Forward:
						value.forward = vector3Variable.Value;
						break;
					case TransformProperty.Property.HasChanged:
						value.hasChanged = booleanVariable.Value;
						break;
					case TransformProperty.Property.HierarchyCapacity:
						value.hierarchyCapacity = integerVariable.Value;
						break;
					case TransformProperty.Property.HierarchyCount:
						Debug.LogWarning("Cannot Set HierarchyCount, it is read only");
						break;
					case TransformProperty.Property.LocalEulerAngles:
						value.localEulerAngles = vector3Variable.Value;
						break;
					case TransformProperty.Property.LocalPosition:
						value.localPosition = vector3Variable.Value;
						break;
					case TransformProperty.Property.LocalScale:
						value.localScale = vector3Variable.Value;
						break;
					case TransformProperty.Property.LossyScale:
						Debug.LogWarning("Cannot Set LossyScale, it is read only");
						break;
					case TransformProperty.Property.Parent:
						value.parent = transformVariable.Value;
						break;
					case TransformProperty.Property.Position:
						value.position = vector3Variable.Value;
						break;
					case TransformProperty.Property.Right:
						value.right = vector3Variable.Value;
						break;
					case TransformProperty.Property.Root:
						Debug.LogWarning("Cannot Set Root, it is read only");
						break;
					case TransformProperty.Property.Up:
						value.up = vector3Variable.Value;
						break;
					}
				}
			}
			else
			{
				switch (this.property)
				{
				case TransformProperty.Property.ChildCount:
					integerVariable.Value = value.childCount;
					break;
				case TransformProperty.Property.EulerAngles:
					vector3Variable.Value = value.eulerAngles;
					break;
				case TransformProperty.Property.Forward:
					vector3Variable.Value = value.forward;
					break;
				case TransformProperty.Property.HasChanged:
					booleanVariable.Value = value.hasChanged;
					break;
				case TransformProperty.Property.HierarchyCapacity:
					integerVariable.Value = value.hierarchyCapacity;
					break;
				case TransformProperty.Property.HierarchyCount:
					integerVariable.Value = value.hierarchyCount;
					break;
				case TransformProperty.Property.LocalEulerAngles:
					vector3Variable.Value = value.localEulerAngles;
					break;
				case TransformProperty.Property.LocalPosition:
					vector3Variable.Value = value.localPosition;
					break;
				case TransformProperty.Property.LocalScale:
					vector3Variable.Value = value.localScale;
					break;
				case TransformProperty.Property.LossyScale:
					vector3Variable.Value = value.lossyScale;
					break;
				case TransformProperty.Property.Parent:
					transformVariable.Value = value.parent;
					break;
				case TransformProperty.Property.Position:
					vector3Variable.Value = value.position;
					break;
				case TransformProperty.Property.Right:
					vector3Variable.Value = value.right;
					break;
				case TransformProperty.Property.Root:
					transformVariable.Value = value.parent;
					break;
				case TransformProperty.Property.Up:
					vector3Variable.Value = value.up;
					break;
				}
			}
			this.Continue();
		}

		// Token: 0x060073A4 RID: 29604 RVA: 0x002AB868 File Offset: 0x002A9A68
		public override string GetSummary()
		{
			if (this.transformData.Value == null)
			{
				return "Error: no transform set";
			}
			Object @object = this.inOutVar as BooleanVariable;
			IntegerVariable integerVariable = this.inOutVar as IntegerVariable;
			Vector3Variable vector3Variable = this.inOutVar as Vector3Variable;
			TransformVariable transformVariable = this.inOutVar as TransformVariable;
			if (@object == null && integerVariable == null && vector3Variable == null && transformVariable == null)
			{
				return "Error: no variable set to push or pull data to or from";
			}
			return this.getOrSet.ToString() + " " + this.property.ToString();
		}

		// Token: 0x060073A5 RID: 29605 RVA: 0x0004C5E0 File Offset: 0x0004A7E0
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x060073A6 RID: 29606 RVA: 0x0004EE9A File Offset: 0x0004D09A
		public override bool HasReference(Variable variable)
		{
			return this.transformData.transformRef == variable || this.inOutVar == variable;
		}

		// Token: 0x0400656F RID: 25967
		public TransformProperty.GetSet getOrSet;

		// Token: 0x04006570 RID: 25968
		[SerializeField]
		protected TransformProperty.Property property = TransformProperty.Property.Position;

		// Token: 0x04006571 RID: 25969
		[SerializeField]
		protected TransformData transformData;

		// Token: 0x04006572 RID: 25970
		[SerializeField]
		[VariableProperty(new Type[]
		{
			typeof(BooleanVariable),
			typeof(IntegerVariable),
			typeof(Vector3Variable),
			typeof(TransformVariable)
		})]
		protected Variable inOutVar;

		// Token: 0x020012A8 RID: 4776
		public enum GetSet
		{
			// Token: 0x04006574 RID: 25972
			Get,
			// Token: 0x04006575 RID: 25973
			Set
		}

		// Token: 0x020012A9 RID: 4777
		public enum Property
		{
			// Token: 0x04006577 RID: 25975
			ChildCount,
			// Token: 0x04006578 RID: 25976
			EulerAngles,
			// Token: 0x04006579 RID: 25977
			Forward,
			// Token: 0x0400657A RID: 25978
			HasChanged,
			// Token: 0x0400657B RID: 25979
			HierarchyCapacity,
			// Token: 0x0400657C RID: 25980
			HierarchyCount,
			// Token: 0x0400657D RID: 25981
			LocalEulerAngles,
			// Token: 0x0400657E RID: 25982
			LocalPosition,
			// Token: 0x0400657F RID: 25983
			LocalScale,
			// Token: 0x04006580 RID: 25984
			LossyScale,
			// Token: 0x04006581 RID: 25985
			Parent,
			// Token: 0x04006582 RID: 25986
			Position,
			// Token: 0x04006583 RID: 25987
			Right,
			// Token: 0x04006584 RID: 25988
			Root,
			// Token: 0x04006585 RID: 25989
			Up
		}
	}
}
