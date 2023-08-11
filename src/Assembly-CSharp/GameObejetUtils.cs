using System;
using UnityEngine;
using UnityEngine.UI;

public static class GameObejetUtils
{
	public static GameObject Inst(this GameObject obj, Transform parent = null)
	{
		if ((Object)(object)parent != (Object)null)
		{
			return Object.Instantiate<GameObject>(obj, parent);
		}
		return Object.Instantiate<GameObject>(obj);
	}

	public static bool IsEqualTo(this float a, float b, float margin)
	{
		return Math.Abs(a - b) < margin;
	}

	public static bool IsEqualTo(this float a, double b)
	{
		return Math.Abs((double)a - b) < 1.401298464324817E-45;
	}

	public static Transform SetPostionX(this Transform transform, float x)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		transform.position = new Vector3(x, transform.position.y, transform.position.z);
		return transform;
	}

	public static Transform SetPostionY(this Transform transform, float y)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		transform.position = new Vector3(transform.position.x, y, transform.position.z);
		return transform;
	}

	public static Transform SetPostionZ(this Transform transform, float z)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		transform.position = new Vector3(transform.position.x, transform.position.y, z);
		return transform;
	}

	public static Transform SetLocalPositionX(this Transform transform, float x)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		transform.localPosition = new Vector3(x, transform.localPosition.y, transform.localPosition.z);
		return transform;
	}

	public static Transform SetLocalPositionY(this Transform transform, float y)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
		return transform;
	}

	public static Transform SetLocalPositionZ(this Transform transform, float z)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, z);
		return transform;
	}

	public static void SetText(this Text text, object content)
	{
		if (content is string)
		{
			text.text = ((string)content).ToCN();
		}
		else
		{
			text.text = content.ToString().ToCN();
		}
	}

	public static void SetText(this Text text, object content, string color)
	{
		if (!color.Contains("#"))
		{
			color = "#" + color;
		}
		if (content is string)
		{
			text.text = "<color=" + color + ">" + ((string)content).ToCN() + "</color>";
		}
		else
		{
			text.text = "<color=" + color + ">" + content.ToString().ToCN() + "</color>";
		}
	}

	public static void AddText(this Text text, object content, string color)
	{
		if (!color.Contains("#"))
		{
			color = "#" + color;
		}
		if (content is string)
		{
			Text val = text;
			val.text = val.text + "<color=" + color + ">" + ((string)content).ToCN() + "</color>";
		}
		else
		{
			Text val = text;
			val.text = val.text + "<color=" + color + ">" + content.ToString().ToCN() + "</color>";
		}
	}

	public static void AddText(this Text text, object content)
	{
		if (content is string)
		{
			text.text += Tools.Code64((string)content);
		}
		else
		{
			text.text += content.ToString().ToCN();
		}
	}

	public static void Hide(this Image img)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		((Graphic)img).color = new Color(((Graphic)img).color.r, ((Graphic)img).color.g, ((Graphic)img).color.b, 0f);
	}

	public static void Show(this Image img)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		((Graphic)img).color = new Color(((Graphic)img).color.r, ((Graphic)img).color.g, ((Graphic)img).color.b, 1f);
	}
}
