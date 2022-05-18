using System;
using UnityEngine;

// Token: 0x020003F7 RID: 1015
public class GameVersion : MonoBehaviour
{
	// Token: 0x06001B8D RID: 7053 RVA: 0x00017273 File Offset: 0x00015473
	private void Awake()
	{
		GameVersion.inst = this;
	}

	// Token: 0x06001B8E RID: 7054 RVA: 0x0001727B File Offset: 0x0001547B
	private void Start()
	{
		this.realTest = false;
	}

	// Token: 0x06001B8F RID: 7055 RVA: 0x00017284 File Offset: 0x00015484
	public int GetGameVersion()
	{
		return this.gameVersion;
	}

	// Token: 0x0400174D RID: 5965
	[SerializeField]
	private int gameVersion;

	// Token: 0x0400174E RID: 5966
	public static GameVersion inst;

	// Token: 0x0400174F RID: 5967
	public bool realTest;
}
