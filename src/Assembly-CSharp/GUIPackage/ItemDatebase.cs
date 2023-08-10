using System;
using System.Collections.Generic;
using UnityEngine;

namespace GUIPackage;

public class ItemDatebase : MonoBehaviour
{
	public static ItemDatebase Inst;

	public Dictionary<int, item> items = new Dictionary<int, item>();

	public List<Texture2D> PingZhi;

	public List<Sprite> PingZhiUp;

	private void Awake()
	{
		Inst = this;
	}

	public void Preload(int taskID)
	{
		LoadSync(taskID);
		Loom.RunAsync(delegate
		{
			LoadAsync(taskID);
		});
	}

	public void LoadSync(int taskID)
	{
		try
		{
			for (int i = 1; i <= 6; i++)
			{
				PingZhi.Add(ResManager.inst.LoadTexture2D("Ui Icon/tab/item" + i));
				PingZhiUp.Add(ResManager.inst.LoadSprite("Ui Icon/tab/itemUP" + i));
			}
		}
		catch (Exception arg)
		{
			PreloadManager.IsException = true;
			PreloadManager.ExceptionData += $"{arg}\n";
			PreloadManager.Inst.TaskDone(taskID);
		}
	}

	public void LoadAsync(int taskID)
	{
		try
		{
			foreach (JSONObject item in jsonData.instance._ItemJsonData.list)
			{
				items.Add(item["id"].I, new item(item["id"].I));
			}
			PreloadManager.Inst.TaskDone(taskID);
		}
		catch (Exception arg)
		{
			PreloadManager.IsException = true;
			PreloadManager.ExceptionData += $"{arg}\n";
			PreloadManager.Inst.TaskDone(taskID);
		}
	}
}
