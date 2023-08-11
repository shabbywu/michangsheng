using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Obsolete]
public class UI_SelectAvatar : MonoBehaviour
{
	public Button bt_createAvatar;

	public Button bt_removeAvatar;

	public Toggle[] tg_avatars = (Toggle[])(object)new Toggle[3];

	public int nowAvater;

	public int nowAvaterSurface = 1;

	public GameObject selectAvaterUI;

	public GameObject selectAvaterUITemple;

	public GameObject selectAvaterSurfaceUI;

	public GameObject selectAvaterSurfaceUITemple;

	private Dictionary<ulong, Dictionary<string, object>> ui_avatarList;

	private Dictionary<string, ulong> dic_name_to_dbid = new Dictionary<string, ulong>();
}
