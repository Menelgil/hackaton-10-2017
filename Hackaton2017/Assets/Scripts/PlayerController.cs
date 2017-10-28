using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  public float WalkSpeed;

  private Vector3 ? _targetPosition;

  // Use this for initialization
  void Start() {
    _targetPosition = null;
  }

  // Update is called once per frame
  void Update() {
    if (Input.GetMouseButtonDown(0)) {
      RaycastHit hit;
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      if (Physics.Raycast(ray, out hit, 100.0f)) {
        Debug.Log("Clicked on " + hit.transform.name);

        Debug.Log("Interraction not possible: moving...");
        MoveTo(hit.point);
      }
    }

    if (_targetPosition.HasValue) {
      transform.position = Vector3.MoveTowards(transform.position, _targetPosition.Value, Time.deltaTime * WalkSpeed);
      if ((_targetPosition.Value - transform.position).magnitude < .1f) {
        transform.position = _targetPosition.Value;
        _targetPosition = null;
      }
    }
  }

  public void OnCollisionEnter(Collision collision) {
    _targetPosition = null;
  }

  private void PickItem() {

  }

  private void InteractWithItem() {

  }

  private void MoveTo(Vector3 target) {
    // rotate to the point
    Vector3 direction = target - transform.position;
    direction.y = 0;
    if (direction.magnitude > 1) {
      direction.Normalize();
    }
    transform.rotation = Quaternion.LookRotation(direction, Vector3.up);

    // move to the point
    target.y = transform.position.y;
    _targetPosition = target;
  }
}
