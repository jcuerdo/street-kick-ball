using UnityEngine;
using System.Collections;

public class player : MonoBehaviour 
{
	private GameInput game_input;
	public Game game;
	private AdMob admob;
	
	void Start () 
	{
		this.game_input =  new GameInput();
		this.game = new Game();
		this.admob = new AdMob();
		this.admob.requestBanner();
		this.admob.requestBannerInterstitial();
	}

	void Update()
	{
		if( !game.isGameOver() )
		{
			this.game.updateGame();
			this.game_input.moveMainCharacter( transform );
			GameObject.Find( "score" ).GetComponent<GUIText>().text = this.game.getScore().ToString();
		}
		if(game.isStarted())
		{
			this.admob.hideBanners();
		}
	}
	
	void OnGUI()
	{
		GUIStyle text_style =  new GUIStyle();
		text_style.fontSize = Screen.width/20;
		text_style.alignment = TextAnchor.MiddleCenter;
		text_style.normal.textColor = Color.white;


		GUIStyle button_style = new GUIStyle(GUI.skin.button);
		button_style.fontSize = Screen.width/20;

		Texture2D left = (Texture2D)(Resources.Load( "left" ));
		Texture2D right = (Texture2D)(Resources.Load( "right" ));
		if ( !this.game.isStarted() )
		{
			this.admob.showBanners();
			this.admob.showInterstitial();
			int best_score = PlayerPrefs.GetInt( "best_score" );
			int last_score = PlayerPrefs.GetInt( "last_score" );

			GUI.Box(new Rect (Screen.width/4,Screen.height/4 - 5, Screen.width/2 , Screen.height/2 ), "" );
			GUI.Box(new Rect( Screen.width/2 - Screen.width/6,Screen.height/4,Screen.width/2 - Screen.width/6,Screen.height/8), "Best: " + best_score + " Last: " + last_score, text_style );
			if( GUI.Button(new Rect( Screen.width/2 - Screen.width/6,Screen.height/4 + (Screen.height/8) ,Screen.width/2 - Screen.width/6,Screen.height/8), "Start Game",button_style )) 
			{
				this.game.startGame();
			}
			if( GUI.Button(new Rect( Screen.width/2 - Screen.width/6,Screen.height/4 + (Screen.height/8*2) + 20,Screen.width/2 - Screen.width/6,Screen.height/8), "Quit",button_style )) 
			{
				Application.Quit();
			}
		}
		else
		{
			if( this.game.isGameOver() )
			{
				if( GUI.Button(new Rect( Screen.width/2 - Screen.width/6,Screen.height/2 - Screen.height/6,Screen.width/2 - Screen.width/6,Screen.height/8), "Back to menu",button_style )) 
				{
					Application.LoadLevel(0);
				}
			}
			else
			{
				if( GUI.RepeatButton(new Rect( 5, Screen.height * 6.6f / 8,Screen.width/7,Screen.height/6 ),left, new GUIStyle() )) 
				{
					game_input.move( transform, -1 );
				}
				if( GUI.RepeatButton(new Rect(Screen.width - Screen.width/7 + 10, Screen.height * 6.6f / 8,Screen.width/7,Screen.height/6 ),right,new GUIStyle() )) 
				{
					game_input.move( transform, 1 );
				}
			}
		}
	}
}
