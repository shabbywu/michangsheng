using System;
using System.Collections.Generic;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000EE5 RID: 3813
	public abstract class DroppedItemBase : Entity
	{
		// Token: 0x06005BB5 RID: 23477 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onDialogIDChanged(uint oldValue)
		{
		}

		// Token: 0x06005BB6 RID: 23478 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onEntityNOChanged(uint oldValue)
		{
		}

		// Token: 0x06005BB7 RID: 23479 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onItemCountChanged(uint oldValue)
		{
		}

		// Token: 0x06005BB8 RID: 23480 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onItemIdChanged(int oldValue)
		{
		}

		// Token: 0x06005BB9 RID: 23481 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onModelIDChanged(uint oldValue)
		{
		}

		// Token: 0x06005BBA RID: 23482 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onModelScaleChanged(byte oldValue)
		{
		}

		// Token: 0x06005BBB RID: 23483 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onNameChanged(string oldValue)
		{
		}

		// Token: 0x06005BBC RID: 23484 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onUidChanged(uint oldValue)
		{
		}

		// Token: 0x06005BBD RID: 23485 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onUtypeChanged(uint oldValue)
		{
		}

		// Token: 0x06005BBE RID: 23486 RVA: 0x00040901 File Offset: 0x0003EB01
		public DroppedItemBase()
		{
		}

		// Token: 0x06005BBF RID: 23487 RVA: 0x000042DD File Offset: 0x000024DD
		public override void onComponentsEnterworld()
		{
		}

		// Token: 0x06005BC0 RID: 23488 RVA: 0x000042DD File Offset: 0x000024DD
		public override void onComponentsLeaveworld()
		{
		}

		// Token: 0x06005BC1 RID: 23489 RVA: 0x0004091C File Offset: 0x0003EB1C
		public override void onGetBase()
		{
			this.baseEntityCall = new EntityBaseEntityCall_DroppedItemBase(this.id, this.className);
		}

		// Token: 0x06005BC2 RID: 23490 RVA: 0x00040935 File Offset: 0x0003EB35
		public override void onGetCell()
		{
			this.cellEntityCall = new EntityCellEntityCall_DroppedItemBase(this.id, this.className);
		}

		// Token: 0x06005BC3 RID: 23491 RVA: 0x0004094E File Offset: 0x0003EB4E
		public override void onLoseCell()
		{
			this.cellEntityCall = null;
		}

		// Token: 0x06005BC4 RID: 23492 RVA: 0x00040957 File Offset: 0x0003EB57
		public override EntityCall getBaseEntityCall()
		{
			return this.baseEntityCall;
		}

		// Token: 0x06005BC5 RID: 23493 RVA: 0x0004095F File Offset: 0x0003EB5F
		public override EntityCall getCellEntityCall()
		{
			return this.cellEntityCall;
		}

		// Token: 0x06005BC6 RID: 23494 RVA: 0x000042DD File Offset: 0x000024DD
		public override void attachComponents()
		{
		}

		// Token: 0x06005BC7 RID: 23495 RVA: 0x000042DD File Offset: 0x000024DD
		public override void detachComponents()
		{
		}

		// Token: 0x06005BC8 RID: 23496 RVA: 0x002512E8 File Offset: 0x0024F4E8
		public override void onRemoteMethodCall(MemoryStream stream)
		{
			ScriptModule scriptModule = EntityDef.moduledefs["DroppedItem"];
			ushort num;
			if (scriptModule.usePropertyDescrAlias)
			{
				num = (ushort)stream.readUint8();
			}
			else
			{
				num = stream.readUint16();
			}
			ushort key;
			if (scriptModule.useMethodDescrAlias)
			{
				key = (ushort)stream.readUint8();
			}
			else
			{
				key = stream.readUint16();
			}
			if (num == 0)
			{
				Method method = scriptModule.idmethods[key];
				ushort methodUtype = method.methodUtype;
				return;
			}
			ushort properUtype = scriptModule.idpropertys[num].properUtype;
		}

		// Token: 0x06005BC9 RID: 23497 RVA: 0x00251368 File Offset: 0x0024F568
		public override void onUpdatePropertys(MemoryStream stream)
		{
			ScriptModule scriptModule = EntityDef.moduledefs["DroppedItem"];
			Dictionary<ushort, Property> idpropertys = scriptModule.idpropertys;
			while (stream.length() > 0U)
			{
				ushort num;
				ushort key;
				if (scriptModule.usePropertyDescrAlias)
				{
					num = (ushort)stream.readUint8();
					key = (ushort)stream.readUint8();
				}
				else
				{
					num = stream.readUint16();
					key = stream.readUint16();
				}
				if (num != 0)
				{
					ushort properUtype = idpropertys[num].properUtype;
					return;
				}
				Property property = idpropertys[key];
				ushort properUtype2 = property.properUtype;
				if (properUtype2 <= 96)
				{
					if (properUtype2 != 90)
					{
						if (properUtype2 != 91)
						{
							if (properUtype2 == 96)
							{
								uint oldValue = this.dialogID;
								this.dialogID = stream.readUint32();
								if (property.isBase())
								{
									if (this.inited)
									{
										this.onDialogIDChanged(oldValue);
									}
								}
								else if (this.inWorld)
								{
									this.onDialogIDChanged(oldValue);
								}
							}
						}
						else
						{
							uint oldValue2 = this.itemCount;
							this.itemCount = stream.readUint32();
							if (property.isBase())
							{
								if (this.inited)
								{
									this.onItemCountChanged(oldValue2);
								}
							}
							else if (this.inWorld)
							{
								this.onItemCountChanged(oldValue2);
							}
						}
					}
					else
					{
						int oldValue3 = this.itemId;
						this.itemId = stream.readInt32();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onItemIdChanged(oldValue3);
							}
						}
						else if (this.inWorld)
						{
							this.onItemIdChanged(oldValue3);
						}
					}
				}
				else
				{
					switch (properUtype2)
					{
					case 40000:
					{
						Vector3 position = this.position;
						this.position = stream.readVector3();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onPositionChanged(position);
							}
						}
						else if (this.inWorld)
						{
							this.onPositionChanged(position);
						}
						break;
					}
					case 40001:
					{
						Vector3 direction = this.direction;
						this.direction = stream.readVector3();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onDirectionChanged(direction);
							}
						}
						else if (this.inWorld)
						{
							this.onDirectionChanged(direction);
						}
						break;
					}
					case 40002:
						stream.readUint32();
						break;
					default:
						switch (properUtype2)
						{
						case 41003:
						{
							string oldValue4 = this.name;
							this.name = stream.readUnicode();
							if (property.isBase())
							{
								if (this.inited)
								{
									this.onNameChanged(oldValue4);
								}
							}
							else if (this.inWorld)
							{
								this.onNameChanged(oldValue4);
							}
							break;
						}
						case 41004:
						{
							uint oldValue5 = this.uid;
							this.uid = stream.readUint32();
							if (property.isBase())
							{
								if (this.inited)
								{
									this.onUidChanged(oldValue5);
								}
							}
							else if (this.inWorld)
							{
								this.onUidChanged(oldValue5);
							}
							break;
						}
						case 41005:
						{
							uint oldValue6 = this.utype;
							this.utype = stream.readUint32();
							if (property.isBase())
							{
								if (this.inited)
								{
									this.onUtypeChanged(oldValue6);
								}
							}
							else if (this.inWorld)
							{
								this.onUtypeChanged(oldValue6);
							}
							break;
						}
						case 41006:
						{
							uint oldValue7 = this.modelID;
							this.modelID = stream.readUint32();
							if (property.isBase())
							{
								if (this.inited)
								{
									this.onModelIDChanged(oldValue7);
								}
							}
							else if (this.inWorld)
							{
								this.onModelIDChanged(oldValue7);
							}
							break;
						}
						case 41007:
						{
							byte oldValue8 = this.modelScale;
							this.modelScale = stream.readUint8();
							if (property.isBase())
							{
								if (this.inited)
								{
									this.onModelScaleChanged(oldValue8);
								}
							}
							else if (this.inWorld)
							{
								this.onModelScaleChanged(oldValue8);
							}
							break;
						}
						default:
							if (properUtype2 == 51007)
							{
								uint oldValue9 = this.entityNO;
								this.entityNO = stream.readUint32();
								if (property.isBase())
								{
									if (this.inited)
									{
										this.onEntityNOChanged(oldValue9);
									}
								}
								else if (this.inWorld)
								{
									this.onEntityNOChanged(oldValue9);
								}
							}
							break;
						}
						break;
					}
				}
			}
		}

		// Token: 0x06005BCA RID: 23498 RVA: 0x002517A0 File Offset: 0x0024F9A0
		public override void callPropertysSetMethods()
		{
			Dictionary<ushort, Property> idpropertys = EntityDef.moduledefs["DroppedItem"].idpropertys;
			uint oldValue = this.dialogID;
			Property property = idpropertys[4];
			if (property.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onDialogIDChanged(oldValue);
				}
			}
			else if (this.inWorld && (!property.isOwnerOnly() || base.isPlayer()))
			{
				this.onDialogIDChanged(oldValue);
			}
			Vector3 direction = this.direction;
			Property property2 = idpropertys[2];
			if (property2.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onDirectionChanged(direction);
				}
			}
			else if (this.inWorld && (!property2.isOwnerOnly() || base.isPlayer()))
			{
				this.onDirectionChanged(direction);
			}
			uint oldValue2 = this.entityNO;
			Property property3 = idpropertys[5];
			if (property3.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onEntityNOChanged(oldValue2);
				}
			}
			else if (this.inWorld && (!property3.isOwnerOnly() || base.isPlayer()))
			{
				this.onEntityNOChanged(oldValue2);
			}
			uint oldValue3 = this.itemCount;
			Property property4 = idpropertys[6];
			if (property4.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onItemCountChanged(oldValue3);
				}
			}
			else if (this.inWorld && (!property4.isOwnerOnly() || base.isPlayer()))
			{
				this.onItemCountChanged(oldValue3);
			}
			int oldValue4 = this.itemId;
			Property property5 = idpropertys[7];
			if (property5.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onItemIdChanged(oldValue4);
				}
			}
			else if (this.inWorld && (!property5.isOwnerOnly() || base.isPlayer()))
			{
				this.onItemIdChanged(oldValue4);
			}
			uint oldValue5 = this.modelID;
			Property property6 = idpropertys[8];
			if (property6.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onModelIDChanged(oldValue5);
				}
			}
			else if (this.inWorld && (!property6.isOwnerOnly() || base.isPlayer()))
			{
				this.onModelIDChanged(oldValue5);
			}
			byte oldValue6 = this.modelScale;
			Property property7 = idpropertys[9];
			if (property7.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onModelScaleChanged(oldValue6);
				}
			}
			else if (this.inWorld && (!property7.isOwnerOnly() || base.isPlayer()))
			{
				this.onModelScaleChanged(oldValue6);
			}
			string oldValue7 = this.name;
			Property property8 = idpropertys[10];
			if (property8.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onNameChanged(oldValue7);
				}
			}
			else if (this.inWorld && (!property8.isOwnerOnly() || base.isPlayer()))
			{
				this.onNameChanged(oldValue7);
			}
			Vector3 position = this.position;
			Property property9 = idpropertys[1];
			if (property9.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onPositionChanged(position);
				}
			}
			else if (this.inWorld && (!property9.isOwnerOnly() || base.isPlayer()))
			{
				this.onPositionChanged(position);
			}
			uint oldValue8 = this.uid;
			Property property10 = idpropertys[11];
			if (property10.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onUidChanged(oldValue8);
				}
			}
			else if (this.inWorld && (!property10.isOwnerOnly() || base.isPlayer()))
			{
				this.onUidChanged(oldValue8);
			}
			uint oldValue9 = this.utype;
			Property property11 = idpropertys[12];
			if (property11.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onUtypeChanged(oldValue9);
					return;
				}
			}
			else if (this.inWorld && (!property11.isOwnerOnly() || base.isPlayer()))
			{
				this.onUtypeChanged(oldValue9);
			}
		}

		// Token: 0x04005A16 RID: 23062
		public EntityBaseEntityCall_DroppedItemBase baseEntityCall;

		// Token: 0x04005A17 RID: 23063
		public EntityCellEntityCall_DroppedItemBase cellEntityCall;

		// Token: 0x04005A18 RID: 23064
		public uint dialogID;

		// Token: 0x04005A19 RID: 23065
		public uint entityNO;

		// Token: 0x04005A1A RID: 23066
		public uint itemCount;

		// Token: 0x04005A1B RID: 23067
		public int itemId;

		// Token: 0x04005A1C RID: 23068
		public uint modelID;

		// Token: 0x04005A1D RID: 23069
		public byte modelScale = 30;

		// Token: 0x04005A1E RID: 23070
		public string name = "";

		// Token: 0x04005A1F RID: 23071
		public uint uid;

		// Token: 0x04005A20 RID: 23072
		public uint utype;
	}
}
