using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YSGame.TuJian;

namespace WXB;

public static class Tools
{
	public const int s_dyn_default_speed = 320;

	public const int s_offset_default_speed = 320;

	private static readonly VertexHelper s_VertexHelper = new VertexHelper();

	private static Font DefaultFont;

	public static Func<string, Sprite> s_get_sprite = null;

	public static Func<string, Font> s_get_font = null;

	public static Func<string, Cartoon> s_get_cartoon = null;

	public static Action<List<Cartoon>> s_get_cartoons = null;

	public static VertexHelper vertexHelper => s_VertexHelper;

	public static void LB2LT(ref Vector2 pos, float height)
	{
		pos.y = height - pos.y;
	}

	public static void LT2LB(ref Vector2 pos, float height)
	{
		pos.y += height;
	}

	public static Font GetFont(string name)
	{
		if (s_get_font == null && (Object)(object)SymbolTextInit.GetFont(name) == (Object)null)
		{
			return DefaultFont;
		}
		return s_get_font(name);
	}

	public static Font GetDefaultFont()
	{
		if ((Object)(object)DefaultFont == (Object)null)
		{
			DefaultFont = Object.FindObjectOfType<Font>();
		}
		return DefaultFont;
	}

	public static Sprite GetSprite(string name)
	{
		if (s_get_sprite == null)
		{
			return TuJianDB.GetRichTextSprite(name);
		}
		return s_get_sprite(name);
	}

	public static Cartoon GetCartoon(string name)
	{
		if (s_get_cartoon == null)
		{
			return SymbolTextInit.GetCartoon(name);
		}
		return s_get_cartoon(name);
	}

	public static void GetAllCartoons(List<Cartoon> cartoons)
	{
		if (s_get_cartoons == null)
		{
			SymbolTextInit.GetCartoons(cartoons);
		}
		else
		{
			s_get_cartoons(cartoons);
		}
	}

