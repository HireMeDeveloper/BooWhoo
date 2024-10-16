using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public void OnEnter(PlayerInteraction playerInteraction);
    public void OnExit(PlayerInteraction playerInteraction);
    public void OnInteract(PlayerInteraction playerInteraction);
}
