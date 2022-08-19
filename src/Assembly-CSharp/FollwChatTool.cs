using System;
using Fungus;
using KBEngine;
using UnityEngine;

// Token: 0x02000189 RID: 393
public class FollwChatTool : MonoBehaviour
{
	// Token: 0x060010D9 RID: 4313 RVA: 0x000643C3 File Offset: 0x000625C3
	private void Start()
	{
		this.flowchat = base.GetComponent<Flowchart>();
		this.avatar = Tools.instance.getPlayer();
	}

	// Token: 0x060010DA RID: 4314 RVA: 0x000643E1 File Offset: 0x000625E1
	public void StartFight()
	{
		Tools.instance.startFight(this.flowchat.GetIntegerVariable("MonsterID"));
	}

	// Token: 0x060010DB RID: 4315 RVA: 0x00064400 File Offset: 0x00062600
	public void AddItem()
	{
		int itemID = (int)this.flowchat.GetVariable<Vector2Variable>("Item").Value.x;
		int count = (int)this.flowchat.GetVariable<Vector2Variable>("Item").Value.y;
		((Avatar)KBEngineApp.app.player()).addItem(itemID, count, Tools.CreateItemSeid(itemID), false);
	}

	// Token: 0x060010DC RID: 4316 RVA: 0x00064462 File Offset: 0x00062662
	public void SetStaticTalk()
	{
		GlobalValue.SetTalk(0, (int)this.flowchat.GetVariable<Vector2Variable>("StaticTalk").Value.x, "FollwChatTool.SetStaticTalk " + this.flowchat.GetParentName());
	}

	// Token: 0x060010DD RID: 4317 RVA: 0x0006449A File Offset: 0x0006269A
	public void GetStaticTalk()
	{
		this.flowchat.GetVariable<Vector2Variable>("StaticTalk").Value = new Vector2(0f, (float)GlobalValue.GetTalk(1, "FollwChatTool.GetStaticTalk " + this.flowchat.GetParentName()));
	}

	// Token: 0x060010DE RID: 4318 RVA: 0x000644D8 File Offset: 0x000626D8
	public void SetStaticValues()
	{
		int id = (int)this.flowchat.GetVariable<Vector2Variable>("StaticValues").Value.x;
		int value = (int)this.flowchat.GetVariable<Vector2Variable>("StaticValues").Value.y;
		GlobalValue.Set(id, value, "FollwChatTool.SetStaticValues " + this.flowchat.GetParentName());
	}

	// Token: 0x060010DF RID: 4319 RVA: 0x00064538 File Offset: 0x00062738
	public void GetStaticValues()
	{
		int value = this.flowchat.GetVariable<IntegerVariable>("GetID").Value;
		switch (value)
		{
		case 1:
			this.flowchat.SetIntegerVariable("TempValue", this.avatar.HP);
			return;
		case 2:
			this.flowchat.SetIntegerVariable("TempValue", (int)this.avatar.exp);
			return;
		case 3:
			this.flowchat.SetIntegerVariable("TempValue", (int)this.avatar.shouYuan);
			return;
		default:
		{
			int value2 = GlobalValue.Get(value, "FollwChatTool.GetStaticValues " + this.flowchat.GetParentName());
			this.flowchat.SetIntegerVariable("TempValue", value2);
			return;
		}
		}
	}

	// Token: 0x060010E0 RID: 4320 RVA: 0x000645F4 File Offset: 0x000627F4
	public void InitFungaus()
	{
		this.flowchat.SetIntegerVariable("ShenShi", this.avatar.shengShi);
		this.flowchat.SetIntegerVariable("JinJie", (int)this.avatar.level);
		this.flowchat.SetIntegerVariable("DunSu", this.avatar.dunSu);
		this.flowchat.SetIntegerVariable("ZiZhi", this.avatar.ZiZhi);
		this.flowchat.SetIntegerVariable("WuXin", (int)this.avatar.wuXin);
		this.flowchat.SetIntegerVariable("ShaQi", (int)this.avatar.shaQi);
		this.flowchat.SetIntegerVariable("MenPai", (int)this.avatar.menPai);
	}

	// Token: 0x04000C16 RID: 3094
	public Flowchart flowchat;

	// Token: 0x04000C17 RID: 3095
	private Avatar avatar;
}
