using KBEngine;
using UnityEngine;
using YSGame;

public class AvatarInfoCell : MonoBehaviour
{
	public enum FileType
	{
		SAVE,
		LOAD
	}

	public FileType fileType;

	public int index;

	public static AvatarInfoCell inst;

	private void Start()
	{
		inst = this;
	}

	private void Awake()
	{
		inst = this;
	}

	public void click()
	{
		int @int = PlayerPrefs.GetInt("NowPlayerFileAvatar");
		if (fileType == FileType.SAVE)
		{
			if (index == 0)
			{
				UIPopTip.Inst.Pop("不能覆盖自动存档");
			}
			else if (YSSaveGame.GetInt("SaveAvatar" + Tools.instance.getSaveID(@int, index)) == 1)
			{
				selectBox.instence.setChoice("是否覆盖当前存档", new EventDelegate(saveGame), null);
			}
			else
			{
				saveGame();
			}
		}
		else
		{
			if (fileType != FileType.LOAD || !YSSaveGame.HasFile(Paths.GetSavePath(), "AvatarInfo" + Tools.instance.getSaveID(@int, index)))
			{
				return;
			}
			selectBox.instence.setChoice("是否读取当前存档", new EventDelegate(delegate
			{
				if (jsonData.instance.saveState == 1)
				{
					UIPopTip.Inst.Pop("正在存档,请存完后再读取");
				}
				else if (!NpcJieSuanManager.inst.isCanJieSuan)
				{
					UIPopTip.Inst.Pop("正在结算中,请稍后读档");
					UIPopTip.Inst.Pop("如果一直提示,请向官方报备");
				}
				else
				{
					if ((Object)(object)FpUIMag.inst != (Object)null)
					{
						Object.Destroy((Object)(object)((Component)FpUIMag.inst).gameObject);
					}
					if ((Object)(object)TpUIMag.inst != (Object)null)
					{
						Object.Destroy((Object)(object)((Component)TpUIMag.inst).gameObject);
					}
					if ((Object)(object)PanelMamager.inst.UISceneGameObject != (Object)null)
					{
						PanelMamager.inst.UISceneGameObject.SetActive(false);
					}
					loadGame();
				}
			}), null);
		}
	}

	public void loadGame()
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		int @int = PlayerPrefs.GetInt("NowPlayerFileAvatar");
		YSSaveGame.Reset();
		KBEngineApp.app.entities[10] = null;
		KBEngineApp.app.entities.Remove(10);
		GameObject val = new GameObject();
		val.AddComponent<StartGame>();
		val.GetComponent<StartGame>().startGame(@int, index);
	}

	public void saveGame()
	{
		int @int = PlayerPrefs.GetInt("NowPlayerFileAvatar");
		Tools.instance.playerSaveGame(@int, index);
	}
}
