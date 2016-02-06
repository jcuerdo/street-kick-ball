using UnityEngine;
using System.Collections;

public class Game: MonoBehaviour 
{
	private bool game_over = false; 
	private bool game_started = false;
	private int score = 0;

	public Game()
	{
		Time.timeScale = 0;
	}

	public void updateGame()
	{
		this.testGameOver();
	}

	private void testGameOver()
	{
		if(this.game_over )
		{
			setScoreRecord();
		}
	}

	private void setScoreRecord()
	{
		int best_score = PlayerPrefs.GetInt( "best_score" );

		if ( this.score >= best_score )
		{
			PlayerPrefs.SetInt( "best_score", this.score );
		}
		PlayerPrefs.SetInt( "last_score", score );
	}

	public bool isGameOver()
	{
		return this.game_over;
	}

	public bool isStarted()
	{
		return this.game_started;
	}

	public void startGame()
	{
		Time.timeScale = 1;
		this.game_started = true;
	}

	public void incrementScore( int increment = 1 )
	{
		this.score += increment;
	}

	public int getScore()
	{
		return this.score;	
	}

	public void gameOver()
	{
		this.game_over = true;
	}
}
