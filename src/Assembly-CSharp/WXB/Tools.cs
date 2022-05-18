using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YSGame.TuJian;

namespace WXB
{
	// Token: 0x020009B8 RID: 2488
	public static class Tools
	{
		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x06003F54 RID: 16212 RVA: 0x0002D7DF File Offset: 0x0002B9DF
		public static VertexHelper vertexHelper
		{
			get
			{
				return Tools.s_VertexHelper;
			}
		}

		// Token: 0x06003F55 RID: 16213 RVA: 0x0002D7E6 File Offset: 0x0002B9E6
		public static void LB2LT(ref Vector2 pos, float height)
		{
			pos.y = height - pos.y;
		}

		// Token: 0x06003F56 RID: 16214 RVA: 0x0002D7F6 File Offset: 0x0002B9F6
		public static void LT2LB(ref Vector2 pos, float height)
		{
			pos.y += height;
		}

		// Token: 0x06003F57 RID: 16215 RVA: 0x0002D803 File Offset: 0x0002BA03
		public static Font GetFont(string name)
		{
			if (Tools.s_get_font == null && SymbolTextInit.GetFont(name) == null)
			{
				return Tools.DefaultFont;
			}
			return Tools.s_get_font(name);
		}

		// Token: 0x06003F58 RID: 16216 RVA: 0x0002D82B File Offset: 0x0002BA2B
		public static Font GetDefaultFont()
		{
			if (Tools.DefaultFont == null)
			{
				Tools.DefaultFont = Object.FindObjectOfType<Font>();
			}
			return Tools.DefaultFont;
		}

		// Token: 0x06003F59 RID: 16217 RVA: 0x0002D849 File Offset: 0x0002BA49
		public static Sprite GetSprite(string name)
		{
			if (Tools.s_get_sprite == null)
			{
				return TuJianDB.GetRichTextSprite(name);
			}
			return Tools.s_get_sprite(name);
		}

		// Token: 0x06003F5A RID: 16218 RVA: 0x0002D864 File Offset: 0x0002BA64
		public static Cartoon GetCartoon(string name)
		{
			if (Tools.s_get_cartoon == null)
			{
				return SymbolTextInit.GetCartoon(name);
			}
			return Tools.s_get_cartoon(name);
		}

		// Token: 0x06003F5B RID: 16219 RVA: 0x0002D87F File Offset: 0x0002BA7F
		public static void GetAllCartoons(List<Cartoon> cartoons)
		{
			if (Tools.s_get_cartoons == null)
			{
				SymbolTextInit.GetCartoons(cartoons);
				return;
			}
			Tools.s_get_cartoons(cartoons);
		}

		// Token: 0x06003F5C RID: 16220 RVA: 0x001B9160 File Offset: 0x001B7360
		public static void AddLine(VertexHelper vh, Vector2 leftPos, Vector2 uv, float width, float height, Color color)
		{
			Vector2 vector;
			vector..ctor(leftPos.x, leftPos.y);
			int currentVertCount = vh.currentVertCount;
			vh.AddVert(vector, color, uv);
			vh.AddVert(new Vector3(vector.x + width, vector.y, 0f), color, uv);
			vh.AddVert(new Vector3(vector.x + width, vector.y - height, 0f), color, uv);
			vh.AddVert(new Vector3(vector.x, vector.y - height, 0f), color, uv);
			vh.AddTriangle(currentVertCount, currentVertCount + 1, currentVertCount + 2);
			vh.AddTriangle(currentVertCount + 2, currentVertCount + 3, currentVertCount);
		}

		// Token: 0x06003F5D RID: 16221 RVA: 0x0002D89A File Offset: 0x0002BA9A
		public static bool IsHexadecimal(char c)
		{
			return (c >= '0' && c <= '9') || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F');
		}

		// Token: 0x06003F5E RID: 16222 RVA: 0x001B922C File Offset: 0x001B742C
		public static float stringToFloat(string str, float df)
		{
			float result = 0f;
			if (!float.TryParse(str, out result))
			{
				return df;
			}
			return result;
		}

		// Token: 0x06003F5F RID: 16223 RVA: 0x001B924C File Offset: 0x001B744C
		public static int stringToInt(string str, int df)
		{
			int result = 0;
			if (!int.TryParse(str, out result))
			{
				return df;
			}
			return result;
		}

