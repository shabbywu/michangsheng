using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000411 RID: 1041
[Obsolete]
public class UI_SelectAvatar : MonoBehaviour
{
	// Token: 0x04001B14 RID: 6932
	public Button bt_createAvatar;

	// Token: 0x04001B15 RID: 6933
	public Button bt_removeAvatar;

	// Token: 0x04001B16 RID: 6934
	public Toggle[] tg_avatars = new Toggle[3];

	// Token: 0x04001B17 RID: 6935
	public int nowAvater;

	// Token: 0x04001B18 RID: 6936
	public int nowAvaterSurface = 1;

	// Token: 0x04001B19 RID: 6937
	public GameObject selectAvaterUI;

	// Token: 0x04001B1A RID: 6938
	public GameObject selectAvaterUITemple;

	// Token: 0x04001B1B RID: 6939
	public GameObject selectAvaterSurfaceUI;

	// Token: 0x04001B1C RID: 6940
	public GameObject selectAvaterSurfaceUITemple;

	// Token: 0x04001B1D RID: 6941
	private Dictionary<ulong, Dictionary<string, object>> ui_avatarList;

	// Token: 0x04001B1E RID: 6942
	private Dictionary<string, ulong> dic_name_to_dbid = new Dictionary<string, ulong>();
}
