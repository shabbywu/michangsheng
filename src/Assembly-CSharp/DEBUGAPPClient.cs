using System.IO;
using System.Text;
using Fungus;
using KBEngine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DEBUGAPPClient : MonoBehaviour
{
	public enum DebugType
	{
		Map,
		Fight,
		PaiMai,
		jieDan
	}

	public bool isDEBUG;

	public string debugsceneName = "";

	public bool click;

	public DebugType type;

	public int monstarID = 1;

	public StartFight.FightEnumType fightType;

	public int FightTalkID;

	public int FightCardID;

	public int paiMaiID;

	public int PaiMaiAvatar = 1;

	public int AvatarIndex;

	public int SaveIndex;

	public static bool debugFightByAI;

	private void Awake()
	{
	}

	private void Start()
	{
		((MonoBehaviour)this).Invoke("start", 0.2f);
	}

	public static void debugSave(int skillID, int skillDamage)
	{
		if (debugFightByAI)
		{
			JSONObject jSONObject = new JSONObject(JSONObject.Type.OBJECT);
			jSONObject.AddField("skillID", skillID);
			jSONObject.AddField("skillDamage", skillDamage);
			StreamWriter streamWriter = new StreamWriter("D:/d_crafting.py", append: true, Encoding.UTF8);
			streamWriter.Write("datas =" + jSONObject.ToString());
			streamWriter.Close();
		}
	}

	public void start()
	{
		if (type == DebugType.Fight || type == DebugType.jieDan)
		{
			((Component)this).GetComponent<StartGame>().addAvatar(AvatarIndex, SaveIndex);
			Tools.instance.MonstarID = monstarID;
			Tools.instance.FinalScene = "AllMaps";
			Tools.instance.monstarMag.FightType = fightType;
			Avatar player = Tools.instance.getPlayer();
			if (type == DebugType.jieDan)
			{
				Tools.instance.getPlayer().spell.addDBuff(4010, 2);
			}
			Tools.instance.monstarMag.gameStartHP = player.HP;
			Tools.instance.monstarMag.FightTalkID = FightTalkID;
			Tools.instance.monstarMag.FightCardID = FightCardID;
			SceneManager.LoadScene((type == DebugType.Fight) ? "YSFight" : "YSJieDanFight");
		}
		else if (type == DebugType.Map)
		{
			if (!click)
			{
				((Component)this).GetComponent<StartGame>().addAvatar(AvatarIndex, SaveIndex);
				Tools.instance.loadMapScenes(debugsceneName);
				click = true;
			}
		}
		else if (type == DebugType.PaiMai && !click)
		{
			((Component)this).GetComponent<StartGame>().addAvatar(AvatarIndex, SaveIndex);
			Tools.instance.getPlayer().nowPaiMaiCompereAvatarID = PaiMaiAvatar;
			Tools.instance.getPlayer().nowPaiMaiID = paiMaiID;
			SceneManager.LoadScene("PaiMai");
			click = true;
		}
	}

	private void Update()
	{
	}
}
