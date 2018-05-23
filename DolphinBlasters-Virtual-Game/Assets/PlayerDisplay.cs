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
    private Sprite _normal_head_sprite;

    [SerializeField]
    private Sprite _damaged_head_sprite;

    [SerializeField]
    private Sprite _death_sprite;

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
		if(_character.gameObject.activeSelf == false)
        {
            gameObject.SetActive(false);
        }
        if(_character.GetGotHit() == true)
        {
            _head.sprite = _damaged_head_sprite;
            _slider_background.sprite = _damaged_slider_sprite;
        }
        else
        {
            _head.sprite = _normal_head_sprite;
            _slider_background.sprite = _normal_slider_sprite;
        }
        _slider.value = _character.GetPowerLevel();
	}
}
