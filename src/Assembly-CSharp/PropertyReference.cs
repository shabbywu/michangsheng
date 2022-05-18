using System;
using System.Diagnostics;
using System.Reflection;
using UnityEngine;

// Token: 0x020000C6 RID: 198
[Serializable]
public class PropertyReference
{
	// Token: 0x170000E2 RID: 226
	// (get) Token: 0x060007C2 RID: 1986 RVA: 0x0000A7EA File Offset: 0x000089EA
	// (set) Token: 0x060007C3 RID: 1987 RVA: 0x0000A7F2 File Offset: 0x000089F2
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

	// Token: 0x170000E3 RID: 227
	// (get) Token: 0x060007C4 RID: 1988 RVA: 0x0000A809 File Offset: 0x00008A09
	// (set) Token: 0x060007C5 RID: 1989 RVA: 0x0000A811 File Offset: 0x00008A11
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

	// Token: 0x170000E4 RID: 228
	// (get) Token: 0x060007C6 RID: 1990 RVA: 0x0000A828 File Offset: 0x00008A28
	public bool isValid
	{
		get
		{
			return this.mTarget != null && !string.IsNullOrEmpty(this.mName);
		}
	}

	// Token: 0x170000E5 RID: 229
	// (get) Token: 0x060007C7 RID: 1991 RVA: 0x000800FC File Offset: 0x0007E2FC
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

	// Token: 0x060007C8 RID: 1992 RVA: 0x0000403D File Offset: 0x0000223D
	public PropertyReference()
	{
	}

	// Token: 0x060007C9 RID: 1993 RVA: 0x0000A848 File Offset: 0x00008A48
	public PropertyReference(Component target, string fieldName)
	{
		this.mTarget = target;
		this.mName = fieldName;
	}

	// Token: 0x060007CA RID: 1994 RVA: 0x00080138 File Offset: 0x0007E338
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

	// Token: 0x060007CB RID: 1995 RVA: 0x000801B0 File Offset: 0x0007E3B0
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

	// Token: 0x060007CC RID: 1996 RVA: 0x0000A85E File Offset: 0x00008A5E
	public override int GetHashCode()
	{
		return PropertyReference.s_Hash;
	}

	// Token: 0x060007CD RID: 1997 RVA: 0x0000A865 File Offset: 0x00008A65
	public void Set(Component target, string methodName)
	{
		this.mTarget = target;
		this.mName = methodName;
	}

	// Token: 0x060007CE RID: 1998 RVA: 0x0000A875 File Offset: 0x00008A75
	public void Clear()
	{
		this.mTarget = null;
		this.mName = null;
	}

	// Token: 0x060007CF RID: 1999 RVA: 0x0000A885 File Offset: 0x00008A85
	public void Reset()
	{
		this.mField = null;
		this.mProperty = null;
	}

	// Token: 0x060007D0 RID: 2000 RVA: 0x0000A895 File Offset: 0x00008A95
	public override string ToString()
	{
		return PropertyReference.ToString(this.mTarget, this.name);
	}

	// Token: 0x060007D1 RID: 2001 RVA: 0x00080204 File Offset: 0x0007E404
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

	// Token: 0x060007D2 RID: 2002 RVA: 0x00080260 File Offset: 0x0007E460
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

	// Token: 0x060007D3 RID: 2003 RVA: 0x000802E8 File Offset: 0x0007E4E8
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

	// Token: 0x060007D4 RID: 2004 RVA: 0x0008041C File Offset: 0x0007E61C
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

	// Token: 0x060007D5 RID: 2005 RVA: 0x000804A0 File Offset: 0x0007E6A0
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

	// Token: 0x060007D6 RID: 2006 RVA: 0x000804E8 File Offset: 0x0007E6E8
	public static bool Convert(Type from, Type to)
	{
		object obj = null;
		return PropertyReference.Convert(ref obj, from, to);
	}

	// Token: 0x060007D7 RID: 2007 RVA: 0x0000A8A8 File Offset: 0x00008AA8
	public static bool Convert(object value, Type to)
	{
		if (value == null)
		{
			value = null;
			return PropertyReference.Convert(ref value, to, to);
		}
		return PropertyReference.Convert(ref value, value.GetType(), to);
	}

	// Token: 0x060007D8 RID: 2008 RVA: 0x00080500 File Offset: 0x0007E700
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

	// Token: 0x04000579 RID: 1401
	[SerializeField]
	private Component mTarget;

	// Token: 0x0400057A RID: 1402
	[SerializeField]
	private string mName;

	// Token: 0x0400057B RID: 1403
	private FieldInfo mField;

	// Token: 0x0400057C RID: 1404
	private PropertyInfo mProperty;

	// Token: 0x0400057D RID: 1405
	private static int s_Hash = "PropertyBinding".GetHashCode();
}
