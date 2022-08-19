using System;
using UnityEngine;

// Token: 0x020002BB RID: 699
public class GameVersion : MonoBehaviour
{
	// Token: 0x06001899 RID: 6297 RVA: 0x000B08DD File Offset: 0x000AEADD
	private void Awake()
	{
		GameVersion.inst = this;
	}

	// Token: 0x0600189A RID: 6298 RVA: 0x000B08E5 File Offset: 0x000AEAE5
	private void Start()
	{
		this.realTest = false;
	}

	// Token: 0x0600189B RID: 6299 RVA: 0x000B08EE File Offset: 0x000AEAEE
	public int GetGameVersion()
	{
		return this.gameVersion;
	}

	// Token: 0x040013AA RID: 5034
	[SerializeField]
	private int gameVersion;

	// Token: 0x040013AB RID: 5035
	public static GameVersion inst;

	// Token: 0x040013AC RID: 5036
	public bool realTest;
}
