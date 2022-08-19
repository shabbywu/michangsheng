using System;
using System.Collections.Generic;
using System.Reflection;

namespace KBEngine
{
	// Token: 0x02000C5F RID: 3167
	public class ScriptModule
	{
		// Token: 0x0600561E RID: 22046 RVA: 0x0023C138 File Offset: 0x0023A338
		public ScriptModule(string modulename)
		{
			this.name = modulename;
			foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				this.entityScript = assembly.GetType("KBEngine." + modulename);
				if (this.entityScript == null)
				{
					this.entityScript = assembly.GetType(modulename);
				}
				if (this.entityScript != null)
				{
					break;
				}
			}
			this.usePropertyDescrAlias = false;
			this.useMethodDescrAlias = false;
			if (this.entityScript == null)
			{
				Dbg.ERROR_MSG("can't load(KBEngine." + modulename + ")!");
			}
		}

		// Token: 0x040050F8 RID: 20728
		public string name;

		// Token: 0x040050F9 RID: 20729
		public bool usePropertyDescrAlias;

		// Token: 0x040050FA RID: 20730
		public bool useMethodDescrAlias;

		// Token: 0x040050FB RID: 20731
		public Dictionary<string, Property> propertys = new Dictionary<string, Property>();

		// Token: 0x040050FC RID: 20732
		public Dictionary<ushort, Property> idpropertys = new Dictionary<ushort, Property>();

		// Token: 0x040050FD RID: 20733
		public Dictionary<string, Method> methods = new Dictionary<string, Method>();

		// Token: 0x040050FE RID: 20734
		public Dictionary<string, Method> base_methods = new Dictionary<string, Method>();

		// Token: 0x040050FF RID: 20735
		public Dictionary<string, Method> cell_methods = new Dictionary<string, Method>();

		// Token: 0x04005100 RID: 20736
		public Dictionary<ushort, Method> idmethods = new Dictionary<ushort, Method>();

		// Token: 0x04005101 RID: 20737
		public Dictionary<ushort, Method> idbase_methods = new Dictionary<ushort, Method>();

		// Token: 0x04005102 RID: 20738
		public Dictionary<ushort, Method> idcell_methods = new Dictionary<ushort, Method>();

		// Token: 0x04005103 RID: 20739
		public Type entityScript;
	}
}
