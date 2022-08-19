using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace WXB
{
	// Token: 0x020006B9 RID: 1721
	public class TextParser
	{
		// Token: 0x06003658 RID: 13912 RVA: 0x00174050 File Offset: 0x00172250
		public T CreateNode<T>() where T : NodeBase, new()
		{
			T t = Activator.CreateInstance<T>();
			t.Reset(this.mOwner, this.currentConfig.anchor);
			return t;
		}

		// Token: 0x06003659 RID: 13913 RVA: 0x00174073 File Offset: 0x00172273
		private static bool Get(char c, out Anchor a)
		{
			switch (c)
			{
			case '1':
				a = Anchor.MiddleLeft;
				return true;
			case '2':
				a = Anchor.MiddleCenter;
				return true;
			case '3':
				a = Anchor.MiddleRight;
				return true;
			default:
				a = Anchor.MiddleCenter;
				return false;
			}
		}

		// Token: 0x0600365A RID: 13914 RVA: 0x0017409F File Offset: 0x0017229F
		private static bool Get(char c, out LineAlignment a)
		{
			switch (c)
			{
			case '1':
				a = LineAlignment.Top;
				return true;
			case '2':
				a = LineAlignment.Center;
				return true;
			case '3':
				a = LineAlignment.Bottom;
				return true;
			default:
				a = LineAlignment.Default;
				return false;
			}
		}

		// Token: 0x0600365B RID: 13915 RVA: 0x001740CB File Offset: 0x001722CB
		public TextParser()
		{
			this.clear();
			this.Reg();
			this.RegTag();
		}

		// Token: 0x0600365C RID: 13916 RVA: 0x001740F0 File Offset: 0x001722F0
		private static bool ParserInt(ref int d_curPos, string text, ref int value, int num = 3)
		{
			bool result;
			using (PD<StringBuilder> sb = Pool.GetSB())
			{
				StringBuilder value2 = sb.value;
				d_curPos++;
				while (text.Length > d_curPos && text[d_curPos] >= '0' && text[d_curPos] <= '9')
				{
					value2.Append(text[d_curPos]);
					d_curPos++;
					if (value2.Length >= num)
					{
						break;
					}
				}
				value = Tools.stringToInt(value2.ToString(), -1);
				if (value2.Length == 0)
				{
					d_curPos--;
					result = false;
				}
				else
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x0600365D RID: 13917 RVA: 0x00174198 File Offset: 0x00172398
		private static bool ParserFloat(ref int d_curPos, string text, ref float value, int num = 3)
		{
			bool result;
			using (PD<StringBuilder> sb = Pool.GetSB())
			{
				StringBuilder value2 = sb.value;
				d_curPos++;
				bool flag = false;
				while (text.Length > d_curPos && ((text[d_curPos] >= '0' && text[d_curPos] <= '9') || text[d_curPos] == '.'))
				{
					if (text[d_curPos] == '.')
					{
						flag = true;
					}
					value2.Append(text[d_curPos]);
					d_curPos++;
					int num2 = flag ? (num + 1) : num;
					if (value2.Length >= num2)
					{
						break;
					}
				}
				value = Tools.stringToFloat(value2.ToString(), 0f);
				if (value2.Length == 0)
				{
					d_curPos--;
					result = false;
				}
				else
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x0600365E RID: 13918 RVA: 0x0017426C File Offset: 0x0017246C
		public void parser(Owner owner, string text, TextParser.Config config, List<NodeBase> vList)
		{
			this.clear();
			this.mOwner = owner;
			this.d_nodeList = vList;
			this.startConfig.Set(config);
			this.currentConfig.Set(config);
			if (this.currentConfig.font == null)
			{
				Debug.LogError("TextParser pFont == null");
				return;
			}
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			int i = text.Length;
			while (i > this.d_curPos)
			{
				if (!this.d_bBegin)
				{
					char c = text[this.d_curPos];
					if (c != '\n')
					{
						if (c != '#')
						{
							if (c != '<')
							{
								this.d_text.Append(text[this.d_curPos]);
								this.d_curPos++;
							}
							else
							{
								int num = text.IndexOf('>', this.d_curPos);
								if (num != -1)
								{
									string param = null;
									int num2 = text.IndexOfAny(new char[]
									{
										' ',
										'='
									}, this.d_curPos);
									string tag;
									if (num2 != -1 && num2 < num)
									{
										tag = text.Substring(this.d_curPos + 1, num2 - this.d_curPos);
										param = text.Substring(num2 + 1, num - num2 - 1);
									}
									else
									{
										tag = text.Substring(this.d_curPos + 1, num - this.d_curPos - 1);
									}
									if (this.d_text.Length != 0)
									{
										this.save(false);
									}
									this.TagParam(tag, param);
									this.d_curPos = num + 1;
								}
								else
								{
									this.d_text.Append(text[this.d_curPos]);
									this.d_curPos++;
								}
							}
						}
						else
						{
							this.d_bBegin = true;
							this.d_curPos++;
						}
					}
					else
					{
						this.save(true);
						this.d_curPos++;
					}
				}
				else
				{
					char c2 = text[this.d_curPos];
					TextParser.OnFun onFun;
					if (c2 < '\u0080' && (onFun = this.OnFuns[(int)c2]) != null)
					{
						onFun(text);
					}
					else
					{
						this.d_text.Append(text[this.d_curPos]);
						this.d_curPos++;
					}
					this.d_bBegin = false;
				}
			}
			if (this.d_text.Length != 0)
			{
				this.save(false);
			}
			this.clear();
		}

		// Token: 0x0600365F RID: 13919 RVA: 0x001744C8 File Offset: 0x001726C8
		protected void save(bool isNewLine)
		{
			if (this.d_text.Length != 0)
			{
				TextNode textNode = this.CreateNode<TextNode>();
				textNode.d_text = this.d_text.ToString();
				textNode.SetConfig(this.currentConfig);
				textNode.setNewLine(isNewLine);
				this.d_nodeList.Add(textNode);
				this.d_text.Remove(0, this.d_text.Length);
				return;
			}
			if (isNewLine)
			{
				if (this.d_nodeList.Count != 0)
				{
					NodeBase nodeBase = this.d_nodeList.back<NodeBase>();
					if (!nodeBase.isNewLine())
					{
						nodeBase.setNewLine(true);
						return;
					}
				}
				LineNode lineNode = this.CreateNode<LineNode>();
				lineNode.SetConfig(this.currentConfig);
				lineNode.font = this.currentConfig.font;
				lineNode.fontSize = this.currentConfig.fontSize;
				lineNode.fs = this.currentConfig.fontStyle;
				lineNode.setNewLine(true);
				this.d_nodeList.Add(lineNode);
				return;
			}
		}

		// Token: 0x06003660 RID: 13920 RVA: 0x001745C0 File Offset: 0x001727C0
		protected void saveX(float value)
		{
			XSpaceNode xspaceNode = this.CreateNode<XSpaceNode>();
			xspaceNode.d_offset = value;
			this.d_nodeList.Add(xspaceNode);
		}

		// Token: 0x06003661 RID: 13921 RVA: 0x001745E8 File Offset: 0x001727E8
		protected void saveY(float value)
		{
			if (this.d_nodeList.Count != 0 && !this.d_nodeList.back<NodeBase>().isNewLine())
			{
				this.d_nodeList.back<NodeBase>().setNewLine(true);
			}
			YSpaceNode yspaceNode = this.CreateNode<YSpaceNode>();
			yspaceNode.d_offset = value;
			yspaceNode.setNewLine(true);
			this.d_nodeList.Add(yspaceNode);
		}

		// Token: 0x06003662 RID: 13922 RVA: 0x00174648 File Offset: 0x00172848
		protected void saveZ(float value)
		{
			YSpaceNode yspaceNode = this.CreateNode<YSpaceNode>();
			yspaceNode.d_offset = value;
			yspaceNode.setNewLine(false);
			this.d_nodeList.Add(yspaceNode);
		}

		// Token: 0x06003663 RID: 13923 RVA: 0x00174678 File Offset: 0x00172878
		protected void saveHy()
		{
			if (this.d_text.Length == 0)
			{
				return;
			}
			string text = this.d_text.ToString();
			this.d_text.Remove(0, this.d_text.Length);
			HyperlinkNode hyperlinkNode = this.CreateNode<HyperlinkNode>();
			string text2 = string.Empty;
			if (text[text.Length - 1] == '}')
			{
				int num = text.IndexOf('{', 0);
				if (num != -1)
				{
					text2 = text.Substring(num, text.Length - num);
					hyperlinkNode.d_link = text2.Replace("{", "").Replace("}", "");
					text = text.Remove(num, text.Length - num);
				}
			}
			hyperlinkNode.d_text = "";
			hyperlinkNode.SetConfig(this.currentConfig);
			this.ParseHyText(text, hyperlinkNode);
			this.d_nodeList.Add(hyperlinkNode);
		}

		// Token: 0x06003664 RID: 13924 RVA: 0x00174754 File Offset: 0x00172954
		protected void clear()
		{
			this.startConfig.Clear();
			this.currentConfig.Clear();
			this.d_nodeList = null;
			this.d_curPos = 0;
			this.d_text.Remove(0, this.d_text.Length);
			this.d_bBegin = false;
			this.mOwner = null;
		}

		// Token: 0x06003665 RID: 13925 RVA: 0x001747AC File Offset: 0x001729AC
		private static Color GetColour(uint code)
		{
			if (code <= 75U)
			{
				if (code == 66U)
				{
					return Color.blue;
				}
				if (code == 71U)
				{
					return Color.green;
				}
				if (code == 75U)
				{
					return Color.black;
				}
			}
			else
			{
				if (code == 82U)
				{
					return Color.red;
				}
				if (code == 87U)
				{
					return Color.white;
				}
				if (code == 89U)
				{
					return Color.yellow;
				}
			}
			return Color.white;
		}

		// Token: 0x06003666 RID: 13926 RVA: 0x0017480C File Offset: 0x00172A0C
		private void Reg()
		{
			this.OnFuns = new TextParser.OnFun[128];
			this.OnFuns[82] = new TextParser.OnFun(this.ParserSureColor);
			this.OnFuns[71] = new TextParser.OnFun(this.ParserSureColor);
			this.OnFuns[66] = new TextParser.OnFun(this.ParserSureColor);
			this.OnFuns[75] = new TextParser.OnFun(this.ParserSureColor);
			this.OnFuns[89] = new TextParser.OnFun(this.ParserSureColor);
			this.OnFuns[87] = new TextParser.OnFun(this.ParserSureColor);
			this.OnFuns[35] = new TextParser.OnFun(this.ParserOutputChar);
			this.OnFuns[91] = new TextParser.OnFun(this.ParserFontColorS);
			this.OnFuns[98] = new TextParser.OnFun(this.ParserBlink);
			this.OnFuns[99] = new TextParser.OnFun(this.ParserFontColor);
			this.OnFuns[100] = new TextParser.OnFun(this.ParserFontStyle);
			this.OnFuns[101] = new TextParser.OnFun(this.ParserStrickout);
			this.OnFuns[109] = new TextParser.OnFun(this.ParserDynStrickout);
			this.OnFuns[102] = new TextParser.OnFun(this.ParserFont);
			this.OnFuns[110] = new TextParser.OnFun(this.ParserRestoreColor);
			this.OnFuns[103] = new TextParser.OnFun(this.ParserRestore);
			this.OnFuns[114] = new TextParser.OnFun(this.ParserNewLine);
			this.OnFuns[117] = new TextParser.OnFun(this.ParserUnderLine);
			this.OnFuns[116] = new TextParser.OnFun(this.ParserDynUnderline);
			this.OnFuns[108] = new TextParser.OnFun(this.ParserDynSpeed);
			this.OnFuns[104] = new TextParser.OnFun(this.ParserHyperlink);
			this.OnFuns[115] = new TextParser.OnFun(this.ParserFontSize);
			this.OnFuns[120] = new TextParser.OnFun(this.ParserXYZ);
			this.OnFuns[121] = new TextParser.OnFun(this.ParserXYZ);
			this.OnFuns[122] = new TextParser.OnFun(this.ParserXYZ);
			this.OnFuns[119] = new TextParser.OnFun(this.ParserFormatting);
			this.OnFuns[97] = new TextParser.OnFun(this.ParserLineAlignment);
			this.OnFuns[48] = new TextParser.OnFun(this.ParserCartoon);
			this.OnFuns[49] = new TextParser.OnFun(this.ParserCartoon);
			this.OnFuns[50] = new TextParser.OnFun(this.ParserCartoon);
			this.OnFuns[51] = new TextParser.OnFun(this.ParserCartoon);
			this.OnFuns[52] = new TextParser.OnFun(this.ParserCartoon);
			this.OnFuns[53] = new TextParser.OnFun(this.ParserCartoon);
			this.OnFuns[54] = new TextParser.OnFun(this.ParserCartoon);
			this.OnFuns[55] = new TextParser.OnFun(this.ParserCartoon);
			this.OnFuns[56] = new TextParser.OnFun(this.ParserCartoon);
			this.OnFuns[57] = new TextParser.OnFun(this.ParserCartoon);
		}

		// Token: 0x06003667 RID: 13927 RVA: 0x00174B34 File Offset: 0x00172D34
		private void ParserCartoon(string text)
		{
			Cartoon cartoon = null;
			int num = this.d_curPos - 1;
			for (int i = 3; i >= 1; i--)
			{
				int num2 = -1;
				if (TextParser.ParserInt(ref num, text, ref num2, i))
				{
					cartoon = Tools.GetCartoon(num2.ToString());
					if (cartoon != null)
					{
						break;
					}
				}
				num = this.d_curPos - 1;
			}
			if (cartoon == null)
			{
				return;
			}
			this.d_curPos = num;
			this.save(false);
			CartoonNode cartoonNode = this.CreateNode<CartoonNode>();
			cartoonNode.cartoon = cartoon;
			cartoonNode.width = (float)cartoon.width;
			cartoonNode.height = (float)cartoon.height;
			cartoonNode.SetConfig(this.currentConfig);
			this.d_nodeList.Add(cartoonNode);
		}

		// Token: 0x06003668 RID: 13928 RVA: 0x00174BD4 File Offset: 0x00172DD4
		private void ParserDynSpeed(string text)
		{
			int num = -1;
			if (!TextParser.ParserInt(ref this.d_curPos, text, ref num, 5))
			{
				return;
			}
			if (num <= 0 || num == this.currentConfig.dyncSpeed)
			{
				return;
			}
			this.save(false);
			this.currentConfig.dyncSpeed = num;
		}

		// Token: 0x06003669 RID: 13929 RVA: 0x00174C1B File Offset: 0x00172E1B
		private void ParserDynUnderline(string text)
		{
			this.save(false);
			this.currentConfig.isDyncUnderline = !this.currentConfig.isDyncUnderline;
			this.d_curPos++;
		}

		// Token: 0x0600366A RID: 13930 RVA: 0x00174C4B File Offset: 0x00172E4B
		private void ParserDynStrickout(string text)
		{
			this.save(false);
			this.currentConfig.isDyncStrickout = !this.currentConfig.isDyncStrickout;
			this.d_curPos++;
		}

		// Token: 0x0600366B RID: 13931 RVA: 0x00174C7C File Offset: 0x00172E7C
		private void ParserRestoreColor(string text)
		{
			if (this.currentConfig.fontColor != this.startConfig.fontColor)
			{
				this.save(false);
				this.d_curPos++;
				this.currentConfig.Set(this.startConfig);
				return;
			}
			this.d_curPos++;
		}

		// Token: 0x0600366C RID: 13932 RVA: 0x00174CDC File Offset: 0x00172EDC
		private void ParserRestore(string text)
		{
			if (!this.currentConfig.isSame(this.startConfig))
			{
				this.save(false);
				this.d_curPos++;
				this.currentConfig.Set(this.startConfig);
				return;
			}
			this.d_curPos++;
		}

		// Token: 0x0600366D RID: 13933 RVA: 0x00174D34 File Offset: 0x00172F34
		private void ParserSureColor(string text)
		{
			Color colour = TextParser.GetColour((uint)text[this.d_curPos]);
			if (this.currentConfig.fontColor != colour)
			{
				this.save(false);
				this.currentConfig.fontColor = colour;
			}
			this.d_curPos++;
		}

		// Token: 0x0600366E RID: 13934 RVA: 0x00174D87 File Offset: 0x00172F87
		private void ParserBlink(string text)
		{
			this.save(false);
			this.currentConfig.isBlink = !this.currentConfig.isBlink;
			this.d_curPos++;
		}

		// Token: 0x0600366F RID: 13935 RVA: 0x00174DB8 File Offset: 0x00172FB8
		private void ParserLineAlignment(string text)
		{
			LineAlignment lineAlignment;
			if (text.Length > this.d_curPos + 1 && TextParser.Get(text[this.d_curPos + 1], out lineAlignment))
			{
				this.d_curPos++;
				if (this.currentConfig.lineAlignment != lineAlignment)
				{
					this.currentConfig.lineAlignment = lineAlignment;
					this.save(false);
				}
			}
			this.d_curPos++;
		}

		// Token: 0x06003670 RID: 13936 RVA: 0x00174E2C File Offset: 0x0017302C
		private void ParserFormatting(string text)
		{
			Anchor anchor;
			if (text.Length > this.d_curPos + 1 && TextParser.Get(text[this.d_curPos + 1], out anchor))
			{
				this.d_curPos++;
				if (this.currentConfig.anchor != anchor)
				{
					this.currentConfig.anchor = anchor;
					this.save(false);
				}
			}
			this.d_curPos++;
		}

		// Token: 0x06003671 RID: 13937 RVA: 0x00174E9D File Offset: 0x0017309D
		private static bool GetFontStyle(char c, out FontStyle fs)
		{
			switch (c)
			{
			case '1':
				fs = 0;
				return true;
			case '2':
				fs = 1;
				return true;
			case '3':
				fs = 2;
				return true;
			case '4':
				fs = 3;
				return true;
			default:
				fs = 0;
				return false;
			}
		}

		// Token: 0x06003672 RID: 13938 RVA: 0x00174ED4 File Offset: 0x001730D4
		private void ParserFontStyle(string text)
		{
			if (text.Length > this.d_curPos + 1)
			{
				FontStyle fontStyle;
				if (!TextParser.GetFontStyle(text[this.d_curPos + 1], out fontStyle))
				{
					this.d_curPos++;
					return;
				}
				this.d_curPos += 2;
				if (this.currentConfig.fontStyle != fontStyle)
				{
					this.save(false);
					this.currentConfig.fontStyle = fontStyle;
					return;
				}
			}
			else
			{
				this.d_curPos++;
			}
		}

		// Token: 0x06003673 RID: 13939 RVA: 0x00174F55 File Offset: 0x00173155
		private void ParserStrickout(string text)
		{
			this.save(false);
			this.currentConfig.isStrickout = !this.currentConfig.isStrickout;
			this.d_curPos++;
		}

		// Token: 0x06003674 RID: 13940 RVA: 0x00174F88 File Offset: 0x00173188
		private void ParserFontColorS(string text)
		{
			this.d_curPos--;
			Color color = Tools.ParserColorName(text, ref this.d_curPos, this.currentConfig.fontColor);
			if (color != this.currentConfig.fontColor)
			{
				this.save(false);
				this.currentConfig.fontColor = color;
			}
		}

		// Token: 0x06003675 RID: 13941 RVA: 0x00174FE4 File Offset: 0x001731E4
		private void ParserFontColor(string text)
		{
			Color color = Tools.ParserColorName(text, ref this.d_curPos, this.currentConfig.fontColor);
			if (color != this.currentConfig.fontColor)
			{
				this.save(false);
				this.currentConfig.fontColor = color;
			}
		}

		// Token: 0x06003676 RID: 13942 RVA: 0x0017502F File Offset: 0x0017322F
		private void ParserUnderLine(string text)
		{
			this.save(false);
			this.currentConfig.isUnderline = !this.currentConfig.isUnderline;
			this.d_curPos++;
		}

		// Token: 0x06003677 RID: 13943 RVA: 0x0017505F File Offset: 0x0017325F
		private void ParserNewLine(string text)
		{
			this.save(true);
			this.d_curPos++;
		}

		// Token: 0x06003678 RID: 13944 RVA: 0x00175076 File Offset: 0x00173276
		private void ParserOutputChar(string text)
		{
			this.d_text.Append('#');
			this.d_curPos++;
		}

		// Token: 0x06003679 RID: 13945 RVA: 0x00175094 File Offset: 0x00173294
		private void ParserHyperlink(string text)
		{
			int num = text.IndexOf("#h", this.d_curPos + 1);
			if (num == -1)
			{
				this.d_curPos++;
				return;
			}
			this.save(false);
			this.d_text.Remove(0, this.d_text.Length);
			this.d_text.Append(text, this.d_curPos + 1, num - this.d_curPos - 1);
			this.saveHy();
			this.d_curPos = num + 2;
		}

		// Token: 0x0600367A RID: 13946 RVA: 0x00175118 File Offset: 0x00173318
		private void ParserFontSize(string text)
		{
			float num = 1f;
			if (!TextParser.ParserFloat(ref this.d_curPos, text, ref num, 2))
			{
				return;
			}
			this.save(false);
			this.currentConfig.fontSize = (int)num;
		}

		// Token: 0x0600367B RID: 13947 RVA: 0x00175154 File Offset: 0x00173354
		private void ParserXYZ(string text)
		{
			int index = this.d_curPos;
			float num = 0f;
			if (!TextParser.ParserFloat(ref this.d_curPos, text, ref num, 3))
			{
				return;
			}
			if (num == 0f)
			{
				return;
			}
			if (text[index] == 'x')
			{
				this.save(false);
				this.saveX(num);
				return;
			}
			if (text[index] == 'y')
			{
				if (this.d_text.Length != 0)
				{
					this.save(true);
				}
				this.saveY(num);
				return;
			}
			if (text[index] == 'z')
			{
				if (this.d_text.Length != 0)
				{
					this.save(false);
				}
				this.saveZ(num);
			}
		}

		// Token: 0x0600367C RID: 13948 RVA: 0x001751F0 File Offset: 0x001733F0
		private void ParserFont(string text)
		{
			this.d_curPos++;
			Font font = Tools.ParserFontName(text, ref this.d_curPos);
			if (font != null)
			{
				if (this.currentConfig.font != font)
				{
					this.save(false);
					this.currentConfig.font = font;
					return;
				}
			}
			else
			{
				this.d_curPos--;
			}
		}

		// Token: 0x0600367D RID: 13949 RVA: 0x00175258 File Offset: 0x00173458
		private void ParseHyText(string text, HyperlinkNode data)
		{
			if (TextParser.hyConfig == null)
			{
				TextParser.hyConfig = new TextParser.HyConfig(this);
			}
			TextParser.hyConfig.text = text;
			TextParser.hyConfig.node = data;
			TextParser.hyConfig.startPos = 0;
			TextParser.hyConfig.lenght = text.Length;
			using (PD<StringBuilder> sb = Pool.GetSB())
			{
				TextParser.hyConfig.BeginParser(sb.value);
				TextParser.hyConfig.Clear();
			}
		}

		// Token: 0x0600367E RID: 13950 RVA: 0x001752E8 File Offset: 0x001734E8
		private void Reg(string type, Action<string, TagAttributes> fun)
		{
			this.TagFuns.Add(type, delegate(string key, string param)
			{
				TextParser.s_TagAttributes.Release();
				TextParser.s_TagAttributes.Add(param);
				try
				{
					fun(type, TextParser.s_TagAttributes);
				}
				catch (Exception ex)
				{
					Debug.LogException(ex);
				}
				TextParser.s_TagAttributes.Release();
			});
		}

		// Token: 0x0600367F RID: 13951 RVA: 0x00175326 File Offset: 0x00173526
		private static Color ParserColorName(string name, int startpos, Color c)
		{
			if (string.IsNullOrEmpty(name))
			{
				return c;
			}
			if (name[startpos] == '#')
			{
				return Tools.ParseColor(name, startpos, c);
			}
			return ColorConst.Get(name, c);
		}

		// Token: 0x06003680 RID: 13952 RVA: 0x00175350 File Offset: 0x00173550
		private static void SetDefaultConfig(NodeBase nb, TagAttributes att)
		{
			nb.d_bBlink = att.getValueAsBool("b", nb.d_bBlink);
			nb.d_color = TextParser.ParserColorName(att.getValueAsString("c"), 0, nb.d_color);
			int valueAsInteger = att.getValueAsInteger("x", -1);
			if (valueAsInteger > 0)
			{
				nb.d_bOffset = true;
				nb.d_rectOffset.xMin = (float)(-(float)valueAsInteger / 2);
				nb.d_rectOffset.xMax = (float)(valueAsInteger / 2);
			}
			valueAsInteger = att.getValueAsInteger("y", -1);
			if (valueAsInteger > 0)
			{
				nb.d_bOffset = true;
				nb.d_rectOffset.yMin = (float)(-(float)valueAsInteger / 2);
				nb.d_rectOffset.yMax = (float)(valueAsInteger / 2);
			}
		}

		// Token: 0x06003681 RID: 13953 RVA: 0x00175400 File Offset: 0x00173600
		private static void SetSizeConfig(RectNode nb, TagAttributes att, Vector2 size)
		{
			nb.width = att.getValueAsFloat("w", size.x);
			nb.height = att.getValueAsFloat("h", size.y);
			int valueAsInteger = att.getValueAsInteger("t", 0);
			if (valueAsInteger == 1)
			{
				nb.height = nb.width * size.y / size.x;
				return;
			}
			if (valueAsInteger != 2)
			{
				return;
			}
			nb.width = nb.height * size.x / size.y;
		}

		// Token: 0x06003682 RID: 13954 RVA: 0x00175488 File Offset: 0x00173688
		private void RegTag()
		{
			this.TagFuns = new Dictionary<string, Action<string, string>>();
			this.Reg("sprite ", delegate(string tag, TagAttributes att)
			{
				string valueAsString = att.getValueAsString("n");
				Sprite sprite = Tools.GetSprite(valueAsString);
				if (sprite == null)
				{
					Debug.LogErrorFormat("not find sprite:{0}!", new object[]
					{
						valueAsString
					});
					return;
				}
				Vector2 size = sprite.rect.size;
				SpriteNode spriteNode = this.CreateNode<SpriteNode>();
				spriteNode.sprite = sprite;
				spriteNode.SetConfig(this.currentConfig);
				TextParser.SetSizeConfig(spriteNode, att, size);
				TextParser.SetDefaultConfig(spriteNode, att);
				this.d_nodeList.Add(spriteNode);
			});
			this.Reg("pos ", delegate(string tag, TagAttributes att)
			{
				SetPosNode setPosNode = this.CreateNode<SetPosNode>();
				setPosNode.d_value = att.getValueAsFloat("v", 0f);
				setPosNode.type = (TypePosition)att.getValueAsInteger("t", 0);
				this.d_nodeList.Add(setPosNode);
			});
			this.Reg("RectSprite ", delegate(string tag, TagAttributes att)
			{
				Sprite sprite = Tools.GetSprite(att.getValueAsString("n"));
				if (sprite == null)
				{
					Debug.LogErrorFormat("not find sprite:{0}!", new object[]
					{
						att.getValueAsString("n")
					});
					return;
				}
				RectSpriteNode rectSpriteNode = this.CreateNode<RectSpriteNode>();
				rectSpriteNode.SetConfig(this.currentConfig);
				Rect rect = sprite.rect;
				rectSpriteNode.sprite = sprite;
				rectSpriteNode.rect.width = att.getValueAsFloat("w", rect.width);
				rectSpriteNode.rect.height = att.getValueAsFloat("h", rect.height);
				int valueAsInteger = att.getValueAsInteger("t", 0);
				if (valueAsInteger != 1)
				{
					if (valueAsInteger == 2)
					{
						rectSpriteNode.rect.width = rectSpriteNode.rect.height * rect.width / rect.height;
					}
				}
				else
				{
					rectSpriteNode.rect.height = rectSpriteNode.rect.width * rect.height / rect.width;
				}
				rectSpriteNode.rect.x = att.getValueAsFloat("px", 0f);
				rectSpriteNode.rect.y = att.getValueAsFloat("py", 0f);
				TextParser.SetDefaultConfig(rectSpriteNode, att);
				this.d_nodeList.Add(rectSpriteNode);
			});
			this.Reg("hy ", delegate(string tag, TagAttributes att)
			{
				HyperlinkNode hyperlinkNode = this.CreateNode<HyperlinkNode>();
				hyperlinkNode.SetConfig(this.currentConfig);
				hyperlinkNode.d_text = att.getValueAsString("t");
				hyperlinkNode.d_link = att.getValueAsString("l");
				hyperlinkNode.d_fontSize = att.getValueAsInteger("fs", hyperlinkNode.d_fontSize);
				hyperlinkNode.d_fontStyle = att.getValueAsInteger("ft", hyperlinkNode.d_fontStyle);
				if (att.exists("fn"))
				{
					hyperlinkNode.d_font = Tools.GetFont(att.getValueAsString("fn"));
				}
				hyperlinkNode.d_color = TextParser.ParserColorName(att.getValueAsString("fc"), 0, hyperlinkNode.d_color);
				hyperlinkNode.hoveColor = TextParser.ParserColorName(att.getValueAsString("fhc"), 0, hyperlinkNode.hoveColor);
				hyperlinkNode.d_bUnderline = att.getValueAsBool("ul", hyperlinkNode.d_bUnderline);
				hyperlinkNode.d_bStrickout = att.getValueAsBool("so", hyperlinkNode.d_bStrickout);
				this.d_nodeList.Add(hyperlinkNode);
			});
			this.Reg("face ", delegate(string tag, TagAttributes att)
			{
				Cartoon cartoon = Tools.GetCartoon(att.getValueAsString("n"));
				if (cartoon == null)
				{
					return;
				}
				CartoonNode cartoonNode = this.CreateNode<CartoonNode>();
				cartoonNode.cartoon = cartoon;
				cartoonNode.width = (float)cartoon.width;
				cartoonNode.height = (float)cartoon.height;
				cartoonNode.SetConfig(this.currentConfig);
				TextParser.SetSizeConfig(cartoonNode, att, new Vector2((float)cartoon.width, (float)cartoon.height));
				TextParser.SetDefaultConfig(cartoonNode, att);
				this.d_nodeList.Add(cartoonNode);
			});
			this.TagFuns.Add("color=", delegate(string tag, string param)
			{
				if (string.IsNullOrEmpty(param))
				{
					return;
				}
				this.currentConfig.fontColor = TextParser.ParserColorName(param, 1, this.currentConfig.fontColor);
			});
			this.TagFuns.Add("/color", delegate(string tag, string param)
			{
				this.currentConfig.fontColor = this.startConfig.fontColor;
			});
			this.TagFuns.Add("b", delegate(string tag, string param)
			{
				this.currentConfig.fontStyle = (this.currentConfig.fontStyle | 1);
			});
			this.TagFuns.Add("/b", delegate(string tag, string param)
			{
				this.currentConfig.fontStyle = (this.currentConfig.fontStyle & -2);
			});
			this.TagFuns.Add("i", delegate(string tag, string param)
			{
				this.currentConfig.fontStyle = (this.currentConfig.fontStyle | 2);
			});
			this.TagFuns.Add("/i", delegate(string tag, string param)
			{
				this.currentConfig.fontStyle = (this.currentConfig.fontStyle & -3);
			});
			this.TagFuns.Add("size=", delegate(string tag, string param)
			{
				this.currentConfig.fontSize = (int)Tools.stringToFloat(param, (float)this.currentConfig.fontSize);
			});
			this.TagFuns.Add("/size", delegate(string tag, string param)
			{
				this.currentConfig.fontSize = this.startConfig.fontSize;
			});
			this.Reg("ol ", delegate(string tag, TagAttributes att)
			{
				this.currentConfig.effectType = EffectType.Outline;
				TextParser.ParamEffectType(ref this.currentConfig, att);
			});
			this.TagFuns.Add("/ol", delegate(string tag, string param)
			{
				this.currentConfig.effectType = EffectType.Null;
			});
			this.Reg("so ", delegate(string tag, TagAttributes att)
			{
				this.currentConfig.effectType = EffectType.Outline;
				TextParser.ParamEffectType(ref this.currentConfig, att);
			});
			this.TagFuns.Add("/so", delegate(string tag, string param)
			{
				this.currentConfig.effectType = EffectType.Null;
			});
			this.Reg("offset ", delegate(string tag, TagAttributes att)
			{
				float valueAsFloat = att.getValueAsFloat("x", 0f);
				float valueAsFloat2 = att.getValueAsFloat("y", 0f);
				if (valueAsFloat <= 0f && valueAsFloat2 <= 0f)
				{
					return;
				}
				this.currentConfig.isOffset = true;
				this.currentConfig.offsetRect.xMin = -valueAsFloat / 2f;
				this.currentConfig.offsetRect.xMax = valueAsFloat / 2f;
				this.currentConfig.offsetRect.yMin = -valueAsFloat / 2f;
				this.currentConfig.offsetRect.yMax = valueAsFloat / 2f;
			});
		}

		// Token: 0x06003683 RID: 13955 RVA: 0x00175670 File Offset: 0x00173870
		private static void ParamEffectType(ref TextParser.Config config, TagAttributes att)
		{
			config.effectColor = TextParser.ParserColorName(att.getValueAsString("c"), 0, Color.black);
			config.effectDistance.x = att.getValueAsFloat("x", 1f);
			config.effectDistance.y = att.getValueAsFloat("y", 1f);
		}

		// Token: 0x06003684 RID: 13956 RVA: 0x001756D0 File Offset: 0x001738D0
		private void TagParam(string tag, string param)
		{
			Action<string, string> action;
			if (this.TagFuns.TryGetValue(tag, out action))
			{
				action(tag, param);
				return;
			}
			Debug.LogErrorFormat("tag:{0} param:{1} not find!", new object[]
			{
				tag,
				param
			});
		}

		// Token: 0x04002F80 RID: 12160
		private Owner mOwner;

		// Token: 0x04002F81 RID: 12161
		protected int d_curPos;

		// Token: 0x04002F82 RID: 12162
		protected TextParser.Config startConfig;

		// Token: 0x04002F83 RID: 12163
		protected TextParser.Config currentConfig;

		// Token: 0x04002F84 RID: 12164
		protected List<NodeBase> d_nodeList;

		// Token: 0x04002F85 RID: 12165
		protected StringBuilder d_text = new StringBuilder();

		// Token: 0x04002F86 RID: 12166
		protected bool d_bBegin;

		// Token: 0x04002F87 RID: 12167
		private TextParser.OnFun[] OnFuns;

		// Token: 0x04002F88 RID: 12168
		private static TextParser.HyConfig hyConfig = null;

		// Token: 0x04002F89 RID: 12169
		private Dictionary<string, Action<string, string>> TagFuns;

		// Token: 0x04002F8A RID: 12170
		private static TagAttributes s_TagAttributes = new TagAttributes();

		// Token: 0x0200150D RID: 5389
		public struct Config
		{
			// Token: 0x060082DD RID: 33501 RVA: 0x002DCD30 File Offset: 0x002DAF30
			public void Clear()
			{
				this.anchor = Anchor.Null;
				this.font = null;
				this.fontStyle = 0;
				this.fontSize = 0;
				this.fontColor = Color.white;
				this.isUnderline = false;
				this.isStrickout = false;
				this.isBlink = false;
				this.isDyncUnderline = false;
				this.isDyncStrickout = false;
				this.dyncSpeed = 0;
				this.isOffset = false;
				this.offsetRect.Set(0f, 0f, 0f, 0f);
				this.effectType = EffectType.Null;
				this.effectColor = Color.black;
				this.effectDistance = Vector2.zero;
				this.lineAlignment = LineAlignment.Default;
			}

			// Token: 0x060082DE RID: 33502 RVA: 0x002DCDDC File Offset: 0x002DAFDC
			public void Set(TextParser.Config c)
			{
				this.anchor = c.anchor;
				this.font = c.font;
				this.fontStyle = c.fontStyle;
				this.fontSize = c.fontSize;
				this.fontColor = c.fontColor;
				this.isUnderline = c.isUnderline;
				this.isStrickout = c.isStrickout;
				this.isBlink = c.isBlink;
				this.dyncSpeed = c.dyncSpeed;
				this.isOffset = c.isOffset;
				this.offsetRect = c.offsetRect;
				this.effectType = c.effectType;
				this.effectColor = c.effectColor;
				this.effectDistance = c.effectDistance;
				this.isDyncUnderline = c.isDyncUnderline;
				this.isDyncStrickout = c.isDyncStrickout;
				this.lineAlignment = c.lineAlignment;
			}

			// Token: 0x060082DF RID: 33503 RVA: 0x002DCEB8 File Offset: 0x002DB0B8
			public bool isSame(TextParser.Config c)
			{
				return this.anchor == c.anchor && this.font == c.font && this.fontStyle == c.fontStyle && this.isUnderline == c.isUnderline && this.fontColor == c.fontColor && this.isStrickout == c.isStrickout && this.isBlink == c.isBlink && this.fontSize == c.fontSize && this.lineAlignment == c.lineAlignment && this.isDyncUnderline == c.isDyncUnderline && this.isDyncStrickout == c.isDyncStrickout && this.dyncSpeed == c.dyncSpeed && ((this.effectType == EffectType.Null && c.effectType == EffectType.Null) || (this.effectType == c.effectType && this.effectColor == c.effectColor && this.effectDistance == c.effectDistance)) && ((!this.isOffset && !c.isOffset) || (this.isOffset == c.isOffset && this.offsetRect == c.offsetRect));
			}

			// Token: 0x04006E2E RID: 28206
			public Anchor anchor;

			// Token: 0x04006E2F RID: 28207
			public Font font;

			// Token: 0x04006E30 RID: 28208
			public FontStyle fontStyle;

			// Token: 0x04006E31 RID: 28209
			public int fontSize;

			// Token: 0x04006E32 RID: 28210
			public Color fontColor;

			// Token: 0x04006E33 RID: 28211
			public bool isUnderline;

			// Token: 0x04006E34 RID: 28212
			public bool isStrickout;

			// Token: 0x04006E35 RID: 28213
			public bool isBlink;

			// Token: 0x04006E36 RID: 28214
			public bool isDyncUnderline;

			// Token: 0x04006E37 RID: 28215
			public bool isDyncStrickout;

			// Token: 0x04006E38 RID: 28216
			public int dyncSpeed;

			// Token: 0x04006E39 RID: 28217
			public bool isOffset;

			// Token: 0x04006E3A RID: 28218
			public Rect offsetRect;

			// Token: 0x04006E3B RID: 28219
			public EffectType effectType;

			// Token: 0x04006E3C RID: 28220
			public Color effectColor;

			// Token: 0x04006E3D RID: 28221
			public Vector2 effectDistance;

			// Token: 0x04006E3E RID: 28222
			public LineAlignment lineAlignment;
		}

		// Token: 0x0200150E RID: 5390
		// (Invoke) Token: 0x060082E1 RID: 33505
		private delegate void OnFun(string c);

		// Token: 0x0200150F RID: 5391
		private class HyConfig
		{
			// Token: 0x060082E4 RID: 33508 RVA: 0x002DD014 File Offset: 0x002DB214
			public HyConfig(TextParser p)
			{
				this.parser = p;
				this.OnFunHys = new TextParser.HyConfig.OnFunHy[128];
				this.OnFunHys[82] = new TextParser.HyConfig.OnFunHy(this.ParserSureColor);
				this.OnFunHys[71] = new TextParser.HyConfig.OnFunHy(this.ParserSureColor);
				this.OnFunHys[66] = new TextParser.HyConfig.OnFunHy(this.ParserSureColor);
				this.OnFunHys[75] = new TextParser.HyConfig.OnFunHy(this.ParserSureColor);
				this.OnFunHys[89] = new TextParser.HyConfig.OnFunHy(this.ParserSureColor);
				this.OnFunHys[87] = new TextParser.HyConfig.OnFunHy(this.ParserSureColor);
				this.OnFunHys[80] = new TextParser.HyConfig.OnFunHy(this.ParserSureColor);
				this.OnFunHys[99] = new TextParser.HyConfig.OnFunHy(this.ParserFontColor);
				this.OnFunHys[110] = new TextParser.HyConfig.OnFunHy(this.ParserRestore);
				this.OnFunHys[115] = new TextParser.HyConfig.OnFunHy(this.ParserFontSize);
				this.OnFunHys[102] = new TextParser.HyConfig.OnFunHy(this.ParserFont);
				this.OnFunHys[35] = new TextParser.HyConfig.OnFunHy(this.ParserOutputChar);
				this.OnFunHys[117] = delegate(string text)
				{
					this.node.d_bUnderline = !this.node.d_bUnderline;
					this.startPos++;
				};
				this.OnFunHys[101] = delegate(string text)
				{
					this.node.d_bStrickout = !this.node.d_bStrickout;
					this.startPos++;
				};
			}

			// Token: 0x060082E5 RID: 33509 RVA: 0x002DD16F File Offset: 0x002DB36F
			private void ParserOutputChar(string text)
			{
				this.sb.Append("#");
				this.startPos++;
			}

			// Token: 0x060082E6 RID: 33510 RVA: 0x002DD190 File Offset: 0x002DB390
			private void ParserSureColor(string text)
			{
				this.node.d_color = TextParser.GetColour((uint)text[this.startPos]);
				this.startPos++;
			}

			// Token: 0x060082E7 RID: 33511 RVA: 0x002DD1BC File Offset: 0x002DB3BC
			private void ParserFontColor(string text)
			{
				this.node.d_color = Tools.ParserColorName(text, ref this.startPos, this.node.d_color);
			}

			// Token: 0x060082E8 RID: 33512 RVA: 0x002DD1E0 File Offset: 0x002DB3E0
			private void ParserRestore(string text)
			{
				this.node.SetConfig(this.parser.startConfig);
			}

			// Token: 0x060082E9 RID: 33513 RVA: 0x002DD1F8 File Offset: 0x002DB3F8
			private void ParserFontSize(string text)
			{
				float num = 1f;
				if (!TextParser.ParserFloat(ref this.startPos, text, ref num, 3))
				{
					return;
				}
				this.node.d_fontSize = (int)num;
			}

			// Token: 0x060082EA RID: 33514 RVA: 0x002DD22C File Offset: 0x002DB42C
			private void ParserFont(string text)
			{
				this.startPos++;
				Font font = Tools.ParserFontName(text, ref this.startPos);
				if (font != null)
				{
					this.node.d_font = font;
					return;
				}
				this.startPos--;
			}

			// Token: 0x060082EB RID: 33515 RVA: 0x002DD278 File Offset: 0x002DB478
			public void Clear()
			{
				this.text = null;
				this.node = null;
				this.sb = null;
				this.startPos = 0;
			}

			// Token: 0x060082EC RID: 33516 RVA: 0x002DD298 File Offset: 0x002DB498
			public void BeginParser(StringBuilder s)
			{
				this.sb = s;
				bool flag = false;
				while (this.lenght > this.startPos)
				{
					if (!flag)
					{
						if (this.text[this.startPos] == '#')
						{
							flag = true;
							this.startPos++;
						}
						else
						{
							this.sb.Append(this.text[this.startPos]);
							this.startPos++;
						}
					}
					else
					{
						char c = this.text[this.startPos];
						TextParser.HyConfig.OnFunHy onFunHy;
						if (c < '\u0080' && (onFunHy = this.OnFunHys[(int)c]) != null)
						{
							onFunHy(this.text);
						}
						else
						{
							this.sb.Append(this.text[this.startPos]);
							this.startPos++;
						}
						flag = false;
					}
				}
				this.node.d_text = this.sb.ToString();
			}

			// Token: 0x04006E3F RID: 28223
			public string text = "";

			// Token: 0x04006E40 RID: 28224
			public HyperlinkNode node;

			// Token: 0x04006E41 RID: 28225
			public int startPos;

			// Token: 0x04006E42 RID: 28226
			public int lenght;

			// Token: 0x04006E43 RID: 28227
			private StringBuilder sb;

			// Token: 0x04006E44 RID: 28228
			public TextParser parser;

			// Token: 0x04006E45 RID: 28229
			private TextParser.HyConfig.OnFunHy[] OnFunHys;

			// Token: 0x0200175A RID: 5978
			// (Invoke) Token: 0x06008992 RID: 35218
			private delegate void OnFunHy(string text);
		}
	}
}
