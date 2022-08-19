using System;
using System.Diagnostics;
using System.Reflection;
using UnityEngine;

// Token: 0x02000089 RID: 137
[Serializable]
public class PropertyReference
{
	// Token: 0x170000CE RID: 206
	// (get) Token: 0x0600073F RID: 1855 RVA: 0x0002B114 File Offset: 0x00029314
	// (set) Token: 0x06000740 RID: 1856 RVA: 0x0002B11C File Offset: 0x0002931C
	public Component target
	{
		get
		{
			return this.mTarget;
		}
		set
		{
			this.mTarget = value;
			this.mProperty = null;
			this.mField = null;
		}
	}

	// Token: 0x170000CF RID: 207
	// (get) Token: 0x06000741 RID: 1857 RVA: 0x0002B133 File Offset: 0x00029333
	// (set) Token: 0x06000742 RID: 1858 RVA: 0x0002B13B File Offset: 0x0002933B
	public string name
	{
		get
		{
			return this.mName;
		}
		set
		{
			this.mName = value;
			this.mProperty = null;
			this.mField = null;
		}
	}

	// Token: 0x170000D0 RID: 208
	// (get) Token: 0x06000743 RID: 1859 RVA: 0x0002B152 File Offset: 0x00029352
	public bool isValid
	{
		get
		{
			return this.mTarget != null && !string.IsNullOrEmpty(this.mName);
		}
	}

	// Token: 0x170000D1 RID: 209
	// (get) Token: 0x06000744 RID: 1860 RVA: 0x0002B174 File Offset: 0x00029374
	public bool isEnabled
	{
		get
		{
			if (this.mTarget == null)
			{
				return false;
			}
			MonoBehaviour monoBehaviour = this.mTarget as MonoBehaviour;
			return monoBehaviour == null || monoBehaviour.enabled;
		}
	}

	// Token: 0x06000745 RID: 1861 RVA: 0x000027FC File Offset: 0x000009FC
	public PropertyReference()
	{
	}

	// Token: 0x06000746 RID: 1862 RVA: 0x0002B1AE File Offset: 0x000293AE
	public PropertyReference(Component target, string fieldName)
	{
		this.mTarget = target;
		this.mName = fieldName;
	}

	// Token: 0x06000747 RID: 1863 RVA: 0x0002B1C4 File Offset: 0x000293C4
	public Type GetPropertyType()
	{
		if (this.mProperty == null && this.mField == null && this.isValid)
		{
			this.Cache();
		}
		if (this.mProperty != null)
		{
			return this.mProperty.PropertyType;
		}
		if (this.mField != null)
		{
			return this.mField.FieldType;
		}
		return typeof(void);
	}

	// Token: 0x06000748 RID: 1864 RVA: 0x0002B23C File Offset: 0x0002943C
	public override bool Equals(object obj)
	{
		if (obj == null)
		{
			return !this.isValid;
		}
		if (obj is PropertyReference)
		{
			PropertyReference propertyReference = obj as PropertyReference;
			return this.mTarget == propertyReference.mTarget && string.Equals(this.mName, propertyReference.mName);
		}
		return false;
	}

	// Token: 0x06000749 RID: 1865 RVA: 0x0002B28D File Offset: 0x0002948D
	public override int GetHashCode()
	{
		return PropertyReference.s_Hash;
	}

	// Token: 0x0600074A RID: 1866 RVA: 0x0002B294 File Offset: 0x00029494
	public void Set(Component target, string methodName)
	{
		this.mTarget = target;
		this.mName = methodName;
	}

	// Token: 0x0600074B RID: 1867 RVA: 0x0002B2A4 File Offset: 0x000294A4
	public void Clear()
	{
		this.mTarget = null;
		this.mName = null;
	}

	// Token: 0x0600074C RID: 1868 RVA: 0x0002B2B4 File Offset: 0x000294B4
	public void Reset()
	{
		this.mField = null;
		this.mProperty = null;
	}

	// Token: 0x0600074D RID: 1869 RVA: 0x0002B2C4 File Offset: 0x000294C4
	public override string ToString()
	{
		return PropertyReference.ToString(this.mTarget, this.name);
	}

	// Token: 0x0600074E RID: 1870 RVA: 0x0002B2D8 File Offset: 0x000294D8
	public static string ToString(Component comp, string property)
	{
		if (!(comp != null))
		{
			return null;
		}
		string text = comp.GetType().ToString();
		int num = text.LastIndexOf('.');
		if (num > 0)
		{
			text = text.Substring(num + 1);
		}
		if (!string.IsNullOrEmpty(property))
		{
			return text + "." + property;
		}
		return text + ".[property]";
	}

