using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// Token: 0x0200003E RID: 62
public static class JSONTemplates
{
	// Token: 0x06000410 RID: 1040 RVA: 0x0006C0CC File Offset: 0x0006A2CC
	public static JSONObject TOJSON(object obj)
	{
		if (JSONTemplates.touched.Add(obj))
		{
			JSONObject obj2 = JSONObject.obj;
			foreach (FieldInfo fieldInfo in obj.GetType().GetFields())
			{
				JSONObject jsonobject = JSONObject.nullJO;
				if (!fieldInfo.GetValue(obj).Equals(null))
				{
					MethodInfo method = typeof(JSONTemplates).GetMethod("From" + fieldInfo.FieldType.Name);
					if (method != null)
					{
						jsonobject = (JSONObject)method.Invoke(null, new object[]
						{
							fieldInfo.GetValue(obj)
						});
					}
					else if (fieldInfo.FieldType == typeof(string))
					{
						jsonobject = JSONObject.CreateStringObject(fieldInfo.GetValue(obj).ToString());
					}
					else
					{
						jsonobject = JSONObject.Create(fieldInfo.GetValue(obj).ToString(), -2, false, false);
					}
				}
				if (jsonobject)
				{
					if (jsonobject.type != JSONObject.Type.NULL)
					{
						obj2.AddField(fieldInfo.Name, jsonobject);
					}
					else
					{
						Debug.LogWarning(string.Concat(new string[]
						{
							"Null for this non-null object, property ",
							fieldInfo.Name,
							" of class ",
							obj.GetType().Name,
							". Object type is ",
							fieldInfo.FieldType.Name
						}));
					}
				}
			}
			foreach (PropertyInfo propertyInfo in obj.GetType().GetProperties())
			{
				JSONObject jsonobject2 = JSONObject.nullJO;
				if (!propertyInfo.GetValue(obj, null).Equals(null))
				{
					MethodInfo method2 = typeof(JSONTemplates).GetMethod("From" + propertyInfo.PropertyType.Name);
					if (method2 != null)
					{
						jsonobject2 = (JSONObject)method2.Invoke(null, new object[]
						{
							propertyInfo.GetValue(obj, null)
						});
					}
					else if (propertyInfo.PropertyType == typeof(string))
					{
						jsonobject2 = JSONObject.CreateStringObject(propertyInfo.GetValue(obj, null).ToString());
					}
					else
					{
						jsonobject2 = JSONObject.Create(propertyInfo.GetValue(obj, null).ToString(), -2, false, false);
					}
				}
				if (jsonobject2)
				{
					if (jsonobject2.type != JSONObject.Type.NULL)
					{
						obj2.AddField(propertyInfo.Name, jsonobject2);
					}
					else
					{
						Debug.LogWarning(string.Concat(new string[]
						{
							"Null for this non-null object, property ",
							propertyInfo.Name,
							" of class ",
							obj.GetType().Name,
							". Object type is ",
							propertyInfo.PropertyType.Name
						}));
					}
				}
			}
			return obj2;
		}
		Debug.LogWarning("trying to save the same data twice");
		return JSONObject.nullJO;
	}

	// Token: 0x06000411 RID: 1041 RVA: 0x0006C39C File Offset: 0x0006A59C
	public static Vector2 ToVector2(JSONObject obj)
	{
		float num = obj["x"] ? obj["x"].f : 0f;
		float num2 = obj["y"] ? obj["y"].f : 0f;
		return new Vector2(num, num2);
	}

	// Token: 0x06000412 RID: 1042 RVA: 0x0006C404 File Offset: 0x0006A604
	public static JSONObject FromVector2(Vector2 v)
	{
		JSONObject obj = JSONObject.obj;
		if (v.x != 0f)
		{
			obj.AddField("x", v.x);
		}
		if (v.y != 0f)
		{
			obj.AddField("y", v.y);
		}
		return obj;
	}

	// Token: 0x06000413 RID: 1043 RVA: 0x0006C454 File Offset: 0x0006A654
	public static JSONObject FromVector3(Vector3 v)
	{
		JSONObject obj = JSONObject.obj;
		if (v.x != 0f)
		{
			obj.AddField("x", v.x);
		}
		if (v.y != 0f)
		{
			obj.AddField("y", v.y);
		}
		if (v.z != 0f)
		{
			obj.AddField("z", v.z);
		}
		return obj;
	}

