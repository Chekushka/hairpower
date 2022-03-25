﻿using UnityEngine;
using System.Collections;

// Namespace
namespace RayFire
{
    // Event
    public class RFEvent
    {
        // Rigid Delegate & events
        public delegate void     EventAction(RayfireRigid rigid);
        public event EventAction LocalEvent;
        
        // RigidRoot Delegate & events
        public delegate void         EventActionRoot(RayfireRigidRoot root);
        public event EventActionRoot LocalEventRoot;
        
        // Local Rigid
        public void InvokeLocalEvent(RayfireRigid rigid)
        {
            if (LocalEvent != null)
                LocalEvent.Invoke(rigid);
        }
        
        // Local RigidRoot Shard
        public void InvokeLocalEventRoot(RayfireRigidRoot root)
        {
            if (LocalEventRoot != null)
                LocalEventRoot.Invoke(root);
        }
    }
    
    // Demolition Event
    public class RFDemolitionEvent : RFEvent
    {
        // Delegate & events
        public static event EventAction GlobalEvent;
        
        // Demolition event
        public static void InvokeGlobalEvent(RayfireRigid rigid)
        {
            if (GlobalEvent != null)
                GlobalEvent.Invoke(rigid);
        }
    }
    
    // Activation Event
    public class RFActivationEvent : RFEvent
    {
        // Delegate & events
        public static event EventAction     GlobalEvent;
        public static event EventActionRoot GlobalEventRoot;
        
        // Activation event
        public static void InvokeGlobalEvent(RayfireRigid rigid)
        {
            if (GlobalEvent != null)
                GlobalEvent.Invoke(rigid);
        }
        
        // Activation event
        public static void InvokeGlobalEventRoot(RayfireRigidRoot root)
        {
            if (GlobalEventRoot != null)
                GlobalEventRoot.Invoke(root);
        }
    }
    
    // Restriction Event
    public class RFRestrictionEvent : RFEvent
    {
        // Delegate & events
        public static event EventAction GlobalEvent;

        // Restriction event
        public static void InvokeGlobalEvent(RayfireRigid rigid)
        {
            if (GlobalEvent != null)
                GlobalEvent.Invoke(rigid);
        }
    }
    
    // Shot Event
    public class RFShotEvent
    {
        // Delegate & events
        public delegate void EventAction(RayfireGun gun);
        public static event EventAction GlobalEvent;
        public event EventAction LocalEvent;
       
        // Global
        public static void InvokeGlobalEvent(RayfireGun gun)
        {
            if (GlobalEvent != null)
                GlobalEvent.Invoke(gun);
        }
        
        // Local
        public void InvokeLocalEvent(RayfireGun gun)
        {
            if (LocalEvent != null)
                LocalEvent.Invoke(gun);
        }
    }

    // Explosion Event
    public class RFExplosionEvent
    {
        // Delegate & events
        public delegate void EventAction(RayfireBomb bomb);
        public static event EventAction GlobalEvent;
        public event EventAction LocalEvent;
       
        // Global
        public static void InvokeGlobalEvent(RayfireBomb bomb)
        {
            if (GlobalEvent != null)
                GlobalEvent.Invoke(bomb);
        }
        
        // Local
        public void InvokeLocalEvent(RayfireBomb bomb)
        {
            if (LocalEvent != null)
                LocalEvent.Invoke(bomb);
        }
    }
    
    // Slice Event
    public class RFSliceEvent
    {
        // Delegate & events
        public delegate void EventAction(RayfireBlade blade);
        public static event EventAction GlobalEvent;
        public event EventAction LocalEvent;
       
        // Global
        public static void InvokeGlobalEvent(RayfireBlade blade)
        {
            if (GlobalEvent != null)
                GlobalEvent.Invoke(blade);
        }
        
        // Local
        public void InvokeLocalEvent(RayfireBlade blade)
        {
            if (LocalEvent != null)
                LocalEvent.Invoke(blade);
        }
    }
    
    // Connectivity Event
    public class RFConnectivityEvent
    {
        // Delegate & events
        public delegate void            EventAction(RayfireConnectivity connectivity);
        public static event EventAction GlobalEvent;
        public event        EventAction LocalEvent;
       
        // Global
        public static void InvokeGlobalEvent(RayfireConnectivity connectivity)
        {
            if (GlobalEvent != null)
                GlobalEvent.Invoke(connectivity);
        }
        
        // Local
        public void InvokeLocalEvent(RayfireConnectivity connectivity)
        {
            if (LocalEvent != null)
                LocalEvent.Invoke(connectivity);
        }
    }
}