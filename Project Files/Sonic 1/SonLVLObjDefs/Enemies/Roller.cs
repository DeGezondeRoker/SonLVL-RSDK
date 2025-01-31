using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S1ObjectDefinitions.Enemies
{
	class Roller : ObjectDefinition
	{
		private Sprite sprite;

		public override void Init(ObjectData data)
		{
			switch (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1])
			{
				case '3':
				default:
					sprite = new Sprite(LevelData.GetSpriteSheet("SYZ/Objects.gif").GetSection(1, 179, 29, 47), -16, -33);
					break;
				case '7':
					sprite = new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(105, 30, 29, 47), -16, -33);
					break;
			}
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
		}
		
		public override string SubtypeName(byte subtype)
		{
			return subtype + "";
		}

		public override Sprite Image
		{
			get { return sprite; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprite;
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprite;
		}
	}
}