using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ritual : InteractibleItem {
  #region Public Properties
  public int Countdown { get; private set; }
  public Material[] Materials;
  #endregion

  #region Private Members
  private MeshRenderer _renderer;
  #endregion

  #region Unity Callbacks
  // Use this for initialization
  private void Start () {
    this.Countdown = Materials.Length;
    this._renderer = this.GetComponent<MeshRenderer>();
	}
  #endregion

  #region Public Interface
  public override void InteractWith(PlayerController player) {
    Inventory inv = player.GetComponent<Inventory>();
    PickableItem carriedItem = inv.ReleaseItem();
    QuestItem questItem = carriedItem.GetComponent<QuestItem>();

    if (questItem == null) {
      inv.GrabItem(carriedItem);
      Debug.LogFormat("This {0} is useless for the ritual.", carriedItem.name);
    } else {
      questItem.transform.parent = this.transform;
      GameObject.Destroy(questItem);

      if (NextStep() == 0) {
        Debug.LogWarning("WE HAVE A WINNER");
      }
    }
  }

  public int NextStep() {
    if (this.Countdown > 0)
      this.Countdown -= 1;
    this._renderer.material = this.Materials[this.Countdown];
    return this.Countdown;
  }

  public int PreviousStep() {
    if (this.Countdown < 5)
      this.Countdown += 1;
    this._renderer.material = this.Materials[this.Countdown];
    return this.Countdown;
  }
  #endregion
}
