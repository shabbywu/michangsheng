using System;
using System.Collections.Generic;
using System.Text;

namespace Fungus
{
	// Token: 0x02000F07 RID: 3847
	public class StringSubstituter : IStringSubstituter
	{
		// Token: 0x06006C46 RID: 27718 RVA: 0x002986EA File Offset: 0x002968EA
		public static void RegisterHandler(ISubstitutionHandler handler)
		{
			if (!StringSubstituter.substitutionHandlers.Contains(handler))
			{
				StringSubstituter.substitutionHandlers.Add(handler);
			}
		}

		// Token: 0x06006C47 RID: 27719 RVA: 0x00298704 File Offset: 0x00296904
		public static void UnregisterHandler(ISubstitutionHandler handler)
		{
			StringSubstituter.substitutionHandlers.Remove(handler);
		}

		// Token: 0x06006C48 RID: 27720 RVA: 0x00298712 File Offset: 0x00296912
		public StringSubstituter(int recursionDepth = 5)
		{
			this.stringBuilder = new StringBuilder(1024);
			this.recursionDepth = recursionDepth;
		}

		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x06006C49 RID: 27721 RVA: 0x00298731 File Offset: 0x00296931
		public virtual StringBuilder _StringBuilder
		{
			get
			{
				return this.stringBuilder;
			}
		}

		// Token: 0x06006C4A RID: 27722 RVA: 0x00298739 File Offset: 0x00296939
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

		// Token: 0x06006C4B RID: 27723 RVA: 0x00298770 File Offset: 0x00296970
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

		// Token: 0x04005AF4 RID: 23284
		protected static List<ISubstitutionHandler> substitutionHandlers = new List<ISubstitutionHandler>();

		// Token: 0x04005AF5 RID: 23285
		protected StringBuilder stringBuilder;

		// Token: 0x04005AF6 RID: 23286
		protected int recursionDepth;
	}
}
