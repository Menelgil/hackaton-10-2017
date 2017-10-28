using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour {
  #region Private Members
  private Renderer _renderer;
  private Collider _collider;
  #endregion

  #region Unity Callbacks
  // Use this for initialization
  private void Start () {
    _renderer = GetComponent<Renderer>();
    _collider = GetComponent<Collider>();
  }

  // Update is called once per frame
  private void Update () {
  }
  #endregion

  #region Public Interface
  public void PickedBy(Transform grabber) {
    Debug.Log(string.Format("{0} picked by {1}", transform.name, grabber.name));
    transform.parent = grabber;
    _renderer.enabled = false;
    _collider.enabled = false;
  }
  #endregion
}
