using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDisplay : MonoBehaviour {

    [SerializeField]
    private Sprite _normal_slider_sprite;

    [SerializeField]
    private Sprite _damaged_slider_sprite;

    [SerializeField]
    private Sprite _death_slider_sprite;

    [SerializeField]
    private Sprite _normal_head_sprite;

    [SerializeField]
    private Sprite _damaged_head_sprite;

    [SerializeField]
    private Sprite _normal_power_charged;

    [SerializeField]
    private Sprite _damaged_power_charged;

    [SerializeField]
    private Sprite _death_head_sprite;

    [SerializeField]
    private Image _slider_background;

    [SerializeField]
    private Image _head;

    [SerializeField]
    private CharacterBehaviour _character;

    [SerializeField]
    private Slider _slider;

    private void Start()
    {
        gameObject.SetActive(true);
        _slider.maxValue = 100f;
        _slider.minValue = 0f;
    }

    // Update is called once per frame
    void Update () {
        _slider.value = _character.GetPowerLevel();

        if (_character.gameObject.activeSelf == false)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
        if(_character.GetIsAlive() == false)
        {
            _head.sprite = _death_head_sprite;
            _slider_background.sprite = _death_slider_sprite;
            return;
        }
        if(_character.GetGotHit() == true && _slider.value < _slider.maxValue)
        {
            _head.sprite = _damaged_head_sprite;
            _slider_background.sprite = _damaged_slider_sprite;
        }
        else if(_character.GetGotHit() == true && _slider.value >= _slider.maxValue)
        {
            _head.sprite = _damaged_power_charged;
            _slider_background.sprite = _damaged_slider_sprite;
        }
        else if(_character.GetGotHit() == false && _slider.value < _slider.maxValue)
        {
            _head.sprite = _normal_head_sprite;
            _slider_background.sprite = _normal_slider_sprite;
        }
        else if(_character.GetGotHit() == false && _slider.value >= _slider.maxValue)
        {
            _head.sprite = _normal_power_charged;
            _slider_background.sprite = _normal_slider_sprite;
        }
	}
}
