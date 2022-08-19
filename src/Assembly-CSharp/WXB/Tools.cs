using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YSGame.TuJian;

namespace WXB
{
	// Token: 0x020006A3 RID: 1699
	public static class Tools
	{
		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06003593 RID: 13715 RVA: 0x001711FB File Offset: 0x0016F3FB
		public static VertexHelper vertexHelper
		{
			get
			{
				return Tools.s_VertexHelper;
			}
		}

		// Token: 0x06003594 RID: 13716 RVA: 0x00171202 File Offset: 0x0016F402
		public static void LB2LT(ref Vector2 pos, float height)
		{
			pos.y = height - pos.y;
		}

		// Token: 0x06003595 RID: 13717 RVA: 0x00171212 File Offset: 0x0016F412
		public static void LT2LB(ref Vector2 pos, float height)
		{
			pos.y += height;
		}

		// Token: 0x06003596 RID: 13718 RVA: 0x0017121F File Offset: 0x0016F41F
		public static Font GetFont(string name)
		{
			if (Tools.s_get_font == null && SymbolTextInit.GetFont(name) == null)
			{
				return Tools.DefaultFont;
			}
			return Tools.s_get_font(name);
		}

		// Token: 0x06003597 RID: 13719 RVA: 0x00171247 File Offset: 0x0016F447
		public static Font GetDefaultFont()
		{
			if (Tools.DefaultFont == null)
			{
				Tools.DefaultFont = Object.FindObjectOfType<Font>();
			}
			return Tools.DefaultFont;
		}

		// Token: 0x06003598 RID: 13720 RVA: 0x00171265 File Offset: 0x0016F465
		public static Sprite GetSprite(string name)
		{
			if (Tools.s_get_sprite == null)
			{
				return TuJianDB.GetRichTextSprite(name);
			}
			return Tools.s_get_sprite(name);
		}

		// Token: 0x06003599 RID: 13721 RVA: 0x00171280 File Offset: 0x0016F480
		public static Cartoon GetCartoon(string name)
		{
			if (Tools.s_get_cartoon == null)
			{
				return SymbolTextInit.GetCartoon(name);
			}
			return Tools.s_get_cartoon(name);
		}

		// Token: 0x0600359A RID: 13722 RVA: 0x0017129B File Offset: 0x0016F49B
		public static void GetAllCartoons(List<Cartoon> cartoons)
		{
			if (Tools.s_get_cartoons == null)
			{
				SymbolTextInit.GetCartoons(cartoons);
				return;
			}
			Tools.s_get_cartoons(cartoons);
		}

		// Token: 0x0600359B RID: 13723 RVA: 0x001712B8 File Offset: 0x0016F4B8
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

		// Token: 0x0600359C RID: 13724 RVA: 0x00171383 File Offset: 0x0016F583
		public static bool IsHexadecimal(char c)
		{
			return (c >= '0' && c <= '9') || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F');
		}

		// Token: 0x0600359D RID: 13725 RVA: 0x001713A8 File Offset: 0x0016F5A8
		public static float stringToFloat(string str, float df)
		{
			float result = 0f;
			if (!float.TryParse(str, out result))
			{
				return df;
			}
			return result;
		}

		// Token: 0x0600359E RID: 13726 RVA: 0x001713C8 File Offset: 0x0016F5C8
		public static int stringToInt(string str, int df)
		{
			int result = 0;
			if (!int.TryParse(str, out result))
			{
				return df;
			}
			return result;
		}

		// Token: 0x0600359F RID: 13727 RVA: 0x001713E4 File Offset: 0x0016F5E4
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

		// Token: 0x060035A0 RID: 13728 RVA: 0x00171460 File Offset: 0x0016F660
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

		// Token: 0x060035A1 RID: 13729 RVA: 0x001714F8 File Offset: 0x0016F6F8
		public static Color ParseColor(string text, int offset, Color defc)
		{
			Color result;
			if (Tools.ParseColor(text, offset, out result))
			{
				return result;
			}
			return defc;
		}

		// Token: 0x060035A2 RID: 13730 RVA: 0x00171514 File Offset: 0x0016F714
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

		// Token: 0x060035A3 RID: 13731 RVA: 0x0017155C File Offset: 0x0016F75C
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

		// Token: 0x060035A4 RID: 13732 RVA: 0x001715F0 File Offset: 0x0016F7F0
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

		// Token: 0x060035A5 RID: 13733 RVA: 0x001716AC File Offset: 0x0016F8AC
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

		// Token: 0x060035A6 RID: 13734 RVA: 0x0017176C File Offset: 0x0016F96C
		public static Color ParserColorName(string text, Color dc)
		{
			int num = 0;
			return Tools.ParserColorName(text, ref num, dc);
		}

		// Token: 0x060035A7 RID: 13735 RVA: 0x00171784 File Offset: 0x0016F984
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

		// Token: 0x060035A8 RID: 13736 RVA: 0x0017183C File Offset: 0x0016FA3C
		public static T AddChild<T>(this GameObject go) where T : MonoBehaviour
		{
			GameObject gameObject = new GameObject();
			go.AddChild(gameObject);
			return gameObject.AddComponent<T>();
		}

		// Token: 0x060035A9 RID: 13737 RVA: 0x0017185C File Offset: 0x0016FA5C
		public static void AddChild(this GameObject go, GameObject child)
		{
			Transform transform = child.transform;
			transform.SetParent(go.transform);
			transform.localScale = Vector3.one;
			transform.localPosition = Vector3.zero;
			transform.localEulerAngles = Vector3.zero;
			child.layer = go.layer;
		}

		// Token: 0x060035AA RID: 13738 RVA: 0x0017189C File Offset: 0x0016FA9C
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

		// Token: 0x060035AB RID: 13739 RVA: 0x00171914 File Offset: 0x0016FB14
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

		// Token: 0x04002F0D RID: 12045
		public const int s_dyn_default_speed = 320;

		// Token: 0x04002F0E RID: 12046
		public const int s_offset_default_speed = 320;

		// Token: 0x04002F0F RID: 12047
		private static readonly VertexHelper s_VertexHelper = new VertexHelper();

		// Token: 0x04002F10 RID: 12048
		private static Font DefaultFont;

		// Token: 0x04002F11 RID: 12049
		public static Func<string, Sprite> s_get_sprite = null;

		// Token: 0x04002F12 RID: 12050
		public static Func<string, Font> s_get_font = null;

		// Token: 0x04002F13 RID: 12051
		public static Func<string, Cartoon> s_get_cartoon = null;

		// Token: 0x04002F14 RID: 12052
		public static Action<List<Cartoon>> s_get_cartoons = null;
	}
}
