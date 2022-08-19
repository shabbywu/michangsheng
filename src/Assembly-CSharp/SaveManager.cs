using System;
using UnityEngine;

// Token: 0x02000375 RID: 885
public class SaveManager : MonoBehaviour
{
	// Token: 0x06001D8E RID: 7566 RVA: 0x000D0C31 File Offset: 0x000CEE31
	private void Awake()
	{
		SaveManager.inst = this;
	}

	// Token: 0x06001D8F RID: 7567 RVA: 0x000D0C39 File Offset: 0x000CEE39
	public void updateState()
	{
		if (this.save.value)
		{
			this.avatarInfo.showSave();
			return;
		}
		this.avatarInfo.showLoad();
	}

	// Token: 0x06001D90 RID: 7568 RVA: 0x000D0C5F File Offset: 0x000CEE5F
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

	// Token: 0x0400181F RID: 6175
	public GameObject savePanel;

	// Token: 0x04001820 RID: 6176
	public AvatarInfoFile avatarInfo;

	// Token: 0x04001821 RID: 6177
	public UIToggle save;

	// Token: 0x04001822 RID: 6178
	public static SaveManager inst;
}