		// Token: 0x06003F60 RID: 16224 RVA: 0x001B9268 File Offset: 0x001B7468
		public static Font ParserFontName(string text, ref int startpos)
		{
			int length = text.Length;
			string text2 = "";
			while (length > startpos && text2.Length < 10)
			{
				text2 += text[startpos].ToString();
				startpos++;
			}
			Font result = null;
			while (text2.Length != 0 && !((result = Tools.GetFont(text2)) != null))
			{
				text2 = text2.Remove(text2.Length - 1, 1);
				startpos--;
			}
			return result;
		}

		// Token: 0x06003F61 RID: 16225 RVA: 0x001B92E4 File Offset: 0x001B74E4
		public static bool ScreenPointToWorldPointInRectangle(RectTransform rectTrans, Vector2 screenPoint, Camera cam, out Vector2 worldPoint)
		{
			worldPoint = Vector2.zero;
			Ray ray = RectTransformUtility.ScreenPointToRay(cam, screenPoint);
			float num;
			if (!new Plane(rectTrans.rotation * Vector3.back, rectTrans.position).Raycast(ray, ref num))
			{
				return false;
			}
			worldPoint = ray.GetPoint(num);
			worldPoint = rectTrans.worldToLocalMatrix.MultiplyPoint(worldPoint);
			Tools.LB2LT(ref worldPoint, rectTrans.rect.height);
			return true;
		}

		// Token: 0x06003F62 RID: 16226 RVA: 0x001B937C File Offset: 0x001B757C
		public static Color ParseColor(string text, int offset, Color defc)
		{
			Color result;
			if (Tools.ParseColor(text, offset, out result))
			{
				return result;
			}
			return defc;
		}

		// Token: 0x06003F63 RID: 16227 RVA: 0x001B9398 File Offset: 0x001B7598
		public static bool ParseColor(string text, int offset, out Color color)
		{
			color = Color.white;
			int num = text.Length - offset;
			if (num >= 8)
			{
				color = Tools.ParseColor32(text, offset);
				return true;
			}
			if (num >= 6)
			{
				color = Tools.ParseColor24(text, offset);
				return true;
			}
			return false;
		}

		// Token: 0x06003F64 RID: 16228 RVA: 0x001B93E0 File Offset: 0x001B75E0
		public static Color ParseColor24(string text, int offset)
		{
			if (text[offset] == '#')
			{
				offset++;
			}
			int num = Tools.HexToDecimal(text[offset]) << 4 | Tools.HexToDecimal(text[offset + 1]);
			int num2 = Tools.HexToDecimal(text[offset + 2]) << 4 | Tools.HexToDecimal(text[offset + 3]);
			int num3 = Tools.HexToDecimal(text[offset + 4]) << 4 | Tools.HexToDecimal(text[offset + 5]);
			float num4 = 0.003921569f;
			return new Color(num4 * (float)num, num4 * (float)num2, num4 * (float)num3);
		}

		// Token: 0x06003F65 RID: 16229 RVA: 0x001B9474 File Offset: 0x001B7674
		public static Color ParseColor32(string text, int offset)
		{
			if (text[offset] == '#')
			{
				offset++;
			}
			int num = Tools.HexToDecimal(text[offset]) << 4 | Tools.HexToDecimal(text[offset + 1]);
			int num2 = Tools.HexToDecimal(text[offset + 2]) << 4 | Tools.HexToDecimal(text[offset + 3]);
			int num3 = Tools.HexToDecimal(text[offset + 4]) << 4 | Tools.HexToDecimal(text[offset + 5]);
			int num4 = Tools.HexToDecimal(text[offset + 6]) << 4 | Tools.HexToDecimal(text[offset + 7]);
			float num5 = 0.003921569f;
			return new Color(num5 * (float)num, num5 * (float)num2, num5 * (float)num3, num5 * (float)num4);
		}

