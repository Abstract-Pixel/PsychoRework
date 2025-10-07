using CustomInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputTask : TutorialTask
{
    [SerializeField] 
    private InputActionReference[] requiredActions;
    
    [SerializeField] bool requireAllActions;
    [SerializeField][ShowIf(nameof(requireAllActions))] bool simultaneousActions = false;
    [Resettable] bool cancelled = false;

    
    protected override void Start()
    {
        base.Start();
        foreach (InputActionReference action in requiredActions)
        {
            action.action.Enable();
        }
    }

    private void Update()
    {
        if (cancelled || !started) { return; }
        foreach (InputActionReference action in requiredActions)
        {
            action.action.performed += CompleteAction;
        }
    }

    private void CompleteAction(InputAction.CallbackContext context)
    {
        Debug.Log($"{context.action} performed");
        CompleteTask();
        cancelled = true;    
    }

    public override void CancelTask()
    {
        cancelled = true;
    }
    
}