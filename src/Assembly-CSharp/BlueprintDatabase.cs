using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001F7 RID: 503
public class BlueprintDatabase : ScriptableObject
{
	// Token: 0x04000C86 RID: 3206
	[SerializeField]
	public List<Blueprint> blueprints = new List<Blueprint>();
}
