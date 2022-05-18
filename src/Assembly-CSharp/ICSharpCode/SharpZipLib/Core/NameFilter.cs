using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ICSharpCode.SharpZipLib.Core
{
	// Token: 0x0200082E RID: 2094
	public class NameFilter : IScanFilter
	{
		// Token: 0x060036D5 RID: 14037 RVA: 0x00027DE4 File Offset: 0x00025FE4
		public NameFilter(string filter)
		{
			this.filter_ = filter;
			this.inclusions_ = new List<Regex>();
			this.exclusions_ = new List<Regex>();
			this.Compile();
		}

		// Token: 0x060036D6 RID: 14038 RVA: 0x0019C418 File Offset: 0x0019A618
		public static bool IsValidExpression(string expression)
		{
			bool result = true;
			try
			{
				new Regex(expression, RegexOptions.IgnoreCase | RegexOptions.Singleline);
			}
			catch (ArgumentException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060036D7 RID: 14039 RVA: 0x0019C448 File Offset: 0x0019A648
		public static bool IsValidFilterExpression(string toTest)
		{
			bool result = true;
			try
			{
				if (toTest != null)
				{
					string[] array = NameFilter.SplitQuoted(toTest);
					for (int i = 0; i < array.Length; i++)
					{
						if (array[i] != null && array[i].Length > 0)
						{
							string pattern;
							if (array[i][0] == '+')
							{
								pattern = array[i].Substring(1, array[i].Length - 1);
							}
							else if (array[i][0] == '-')
							{
								pattern = array[i].Substring(1, array[i].Length - 1);
							}
							else
							{
								pattern = array[i];
							}
							new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);
						}
					}
				}
			}
			catch (ArgumentException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060036D8 RID: 14040 RVA: 0x0019C4EC File Offset: 0x0019A6EC
		public static string[] SplitQuoted(string original)
		{
			char c = '\\';
			char[] array = new char[]
			{
				';'
			};
			List<string> list = new List<string>();
			if (!string.IsNullOrEmpty(original))
			{
				int i = -1;
				StringBuilder stringBuilder = new StringBuilder();
				while (i < original.Length)
				{
					i++;
					if (i >= original.Length)
					{
						list.Add(stringBuilder.ToString());
					}
					else if (original[i] == c)
					{
						i++;
						if (i >= original.Length)
						{
							throw new ArgumentException("Missing terminating escape character", "original");
						}
						if (Array.IndexOf<char>(array, original[i]) < 0)
						{
							stringBuilder.Append(c);
						}
						stringBuilder.Append(original[i]);
					}
					else if (Array.IndexOf<char>(array, original[i]) >= 0)
					{
						list.Add(stringBuilder.ToString());
						stringBuilder.Length = 0;
					}
					else
					{
						stringBuilder.Append(original[i]);
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x060036D9 RID: 14041 RVA: 0x00027E0F File Offset: 0x0002600F
		public override string ToString()
		{
			return this.filter_;
		}

		// Token: 0x060036DA RID: 14042 RVA: 0x0019C5E0 File Offset: 0x0019A7E0
		public bool IsIncluded(string name)
		{
			bool result = false;
			if (this.inclusions_.Count == 0)
			{
				result = true;
			}
			else
			{
				using (List<Regex>.Enumerator enumerator = this.inclusions_.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.IsMatch(name))
						{
							result = true;
							break;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060036DB RID: 14043 RVA: 0x0019C64C File Offset: 0x0019A84C
		public bool IsExcluded(string name)
		{
			bool result = false;
			using (List<Regex>.Enumerator enumerator = this.exclusions_.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsMatch(name))
					{
						result = true;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x060036DC RID: 14044 RVA: 0x00027E17 File Offset: 0x00026017
		public bool IsMatch(string name)
		{
			return this.IsIncluded(name) && !this.IsExcluded(name);
		}

		// Token: 0x060036DD RID: 14045 RVA: 0x0019C6A8 File Offset: 0x0019A8A8
		private void Compile()
		{
			if (this.filter_ == null)
			{
				return;
			}
			string[] array = NameFilter.SplitQuoted(this.filter_);
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != null && array[i].Length > 0)
				{
					bool flag = array[i][0] != '-';
					string pattern;
					if (array[i][0] == '+')
					{
						pattern = array[i].Substring(1, array[i].Length - 1);
					}
					else if (array[i][0] == '-')
					{
						pattern = array[i].Substring(1, array[i].Length - 1);
					}
					else
					{
						pattern = array[i];
					}
					if (flag)
					{
						this.inclusions_.Add(new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline));
					}
					else
					{
						this.exclusions_.Add(new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline));
					}
				}
			}
		}

		// Token: 0x0400311F RID: 12575
		private string filter_;

		// Token: 0x04003120 RID: 12576
		private List<Regex> inclusions_;

		// Token: 0x04003121 RID: 12577
		private List<Regex> exclusions_;
	}
}
