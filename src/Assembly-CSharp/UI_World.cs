using KBEngine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_World : MonoBehaviour
{
	public int nowAvater;

	public int nowAvaterSurface = 1;

	public GameObject nowAvaterObj;

	private void Awake()
	{
		SceneManager.LoadSceneAsync("GameWorld", (LoadSceneMode)1);
		Event.registerOut("SetBackgroudAvater", this, "SetBackgroudAvater");
		Event.registerOut("removeBackgroudAvater", this, "removeBackgroudAvater");
	}

	private void Start()
	{
	}

	private void OnDestroy()
	{
		Event.deregisterOut(this);
	}

	public void removeBackgroudAvater()
	{
		Object.Destroy((Object)(object)nowAvaterObj);
	}

	private void Update()
	{
	}

	public void SetBackgroudAvater(int _nowAvater, int _nowAvaterSurface)
	{
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Expected O, but got Unknown
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		if (nowAvater != _nowAvater || nowAvaterSurface != _nowAvaterSurface)
		{
			nowAvater = _nowAvater;
			nowAvaterSurface = _nowAvaterSurface;
			if ((Object)(object)nowAvaterObj != (Object)null)
			{
				Object.Destroy((Object)(object)nowAvaterObj);
			}
			GameObject val = (GameObject)Resources.Load("Effect/Prefab/gameEntity/Avater/Avater" + _nowAvater + "/Avater" + _nowAvater + "_" + _nowAvaterSurface);
			nowAvaterObj = Object.Instantiate<GameObject>(val, new Vector3(735.42f, 100.017f, 751.23f), Quaternion.Euler(new Vector3(0f, -199f, 0f)));
			Transform transform = nowAvaterObj.transform;
			transform.localScale *= 0.7f;
			Object.Destroy((Object)(object)nowAvaterObj.gameObject.GetComponent<CharacterController>());
		}
	}
}
