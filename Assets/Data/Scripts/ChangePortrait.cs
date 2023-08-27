using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChangePortrait : MonoBehaviour
{
    [SerializeField] private List<Sprite> _pictures;
    [SerializeField] private float _threshold = 0.6f;

    private Dictionary<Sprite,bool> _dicPictures;
    private Camera _activeCamera;
    private Vector3 _currentDirection;
    private Vector3 _playerDirection;
    private Texture _originalSprite;
    private MeshRenderer _meshRenderer;
    private int _picIndex = 0;
    private bool done = false;
    
    private void Start() {
        _meshRenderer = GetComponent<MeshRenderer>();
        _activeCamera = Camera.main;
        _currentDirection = transform.forward;
        _dicPictures = new Dictionary<Sprite, bool>();
        _originalSprite = _meshRenderer.material.mainTexture;
        
        foreach(var i in _pictures){
            _dicPictures.Add(i,false);
        }
    }

    private void Update() {
        _playerDirection = _activeCamera.transform.forward;
        // Debug.Log(Vector3.Dot(_currentDirection, _playerDirection));
        if(_picIndex < _dicPictures.Count)
        {
            if(Vector3.Dot(_currentDirection, _playerDirection) < _threshold){
                _dicPictures[_pictures[_picIndex]] = true;
            }
            else
            {
                if(_dicPictures[_pictures[_picIndex]] == true){
                    _meshRenderer.material.mainTexture = _pictures[_picIndex].texture;
                    _picIndex++;
                }
                
            }
        }
        else
        {
            if(Vector3.Dot(_currentDirection, _playerDirection) < _threshold){
                done = true;
            }
            else{
                if(done)
                    _meshRenderer.material.mainTexture = _originalSprite;
            }
            
            
        }
    }
}
