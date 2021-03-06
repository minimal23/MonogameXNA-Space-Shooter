﻿using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace monogameTest
{
	public enum GameState
	{
		Menu,
		Difficulty,
		Play,
		GameOver
	}

	public enum Difficulty
	{
		Easy,
		Medium,
		Hard
	}

	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		Player p = new Player();
		BG bg = new BG();
		SoundManager sm = new SoundManager ();
		List<Enemy> enemyList = new List<Enemy>();
		List<Explosion> explosionList = new List<Explosion>();
		Random random = new Random();
		HUD hud = new HUD();
		GameState gameState = new GameState();
		Texture2D gamemenu, gameover, gamedifficulty;
		SpriteFont goscoreFont;

		public Game1 ()
		{
			graphics = new GraphicsDeviceManager(this);
			graphics.IsFullScreen = false;
			graphics.PreferredBackBufferHeight = 640;
			graphics.PreferredBackBufferWidth = 480;
			this.Window.Title = "Prepare to die !";
			Content.RootDirectory = "Content";
			gameState = GameState.Menu;
		}

		protected override void Initialize ()
		{
			// TODO: Add your initialization logic here
            
			base.Initialize ();
		}

		protected override void LoadContent ()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);
			p.LoadContent(Content);
			bg.LoadContent(Content);
			hud.LoadContent (Content);
			sm.LoadContent (Content);
			gamemenu = Content.Load<Texture2D> ("Gamemenu");
			gameover = Content.Load<Texture2D> ("Gameover");
			gamedifficulty = Content.Load<Texture2D> ("gamedifficulty");
			goscoreFont = Content.Load<SpriteFont> ("Decker");

			//TODO: use this.Content to load your game content here 
		}

		protected override void Update (GameTime gameTime)
		{
			// For Mobile devices, this logic will close the Game when the Back button is pressed
			// Exit() is obsolete on iOS
			#if !__IOS__ &&  !__TVOS__
			if (GamePad.GetState (PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState ().IsKeyDown (Keys.Escape))
				Exit ();
			#endif
            
			switch (gameState) 
			{
			case GameState.Menu:
				{
					KeyboardState keystate1 = Keyboard.GetState ();
					if (keystate1.IsKeyDown (Keys.Enter)) 
					{
						gameState = GameState.Difficulty;
					}
					bg.speed = 1;
					bg.Update (gameTime);
					break;
				}
			case GameState.Difficulty:
				{
					hud.InitHScoreFile ();
					KeyboardState keystate2 = Keyboard.GetState ();
					if (keystate2.IsKeyDown (Keys.D1))
					{
						hud.diff = Difficulty.Easy;
						hud.LoadHighScoreE ();
						gameState = GameState.Play;
						MediaPlayer.Play (sm.bgM);
					}
					if (keystate2.IsKeyDown (Keys.D2))
					{
						hud.diff = Difficulty.Medium;
						hud.LoadHighScoreM ();
						gameState = GameState.Play;
						MediaPlayer.Play (sm.bgM);
					}
					if (keystate2.IsKeyDown (Keys.D3))
					{
						hud.diff = Difficulty.Hard;
						hud.LoadHighScoreH ();
						gameState = GameState.Play;
						MediaPlayer.Play (sm.bgM);
					}
					bg.speed = 1;
					bg.Update (gameTime);
					break;
				}
			case GameState.Play:
				{
					bg.speed = 5;
					foreach (Enemy e in enemyList)
					{
						for (int i = 0; i < e.bulletList.Count; i++)
						{
							if (p.boundingBox.Intersects(e.bulletList[i].boundingBox))
							{
								e.bulletList[i].isVisible = false;
								if (hud.diff == Difficulty.Medium || hud.diff == Difficulty.Easy)
									p.health -= 10;
								if (hud.diff == Difficulty.Hard)
									p.health -= 20;
							}
						}

						for (int i=0;i<p.bulletList.Count;i++)
						{
							if (e.boundingBox.Intersects(p.bulletList[i].boundingBox))
							{
								sm.explosionSound.Play ();
								explosionList.Add(new Explosion(Content.Load<Texture2D>("explosion3"), new Vector2(e.position.X,e.position.Y)));
								p.bulletList[i].isVisible = false;
								if (hud.diff == Difficulty.Easy)
									e.hp -= 100;
								if (hud.diff == Difficulty.Medium || hud.diff == Difficulty.Hard)
									e.hp -= 50;
							}
						}
						if (e.hp <= 0) 
						{
							e.isVisible = false;
							hud.playerScore += 5;
						}
						e.Update(gameTime);
					}
					foreach (Explosion ex in explosionList)
						ex.Update (gameTime);
					p.Update(gameTime);
					bg.Update(gameTime);
					LoadEnemies();
					ManageExplosions ();
					if (p.health <= 0)
					{
						if (hud.diff == Difficulty.Easy) 
						{
							StreamWriter writer = new StreamWriter ("highscoreE.txt");
							if (hud.highScoreE < hud.playerScore) 
							{
								hud.highScoreE = hud.playerScore;
								writer.WriteLine (hud.playerScore);
							}
							writer.Close ();
						}
						if (hud.diff == Difficulty.Medium) 
						{
							StreamWriter writer = new StreamWriter ("highscoreM.txt");
							if (hud.highScoreM < hud.playerScore) 
							{
								hud.highScoreM = hud.playerScore;
								writer.WriteLine (hud.playerScore);
							}
							writer.Close ();
						}
						if (hud.diff == Difficulty.Hard) 
						{
							StreamWriter writer = new StreamWriter ("highscoreH.txt");
							if (hud.highScoreH < hud.playerScore) 
							{
								hud.highScoreH = hud.playerScore;
								writer.WriteLine (hud.playerScore);
							}
							writer.Close ();
						}
						gameState = GameState.GameOver;
					}
					break;
				}
			case GameState.GameOver:
				{
					KeyboardState keystate2 = Keyboard.GetState ();
					if (keystate2.IsKeyDown (Keys.R)) 
					{
						p.position = new Vector2(210, 500);
						enemyList.Clear ();
						p.health = 100;
						hud.playerScore = 0;
						gameState = GameState.Menu;
					}
					bg.speed = 1;
					bg.Update (gameTime);
					MediaPlayer.Stop ();
					break;
				}
			}

			// TODO: Add your update logic here

			base.Update (gameTime);
		}

		protected override void Draw (GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear (Color.CornflowerBlue);
            
			//TODO: Add your drawing code here
			spriteBatch.Begin();

			switch (gameState) 
			{
			case GameState.Menu:
				{
					bg.Draw(spriteBatch);
					spriteBatch.Draw (gamemenu, new Vector2 (0, 0), Color.White);
					break;
				}
			case GameState.Difficulty:
				{
					bg.Draw (spriteBatch);
					spriteBatch.Draw (gamedifficulty, new Vector2 (0, 0), Color.White);
					break;
				}
			case GameState.Play:
				{
					bg.Draw(spriteBatch);
					p.Draw(spriteBatch);
					foreach (Enemy e in enemyList)
					{
						e.Draw(spriteBatch);
					}
					foreach (Explosion ex in explosionList) 
					{
						ex.Draw (spriteBatch);
					}
					hud.Draw (spriteBatch);
					break;
				}
			case GameState.GameOver:
				{
					bg.Draw(spriteBatch);
					spriteBatch.Draw (gameover, new Vector2 (0, 0), Color.White);
					spriteBatch.DrawString (goscoreFont, "Your score was : " + hud.playerScore, new Vector2 (160, 300), Color.White);
					if (hud.diff == Difficulty.Easy)
						spriteBatch.DrawString (goscoreFont, "High score : " + hud.highScoreE, new Vector2 (160, 320), Color.White);
					if (hud.diff == Difficulty.Medium)
						spriteBatch.DrawString (goscoreFont, "High score : " + hud.highScoreM, new Vector2 (160, 320), Color.White);
					if (hud.diff == Difficulty.Hard)
						spriteBatch.DrawString (goscoreFont, "High score : " + hud.highScoreH, new Vector2 (160, 320), Color.White);
					spriteBatch.DrawString (goscoreFont, "Difficulty : " + hud.diff.ToString(), new Vector2 (160, 340), Color.White);
					break;
				}
			}

			spriteBatch.End();
			base.Draw (gameTime);
		}

		public void LoadEnemies()
		{
			int n;
			int rndX = random.Next(0, 380);
			int rndY = random.Next(-500, -100);
			if (hud.diff == Difficulty.Easy || hud.diff == Difficulty.Medium)
				n = 5;
			else
				n = 7;
			if(enemyList.Count < n)
			{
				enemyList.Add(new Enemy(Content.Load<Texture2D>("enemy.png"), new Vector2(rndX, rndY), Content.Load<Texture2D>("enemybullet.png")));
			}

			for (int i=0;i<enemyList.Count;i++)
			{
				if (!enemyList[i].isVisible)
				{
					enemyList.RemoveAt(i);
					i--;
				}
			}
		}

		public void ManageExplosions()
		{
			for (int i=0;i<explosionList.Count;i++)
			{
				if (!explosionList[i].isVisible)
				{
					explosionList.RemoveAt(i);
					i--;
				}
			}
		}
	}
}