		// Token: 0x06003F66 RID: 16230 RVA: 0x0007A59C File Offset: 0x0007879C
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
			case ':':
			case ';':
			case '<':
			case '=':
			case '>':
			case '?':
			case '@':
				return 15;
			case 'A':
				break;
			case 'B':
				return 11;
			case 'C':
				return 12;
			case 'D':
				return 13;
			case 'E':
				return 14;
			case 'F':
				return 15;
			default:
				switch (ch)
				{
				case 'a':
					break;
				case 'b':
					return 11;
				case 'c':
					return 12;
				case 'd':
					return 13;
				case 'e':
					return 14;
				case 'f':
					return 15;
				default:
					return 15;
				}
				break;
			}
			return 10;
		}

		// Token: 0x06003F67 RID: 16231 RVA: 0x001B9530 File Offset: 0x001B7730
		public static Color ParserColorName(string text, Color dc)
		{
			int num = 0;
			return Tools.ParserColorName(text, ref num, dc);
		}

		// Token: 0x06003F68 RID: 16232 RVA: 0x001B9548 File Offset: 0x001B7748
		public static Color ParserColorName(string text, ref int startpos, Color dc)
		{
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
			else
			{
				int num2 = startpos + 1;
				startpos = num2;
				int startIndex = num2;
				int num3 = 0;
				int length = text.Length;
				while (length > startpos + num3 && Tools.IsHexadecimal(text[startpos + num3]) && num3 < 8)
				{
					num3++;
				}
				Color result;
				if (Tools.ParseColor(text.Substring(startIndex, num3), 0, out result))
				{
					startpos += num3;
					return result;
				}
				startpos += num3;
				return dc;
			}
		}

		// Token: 0x06003F69 RID: 16233 RVA: 0x001B9600 File Offset: 0x001B7800
		public static T AddChild<T>(this GameObject go) where T : MonoBehaviour
		{
			GameObject gameObject = new GameObject();
			go.AddChild(gameObject);
			return gameObject.AddComponent<T>();
		}

		// Token: 0x06003F6A RID: 16234 RVA: 0x0002D8BD File Offset: 0x0002BABD
		public static void AddChild(this GameObject go, GameObject child)
		{
			Transform transform = child.transform;
			transform.SetParent(go.transform);
			transform.localScale = Vector3.one;
			transform.localPosition = Vector3.zero;
			transform.localEulerAngles = Vector3.zero;
			child.layer = go.layer;
		}

		// Token: 0x06003F6B RID: 16235 RVA: 0x001B9620 File Offset: 0x001B7820
		public static void Destroy(Object obj)
		{
			if (obj)
			{
				if (obj is Transform)
				{
					GameObject gameObject = ((Transform)obj).gameObject;
					if (Application.isPlaying)
					{
						Object.Destroy(gameObject);
						return;
					}
					Object.DestroyImmediate(gameObject);
					return;
				}
				else if (obj is GameObject)
				{
					GameObject gameObject2 = obj as GameObject;
					if (Application.isPlaying)
					{
						Object.Destroy(gameObject2);
						return;
					}
					Object.DestroyImmediate(gameObject2);
					return;
				}
				else
				{
					if (Application.isPlaying)
					{
						Object.Destroy(obj);
						return;
					}
					Object.DestroyImmediate(obj);
				}
			}
		}

		// Token: 0x06003F6C RID: 16236 RVA: 0x001B9698 File Offset: 0x001B7898
		public static void UpdateRect(RectTransform child, Vector2 offset)
		{
			RectTransform rectTransform = child.parent as RectTransform;
			if (rectTransform == null)
			{
				return;
			}
			child.pivot = rectTransform.pivot;
			child.anchorMin = Vector2.zero;
			child.anchorMax = Vector2.one;
			child.offsetMax = Vector2.zero;
			child.offsetMin = Vector2.zero;
			child.localPosition = offset;
			child.localScale = Vector3.one;
			child.localEulerAngles = Vector3.zero;
		}

		// Token: 0x040038C8 RID: 14536
		public const int s_dyn_default_speed = 320;

		// Token: 0x040038C9 RID: 14537
		public const int s_offset_default_speed = 320;

		// Token: 0x040038CA RID: 14538
		private static readonly VertexHelper s_VertexHelper = new VertexHelper();

		// Token: 0x040038CB RID: 14539
		private static Font DefaultFont;

		// Token: 0x040038CC RID: 14540
		public static Func<string, Sprite> s_get_sprite = null;

		// Token: 0x040038CD RID: 14541
		public static Func<string, Font> s_get_font = null;

		// Token: 0x040038CE RID: 14542
		public static Func<string, Cartoon> s_get_cartoon = null;

		// Token: 0x040038CF RID: 14543
		public static Action<List<Cartoon>> s_get_cartoons = null;
	}
}
