using System;
using System.Collections.Generic;
using System.Reflection;

namespace KBEngine
{
	// Token: 0x02000FEA RID: 4074
	public class ScriptModule
	{
		// Token: 0x0600606D RID: 24685 RVA: 0x00268CC8 File Offset: 0x00266EC8
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

		// Token: 0x04005BB2 RID: 23474
		public string name;

		// Token: 0x04005BB3 RID: 23475
		public bool usePropertyDescrAlias;

		// Token: 0x04005BB4 RID: 23476
		public bool useMethodDescrAlias;

		// Token: 0x04005BB5 RID: 23477
		public Dictionary<string, Property> propertys = new Dictionary<string, Property>();

		// Token: 0x04005BB6 RID: 23478
		public Dictionary<ushort, Property> idpropertys = new Dictionary<ushort, Property>();

		// Token: 0x04005BB7 RID: 23479
		public Dictionary<string, Method> methods = new Dictionary<string, Method>();

		// Token: 0x04005BB8 RID: 23480
		public Dictionary<string, Method> base_methods = new Dictionary<string, Method>();

		// Token: 0x04005BB9 RID: 23481
		public Dictionary<string, Method> cell_methods = new Dictionary<string, Method>();

		// Token: 0x04005BBA RID: 23482
		public Dictionary<ushort, Method> idmethods = new Dictionary<ushort, Method>();

		// Token: 0x04005BBB RID: 23483
		public Dictionary<ushort, Method> idbase_methods = new Dictionary<ushort, Method>();

		// Token: 0x04005BBC RID: 23484
		public Dictionary<ushort, Method> idcell_methods = new Dictionary<ushort, Method>();

		// Token: 0x04005BBD RID: 23485
		public Type entityScript;
	}
}
