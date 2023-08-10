namespace KBEngine;

public class DATATYPE_ShopBuyInfo : DATATYPE_BASE
{
	public ShopBuyInfo createFromStreamEx(MemoryStream stream)
	{
		return new ShopBuyInfo
		{
			shopuuid = stream.readUint32(),
			buytime = stream.readUint8()
		};
	}

	public void addToStreamEx(Bundle stream, ShopBuyInfo v)
	{
		stream.writeUint32(v.shopuuid);
		stream.writeUint8(v.buytime);
	}
}
