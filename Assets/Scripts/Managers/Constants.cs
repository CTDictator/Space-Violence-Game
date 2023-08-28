using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
    // Planet name generator.
    public const int minSyllables = 2;
    public const int maxSyllables = 3;

    // Planetary modifiers.
    public const int maxAllowedModifiers = 3;
    public const int minAllowedModifiers = 1;

    // Planetary capacity and rate.
    public const int defaultShipCapacity = 100;
    public const int randomShipCapacityRange = 30;
    public const int minimumCapacity = 20;
    public const int timeTilFullProsperity = 30;
    public const float prosperityGainRate = 1.0f / timeTilFullProsperity;
    public const float timeToFullProduction = 30.0f;

    // Camera constants.
    public const float cameraSpeed = 3.0f;
    public const float minCameraOrthoSize = 1.0f;
    public const float maxCameraOrthoSize = 10.0f;
    public const float aspectRatio = 16.0f / 9.0f;
    public const float camRangeSize = mapRangeSize + 1.0f;
    public const float minDragDistance = 1.0f;

    // Map constants.
    public const float mapRangeSize = 20.0f;
    public const float minPlanetDistance = 2.0f;
    public const int numPlanets = 60;
    public const int numEmpires = 4;

    // Ship constants.
    public const int shipStrength = 1;
    public const float shipSpeed = 5.0f;

    // AI constants.
    public const float aiCheckFrequency = 1.0f;
    public const float planetScanRadius = 10.0f;
    public const int minAttackers = 2;
    public const int maxAttackers = 6;
}