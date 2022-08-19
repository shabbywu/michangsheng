using System;
using System.Collections.Generic;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000B68 RID: 2920
	public abstract class DroppedItemBase : Entity
	{
		// Token: 0x06005179 RID: 20857 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onDialogIDChanged(uint oldValue)
		{
		}

		// Token: 0x0600517A RID: 20858 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onEntityNOChanged(uint oldValue)
		{
		}

		// Token: 0x0600517B RID: 20859 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onItemCountChanged(uint oldValue)
		{
		}

		// Token: 0x0600517C RID: 20860 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onItemIdChanged(int oldValue)
		{
		}

		// Token: 0x0600517D RID: 20861 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onModelIDChanged(uint oldValue)
		{
		}

		// Token: 0x0600517E RID: 20862 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onModelScaleChanged(byte oldValue)
		{
		}

		// Token: 0x0600517F RID: 20863 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onNameChanged(string oldValue)
		{
		}

		// Token: 0x06005180 RID: 20864 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onUidChanged(uint oldValue)
		{
		}

		// Token: 0x06005181 RID: 20865 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onUtypeChanged(uint oldValue)
		{
		}

		// Token: 0x06005182 RID: 20866 RVA: 0x0022209A File Offset: 0x0022029A
		public DroppedItemBase()
		{
		}

		// Token: 0x06005183 RID: 20867 RVA: 0x00004095 File Offset: 0x00002295
		public override void onComponentsEnterworld()
		{
		}

		// Token: 0x06005184 RID: 20868 RVA: 0x00004095 File Offset: 0x00002295
		public override void onComponentsLeaveworld()
		{
		}

		// Token: 0x06005185 RID: 20869 RVA: 0x002220B5 File Offset: 0x002202B5
		public override void onGetBase()
		{
			this.baseEntityCall = new EntityBaseEntityCall_DroppedItemBase(this.id, this.className);
		}

		// Token: 0x06005186 RID: 20870 RVA: 0x002220CE File Offset: 0x002202CE
		public override void onGetCell()
		{
			this.cellEntityCall = new EntityCellEntityCall_DroppedItemBase(this.id, this.className);
		}

		// Token: 0x06005187 RID: 20871 RVA: 0x002220E7 File Offset: 0x002202E7
		public override void onLoseCell()
		{
			this.cellEntityCall = null;
		}

		// Token: 0x06005188 RID: 20872 RVA: 0x002220F0 File Offset: 0x002202F0
		public override EntityCall getBaseEntityCall()
		{
			return this.baseEntityCall;
		}

		// Token: 0x06005189 RID: 20873 RVA: 0x002220F8 File Offset: 0x002202F8
		public override EntityCall getCellEntityCall()
		{
			return this.cellEntityCall;
		}

		// Token: 0x0600518A RID: 20874 RVA: 0x00004095 File Offset: 0x00002295
		public override void attachComponents()
		{
		}

		// Token: 0x0600518B RID: 20875 RVA: 0x00004095 File Offset: 0x00002295
		public override void detachComponents()
		{
		}

		// Token: 0x0600518C RID: 20876 RVA: 0x00222100 File Offset: 0x00220300
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

		// Token: 0x0600518D RID: 20877 RVA: 0x00222180 File Offset: 0x00220380
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

		// Token: 0x0600518E RID: 20878 RVA: 0x002225B8 File Offset: 0x002207B8
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

		// Token: 0x04004F8B RID: 20363
		public EntityBaseEntityCall_DroppedItemBase baseEntityCall;

		// Token: 0x04004F8C RID: 20364
		public EntityCellEntityCall_DroppedItemBase cellEntityCall;

		// Token: 0x04004F8D RID: 20365
		public uint dialogID;

		// Token: 0x04004F8E RID: 20366
		public uint entityNO;

		// Token: 0x04004F8F RID: 20367
		public uint itemCount;

		// Token: 0x04004F90 RID: 20368
		public int itemId;

		// Token: 0x04004F91 RID: 20369
		public uint modelID;

		// Token: 0x04004F92 RID: 20370
		public byte modelScale = 30;

		// Token: 0x04004F93 RID: 20371
		public string name = "";

		// Token: 0x04004F94 RID: 20372
		public uint uid;

		// Token: 0x04004F95 RID: 20373
		public uint utype;
	}
}
