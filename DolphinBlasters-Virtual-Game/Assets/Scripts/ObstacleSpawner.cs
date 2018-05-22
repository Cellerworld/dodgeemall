﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour {

    [SerializeField]
    private float _drop_radius;
    [SerializeField]
    private int _max_obstacle_count;
    [SerializeField]
    private float _min_drop_cooldown;
    [SerializeField]
    private float _max_drop_cooldown;
    [SerializeField]
    private GameObject[] _obstacle;

    private float _drop_cooldown;
    private List<GameObject> _obstacles;

    private void Start()
    {
        _obstacles = new List<GameObject>();
        _drop_cooldown = _max_drop_cooldown;
    }

    private void Update()
    {
        if(_obstacles.Count < _max_obstacle_count)
        {
            if(_drop_cooldown <= 0)
            {
                DropObstacle();
                _drop_cooldown = Random.Range(_min_drop_cooldown, _max_drop_cooldown);
            }
            else
            {
                _drop_cooldown -= Time.deltaTime;
            }
        }
    }

    //removes the given obstacle
    public void RemoveObstacle(GameObject obstacle)
    {
        if (obstacle != null && _obstacles.Contains(obstacle))
        {
			
            _obstacles.Remove(obstacle);
        }
    }

    //drops a random obstacle in a random position
    private void DropObstacle()
    {
        Vector3 drop_position = new Vector3(transform.position.x + Random.Range(-_drop_radius, _drop_radius), transform.position.y, Random.Range(-_drop_radius, _drop_radius));
        GameObject obstacle = Instantiate(_obstacle[Random.Range(0, _obstacle.Length)], drop_position, Quaternion.Euler(0f, Random.Range(0f, 360f), 0f));
        _obstacles.Add(obstacle);
    }

    //draws a gizmo :^)
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(transform.position, new Vector3(2 * _drop_radius, _drop_radius, 2 * _drop_radius));
    }
}
