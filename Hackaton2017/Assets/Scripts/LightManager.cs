using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LightManager : MonoBehaviour {
  #region Private Members
  private Light[] _lightLights;
  private Light[] _darkLights;
  #endregion

  #region Unity Callbacks
  private void Start () {
    this._darkLights = this.GetDarkLights();
    this._lightLights = this.GetLightLights();
  }

  private void Update () {
  }
  #endregion

  #region Private Methods
  private Light[] GetDarkLights () {
    var lightObjects = GameObject.FindGameObjectsWithTag("Dark Light");
    var lights = lightObjects.Select(obj => obj.GetComponent<Light>());
    return lights.ToArray();
  }

  private Light[] GetLightLights () {
    var lightObjects = GameObject.FindGameObjectsWithTag("Light Light");
    var lights = lightObjects.Select(obj => obj.GetComponent<Light>());
    return lights.ToArray();
  }
  #endregion

  #region Public Interface
  public void SwitchLightsOff() {
    foreach (var light in this._lightLights) {
      light.enabled = false;
    }
    foreach (var light in this._darkLights) {
      light.enabled = true;
    }
  }

  public void SwitchLightsOn() {
    foreach (var light in this._lightLights) {
      light.enabled = true;
    }
    foreach (var light in this._darkLights) {
      light.enabled = false;
    }
  }
  #endregion
}
