using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.CNZ
{
	class Eggman : ObjectDefinition
	{
		private Sprite sprite;

		public override void Init(ObjectData data)
		{
			Sprite[] sprites = new Sprite[2];
			
			if (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1] == '4')
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("CNZ/Objects.gif");
				sprites[0] = new Sprite(sheet.GetSection(76, 140, 44, 16), -28, -16);
				sprites[1] = new Sprite(sheet.GetSection(175, 183, 80, 72), -40, -40);
			}
			else
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("MBZ/Objects.gif");
				sprites[0] = new Sprite(sheet.GetSection(1, 5, 44, 16), -28, -16);
				sprites[1] = new Sprite(sheet.GetSection(232, 112, 80, 72), -40, -40);
			}
			
			sprite = new Sprite(sprites);
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
			get { return sprite;; }
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