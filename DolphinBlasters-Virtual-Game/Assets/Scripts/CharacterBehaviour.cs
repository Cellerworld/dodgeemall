﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(CharacterController))]
public abstract class CharacterBehaviour : MonoBehaviour {

	[SerializeField]
	protected float _throw_power;

    [SerializeField]
    protected float _die_magnitude;

    [SerializeField]
    protected float _bounce_multiplier;

    [SerializeField]
    protected float _max_ball_velocity;

    [SerializeField]
    protected float _desired_movement_speed;

    [SerializeField]
    protected Transform _holder;

    [SerializeField]
    protected float _dash_speed;

    [SerializeField]
    protected float _start_dash_time;

    [SerializeField]
    protected float _max_ball_time;

    protected float _ball_time;

    protected float _dash_time;

    protected bool _is_dashing;

    protected GameObject _ball;

    protected Rigidbody _rb;

    protected float _wall_hit_magnitude;

    protected bool _got_hit;

    protected bool _is_at_wall;

    protected float _hit_timer;

    protected Vector3 _ball_velocity;

    protected Rigidbody _ball_rb;

    [SerializeField]
    protected float _desired_hit_timer;

    protected float _power_level;

    public enum ABILITY { TELEPORT, FREEZE, ROOT, SPEED}
    [SerializeField]
    protected ABILITY _character_ability;
    [SerializeField]
    protected float _ability_duration;

    protected float _ability_duration_left;

    protected float _root_timer;

    protected float _movement_speed;

    protected bool _is_frozen;

    [SerializeField]
    protected float _slow_down_multiplier;

    [SerializeField]
    protected int _player_number;

    protected Animator _anim;

    //protected CharacterController _controller;

    protected abstract void Move();
    protected abstract void Fire();
    protected abstract void Dodge();


