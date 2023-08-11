using UnityEngine;
using script.MenPaiTask.ZhangLao.UI.Ctr;
using script.NewLianDan.Base;

namespace script.MenPaiTask.ZhangLao.UI.UI;

public class ElderTaskUI : BasePanel
{
	public ElderTaskCtr Ctr;

	public GameObject 已完成;

	public GameObject 执行中;

	public GameObject 待接取;

	public Transform 任务列表列表;

	private bool isInit;

	public ElderTaskUI(GameObject gameObject)
	{
		_go = gameObject;
		Ctr = new ElderTaskCtr();
	}

	private void Init()
	{
		已完成 = Get("任务列表/Viewport/Content/已完成");
		执行中 = Get("任务列表/Viewport/Content/执行中");
		待接取 = Get("任务列表/Viewport/Content/待接取");
		任务列表列表 = Get("任务列表/Viewport/Content").transform;
		Ctr.CreateTaskList();
	}

	public override void Show()
	{
		if (!isInit)
		{
			Init();
			isInit = true;
		}
		base.Show();
	}
}
