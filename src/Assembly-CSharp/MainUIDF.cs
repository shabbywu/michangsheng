using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainUIDF : MonoBehaviour, IESCClose
{
	public List<int> targetLevelList;

	public int maxNum;

	private int startIndex = 100;

	[SerializeField]
	private GameObject doufaobj;

	[SerializeField]
	private Transform doufaoList;

	private bool isInit;

	private List<GameObject> cellList = new List<GameObject>();

	public void Init()
	{
		if (!isInit)
		{
			ESCCloseManager.Inst.RegisterClose(this);
			isInit = true;
		}
		((Component)this).gameObject.SetActive(true);
	}

	public void RefreshSaveSlot()
	{
		bool flag = false;
		string text = "";
		for (int num = cellList.Count - 1; num >= 0; num--)
		{
			Object.Destroy((Object)(object)cellList[num]);
		}
		cellList.Clear();
		startIndex = 100;
		for (int i = 0; i < maxNum; i++)
		{
			GameObject val = Object.Instantiate<GameObject>(doufaobj, doufaoList);
			cellList.Add(val);
			MainUIDFCell component = val.GetComponent<MainUIDFCell>();
			if (i == maxNum - 1)
			{
				text = "剧情模式通关后解锁";
				component.Init(startIndex, 15, isLock: true, text);
			}
			else
			{
				int level = targetLevelList[i] - 1;
				int num2 = targetLevelList[i];
				flag = targetLevelList[i] > MainUIMag.inst.maxLevel;
				text = "剧情模式达到" + jsonData.instance.LevelUpDataJsonData[num2.ToString()]["Name"].Str + "后解锁";
				component.Init(startIndex, level, flag, text);
			}
			startIndex++;
		}
	}

	public void Close()
	{
		((Component)this).gameObject.SetActive(false);
		ESCCloseManager.Inst.UnRegisterClose(this);
	}

	public bool TryEscClose()
	{
		Close();
		return true;
	}

	public void ClearDFSave()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Expected O, but got Unknown
		USelectBox.Show("确定清空神仙斗法存档吗？之后可以重新生成。", (UnityAction)delegate
		{
			startIndex = 100;
			for (int i = 0; i < maxNum; i++)
			{
				YSNewSaveSystem.DeleteSave(i + startIndex);
			}
			RefreshSaveSlot();
			UIPopTip.Inst.Pop("已清除");
		});
	}
}
