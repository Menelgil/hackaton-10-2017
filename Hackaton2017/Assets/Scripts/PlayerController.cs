using System;
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
  private float _targetEpsilon;
  private Action _targetReached;

  private const float DefaultEpsilon = .1f;
  #endregion

  #region Unity Callbacks
  // Use this for initialization
  private void Start() {
    _targetPosition = null;
    _targetReached = null;
    _targetEpsilon = DefaultEpsilon;
    _inventory = GetComponent<Inventory>();
  }

  // Update is called once per frame
  private void Update() {
    if (Input.GetButton("Fire1")) {
      RaycastHit hit;
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      if (Physics.Raycast(ray, out hit, 100)) {
        PickableItem pickable = hit.collider.GetComponent<PickableItem>();
        InteractibleItem interactible = hit.collider.GetComponent<InteractibleItem>();

        if (pickable != null) {
          RotateTo(pickable.transform.position);
          MoveTo(target: pickable.transform.position, reach: InteractingDistance, andThen: () => {
            PickItem(pickable);
          });
        } else if (interactible != null) {
          RotateTo(interactible.transform.position);
          MoveTo(target: interactible.transform.position, reach: InteractingDistance, andThen: () => {
            InteractWithItem(interactible);
          });
        } else {
          RotateTo(hit.point);
          MoveTo(target: hit.point);
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
      if (remainingDistance < _targetEpsilon) {
        if (_targetReached != null) {
          _targetReached();
        }
        _targetReached = null;
        _targetPosition = null;
      }
    }
  }

  private void PickItem(PickableItem item) {
    if (!item.IsPickable) {
      // we don't know what the item will be used for
      Debug.Log("This will probably be usefull later.");
    } else if (!_inventory.GrabItem(item)) {
      // we already carry something
      Debug.Log("My hands are full, I can't pick this up.");
    } else {
      item.PickedBy(this.transform);
      Debug.LogFormat("I might need this.");
    }
  }

  private void InteractWithItem(InteractibleItem item) {
    item.InteractWith(this);
  }

  private void MoveTo(Vector3 target, float reach = DefaultEpsilon, Action andThen = null) {
    Vector3 targetPos = target;
    targetPos.y = transform.position.y;
    _targetPosition = targetPos;
    _targetEpsilon = reach;
    _targetReached = andThen;
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
