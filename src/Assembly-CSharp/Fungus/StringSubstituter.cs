using System;
using System.Collections.Generic;
using System.Text;

namespace Fungus
{
	// Token: 0x020013AC RID: 5036
	public class StringSubstituter : IStringSubstituter
	{
		// Token: 0x060079FA RID: 31226 RVA: 0x00053351 File Offset: 0x00051551
		public static void RegisterHandler(ISubstitutionHandler handler)
		{
			if (!StringSubstituter.substitutionHandlers.Contains(handler))
			{
				StringSubstituter.substitutionHandlers.Add(handler);
			}
		}

		// Token: 0x060079FB RID: 31227 RVA: 0x0005336B File Offset: 0x0005156B
		public static void UnregisterHandler(ISubstitutionHandler handler)
		{
			StringSubstituter.substitutionHandlers.Remove(handler);
		}

		// Token: 0x060079FC RID: 31228 RVA: 0x00053379 File Offset: 0x00051579
		public StringSubstituter(int recursionDepth = 5)
		{
			this.stringBuilder = new StringBuilder(1024);
			this.recursionDepth = recursionDepth;
		}

		// Token: 0x17000B8B RID: 2955
		// (get) Token: 0x060079FD RID: 31229 RVA: 0x00053398 File Offset: 0x00051598
		public virtual StringBuilder _StringBuilder
		{
			get
			{
				return this.stringBuilder;
			}
		}

		// Token: 0x060079FE RID: 31230 RVA: 0x000533A0 File Offset: 0x000515A0
		public virtual string SubstituteStrings(string input)
		{
			this.stringBuilder.Length = 0;
			this.stringBuilder.Append(input);
			if (this.SubstituteStrings(this.stringBuilder))
			{
				return this.stringBuilder.ToString();
			}
			return input;
		}

		// Token: 0x060079FF RID: 31231 RVA: 0x002B97F8 File Offset: 0x002B79F8
		public virtual bool SubstituteStrings(StringBuilder input)
		{
			bool result = false;
			for (int i = 0; i < this.recursionDepth; i++)
			{
				bool flag = false;
				using (List<ISubstitutionHandler>.Enumerator enumerator = StringSubstituter.substitutionHandlers.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.SubstituteStrings(input))
						{
							flag = true;
							result = true;
						}
					}
				}
				if (!flag)
				{
					break;
				}
			}
			return result;
		}

		// Token: 0x0400696D RID: 26989
		protected static List<ISubstitutionHandler> substitutionHandlers = new List<ISubstitutionHandler>();

		// Token: 0x0400696E RID: 26990
		protected StringBuilder stringBuilder;

		// Token: 0x0400696F RID: 26991
		protected int recursionDepth;
	}
}
