using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public static class JSONTemplates
{
	private static readonly HashSet<object> touched = new HashSet<object>();

	public static JSONObject TOJSON(object obj)
	{
		if (touched.Add(obj))
		{
			JSONObject obj2 = JSONObject.obj;
			FieldInfo[] fields = obj.GetType().GetFields();
			foreach (FieldInfo fieldInfo in fields)
			{
				JSONObject jSONObject = JSONObject.nullJO;
				if (!fieldInfo.GetValue(obj).Equals(null))
				{
					MethodInfo method = typeof(JSONTemplates).GetMethod("From" + fieldInfo.FieldType.Name);
					jSONObject = ((method != null) ? ((JSONObject)method.Invoke(null, new object[1] { fieldInfo.GetValue(obj) })) : ((!(fieldInfo.FieldType == typeof(string))) ? JSONObject.Create(fieldInfo.GetValue(obj).ToString()) : JSONObject.CreateStringObject(fieldInfo.GetValue(obj).ToString())));
				}
				if ((bool)jSONObject)
				{
					if (jSONObject.type != 0)
					{
						obj2.AddField(fieldInfo.Name, jSONObject);
						continue;
					}
					Debug.LogWarning((object)("Null for this non-null object, property " + fieldInfo.Name + " of class " + obj.GetType().Name + ". Object type is " + fieldInfo.FieldType.Name));
				}
			}
			PropertyInfo[] properties = obj.GetType().GetProperties();
			foreach (PropertyInfo propertyInfo in properties)
			{
				JSONObject jSONObject2 = JSONObject.nullJO;
				if (!propertyInfo.GetValue(obj, null).Equals(null))
				{
					MethodInfo method2 = typeof(JSONTemplates).GetMethod("From" + propertyInfo.PropertyType.Name);
					jSONObject2 = ((method2 != null) ? ((JSONObject)method2.Invoke(null, new object[1] { propertyInfo.GetValue(obj, null) })) : ((!(propertyInfo.PropertyType == typeof(string))) ? JSONObject.Create(propertyInfo.GetValue(obj, null).ToString()) : JSONObject.CreateStringObject(propertyInfo.GetValue(obj, null).ToString())));
				}
				if ((bool)jSONObject2)
				{
					if (jSONObject2.type != 0)
					{
						obj2.AddField(propertyInfo.Name, jSONObject2);
						continue;
					}
					Debug.LogWarning((object)("Null for this non-null object, property " + propertyInfo.Name + " of class " + obj.GetType().Name + ". Object type is " + propertyInfo.PropertyType.Name));
				}
			}
			return obj2;
		}
		Debug.LogWarning((object)"trying to save the same data twice");
		return JSONObject.nullJO;
	}

	public static Vector2 ToVector2(JSONObject obj)
	{
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		float num = (obj["x"] ? obj["x"].f : 0f);
		float num2 = (obj["y"] ? obj["y"].f : 0f);
		return new Vector2(num, num2);
	}

	public static JSONObject FromVector2(Vector2 v)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
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

	public static JSONObject FromVector3(Vector3 v)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
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

	public static Vector3 ToVector3(JSONObject obj)
	{
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		float num = (obj["x"] ? obj["x"].f : 0f);
		float num2 = (obj["y"] ? obj["y"].f : 0f);
		float num3 = (obj["z"] ? obj["z"].f : 0f);
		return new Vector3(num, num2, num3);
	}

	public static JSONObject FromVector4(Vector4 v)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
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

	public static Vector4 ToVector4(JSONObject obj)
	{
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		float num = (obj["x"] ? obj["x"].f : 0f);
		float num2 = (obj["y"] ? obj["y"].f : 0f);
		float num3 = (obj["z"] ? obj["z"].f : 0f);
		float num4 = (obj["w"] ? obj["w"].f : 0f);
		return new Vector4(num, num2, num3, num4);
	}

	public static JSONObject FromMatrix4x4(Matrix4x4 m)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
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

	public static Matrix4x4 ToMatrix4x4(JSONObject obj)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		Matrix4x4 result = default(Matrix4x4);
		if ((bool)obj["m00"])
		{
			result.m00 = obj["m00"].f;
		}
		if ((bool)obj["m01"])
		{
			result.m01 = obj["m01"].f;
		}
		if ((bool)obj["m02"])
		{
			result.m02 = obj["m02"].f;
		}
		if ((bool)obj["m03"])
		{
			result.m03 = obj["m03"].f;
		}
		if ((bool)obj["m10"])
		{
			result.m10 = obj["m10"].f;
		}
		if ((bool)obj["m11"])
		{
			result.m11 = obj["m11"].f;
		}
		if ((bool)obj["m12"])
		{
			result.m12 = obj["m12"].f;
		}
		if ((bool)obj["m13"])
		{
			result.m13 = obj["m13"].f;
		}
		if ((bool)obj["m20"])
		{
			result.m20 = obj["m20"].f;
		}
		if ((bool)obj["m21"])
		{
			result.m21 = obj["m21"].f;
		}
		if ((bool)obj["m22"])
		{
			result.m22 = obj["m22"].f;
		}
		if ((bool)obj["m23"])
		{
			result.m23 = obj["m23"].f;
		}
		if ((bool)obj["m30"])
		{
			result.m30 = obj["m30"].f;
		}
		if ((bool)obj["m31"])
		{
			result.m31 = obj["m31"].f;
		}
		if ((bool)obj["m32"])
		{
			result.m32 = obj["m32"].f;
		}
		if ((bool)obj["m33"])
		{
			result.m33 = obj["m33"].f;
		}
		return result;
	}

	public static JSONObject FromQuaternion(Quaternion q)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
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

	public static Quaternion ToQuaternion(JSONObject obj)
	{
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		float num = (obj["x"] ? obj["x"].f : 0f);
		float num2 = (obj["y"] ? obj["y"].f : 0f);
		float num3 = (obj["z"] ? obj["z"].f : 0f);
		float num4 = (obj["w"] ? obj["w"].f : 0f);
		return new Quaternion(num, num2, num3, num4);
	}

	public static JSONObject FromColor(Color c)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
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

	public static Color ToColor(JSONObject obj)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		Color result = default(Color);
		for (int i = 0; i < obj.Count; i++)
		{
			switch (obj.keys[i])
			{
			case "r":
				result.r = obj[i].f;
				break;
			case "g":
				result.g = obj[i].f;
				break;
			case "b":
				result.b = obj[i].f;
				break;
			case "a":
				result.a = obj[i].f;
				break;
			}
		}
		return result;
	}

	public static JSONObject FromLayerMask(LayerMask l)
	{
		JSONObject obj = JSONObject.obj;
		obj.AddField("value", ((LayerMask)(ref l)).value);
		return obj;
	}

	public static LayerMask ToLayerMask(JSONObject obj)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		LayerMask result = default(LayerMask);
		((LayerMask)(ref result)).value = (int)obj["value"].n;
		return result;
	}

	public static JSONObject FromRect(Rect r)
	{
		JSONObject obj = JSONObject.obj;
		if (((Rect)(ref r)).x != 0f)
		{
			obj.AddField("x", ((Rect)(ref r)).x);
		}
		if (((Rect)(ref r)).y != 0f)
		{
			obj.AddField("y", ((Rect)(ref r)).y);
		}
		if (((Rect)(ref r)).height != 0f)
		{
			obj.AddField("height", ((Rect)(ref r)).height);
		}
		if (((Rect)(ref r)).width != 0f)
		{
			obj.AddField("width", ((Rect)(ref r)).width);
		}
		return obj;
	}

	public static Rect ToRect(JSONObject obj)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		Rect result = default(Rect);
		for (int i = 0; i < obj.Count; i++)
		{
			switch (obj.keys[i])
			{
			case "x":
				((Rect)(ref result)).x = obj[i].f;
				break;
			case "y":
				((Rect)(ref result)).y = obj[i].f;
				break;
			case "height":
				((Rect)(ref result)).height = obj[i].f;
				break;
			case "width":
				((Rect)(ref result)).width = obj[i].f;
				break;
			}
		}
		return result;
	}

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

	public static RectOffset ToRectOffset(JSONObject obj)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		RectOffset val = new RectOffset();
		for (int i = 0; i < obj.Count; i++)
		{
			switch (obj.keys[i])
			{
			case "bottom":
				val.bottom = (int)obj[i].n;
				break;
			case "left":
				val.left = (int)obj[i].n;
				break;
			case "right":
				val.right = (int)obj[i].n;
				break;
			case "top":
				val.top = (int)obj[i].n;
				break;
			}
		}
		return val;
	}

	public static AnimationCurve ToAnimationCurve(JSONObject obj)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		AnimationCurve val = new AnimationCurve();
		if (obj.HasField("keys"))
		{
			JSONObject field = obj.GetField("keys");
			for (int i = 0; i < field.list.Count; i++)
			{
				val.AddKey(ToKeyframe(field[i]));
			}
		}
		if (obj.HasField("preWrapMode"))
		{
			val.preWrapMode = (WrapMode)(int)obj.GetField("preWrapMode").n;
		}
		if (obj.HasField("postWrapMode"))
		{
			val.postWrapMode = (WrapMode)(int)obj.GetField("postWrapMode").n;
		}
		return val;
	}

	public static JSONObject FromAnimationCurve(AnimationCurve a)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		JSONObject obj = JSONObject.obj;
		WrapMode val = a.preWrapMode;
		obj.AddField("preWrapMode", ((object)(WrapMode)(ref val)).ToString());
		val = a.postWrapMode;
		obj.AddField("postWrapMode", ((object)(WrapMode)(ref val)).ToString());
		if (a.keys.Length != 0)
		{
			JSONObject jSONObject = JSONObject.Create();
			for (int i = 0; i < a.keys.Length; i++)
			{
				jSONObject.Add(FromKeyframe(a.keys[i]));
			}
			obj.AddField("keys", jSONObject);
		}
		return obj;
	}

	public static Keyframe ToKeyframe(JSONObject obj)
	{
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		Keyframe result = default(Keyframe);
		((Keyframe)(ref result))._002Ector(obj.HasField("time") ? obj.GetField("time").n : 0f, obj.HasField("value") ? obj.GetField("value").n : 0f);
		if (obj.HasField("inTangent"))
		{
			((Keyframe)(ref result)).inTangent = obj.GetField("inTangent").n;
		}
		if (obj.HasField("outTangent"))
		{
			((Keyframe)(ref result)).outTangent = obj.GetField("outTangent").n;
		}
		if (obj.HasField("tangentMode"))
		{
			((Keyframe)(ref result)).tangentMode = (int)obj.GetField("tangentMode").n;
		}
		return result;
	}

	public static JSONObject FromKeyframe(Keyframe k)
	{
		JSONObject obj = JSONObject.obj;
		if (((Keyframe)(ref k)).inTangent != 0f)
		{
			obj.AddField("inTangent", ((Keyframe)(ref k)).inTangent);
		}
		if (((Keyframe)(ref k)).outTangent != 0f)
		{
			obj.AddField("outTangent", ((Keyframe)(ref k)).outTangent);
		}
		if (((Keyframe)(ref k)).tangentMode != 0)
		{
			obj.AddField("tangentMode", ((Keyframe)(ref k)).tangentMode);
		}
		if (((Keyframe)(ref k)).time != 0f)
		{
			obj.AddField("time", ((Keyframe)(ref k)).time);
		}
		if (((Keyframe)(ref k)).value != 0f)
		{
			obj.AddField("value", ((Keyframe)(ref k)).value);
		}
		return obj;
	}
}
