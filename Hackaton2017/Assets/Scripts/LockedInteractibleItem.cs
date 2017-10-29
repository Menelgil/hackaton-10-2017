using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LockedInteractibleItem : InteractibleItem
{
  #region Protected Members
  protected bool _isInteractionEnabled;
  protected bool _firstInteraction;
  #endregion

  #region Public Properties
  public PickableItem ExpectedKey;
  #endregion

  #region Unity Callbacks
  // Use this for initialization
  private void Start () {
    _firstInteraction = true;
    _isInteractionEnabled = false;
	}
	
	// Update is called once per frame
	private void Update () {
	}
  #endregion

  #region Specialized Behavior
  protected abstract void DoInteraction(PlayerController player, Inventory inventory, PickableItem key);
  #endregion

  #region Public Interface
  public override void InteractWith(PlayerController player) {
    if (_firstInteraction) {
      _firstInteraction = false;
      _isInteractionEnabled = true;
      
      if (ExpectedKey != null) {
        ExpectedKey.IsPickable = true;
        Debug.LogFormat("I need a {0} to use this {1}", ExpectedKey.name, this.name);
      }
    }
    else if (_isInteractionEnabled) {
      Inventory inv = player.GetComponent<Inventory>();

      if (ExpectedKey == null) {
        DoInteraction(player, inv, null);
      }
      else {
        PickableItem key = inv.ReleaseItem();
        if (key == null) { // no key provided
          Debug.LogFormat("I need a {0} to use this {1}", ExpectedKey.name, this.name);
        }
        else if (ExpectedKey.GetInstanceID() != key.GetInstanceID()) { // invalid key
          inv.GrabItem(key);
          Debug.LogFormat("I can't use a {0} with this {1}", key.name, this.name);
        }
        else {
          DoInteraction(player, inv, key);
        }
      }
    }
  }
  #endregion
}
