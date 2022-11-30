using UnityEngine;

public class Pipes : MonoBehaviour {
    public float speed = 5f;
    private float _leftEdge;

    private void Start() {
        _leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 1f;
    }

    private void Update() {
        transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x < _leftEdge) {
            Destroy(gameObject);
        }
    }
}
