using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractibleItem : MonoBehaviour {
  public abstract void InteractWith(PlayerController player);
}
