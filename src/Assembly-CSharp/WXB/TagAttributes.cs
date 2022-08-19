using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace WXB
{
	// Token: 0x020006A2 RID: 1698
	public class TagAttributes
	{
		// Token: 0x06003584 RID: 13700 RVA: 0x00170EBC File Offset: 0x0016F0BC
		public override string ToString()
		{
			string result;
			using (PD<StringBuilder> sb = Pool.GetSB())
			{
				StringBuilder value = sb.value;
				value.AppendFormat("count:{0}", this.d_attrs.Count);
				value.AppendLine();
				foreach (KeyValuePair<string, string> keyValuePair in this.d_attrs)
				{
					value.AppendLine("key:{0} value:{1}", new string[]
					{
						keyValuePair.Key,
						keyValuePair.Value
					});
				}
				string text = value.ToString();
				value.Length = 0;
				result = text;
			}
			return result;
		}

		// Token: 0x06003585 RID: 13701 RVA: 0x00170F90 File Offset: 0x0016F190
		public void Release()
		{
			this.d_attrs.Clear();
		}

		// Token: 0x06003586 RID: 13702 RVA: 0x00170FA0 File Offset: 0x0016F1A0
		public void Add(string text)
		{
			int i = 0;
			int length = text.Length;
			string key = string.Empty;
			string value = string.Empty;
			int num = 0;
			bool flag = false;
			while (i < length)
			{
				if (text[i] == '=')
				{
					key = text.Substring(num, i - num);
					num = i + 1;
					flag = true;
				}
				else if (text[i] == ' ')
				{
					if (!flag)
					{
						Debug.LogErrorFormat("error param!", Array.Empty<object>());
						return;
					}
					value = text.Substring(num, i - num);
					num = i + 1;
					this.d_attrs[key] = value;
					flag = false;
				}
				i++;
			}
			if (flag)
			{
				value = text.Substring(num);
				this.d_attrs[key] = value;
			}
		}

		// Token: 0x06003587 RID: 13703 RVA: 0x0017104F File Offset: 0x0016F24F
		public void add(string attrName, string attrValue)
		{
			this.d_attrs[attrName] = attrValue;
		}

		// Token: 0x06003588 RID: 13704 RVA: 0x0017105E File Offset: 0x0016F25E
		public void remove(ref string attrName)
		{
			this.d_attrs.Remove(attrName);
		}

		// Token: 0x06003589 RID: 13705 RVA: 0x0017106E File Offset: 0x0016F26E
		public bool exists(string attrName)
		{
			return this.d_attrs.ContainsKey(attrName);
		}

		// Token: 0x0600358A RID: 13706 RVA: 0x0017107C File Offset: 0x0016F27C
		public int getCount()
		{
			return this.d_attrs.Count;
		}

		// Token: 0x0600358B RID: 13707 RVA: 0x0017108C File Offset: 0x0016F28C
		public string getValue(string attrName)
		{
			string result = "";
			this.d_attrs.TryGetValue(attrName, out result);
			return result;
		}

		// Token: 0x0600358C RID: 13708 RVA: 0x001710B0 File Offset: 0x0016F2B0
		public string getValueAsString(string attrName)
		{
			string result = "";
			if (!this.d_attrs.TryGetValue(attrName, out result))
			{
				return "";
			}
			return result;
		}

		// Token: 0x0600358D RID: 13709 RVA: 0x001710DC File Offset: 0x0016F2DC
		public Color getValueAsColor(string attrName, Color color)
		{
			string text = "";
			if (!this.d_attrs.TryGetValue(attrName, out text))
			{
				return color;
			}
			return Tools.ParseColor(text, 0, Color.white);
		}

		// Token: 0x0600358E RID: 13710 RVA: 0x00171110 File Offset: 0x0016F310
		public string getValueAsString(string attrName, string def)
		{
			string result = "";
			if (!this.d_attrs.TryGetValue(attrName, out result))
			{
				return def;
			}
			return result;
		}

		// Token: 0x0600358F RID: 13711 RVA: 0x00171138 File Offset: 0x0016F338
		public bool getValueAsBool(string attrName, bool def)
		{
			string text = "";
			if (!this.d_attrs.TryGetValue(attrName, out text))
			{
				return def;
			}
			bool result = def;
			if (bool.TryParse(text, out result))
			{
				return result;
			}
			int result2 = 0;
			if (int.TryParse(text, out result2))
			{
				return result2 != 0;
			}
			return def;
		}

		// Token: 0x06003590 RID: 13712 RVA: 0x00171180 File Offset: 0x0016F380
		public int getValueAsInteger(string attrName, int def)
		{
			string s = "";
			if (!this.d_attrs.TryGetValue(attrName, out s))
			{
				return def;
			}
			int result = def;
			if (!int.TryParse(s, out result))
			{
				return def;
			}
			return result;
		}

		// Token: 0x06003591 RID: 13713 RVA: 0x001711B4 File Offset: 0x0016F3B4
		public float getValueAsFloat(string attrName, float def)
		{
			string s = "";
			if (!this.d_attrs.TryGetValue(attrName, out s))
			{
				return def;
			}
			float result = def;
			if (!float.TryParse(s, out result))
			{
				return def;
			}
			return result;
		}

		// Token: 0x04002F0C RID: 12044
		private Dictionary<string, string> d_attrs = new Dictionary<string, string>();
	}
}
