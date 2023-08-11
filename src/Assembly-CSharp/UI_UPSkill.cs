using UnityEngine;

public class UI_UPSkill : MonoBehaviour
{
	public UILabel label;

	public UILabel label1;

	public UILabel label2;

	public UILabel label3;

	public UILabel label4;

	public UIButton btn;

	public GameObject obj1;

	public GameObject grid;

	public void initSkill(int StaticSkillID)
	{
	}

	public void close()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		((Component)this).transform.localScale = Vector3.zero;
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
