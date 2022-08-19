using System;
using UnityEngine.Events;

// Token: 0x02000449 RID: 1097
public interface ISelectNum
{
	// Token: 0x060022B0 RID: 8880
	void Init(string itemName, int maxNum, UnityAction Ok = null, UnityAction Cancel = null);

	// Token: 0x060022B1 RID: 8881
	void AddNum();

	// Token: 0x060022B2 RID: 8882
	void ReduceNum();
}
