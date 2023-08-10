using KBEngine;
using UnityEngine;
using UnityEngine.UI;

public class UI_Target : MonoBehaviour
{
	public bool bShowDetail;

	public Slider slider_hp;

	public Text text_targetName;

	public GameObject GO_targetUI;

	public GameEntity GE_target;

	public Avatar avatar;

	public PlayerSetRandomFace randomFace;

	public GameObject bufflist;

	public GameObject buffTemp;

	public GameObject buffTemp2;

	public Image leveIcon;

	public int BuffCount;

	public Text text_hpDetail;

	private bool shouldUpdataBuff;

	private bool isException;

	public bool canAtack()
	{
		return GE_target.canAttack;
	}

	public void deactivate()
	{
		GO_targetUI.SetActive(false);
	}
}
