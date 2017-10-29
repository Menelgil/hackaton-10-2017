using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour {
  #region Private Members
  private Renderer _renderer;
  private Collider _collider;
  #endregion

  #region Public Properties
  public bool IsPickable { get; set; }
  #endregion

  #region Unity Callbacks
  // Use this for initialization
  private void Start () {
    IsPickable = false;
    _renderer = GetComponent<Renderer>();
    _collider = GetComponent<Collider>();
  }

  // Update is called once per frame
  private void Update () {
  }
  #endregion

  #region Public Interface
  public void PickedBy(Transform grabber) {
    if (IsPickable) {
      transform.parent = grabber;
      _renderer.enabled = false;
      _collider.enabled = false;
    }
  }
  #endregion
}