	public static void AddLine(VertexHelper vh, Vector2 leftPos, Vector2 uv, float width, float height, Color color)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		Vector2 val = default(Vector2);
		((Vector2)(ref val))._002Ector(leftPos.x, leftPos.y);
		int currentVertCount = vh.currentVertCount;
		vh.AddVert(Vector2.op_Implicit(val), Color32.op_Implicit(color), uv);
		vh.AddVert(new Vector3(val.x + width, val.y, 0f), Color32.op_Implicit(color), uv);
		vh.AddVert(new Vector3(val.x + width, val.y - height, 0f), Color32.op_Implicit(color), uv);
		vh.AddVert(new Vector3(val.x, val.y - height, 0f), Color32.op_Implicit(color), uv);
		vh.AddTriangle(currentVertCount, currentVertCount + 1, currentVertCount + 2);
		vh.AddTriangle(currentVertCount + 2, currentVertCount + 3, currentVertCount);
	}

	public static bool IsHexadecimal(char c)
	{
		if ((c >= '0' && c <= '9') || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F'))
		{
			return true;
		}
		return false;
	}

	public static float stringToFloat(string str, float df)
	{
		float result = 0f;
		if (!float.TryParse(str, out result))
		{
			return df;
		}
		return result;
	}

	public static int stringToInt(string str, int df)
	{
		int result = 0;
		if (!int.TryParse(str, out result))
		{
			return df;
		}
		return result;
	}

	public static Font ParserFontName(string text, ref int startpos)
	{
		int length = text.Length;
		string text2 = "";
		while (length > startpos && text2.Length < 10)
		{
			text2 += text[startpos];
			startpos++;
		}
		Font result = null;
		while (text2.Length != 0 && !((Object)(object)(result = GetFont(text2)) != (Object)null))
		{
			text2 = text2.Remove(text2.Length - 1, 1);
			startpos--;
		}
		return result;
	}

	public static bool ScreenPointToWorldPointInRectangle(RectTransform rectTrans, Vector2 screenPoint, Camera cam, out Vector2 worldPoint)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		worldPoint = Vector2.zero;
		Ray val = RectTransformUtility.ScreenPointToRay(cam, screenPoint);
		Plane val2 = new Plane(((Transform)rectTrans).rotation * Vector3.back, ((Transform)rectTrans).position);
		float num = default(float);
		if (!((Plane)(ref val2)).Raycast(val, ref num))
		{
			return false;
		}
		worldPoint = Vector2.op_Implicit(((Ray)(ref val)).GetPoint(num));
		Matrix4x4 worldToLocalMatrix = ((Transform)rectTrans).worldToLocalMatrix;
		worldPoint = Vector2.op_Implicit(((Matrix4x4)(ref worldToLocalMatrix)).MultiplyPoint(Vector2.op_Implicit(worldPoint)));
		Rect rect = rectTrans.rect;
		LB2LT(ref worldPoint, ((Rect)(ref rect)).height);
		return true;
	}

	public static Color ParseColor(string text, int offset, Color defc)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		if (ParseColor(text, offset, out var color))
		{
			return color;
		}
		return defc;
	}

	public static bool ParseColor(string text, int offset, out Color color)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		color = Color.white;
		int num = text.Length - offset;
		if (num >= 8)
		{
			color = ParseColor32(text, offset);
			return true;
		}
		if (num >= 6)
		{
			color = ParseColor24(text, offset);
			return true;
		}
		return false;
	}

	public static Color ParseColor24(string text, int offset)
	{
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		if (text[offset] == '#')
		{
			offset++;
		}
		int num = (HexToDecimal(text[offset]) << 4) | HexToDecimal(text[offset + 1]);
		int num2 = (HexToDecimal(text[offset + 2]) << 4) | HexToDecimal(text[offset + 3]);
		int num3 = (HexToDecimal(text[offset + 4]) << 4) | HexToDecimal(text[offset + 5]);
		float num4 = 0.003921569f;
		return new Color(num4 * (float)num, num4 * (float)num2, num4 * (float)num3);
	}

	public static Color ParseColor32(string text, int offset)
	{
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		if (text[offset] == '#')
		{
			offset++;
		}
		int num = (HexToDecimal(text[offset]) << 4) | HexToDecimal(text[offset + 1]);
		int num2 = (HexToDecimal(text[offset + 2]) << 4) | HexToDecimal(text[offset + 3]);
		int num3 = (HexToDecimal(text[offset + 4]) << 4) | HexToDecimal(text[offset + 5]);
		int num4 = (HexToDecimal(text[offset + 6]) << 4) | HexToDecimal(text[offset + 7]);
		float num5 = 0.003921569f;
		return new Color(num5 * (float)num, num5 * (float)num2, num5 * (float)num3, num5 * (float)num4);
	}

	public static int HexToDecimal(char ch)
	{
		switch (ch)
		{
		case '0':
			return 0;
		case '1':
			return 1;
		case '2':
			return 2;
		case '3':
			return 3;
		case '4':
			return 4;
		case '5':
			return 5;
		case '6':
			return 6;
		case '7':
			return 7;
		case '8':
			return 8;
		case '9':
			return 9;
		case 'A':
		case 'a':
			return 10;
		case 'B':
		case 'b':
			return 11;
		case 'C':
		case 'c':
			return 12;
		case 'D':
		case 'd':
			return 13;
		case 'E':
		case 'e':
			return 14;
		case 'F':
		case 'f':
			return 15;
		default:
			return 15;
		}
	}

	public static Color ParserColorName(string text, Color dc)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		int startpos = 0;
		return ParserColorName(text, ref startpos, dc);
	}

	public static Color ParserColorName(string text, ref int startpos, Color dc)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		if (startpos >= text.Length - 1)
		{
			return dc;
		}
		if (text[startpos + 1] == '[')
		{
			int num = text.IndexOf(']', startpos + 1);
			if (num != -1)
			{
				string name = text.Substring(startpos + 2, num - startpos - 2);
				startpos = num + 1;
				return ColorConst.Get(name, dc);
			}
			startpos++;
			return dc;
		}
		int startIndex = ++startpos;
		int i = 0;
		for (int length = text.Length; length > startpos + i && IsHexadecimal(text[startpos + i]) && i < 8; i++)
		{
		}
		if (ParseColor(text.Substring(startIndex, i), 0, out var color))
		{
			startpos += i;
			return color;
		}
		startpos += i;
		return dc;
	}

	public static T AddChild<T>(this GameObject go) where T : MonoBehaviour
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		GameObject val = new GameObject();
		go.AddChild(val);
		return val.AddComponent<T>();
	}

	public static void AddChild(this GameObject go, GameObject child)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		Transform transform = child.transform;
		transform.SetParent(go.transform);
		transform.localScale = Vector3.one;
		transform.localPosition = Vector3.zero;
		transform.localEulerAngles = Vector3.zero;
		child.layer = go.layer;
	}

	public static void Destroy(Object obj)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		if (!Object.op_Implicit(obj))
		{
			return;
		}
		if (obj is Transform)
		{
			GameObject gameObject = ((Component)(Transform)obj).gameObject;
			if (Application.isPlaying)
			{
				Object.Destroy((Object)(object)gameObject);
			}
			else
			{
				Object.DestroyImmediate((Object)(object)gameObject);
			}
		}
		else if (obj is GameObject)
		{
			GameObject val = (GameObject)(object)((obj is GameObject) ? obj : null);
			if (Application.isPlaying)
			{
				Object.Destroy((Object)(object)val);
			}
			else
			{
				Object.DestroyImmediate((Object)(object)val);
			}
		}
		else if (Application.isPlaying)
		{
			Object.Destroy(obj);
		}
		else
		{
			Object.DestroyImmediate(obj);
		}
	}

	public static void UpdateRect(RectTransform child, Vector2 offset)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		Transform parent = ((Transform)child).parent;
		RectTransform val = (RectTransform)(object)((parent is RectTransform) ? parent : null);
		if (!((Object)(object)val == (Object)null))
		{
			child.pivot = val.pivot;
			child.anchorMin = Vector2.zero;
			child.anchorMax = Vector2.one;
			child.offsetMax = Vector2.zero;
			child.offsetMin = Vector2.zero;
			((Transform)child).localPosition = Vector2.op_Implicit(offset);
			((Transform)child).localScale = Vector3.one;
			((Transform)child).localEulerAngles = Vector3.zero;
		}
	}
}
