using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float speed = 6f;
	Vector3 movement;
	Animator anim;
	Rigidbody playerRigidbody;
	int floorMask;
	float camRaylength = 100f;
	//awake duoc goi tu dong 
	void Awake(){
		floorMask = LayerMask.GetMask ("Floor");
		anim = GetComponent<Animator> ();

		playerRigidbody = GetComponent<Rigidbody> ();

	}
	void FixedUpdate(){
		// raw axis chi co gia tri -1,0,1 	khac normal axis gia tri thay doi [-1,1]
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical") ;
		Move (h, v);
		Turning ();
		Animating (h, v);

	}
	void Move(float h, float v){
		movement.Set (h,0f,v);
		//delta time la thoi gian giua 2 lan update
		movement = movement.normalized*speed*Time.deltaTime; 
		playerRigidbody.MovePosition(transform.position+ movement);
	}

	void Turning(){
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit floorHit;
		if(Physics.Raycast(camRay, out floorHit, camRaylength, floorMask)){
			print ("hit" + floorHit.point);
			Vector3 playerToMouse = floorHit.transform.position - transform.position;
			//Debug.DrawRay (Camera.main.GetComponent<Transform)(), floorHit.point);
			playerToMouse.y = 0f;
			Debug.DrawLine (floorHit.transform.position, transform.position);
			Quaternion newRotation = Quaternion.LookRotation (playerToMouse);
			playerRigidbody.MoveRotation (newRotation);
		}
			
	}
	void Animating(float h, float v){
		//neu h khac 0 hoac v khac 0 thi walking = true;		
		bool walking = h != 0f || v != 0f;
		anim.SetBool ("IsWalking", walking);

	}
}
