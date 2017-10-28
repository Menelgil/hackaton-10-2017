using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
  #region Public Properties
  public float WalkSpeed;
  public float InteractingDistance;
  #endregion

  #region Private Members
  private Inventory _inventory;
  private Vector3? _targetPosition;
  private PickableItem _lootTarget;
  #endregion

  #region Unity Callbacks
  // Use this for initialization
  private void Start() {
    _lootTarget = null;
    _targetPosition = null;
    _inventory = GetComponent<Inventory>();
  }

  // Update is called once per frame
  private void Update() {
    if (Input.GetMouseButtonDown(0)) {
      RaycastHit hit;
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      if (Physics.Raycast(ray, out hit, 100.0f)) {
        float distance = (hit.point - transform.position).magnitude;
        Debug.Log(string.Format("Clicked on {0} @{1}m (in interaction distance: {2})", hit.transform.name, distance, distance < InteractingDistance));

        PickableItem pickable = hit.collider.GetComponent<PickableItem>();
        if (pickable != null) {
          if (distance < InteractingDistance) {
            PickItem(pickable);
          } else {
            Debug.Log("Too far to pick item, moving and looting...");
            RotateTo(pickable.transform.position);
            MoveAndLoot(pickable);
          }
        } else {
          Debug.Log("No interaction, moving...");
          RotateTo(hit.point);
          MoveTo(hit.point);
        }
      }
    }

    UpdateMovement();
  }

  private void OnCollisionEnter(Collision collision) {
    _targetPosition = null;
  }
  #endregion

  #region Private Methods
  private void UpdateMovement() {
    if (_targetPosition.HasValue) {
      transform.position = Vector3.MoveTowards(transform.position, _targetPosition.Value, Time.deltaTime * WalkSpeed);

      float remainingDistance = (_targetPosition.Value - transform.position).magnitude;
      if (_lootTarget != null && remainingDistance < InteractingDistance) {
        PickItem(_lootTarget);
        _targetPosition = null;
        _lootTarget = null;
      } else if (remainingDistance < .1f) {
        transform.position = _targetPosition.Value;
        _targetPosition = null;
        _lootTarget = null;
      }
    }
  }

  private void PickItem(PickableItem item) {
    if (_inventory.GrabItem(item)) { // we picked-up the item
      item.PickedBy(this.transform);
    } else { // we already carry something
      Debug.Log("We are already carrying something");
      // TODO: UI to show the user we cannot grab another item
    }
  }

  private void InteractWithItem() {

  }

  private void MoveTo(Vector3 target) {
    target.y = transform.position.y;
    _targetPosition = target;
  }

  private void MoveAndLoot(PickableItem target) {
    _lootTarget = target;
    Vector3 targetPosition = target.transform.position;
    targetPosition.y = transform.position.y;
    _targetPosition = targetPosition;
  }

  private void RotateTo(Vector3 target) {
    Vector3 direction = target - transform.position;
    direction.y = 0;
    if (direction.magnitude > 1) {
      direction.Normalize();
    }
    transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
  }
  #endregion
}
