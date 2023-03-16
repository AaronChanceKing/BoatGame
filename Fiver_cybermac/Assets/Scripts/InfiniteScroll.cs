using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteScroll : MonoBehaviour
{
    [SerializeField] private float _scrollSpeed = -5f;
    [SerializeField] private float _scrollLength = 80f;
    [SerializeField] private bool _animation = true;
    [SerializeField] private float _animationSpeed = 1f;
    private Vector2 _startPos;

    // Start is called before the first frame update
    void Start()
    {
        if(_animation)
            GetComponent<Animator>().speed = _animationSpeed;
        _startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float newPos = Mathf.Repeat(Time.time * _scrollSpeed, _scrollLength);
        transform.position = _startPos + Vector2.left * newPos;
    }
}
