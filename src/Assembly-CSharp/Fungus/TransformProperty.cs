using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E56 RID: 3670
	[CommandInfo("Transform", "Property", "Get or Set a property of a transform component", 0)]
	[AddComponentMenu("")]
	public class TransformProperty : Command
	{
		// Token: 0x06006715 RID: 26389 RVA: 0x00288900 File Offset: 0x00286B00
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

		// Token: 0x06006716 RID: 26390 RVA: 0x00288BFC File Offset: 0x00286DFC
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

		// Token: 0x06006717 RID: 26391 RVA: 0x0027D3DB File Offset: 0x0027B5DB
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x06006718 RID: 26392 RVA: 0x00288CA6 File Offset: 0x00286EA6
		public override bool HasReference(Variable variable)
		{
			return this.transformData.transformRef == variable || this.inOutVar == variable;
		}

		// Token: 0x0400582B RID: 22571
		public TransformProperty.GetSet getOrSet;

		// Token: 0x0400582C RID: 22572
		[SerializeField]
		protected TransformProperty.Property property = TransformProperty.Property.Position;

		// Token: 0x0400582D RID: 22573
		[SerializeField]
		protected TransformData transformData;

		// Token: 0x0400582E RID: 22574
		[SerializeField]
		[VariableProperty(new Type[]
		{
			typeof(BooleanVariable),
			typeof(IntegerVariable),
			typeof(Vector3Variable),
			typeof(TransformVariable)
		})]
		protected Variable inOutVar;

		// Token: 0x020016C5 RID: 5829
		public enum GetSet
		{
			// Token: 0x040073A0 RID: 29600
			Get,
			// Token: 0x040073A1 RID: 29601
			Set
		}

		// Token: 0x020016C6 RID: 5830
		public enum Property
		{
			// Token: 0x040073A3 RID: 29603
			ChildCount,
			// Token: 0x040073A4 RID: 29604
			EulerAngles,
			// Token: 0x040073A5 RID: 29605
			Forward,
			// Token: 0x040073A6 RID: 29606
			HasChanged,
			// Token: 0x040073A7 RID: 29607
			HierarchyCapacity,
			// Token: 0x040073A8 RID: 29608
			HierarchyCount,
			// Token: 0x040073A9 RID: 29609
			LocalEulerAngles,
			// Token: 0x040073AA RID: 29610
			LocalPosition,
			// Token: 0x040073AB RID: 29611
			LocalScale,
			// Token: 0x040073AC RID: 29612
			LossyScale,
			// Token: 0x040073AD RID: 29613
			Parent,
			// Token: 0x040073AE RID: 29614
			Position,
			// Token: 0x040073AF RID: 29615
			Right,
			// Token: 0x040073B0 RID: 29616
			Root,
			// Token: 0x040073B1 RID: 29617
			Up
		}
	}
}
