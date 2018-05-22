using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

    [SerializeField]
    private Canvas _canvas;

    private ObstacleSpawner _spawner;
    private bool _is_grounded;

    private void Start()
    {
        _is_grounded = false;
        _spawner = FindObjectOfType<ObstacleSpawner>();
    }

    private void Update()
    {
        _canvas.transform.position = new Vector3(transform.position.x, 0.1f, transform.position.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            GetComponent<AudioSource>().Play();
            if (_is_grounded == false)
            {
                Rigidbody _rb = collision.gameObject.GetComponent<Rigidbody>();
                _rb.velocity = -_rb.velocity;
                StartCoroutine(DestroyObject());
            }
            Destroy(gameObject);
            return;
        }
        if(collision.gameObject.tag == "Ground")
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            _is_grounded = true;
            gameObject.isStatic = true;
        }
        if(collision.gameObject.tag == "Character" && _is_grounded == false)
        {
            collision.gameObject.GetComponent<CharacterBehaviour>().ReceiveDamage(gameObject, transform);
            Destroy(gameObject);
            return;
        }
        if(collision.gameObject.tag == "Obstacle")
        {
            Destroy(gameObject);
            return;
        }
    }

	IEnumerator DestroyObject()
	{
		yield return new WaitForSeconds (1);
		_spawner.RemoveObstacle(this.gameObject);
		Destroy(gameObject);
	}
}
