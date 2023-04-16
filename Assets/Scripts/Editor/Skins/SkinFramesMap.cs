using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class SkinFramesMap
{
    public static string[] frames = {
        "Idle_1",
        "Idle_2",
        "Idle_3",
        "Idle_4",

        "Walking_1",
        "Walking_2",
        "Walking_3",
        "Walking_4",
        "Walking_5",

        "Punching_1",
        "Punching_2",
        "Punching_3",
        "Punching_4",

        "Jumping_1",
        "Jumping_2",
        "Jumping_3",
        "Jumping_4",
        
        "Climbing_1",
        "Climbing_2",
        "Climbing_3",

        "Pistol_Horizontal_Attack_1",
        "Pistol_Horizontal_Attack_2",

        "Pistol_Up_Attack_1",
        "Pistol_Up_Attack_2",
        
        "Pistol_Down_Attack_1",
        "Pistol_Down_Attack_2",
        
        "M4_Horizontal_Attack_1",
        "M4_Horizontal_Attack_2",

        "M4_Up_Attack_1",
        "M4_Up_Attack_2",

        "M4_Down_Attack_1",
        "M4_Down_Attack_2",
        
        "Revolver_Horizontal_Attack_1",
        "Revolver_Horizontal_Attack_2",

        "Revolver_Up_Attack_1",
        "Revolver_Up_Attack_2",

        "Revolver_Down_Attack_1",
        "Revolver_Down_Attack_2",
    };

    public static string[,] animations = {
        {
            "Idle_1",
            "Idle_2",
            "Idle_3",
            "Idle_4",
            null,
            null,
            null,
        },
        {
            "Walking_1",
            "Walking_2",
            "Walking_3",
            "Walking_4",
            "Walking_5",
            "Walking_4",
            "Walking_3",
        },
        {
            "Punching_1",
            "Punching_2",
            "Punching_3",
            "Punching_4", 
            null,
            null,
            null,
        },
        {
            "Jumping_1",
            "Jumping_2",
            "Jumping_3",
            "Jumping_4",
            null,
            null,
            null,
        }, 
        {
            "Jumping_4",
            null,
            null,
            null,
            null,
            null,
            null,
        },
        {
            "Climbing_1",
            "Climbing_2",
            "Climbing_1",
            "Climbing_3", 
            null,
            null,
            null,
        },
        {
            "Pistol_Horizontal_Attack_1",
            "Pistol_Horizontal_Attack_2",
            "Pistol_Horizontal_Attack_1",
            null,
            null,
            null,
            null,
        },
        {
            "Pistol_Up_Attack_1",
            "Pistol_Up_Attack_2",
            "Pistol_Up_Attack_1",
            null,
            null,
            null,
            null,
        },
        {
            "Pistol_Down_Attack_1",
            "Pistol_Down_Attack_2",
            "Pistol_Down_Attack_1",
            null,
            null,
            null,
            null,
        },
        {
            "M4_Horizontal_Attack_1",
            "M4_Horizontal_Attack_2",
            "M4_Horizontal_Attack_1",
            null,
            null,
            null,
            null,
        },
        {
            "M4_Up_Attack_1",
            "M4_Up_Attack_2",
            "M4_Up_Attack_1",
            null,
            null,
            null,
            null,
        },
        {
            "M4_Down_Attack_1",
            "M4_Down_Attack_2",
            "M4_Down_Attack_1",
            null,
            null,
            null,
            null,
        },
        {
            "Revolver_Horizontal_Attack_1",
            "Revolver_Horizontal_Attack_2",
            "Revolver_Horizontal_Attack_1",
            null,
            null,
            null,
            null,
        },
        {
            "Revolver_Up_Attack_1",
            "Revolver_Up_Attack_2",
            "Revolver_Up_Attack_1",
            null,
            null,
            null,
            null,
        },
        {
            "Revolver_Down_Attack_1",
            "Revolver_Down_Attack_2",
            "Revolver_Down_Attack_1",
            null,
            null,
            null,
            null,
        },
    };
    public static string[] animationsNames = {
        "Idle",
        "Walk",
        "Punch",
        "Jump",
        "Air",
        "Climb",

        "Pistol_Horizontal_Attack",
        "Pistol_Up_Attack",
        "Pistol_Down_Attack",

        "M4_Horizontal_Attack",
        "M4_Up_Attack",
        "M4_Down_Attack",

        "Revolver_Horizontal_Attack",
        "Revolver_Up_Attack",
        "Revolver_Down_Attack",
    };
} 