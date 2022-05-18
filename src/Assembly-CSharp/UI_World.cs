using System;
using KBEngine;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020005CB RID: 1483
public class UI_World : MonoBehaviour
{
	// Token: 0x06002575 RID: 9589 RVA: 0x0001E060 File Offset: 0x0001C260
	private void Awake()
	{
		SceneManager.LoadSceneAsync("GameWorld", 1);
		Event.registerOut("SetBackgroudAvater", this, "SetBackgroudAvater");
		Event.registerOut("removeBackgroudAvater", this, "removeBackgroudAvater");
	}

	// Token: 0x06002576 RID: 9590 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x06002577 RID: 9591 RVA: 0x0001429C File Offset: 0x0001249C
	private void OnDestroy()
	{
		Event.deregisterOut(this);
	}

	// Token: 0x06002578 RID: 9592 RVA: 0x0001E090 File Offset: 0x0001C290
	public void removeBackgroudAvater()
	{
		Object.Destroy(this.nowAvaterObj);
	}

	// Token: 0x06002579 RID: 9593 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x0600257A RID: 9594 RVA: 0x0012AED0 File Offset: 0x001290D0
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

	// Token: 0x04001FFA RID: 8186
	public int nowAvater;

	// Token: 0x04001FFB RID: 8187
	public int nowAvaterSurface = 1;

	// Token: 0x04001FFC RID: 8188
	public GameObject nowAvaterObj;
}
