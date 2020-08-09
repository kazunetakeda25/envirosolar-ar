using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Projector))]
public class ProjectorAnimation : MonoBehaviour
{
    public Texture[] _frames;
    private Projector _projector;
    private Material _projMat;

    public float _speed = .1f;
    private int _curFrame = 0;
    private float _timer = 0;
   

    void Awake()
    {
        _projector = GetComponent<Projector>();
        _projMat = _projector.material;
    }
    void Update()
    {
        bool nextFrame = false;
        _timer += Time.deltaTime;
        if(_timer >= _speed)
        {
            nextFrame = true;
            _timer = _timer % _speed;
        }
        if (nextFrame) {
            _curFrame = (int)Mathf.Repeat(_curFrame +1, _frames.Length);
            Texture tex = _frames[_curFrame];
            _projMat.SetTexture("_ShadowTex", _frames[_curFrame]);
        }
    }

    void OnDisable()
    {
        _projMat.SetTexture("_ShadowTex", _frames[0]);
    }
}
