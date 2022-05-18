using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200139B RID: 5019
	public abstract class LuaBindingsBase : MonoBehaviour
	{
		// Token: 0x06007988 RID: 31112
		public abstract void AddBindings(LuaEnvironment luaEnv);

		// Token: 0x17000B76 RID: 2934
		// (get) Token: 0x06007989 RID: 31113
		public abstract List<BoundObject> BoundObjects { get; }
	}
}
