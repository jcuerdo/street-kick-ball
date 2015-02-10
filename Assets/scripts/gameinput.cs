using UnityEngine;


public class GameInput 
{
	private Vector2 firstPressPos;
	private Vector2 secondPressPos;
	const float MOVE_VELOCITY = 0.15f;
	
	public float getTouch()
	{
		float result = 0;
		if(Input.touches.Length > 0)	
		{
			Touch t = Input.GetTouch(0);
			
			if(t.phase == TouchPhase.Began)
			{
				firstPressPos = new Vector2(t.position.x,t.position.y);
			}
			
			if(t.phase == TouchPhase.Moved)	
			{
				result = secondPressPos.x - firstPressPos.x;
				firstPressPos = secondPressPos;
			}
		}
		if( result == 0 )
		{
			return 0;
		}
		return result > 0 ? 0.1f : -0.1f;
	}

	public void moveMainCharacter( Transform character )
	{
		if ( Input.GetKey( "left" ) ) 
		{  
			this.move( character, -1 );
		}
		if ( Input.GetKey( "right" ) ) 
		{    
			this.move( character, 1 );
		}
	}

	public void move( Transform character, int direction = 0 )
	{
		Vector3 pos = character.position;
		GameObject left = (GameObject) GameObject.Find( "left" );
		GameObject right = (GameObject) GameObject.Find( "right" );
		if ( ( character.rigidbody.position.x >= left.rigidbody.position.x + 1f && direction < 0 ) 
		    || character.rigidbody.position.x <= right.rigidbody.position.x - 1f && direction > 0 )
		{
			pos.x += MOVE_VELOCITY * direction;
		}
		character.position = pos;
	}
}
