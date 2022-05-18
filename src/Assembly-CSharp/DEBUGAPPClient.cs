using System;
using System.IO;
using System.Text;
using Fungus;
using KBEngine;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020005CE RID: 1486
public class DEBUGAPPClient : MonoBehaviour
{
	// Token: 0x06002582 RID: 9602 RVA: 0x000042DD File Offset: 0x000024DD
	private void Awake()
	{
	}

	// Token: 0x06002583 RID: 9603 RVA: 0x0001E0FF File Offset: 0x0001C2FF
	private void Start()
	{
		base.Invoke("start", 0.2f);
	}

	// Token: 0x06002584 RID: 9604 RVA: 0x0012B060 File Offset: 0x00129260
	public static void debugSave(int skillID, int skillDamage)
	{
		if (DEBUGAPPClient.debugFightByAI)
		{
			JSONObject jsonobject = new JSONObject(JSONObject.Type.OBJECT);
			jsonobject.AddField("skillID", skillID);
			jsonobject.AddField("skillDamage", skillDamage);
			StreamWriter streamWriter = new StreamWriter("D:/d_crafting.py", true, Encoding.UTF8);
			streamWriter.Write("datas =" + jsonobject.ToString());
			streamWriter.Close();
		}
	}

	// Token: 0x06002585 RID: 9605 RVA: 0x0012B0C0 File Offset: 0x001292C0
	public void start()
	{
		if (this.type == DEBUGAPPClient.DebugType.Fight || this.type == DEBUGAPPClient.DebugType.jieDan)
		{
			base.GetComponent<StartGame>().addAvatar(this.AvatarIndex, this.SaveIndex);
			Tools.instance.MonstarID = this.monstarID;
			Tools.instance.FinalScene = "AllMaps";
			Tools.instance.monstarMag.FightType = this.fightType;
			Avatar player = Tools.instance.getPlayer();
			if (this.type == DEBUGAPPClient.DebugType.jieDan)
			{
				Tools.instance.getPlayer().spell.addDBuff(4010, 2);
			}
			Tools.instance.monstarMag.gameStartHP = player.HP;
			Tools.instance.monstarMag.FightTalkID = this.FightTalkID;
			Tools.instance.monstarMag.FightCardID = this.FightCardID;
			SceneManager.LoadScene((this.type == DEBUGAPPClient.DebugType.Fight) ? "YSFight" : "YSJieDanFight");
			return;
		}
		if (this.type == DEBUGAPPClient.DebugType.Map)
		{
			if (!this.click)
			{
				base.GetComponent<StartGame>().addAvatar(this.AvatarIndex, this.SaveIndex);
				Tools.instance.loadMapScenes(this.debugsceneName, true);
				this.click = true;
				return;
			}
		}
		else if (this.type == DEBUGAPPClient.DebugType.PaiMai && !this.click)
		{
			base.GetComponent<StartGame>().addAvatar(this.AvatarIndex, this.SaveIndex);
			Tools.instance.getPlayer().nowPaiMaiCompereAvatarID = this.PaiMaiAvatar;
			Tools.instance.getPlayer().nowPaiMaiID = this.paiMaiID;
			SceneManager.LoadScene("PaiMai");
			this.click = true;
		}
	}

	// Token: 0x06002586 RID: 9606 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x04002000 RID: 8192
	public bool isDEBUG;

	// Token: 0x04002001 RID: 8193
	public string debugsceneName = "";

	// Token: 0x04002002 RID: 8194
	public bool click;

	// Token: 0x04002003 RID: 8195
	public DEBUGAPPClient.DebugType type;

	// Token: 0x04002004 RID: 8196
	public int monstarID = 1;

	// Token: 0x04002005 RID: 8197
	public StartFight.FightEnumType fightType;

	// Token: 0x04002006 RID: 8198
	public int FightTalkID;

	// Token: 0x04002007 RID: 8199
	public int FightCardID;

	// Token: 0x04002008 RID: 8200
	public int paiMaiID;

	// Token: 0x04002009 RID: 8201
	public int PaiMaiAvatar = 1;

	// Token: 0x0400200A RID: 8202
	public int AvatarIndex;

	// Token: 0x0400200B RID: 8203
	public int SaveIndex;

	// Token: 0x0400200C RID: 8204
	public static bool debugFightByAI;

	// Token: 0x020005CF RID: 1487
	public enum DebugType
	{
		// Token: 0x0400200E RID: 8206
		Map,
		// Token: 0x0400200F RID: 8207
		Fight,
		// Token: 0x04002010 RID: 8208
		PaiMai,
		// Token: 0x04002011 RID: 8209
		jieDan
	}
}
