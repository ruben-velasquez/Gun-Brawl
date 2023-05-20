[System.Serializable]
public class ComputerActions
{
    public bool up = false;
    public bool down = false;
    public bool left = false;
    public bool right = false;
    public bool jump = false;
    public bool followjump = true;
    public bool punch = false;
    public bool shoot = false;
    public bool interact = false;

    public void Reset() {
        up = false;
        down = false;
        left = false;
        right = false;
        jump = false;
        punch = false;
        shoot = false;
        interact = false;
    }
}