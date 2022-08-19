using System;
using System.IO;
using System.Text;
using Fungus;
using KBEngine;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x0200041B RID: 1051
public class DEBUGAPPClient : MonoBehaviour
{
	// Token: 0x060021C8 RID: 8648 RVA: 0x00004095 File Offset: 0x00002295
	private void Awake()
	{
	}

	// Token: 0x060021C9 RID: 8649 RVA: 0x000E9A80 File Offset: 0x000E7C80
	private void Start()
	{
		base.Invoke("start", 0.2f);
	}

	// Token: 0x060021CA RID: 8650 RVA: 0x000E9A94 File Offset: 0x000E7C94
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

	// Token: 0x060021CB RID: 8651 RVA: 0x000E9AF4 File Offset: 0x000E7CF4
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

	// Token: 0x060021CC RID: 8652 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}

	// Token: 0x04001B3F RID: 6975
	public bool isDEBUG;

	// Token: 0x04001B40 RID: 6976
	public string debugsceneName = "";

	// Token: 0x04001B41 RID: 6977
	public bool click;

	// Token: 0x04001B42 RID: 6978
	public DEBUGAPPClient.DebugType type;

	// Token: 0x04001B43 RID: 6979
	public int monstarID = 1;

	// Token: 0x04001B44 RID: 6980
	public StartFight.FightEnumType fightType;

	// Token: 0x04001B45 RID: 6981
	public int FightTalkID;

	// Token: 0x04001B46 RID: 6982
	public int FightCardID;

	// Token: 0x04001B47 RID: 6983
	public int paiMaiID;

	// Token: 0x04001B48 RID: 6984
	public int PaiMaiAvatar = 1;

	// Token: 0x04001B49 RID: 6985
	public int AvatarIndex;

	// Token: 0x04001B4A RID: 6986
	public int SaveIndex;

	// Token: 0x04001B4B RID: 6987
	public static bool debugFightByAI;

	// Token: 0x02001397 RID: 5015
	public enum DebugType
	{
		// Token: 0x040068D3 RID: 26835
		Map,
		// Token: 0x040068D4 RID: 26836
		Fight,
		// Token: 0x040068D5 RID: 26837
		PaiMai,
		// Token: 0x040068D6 RID: 26838
		jieDan
	}
}
