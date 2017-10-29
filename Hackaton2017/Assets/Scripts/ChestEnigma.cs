using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestEnigma : InteractibleItem {
  #region Public Properties
  public PickableItem QuestItem;
  #endregion

  #region Specialized Behavior
  protected override void DoInteraction(PlayerController player, Inventory inventory, PickableItem key) {
    key.transform.parent = this.transform;
    GameObject.Destroy(key);

    QuestItem.transform.parent = player.transform;
    inventory.GrabItem(QuestItem);

    _isInteractionEnabled = false;
    Debug.LogFormat("Nice! I can use this {0} to prepare the exorcism ritual!", QuestItem.name);
  }
  #endregion
}