	// Token: 0x06000414 RID: 1044 RVA: 0x0006C4C4 File Offset: 0x0006A6C4
	public static Vector3 ToVector3(JSONObject obj)
	{
		float num = obj["x"] ? obj["x"].f : 0f;
		float num2 = obj["y"] ? obj["y"].f : 0f;
		float num3 = obj["z"] ? obj["z"].f : 0f;
		return new Vector3(num, num2, num3);
	}

	// Token: 0x06000415 RID: 1045 RVA: 0x0006C558 File Offset: 0x0006A758
	public static JSONObject FromVector4(Vector4 v)
	{
		JSONObject obj = JSONObject.obj;
		if (v.x != 0f)
		{
			obj.AddField("x", v.x);
		}
		if (v.y != 0f)
		{
			obj.AddField("y", v.y);
		}
		if (v.z != 0f)
		{
			obj.AddField("z", v.z);
		}
		if (v.w != 0f)
		{
			obj.AddField("w", v.w);
		}
		return obj;
	}

	// Token: 0x06000416 RID: 1046 RVA: 0x0006C5E4 File Offset: 0x0006A7E4
	public static Vector4 ToVector4(JSONObject obj)
	{
		float num = obj["x"] ? obj["x"].f : 0f;
		float num2 = obj["y"] ? obj["y"].f : 0f;
		float num3 = obj["z"] ? obj["z"].f : 0f;
		float num4 = obj["w"] ? obj["w"].f : 0f;
		return new Vector4(num, num2, num3, num4);
	}

	// Token: 0x06000417 RID: 1047 RVA: 0x0006C6A0 File Offset: 0x0006A8A0
	public static JSONObject FromMatrix4x4(Matrix4x4 m)
	{
		JSONObject obj = JSONObject.obj;
		if (m.m00 != 0f)
		{
			obj.AddField("m00", m.m00);
		}
		if (m.m01 != 0f)
		{
			obj.AddField("m01", m.m01);
		}
		if (m.m02 != 0f)
		{
			obj.AddField("m02", m.m02);
		}
		if (m.m03 != 0f)
		{
			obj.AddField("m03", m.m03);
		}
		if (m.m10 != 0f)
		{
			obj.AddField("m10", m.m10);
		}
		if (m.m11 != 0f)
		{
			obj.AddField("m11", m.m11);
		}
		if (m.m12 != 0f)
		{
			obj.AddField("m12", m.m12);
		}
		if (m.m13 != 0f)
		{
			obj.AddField("m13", m.m13);
		}
		if (m.m20 != 0f)
		{
			obj.AddField("m20", m.m20);
		}
		if (m.m21 != 0f)
		{
			obj.AddField("m21", m.m21);
		}
		if (m.m22 != 0f)
		{
			obj.AddField("m22", m.m22);
		}
		if (m.m23 != 0f)
		{
			obj.AddField("m23", m.m23);
		}
		if (m.m30 != 0f)
		{
			obj.AddField("m30", m.m30);
		}
		if (m.m31 != 0f)
		{
			obj.AddField("m31", m.m31);
		}
		if (m.m32 != 0f)
		{
			obj.AddField("m32", m.m32);
		}
		if (m.m33 != 0f)
		{
			obj.AddField("m33", m.m33);
		}
		return obj;
	}

	// Token: 0x06000418 RID: 1048 RVA: 0x0006C894 File Offset: 0x0006AA94
	public static Matrix4x4 ToMatrix4x4(JSONObject obj)
	{
		Matrix4x4 result = default(Matrix4x4);
		if (obj["m00"])
		{
			result.m00 = obj["m00"].f;
		}
		if (obj["m01"])
		{
			result.m01 = obj["m01"].f;
		}
		if (obj["m02"])
		{
			result.m02 = obj["m02"].f;
		}
		if (obj["m03"])
		{
			result.m03 = obj["m03"].f;
		}
		if (obj["m10"])
		{
			result.m10 = obj["m10"].f;
		}
		if (obj["m11"])
		{
			result.m11 = obj["m11"].f;
		}
		if (obj["m12"])
		{
			result.m12 = obj["m12"].f;
		}
		if (obj["m13"])
		{
			result.m13 = obj["m13"].f;
		}
		if (obj["m20"])
		{
			result.m20 = obj["m20"].f;
		}
		if (obj["m21"])
		{
			result.m21 = obj["m21"].f;
		}
		if (obj["m22"])
		{
			result.m22 = obj["m22"].f;
		}
		if (obj["m23"])
		{
			result.m23 = obj["m23"].f;
		}
		if (obj["m30"])
		{
			result.m30 = obj["m30"].f;
		}
		if (obj["m31"])
		{
			result.m31 = obj["m31"].f;
		}
		if (obj["m32"])
		{
			result.m32 = obj["m32"].f;
		}
		if (obj["m33"])
		{
			result.m33 = obj["m33"].f;
		}
		return result;
	}

