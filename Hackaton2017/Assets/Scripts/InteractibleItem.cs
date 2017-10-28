using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractionResult {
  Success,
  MissingKey,
  InvalidKey
}

public abstract class InteractibleItem : MonoBehaviour {
  #region Public Properties
  public PickableItem ExpectedKey;
  #endregion

  #region Unity Callbacks
  // Use this for initialization
  private void Start () {
	}
	
	// Update is called once per frame
	private void Update () {
	}
  #endregion

  #region Specialized Behavior
  protected abstract void DoInteraction();
  #endregion

  #region Public Interface
  public InteractionResult Interact(PickableItem key) {
    if (ExpectedKey != null && key == null) {
      return InteractionResult.MissingKey;
    } else if (ExpectedKey != null && key != null && ExpectedKey.GetInstanceID() != key.GetInstanceID()) {
      return InteractionResult.InvalidKey;
    }

    DoInteraction();
    return InteractionResult.Success;
  }
  #endregion
}
