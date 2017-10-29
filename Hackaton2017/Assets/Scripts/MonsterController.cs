using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : EntityController
{
  #region Private Members
  private bool _isAsleep;
  private PlayerController _player;
  private bool _carryRitualPiece;
  #endregion

  #region Public Properties
  public float AttackReach;
  public Ritual DarkRitual;
  #endregion

  #region Unity Callbacks
  // Use this for initialization
  protected override void Start() {
    base.Start();
    _isAsleep = true;
    _carryRitualPiece = false;
    _player = GameObject.FindObjectOfType<PlayerController>();
  }

  // Update is called once per frame
  protected override void Update() {
    if (_isAsleep)
      return;
    // we need to attack the player to collect dark ritual pieces
    // if we don't already carry a piece we go to the player and attack him
    if (!_carryRitualPiece) {
      RotateTo(_player.transform.position);
      MoveTo(target: _player.transform.position, reach: AttackReach, andThen: AttackPlayer);
    }
    else { // we've got a ritual piece, go to the ritual
      RotateTo(DarkRitual.transform.position);
      MoveTo(target: DarkRitual.transform.position, reach: DefaultReach, andThen: UpdateDarkRitual);
    }

    base.Update();
  }
  #endregion

  #region Private Methods
  private void AttackPlayer() {
    Debug.LogWarning("Thud!");
    _carryRitualPiece = true;
  }

  private void UpdateDarkRitual() {
    _carryRitualPiece = false;
    if (DarkRitual.NextStep() == 0) {
      Debug.LogWarning("WE HAVE A LOOSER");
    }
  }
  #endregion

  #region Public Interface
  public void WakeUp() {
    _isAsleep = false;
    Debug.LogWarning("GROAAAAR!!!!");
  }

  public void Sleep() {
    _isAsleep = true;
    Debug.LogWarning("Zzzz...");
  }
  #endregion
}
