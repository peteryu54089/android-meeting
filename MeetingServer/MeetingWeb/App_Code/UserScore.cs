using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UserScore
/// </summary>
public class UserScore
{
    public UserScore()
    { }

    public UserScore(string id, int score)
    {
        this.id = id;
        this.score = score;
    }

    private string id;
    public string Id
    {
        get { return id; }
        set { id = value; }
    }

    private int score;
    public int Score
    {
        get { return score; }
        set { score = value; }
    }
}