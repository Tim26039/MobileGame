using UnityEngine;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Speler : MonoBehaviour
{
	Animator animatie;
	Touch initialTouch = new Touch();
	bool hasSwiped;
	private float distance = 0;
	float tijd = 0f;
	float Tijd = 0f;
	bool tijdje;

	float snelheid = 0;

	Vector3 Positie1;
	Vector3 Positie2;
	Vector3 Positie3;

	Vector3 startpositie;

	int score = 0;

	public Text SCORE;

	public AudioClip munt;
	private AudioSource soundeffect;

	// Use this for initialization
	void Start () 
	{
		soundeffect = GetComponent<AudioSource>();

		hasSwiped = false;
		animatie = GetComponent <Animator> ();
		tijdje = false;
		score = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		transform.Translate (0, 0, -0.3f);
		tijd += Time.deltaTime;
		Tijd += Time.deltaTime;
		positie ();
	}


	void positie ()
	{
		float dir = 0;

		dir = Input.acceleration.x ;
		snelheid = -dir;

		if (dir < -0.6f && Tijd > 0.6f) 
		{
			if (startpositie.x == Positie2.x) 
			{
				startpositie = Positie1;
				transform.position =  startpositie;
			}

			else if(startpositie.x == Positie3.x)
			{
				startpositie = Positie2;
				transform.position =  startpositie;
			}

			Tijd = 0;
		}

		if (dir > 0.6f && Tijd > 0.6f) 
		{
			if (startpositie.x == Positie2.x) 
			{
				startpositie = Positie3;
				transform.position =  startpositie;
			}

			else if (startpositie.x == Positie1.x) 
			{
				startpositie = Positie2;
				transform.position =  startpositie;
			}

			Tijd = 0;
		}
			

		Positie1 = new Vector3 (-3.3f, transform.position.y, transform.position.z);
		Positie2 = new Vector3 (0f, transform.position.y, transform.position.z);
		Positie3 = new Vector3(3.3f, transform.position.y, transform.position.z);
		
	}

	void OnCollisionEnter (Collision collider)
	{
		animatie.SetBool ("Landen", true);

		if (collider.gameObject.tag == "Munt") 
		{
			score += 1;

			if (score < 10)
			{
				SCORE.text = "0" + score.ToString ();
			} 

			else 
			{
				SCORE.text = score.ToString ();
			}

			soundeffect.PlayOneShot (munt, 0.8f);
			Destroy (collider.gameObject);
		}
	}

	void OnCollisionExit (Collision collider)
	{
		animatie.SetBool ("Landen", false);
	}
		
	void OnCollisionStay ()
	{
		if (Input.GetKeyDown (KeyCode.Space))
		{
			animatie.SetTrigger ("Springen");
			this.GetComponent<Rigidbody> ().AddForce (new Vector3 (0, 450f, 150f));
		} 

		if (tijd > 0.1f && tijdje == true)
		{
			this.GetComponent<Rigidbody> ().velocity = new Vector3 (this.GetComponent<Rigidbody> ().velocity.x, 0, this.GetComponent<Rigidbody> ().velocity.z);
			this.GetComponent<Rigidbody> ().AddForce (new Vector3 (0, 350f, 250f));
			tijdje = false;
		}


		foreach(Touch t in Input.touches)
		{
			if (t.phase == TouchPhase.Began) 
			{
				initialTouch = t;
			}

			else if (t.phase == TouchPhase.Moved && !hasSwiped) 
			{
				float deltaY = initialTouch.position.y - t.position.y;
				float deltaX = initialTouch.position.x - t.position.x;
				distance = Mathf.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
				bool swipedSideways = Mathf.Abs(deltaX) > Mathf.Abs(deltaY);

				if (distance > 10f)
				{
					if (swipedSideways && deltaX > 0) //swiped left
					{

					}
					
					else if (swipedSideways && deltaX <= 0) //swiped right
					{

					}
					
					else if (!swipedSideways && deltaY > 0) //swiped down
					{

					}
					
					else if (!swipedSideways && deltaY <= 0)  //swiped up
					{
						tijdje = true;
						tijd = 0.0f;
						animatie.SetTrigger ("Springen");
					}

					hasSwiped = true;
				}
			}

			else if (t.phase == TouchPhase.Ended)
			{
				initialTouch = new Touch();
				hasSwiped = false;
			}
		}
		
	}
}
