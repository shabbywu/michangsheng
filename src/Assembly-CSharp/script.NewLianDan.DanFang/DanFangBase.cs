using System.Collections.Generic;

namespace script.NewLianDan.DanFang;

public class DanFangBase
{
	public Dictionary<int, int> ZhuYao1 = new Dictionary<int, int>();

	public Dictionary<int, int> ZhuYao2 = new Dictionary<int, int>();

	public Dictionary<int, int> FuYao1 = new Dictionary<int, int>();

	public Dictionary<int, int> FuYao2 = new Dictionary<int, int>();

	public Dictionary<int, int> YaoYin = new Dictionary<int, int>();

	public int ZhuYaoYaoXin1;

	public int ZhuYaoYaoXin2;

	public int FuYaoYaoXin1;

	public int FuYaoYaoXin2;

	public JSONObject Json;
}
