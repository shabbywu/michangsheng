using System;
using System.Collections.Generic;

// Token: 0x02000581 RID: 1409
[Serializable]
public class faceInfoDataBaseList
{
	// Token: 0x04001EB8 RID: 7864
	public string Name;

	// Token: 0x04001EB9 RID: 7865
	public string JsonInfoName;

	// Token: 0x04001EBA RID: 7866
	public List<faceInfoDataBase> faceList = new List<faceInfoDataBase>();
}