	// Token: 0x06000419 RID: 1049 RVA: 0x0006CB3C File Offset: 0x0006AD3C
	public static JSONObject FromQuaternion(Quaternion q)
	{
		JSONObject obj = JSONObject.obj;
		if (q.w != 0f)
		{
			obj.AddField("w", q.w);
		}
		if (q.x != 0f)
		{
			obj.AddField("x", q.x);
		}
		if (q.y != 0f)
		{
			obj.AddField("y", q.y);
		}
		if (q.z != 0f)
		{
			obj.AddField("z", q.z);
		}
		return obj;
	}

	// Token: 0x0600041A RID: 1050 RVA: 0x0006CBC8 File Offset: 0x0006ADC8
	public static Quaternion ToQuaternion(JSONObject obj)
	{
		float num = obj["x"] ? obj["x"].f : 0f;
		float num2 = obj["y"] ? obj["y"].f : 0f;
		float num3 = obj["z"] ? obj["z"].f : 0f;
		float num4 = obj["w"] ? obj["w"].f : 0f;
		return new Quaternion(num, num2, num3, num4);
	}

	// Token: 0x0600041B RID: 1051 RVA: 0x0006CC84 File Offset: 0x0006AE84
	public static JSONObject FromColor(Color c)
	{
		JSONObject obj = JSONObject.obj;
		if (c.r != 0f)
		{
			obj.AddField("r", c.r);
		}
		if (c.g != 0f)
		{
			obj.AddField("g", c.g);
		}
		if (c.b != 0f)
		{
			obj.AddField("b", c.b);
		}
		if (c.a != 0f)
		{
			obj.AddField("a", c.a);
		}
		return obj;
	}

	// Token: 0x0600041C RID: 1052 RVA: 0x0006CD10 File Offset: 0x0006AF10
	public static Color ToColor(JSONObject obj)
	{
		Color result = default(Color);
		for (int i = 0; i < obj.Count; i++)
		{
			string a = obj.keys[i];
			if (!(a == "r"))
			{
				if (!(a == "g"))
				{
					if (!(a == "b"))
					{
						if (a == "a")
						{
							result.a = obj[i].f;
						}
					}
					else
					{
						result.b = obj[i].f;
					}
				}
				else
				{
					result.g = obj[i].f;
				}
			}
			else
			{
				result.r = obj[i].f;
			}
		}
		return result;
	}

	// Token: 0x0600041D RID: 1053 RVA: 0x00007A92 File Offset: 0x00005C92
	public static JSONObject FromLayerMask(LayerMask l)
	{
		JSONObject obj = JSONObject.obj;
		obj.AddField("value", l.value);
		return obj;
	}

	// Token: 0x0600041E RID: 1054 RVA: 0x0006CDD4 File Offset: 0x0006AFD4
	public static LayerMask ToLayerMask(JSONObject obj)
	{
		LayerMask result = default(LayerMask);
		result.value = (int)obj["value"].n;
		return result;
	}

	// Token: 0x0600041F RID: 1055 RVA: 0x0006CE04 File Offset: 0x0006B004
	public static JSONObject FromRect(Rect r)
	{
		JSONObject obj = JSONObject.obj;
		if (r.x != 0f)
		{
			obj.AddField("x", r.x);
		}
		if (r.y != 0f)
		{
			obj.AddField("y", r.y);
		}
		if (r.height != 0f)
		{
			obj.AddField("height", r.height);
		}
		if (r.width != 0f)
		{
			obj.AddField("width", r.width);
		}
		return obj;
	}

	// Token: 0x06000420 RID: 1056 RVA: 0x0006CE98 File Offset: 0x0006B098
	public static Rect ToRect(JSONObject obj)
	{
		Rect result = default(Rect);
		for (int i = 0; i < obj.Count; i++)
		{
			string a = obj.keys[i];
			if (!(a == "x"))
			{
				if (!(a == "y"))
				{
					if (!(a == "height"))
					{
						if (a == "width")
						{
							result.width = obj[i].f;
						}
					}
					else
					{
						result.height = obj[i].f;
					}
				}
				else
				{
					result.y = obj[i].f;
				}
			}
			else
			{
				result.x = obj[i].f;
			}
		}
		return result;
	}

