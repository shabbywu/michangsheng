using Fungus;
using KBEngine;
using UnityEngine;

public class FollwChatTool : MonoBehaviour
{
	public Flowchart flowchat;

	private Avatar avatar;

	private void Start()
	{
		flowchat = ((Component)this).GetComponent<Flowchart>();
		avatar = Tools.instance.getPlayer();
	}

	public void StartFight()
	{
		Tools.instance.startFight(flowchat.GetIntegerVariable("MonsterID"));
	}

	public void AddItem()
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		int itemID = (int)flowchat.GetVariable<Vector2Variable>("Item").Value.x;
		int count = (int)flowchat.GetVariable<Vector2Variable>("Item").Value.y;
		((Avatar)KBEngineApp.app.player()).addItem(itemID, count, Tools.CreateItemSeid(itemID));
	}

	public void SetStaticTalk()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		GlobalValue.SetTalk(0, (int)flowchat.GetVariable<Vector2Variable>("StaticTalk").Value.x, "FollwChatTool.SetStaticTalk " + flowchat.GetParentName());
	}

	public void GetStaticTalk()
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		flowchat.GetVariable<Vector2Variable>("StaticTalk").Value = new Vector2(0f, (float)GlobalValue.GetTalk(1, "FollwChatTool.GetStaticTalk " + flowchat.GetParentName()));
	}

	public void SetStaticValues()
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		int id = (int)flowchat.GetVariable<Vector2Variable>("StaticValues").Value.x;
		int value = (int)flowchat.GetVariable<Vector2Variable>("StaticValues").Value.y;
		GlobalValue.Set(id, value, "FollwChatTool.SetStaticValues " + flowchat.GetParentName());
	}

	public void GetStaticValues()
	{
		int value = flowchat.GetVariable<IntegerVariable>("GetID").Value;
		switch (value)
		{
		case 1:
			flowchat.SetIntegerVariable("TempValue", avatar.HP);
			break;
		case 2:
			flowchat.SetIntegerVariable("TempValue", (int)avatar.exp);
			break;
		case 3:
			flowchat.SetIntegerVariable("TempValue", (int)avatar.shouYuan);
			break;
		default:
		{
			int value2 = GlobalValue.Get(value, "FollwChatTool.GetStaticValues " + flowchat.GetParentName());
			flowchat.SetIntegerVariable("TempValue", value2);
			break;
		}
		}
	}

	public void InitFungaus()
	{
		flowchat.SetIntegerVariable("ShenShi", avatar.shengShi);
		flowchat.SetIntegerVariable("JinJie", avatar.level);
		flowchat.SetIntegerVariable("DunSu", avatar.dunSu);
		flowchat.SetIntegerVariable("ZiZhi", avatar.ZiZhi);
		flowchat.SetIntegerVariable("WuXin", (int)avatar.wuXin);
		flowchat.SetIntegerVariable("ShaQi", (int)avatar.shaQi);
		flowchat.SetIntegerVariable("MenPai", avatar.menPai);
	}
}
