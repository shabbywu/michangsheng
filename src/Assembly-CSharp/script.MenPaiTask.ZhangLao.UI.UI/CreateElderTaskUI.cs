using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using script.MenPaiTask.ZhangLao.UI.Ctr;
using script.NewLianDan.Base;

namespace script.MenPaiTask.ZhangLao.UI.UI;

public class CreateElderTaskUI : BasePanel
{
	private bool isInit;

	public CreateElderTaskCtr Ctr;

	public GameObject ItemPrefab;

	public Transform ItemParent;

	private Text 灵石;

	private Text 声望;

	private Text 任务内容;

	public CreateElderTaskUI(GameObject gameObject)
	{
		_go = gameObject;
		Ctr = new CreateElderTaskCtr();
	}

	private void Init()
	{
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Expected O, but got Unknown
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Expected O, but got Unknown
		ItemPrefab = Get("提交物品列表/1");
		ItemParent = Get("提交物品列表").transform;
		灵石 = Get<Text>("Cost/灵石/Value");
		声望 = Get<Text>("Cost/声望/Value");
		任务内容 = Get<Text>("任务内容");
		Get<FpBtn>("发布按钮").mouseUpEvent.AddListener(new UnityAction(Ctr.PublishTask));
		Get<FpBtn>("返回按钮").mouseUpEvent.AddListener((UnityAction)delegate
		{
			Ctr.ClearItemList();
			ElderTaskUIMag.Inst.OpenElderTaskUI();
		});
		Ctr.CreateItemList();
	}

	public void UpdateUI()
	{
		灵石.SetText(Ctr.NeedMoney);
		声望.SetText(Ctr.NeedReputation);
		任务内容.SetText(Ctr.CreateTaskDesc());
	}

	public override void Hide()
	{
		base.Hide();
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
