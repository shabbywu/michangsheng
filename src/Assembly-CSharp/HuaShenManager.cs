using KBEngine;
using UnityEngine;
using UnityEngine.UI;

public class HuaShenManager : MonoBehaviour
{
	public static HuaShenManager Inst;

	public UIHuaShenBuffShow FanXingShow;

	public UIHuaShenBuffShow XianXingShow;

	public UIHuaShenBuffShow CuiTiShow;

	public UIHuaShenBuffShow SuHunShow;

	public Text TaiText;

	public Animator SceneAnimator;

	private Avatar player;

	private bool isXianTai = true;

	private int animChangeCount;

	private void Awake()
	{
		Inst = this;
		player = PlayerEx.Player;
	}

	private void Update()
	{
		RefreshUI();
	}

	public void RefreshUI()
	{
		FanXingShow.SetNumber(player.buffmag.GetBuffSum(3132));
		XianXingShow.SetNumber(player.buffmag.GetBuffSum(3133));
		CuiTiShow.SetNumber(player.buffmag.GetBuffSum(3134));
		SuHunShow.SetNumber(player.buffmag.GetBuffSum(3135));
		int buffSum = player.buffmag.GetBuffSum(3130);
		int buffSum2 = player.buffmag.GetBuffSum(3131);
		if (buffSum > 0)
		{
			if (isXianTai)
			{
				StartFanTiAnim();
			}
		}
		else if (buffSum2 > 0 && !isXianTai)
		{
			StartXianTaiAnim();
		}
	}

	public void StartFanTiAnim()
	{
		Debug.Log((object)"切换到凡体");
		isXianTai = false;
		TaiText.text = "凡 体";
		if (animChangeCount > 0)
		{
			SceneAnimator.Play("HuaShenXianTaiToFanTiAnim");
		}
		animChangeCount++;
	}

	public void StartXianTaiAnim()
	{
		Debug.Log((object)"切换到仙胎");
		isXianTai = true;
		TaiText.text = "仙 胎";
		SceneAnimator.Play("HuaShenFanTiToXianTaiAnim");
		animChangeCount++;
	}

	private void OnDestroy()
	{
		Inst = null;
	}
}