	// Token: 0x0600074F RID: 1871 RVA: 0x0002B334 File Offset: 0x00029534
	[DebuggerHidden]
	[DebuggerStepThrough]
	public object Get()
	{
		if (this.mProperty == null && this.mField == null && this.isValid)
		{
			this.Cache();
		}
		if (this.mProperty != null)
		{
			if (this.mProperty.CanRead)
			{
				return this.mProperty.GetValue(this.mTarget, null);
			}
		}
		else if (this.mField != null)
		{
			return this.mField.GetValue(this.mTarget);
		}
		return null;
	}

	// Token: 0x06000750 RID: 1872 RVA: 0x0002B3BC File Offset: 0x000295BC
	[DebuggerHidden]
	[DebuggerStepThrough]
	public bool Set(object value)
	{
		if (this.mProperty == null && this.mField == null && this.isValid)
		{
			this.Cache();
		}
		if (this.mProperty == null && this.mField == null)
		{
			return false;
		}
		if (value == null)
		{
			try
			{
				if (this.mProperty != null)
				{
					this.mProperty.SetValue(this.mTarget, null, null);
				}
				else
				{
					this.mField.SetValue(this.mTarget, null);
				}
			}
			catch (Exception)
			{
				return false;
			}
		}
		if (!this.Convert(ref value))
		{
			if (Application.isPlaying)
			{
				Debug.LogError(string.Concat(new object[]
				{
					"Unable to convert ",
					value.GetType(),
					" to ",
					this.GetPropertyType()
				}));
			}
		}
		else
		{
			if (this.mField != null)
			{
				this.mField.SetValue(this.mTarget, value);
				return true;
			}
			if (this.mProperty.CanWrite)
			{
				this.mProperty.SetValue(this.mTarget, value, null);
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000751 RID: 1873 RVA: 0x0002B4F0 File Offset: 0x000296F0
	[DebuggerHidden]
	[DebuggerStepThrough]
	private bool Cache()
	{
		if (this.mTarget != null && !string.IsNullOrEmpty(this.mName))
		{
			Type type = this.mTarget.GetType();
			this.mField = type.GetField(this.mName);
			this.mProperty = type.GetProperty(this.mName);
		}
		else
		{
			this.mField = null;
			this.mProperty = null;
		}
		return this.mField != null || this.mProperty != null;
	}

	// Token: 0x06000752 RID: 1874 RVA: 0x0002B574 File Offset: 0x00029774
	private bool Convert(ref object value)
	{
		if (this.mTarget == null)
		{
			return false;
		}
		Type propertyType = this.GetPropertyType();
		Type from;
		if (value == null)
		{
			if (!propertyType.IsClass)
			{
				return false;
			}
			from = propertyType;
		}
		else
		{
			from = value.GetType();
		}
		return PropertyReference.Convert(ref value, from, propertyType);
	}

	// Token: 0x06000753 RID: 1875 RVA: 0x0002B5BC File Offset: 0x000297BC
	public static bool Convert(Type from, Type to)
	{
		object obj = null;
		return PropertyReference.Convert(ref obj, from, to);
	}

	// Token: 0x06000754 RID: 1876 RVA: 0x0002B5D4 File Offset: 0x000297D4
	public static bool Convert(object value, Type to)
	{
		if (value == null)
		{
			value = null;
			return PropertyReference.Convert(ref value, to, to);
		}
		return PropertyReference.Convert(ref value, value.GetType(), to);
	}

	// Token: 0x06000755 RID: 1877 RVA: 0x0002B5F4 File Offset: 0x000297F4
	public static bool Convert(ref object value, Type from, Type to)
	{
		if (to.IsAssignableFrom(from))
		{
			return true;
		}
		if (to == typeof(string))
		{
			value = ((value != null) ? value.ToString() : "null");
			return true;
		}
		if (value == null)
		{
			return false;
		}
		float num2;
		if (to == typeof(int))
		{
			if (from == typeof(string))
			{
				int num;
				if (int.TryParse((string)value, out num))
				{
					value = num;
					return true;
				}
			}
			else if (from == typeof(float))
			{
				value = Mathf.RoundToInt((float)value);
				return true;
			}
		}
		else if (to == typeof(float) && from == typeof(string) && float.TryParse((string)value, out num2))
		{
			value = num2;
			return true;
		}
		return false;
	}

	// Token: 0x04000485 RID: 1157
	[SerializeField]
	private Component mTarget;

	// Token: 0x04000486 RID: 1158
	[SerializeField]
	private string mName;

	// Token: 0x04000487 RID: 1159
	private FieldInfo mField;

	// Token: 0x04000488 RID: 1160
	private PropertyInfo mProperty;

	// Token: 0x04000489 RID: 1161
	private static int s_Hash = "PropertyBinding".GetHashCode();
}
