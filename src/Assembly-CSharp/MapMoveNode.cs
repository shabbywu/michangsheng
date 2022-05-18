using System;
using UnityEngine;

// Token: 0x02000280 RID: 640
public class MapMoveNode : MonoBehaviour
{
	// Token: 0x060013AD RID: 5037 RVA: 0x000B5B54 File Offset: 0x000B3D54
	private void Start()
	{
		string name = base.transform.parent.name;
		int num = name.IndexOf("_");
		string s = name.Substring(0, num);
		string s2 = name.Substring(num + 1, name.Length - num - 1);
		int.TryParse(s, out this.StartNode);
		int.TryParse(s2, out this.EndNode);
	}

	// Token: 0x060013AE RID: 5038 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x04000F47 RID: 3911
	public int StartNode;

	// Token: 0x04000F48 RID: 3912
	public int EndNode;
}
