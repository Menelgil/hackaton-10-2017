using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ritual : MonoBehaviour {
  public int Countdown { get; private set; }
  public Material[] Materials;
  private MeshRenderer _renderer;

	// Use this for initialization
	private void Start () {
    this.Countdown = 6;
    this._renderer = this.GetComponent<MeshRenderer>();
	}

	// Update is called once per frame
	private void Update () {
	}

  public int NextStep () {
    if (this.Countdown > 0)
      this.Countdown -= 1;
    this._renderer.material = this.Materials[this.Countdown];
    return this.Countdown;
  }

  public int PreviousStep () {
    if (this.Countdown < 5)
      this.Countdown += 1;
    this._renderer.material = this.Materials[this.Countdown];
    return this.Countdown;
  }
}
