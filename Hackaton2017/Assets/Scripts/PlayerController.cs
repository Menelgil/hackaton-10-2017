using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : EntityController {
  #region Public Properties
  public float InteractingDistance;
  #endregion

  #region Private Members
  private Inventory _inventory;
  #endregion

  #region Unity Callbacks
  // Use this for initialization
  protected override void Start() {
    base.Start();
    _inventory = GetComponent<Inventory>();
  }

  // Update is called once per frame
  protected override void Update() {
    if (Input.GetButton("Fire1")) {
      RaycastHit hit;
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      if (Physics.Raycast(ray, out hit, 100)) {
        PickableItem pickable = hit.collider.GetComponent<PickableItem>();
        InteractibleItem interactible = hit.collider.GetComponent<InteractibleItem>();

        if (pickable != null) {
          RotateTo(pickable.transform.position);
          MoveTo(target: pickable.transform.position, reach: InteractingDistance, andThen: () => {
            PickItem(pickable);
          });
        } else if (interactible != null) {
          RotateTo(interactible.transform.position);
          MoveTo(target: interactible.transform.position, reach: InteractingDistance, andThen: () => {
            InteractWithItem(interactible);
          });
        } else {
          RotateTo(hit.point);
          MoveTo(target: hit.point);
        }
      }
    }

    base.Update();
  }
  #endregion

  #region Private Methods
  private void PickItem(PickableItem item) {
    if (!item.IsPickable) {
      // we don't know what the item will be used for
      Debug.Log("This will probably be usefull later.");
    } else if (!_inventory.GrabItem(item)) {
      // we already carry something
      Debug.Log("My hands are full, I can't pick this up.");
    } else {
      item.PickedBy(this.transform);
      Debug.LogFormat("I might need this.");
    }
  }

  private void InteractWithItem(InteractibleItem item) {
    item.InteractWith(this);
  }
  #endregion
}
