using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[RequireComponent(typeof(CharacterController))]
public abstract class CharacterBehaviour : MonoBehaviour {

	[SerializeField]
	private InGameAudio gameAudio;

    [SerializeField]
    public int _id;

    [SerializeField]
    protected AudioClip[] _hit_sounds;

    [SerializeField]
    protected AudioClip _ability_clip;

    [SerializeField]
    protected AudioClip _charged_sound;

    [SerializeField]
    protected Image _circle;

    [SerializeField]
    protected Image _first_arrow;

    [SerializeField]
    protected Image _second_arrow;

    [SerializeField]
    protected Image _third_arrow;

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

    [SerializeField]
    protected bool _got_hit;

    protected bool _is_at_wall;

    protected float _hit_timer;

    protected Vector3 _ball_velocity;

    protected Rigidbody _ball_rb;

    [SerializeField]
    protected float _desired_hit_timer;

    [SerializeField]
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

	public int _player_number;

    [SerializeField]
    protected float _needed_power_level;

    [SerializeField]
    protected GameObject _ability_particle;

    [SerializeField]
    protected Transform _rainbow_start;

    protected GameObject _active_particle;

    protected Animator _anim;

    protected AudioSource _audio;
    
    protected bool _is_alive;

    protected bool _is_charged;

    protected abstract void Move();
    protected abstract void Fire();
    protected abstract void Dodge();

    [SerializeField]
    protected LoadSceneManager _sceen_manager;

    protected void Start()
    {
        _audio = GetComponent<AudioSource>();
        _anim = GetComponentInChildren<Animator>();
        _dash_time = _start_dash_time;
		_movement_speed = _desired_movement_speed;
        _is_alive = true;
        for(int i = 0; i < 4; i++)
        {
            if(GameManager.registarted_player_controllernumber[i] == _player_number)
            {
                _circle.color = GameManager.player_colors[i];
            }
        }
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
        if(GameManager.is_animation_over == true)
        {
            if (_is_alive == true)
            {
                if (_is_frozen)
                {
                    _bounce_multiplier -= 0.0001f;
                }
                if (_ball != null)
                {
                    if (_ball_time <= 0)
                    {
                        _ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
                        Collider collider = _ball.GetComponent<SphereCollider>();
                        collider.isTrigger = false;
                        _ball = null;
                        GameManager.SetRestrictedCharacrter(this);
                        GameManager.current_ball_owner = null;
                        _movement_speed = _desired_movement_speed;
                    }
                    else
                    {
                        if (_ball_time <= _max_ball_time * 0.75f)
                        {
                            Debug.Log("SCHEI?E!");
                            _first_arrow.enabled = true;
                        }
                        if (_ball_time <= _max_ball_time * 0.5f)
                        {
                            _second_arrow.enabled = true;
                        }
                        if (_ball_time <= _max_ball_time * 0.25f)
                        {
                            _third_arrow.enabled = true;
                        }
                        Debug.Log(_ball_time <= _max_ball_time * 0.75f);
                        _movement_speed = _movement_speed * _slow_down_multiplier;
                        _ball_time -= Time.deltaTime;
                    }
                }
                if (_ball == null)
                {
                    _first_arrow.enabled = false;
                    _second_arrow.enabled = false;
                    _third_arrow.enabled = false;
                }
                if (_ability_duration_left <= 0 && _character_ability == ABILITY.SPEED && _ball == null)
                {
                    _movement_speed = _desired_movement_speed;
                }
                if (_hit_timer <= 0)
                {
                    _got_hit = false;
                }
                else
                {
                    _hit_timer -= Time.deltaTime;
                }
                _ability_duration_left -= Time.deltaTime;
            }
        }
    }

