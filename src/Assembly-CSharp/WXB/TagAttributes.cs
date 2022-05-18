using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace WXB
{
	// Token: 0x020009B7 RID: 2487
	public class TagAttributes
	{
		// Token: 0x06003F45 RID: 16197 RVA: 0x001B8E80 File Offset: 0x001B7080
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

		// Token: 0x06003F46 RID: 16198 RVA: 0x0002D785 File Offset: 0x0002B985
		public void Release()
		{
			this.d_attrs.Clear();
		}

		// Token: 0x06003F47 RID: 16199 RVA: 0x001B8F54 File Offset: 0x001B7154
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

		// Token: 0x06003F48 RID: 16200 RVA: 0x0002D792 File Offset: 0x0002B992
		public void add(string attrName, string attrValue)
		{
			this.d_attrs[attrName] = attrValue;
		}

		// Token: 0x06003F49 RID: 16201 RVA: 0x0002D7A1 File Offset: 0x0002B9A1
		public void remove(ref string attrName)
		{
			this.d_attrs.Remove(attrName);
		}

		// Token: 0x06003F4A RID: 16202 RVA: 0x0002D7B1 File Offset: 0x0002B9B1
		public bool exists(string attrName)
		{
			return this.d_attrs.ContainsKey(attrName);
		}

		// Token: 0x06003F4B RID: 16203 RVA: 0x0002D7BF File Offset: 0x0002B9BF
		public int getCount()
		{
			return this.d_attrs.Count;
		}

		// Token: 0x06003F4C RID: 16204 RVA: 0x001B9004 File Offset: 0x001B7204
		public string getValue(string attrName)
		{
			string result = "";
			this.d_attrs.TryGetValue(attrName, out result);
			return result;
		}

		// Token: 0x06003F4D RID: 16205 RVA: 0x001B9028 File Offset: 0x001B7228
		public string getValueAsString(string attrName)
		{
			string result = "";
			if (!this.d_attrs.TryGetValue(attrName, out result))
			{
				return "";
			}
			return result;
		}

		// Token: 0x06003F4E RID: 16206 RVA: 0x001B9054 File Offset: 0x001B7254
		public Color getValueAsColor(string attrName, Color color)
		{
			string text = "";
			if (!this.d_attrs.TryGetValue(attrName, out text))
			{
				return color;
			}
			return Tools.ParseColor(text, 0, Color.white);
		}

		// Token: 0x06003F4F RID: 16207 RVA: 0x001B9088 File Offset: 0x001B7288
		public string getValueAsString(string attrName, string def)
		{
			string result = "";
			if (!this.d_attrs.TryGetValue(attrName, out result))
			{
				return def;
			}
			return result;
		}

		// Token: 0x06003F50 RID: 16208 RVA: 0x001B90B0 File Offset: 0x001B72B0
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

		// Token: 0x06003F51 RID: 16209 RVA: 0x001B90F8 File Offset: 0x001B72F8
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

		// Token: 0x06003F52 RID: 16210 RVA: 0x001B912C File Offset: 0x001B732C
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

		// Token: 0x040038C7 RID: 14535
		private Dictionary<string, string> d_attrs = new Dictionary<string, string>();
	}
}
