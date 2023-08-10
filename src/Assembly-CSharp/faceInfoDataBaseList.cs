using System;
using System.Collections.Generic;

[Serializable]
public class faceInfoDataBaseList
{
	public string Name;

	public string JsonInfoName;

	public List<faceInfoDataBase> faceList = new List<faceInfoDataBase>();
}
