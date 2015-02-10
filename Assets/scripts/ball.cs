using UnityEngine;
using System.Collections;

public class ball : MonoBehaviour {

	private Vector2 velocity;
	const float REBOUND_FACTOR = -1.4f;
	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void FixedUpdate() {
		this.velocity = rigidbody.velocity;
	}

	void OnCollisionEnter( Collision otherObject )
	{
		switch( otherObject.gameObject.name )
		{
		case "down":
			this.velocity.y = this.velocity.y / REBOUND_FACTOR;
			rigidbody.velocity = this.velocity ;
			break;
		case "right":
		case "left":
			this.velocity.x = this.velocity.x / REBOUND_FACTOR;
			rigidbody.velocity = this.velocity ;
			break;
		case "player":
			Vector2 contact1 = otherObject.contacts[0].point;
			Vector2 contact2 = otherObject.contacts[1].point;
			Vector2 player_position = otherObject.gameObject.rigidbody.position;
			if( contact1.y >= otherObject.rigidbody.position.y + 1.5f )
			{
				this.velocity = Vector2.up * 7.5f;
				this.velocity.x = ( ( ( contact1.x + contact2.x ) / 2 ) - player_position.x ) * 15f;
				rigidbody.velocity = this.velocity;
				player p = (player) otherObject.gameObject.GetComponent( "player" );
				p.game.incrementScore();
			}
			else
			{
				this.velocity.x = this.velocity.x / REBOUND_FACTOR;
				rigidbody.velocity = this.velocity;
			}

			break;

		}
	}
}