	// Token: 0x06000421 RID: 1057 RVA: 0x0006CF5C File Offset: 0x0006B15C
	public static JSONObject FromRectOffset(RectOffset r)
	{
		JSONObject obj = JSONObject.obj;
		if (r.bottom != 0)
		{
			obj.AddField("bottom", r.bottom);
		}
		if (r.left != 0)
		{
			obj.AddField("left", r.left);
		}
		if (r.right != 0)
		{
			obj.AddField("right", r.right);
		}
		if (r.top != 0)
		{
			obj.AddField("top", r.top);
		}
		return obj;
	}

	// Token: 0x06000422 RID: 1058 RVA: 0x0006CFD4 File Offset: 0x0006B1D4
	public static RectOffset ToRectOffset(JSONObject obj)
	{
		RectOffset rectOffset = new RectOffset();
		for (int i = 0; i < obj.Count; i++)
		{
			string a = obj.keys[i];
			if (!(a == "bottom"))
			{
				if (!(a == "left"))
				{
					if (!(a == "right"))
					{
						if (a == "top")
						{
							rectOffset.top = (int)obj[i].n;
						}
					}
					else
					{
						rectOffset.right = (int)obj[i].n;
					}
				}
				else
				{
					rectOffset.left = (int)obj[i].n;
				}
			}
			else
			{
				rectOffset.bottom = (int)obj[i].n;
			}
		}
		return rectOffset;
	}

	// Token: 0x06000423 RID: 1059 RVA: 0x0006D094 File Offset: 0x0006B294
	public static AnimationCurve ToAnimationCurve(JSONObject obj)
	{
		AnimationCurve animationCurve = new AnimationCurve();
		if (obj.HasField("keys"))
		{
			JSONObject field = obj.GetField("keys");
			for (int i = 0; i < field.list.Count; i++)
			{
				animationCurve.AddKey(JSONTemplates.ToKeyframe(field[i]));
			}
		}
		if (obj.HasField("preWrapMode"))
		{
			animationCurve.preWrapMode = (int)obj.GetField("preWrapMode").n;
		}
		if (obj.HasField("postWrapMode"))
		{
			animationCurve.postWrapMode = (int)obj.GetField("postWrapMode").n;
		}
		return animationCurve;
	}

	// Token: 0x06000424 RID: 1060 RVA: 0x0006D134 File Offset: 0x0006B334
	public static JSONObject FromAnimationCurve(AnimationCurve a)
	{
		JSONObject obj = JSONObject.obj;
		obj.AddField("preWrapMode", a.preWrapMode.ToString());
		obj.AddField("postWrapMode", a.postWrapMode.ToString());
		if (a.keys.Length != 0)
		{
			JSONObject jsonobject = JSONObject.Create();
			for (int i = 0; i < a.keys.Length; i++)
			{
				jsonobject.Add(JSONTemplates.FromKeyframe(a.keys[i]));
			}
			obj.AddField("keys", jsonobject);
		}
		return obj;
	}

	// Token: 0x06000425 RID: 1061 RVA: 0x0006D1CC File Offset: 0x0006B3CC
	public static Keyframe ToKeyframe(JSONObject obj)
	{
		Keyframe result;
		result..ctor(obj.HasField("time") ? obj.GetField("time").n : 0f, obj.HasField("value") ? obj.GetField("value").n : 0f);
		if (obj.HasField("inTangent"))
		{
			result.inTangent = obj.GetField("inTangent").n;
		}
		if (obj.HasField("outTangent"))
		{
			result.outTangent = obj.GetField("outTangent").n;
		}
		if (obj.HasField("tangentMode"))
		{
			result.tangentMode = (int)obj.GetField("tangentMode").n;
		}
		return result;
	}

	// Token: 0x06000426 RID: 1062 RVA: 0x0006D298 File Offset: 0x0006B498
	public static JSONObject FromKeyframe(Keyframe k)
	{
		JSONObject obj = JSONObject.obj;
		if (k.inTangent != 0f)
		{
			obj.AddField("inTangent", k.inTangent);
		}
		if (k.outTangent != 0f)
		{
			obj.AddField("outTangent", k.outTangent);
		}
		if (k.tangentMode != 0)
		{
			obj.AddField("tangentMode", k.tangentMode);
		}
		if (k.time != 0f)
		{
			obj.AddField("time", k.time);
		}
		if (k.value != 0f)
		{
			obj.AddField("value", k.value);
		}
		return obj;
	}

	// Token: 0x04000254 RID: 596
	private static readonly HashSet<object> touched = new HashSet<object>();
}
