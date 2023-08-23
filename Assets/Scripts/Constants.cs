// Constants used across the game.
public static class Constants
{
    // Camera constants.
    public const float cameraSpeed = 3.0f;
    public const float minCameraOrthoSize = 1.0f;
    public const float maxCameraOrthoSize = 10.0f;

    // Map constants.
    public const float mapRangeSize = 20.0f;
    public const float camRangeSize = mapRangeSize + 1.0f;
    public const float minPlanetDistance = 2.0f;
    public const int numPlanets = 60;

    // Planetary capacity.
    public const int defaultShipCapacity = 100;
    public const int randomShipCapacityRange = 30;
    public const int minimumCapacity = 10;

    public const int greatHabitabilityScore = 50;
    public const int normalHabitabilityScore = 20;
    public const int poorHabitabilityScore = 0;
    public const int awfulHabitabilityScore = -20;

    public const int smallPositiveMod = 10;
    public const int largePositiveMod = 30;
    public const int smallNegativeMod = -10;
    public const int largeNegativeMod = -30;

    // Planetary modifiers
    public const int maxAllowedModifiers = 3;
    public const int maxModifierCategories = 9;

    // Planetary production rate.
    public const float timeToFullProduction = 30.0f;
}
