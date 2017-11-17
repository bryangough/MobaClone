//

//This is for dividing the teams
//The display will have friends as blue and enemies as red
public enum Team
{
    Left,
    Right,
    Neutral,
    None
}
public enum TARGET_TYPE
{
    MYTEAM,
    OTHERTEAM,
    BOTH
}

public enum MOVEMENT_STATE
{
    IDLE,
    TARGET,
    LOCATION
}
public enum AI_STATE
{
    IDLE,
    WANDER,
    CHASE,
    HURT,
    SIT,
    WAKING
}
public enum POWER_TYPE
{   
    offensive,
    support
}

public enum Damage_TYPE
{
    PHYSICAL
}