    protected void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        _dash_time = _start_dash_time;
		_movement_speed = _desired_movement_speed;
    }

    public void SetRootTimer(float root_time)
    {
        _root_timer = root_time;
    }

    //rotates the player towards the direction he is moving
    protected void Rotate()
    {
        Vector3 velocity = _rb.velocity;
        if (velocity.magnitude > 0.2f)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(velocity.x, 0, velocity.z));
        }
    }

    protected void FixedUpdate()
    {
        //ball falls to ground if the holding time exceeds
        if(_is_frozen)
        {
            //TODO: decrease _bounce_multiplier
        }
        if (_ball != null)
        {
            if (_ball_time <= 0)
            {
                _ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
                _ball = null;
                GameManager.SetRestrictedCharacrter(this);
                GameManager.current_ball_owner = null;
                _movement_speed = _desired_movement_speed;
            }
            else
            {
                _movement_speed = _movement_speed * _slow_down_multiplier;
                _ball_time -= Time.deltaTime;
            }
        }
        if(_ability_duration_left <= 0 && _character_ability == ABILITY.SPEED && _ball == null)
        {
            _movement_speed = _desired_movement_speed;
        }
        if(_hit_timer <= 0)
        {
            _got_hit = false;
        }
        else
        {
            _hit_timer -= Time.deltaTime;
        }
        _ability_duration_left -= Time.deltaTime;
    }

    //TODO find the right value
    protected void HandleAnimtaion()
    {
        if(_is_dashing)
        {
            _anim.Play("Dash");
        }
        else if(_ball != null && (Mathf.Abs(Input.GetAxis("Vertical" + _player_number)) > 0.1f || Mathf.Abs(Input.GetAxis("Horizontal" + _player_number)) > 0.1f))
        {
            _anim.Play("Fly_Ball");
        }
        else if(_ball == null && (Mathf.Abs(Input.GetAxis("Vertical" + _player_number)) > 0.1f || Mathf.Abs(Input.GetAxis("Horizontal" + _player_number)) > 0.1f))
        {
            _anim.Play("Fly");
        }
        else if(_ball != null && (Mathf.Abs(Input.GetAxis("Vertical" + _player_number)) < 0.1f || Mathf.Abs(Input.GetAxis("Horizontal" + _player_number)) < 0.1f))
        {
            _anim.Play("Idle_Ball");
        }
        else if(_ball == null && (Mathf.Abs(Input.GetAxis("Vertical" + _player_number)) < 0.1f || Mathf.Abs(Input.GetAxis("Horizontal" + _player_number)) < 0.1f))
        {
            _anim.Play("Idle");
        }
    }

    //TODO: fix it that the character sometimes doesnt get a draw back
    protected void CalculateBlowBack(GameObject obj, Transform trans)
    {
        if(obj.tag == "Ball")
        {
			//_rb.velocity = Vector3.zero;
            Vector3 dir = transform.position - trans.position;
			Rigidbody obj_rb = obj.GetComponent<Rigidbody>();
			dir = obj_rb.velocity * _bounce_multiplier;
			_rb.velocity = dir;
        }
        else
        {
            Vector3 dir = transform.position - trans.position;
            dir.y = 0f;
            Debug.Log(dir + " " + dir.normalized + " " + dir.normalized * _bounce_multiplier + " " + _bounce_multiplier);
            _rb.AddForce(dir.normalized * 0.1f * _bounce_multiplier);
        }
    }

    //increases the _bounce_multiplier so it gets harder for the player when he gets hit 
    public void ReceiveDamage(GameObject obj, Transform trans)
    {
        if (_is_at_wall == true)
        {
            Die();
            Destroy(this.gameObject);
            return;
        }
        CalculateBlowBack(obj, trans);
        _bounce_multiplier *= 1.1f;
    }

    //Destroys the character
    protected void Die()
    {
        Destroy(this.gameObject);
    }

    protected void OnTriggerExit(Collider other)
    {
        if(other.tag == "Border")
        {
            _is_at_wall = false;
        }
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Border")
        {
            _wall_hit_magnitude = _rb.velocity.magnitude;
            _is_at_wall = true;
        }
        if(other.tag == "Ball")
        {
            _ball_velocity = other.gameObject.GetComponent<Rigidbody>().velocity;
        }
    }

    protected void PickUpBall(GameObject ball, Rigidbody ball_rb)
    {
        _ball = ball;
        ball_rb.velocity = Vector3.zero;
        _ball_time = _max_ball_time;
        GameManager.current_ball_owner = this;
        _ball_rb = ball_rb;
    }

    //recognizes collisions
    //if collided with the ball while the ball is slow enough he picks up the ball
    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            if (_is_frozen == false)
            {
                Transform trans = collision.gameObject.transform;
                GameObject ball = collision.gameObject;
                Rigidbody ball_rb = ball.GetComponent<Rigidbody>();
                if (_ball_velocity.magnitude < _max_ball_velocity && GameManager.current_ball_owner == null && GameManager.restricted_character != this)
                {
                    PickUpBall(ball, ball_rb);
                }
                else if (GameManager.current_ball_owner == null && _got_hit == false && _ball_velocity.magnitude > _max_ball_velocity && _is_dashing == false)
                {
                    _got_hit = true;
                    _hit_timer = _desired_hit_timer;
                    ReceiveDamage(ball, trans);
                }
                else if (GameManager.current_ball_owner == null && _got_hit == false && _is_dashing == true)
                {
                    PickUpBall(ball, ball_rb);
                }
            }
            else
            {
                _rb.velocity = Vector3.zero;
                _root_timer = 0f;
                _is_frozen = false;
                _rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
            }
        }
        if(collision.gameObject.tag == "Border")
        {
            if(_wall_hit_magnitude > _die_magnitude && _got_hit == true)
            {
                Die();
            }
        }
    }

    protected void UseAbility(ABILITY ability)
    {
        if (ability == ABILITY.TELEPORT)
        {
            CharacterBehaviour character = null;
            for(int i = 0; i < 100; i++)
            {
                character = GameManager.active_characters[Random.Range(0, GameManager.active_characters.Length)];
                if (character != null && character != this)
                {
                    break;
                }
            }
            //swap places with the target
            Vector3 target_pos = character.transform.position;
            character.transform.position = transform.position;
            transform.position = target_pos;
        }
        else if(ability == ABILITY.FREEZE)
        {
            //root yourself, make yourself invincible and decrease the _bounce_multiplier
            SetRootTimer(_ability_duration);
            _is_frozen = true;
            _rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        }
        else if(ability == ABILITY.ROOT)
        {
            foreach(CharacterBehaviour character in GameManager.active_characters)
            {
                Debug.Log(character);
                if(character != this)
                {
                    //root the other players
                    character.SetRootTimer(_ability_duration);
                    character.GetComponent<Rigidbody>().velocity = Vector3.zero;
                }
            }
        }
        else if(ability == ABILITY.SPEED)
        {
            _ability_duration_left = _ability_duration;
            _movement_speed *= 1.5f;
        }
    }
}
