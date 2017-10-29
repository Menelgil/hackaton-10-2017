using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricSwitch : InteractibleItem {
  #region Public Properties
  public LightManager LightManager;
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
  protected override void DoInteraction() {
    LightManager.SwitchLightsOn();
  }
  #endregion
}
