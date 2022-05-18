using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020005C3 RID: 1475
[Obsolete]
public class UI_SelectAvatar : MonoBehaviour
{
	// Token: 0x04001FD3 RID: 8147
	public Button bt_createAvatar;

	// Token: 0x04001FD4 RID: 8148
	public Button bt_removeAvatar;

	// Token: 0x04001FD5 RID: 8149
	public Toggle[] tg_avatars = new Toggle[3];

	// Token: 0x04001FD6 RID: 8150
	public int nowAvater;

	// Token: 0x04001FD7 RID: 8151
	public int nowAvaterSurface = 1;

	// Token: 0x04001FD8 RID: 8152
	public GameObject selectAvaterUI;

	// Token: 0x04001FD9 RID: 8153
	public GameObject selectAvaterUITemple;

	// Token: 0x04001FDA RID: 8154
	public GameObject selectAvaterSurfaceUI;

	// Token: 0x04001FDB RID: 8155
	public GameObject selectAvaterSurfaceUITemple;

	// Token: 0x04001FDC RID: 8156
	private Dictionary<ulong, Dictionary<string, object>> ui_avatarList;

	// Token: 0x04001FDD RID: 8157
	private Dictionary<string, ulong> dic_name_to_dbid = new Dictionary<string, ulong>();
}
