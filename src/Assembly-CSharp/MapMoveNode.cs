using System;
using UnityEngine;

// Token: 0x02000190 RID: 400
public class MapMoveNode : MonoBehaviour
{
	// Token: 0x0600112A RID: 4394 RVA: 0x00067530 File Offset: 0x00065730
	private void Start()
	{
		string name = base.transform.parent.name;
		int num = name.IndexOf("_");
		string s = name.Substring(0, num);
		string s2 = name.Substring(num + 1, name.Length - num - 1);
		int.TryParse(s, out this.StartNode);
		int.TryParse(s2, out this.EndNode);
	}

	// Token: 0x0600112B RID: 4395 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}

	// Token: 0x04000C47 RID: 3143
	public int StartNode;

	// Token: 0x04000C48 RID: 3144
	public int EndNode;
}
