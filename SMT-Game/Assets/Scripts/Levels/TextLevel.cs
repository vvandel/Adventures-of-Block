using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Assets.Scripts.Parser;
using System.Linq;


public class TextLevel : Level
{

    public string LevelName { get; set; }
    Command[] commands;
    Dictionary<string, object> variables = new Dictionary<string, object>();
    Dictionary<string, ObjectType> types = new Dictionary<string, ObjectType>();
    public void Load(string level)
    {
        var c = LevelParse.LoadLevel(level);
        commands = c.commands;
        baseState = c.baseState;
        State.CurrentLevelState = baseState;
    }

    public float fparse(string f, float d, bool usevar = false)
    {
        if (usevar && variables.ContainsKey(f) && variables[f] is float)
                return (float)variables[f];
        else
        {
            float r = 0;
            bool success = float.TryParse(f, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out r);
            if (success)
                return r;
            else
                return d;
        }
    }
    public float? fparse(string f, float? d, bool usevar = false)
    {
        if (usevar && variables.ContainsKey(f) && (variables[f] is float || variables[f] == null))
            return (float)variables[f];
        else
        {
            float r = 0;
            bool success = float.TryParse(f, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out r);
            if (success)
                return r;
            else
                return d;
        }
    }
    public bool bparse(string b, bool d, bool usevar = false)
    {
        if (usevar && variables.ContainsKey(b) && variables[b] is bool)
            return (bool)variables[b];
        else
        {
            bool r = false;
            bool success = bool.TryParse(b, out r);
            if (success) return r;
            else return d;
        }
    }
    public int iparse(string i, int d, bool usevar = false)
    {
        if (usevar && variables.ContainsKey(i) && variables[i] is int)
            return (int)variables[i];
        else
        {
            int r = 0;
            bool success = int.TryParse(i, out r);
            if (success) return r;
            else return d;
        }
    }

    public string svarorvalue(string s)
    {
        if (variables.ContainsKey(s) && variables[s] is string)
            return (string)variables[s];
        else return s;
    }

    protected GameObject spawnCloud(string[] args)
    {
        float x = 0f, y = 1f;
        float? size = 1f, waitDuration = 2f, thunderDuration = 2f;
        bool halfDuration = false;

        for (int i = 0; i < args.Length; i++)
        {
            if (i == 0) x = fparse(args[i], x, true);
            else if (i == 1) size = fparse(args[i], size, true);
            else if (i == 2) waitDuration = fparse(args[i], waitDuration, true);
            else if (i == 3) thunderDuration = fparse(args[i], thunderDuration, true);
            else if (i == 4) halfDuration = bparse(args[i], halfDuration, true);
        }
        return spawnCloud(x, y, size, waitDuration, thunderDuration, halfDuration);
    }

    int rand(int count)
    {
        int r = Random.Range(0, count);
        return r;
    }


    protected GameObject spawnBomb(string[] args)
    {
        float x = 0, y = 0.05f, explosionDelay = 4;
        float? radius = 4, teleDuration = 2;

        for (int i = 0; i < args.Length; i++)
        {
            if (i == 0) x = fparse(args[i], x, true);
            else if (i == 1) y = fparse(args[i], y, true);
            else if (i == 2) radius = fparse(args[i], radius, true);
            else if (i == 3) explosionDelay = fparse(args[i], explosionDelay, true);
            else if (i == 4) teleDuration = fparse(args[i], teleDuration, true);
        }
        return spawnBomb(x, y, radius, explosionDelay, teleDuration);
    }

    protected GameObject spawnRaft(string[] args)
    {
        float x = 0, y = -1f;
        for (int i = 0; i < args.Length; i++)
        {
            if (i == 0) x = fparse(args[i], x, true);
            else if (i == 1) y = fparse(args[i], y, true);
        }
        return rafts.Spawn(v2(x, y), true);
    }


    protected GameObject spawnStar(string[] args)
    {
        float x = 0, y = 1f;
        for (int i = 0; i < args.Length; i++)
        {
            if (i == 0) x = fparse(args[i], x, true);
            else if (i == 1) y = fparse(args[i], y, true);
        }
        return stars.Spawn(v2(x, y), true);
    }


