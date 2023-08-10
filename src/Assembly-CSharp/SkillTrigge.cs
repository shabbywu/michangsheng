using KBEngine;
using UnityEngine;

public class SkillTrigge : MonoBehaviour
{
	public int skillID;

	public Entity caster;

	private GameObject renderEntity;

	private void Start()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		renderEntity = (GameObject)caster.renderObj;
	}

	private void Update()
	{
	}

	private void OnTriggerEnter(Collider collider)
	{
		Debug.Log((object)"开始接触");
		if (KBEngineApp.app.player() == caster)
		{
			Avatar avatar = (Avatar)KBEngineApp.app.player();
			GameEntity component = ((Component)((Component)collider).GetComponent<Collider>()).GetComponent<GameEntity>();
			if (avatar != null && Object.op_Implicit((Object)(object)component) && component.canAttack)
			{
				int postInt = Utility.getPostInt(((Object)((Component)collider).GetComponent<Collider>()).name);
				avatar.cellCall("SkillDamage", skillID, postInt);
			}
		}
	}

	private void OnTriggerExit(Collider collider)
	{
		Debug.Log((object)"接触结束");
	}

	private void OnTriggerStay(Collider collider)
	{
		Debug.Log((object)"接触持续中");
	}
}
