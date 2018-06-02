using UnityEngine;

public class CameraManager : MonoBehaviour {

	public Transform _mainCameraTransform;

	private const float MOVEMENT_MARGIN = 0.95f;

	private float _velocity = 0;
	private float _acceleration = 0.1f;
	
	private const int MAX_SCROLLING_SPEED = 10;

	

	void Start ()
	{
		_mainCameraTransform = GetComponent<Transform>();
	}
	
	void Update ()
	{
		CameraMovement();
	}

	private void CameraMovement()
	{
		bool IsAtScreenEdge = false;
		if(Input.mousePosition.y > Screen.height * MOVEMENT_MARGIN)
		{
			float currentVelocity = _velocity + Time.deltaTime * _acceleration;
			_velocity = currentVelocity > MAX_SCROLLING_SPEED ? MAX_SCROLLING_SPEED : currentVelocity;
			_mainCameraTransform.Translate(Vector3.up * _velocity, Space.World);
			IsAtScreenEdge = true;
		}
		else if(Input.mousePosition.y < Screen.height - Screen.height * MOVEMENT_MARGIN)
		{
			float currentVelocity = _velocity + Time.deltaTime * _acceleration;
			_velocity = currentVelocity > MAX_SCROLLING_SPEED ? MAX_SCROLLING_SPEED : currentVelocity;
			_mainCameraTransform.Translate(Vector3.down * _velocity, Space.World);
			IsAtScreenEdge = true;
		}

		if (Input.mousePosition.x > Screen.width * MOVEMENT_MARGIN)
		{
			float currentVelocity = _velocity + Time.deltaTime * _acceleration;
			_velocity = currentVelocity > MAX_SCROLLING_SPEED ? MAX_SCROLLING_SPEED : currentVelocity;
			_mainCameraTransform.Translate(Vector3.right * _velocity, Space.World);
			IsAtScreenEdge = true;
		}
		else if (Input.mousePosition.x < Screen.width - Screen.width * MOVEMENT_MARGIN)
		{
			float currentVelocity = _velocity + Time.deltaTime * _acceleration;
			_velocity = currentVelocity > MAX_SCROLLING_SPEED ? MAX_SCROLLING_SPEED : currentVelocity;
			_mainCameraTransform.Translate(Vector3.left * _velocity, Space.World);
			IsAtScreenEdge = true;
		}

		if(!IsAtScreenEdge)
		{
			_velocity = 0;
		}
	}
}
