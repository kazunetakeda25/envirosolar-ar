using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	private float xAxisValue;
	private float zAxisValue;
	private float yAxisValue;

	public float mouseSensitivity = 10.0f;
	public float moveSensitivity  = 1.0f;
	public float upDownSensitivity = 5.0f;
	private float clampAngle = 80.0f;

	private float rotY = 0.0f;
	private float rotX = 0.0f;
	private float sign = 0.0f;

	public float force = 10.0f;

	private int x;
	private int y;

	private Camera cam;
	private Ray ray;
	private RaycastHit hit;


	void Start ()
	{
		Vector3 rot = transform.localRotation.eulerAngles;
		rotY = rot.y;
		rotX = rot.x;
		Cursor.lockState = CursorLockMode.Locked;

		x = Screen.width / 2;
		y = Screen.height / 2;

		cam = GetComponent<Camera>();

	}
	void Update()
	{
		if(Input.GetMouseButtonDown(0))
		{
			ray = cam.ScreenPointToRay(new Vector3(x, y));
			if (Physics.Raycast(ray, out hit))
			{
				if (hit.collider != null && hit.collider.GetComponentInParent<Transform>().tag == "balls")
				{
					hit.collider.GetComponentInParent<Rigidbody>().AddForce(transform.forward * force, ForceMode.Impulse);
				}
			}
		}
	}

	void LateUpdate()
	{
		yAxisValue = 0.0f;
		
		if (Input.GetKey(KeyCode.R))
		{
			yAxisValue = (+upDownSensitivity);
			yAxisValue *= Time.deltaTime;
		}
		else if(Input.GetKey(KeyCode.F))
		{
			yAxisValue = (-upDownSensitivity);
			yAxisValue *= Time.deltaTime;
		}

		rotX -= Input.GetAxis("Mouse Y") * mouseSensitivity;
		rotY += Input.GetAxis("Mouse X") * mouseSensitivity;

		// Up and down limit rotation
		rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);


		sign = Mathf.Sign (rotY);
		rotY = Mathf.Repeat (Mathf.Abs(rotY), 360.0f);
		rotY *= sign;

		xAxisValue = Input.GetAxis("Horizontal")*moveSensitivity;
		zAxisValue = Input.GetAxis("Vertical")*moveSensitivity;

		transform.rotation = Quaternion.Euler(rotX, rotY, 0.0f);
		transform.Translate(new Vector3(xAxisValue*0.1f, yAxisValue, zAxisValue*0.1f));
	}
}