using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityController : MonoBehaviour {
  #region Private Members
  private Vector3? _targetPosition;
  private float _targetReach;
  private Action _targetReachedAction;
  #endregion

  #region Protected Members
  protected const float DefaultReach = .1f;
  #endregion

  #region Public Properties
  public float MoveSpeed;
  #endregion

  #region Unity Callbacks
  // Use this for initialization
  protected virtual void Start () {
    _targetPosition = null;
    _targetReachedAction = null;
    _targetReach = DefaultReach;
  }
	
	// Update is called once per frame
	protected virtual void Update () {
    UpdateMovement();
  }

  protected virtual void OnCollisionEnter(Collision collision) {
    _targetPosition = null;
  }
  #endregion

  #region Private Methods
  private void UpdateMovement() {
    if (_targetPosition.HasValue) {
      transform.position = Vector3.MoveTowards(transform.position, _targetPosition.Value, Time.deltaTime * MoveSpeed);

      float remainingDistance = (_targetPosition.Value - transform.position).magnitude;
      if (remainingDistance < _targetReach) {
        if (_targetReachedAction != null) {
          _targetReachedAction();
        }
        _targetReachedAction = null;
        _targetPosition = null;
      }
    }
  }
  #endregion

  #region Protected Methods
  protected void MoveTo(Vector3 target, float reach = DefaultReach, Action andThen = null) {
    Vector3 targetPos = target;
    targetPos.y = transform.position.y;
    _targetPosition = targetPos;
    _targetReach = reach;
    _targetReachedAction = andThen;
  }

  protected void RotateTo(Vector3 target) {
    Vector3 direction = target - transform.position;
    direction.y = 0;
    if (direction.magnitude > 1) {
      direction.Normalize();
    }
    transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
  }
  #endregion
}
