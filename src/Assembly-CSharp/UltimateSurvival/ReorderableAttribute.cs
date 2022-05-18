using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x02000867 RID: 2151
	[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
	public class ReorderableAttribute : PropertyAttribute
	{
		// Token: 0x04003200 RID: 12800
		private RangeAttribute g;
	}
}
