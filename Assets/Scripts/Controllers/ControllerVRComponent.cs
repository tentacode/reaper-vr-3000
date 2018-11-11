using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.VR;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class ControllerVRComponent : MonoBehaviour
{
    public SteamVR_TrackedController SteamVR_TrackedController;
    public SteamVR_LaserPointer SteamVR_LaserPointer;

    public GameEvent EventMenuButtonClicked;
    public GameEvent EventMenuButtonUnclicked;
    public GameEvent EventTriggerClicked;
    public GameEvent EventTriggerUnclicked;
    public GameEvent EventSteamClicked;
    public GameEvent EventPadClicked;
    public GameEvent EventPadUnclicked;
    public GameEvent EventPadTouched;
    public GameEvent EventPadUntouched;
    public GameEvent EventGripped;
    public GameEvent EventUngripped;

    private void OnEnable()
    {
		SteamVR_TrackedController.MenuButtonClicked += Event_MenuButtonClicked;
        SteamVR_TrackedController.MenuButtonUnclicked += Event_MenuButtonUnclicked;
        SteamVR_TrackedController.TriggerClicked += Event_TriggerClicked;
        SteamVR_TrackedController.TriggerUnclicked += Event_TriggerUnclicked;
        SteamVR_TrackedController.SteamClicked += Event_SteamClicked;
        SteamVR_TrackedController.PadClicked += Event_PadClicked;
        SteamVR_TrackedController.PadUnclicked += Event_PadUnclicked;
        SteamVR_TrackedController.PadTouched += Event_PadTouched;
        SteamVR_TrackedController.PadUntouched += Event_PadUntouched;
        SteamVR_TrackedController.Gripped += Event_Gripped;
        SteamVR_TrackedController.Ungripped += Event_Ungripped;
    }

    private void OnDisable()
    {
		SteamVR_TrackedController.MenuButtonClicked -= Event_MenuButtonClicked;
        SteamVR_TrackedController.MenuButtonUnclicked -= Event_MenuButtonUnclicked;
        SteamVR_TrackedController.TriggerClicked -= Event_TriggerClicked;
        SteamVR_TrackedController.TriggerUnclicked -= Event_TriggerUnclicked;
        SteamVR_TrackedController.SteamClicked -= Event_SteamClicked;
        SteamVR_TrackedController.PadClicked -= Event_PadClicked;
        SteamVR_TrackedController.PadUnclicked -= Event_PadUnclicked;
        SteamVR_TrackedController.PadTouched -= Event_PadTouched;
        SteamVR_TrackedController.PadUntouched -= Event_PadUntouched;
        SteamVR_TrackedController.Gripped -= Event_Gripped;
        SteamVR_TrackedController.Ungripped -= Event_Ungripped;
    }

    private void Event_MenuButtonClicked(object sender, ClickedEventArgs e)
    {
        if (EventMenuButtonClicked != null)
            EventMenuButtonClicked.Raise();
    }

    private void Event_MenuButtonUnclicked(object sender, ClickedEventArgs e)
    {
        if (EventMenuButtonUnclicked != null)
            EventMenuButtonUnclicked.Raise();
    }

    private void Event_TriggerClicked(object sender, ClickedEventArgs e)
    {
        if (EventTriggerClicked != null)
            EventTriggerClicked.Raise();
    }

    private void Event_TriggerUnclicked(object sender, ClickedEventArgs e)
    {
        if (EventTriggerUnclicked != null)
            EventTriggerUnclicked.Raise();
    }

    private void Event_SteamClicked(object sender, ClickedEventArgs e)
    {
        if (EventSteamClicked != null)
            EventSteamClicked.Raise();
    }

    private void Event_PadClicked(object sender, ClickedEventArgs e)
    {
        if (EventPadClicked != null)
            EventPadClicked.Raise();
    }

    private void Event_PadUnclicked(object sender, ClickedEventArgs e)
    {
        if (EventPadUnclicked != null)
            EventPadUnclicked.Raise();
    }

    private void Event_PadTouched(object sender, ClickedEventArgs e)
    {
        if (EventPadTouched != null)
            EventPadTouched.Raise();
    }

    private void Event_PadUntouched(object sender, ClickedEventArgs e)
    {
        if (EventPadUntouched != null)
            EventPadUntouched.Raise();
    }

    private void Event_Gripped(object sender, ClickedEventArgs e)
    {
        if (EventGripped != null)
            EventGripped.Raise();
    }

    private void Event_Ungripped(object sender, ClickedEventArgs e)
    {
        if (EventUngripped != null)
            EventUngripped.Raise();
    }
}

