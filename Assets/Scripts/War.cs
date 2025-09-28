using System.Collections.Generic;

[System.Serializable]
public class War
{
    public string name;
    public List<string> offenders;
    public List<string> defenders;

    public War(string name, List<string> offenders, List<string> defenders)
    {
        this.name = name;
        this.offenders = offenders;
        this.defenders = defenders;
    }
}
