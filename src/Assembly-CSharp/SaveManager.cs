using System;
using UnityEngine;

// Token: 0x020004F2 RID: 1266
public class SaveManager : MonoBehaviour
{
	// Token: 0x060020F3 RID: 8435 RVA: 0x0001B282 File Offset: 0x00019482
	private void Awake()
	{
		SaveManager.inst = this;
	}

	// Token: 0x060020F4 RID: 8436 RVA: 0x0001B28A File Offset: 0x0001948A
	public void updateState()
	{
		if (this.save.value)
		{
			this.avatarInfo.showSave();
			return;
		}
		this.avatarInfo.showLoad();
	}

	// Token: 0x060020F5 RID: 8437 RVA: 0x0001B2B0 File Offset: 0x000194B0
	public void updateSaveDate()
	{
		if (this.savePanel.activeSelf)
		{
			if (this.save.value)
			{
				this.avatarInfo.showSave();
				return;
			}
			this.avatarInfo.showLoad();
		}
	}

	// Token: 0x04001C6C RID: 7276
	public GameObject savePanel;

	// Token: 0x04001C6D RID: 7277
	public AvatarInfoFile avatarInfo;

	// Token: 0x04001C6E RID: 7278
	public UIToggle save;

	// Token: 0x04001C6F RID: 7279
	public static SaveManager inst;
}
