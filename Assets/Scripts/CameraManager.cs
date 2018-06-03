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
			if(ReachedUpperMapEdgeY())
			{
				return;
			}

			float currentVelocity = _velocity + Time.deltaTime * _acceleration;
			_velocity = currentVelocity > MAX_SCROLLING_SPEED ? MAX_SCROLLING_SPEED : currentVelocity;
			_mainCameraTransform.Translate(Vector3.up * _velocity, Space.World);
			IsAtScreenEdge = true;
		}
		else if(Input.mousePosition.y < Screen.height - Screen.height * MOVEMENT_MARGIN)
		{
			if (ReachedLowerMapEdgeY())
			{
				return;
			}

			float currentVelocity = _velocity + Time.deltaTime * _acceleration;
			_velocity = currentVelocity > MAX_SCROLLING_SPEED ? MAX_SCROLLING_SPEED : currentVelocity;
			_mainCameraTransform.Translate(Vector3.down * _velocity, Space.World);
			IsAtScreenEdge = true;
		}

		if (Input.mousePosition.x > Screen.width * MOVEMENT_MARGIN)
		{
			if (ReachedRightMapEdgeX())
			{
				return;
			}

			float currentVelocity = _velocity + Time.deltaTime * _acceleration;
			_velocity = currentVelocity > MAX_SCROLLING_SPEED ? MAX_SCROLLING_SPEED : currentVelocity;
			_mainCameraTransform.Translate(Vector3.right * _velocity, Space.World);
			IsAtScreenEdge = true;
		}
		else if (Input.mousePosition.x < Screen.width - Screen.width * MOVEMENT_MARGIN)
		{
			if (ReachedLeftMapEdgeX())
			{
				return;
			}

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

	public bool ReachedRightMapEdgeX()
	{
		if(Globals.CurrentMapSize.x / 2.0f <= _mainCameraTransform.position.x)
		{
			return true;
		}

		return false;
	}

	public bool ReachedLeftMapEdgeX()
	{
		if (-(Globals.CurrentMapSize.x / 2.0f) >= _mainCameraTransform.position.x)
		{
			return true;
		}

		return false;
	}

	public bool ReachedUpperMapEdgeY()
	{
		if (Globals.CurrentMapSize.y / 2.0f <= _mainCameraTransform.position.y)
		{
			return true;
		}

		return false;
	}

	public bool ReachedLowerMapEdgeY()
	{
		if (-(Globals.CurrentMapSize.y / 2.0f) >= _mainCameraTransform.position.y)
		{
			return true;
		}

		return false;
	}
}
