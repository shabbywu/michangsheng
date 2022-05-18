using System;
using UnityEngine.Events;

// Token: 0x02000600 RID: 1536
public interface ISelectNum
{
	// Token: 0x0600266D RID: 9837
	void Init(string itemName, int maxNum, UnityAction Ok = null, UnityAction Cancel = null);

	// Token: 0x0600266E RID: 9838
	void AddNum();

	// Token: 0x0600266F RID: 9839
	void ReduceNum();
}
