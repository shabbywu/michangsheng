using KBEngine;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;
using YSGame.Fight;

public class JieYingManager : MonoBehaviour
{
	private Avatar avatar;

	public GameObject JinDan;

	private Animator JinDanAnimator;

	public GameObject XinMoSpine;

	public GameObject HuaYingPanel;

	public Text CurYiZhi;

	public Text YiZhi_Max;

	public Text CurJingMai;

	public Text JingMai_Max;

	public Text CurJingDanHp;

	public Text JingDanHp_Max;

	public Text CurHuaYingJinDu;

	public Text HuaYingJinDu_Max;

	public Text CurMoNian;

	public Text CurMoNianDesc;

	public Text NextMoNian;

	public Text CurState;

	public SkeletonAnimation yizhizhuaji;

	public SkeletonAnimation jingmaizhuaji;

	public AvatarShowHpDamage JinDanDamage;

	public AvatarShowHpDamage YiZhiDamage;

	public AvatarShowHpDamage JingMaiDamage;

	private int State;

	private void Awake()
	{
		avatar = Tools.instance.getPlayer();
		avatar.jieyin.Init();
		SuiDanStart();
	}

	private void SuiDanStart()
	{
		JinDanAnimator = JinDan.GetComponent<Animator>();
		State = 1;
	}

	public void JinDanAttacked()
	{
		JinDanAnimator.Play("hit");
	}

	public void HuaYingCallBack()
	{
		JinDanAnimator.Play("break");
		UIFightPanel.Inst.BanSkillAndWeapon = true;
		((MonoBehaviour)this).Invoke("HuaYingStart", 2.73f);
	}

	private void HuaYingStart()
	{
		XinMoSpine.SetActive(true);
		HuaYingPanel.SetActive(true);
		JinDan.SetActive(false);
		State = 2;
		UIFightPanel.Inst.BanSkillAndWeapon = false;
	}

	private void HideZhuaJi()
	{
		((Component)yizhizhuaji).gameObject.SetActive(false);
	}

	public void XinMoAttack(int target = 0)
	{
		XinMoSpine.GetComponent<Animator>().Play("attank");
		yizhizhuaji.AnimationName = "animation";
		((Component)yizhizhuaji).gameObject.SetActive(true);
		((MonoBehaviour)this).Invoke("HideZhuaJi", 1.167f);
	}

	public void showDamage(int num, int target = 0)
	{
		num = -num;
		switch (target)
		{
		case 0:
			JinDanDamage.show(num);
			break;
		case 1:
			YiZhiDamage.show(num);
			break;
		case 2:
			JingMaiDamage.show(num);
			break;
		}
	}

	private void getCurMoNian()
	{
		if (avatar.buffmag.getBuffBySeid(212).Count > 0)
		{
			string text = string.Concat(avatar.buffmag.getBuffBySeid(212)[0][2]);
			CurMoNian.text = jsonData.instance.BuffJsonData[text]["name"].Str;
			CurMoNianDesc.text = jsonData.instance.BuffJsonData[text]["descr"].Str;
			string key = jsonData.instance.BuffSeidJsonData[5][text]["value1"][0].ToString();
			NextMoNian.text = jsonData.instance.BuffJsonData[key]["name"].Str;
		}
	}

	private void Update()
	{
		if (avatar != null)
		{
			CurYiZhi.text = avatar.jieyin.YiZhi.ToString();
			YiZhi_Max.text = "/" + avatar.jieyin.YiZhi_Max;
			CurJingMai.text = avatar.jieyin.JinMai.ToString();
			JingMai_Max.text = "/" + avatar.jieyin.JinMai_Max;
			if (State == 1)
			{
				CurState.text = "碎丹";
				CurJingDanHp.text = avatar.jieyin.JinDanHP.ToString();
				JingDanHp_Max.text = "/" + avatar.jieyin.JinDanHP_Max;
			}
			else if (State == 2)
			{
				CurState.text = "化婴";
				CurHuaYingJinDu.text = avatar.jieyin.HuaYing.ToString();
				HuaYingJinDu_Max.text = "/" + avatar.jieyin.HuaYing_Max;
				getCurMoNian();
			}
		}
	}
}
