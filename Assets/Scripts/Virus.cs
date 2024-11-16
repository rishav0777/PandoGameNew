using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : MonoBehaviour
{
    [SerializeField]
    private float _leftLimit = -1.1f;
    [SerializeField]
    private float _rightLimit = 0.6f;
    [SerializeField]
    private float _speed = 2f;

    [SerializeField]
    private int _dir = 1;

    private void Start()
    {
        
    }

    private void Update()
    {
        GoTowards(_dir);
    }

    private void GoTowards(int direction)
    {
        transform.Translate(Vector3.right * direction * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RightWall"))
        {
            Debug.Log("plate RightWall "+ _dir);
            _dir = _dir*(-1);
            Debug.Log(" g " + _dir);
        }

        if (other.CompareTag("LeftWall"))
        {
            Debug.Log("plate leftWall " + _dir);
            _dir = _dir*(-1);
            Debug.Log(" g " + _dir);

        }

    }
}
