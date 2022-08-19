using System;
using UnityEngine;

// Token: 0x02000106 RID: 262
[Serializable]
public class LevelItemsConfiguration
{
	// Token: 0x04000854 RID: 2132
	public string levelName;

	// Token: 0x04000855 RID: 2133
	public bool hasSubLevel;

	// Token: 0x04000856 RID: 2134
	public string subLevelName;

	// Token: 0x04000857 RID: 2135
	public bool isLocked;

	// Token: 0x04000858 RID: 2136
	public string levelToLoad;

	// Token: 0x04000859 RID: 2137
	public Sprite levelImage;

	// Token: 0x0400085A RID: 2138
	public int PlayerID;
}
