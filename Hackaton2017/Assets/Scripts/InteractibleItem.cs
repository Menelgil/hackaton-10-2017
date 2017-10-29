using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractionResult {
  Success,
  MissingKey,
  InvalidKey
}

public abstract class InteractibleItem : MonoBehaviour {
  #region Protected Members
  protected bool _isInteractionEnabled;
  #endregion

  #region Public Properties
  public PickableItem ExpectedKey;
  #endregion

  #region Unity Callbacks
  // Use this for initialization
  private void Start () {
    _isInteractionEnabled = true;
	}
	
	// Update is called once per frame
	private void Update () {
	}
  #endregion

  #region Specialized Behavior
  protected abstract void DoInteraction(PlayerController player, Inventory inventory, PickableItem key);
  #endregion

  #region Public Interface
  public InteractionResult Interact(PlayerController player) {
    if (_isInteractionEnabled) {
      Inventory inv = player.GetComponent<Inventory>();

      if (ExpectedKey == null) {
        DoInteraction(player, inv, null);
        return InteractionResult.Success;
      }
      else {
        PickableItem key = inv.ReleaseItem();
        if (key == null) {
          return InteractionResult.MissingKey;
        }
        else if (ExpectedKey.GetInstanceID() != key.GetInstanceID()) {
          inv.GrabItem(key);
          return InteractionResult.InvalidKey;
        }

        DoInteraction(player, inv, key);
        return InteractionResult.Success;
      }
    }
    return InteractionResult.Success;
  }
  #endregion
}
