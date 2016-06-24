using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace monogameTest
{
	public class HUD
	{
		public int playerScore, screenWidth, screenHeight, highScoreE, highScoreM, highScoreH;
		public SpriteFont scoreFont;
		public Vector2 scorePos;
		public Difficulty diff = new Difficulty();

		public HUD ()
		{
			playerScore = 0;
			screenHeight = 640;
			screenWidth = 480;
			scoreFont = null;
			scorePos = new Vector2 (screenWidth / 2, 10);
		}

		public void LoadContent(ContentManager Content)
		{
			scoreFont = Content.Load<SpriteFont> ("Decker");
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.DrawString (scoreFont, "Score : " + playerScore, scorePos, Color.White);
		}

		public void LoadHighScoreE()
		{
			if (File.Exists ("highscoreE.txt")) 
			{
				highScoreE = int.Parse (File.ReadAllText ("highscoreE.txt"));
			} 
		}
		public void LoadHighScoreM()
		{
			if (File.Exists ("highscoreM.txt")) 
			{
				highScoreM = int.Parse (File.ReadAllText ("highscoreM.txt"));
			}

		}
		public void LoadHighScoreH()
		{
			if (File.Exists ("highscoreH.txt")) 
			{
				highScoreH = int.Parse (File.ReadAllText ("highscoreH.txt"));
			} 
		}
		public void InitHScoreFile()
		{
			if (!File.Exists ("highscoreE.txt"))
				File.WriteAllText ("highscoreE.txt", "0");
			if (!File.Exists ("highscoreM.txt"))
				File.WriteAllText ("highscoreM.txt", "0");
			if (!File.Exists ("highscoreH.txt"))
				File.WriteAllText ("highscoreH.txt", "0");
		}
	}
}

