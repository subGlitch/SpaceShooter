using UnityEngine;


public class PlayerInputView : MonoBehaviour
{
	public delegate void MoveEvent( Vector2Int dir );
	public event MoveEvent OnMove;


	void Update()
	{
		if (Input.GetKeyDown( KeyCode.D ))
			OnMove( Vector2Int.right );
	}
}

