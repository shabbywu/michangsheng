using KBEngine;
using UnityEngine;

public class UI_Deng : MonoBehaviour
{
	public int type = 1;

	public UI2DSprite dengzhao;

	public UI2DSprite huo;

	public UILabel text;

	private void Start()
	{
	}

	public void SetHPDeng()
	{
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		text.text = avatar.HP + "/" + avatar.HP_Max;
		SetHuo(avatar.HP, avatar.HP_Max);
		SetDengZhao(avatar.HP, avatar.HP_Max);
	}

	public void SetEXPDeng()
	{
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		ulong num = 99999999999uL;
		if (jsonData.instance.LevelUpDataJsonData[string.Concat(avatar.level)] != null)
		{
			num = (ulong)jsonData.instance.LevelUpDataJsonData[string.Concat(avatar.level)]["MaxExp"].n;
		}
		string text = num.ToCNNumberWithUnit();
		string text2 = avatar.exp.ToCNNumberWithUnit();
		this.text.text = text2 + "/" + text;
		SetHuo((int)avatar.exp, (int)num);
		SetDengZhao((int)avatar.exp, (int)num);
	}

	public void SetDengZhao(int num, int Max)
	{
		dengzhao.alpha = (float)num / (float)Max;
	}

	public void SetHuo(int num, int Max)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		((Component)huo).transform.localScale = Vector3.one * (0.5f + (float)num / (float)Max / 2f);
	}

	private void Update()
	{
		if (type == 1)
		{
			SetHPDeng();
		}
		else
		{
			SetEXPDeng();
		}
	}
}
