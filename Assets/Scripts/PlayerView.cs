using UnityEngine;


public class PlayerView : MonoBehaviour
{
	public void SetPosition( Vector2 pos )
	{
		GetComponent< Rigidbody2D >().MovePosition( pos );
	}
}

