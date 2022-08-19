using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ICSharpCode.SharpZipLib.Core
{
	// Token: 0x02000585 RID: 1413
	public class NameFilter : IScanFilter
	{
		// Token: 0x06002E5F RID: 11871 RVA: 0x00151585 File Offset: 0x0014F785
		public NameFilter(string filter)
		{
			this.filter_ = filter;
			this.inclusions_ = new List<Regex>();
			this.exclusions_ = new List<Regex>();
			this.Compile();
		}

		// Token: 0x06002E60 RID: 11872 RVA: 0x001515B0 File Offset: 0x0014F7B0
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

		// Token: 0x06002E61 RID: 11873 RVA: 0x001515E0 File Offset: 0x0014F7E0
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

		// Token: 0x06002E62 RID: 11874 RVA: 0x00151684 File Offset: 0x0014F884
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

		// Token: 0x06002E63 RID: 11875 RVA: 0x00151777 File Offset: 0x0014F977
		public override string ToString()
		{
			return this.filter_;
		}

		// Token: 0x06002E64 RID: 11876 RVA: 0x00151780 File Offset: 0x0014F980
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

		// Token: 0x06002E65 RID: 11877 RVA: 0x001517EC File Offset: 0x0014F9EC
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

		// Token: 0x06002E66 RID: 11878 RVA: 0x00151848 File Offset: 0x0014FA48
		public bool IsMatch(string name)
		{
			return this.IsIncluded(name) && !this.IsExcluded(name);
		}

		// Token: 0x06002E67 RID: 11879 RVA: 0x00151860 File Offset: 0x0014FA60
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

		// Token: 0x040028E0 RID: 10464
		private string filter_;

		// Token: 0x040028E1 RID: 10465
		private List<Regex> inclusions_;

		// Token: 0x040028E2 RID: 10466
		private List<Regex> exclusions_;
	}
}
