using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricSwitch : InteractibleItem {
  #region Public Properties
  public LightManager LightManager;
  #endregion

  #region Unity Callbacks
  private void Start() {
    _firstInteraction = false;
    _isInteractionEnabled = true;
  }
  #endregion

  #region Specialized Behavior
  protected override void DoInteraction(PlayerController player, Inventory inventory, PickableItem key) {
    LightManager.SwitchLightsOn();
  }
  #endregion
}
