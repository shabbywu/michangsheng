using System;

// Token: 0x020005F7 RID: 1527
[AttributeUsage(AttributeTargets.Class)]
public class BindPrefab : Attribute
{
	// Token: 0x170002D3 RID: 723
	// (get) Token: 0x06002652 RID: 9810 RVA: 0x0001E8B0 File Offset: 0x0001CAB0
	// (set) Token: 0x06002653 RID: 9811 RVA: 0x0001E8B8 File Offset: 0x0001CAB8
	public string Path { get; private set; }

	// Token: 0x06002654 RID: 9812 RVA: 0x0001E8C1 File Offset: 0x0001CAC1
	public BindPrefab(string path)
	{
		this.Path = path;
	}
}
