using System;
using Unity.VisualScripting;
using UnityEngine;

public interface IInteracteble
{
 public void Interact();

 public virtual string GeTNameInteraction() => "";
}



