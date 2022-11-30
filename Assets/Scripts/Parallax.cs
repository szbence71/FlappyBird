using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {
    private MeshRenderer _meshRenderer;
    private Material _material;
    public float speed;

    void Start() {
        _meshRenderer = GetComponent<MeshRenderer>();
        _material = _meshRenderer.material;
    }

    void Update() {
        _material.mainTextureOffset += Vector2.right * speed * Time.deltaTime;
    }
}
