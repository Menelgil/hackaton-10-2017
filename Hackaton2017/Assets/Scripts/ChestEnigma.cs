using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestEnigma : InteractibleItem {
  #region Public Properties
  public PickableItem QuestItemPrefab;
  #endregion

  #region Specialized Behavior
  protected override void DoInteraction(PlayerController player, Inventory inventory, PickableItem key) {
    key.transform.parent = this.transform;
    GameObject.Destroy(key);

    PickableItem questItem = Instantiate(QuestItemPrefab, player.transform);
    inventory.GrabItem(questItem);

    _isInteractionEnabled = false;
  }
  #endregion
}