    //TODO find the right value
    protected void HandleAnimtaion()
    {
        if(_root_timer >= 0)
        {
            _anim.Play("Idle");
        }
        else if(_root_timer >= 0 && _ball != null)
        {
            _anim.Play("Idle_Ball");
        }
        else if(_is_dashing)
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
    
    protected void CalculateBlowBack(GameObject obj, Transform trans)
    {
        if(obj.tag == "Ball")
        {
            Vector3 dir = transform.position - trans.position;
			Rigidbody obj_rb = obj.GetComponent<Rigidbody>();
			dir = obj_rb.velocity * _bounce_multiplier;
			_rb.velocity = dir;
        }
        else
        {
            Vector3 dir = transform.position - trans.position;
            dir.y = 0f;
            _rb.AddForce(dir.normalized * 0.1f * _bounce_multiplier);
        }
    }

    //increases the _bounce_multiplier so it gets harder for the player when he gets hit 
    public void ReceiveDamage(GameObject obj, Transform trans)
    {
        if (_is_at_wall == true)
        {
            Die();
            //Destroy(this.gameObject);
            return;
        }
		if (Random.Range (0, 100) < 55) {
			GetComponent<AudioSource> ().PlayOneShot (gameAudio.Encouraging [Random.Range (0, gameAudio.Encouraging.Length)]);
		}
        CalculateBlowBack(obj, trans);
        _bounce_multiplier *= 1.1f;
    }

    //Destroys the character
    protected void Die()
    {
		GetComponent<AudioSource> ().PlayOneShot (gameAudio.Effeckts [Random.Range (0, gameAudio.Effeckts.Length)]);
        GameManager.RemovePlayer(this);
        _is_alive = false;
        //Destroy(this.gameObject);
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
        Collider collider = ball.GetComponent<SphereCollider>();
        collider.isTrigger = true;
        _ball = ball;
        ball_rb.velocity = Vector3.zero;
        _ball_time = _max_ball_time;
        GameManager.current_ball_owner = this;
        GameManager.last_ball_owner = this;
        _ball_rb = ball_rb;
		if (Random.Range (0, 100) < 20)
		{
			GetComponent<AudioSource> ().PlayOneShot (gameAudio.Mocking[Random.Range(0,gameAudio.Mocking.Length)]);
		}
    }

    public void AddPowerLevel(int value)
    {
        _power_level += value;
        if(_power_level >= _needed_power_level && _is_charged == false)
        {
            _audio.Stop();
            _audio.clip = _charged_sound;
            _audio.Play();
            _is_charged = true;
        }
        if(_power_level < _needed_power_level)
        {
            _is_charged = false;
        }
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
                    AddPowerLevel(5);
                    PickUpBall(ball, ball_rb);
                }
                else if (GameManager.current_ball_owner == null && _got_hit == false && _ball_velocity.magnitude > _max_ball_velocity && _is_dashing == false)
                {
                    _audio.Stop();
                    _audio.clip = _hit_sounds[Random.Range(0, _hit_sounds.Length)];
                    _audio.Play();
                    if (GameManager.last_ball_owner != null)
                    {
                        GameManager.last_ball_owner.AddPowerLevel(15);
                    }
                    _got_hit = true;
                    _hit_timer = _desired_hit_timer;
                    ReceiveDamage(ball, trans);
                }
                else if (GameManager.current_ball_owner == null && _got_hit == false && _is_dashing == true)
                {
                    AddPowerLevel(10);
                    PickUpBall(ball, ball_rb);
                }
            }
            else
            {
                if(_active_particle != null)
                {
                    Destroy(_active_particle);
                }
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
        _is_charged = false;
        _audio.Stop();
        _audio.clip = _ability_clip;
        _audio.Play();
        if (ability == ABILITY.TELEPORT)
        {
            CharacterBehaviour character = null;
            for(int i = 0; i < 100; i++)
            {
                character = GameManager.active_characters[Random.Range(0, GameManager.active_characters.Count)];
                if (character != null && character != this)
                {
                    break;
                }
            }
            //swap places with the target
            Vector3 pos = character.transform.position;
            pos.y += 1f;
            Destroy(Instantiate(_ability_particle, character.transform.position, Quaternion.Euler(-90, 0, 0)), 1f);
            pos = transform.position;
            pos.y += 1f;
            Destroy(Instantiate(_ability_particle, transform.position, Quaternion.Euler(-90, 0, 0)), 1f);

            Vector3 target_pos = character.transform.position;
            character.transform.position = transform.position;
            transform.position = target_pos;
        }
        else if(ability == ABILITY.FREEZE)
        {
            //root yourself, make yourself invincible and decrease the _bounce_multiplier
            Vector3 pos = transform.position;
            pos.y += 1f;
            _active_particle = Instantiate(_ability_particle, pos, Quaternion.identity);
            SetRootTimer(_ability_duration);
            _is_frozen = true;
            _rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        }
        else if(ability == ABILITY.ROOT)
        {
            foreach(CharacterBehaviour character in GameManager.active_characters)
            {
                if(character != this)
                {
                    //root the other players
                    Destroy(Instantiate(_ability_particle, character.transform.position, Quaternion.identity), _ability_duration);
                    character.SetRootTimer(_ability_duration);
                    character.GetComponent<Rigidbody>().velocity = Vector3.zero;
                }
            }
        }
        else if(ability == ABILITY.SPEED)
        {
            Vector3 pos = transform.position;
            pos.y += 0.75f;
            pos.z -= 2f;
            Destroy(Instantiate(_ability_particle, _rainbow_start), _ability_duration);//.position, Quaternion.identity, transform), _ability_duration);
            _ability_duration_left = _ability_duration;
            _movement_speed *= 1.5f;
        }
    }

	public bool GetGotHit()
	{
		return _got_hit;
	}

    public float GetPowerLevel()
    {
        return _power_level;
    }

    public bool GetIsAlive()
    {
        return _is_alive;
    }
}
