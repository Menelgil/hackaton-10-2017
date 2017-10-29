using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
  #region Private Members
  private PickableItem _item;
  #endregion

  #region Public Properties
  public bool IsEmpty {
    get {
      return _item == null;
    }
  }
  #endregion

  #region Unity Callbacks
  // Use this for initialization
  private void Start () {
    this._item = null;
  }
  #endregion

  #region Public Interface
  public bool GrabItem(PickableItem item) {
    if (this._item != null)
      return false;
    this._item = item;
    return true;
  }

  public PickableItem ReleaseItem() {
    var item = this._item;
    this._item = null;
    return item;
  }
  #endregion
}
