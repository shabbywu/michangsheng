using KBEngine;
using UnityEngine;

public class LevelUpUI : MonoBehaviour
{
	public UILabel desc;

	public UILabel shenshi;

	public UILabel qixue;

	public UILabel shouyuan;

	public UILabel dunsu;

	private void Start()
	{
	}

	public void close()
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		((Component)this).transform.localPosition = new Vector3(0f, 1000f, 0f);
	}

	public void showLevelMax(Avatar avatar)
	{
		desc.text = "体内真元逐渐饱和，你的修为已经达到了[ffe118]" + Tools.instance.Code64ToString(jsonData.instance.LevelUpDataJsonData[string.Concat(avatar.level)]["Name"].str) + "[-]的瓶颈，如果无法突破，就再难提升了";
	}

	public void ShowlevelUpLabel(Avatar avatar, JSONObject jsonInfo, int oldhpmax, int oldshenshi, int oldshouyuan, int oldDunsu)
	{
		desc.text = "周边天地的灵气突然开始涌入你的体内，你感到体内的真元犹如沸腾的开水一般，迅速流动起来。灵气的波动足足持续了一个时辰才平息下来，你终于冲破瓶颈，境界提升至[ffe118]" + Tools.instance.Code64ToString(jsonData.instance.LevelUpDataJsonData[string.Concat(avatar.level)]["Name"].str) + "[-]";
		qixue.text = "气血: " + oldhpmax;
		shenshi.text = "神识: " + oldshenshi;
		shouyuan.text = "寿元: " + oldshouyuan;
		dunsu.text = "遁速: " + oldDunsu;
		((Component)((Component)qixue).transform.Find("NextLabel")).GetComponent<UILabel>().text = string.Concat(avatar.HP_Max);
		((Component)((Component)shenshi).transform.Find("NextLabel")).GetComponent<UILabel>().text = string.Concat(avatar.shengShi);
		((Component)((Component)shouyuan).transform.Find("NextLabel")).GetComponent<UILabel>().text = string.Concat(avatar.shouYuan);
		((Component)((Component)dunsu).transform.Find("NextLabel")).GetComponent<UILabel>().text = string.Concat(avatar.dunSu);
	}

	private void Update()
	{
	}
}
