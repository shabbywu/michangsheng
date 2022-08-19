using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000EFC RID: 3836
	public abstract class LuaEnvironmentInitializer : MonoBehaviour
	{
		// Token: 0x06006BFC RID: 27644
		public abstract void Initialize();

		// Token: 0x06006BFD RID: 27645
		public abstract string PreprocessScript(string input);
	}
}
