using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour {
  #region Private Members
  private Renderer _renderer;
  #endregion

  #region Unity Callbacks
  // Use this for initialization
  private void Start () {
    _renderer = GetComponent<Renderer>();
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
  }
  #endregion
}
