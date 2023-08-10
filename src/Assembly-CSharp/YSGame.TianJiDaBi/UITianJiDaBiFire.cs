using UnityEngine;
using UnityEngine.UI;

namespace YSGame.TianJiDaBi;

public class UITianJiDaBiFire : MonoBehaviour
{
	public Text NumberText;

	public GameObject Normal;

	public GameObject Win;

	public GameObject Fail;

	public Animator FireAnim;

	private Color normalColor = new Color(78f / 85f, 74f / 85f, 63f / 85f);

	private Color winColor = new Color(78f / 85f, 74f / 85f, 63f / 85f);

	private Color failColor = new Color(63f / 85f, 78f / 85f, 77f / 85f);

	public void SetNormal()
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		Normal.SetActive(true);
		Win.SetActive(false);
		Fail.SetActive(false);
		((Graphic)NumberText).color = normalColor;
		FireAnim.Play("UITianJiDaBiFireAnim");
	}

	public void SetWin()
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		Normal.SetActive(false);
		Win.SetActive(true);
		Fail.SetActive(false);
		((Graphic)NumberText).color = winColor;
	}

	public void SetFail()
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		Normal.SetActive(false);
		Win.SetActive(false);
		Fail.SetActive(true);
		((Graphic)NumberText).color = failColor;
	}
}