    object Parse(ObjectType type, params string[] args)
    {
        if(type == ObjectType.Integer)
        {
            if(args.Length == 1)
            {
                return iparse(args[0], 0, true);
            }
        }
        else if(type == ObjectType.Float)
        {
            if(args.Length == 1)
            {
                return fparse(args[0], 0f, true);
            }
        }
        else if (type == ObjectType.String)
        {
            return svarorvalue(args[0]);
        }

        return null;
    }

    void Spawn(ObjectType subject_type, string variable, string[] args)
    {
        GameObject obj;
        if (subject_type == ObjectType.cloud) obj = spawnCloud(args);
        else if (subject_type == ObjectType.bomb) obj = spawnBomb(args);
        else if (subject_type == ObjectType.star) obj = spawnStar(args);
        else if (subject_type == ObjectType.raft) obj = spawnRaft(args);
        else { obj = null; variable = null; }

        if (variable != null)
        {
            variables[variable] = obj;
            types[variable] = subject_type;
        }
    }
    int Execute(Command cmd)
    {
        if (cmd.function == Function.wait)
        {
            if (cmd.arguments.Length == 1)
                return iparse(cmd.arguments[0], 1);
            else return 1;
        }
        else if (cmd.function == Function.flood)
        {
            if (cmd.arguments.Length == 1)
            {
                if (cmd.arguments[0] == "start")
                    StartFlood();
                else if (cmd.arguments[0] == "end")
                    EndFlood();
            }
        }
        else if (cmd.function == Function.spawn)
        {
            Spawn(cmd.subject_type, cmd.variable, cmd.arguments);
            return -1;
        }
        else if (cmd.function == Function.array)
        {
            object[] value = new object[cmd.arguments.Length];
            for (int i = 0; i < cmd.arguments.Length; i++)
            {
                value[i] = Parse(cmd.subject_type, cmd.arguments[i]);
            }

            if (cmd.variable != null)
            {
                types[cmd.variable] = ObjectType.Array(cmd.subject_type);
                variables[cmd.variable] = value;
            }
        }
        else if (cmd.function == Function.select)
        {
            if (cmd.arguments.Length == 2)
            {
                int index = iparse(cmd.arguments[0], 0, true);
                string varname = cmd.arguments[1];
                if (cmd.variable != null)
                {
                    if (variables.ContainsKey(varname))
                    {
                        ObjectType subj_type = ObjectType.UnArray(types[varname]);
                        if (subj_type != null)
                        {
                            types[cmd.variable] = subj_type;
                            variables[cmd.variable] = (variables[varname] as object[])[index];
                        }
                    }
                }
            }

        }
        else if (cmd.function == Function.despawn)
        {
            if (cmd.arguments.Length == 1)
            {
                try
                {
                    GameObject obj = variables[cmd.arguments[0]] as GameObject;
                    ObjectPool.Despawn(obj, types[cmd.arguments[0]].ToString() + " pool");
                }
                catch { }
                return -1;
            }
        }
        else if (cmd.function == Function.choose)
        {
            if (cmd.subject_type == ObjectType.Integer)
            {
                int option = rand(cmd.arguments.Length);
                Debug.Log(option);
                int value = iparse(cmd.arguments[option], 0, true);
                if (cmd.variable != null)
                {
                    types[cmd.variable] = cmd.subject_type;
                    variables[cmd.variable] = value;
                }
            }
            else if (cmd.subject_type == ObjectType.Float)
            {
                int option = rand(cmd.arguments.Length);
                float value = fparse(cmd.arguments[option], 0, true);
                if (cmd.variable != null)
                {
                    types[cmd.variable] = cmd.subject_type;
                    variables[cmd.variable] = value;
                }
            }
        }
        else if (cmd.function == Function.Undefined)
        {
            if (cmd.variable != null)
            {
                if (cmd.subject_type.IsIn(ObjectType.Integer, ObjectType.Float, ObjectType.String))
                {
                    types[cmd.variable] = cmd.subject_type;
                    variables[cmd.variable] = Parse(cmd.subject_type, cmd.arguments);
                }
            }
        }

        return -1;
    }


    protected override IEnumerator LevelScript()
    {
        Load(LevelName);
        Random.InitState(System.DateTime.Now.Millisecond);
        PlayBackground();
        HasLoaded = true;
        foreach (var cmd in commands)
        {
            int waitTime = Execute(cmd);
            if (waitTime > 0)
                yield return WaitTick(waitTime);
        }
        yield return base.LevelScript();
    }
}
