using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LightManager : MonoBehaviour {
  #region Private Members
  private Light[] _lightLights;
  private Light[] _darkLights;
  private float _remainingLightCycleTime;
  private bool _lightsOn;
  #endregion

  #region Public Properties
  public float LightCycleDuration;
  public float Leeway;
  public MonsterController Monster;
  #endregion

  #region Unity Callbacks
  private void Start () {
    _lightsOn = false;
    this._darkLights = this.GetDarkLights();
    this._lightLights = this.GetLightLights();
    SwitchLightsOn();
  }

  private void Update() {
    if (_lightsOn) {
      _remainingLightCycleTime -= Time.deltaTime;

      if (_remainingLightCycleTime <= 0) {
        SwitchLightsOff();
      }
    }
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
    _lightsOn = false;
    Monster.WakeUp();
  }

  public void SwitchLightsOn() {
    foreach (var light in this._lightLights) {
      light.enabled = true;
    }
    foreach (var light in this._darkLights) {
      light.enabled = false;
    }
    if (!_lightsOn) {
      _lightsOn = true;
      _remainingLightCycleTime = Random.Range(LightCycleDuration - Leeway, LightCycleDuration + Leeway);
      Monster.Sleep();
      Debug.LogWarningFormat("New light cycle: {0}s", _remainingLightCycleTime);
    }
  }
  #endregion
}
