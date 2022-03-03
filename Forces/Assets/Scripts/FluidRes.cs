using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyMath;

public class FluidRes : MonoBehaviour {

	// friccion = -1 * u * v(normalizado)
	// Fd = -1/2 * magnitud de la velocidad al cuadrado * Area frontal(que golpea el fluido directamente) * Coeficiente de drag * velocidad normalizada

	[SerializeField]
	Vector3 acceleration;

	[SerializeField]
	private float mass;

	[SerializeField] 
	private float gravity;

	private float weight;

	[SerializeField]
	[Range(0,1)] float damping = 0.9f;

	Vector3 displacement;
	Vector3 velocity;
	[SerializeField]float xBorder, yBorder;

	[SerializeField]
	[Range(0,1)] float u;
	[SerializeField] float n;
	Vector3 friction;


	float frontalArea;
	[SerializeField]
	[Range(0,1)] float dragCo;
	Vector3 rFluid;


	public void Start(){

		frontalArea = transform.localScale.x;

		weight = mass * gravity;
	}

	public void Update()
	{
		
		acceleration = Vector3.zero;

		rFluid = (-1f / 2f) * velocity.magnitude * velocity.magnitude * frontalArea * dragCo * velocity.normalized;

		if (transform.position.y <= 0) {
			ApplyForce (rFluid);
		}

		friction = -1* u * n * velocity.normalized;

		if (transform.position.y <= 0) {
			ApplyForce (friction);
		}

		ApplyForce (new Vector3(0, weight, 0));
		ApplyForce (new Vector3 (0, gravity, 0));
		Move();
		CheckCollision ();

	}

	public void Move()
	{
		velocity += acceleration * Time.deltaTime;

		displacement = velocity * Time.deltaTime;

		transform.position = transform.position + displacement;

		velocity.Draw (transform.position, Color.green);
		transform.position.Draw (Color.red);
	}

	public void CheckCollision(){

		if (transform.position.x >= xBorder || transform.position.x <= -xBorder) {
			velocity.x = velocity.x * -1 * damping;
		}
		if (transform.position.y >= yBorder || transform.position.y <= -yBorder) {
			velocity.y = velocity.y * -1 * damping;
		}


		if (transform.position.x > xBorder ) {
			transform.position = new Vector3(xBorder, transform.position.y) ;
		}
		if (transform.position.x < -xBorder) {
			transform.position = new Vector3(-xBorder, transform.position.y);
		}
		if (transform.position.y > yBorder) {
			transform.position = new Vector3(transform.position.x, yBorder);
		}
		if (transform.position.y < -yBorder) {
			transform.position = new Vector3(transform.position.x, -yBorder);;
		}
	}


	private void ApplyForce(Vector3 force){

		acceleration += force / mass;

	}
}
