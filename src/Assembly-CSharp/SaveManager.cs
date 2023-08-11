using UnityEngine;

public class SaveManager : MonoBehaviour
{
	public GameObject savePanel;

	public AvatarInfoFile avatarInfo;

	public UIToggle save;

	public static SaveManager inst;

	private void Awake()
	{
		inst = this;
	}

	public void updateState()
	{
		if (save.value)
		{
			avatarInfo.showSave();
		}
		else
		{
			avatarInfo.showLoad();
		}
	}

	public void updateSaveDate()
	{
		if (savePanel.activeSelf)
		{
			if (save.value)
			{
				avatarInfo.showSave();
			}
			else
			{
				avatarInfo.showLoad();
			}
		}
	}
}
