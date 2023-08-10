using System;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YSGame;

public class MainUIDataCell : MonoBehaviour
{
	public int index;

	public int id;

	public bool isHas;

	[SerializeField]
	private GameObject hasData;

	[SerializeField]
	private Text level;

	[SerializeField]
	private Text time;

	[SerializeField]
	private Text realSaveTime;

	[SerializeField]
	private GameObject autoSaveTips;

	[SerializeField]
	private Image level_Image;

	[SerializeField]
	private GameObject noData;

	public SaveSlotData data;

	public void Init(int index, int id)
	{
		this.index = index;
		this.id = id;
		data = YSNewSaveSystem.GetAvatarSaveData(index, id);
		isHas = data.HasSave;
		if (data.HasSave)
		{
			if (data.IsBreak)
			{
				level.text = "该存档已损坏";
				time.text = "???";
			}
			else
			{
				level.text = data.AvatarLevelText;
				level_Image.sprite = data.AvatarLevelSprite;
				time.text = data.GameTime;
				realSaveTime.text = data.RealSaveTime;
				((Component)realSaveTime).gameObject.SetActive(true);
				if (!data.IsNewSaveSystem)
				{
					realSaveTime.text += " <color=red><size=30>旧</size></color>";
				}
				if (id == 0)
				{
					autoSaveTips.SetActive(true);
				}
			}
			noData.SetActive(false);
			hasData.SetActive(true);
		}
		else
		{
			noData.SetActive(true);
			hasData.SetActive(false);
		}
		((Component)this).gameObject.SetActive(true);
	}

	public void Click()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Expected O, but got Unknown
		if (!isHas)
		{
			return;
		}
		TySelect.inst.Show("是否读取当前存档", (UnityAction)delegate
		{
			if ((Object)(object)FpUIMag.inst != (Object)null)
			{
				Object.Destroy((Object)(object)((Component)FpUIMag.inst).gameObject);
			}
			YSSaveGame.Reset();
			KBEngineApp.app.entities[10] = null;
			KBEngineApp.app.entities.Remove(10);
			try
			{
				if (data.IsNewSaveSystem)
				{
					YSNewSaveSystem.LoadSave(index, id);
				}
				else
				{
					MainUIMag.inst.startGame(index, id);
				}
			}
			catch (Exception ex)
			{
				Debug.LogError((object)"读档失败");
				Debug.LogError((object)ex);
				UCheckBox.Show("存档读取失败，可能已损坏。如果订阅了模组，请检查是否有模组错误。");
			}
		}, null, isDestorySelf: false);
	}
}
