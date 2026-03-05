using System;
using System.Runtime.InteropServices;
using JetBrains.Rider.Unity.Editor;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.InputSystem;

/// Script Credits
/// Sebastian Lague: https://www.youtube.com/watch?v=rQG9aUWarwE
public class CharacterController : MonoBehaviour
{
    [SerializeField] float speed = 6;
    private Vector3 velocity;

    private new Rigidbody rigidbody;
    private new Camera camera;

    private void OnEnable()
    {
        this.rigidbody = GetComponent<Rigidbody>();
        this.camera = Camera.main;
    }

    void Update()
    {
        Vector3 mousePosition = this.camera.ScreenToWorldPoint(new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            camera.transform.position.y
        ));

        transform.LookAt(mousePosition + Vector3.up * transform.position.y);

        Vector2 moveInput = new();
        if (Input.GetKey(KeyCode.W)) moveInput += Vector2.up;
        if (Input.GetKey(KeyCode.S)) moveInput += Vector2.down;
        if (Input.GetKey(KeyCode.A)) moveInput += Vector2.left;
        if (Input.GetKey(KeyCode.D)) moveInput += Vector2.right;

        this.velocity = new Vector3(moveInput.x, 0, moveInput.y).normalized * this.speed;
    }

    void FixedUpdate()
    {
        this.rigidbody.MovePosition(rigidbody.position + this.velocity * Time.deltaTime);
    }
}