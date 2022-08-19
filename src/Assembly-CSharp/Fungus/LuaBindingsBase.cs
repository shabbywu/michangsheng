using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000EF9 RID: 3833
	public abstract class LuaBindingsBase : MonoBehaviour
	{
		// Token: 0x06006BE3 RID: 27619
		public abstract void AddBindings(LuaEnvironment luaEnv);

		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x06006BE4 RID: 27620
		public abstract List<BoundObject> BoundObjects { get; }
	}
}
