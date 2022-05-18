using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200058E RID: 1422
public class CreateFaceButton : MonoBehaviour
{
	// Token: 0x0600240E RID: 9230 RVA: 0x0001D18E File Offset: 0x0001B38E
	private void Start()
	{
		base.GetComponent<UIButton>().onClick.Add(new EventDelegate(new EventDelegate.Callback(this.resteChoice)));
	}

	// Token: 0x0600240F RID: 9231 RVA: 0x00126F68 File Offset: 0x00125168
	public void resteChoice()
	{
		string text = base.transform.Find("Label").GetComponent<UILabel>().text;
		Create_face component = base.transform.parent.parent.GetComponent<Create_face>();
		AvatarFaceDatabase faceDatabase = component.faceDatabase;
		foreach (object obj in component.ItemGrid.transform)
		{
			Object.Destroy(((Transform)obj).gameObject);
		}
		faceInfoList faceInfo = faceDatabase.getFaceInfo(text);
		if (faceInfo != null)
		{
			int num = 0;
			using (List<faceInfoDataBaseList>.Enumerator enumerator2 = faceInfo.faceList.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					faceInfoDataBaseList faceInfoDataBaseList = enumerator2.Current;
					GameObject gameObject = Object.Instantiate<GameObject>(component.faceChoicePrefab);
					gameObject.transform.parent = component.ItemGrid.transform;
					gameObject.transform.localScale = Vector3.one;
					gameObject.transform.Find("Label").GetComponent<UILabel>().text = faceInfoDataBaseList.Name;
					gameObject.transform.Find("LabelLight").GetComponent<UILabel>().text = faceInfoDataBaseList.Name;
					if (num == 0)
					{
						gameObject.transform.GetComponent<UIToggle>().value = true;
						gameObject.transform.GetComponent<createChoiceBtn>().resteChoice();
						gameObject.transform.GetComponent<createChoiceBtn>().SetHasColorSelect();
					}
					num++;
				}
				goto IL_197;
			}
		}
		UIPopTip.Inst.Pop("找不到类型" + text, PopTipIconType.叹号);
		IL_197:
		component.ItemGrid.repositionNow = true;
	}
}
