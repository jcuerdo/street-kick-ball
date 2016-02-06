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
		this.velocity = GetComponent<Rigidbody>().velocity;
	}

	void OnCollisionEnter( Collision otherObject )
	{
		player player = GameObject.Find( "player" ).GetComponent<player>();

		switch( otherObject.gameObject.name )
		{
		case "down":
			this.velocity.y = this.velocity.y / REBOUND_FACTOR;
			GetComponent<Rigidbody>().velocity = this.velocity ;
			player.game.gameOver();

			break;
		case "right":
		case "left":
			this.velocity.x = this.velocity.x / REBOUND_FACTOR;
			GetComponent<Rigidbody>().velocity = this.velocity ;
			break;
		case "player":
			Vector2 playerVelocity = otherObject.gameObject.GetComponent<Rigidbody>().velocity;
			Vector2 contact = otherObject.contacts[0].point;
			Vector2 playerPosition = otherObject.gameObject.GetComponent<Rigidbody>().position;

			if( contact.y >= (playerPosition.y))
			{
				this.velocity = Vector2.up * 7.5f;

				this.velocity.x = (contact.x + playerVelocity.x) * 2f;
				GetComponent<Rigidbody>().velocity = this.velocity;
				player.game.incrementScore();
			}
			break;


		}
	}
}
