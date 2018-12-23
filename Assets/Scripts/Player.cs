using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	
	public float Speed = 5f;
	public float JumpHeight = 2f;

    public float GroundDistance = 0.2f;
	public LayerMask Ground;
	private bool _isGrounded = true;
	private Transform _groundChecker;


	private Vector3 _inputs = Vector3.zero;
	private Rigidbody Pbody;

    
	public GameObject Cam;
	private Vector3 CamRoation = Vector3.zero;
	private Vector3 Mouse;
	public float lookSensitivy = 3;
	private Vector3 rotation = Vector3.zero;
	private float minPitch = -30f;
	private float maxPitch = 60f;

	// Use this for initialization
	void Start ()
	{
		
		Pbody = GetComponent<Rigidbody>();
		_groundChecker = transform.GetChild(0);
	}
	
	// Update is called once per frame
	void Update ()
	{
		_isGrounded = Physics.CheckSphere(_groundChecker.position, GroundDistance, Ground, QueryTriggerInteraction.Ignore);

		_inputs = Vector3.zero;
        _inputs.x = Input.GetAxis("Horizontal");
        _inputs.z = Input.GetAxis("Vertical");



		if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            Pbody.AddForce(Vector3.up * Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        }
        

		//Rotation as a 3D Vector (turning around)
		float _Yrot = Input.GetAxisRaw("Mouse X") * lookSensitivy;

		Vector3 _rotation = new Vector3(0f, _Yrot, 0f);


		//Apply rotation
		rotation = _rotation;
        
		//Rotation as a 3D Vector (Looking up)
		float _Xrot = Input.GetAxisRaw("Mouse Y") * lookSensitivy;

		Vector3 _cameraRotation = new Vector3( _Xrot, 0f, 0f);


        //Apply rotation
        CamRoation = _cameraRotation;


	}

	void FixedUpdate()  
    {
		Pbody.AddRelativeForce(_inputs * Speed * Time.fixedDeltaTime, ForceMode.VelocityChange);

		//Rotate
        
		Pbody.MoveRotation(Pbody.rotation * Quaternion.Euler(rotation));
        if (Cam != null)
		{
			print(-CamRoation);
			Vector3 CamAngle = Cam.transform.localRotation.eulerAngles;
			print(CamAngle);
			Cam.transform.Rotate(-CamRoation);
            

		}
    }

}
