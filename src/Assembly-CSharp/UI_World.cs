using System;
using KBEngine;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000418 RID: 1048
public class UI_World : MonoBehaviour
{
	// Token: 0x060021BB RID: 8635 RVA: 0x000E9853 File Offset: 0x000E7A53
	private void Awake()
	{
		SceneManager.LoadSceneAsync("GameWorld", 1);
		Event.registerOut("SetBackgroudAvater", this, "SetBackgroudAvater");
		Event.registerOut("removeBackgroudAvater", this, "removeBackgroudAvater");
	}

	// Token: 0x060021BC RID: 8636 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x060021BD RID: 8637 RVA: 0x000826BE File Offset: 0x000808BE
	private void OnDestroy()
	{
		Event.deregisterOut(this);
	}

	// Token: 0x060021BE RID: 8638 RVA: 0x000E9883 File Offset: 0x000E7A83
	public void removeBackgroudAvater()
	{
		Object.Destroy(this.nowAvaterObj);
	}

	// Token: 0x060021BF RID: 8639 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}

	// Token: 0x060021C0 RID: 8640 RVA: 0x000E9890 File Offset: 0x000E7A90
	public void SetBackgroudAvater(int _nowAvater, int _nowAvaterSurface)
	{
		if (this.nowAvater == _nowAvater && this.nowAvaterSurface == _nowAvaterSurface)
		{
			return;
		}
		this.nowAvater = _nowAvater;
		this.nowAvaterSurface = _nowAvaterSurface;
		if (this.nowAvaterObj != null)
		{
			Object.Destroy(this.nowAvaterObj);
		}
		GameObject gameObject = (GameObject)Resources.Load(string.Concat(new object[]
		{
			"Effect/Prefab/gameEntity/Avater/Avater",
			_nowAvater,
			"/Avater",
			_nowAvater,
			"_",
			_nowAvaterSurface
		}));
		this.nowAvaterObj = Object.Instantiate<GameObject>(gameObject, new Vector3(735.42f, 100.017f, 751.23f), Quaternion.Euler(new Vector3(0f, -199f, 0f)));
		this.nowAvaterObj.transform.localScale *= 0.7f;
		Object.Destroy(this.nowAvaterObj.gameObject.GetComponent<CharacterController>());
	}

	// Token: 0x04001B39 RID: 6969
	public int nowAvater;

	// Token: 0x04001B3A RID: 6970
	public int nowAvaterSurface = 1;

	// Token: 0x04001B3B RID: 6971
	public GameObject nowAvaterObj;
}
