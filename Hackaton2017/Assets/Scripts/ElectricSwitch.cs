using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricSwitch : InteractibleItem {
  #region Public Properties
  public LightManager LightManager;
  #endregion

  #region Specialized Behavior
  protected override void DoInteraction(PlayerController player, Inventory inventory, PickableItem key) {
    LightManager.SwitchLightsOn();
  }
  #endregion
}
