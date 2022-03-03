using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyMath;

public class GravForce : MonoBehaviour {
	
	[SerializeField]
	Vector3 acceleration;

	[SerializeField]
	GameObject flork;

	[SerializeField]
	float m1;

	[SerializeField]
	float m2;

	Vector3 f;
	Vector3 r;

	[SerializeField]
	[Range(0,1)] float g = 0.9f;

	[SerializeField] 
	private float gravity;

	private float weight;

	Vector3 displacement;

	[SerializeField]
	Vector3 velocity;



	public void Start(){

		weight = m1 * gravity;
	}

	public void Update()
	{
		r = flork.transform.position - transform.position;
		f = ((g * m1 * m2) / (r.magnitude * r.magnitude)) * r.normalized;

		acceleration = Vector3.zero;

		ApplyForce (f);
		ApplyForce (new Vector3(0, weight, 0));
		ApplyForce (new Vector3 (0, gravity, 0));
		Move();

	}

	public void Move()
	{
		velocity += acceleration * Time.deltaTime;

		displacement = velocity * Time.deltaTime;

		transform.position = transform.position + displacement;

		velocity.Draw (transform.position, Color.green);
		transform.position.Draw (Color.red);
	}


	private void ApplyForce(Vector3 force){

		acceleration += force / m1;

	}
}
