using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerBehaviour : CharacterBehaviour {

    private new void Start()
    {
        base.Start();
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_is_alive == true)
        {
            if (Input.GetButtonDown("Ability" + _player_number) && _power_level >= _needed_power_level)
            {
                UseAbility(_character_ability);
                _power_level = 0;
            }
        }
    }

    private new void FixedUpdate()
    {
        if (GameManager.is_game_over == false && _is_alive == true)
        {
            base.FixedUpdate();
            if (_ball != null)
            {
                _ball_rb.velocity = Vector3.zero;
                _ball.transform.position = _holder.position;
            }
            if (_root_timer <= 0)
            {
                if(_active_particle != null)
                {
                    Destroy(_active_particle);
                }
                _is_frozen = false;
                _rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
            if (_got_hit == false && _is_dashing == false)
                {
                    Move();
                    Rotate();

                    //throws the ball once the player presses the fire button and he has something that he can throw
                    if (Input.GetButton("Fire" + _player_number) && _ball != null)
                    {
                        Fire();
                    }
                }
                Dodge();
            }
            _root_timer -= Time.deltaTime;
            HandleAnimtaion();
        }
    }

    //moves the player according to the input given by the controller
    protected override void Move()
    {
        Vector3 velocity = new Vector3(Input.GetAxis("Horizontal" + _player_number), 0, Input.GetAxis("Vertical" + _player_number)) * _movement_speed * Time.deltaTime;
        _rb.velocity = velocity;
    }
    
    protected override void Dodge()
    {
        if(_is_dashing == false)
        {
            if(Input.GetButtonDown("Dodge" + _player_number))
            {
                _is_dashing = true;
                _dash_time = _start_dash_time;
            }
        }
        if(_is_dashing == true)
        {
            if(_dash_time <= 0.0f)
            {
                _is_dashing = false;
            }
            else
            {
                _dash_time -= Time.deltaTime;

                _rb.velocity = transform.forward * _dash_speed * Time.deltaTime;
            }
        }
    }

    //Fires the ball and sets the ball to null
    protected override void Fire()
    {
        Rigidbody ball_rb = _ball.GetComponent<Rigidbody>();
        ball_rb.velocity = Vector3.zero;
		ball_rb.AddForce(new Vector3(transform.forward.normalized.x * _throw_power, 0, transform.forward.normalized.z * _throw_power), ForceMode.Force);
        GameManager.current_ball_owner = null;
        Collider collider = _ball.GetComponent<SphereCollider>();
        collider.isTrigger = false;
        _ball = null;
        _ball_rb = null;
        _movement_speed = _desired_movement_speed;
        _anim.Play("Flying_Throw");
    }
}
