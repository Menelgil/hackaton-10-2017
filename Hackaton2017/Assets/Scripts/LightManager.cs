using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LightManager : MonoBehaviour {
  private Light[] _lightLights;
  private Light[] _darkLights;

  void Start () {
    this._darkLights = this.GetDarkLights();
    this._lightLights = this.GetLightLights();
  }

  void Update () {
  }

  Light[] GetDarkLights () {
    var lightObjects = GameObject.FindGameObjectsWithTag("Dark Light");
    var lights = lightObjects.Select(obj => obj.GetComponent<Light>());
    return lights.ToArray();
  }

  Light[] GetLightLights () {
    var lightObjects = GameObject.FindGameObjectsWithTag("Light Light");
    var lights = lightObjects.Select(obj => obj.GetComponent<Light>());
    return lights.ToArray();
  }

  void SwitchLightsOff() {
    foreach (var light in this._lightLights) {
      light.enabled = false;
    }
    foreach (var light in this._darkLights) {
      light.enabled = true;
    }
  }

  void SwitchLightsOn() {
    foreach (var light in this._lightLights) {
      light.enabled = true;
    }
    foreach (var light in this._darkLights) {
      light.enabled = false;
    }
  }
}
