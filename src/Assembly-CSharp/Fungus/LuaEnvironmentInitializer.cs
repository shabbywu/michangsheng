using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013A1 RID: 5025
	public abstract class LuaEnvironmentInitializer : MonoBehaviour
	{
		// Token: 0x060079B0 RID: 31152
		public abstract void Initialize();

		// Token: 0x060079B1 RID: 31153
		public abstract string PreprocessScript(string input);
	}
}
