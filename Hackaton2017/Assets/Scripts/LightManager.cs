using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LightManager : MonoBehaviour {
  void Start () {
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
    var lightLights = GetLightLights();
    var darkLights = GetDarkLights();
    foreach (var light in lightLights) {
      light.enabled = false;
    }
    foreach (var light in darkLights) {
      light.enabled = true;
    }
  }

  void SwitchLightsOn() {
    var lightLights = GetLightLights();
    var darkLights = GetDarkLights();
    foreach (var light in lightLights) {
      light.enabled = true;
    }
    foreach (var light in darkLights) {
      light.enabled = false;
    }
  }
}
