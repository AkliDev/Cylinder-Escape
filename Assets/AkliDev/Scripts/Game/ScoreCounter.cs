using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class ScoreCounter : MonoBehaviour
{

    [SerializeField]private TextMeshProUGUI _Text;
    public int _Score;
    private AudioSource _Audio;

    private float _Scale;
    // Use this for initialization
    void Start()
    {
        _Audio = GetComponent<AudioSource>();
        _Text = GetComponent<TextMeshProUGUI>();

    }

    // Update is called once per frame
    void Update()
    {
        _Text.text = _Score.ToString();
        _Text.rectTransform.localScale = new Vector3(_Scale, _Scale, _Scale);

        _Scale = Mathf.Lerp(_Scale, 1, 8 * Time.deltaTime);
    }

    public void AddScore(int amount)
    {
        _Score += amount;
        _Scale = 1.4f;
        _Audio.pitch = Random.Range(0.97f, 1.03f);
        _Audio.Play();

      
    }
}
