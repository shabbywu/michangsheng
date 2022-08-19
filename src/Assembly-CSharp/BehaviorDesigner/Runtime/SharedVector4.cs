using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x02000FCA RID: 4042
	[Serializable]
	public class SharedVector4 : SharedVariable<Vector4>
	{
		// Token: 0x06007027 RID: 28711 RVA: 0x002A8DDB File Offset: 0x002A6FDB
		public static implicit operator SharedVector4(Vector4 value)
		{
			return new SharedVector4
			{
				mValue = value
			};
		}
	}
}
