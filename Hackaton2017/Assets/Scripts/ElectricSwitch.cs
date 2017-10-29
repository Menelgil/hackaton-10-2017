using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricSwitch : InteractibleItem {
  #region Public Properties
  public LightManager LightManager;
  #endregion

  #region Public Interface
  public override void InteractWith(PlayerController player) {
    LightManager.SwitchLightsOn();
  }
  #endregion
}
