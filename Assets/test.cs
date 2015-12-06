using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {
	
	public Mesh objectToCreate;
	public Material material;
	public float cubeRate = 0.30f;
	public bool canMake = true;
	
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey("b")){
			Make();
		}	
	}
	
	void Make(){
		if( canMake == true ){
			canMake = false;
			MakeOneCube();
		}
	}
	
	IEnumerator startWait()
	{
		yield return StartCoroutine(Wait(5));
	}
	
	IEnumerator Wait(float seconds)
	{
		yield return new WaitForSeconds(seconds);
	}
	
	void MakeOneCube(){
		
		var gameObject = new GameObject("Cube");
		var meshFilter = gameObject.AddComponent<MeshFilter>();
		
		gameObject.AddComponent<MeshRenderer>();
		gameObject.GetComponent<Renderer>().material = material;
		
		Rigidbody gameObjectsRigidBody = gameObject.AddComponent<Rigidbody>();
		gameObjectsRigidBody.mass = 50;
		
		gameObject.AddComponent<BoxCollider>();
		
		meshFilter.sharedMesh = objectToCreate;
		
		/******DIRECTION*****/
		
		var direction = transform.TransformDirection(Vector3.forward);
		
		//gameObject.transform.position = (transform.position)*2;
		//gameObject.transform.rotation = transform.rotation;
		
		RaycastHit hit;
		
		if(Physics.Raycast(transform.position, direction, out hit,100)){
			var tempRot = Quaternion.FromToRotation(Vector3.up, hit.normal);
			Instantiate(gameObject, hit.point, tempRot);
			
			if(hit.rigidbody){
				hit.rigidbody.AddForce(1000 * direction);
			}
		}
		
		//yield WaitForSeconds(fireRate);
		//yield return new WaitForSeconds(cubeRate);
		StartCoroutine(startWait());
		
		canMake = true;
	}		
}