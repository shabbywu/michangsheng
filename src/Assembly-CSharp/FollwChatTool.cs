using System;
using Fungus;
using KBEngine;
using UnityEngine;

// Token: 0x0200026F RID: 623
public class FollwChatTool : MonoBehaviour
{
	// Token: 0x06001340 RID: 4928 RVA: 0x0001219B File Offset: 0x0001039B
	private void Start()
	{
		this.flowchat = base.GetComponent<Flowchart>();
		this.avatar = Tools.instance.getPlayer();
	}

	// Token: 0x06001341 RID: 4929 RVA: 0x000121B9 File Offset: 0x000103B9
	public void StartFight()
	{
		Tools.instance.startFight(this.flowchat.GetIntegerVariable("MonsterID"));
	}

	// Token: 0x06001342 RID: 4930 RVA: 0x000B2CFC File Offset: 0x000B0EFC
	public void AddItem()
	{
		int itemID = (int)this.flowchat.GetVariable<Vector2Variable>("Item").Value.x;
		int count = (int)this.flowchat.GetVariable<Vector2Variable>("Item").Value.y;
		((Avatar)KBEngineApp.app.player()).addItem(itemID, count, Tools.CreateItemSeid(itemID), false);
	}

	// Token: 0x06001343 RID: 4931 RVA: 0x000121D5 File Offset: 0x000103D5
	public void SetStaticTalk()
	{
		GlobalValue.SetTalk(0, (int)this.flowchat.GetVariable<Vector2Variable>("StaticTalk").Value.x, "FollwChatTool.SetStaticTalk " + this.flowchat.GetParentName());
	}

	// Token: 0x06001344 RID: 4932 RVA: 0x0001220D File Offset: 0x0001040D
	public void GetStaticTalk()
	{
		this.flowchat.GetVariable<Vector2Variable>("StaticTalk").Value = new Vector2(0f, (float)GlobalValue.GetTalk(1, "FollwChatTool.GetStaticTalk " + this.flowchat.GetParentName()));
	}

	// Token: 0x06001345 RID: 4933 RVA: 0x000B2D60 File Offset: 0x000B0F60
	public void SetStaticValues()
	{
		int id = (int)this.flowchat.GetVariable<Vector2Variable>("StaticValues").Value.x;
		int value = (int)this.flowchat.GetVariable<Vector2Variable>("StaticValues").Value.y;
		GlobalValue.Set(id, value, "FollwChatTool.SetStaticValues " + this.flowchat.GetParentName());
	}

	// Token: 0x06001346 RID: 4934 RVA: 0x000B2DC0 File Offset: 0x000B0FC0
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

	// Token: 0x06001347 RID: 4935 RVA: 0x000B2E7C File Offset: 0x000B107C
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

	// Token: 0x04000EFB RID: 3835
	public Flowchart flowchat;

	// Token: 0x04000EFC RID: 3836
	private Avatar avatar;
}
